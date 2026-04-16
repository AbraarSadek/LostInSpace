# CONTRIBUTING.md

## Guidelines

This project follows explicit inline commenting style and method block end comments. Any new code must maintain:

- Verbose inline comments preceding control structures and method definitions.
- End-of-block comments like `//End of If-Statement`, `//End of Start Method`, and `//End of MainMenuManager Class`.
- Public fields grouped with `[Header("...")]` attributes and inline single-line comments describing them.

## Standards

- Use descriptive variable names and maintain existing code formatting.
- Add Audio-related fields under a `[Header("Audio References:")]` section when adding audio control.
- When toggling audio, use `AudioSource.Pause()` and `AudioSource.UnPause()` to preserve playback position.

This file will be merged into the repository's CONTRIBUTING.md if not present.