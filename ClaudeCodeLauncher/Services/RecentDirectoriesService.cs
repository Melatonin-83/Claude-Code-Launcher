using System.Text.Json;

namespace ClaudeCodeLauncher.Services;

/// <summary>
/// Service for managing recent directories history.
/// Stores directories in %APPDATA%\ClaudeCodeLauncher\recent.json
/// </summary>
public class RecentDirectoriesService
{
    private const int MaxRecentDirectories = 10;
    private const string AppFolderName = "ClaudeCodeLauncher";
    private const string RecentFileName = "recent.json";

    private readonly string _appDataPath;
    private readonly string _recentFilePath;
    private List<string> _recentDirectories;

    public RecentDirectoriesService()
    {
        _appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            AppFolderName);
        _recentFilePath = Path.Combine(_appDataPath, RecentFileName);
        _recentDirectories = new List<string>();

        EnsureAppDataFolderExists();
        LoadRecentDirectories();
    }

    /// <summary>
    /// Gets the list of recent directories.
    /// </summary>
    public IReadOnlyList<string> GetRecentDirectories()
    {
        // Filter out directories that no longer exist
        _recentDirectories = _recentDirectories
            .Where(Directory.Exists)
            .ToList();

        return _recentDirectories.AsReadOnly();
    }

    /// <summary>
    /// Adds a directory to the recent list.
    /// If it already exists, moves it to the top.
    /// </summary>
    public void AddDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            return;

        // Normalize the path
        path = Path.GetFullPath(path);

        // Remove if already exists (will be re-added at top)
        _recentDirectories.RemoveAll(d =>
            string.Equals(d, path, StringComparison.OrdinalIgnoreCase));

        // Insert at the beginning
        _recentDirectories.Insert(0, path);

        // Trim to max size
        if (_recentDirectories.Count > MaxRecentDirectories)
        {
            _recentDirectories = _recentDirectories.Take(MaxRecentDirectories).ToList();
        }

        SaveRecentDirectories();
    }

    /// <summary>
    /// Removes a specific directory from the recent list.
    /// </summary>
    public void RemoveDirectory(string path)
    {
        _recentDirectories.RemoveAll(d =>
            string.Equals(d, path, StringComparison.OrdinalIgnoreCase));
        SaveRecentDirectories();
    }

    /// <summary>
    /// Clears all recent directories.
    /// </summary>
    public void ClearHistory()
    {
        _recentDirectories.Clear();
        SaveRecentDirectories();
    }

    /// <summary>
    /// Gets the most recently used directory, or null if none.
    /// </summary>
    public string? GetLastDirectory()
    {
        var recent = GetRecentDirectories();
        return recent.FirstOrDefault();
    }

    private void EnsureAppDataFolderExists()
    {
        if (!Directory.Exists(_appDataPath))
        {
            Directory.CreateDirectory(_appDataPath);
        }
    }

    private void LoadRecentDirectories()
    {
        try
        {
            if (File.Exists(_recentFilePath))
            {
                var json = File.ReadAllText(_recentFilePath);
                var directories = JsonSerializer.Deserialize<List<string>>(json);
                _recentDirectories = directories ?? new List<string>();
            }
        }
        catch (Exception)
        {
            // If loading fails, start with empty list
            _recentDirectories = new List<string>();
        }
    }

    private void SaveRecentDirectories()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_recentDirectories, options);
            File.WriteAllText(_recentFilePath, json);
        }
        catch (Exception)
        {
            // Silently fail if we can't save
        }
    }
}
