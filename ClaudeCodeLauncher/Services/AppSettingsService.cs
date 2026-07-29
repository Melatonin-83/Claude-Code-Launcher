using System.Text.Json;

namespace ClaudeCodeLauncher.Services;

/// <summary>
/// Represents the interface style for launching Claude CLI.
/// </summary>
public enum InterfaceStyle
{
    Standard,
    MUTHR  // MU/TH/UR - INTERFACE 2037
}

/// <summary>
/// Application settings model.
/// </summary>
public class AppSettings
{
    public InterfaceStyle InterfaceStyle { get; set; } = InterfaceStyle.Standard;
    public string MuthrBatchPath { get; set; } = @"C:\Users\Dmpal\AppData\Local\MUTHR\muthr.bat";
    public string ExecutablePath { get; set; } = string.Empty;  // Empty means use "claude" from PATH
}

/// <summary>
/// Service for managing application settings.
/// Stores settings in %APPDATA%\ClaudeCodeLauncher\settings.json
/// </summary>
public class AppSettingsService
{
    private const string AppFolderName = "ClaudeCodeLauncher";
    private const string SettingsFileName = "settings.json";

    private readonly string _appDataPath;
    private readonly string _settingsFilePath;
    private AppSettings _settings;

    public AppSettingsService()
    {
        _appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            AppFolderName);
        _settingsFilePath = Path.Combine(_appDataPath, SettingsFileName);
        _settings = new AppSettings();

        EnsureAppDataFolderExists();
        LoadSettings();
    }

    /// <summary>
    /// Gets the current interface style setting.
    /// </summary>
    public InterfaceStyle InterfaceStyle => _settings.InterfaceStyle;

    /// <summary>
    /// Gets the path to the MU/TH/UR batch file.
    /// </summary>
    public string MuthrBatchPath => _settings.MuthrBatchPath;

    /// <summary>
    /// Gets the custom executable path (empty string means use PATH).
    /// </summary>
    public string ExecutablePath => _settings.ExecutablePath;

    /// <summary>
    /// Sets the interface style and saves settings.
    /// </summary>
    public void SetInterfaceStyle(InterfaceStyle style)
    {
        _settings.InterfaceStyle = style;
        SaveSettings();
    }

    /// <summary>
    /// Sets the MU/TH/UR batch file path and saves settings.
    /// </summary>
    public void SetMuthrBatchPath(string path)
    {
        _settings.MuthrBatchPath = path;
        SaveSettings();
    }

    /// <summary>
    /// Sets the custom executable path and saves settings.
    /// </summary>
    public void SetExecutablePath(string path)
    {
        _settings.ExecutablePath = path;
        SaveSettings();
    }

    /// <summary>
    /// Checks if a custom executable path is configured and exists.
    /// </summary>
    public bool HasCustomExecutable()
    {
        return !string.IsNullOrEmpty(_settings.ExecutablePath) && File.Exists(_settings.ExecutablePath);
    }

    /// <summary>
    /// Checks if the MU/TH/UR batch file exists.
    /// </summary>
    public bool IsMuthrAvailable()
    {
        return File.Exists(_settings.MuthrBatchPath);
    }

    private void EnsureAppDataFolderExists()
    {
        if (!Directory.Exists(_appDataPath))
        {
            Directory.CreateDirectory(_appDataPath);
        }
    }

    private void LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);
                _settings = settings ?? new AppSettings();
            }
        }
        catch (Exception)
        {
            // If loading fails, use defaults
            _settings = new AppSettings();
        }
    }

    private void SaveSettings()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_settings, options);
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception)
        {
            // Silently fail if we can't save
        }
    }
}
