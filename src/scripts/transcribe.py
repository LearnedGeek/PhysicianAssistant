"""
Meeting Transcription — Whisper large-v3 + Speaker Diarization
Handles Spanish/English code-switching for Infanzia/DrOk meetings.

Usage:
    python transcribe.py meeting.mp4
    python transcribe.py meeting.mp4 --output transcript.txt
    python transcribe.py meeting.mp4 --srt
    python transcribe.py meeting.mp4 --diarize --speakers 3
    python transcribe.py meeting.mp4 --groq --diarize --speakers 3
"""

import argparse
import json
import os
import subprocess
import sys
from pathlib import Path

# Monkey-patches for pyannote 3.x + latest torchaudio/huggingface_hub compatibility
# 1. torchaudio 2.11 removed list_audio_backends() and AudioMetaData
#    but pyannote 3.x still uses both at import time
try:
    import torchaudio
    if not hasattr(torchaudio, "list_audio_backends"):
        torchaudio.list_audio_backends = lambda: ["soundfile"]
    if not hasattr(torchaudio, "AudioMetaData"):
        from dataclasses import dataclass
        @dataclass
        class _AudioMetaData:
            sample_rate: int = 0
            num_frames: int = 0
            num_channels: int = 0
            bits_per_sample: int = 0
            encoding: str = ""
        torchaudio.AudioMetaData = _AudioMetaData
except ImportError:
    pass

# 2. huggingface_hub removed use_auth_token but pyannote 3.x still passes it
try:
    import huggingface_hub.file_download as _hf_dl
    _orig_hf_hub_download = _hf_dl.hf_hub_download
    def _patched_hf_hub_download(*args, **kwargs):
        kwargs.pop("use_auth_token", None)
        return _orig_hf_hub_download(*args, **kwargs)
    _hf_dl.hf_hub_download = _patched_hf_hub_download

    import huggingface_hub
    huggingface_hub.hf_hub_download = _patched_hf_hub_download
except (ImportError, AttributeError):
    pass

# Ensure CUDA libraries from pip packages are discoverable.
# Must happen before any ctranslate2/faster_whisper import.
_nvidia_path = Path(sys.prefix) / "Lib" / "site-packages" / "nvidia"
if not _nvidia_path.exists():
    _nvidia_path = Path.home() / "AppData" / "Roaming" / "Python" / f"Python{sys.version_info.major}{sys.version_info.minor}" / "site-packages" / "nvidia"
if _nvidia_path.exists():
    for lib_dir in _nvidia_path.glob("*/bin"):
        os.add_dll_directory(str(lib_dir))
        os.environ["PATH"] = str(lib_dir) + os.pathsep + os.environ.get("PATH", "")


def _convert_audio(audio_path, suffix, sample_rate=16000, mono=True, bitrate=None):
    """Convert audio to a specific format via FFmpeg."""
    out_path = audio_path.with_suffix(suffix)
    if out_path.exists():
        out_path.unlink()
    cmd = ["ffmpeg", "-i", str(audio_path)]
    if sample_rate:
        cmd += ["-ar", str(sample_rate)]
    if mono:
        cmd += ["-ac", "1"]
    if bitrate:
        cmd += ["-b:a", bitrate]
    cmd += ["-y", str(out_path)]
    subprocess.run(cmd, capture_output=True)
    if not out_path.exists():
        print("Error: FFmpeg conversion failed")
        sys.exit(1)
    return out_path


