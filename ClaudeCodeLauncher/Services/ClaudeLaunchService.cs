using System.Diagnostics;

namespace ClaudeCodeLauncher.Services;

/// <summary>
/// Shared logic for launching Claude Code CLI in a terminal for a given directory.
/// Used by both the main GUI and the headless auto-launch path.
/// </summary>
public static class ClaudeLaunchService
{
    public static bool Launch(string path, AppSettingsService settings)
    {
        try
        {
            if (TryLaunchWithWindowsTerminal(path, settings))
                return true;

            return TryLaunchWithCmd(path, settings);
        }
        catch
        {
            return false;
        }
    }

    private static bool TryLaunchWithWindowsTerminal(string path, AppSettingsService settings)
    {
        try
        {
            string command = GetLaunchCommand(settings);
            var startInfo = new ProcessStartInfo
            {
                FileName = "wt.exe",
                Arguments = $"--startingDirectory \"{path}\" -- cmd /k {command}",
                UseShellExecute = true
            };

            Process.Start(startInfo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool TryLaunchWithCmd(string path, AppSettingsService settings)
    {
        try
        {
            string command = GetLaunchCommand(settings);
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k cd /d \"{path}\" && {command}",
                UseShellExecute = true
            };

            Process.Start(startInfo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string GetLaunchCommand(AppSettingsService settings)
    {
        // Use MU/TH/UR batch file if selected and available
        if (settings.InterfaceStyle == InterfaceStyle.MUTHR && settings.IsMuthrAvailable())
        {
            return $"\"{settings.MuthrBatchPath}\"";
        }

        // Use custom executable path if configured
        if (settings.HasCustomExecutable())
        {
            return $"\"{settings.ExecutablePath}\"";
        }

        // Default to standard claude command from PATH
        return "claude";
    }
}
