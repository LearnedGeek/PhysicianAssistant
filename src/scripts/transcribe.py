"""
Meeting Transcription — Whisper large-v3
Handles Spanish/English code-switching for Infanzia/DrOk meetings.

Usage:
    python transcribe.py meeting.mp3
    python transcribe.py meeting.mp3 --output transcript.txt
    python transcribe.py meeting.mp3 --srt
"""

import argparse
import os
import sys
from pathlib import Path

# Ensure CUDA libraries from pip packages are discoverable.
# Must happen before any ctranslate2/faster_whisper import.
_nvidia_path = Path(sys.prefix) / "Lib" / "site-packages" / "nvidia"
if not _nvidia_path.exists():
    _nvidia_path = Path.home() / "AppData" / "Roaming" / "Python" / f"Python{sys.version_info.major}{sys.version_info.minor}" / "site-packages" / "nvidia"
if _nvidia_path.exists():
    for lib_dir in _nvidia_path.glob("*/bin"):
        os.add_dll_directory(str(lib_dir))
        os.environ["PATH"] = str(lib_dir) + os.pathsep + os.environ.get("PATH", "")

def main():
    parser = argparse.ArgumentParser(description="Transcribe audio with Whisper (multilingual)")
    parser.add_argument("audio", help="Path to audio file (mp3, wav, m4a, etc.)")
    parser.add_argument("--model", default="large-v3", help="Whisper model (default: large-v3)")
    parser.add_argument("--output", "-o", help="Output text file path (default: same name as audio + .txt)")
    parser.add_argument("--srt", action="store_true", help="Also output SRT subtitle file")
    parser.add_argument("--language", default=None, help="Force language (default: auto-detect)")
    args = parser.parse_args()

    audio_path = Path(args.audio)
    if not audio_path.exists():
        print(f"Error: {audio_path} not found")
        sys.exit(1)

    try:
        from faster_whisper import WhisperModel
    except ImportError:
        print("faster-whisper not installed. Run: pip install faster-whisper")
        sys.exit(1)

    # Try GPU first, fall back to CPU if CUDA libraries missing
    print(f"Loading model '{args.model}'...")
    try:
        model = WhisperModel(args.model, device="cuda", compute_type="float16")
        print("Using GPU (CUDA)")
    except RuntimeError:
        print("CUDA not available — using CPU (slower but works)")
        model = WhisperModel(args.model, device="cpu", compute_type="int8")

    print(f"Transcribing {audio_path.name}...")
    segments, info = model.transcribe(
        str(audio_path),
        language=args.language,
        vad_filter=True,
        vad_parameters=dict(
            min_silence_duration_ms=1500,  # wait 1.5s of silence before splitting
            speech_pad_ms=400,             # pad speech segments to avoid cutting words
        ),
        repetition_penalty=1.2,            # reduce hallucinated "Mm-hmm" loops
        no_repeat_ngram_size=3,            # prevent 3-gram repetition
        hallucination_silence_threshold=2, # skip segments during 2s+ silence
    )

    print(f"Detected language: {info.language} (probability {info.language_probability:.2f})")

    # Collect segments, filtering hallucinated filler
    all_segments = []
    filler_patterns = {"mm-hmm", "mmm", "mm hmm", "uh", "um", "hmm", "mhm"}
    for segment in segments:
        text = segment.text.strip()
        # Skip hallucinated filler (repeated "Mm-hmm" during silence)
        if text.lower().rstrip(".,-!?") in filler_patterns:
            continue
        all_segments.append(segment)
        # Print progress
        print(f"  [{segment.start:.1f}s - {segment.end:.1f}s] {text}")

    # Merge short segments into natural sentences (5s window or until pause > 1.5s)
    merged_segments = []
    buffer_start = None
    buffer_end = None
    buffer_text = []

    for seg in all_segments:
        if buffer_start is None:
            buffer_start = seg.start
            buffer_end = seg.end
            buffer_text = [seg.text.strip()]
            continue

        gap = seg.start - buffer_end
        duration = seg.end - buffer_start

        # Flush buffer if: gap > 1.5s (speaker pause) or buffer > 30s
        if gap > 1.5 or duration > 30:
            merged_segments.append((buffer_start, buffer_end, " ".join(buffer_text)))
            buffer_start = seg.start
            buffer_end = seg.end
            buffer_text = [seg.text.strip()]
        else:
            buffer_end = seg.end
            buffer_text.append(seg.text.strip())

    # Flush remaining
    if buffer_text:
        merged_segments.append((buffer_start, buffer_end, " ".join(buffer_text)))

    print(f"\n{len(all_segments)} raw segments merged into {len(merged_segments)} lines\n")

    # Write text output
    output_path = Path(args.output) if args.output else audio_path.with_suffix(".txt")
    with open(output_path, "w", encoding="utf-8") as f:
        for start, end, text in merged_segments:
            f.write(f"[{_format_time(start)} - {_format_time(end)}] {text}\n")

    print(f"\nTranscript saved to: {output_path}")

    # Write SRT if requested
    if args.srt:
        srt_path = audio_path.with_suffix(".srt")
        with open(srt_path, "w", encoding="utf-8") as f:
            for i, (start, end, text) in enumerate(merged_segments, 1):
                f.write(f"{i}\n")
                f.write(f"{_format_srt_time(start)} --> {_format_srt_time(end)}\n")
                f.write(f"{text}\n\n")
        print(f"SRT saved to: {srt_path}")

    print(f"\nDone. {len(merged_segments)} lines from {len(all_segments)} segments, {info.language} detected.")


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