def _run_diarization(audio_path, num_speakers=None):
    """Run pyannote speaker diarization and return list of (start, end, speaker) tuples."""
    try:
        from pyannote.audio import Pipeline
    except ImportError:
        print("pyannote.audio not installed. Run: pip install pyannote.audio")
        sys.exit(1)

    # Convert to WAV first (avoids torchcodec issues on Windows)
    wav_path = _convert_audio(audio_path, ".diarize.wav")

    print("Loading diarization model...")

    # torch 2.6+ defaults weights_only=True; pyannote models from HuggingFace are trusted
    import torch
    _orig_torch_load = torch.load
    def _patched_torch_load(*args, **kwargs):
        kwargs["weights_only"] = False
        return _orig_torch_load(*args, **kwargs)
    torch.load = _patched_torch_load

    # Get token and set as env var (works with both old and new huggingface_hub)
    from huggingface_hub import HfApi
    token = HfApi().token
    if token:
        os.environ["HF_TOKEN"] = token

    pipeline = Pipeline.from_pretrained("pyannote/speaker-diarization-3.1")

    # Move to GPU if available
    if torch.cuda.is_available():
        pipeline.to(torch.device("cuda"))
        print("Diarization using GPU (CUDA)")
    else:
        print("Diarization using CPU")

    print("Running speaker diarization...")

    # Load audio as waveform (bypasses torchcodec which is broken on Windows)
    import soundfile as sf
    audio_data, sample_rate = sf.read(str(wav_path))
    waveform = torch.from_numpy(audio_data).float().unsqueeze(0)  # (1, samples)
    audio_input = {"waveform": waveform, "sample_rate": sample_rate}

    diarization_params = {}
    if num_speakers:
        diarization_params["num_speakers"] = num_speakers

    diarization = pipeline(audio_input, **diarization_params)

    # Clean up temp WAV
    wav_path.unlink(missing_ok=True)

    speaker_segments = []
    for turn, _, speaker in diarization.itertracks(yield_label=True):
        speaker_segments.append((turn.start, turn.end, speaker))

    speakers_found = len(set(s[2] for s in speaker_segments))
    print(f"Diarization complete: {speakers_found} speakers detected, {len(speaker_segments)} segments")

    return speaker_segments


def _get_speaker_at(timestamp, speaker_segments):
    """Find which speaker is talking at a given timestamp."""
    for start, end, speaker in speaker_segments:
        if start <= timestamp <= end:
            return speaker
    # If no exact match, find the closest segment
    min_dist = float("inf")
    closest_speaker = "Unknown"
    for start, end, speaker in speaker_segments:
        mid = (start + end) / 2
        dist = abs(timestamp - mid)
        if dist < min_dist:
            min_dist = dist
            closest_speaker = speaker
    return closest_speaker


def _load_groq_key():
    """Load Groq API key from groq.apikey file next to this script."""
    key_path = Path(__file__).parent / "groq.apikey"
    if not key_path.exists():
        print(f"Error: Groq API key not found at {key_path}")
        print("Create a file 'groq.apikey' in the scripts directory with your API key.")
        sys.exit(1)
    return key_path.read_text().strip()


def _transcribe_groq(audio_path, language=None):
    """Transcribe audio using Groq API. Returns list of segment-like objects."""
    import httpx

    api_key = _load_groq_key()

    # Convert to compressed MP3 to stay under 25MB limit
    # Auto-adjust bitrate downward if needed
    max_size_mb = 25
    for bitrate in ["48k", "32k", "24k"]:
        mp3_path = _convert_audio(audio_path, ".groq.mp3", sample_rate=16000, mono=True, bitrate=bitrate)
        file_size_mb = mp3_path.stat().st_size / (1024 * 1024)
        print(f"Prepared audio for Groq: {file_size_mb:.1f}MB (bitrate: {bitrate})")
        if file_size_mb <= max_size_mb:
            break
    else:
        print(f"Warning: File is still {file_size_mb:.1f}MB after compression — may be rejected.")
        print("Consider splitting the recording or upgrading to Groq dev tier (100MB).")

    print("Sending to Groq API (whisper-large-v3)...")

    data = {"model": "whisper-large-v3", "response_format": "verbose_json"}
    if language:
        data["language"] = language

    with open(mp3_path, "rb") as f:
        response = httpx.post(
            "https://api.groq.com/openai/v1/audio/transcriptions",
            headers={"Authorization": f"Bearer {api_key}"},
            data=data,
            files={"file": (mp3_path.name, f, "audio/mpeg")},
            timeout=300,
        )

    # Clean up temp MP3
    mp3_path.unlink(missing_ok=True)

    if response.status_code != 200:
        print(f"Groq API error {response.status_code}: {response.text}")
        sys.exit(1)

    result = response.json()
    detected_lang = result.get("language", "unknown")
    print(f"Groq transcription complete. Detected language: {detected_lang}")

    # Convert Groq segments to a common format
    segments = []
    for seg in result.get("segments", []):
        segments.append(_GroqSegment(
            start=seg["start"],
            end=seg["end"],
            text=seg["text"].strip(),
        ))

    print(f"  {len(segments)} segments received from Groq")
    return segments, detected_lang


