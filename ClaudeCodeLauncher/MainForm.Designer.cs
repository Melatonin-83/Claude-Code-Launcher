namespace ClaudeCodeLauncher;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        // Title Bar Panel
        this.pnlTitleBar = new Panel();
        this.lblTitle = new Label();
        this.btnClose = new Button();
        this.btnMinimize = new Button();

        // Directory Selection
        this.lblSelectDirectory = new Label();
        this.pnlPathContainer = new Panel();
        this.txtPath = new TextBox();
        this.btnBrowse = new Button();

        // Recent Directories
        this.lblRecentDirectories = new Label();
        this.pnlRecentContainer = new Panel();
        this.lstRecentDirectories = new ListBox();
        this.lnkClearHistory = new LinkLabel();

        // Launch Button
        this.btnLaunch = new Button();

        // Status Label
        this.lblStatus = new Label();

        // Quick Links Panel
        this.pnlQuickLinks = new Panel();
        this.btnGetClaude = new Button();
        this.btnOpenDirectory = new Button();
        this.btnClaudeWeb = new Button();

        this.SuspendLayout();

        // ===== Title Bar Panel =====
        this.pnlTitleBar.Dock = DockStyle.Top;
        this.pnlTitleBar.Height = 40;
        this.pnlTitleBar.Padding = new Padding(16, 0, 8, 0);

        // Title Label
        this.lblTitle.Text = "Claude Code Launcher";
        this.lblTitle.AutoSize = true;
        this.lblTitle.Location = new Point(16, 10);

        // Close Button
        this.btnClose.Text = "\u2715";
        this.btnClose.Size = new Size(32, 32);
        this.btnClose.FlatStyle = FlatStyle.Flat;
        this.btnClose.FlatAppearance.BorderSize = 0;
        this.btnClose.Cursor = Cursors.Hand;
        this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        // Minimize Button
        this.btnMinimize.Text = "\u2014";
        this.btnMinimize.Size = new Size(32, 32);
        this.btnMinimize.FlatStyle = FlatStyle.Flat;
        this.btnMinimize.FlatAppearance.BorderSize = 0;
        this.btnMinimize.Cursor = Cursors.Hand;
        this.btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        this.pnlTitleBar.Controls.Add(this.lblTitle);
        this.pnlTitleBar.Controls.Add(this.btnClose);
        this.pnlTitleBar.Controls.Add(this.btnMinimize);

        // ===== Directory Selection =====
        this.lblSelectDirectory.Text = "Select Directory:";
        this.lblSelectDirectory.AutoSize = true;
        this.lblSelectDirectory.Location = new Point(24, 56);

        this.pnlPathContainer.Location = new Point(24, 80);
        this.pnlPathContainer.Size = new Size(432, 40);
        this.pnlPathContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        this.txtPath.Location = new Point(0, 4);
        this.txtPath.Size = new Size(340, 32);
        this.txtPath.ReadOnly = true;
        this.txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        this.btnBrowse.Text = "Browse...";
        this.btnBrowse.Size = new Size(80, 32);
        this.btnBrowse.Location = new Point(352, 4);
        this.btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        this.pnlPathContainer.Controls.Add(this.txtPath);
        this.pnlPathContainer.Controls.Add(this.btnBrowse);

        // ===== Recent Directories =====
        this.lblRecentDirectories.Text = "Recent Directories:";
        this.lblRecentDirectories.AutoSize = true;
        this.lblRecentDirectories.Location = new Point(24, 136);

        this.pnlRecentContainer.Location = new Point(24, 160);
        this.pnlRecentContainer.Size = new Size(432, 180);
        this.pnlRecentContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        this.lstRecentDirectories.Dock = DockStyle.Fill;
        this.lstRecentDirectories.IntegralHeight = false;

        this.pnlRecentContainer.Controls.Add(this.lstRecentDirectories);

        this.lnkClearHistory.Text = "Clear History";
        this.lnkClearHistory.AutoSize = true;
        this.lnkClearHistory.Location = new Point(374, 348);
        this.lnkClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        // ===== Launch Button =====
        this.btnLaunch.Text = "LAUNCH CLAUDE CODE";
        this.btnLaunch.Size = new Size(432, 50);
        this.btnLaunch.Location = new Point(24, 376);
        this.btnLaunch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        // ===== Quick Links Panel =====
        this.pnlQuickLinks.Location = new Point(24, 436);
        this.pnlQuickLinks.Size = new Size(432, 28);
        this.pnlQuickLinks.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        // ===== Status Label =====
        this.lblStatus.Text = "";
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new Point(24, 470);
        this.lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        this.btnGetClaude.Text = "Get Claude CLI";
        this.btnGetClaude.Size = new Size(130, 24);
        this.btnGetClaude.Location = new Point(0, 0);
        this.btnGetClaude.FlatStyle = FlatStyle.Flat;
        this.btnGetClaude.Cursor = Cursors.Hand;

        this.btnOpenDirectory.Text = "Open Directory";
        this.btnOpenDirectory.Size = new Size(130, 24);
        this.btnOpenDirectory.Location = new Point(140, 0);
        this.btnOpenDirectory.FlatStyle = FlatStyle.Flat;
        this.btnOpenDirectory.Cursor = Cursors.Hand;

        this.btnClaudeWeb.Text = "Claude Web";
        this.btnClaudeWeb.Size = new Size(130, 24);
        this.btnClaudeWeb.Location = new Point(280, 0);
        this.btnClaudeWeb.FlatStyle = FlatStyle.Flat;
        this.btnClaudeWeb.Cursor = Cursors.Hand;

        this.pnlQuickLinks.Controls.Add(this.btnGetClaude);
        this.pnlQuickLinks.Controls.Add(this.btnOpenDirectory);
        this.pnlQuickLinks.Controls.Add(this.btnClaudeWeb);

        // ===== MainForm =====
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(480, 500);
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Claude Code Launcher";
        this.MinimumSize = new Size(400, 440);

        this.Controls.Add(this.pnlTitleBar);
        this.Controls.Add(this.lblSelectDirectory);
        this.Controls.Add(this.pnlPathContainer);
        this.Controls.Add(this.lblRecentDirectories);
        this.Controls.Add(this.pnlRecentContainer);
        this.Controls.Add(this.lnkClearHistory);
        this.Controls.Add(this.btnLaunch);
        this.Controls.Add(this.pnlQuickLinks);
        this.Controls.Add(this.lblStatus);

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private Panel pnlTitleBar;
    private Label lblTitle;
    private Button btnClose;
    private Button btnMinimize;
    private Label lblSelectDirectory;
    private Panel pnlPathContainer;
    private TextBox txtPath;
    private Button btnBrowse;
    private Label lblRecentDirectories;
    private Panel pnlRecentContainer;
    private ListBox lstRecentDirectories;
    private LinkLabel lnkClearHistory;
    private Button btnLaunch;
    private Label lblStatus;
    private Panel pnlQuickLinks;
    private Button btnGetClaude;
    private Button btnOpenDirectory;
    private Button btnClaudeWeb;
}
