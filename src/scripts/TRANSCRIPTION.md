# Meeting Transcription — Whisper Setup

Transcribes Infanzia/DrOk meeting recordings with Spanish/English code-switching support.

## One-Time Setup

```bash
pip install faster-whisper
```

First run downloads the model (~3GB). After that it's cached.

**VRAM note:** large-v3 needs ~5GB VRAM. If Ollama models are loaded, stop them first:
```bash
ollama stop ani-v6-conversation-mistral
ollama stop ani-v6-inner
```

## Usage

**Basic (just give it the mp3):**
```bash
python transcribe.py meeting.mp3
```

Outputs `meeting.txt` with timestamps next to the mp3.

**With SRT subtitles:**
```bash
python transcribe.py meeting.mp3 --srt
```

**Custom output path:**
```bash
python transcribe.py meeting.mp3 --output "E:/Documents/Work/transcripts/infanzia-2026-03-27.txt"
```

**Force language (if auto-detect struggles):**
```bash
python transcribe.py meeting.mp3 --language es
python transcribe.py meeting.mp3 --language en
```

## Output Format

```
[0:00:05 - 0:00:12] So Martin, tell me about the patient flow.
[0:00:13 - 0:00:18] Si, entonces el flujo del paciente empieza con...
[0:00:19 - 0:00:25] And then they switch to the triage screen, right?
```

## Supported Audio Formats

mp3, wav, m4a, flac, ogg, webm — anything ffmpeg can read.

## Comparison Notes

- **Otter.AI:** Struggles with Spanish/English switching. Cloud-based (privacy concern for medical).
- **Whisper large-v3:** Handles code-switching well. Runs locally. Free. Private.
- **Deepgram:** Better for real-time streaming (ANI voice). Whisper is better for batch transcription.

