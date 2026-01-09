# Claude Code Launcher

A Windows desktop application that provides a simple, elegant interface for launching Claude Code CLI in any directory of your choice. Features a sleek dark theme with orange accents matching Claude's branding.

## Features

- **Directory Selection** - Browse and select any folder on your system
- **Recent Directories** - Quick access to your last 10 used directories
- **Windows Terminal Integration** - Launches in Windows Terminal when available
- **Persistent History** - Recent directories are saved between sessions
- **Standalone Executable** - No additional dependencies required
- **Quick Links**:
  - Get Claude CLI - Opens the Claude Code documentation/installer page
  - Open Directory - Opens the selected directory in File Explorer
  - Claude Web - Opens the Claude web interface (claude.ai)

## Requirements

- Windows 10 or Windows 11
- Claude Code CLI installed and available in your PATH
- Windows Terminal (recommended) or Command Prompt

## Installation

The executable is not included in the repository (to avoid bloating the repo with a ~108MB binary). Build it yourself:

1. Ensure you have [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
2. Clone this repository
3. Run the publish command:
   ```bash
   cd ClaudeCodeLauncher
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publish
   ```
4. Run `ClaudeCodeLauncher.exe` from the `publish` folder

## Development

```bash
# Build
dotnet build

# Run
dotnet run
```

## Technical Details

- **Framework**: .NET 9.0 (Windows)
- **UI**: Windows Forms
- **Language**: C#

## Configuration

Application data is stored in:
- Settings: `%APPDATA%\ClaudeCodeLauncher\`
- Recent directories: `%APPDATA%\ClaudeCodeLauncher\recent.json`

## License

MIT