class _GroqSegment:
    """Simple segment object matching faster-whisper's interface."""
    def __init__(self, start, end, text):
        self.start = start
        self.end = end
        self.text = text


def _transcribe_local(audio_path, model_name, language=None):
    """Transcribe audio using local faster-whisper. Returns list of segments and language."""
    try:
        from faster_whisper import WhisperModel
    except ImportError:
        print("faster-whisper not installed. Run: pip install faster-whisper")
        sys.exit(1)

    print(f"\nLoading Whisper model '{model_name}'...")
    try:
        model = WhisperModel(model_name, device="cuda", compute_type="float16")
        print("Using GPU (CUDA)")
    except RuntimeError:
        print("CUDA not available — using CPU (slower but works)")
        model = WhisperModel(model_name, device="cpu", compute_type="int8")

    print(f"Transcribing {audio_path.name}...")
    segments, info = model.transcribe(
        str(audio_path),
        language=language,
        vad_filter=True,
        vad_parameters=dict(
            min_silence_duration_ms=1500,
            speech_pad_ms=400,
        ),
        repetition_penalty=1.2,
        no_repeat_ngram_size=3,
        hallucination_silence_threshold=2,
    )

    print(f"Detected language: {info.language} (probability {info.language_probability:.2f})")

    # Collect all segments (generator → list)
    all_segments = list(segments)
    return all_segments, info.language


