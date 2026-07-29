using ClaudeCodeLauncher.Services;

namespace ClaudeCodeLauncher;

static class Program
{
    private const string DefaultAutoLaunchPath = @"D:\Claude";

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && string.Equals(args[0], "--auto", StringComparison.OrdinalIgnoreCase))
        {
            RunAutoLaunch(args.Length > 1 ? args[1] : DefaultAutoLaunchPath);
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    /// <summary>
    /// Launches Claude Code directly in the given directory without showing the picker UI.
    /// </summary>
    private static void RunAutoLaunch(string path)
    {
        if (!Directory.Exists(path))
        {
            MessageBox.Show(
                $"Directory not found:\n{path}",
                "Claude Code Launcher",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var settingsService = new AppSettingsService();
        var recentService = new RecentDirectoriesService();

        if (!ClaudeLaunchService.Launch(path, settingsService))
        {
            MessageBox.Show(
                "Failed to launch Claude Code. Please check if 'claude' is installed.",
                "Claude Code Launcher",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        recentService.AddDirectory(path);
    }
}
