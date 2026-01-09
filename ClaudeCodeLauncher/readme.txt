================================================================================
                         CLAUDE CODE LAUNCHER
                              v1.0
================================================================================

DESCRIPTION
-----------
Claude Code Launcher is a Windows desktop application that provides a simple,
elegant interface for launching Claude Code CLI in any directory of your choice.
It features a sleek dark theme with orange accents matching Claude's branding.


REQUIREMENTS
------------
- Windows 10 or Windows 11
- Claude Code CLI installed and available in your PATH
- Windows Terminal (recommended) or Command Prompt


INSTALLATION
------------
The executable is not included in the repository (to avoid bloating the repo
with a ~108MB binary). You need to build it yourself:

1. Ensure you have .NET 9.0 SDK installed
2. Open a terminal in the ClaudeCodeLauncher folder
3. Run: dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publish
4. Run ClaudeCodeLauncher.exe from the publish folder

The published application is self-contained and does not require .NET to be
installed on end-user systems.


HOW TO USE
----------
1. Launch ClaudeCodeLauncher.exe
2. Click "Browse" to select a project directory, or choose from recent directories
3. Click the "Launch Claude Code" button
4. Claude Code will open in Windows Terminal (or Command Prompt) at your
   selected directory


FEATURES
--------
- Directory Selection: Browse and select any folder on your system
- Recent Directories: Quick access to your last 10 used directories
- Windows Terminal Integration: Launches in Windows Terminal when available
- Persistent History: Recent directories are saved between sessions
- Standalone Executable: No additional dependencies required
- Quick Links:
  - Get Claude CLI: Opens the Claude Code documentation/installer page
  - Open Directory: Opens the selected directory in File Explorer
  - Claude Web: Opens the Claude web interface (claude.ai)


CONFIGURATION
-------------
Application data is stored in:
  %APPDATA%\ClaudeCodeLauncher\

Recent directories are saved to:
  %APPDATA%\ClaudeCodeLauncher\recent.json


BUILD FROM SOURCE
-----------------
Requirements: .NET 9.0 SDK

Development:
  dotnet build
  dotnet run

Publish standalone executable:
  dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publish


TECHNICAL DETAILS
-----------------
- Framework: .NET 9.0 (Windows)
- UI: Windows Forms
- Language: C#


================================================================================