def main():
    parser = argparse.ArgumentParser(description="Transcribe audio with Whisper (multilingual)")
    parser.add_argument("audio", help="Path to audio file (mp3, wav, m4a, mp4, etc.)")
    parser.add_argument("--model", default="large-v3", help="Whisper model (default: large-v3)")
    parser.add_argument("--output", "-o", help="Output text file path (default: same name as audio + .txt)")
    parser.add_argument("--srt", action="store_true", help="Also output SRT subtitle file")
    parser.add_argument("--language", default=None, help="Force language (default: auto-detect)")
    parser.add_argument("--diarize", action="store_true", help="Enable speaker diarization (requires pyannote.audio)")
    parser.add_argument("--speakers", type=int, default=None, help="Expected number of speakers (helps diarization accuracy)")
    parser.add_argument("--groq", action="store_true", help="Use Groq API for transcription (fast, requires groq.apikey)")
    args = parser.parse_args()

    audio_path = Path(args.audio)
    if not audio_path.exists():
        print(f"Error: {audio_path} not found")
        sys.exit(1)

    # Run diarization first if requested (local GPU — fast)
    speaker_segments = None
    if args.diarize:
        speaker_segments = _run_diarization(audio_path, args.speakers)

    # Transcribe — either Groq API or local
    filler_patterns = {"mm-hmm", "mmm", "mm hmm", "uh", "um", "hmm", "mhm"}

    if args.groq:
        raw_segments, detected_lang = _transcribe_groq(audio_path, args.language)
    else:
        raw_segments, detected_lang = _transcribe_local(audio_path, args.model, args.language)

    # Filter filler and print progress
    all_segments = []
    for segment in raw_segments:
        text = segment.text.strip()
        if text.lower().rstrip(".,-!?") in filler_patterns:
            continue
        all_segments.append(segment)
        speaker_label = ""
        if speaker_segments:
            speaker = _get_speaker_at((segment.start + segment.end) / 2, speaker_segments)
            speaker_label = f" [{speaker}]"
        print(f"  [{segment.start:.1f}s - {segment.end:.1f}s]{speaker_label} {text}")

    # Merge short segments into natural sentences
    # When diarizing, also break on speaker changes
    merged_segments = []
    buffer_start = None
    buffer_end = None
    buffer_text = []
    buffer_speaker = None

    for seg in all_segments:
        current_speaker = None
        if speaker_segments:
            current_speaker = _get_speaker_at((seg.start + seg.end) / 2, speaker_segments)

        if buffer_start is None:
            buffer_start = seg.start
            buffer_end = seg.end
            buffer_text = [seg.text.strip()]
            buffer_speaker = current_speaker
            continue

        gap = seg.start - buffer_end
        duration = seg.end - buffer_start
        speaker_changed = speaker_segments and current_speaker != buffer_speaker

        # Flush buffer if: gap > 1.5s, buffer > 30s, or speaker changed
        if gap > 1.5 or duration > 30 or speaker_changed:
            merged_segments.append((buffer_start, buffer_end, " ".join(buffer_text), buffer_speaker))
            buffer_start = seg.start
            buffer_end = seg.end
            buffer_text = [seg.text.strip()]
            buffer_speaker = current_speaker
        else:
            buffer_end = seg.end
            buffer_text.append(seg.text.strip())

    # Flush remaining
    if buffer_text:
        merged_segments.append((buffer_start, buffer_end, " ".join(buffer_text), buffer_speaker))

    print(f"\n{len(all_segments)} raw segments merged into {len(merged_segments)} lines\n")

    # Write text output
    output_path = Path(args.output) if args.output else audio_path.with_suffix(".txt")
    with open(output_path, "w", encoding="utf-8") as f:
        for start, end, text, speaker in merged_segments:
            if speaker:
                f.write(f"[{_format_time(start)} - {_format_time(end)}] {speaker}: {text}\n")
            else:
                f.write(f"[{_format_time(start)} - {_format_time(end)}] {text}\n")

    print(f"\nTranscript saved to: {output_path}")

    # Write SRT if requested
    if args.srt:
        srt_path = audio_path.with_suffix(".srt")
        with open(srt_path, "w", encoding="utf-8") as f:
            for i, (start, end, text, speaker) in enumerate(merged_segments, 1):
                f.write(f"{i}\n")
                f.write(f"{_format_srt_time(start)} --> {_format_srt_time(end)}\n")
                if speaker:
                    f.write(f"[{speaker}] {text}\n\n")
                else:
                    f.write(f"{text}\n\n")
        print(f"SRT saved to: {srt_path}")

    # Print speaker summary if diarized
    if speaker_segments:
        print("\n--- Speaker Summary ---")
        speaker_times = {}
        for start, end, text, speaker in merged_segments:
            if speaker:
                speaker_times[speaker] = speaker_times.get(speaker, 0) + (end - start)
        for speaker, total in sorted(speaker_times.items(), key=lambda x: -x[1]):
            mins = total / 60
            print(f"  {speaker}: {mins:.1f} minutes")

    engine = "Groq API" if args.groq else f"local ({args.model})"
    print(f"\nDone. {len(merged_segments)} lines from {len(all_segments)} segments, {detected_lang} detected, engine: {engine}.")


def _format_time(seconds: float) -> str:
    m, s = divmod(int(seconds), 60)
    h, m = divmod(m, 60)
    return f"{h}:{m:02d}:{s:02d}"


def _format_srt_time(seconds: float) -> str:
    m, s = divmod(seconds, 60)
    h, m = divmod(int(m), 60)
    ms = int((s % 1) * 1000)
    return f"{int(h):02d}:{int(m):02d}:{int(s):02d},{ms:03d}"


if __name__ == "__main__":
    main()
