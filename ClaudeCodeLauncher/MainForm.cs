using System.Diagnostics;
using System.Runtime.InteropServices;
using ClaudeCodeLauncher.Services;
using ClaudeCodeLauncher.Theme;

namespace ClaudeCodeLauncher;

public partial class MainForm : Form
{
    private readonly RecentDirectoriesService _recentService;
    private string? _selectedPath;

    // For dragging the borderless window
    private bool _dragging;
    private Point _dragCursorPoint;
    private Point _dragFormPoint;

    // For rounded corners
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

    public MainForm()
    {
        InitializeComponent();
        _recentService = new RecentDirectoriesService();

        // Enable double buffering to reduce flickering
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        ApplyTheme();
        SetupEventHandlers();
        LoadRecentDirectories();
        UpdateLaunchButtonState();

        // Apply rounded corners to the form
        Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height,
            ClaudeTheme.BorderRadius * 2, ClaudeTheme.BorderRadius * 2));
    }

    private void ApplyTheme()
    {
        // Form
        ClaudeTheme.ApplyToForm(this);

        // Title Bar
        pnlTitleBar.BackColor = ClaudeTheme.TertiaryBackground;
        ClaudeTheme.StyleLabel(lblTitle, isTitle: true);
        lblTitle.ForeColor = ClaudeTheme.AccentColor;

        // Window control buttons
        btnClose.BackColor = Color.Transparent;
        btnClose.ForeColor = ClaudeTheme.TextSecondary;
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 50, 50);
        btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 30, 30);

        btnMinimize.BackColor = Color.Transparent;
        btnMinimize.ForeColor = ClaudeTheme.TextSecondary;
        btnMinimize.FlatAppearance.MouseOverBackColor = ClaudeTheme.SecondaryBackground;

        // Directory selection
        ClaudeTheme.StyleLabel(lblSelectDirectory);
        pnlPathContainer.BackColor = Color.Transparent;
        ClaudeTheme.StyleTextBox(txtPath);
        ClaudeTheme.StyleButton(btnBrowse);

        // Recent directories
        ClaudeTheme.StyleLabel(lblRecentDirectories);
        ClaudeTheme.StylePanel(pnlRecentContainer, hasBorder: false);
        ClaudeTheme.StyleListBox(lstRecentDirectories);

        // Clear history link
        lnkClearHistory.LinkColor = ClaudeTheme.AccentColor;
        lnkClearHistory.ActiveLinkColor = ClaudeTheme.AccentHover;
        lnkClearHistory.VisitedLinkColor = ClaudeTheme.AccentColor;
        lnkClearHistory.BackColor = Color.Transparent;

        // Launch button
        ClaudeTheme.StyleButton(btnLaunch, isPrimary: true);

        // Status label
        lblStatus.ForeColor = ClaudeTheme.TextMuted;
        lblStatus.BackColor = Color.Transparent;

        // Quick links panel
        pnlQuickLinks.BackColor = Color.Transparent;
        StyleQuickLinkButton(btnGetClaude);
        StyleQuickLinkButton(btnOpenDirectory);
        StyleQuickLinkButton(btnClaudeWeb);
    }

    private void StyleQuickLinkButton(Button btn)
    {
        btn.BackColor = ClaudeTheme.SecondaryBackground;
        btn.ForeColor = ClaudeTheme.AccentColor;
        btn.FlatAppearance.BorderColor = ClaudeTheme.BorderColor;
        btn.FlatAppearance.BorderSize = 1;
        btn.FlatAppearance.MouseOverBackColor = ClaudeTheme.TertiaryBackground;
        btn.Font = new Font(btn.Font.FontFamily, 8f);
    }

    private void SetupEventHandlers()
    {
        // Window control buttons
        btnClose.Click += (s, e) => Close();
        btnMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;

        // Title bar dragging
        pnlTitleBar.MouseDown += TitleBar_MouseDown;
        pnlTitleBar.MouseMove += TitleBar_MouseMove;
        pnlTitleBar.MouseUp += TitleBar_MouseUp;
        lblTitle.MouseDown += TitleBar_MouseDown;
        lblTitle.MouseMove += TitleBar_MouseMove;
        lblTitle.MouseUp += TitleBar_MouseUp;

        // Directory selection
        btnBrowse.Click += BtnBrowse_Click;
        txtPath.TextChanged += (s, e) => UpdateLaunchButtonState();

        // Recent directories
        lstRecentDirectories.SelectedIndexChanged += LstRecentDirectories_SelectedIndexChanged;
        lstRecentDirectories.DoubleClick += LstRecentDirectories_DoubleClick;
        lnkClearHistory.LinkClicked += LnkClearHistory_LinkClicked;

        // Launch button
        btnLaunch.Click += BtnLaunch_Click;

        // Quick link buttons
        btnGetClaude.Click += BtnGetClaude_Click;
        btnOpenDirectory.Click += BtnOpenDirectory_Click;
        btnClaudeWeb.Click += BtnClaudeWeb_Click;

        // Handle resize for rounded corners
        Resize += (s, e) =>
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height,
                ClaudeTheme.BorderRadius * 2, ClaudeTheme.BorderRadius * 2));
        };
    }

    #region Title Bar Dragging

    private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = Location;
        }
    }

    private void TitleBar_MouseMove(object? sender, MouseEventArgs e)
    {
        if (_dragging)
        {
            var diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
            Location = Point.Add(_dragFormPoint, new Size(diff));
        }
    }

    private void TitleBar_MouseUp(object? sender, MouseEventArgs e)
    {
        _dragging = false;
    }

    #endregion

    #region Directory Selection

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select a directory to open Claude Code",
            ShowNewFolderButton = true,
            UseDescriptionForTitle = true
        };

        // Start from last used directory if available
        var lastDir = _recentService.GetLastDirectory();
        if (!string.IsNullOrEmpty(lastDir))
        {
            dialog.InitialDirectory = lastDir;
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            SelectDirectory(dialog.SelectedPath);
        }
    }

    private void SelectDirectory(string path)
    {
        _selectedPath = path;
        txtPath.Text = path;
        UpdateLaunchButtonState();
    }

    #endregion

    #region Recent Directories

    private void LoadRecentDirectories()
    {
        lstRecentDirectories.Items.Clear();
        var directories = _recentService.GetRecentDirectories();

        foreach (var dir in directories)
        {
            lstRecentDirectories.Items.Add(dir);
        }

        // Update clear history visibility
        lnkClearHistory.Visible = directories.Count > 0;

        // Select the most recent if path is empty
        if (string.IsNullOrEmpty(_selectedPath) && directories.Count > 0)
        {
            lstRecentDirectories.SelectedIndex = 0;
        }
    }

    private void LstRecentDirectories_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstRecentDirectories.SelectedItem is string path)
        {
            SelectDirectory(path);
        }
    }

    private void LstRecentDirectories_DoubleClick(object? sender, EventArgs e)
    {
        if (lstRecentDirectories.SelectedItem is string)
        {
            LaunchClaudeCode();
        }
    }

    private void LnkClearHistory_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        var result = MessageBox.Show(
            "Are you sure you want to clear the recent directories history?",
            "Clear History",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _recentService.ClearHistory();
            LoadRecentDirectories();
            _selectedPath = null;
            txtPath.Text = string.Empty;
            UpdateLaunchButtonState();
        }
    }

    #endregion

    #region Launch

    private void UpdateLaunchButtonState()
    {
        var hasValidPath = !string.IsNullOrEmpty(_selectedPath) && Directory.Exists(_selectedPath);
        btnLaunch.Enabled = hasValidPath;

        if (hasValidPath)
        {
            btnLaunch.BackColor = ClaudeTheme.AccentColor;
            btnLaunch.ForeColor = ClaudeTheme.TextPrimary;
        }
        else
        {
            btnLaunch.BackColor = ClaudeTheme.SecondaryBackground;
            btnLaunch.ForeColor = ClaudeTheme.TextMuted;
        }
    }

    private void BtnLaunch_Click(object? sender, EventArgs e)
    {
        LaunchClaudeCode();
    }

    private void LaunchClaudeCode()
    {
        if (string.IsNullOrEmpty(_selectedPath) || !Directory.Exists(_selectedPath))
        {
            ShowStatus("Invalid directory selected.", isError: true);
            return;
        }

        // Add to recent directories
        _recentService.AddDirectory(_selectedPath);
        LoadRecentDirectories();

        try
        {
            // Try Windows Terminal first
            if (TryLaunchWithWindowsTerminal(_selectedPath))
            {
                ShowStatus("Launched Claude Code in Windows Terminal");
                return;
            }

            // Fallback to cmd
            if (TryLaunchWithCmd(_selectedPath))
            {
                ShowStatus("Launched Claude Code in Command Prompt");
                return;
            }

            ShowStatus("Failed to launch Claude Code. Please check if 'claude' is installed.", isError: true);
        }
        catch (Exception ex)
        {
            ShowStatus($"Error: {ex.Message}", isError: true);
        }
    }

    private bool TryLaunchWithWindowsTerminal(string path)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "wt.exe",
                Arguments = $"--startingDirectory \"{path}\" -- cmd /k claude",
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

    private bool TryLaunchWithCmd(string path)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k cd /d \"{path}\" && claude",
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

    private void ShowStatus(string message, bool isError = false)
    {
        lblStatus.Text = message;
        lblStatus.ForeColor = isError ? ClaudeTheme.Error : ClaudeTheme.Success;

        // Auto-clear status after 5 seconds
        var timer = new System.Windows.Forms.Timer { Interval = 5000 };
        timer.Tick += (s, e) =>
        {
            lblStatus.Text = string.Empty;
            timer.Stop();
            timer.Dispose();
        };
        timer.Start();
    }

    #endregion

    #region Quick Links

    private void BtnGetClaude_Click(object? sender, EventArgs e)
    {
        OpenUrl("https://docs.anthropic.com/en/docs/claude-code/overview");
    }

    private void BtnOpenDirectory_Click(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_selectedPath) && Directory.Exists(_selectedPath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _selectedPath,
                UseShellExecute = true
            });
        }
        else
        {
            ShowStatus("Please select a valid directory first.", isError: true);
        }
    }

    private void BtnClaudeWeb_Click(object? sender, EventArgs e)
    {
        OpenUrl("https://claude.ai");
    }

    private void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            ShowStatus($"Failed to open URL: {ex.Message}", isError: true);
        }
    }

    #endregion

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw border
        using var pen = new Pen(ClaudeTheme.BorderColor, 1);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
