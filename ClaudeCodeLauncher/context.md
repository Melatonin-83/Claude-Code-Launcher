# Claude Code Launcher - Application Context

## Repository
- **GitHub**: https://github.com/Melatonin-83/Claude-Code-Launcher
- **Branch**: master
- **Clone**: `git clone https://github.com/Melatonin-83/Claude-Code-Launcher.git`

## Overview
Claude Code Launcher is a Windows Forms desktop application built with C# and .NET 9. It provides a sleek, user-friendly interface for launching Claude Code CLI in a user-selected directory.

## Purpose
- Simplify the process of opening Claude Code in specific project directories
- Provide quick access to recently used directories
- Offer a visually appealing interface that matches Claude's branding (dark theme with orange accents)

## Technical Stack
- **Framework**: .NET 9.0 (Windows)
- **UI Technology**: Windows Forms
- **Language**: C#
- **Target Platform**: Windows 10/11

## Architecture

### Project Structure
```
ClaudeCodeLauncher/
├── ClaudeCodeLauncher.csproj   # Project configuration (.NET 9)
├── Program.cs                   # Application entry point
├── MainForm.cs                  # Main form logic and event handlers
├── MainForm.Designer.cs         # UI designer code
├── Services/
│   └── RecentDirectoriesService.cs  # Directory history management
├── Theme/
│   └── ClaudeTheme.cs           # Color scheme and styling constants
├── context.md                   # This file (app documentation)
└── publish/
    └── ClaudeCodeLauncher.exe   # Standalone executable (~108 MB)
```

### Key Components

#### MainForm
- Custom borderless form with dark theme
- Draggable custom title bar
- Directory selection via FolderBrowserDialog
- Recent directories list with click-to-select and double-click-to-launch
- Large prominent orange launch button
- Rounded corners using Win32 API

#### RecentDirectoriesService
- Persists recent directories to `%APPDATA%\ClaudeCodeLauncher\recent.json`
- Maximum 10 directories stored
- Auto-removes invalid (deleted) directories on load
- Thread-safe JSON serialization

#### ClaudeTheme
- Defines Claude's brand colors:
  - Primary Background: `#1a1a1a` (dark black)
  - Secondary Background: `#2d2d2d` (lighter black)
  - Accent Color: `#E07A3D` (Claude orange)
  - Accent Hover: `#FF8C42` (brighter orange)
  - Text Primary: `#FFFFFF` (white)
  - Text Secondary: `#B0B0B0` (gray)
- Helper methods for consistent styling across UI elements

## Features
1. **Directory Selection**: Browse and select any folder
2. **Recent Directories**: Quick access to last 10 used directories
3. **Windows Terminal Integration**: Launches Claude in Windows Terminal (falls back to cmd)
4. **Persistent History**: Recent directories saved across sessions
5. **Custom Dark Theme**: Sleek black and orange UI matching Claude branding
6. **Standalone Executable**: Self-contained exe that doesn't require .NET installed

## Launch Behavior
1. User selects a directory (via browse or recent list)
2. Clicks "Launch Claude Code" button
3. App attempts to open Windows Terminal with: `wt.exe --startingDirectory "{path}" -- cmd /k claude`
4. If Windows Terminal unavailable, falls back to: `cmd.exe /k cd /d "{path}" && claude`

## Configuration
- Settings stored in: `%APPDATA%\ClaudeCodeLauncher\`
- Recent directories: `recent.json`

## Build Commands

### Development
```bash
dotnet build
dotnet run
```

### Publish Standalone Executable
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publish
```

## Future Enhancements (Potential)
- Favorite directories (pinned)
- Custom terminal profiles
- Keyboard shortcuts
- System tray minimization
- Auto-update checking
- Custom app icon

## Development Notes
- Form uses double-buffering to prevent flickering
- Custom title bar for borderless window allows dragging
- Colors defined in ClaudeTheme for easy theming adjustments
- Uses Win32 `CreateRoundRectRgn` for rounded corners
- Requires Windows Terminal for best experience (falls back to cmd)

## Git Workflow
```bash
# Check status
git status

# Stage and commit
git add .
git commit -m "Your commit message"

# Push to GitHub
git push

# Pull latest changes
git pull
```

---
*Last Updated: January 2026*
