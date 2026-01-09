namespace ClaudeCodeLauncher.Theme;

/// <summary>
/// Defines Claude's brand colors and styling constants for the application.
/// </summary>
public static class ClaudeTheme
{
    // Primary Colors
    public static readonly Color PrimaryBackground = ColorTranslator.FromHtml("#1a1a1a");
    public static readonly Color SecondaryBackground = ColorTranslator.FromHtml("#2d2d2d");
    public static readonly Color TertiaryBackground = ColorTranslator.FromHtml("#252525");

    // Accent Colors (Claude Orange)
    public static readonly Color AccentColor = ColorTranslator.FromHtml("#E07A3D");
    public static readonly Color AccentHover = ColorTranslator.FromHtml("#FF8C42");
    public static readonly Color AccentPressed = ColorTranslator.FromHtml("#C66A2D");

    // Text Colors
    public static readonly Color TextPrimary = ColorTranslator.FromHtml("#FFFFFF");
    public static readonly Color TextSecondary = ColorTranslator.FromHtml("#B0B0B0");
    public static readonly Color TextMuted = ColorTranslator.FromHtml("#707070");

    // Border Colors
    public static readonly Color BorderColor = ColorTranslator.FromHtml("#3d3d3d");
    public static readonly Color BorderFocused = ColorTranslator.FromHtml("#E07A3D");

    // Status Colors
    public static readonly Color Success = ColorTranslator.FromHtml("#4CAF50");
    public static readonly Color Error = ColorTranslator.FromHtml("#F44336");
    public static readonly Color Warning = ColorTranslator.FromHtml("#FF9800");

    // Font Settings
    public static readonly string FontFamily = "Segoe UI";
    public static readonly float FontSizeSmall = 9f;
    public static readonly float FontSizeNormal = 10f;
    public static readonly float FontSizeMedium = 12f;
    public static readonly float FontSizeLarge = 14f;
    public static readonly float FontSizeTitle = 16f;

    // UI Dimensions
    public static readonly int BorderRadius = 8;
    public static readonly int ButtonHeight = 45;
    public static readonly int TextBoxHeight = 35;
    public static readonly int Padding = 16;
    public static readonly int SmallPadding = 8;

    /// <summary>
    /// Applies the dark theme to a form.
    /// </summary>
    public static void ApplyToForm(Form form)
    {
        form.BackColor = PrimaryBackground;
        form.ForeColor = TextPrimary;
        form.Font = new Font(FontFamily, FontSizeNormal);
    }

    /// <summary>
    /// Creates a styled button with Claude theme.
    /// </summary>
    public static void StyleButton(Button button, bool isPrimary = false)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = isPrimary ? 0 : 1;
        button.FlatAppearance.BorderColor = BorderColor;
        button.BackColor = isPrimary ? AccentColor : SecondaryBackground;
        button.ForeColor = TextPrimary;
        button.Font = new Font(FontFamily, FontSizeMedium, isPrimary ? FontStyle.Bold : FontStyle.Regular);
        button.Cursor = Cursors.Hand;

        if (isPrimary)
        {
            button.FlatAppearance.MouseOverBackColor = AccentHover;
            button.FlatAppearance.MouseDownBackColor = AccentPressed;
        }
        else
        {
            button.FlatAppearance.MouseOverBackColor = TertiaryBackground;
            button.FlatAppearance.MouseDownBackColor = PrimaryBackground;
        }
    }

    /// <summary>
    /// Creates a styled text box with Claude theme.
    /// </summary>
    public static void StyleTextBox(TextBox textBox)
    {
        textBox.BackColor = SecondaryBackground;
        textBox.ForeColor = TextPrimary;
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.Font = new Font(FontFamily, FontSizeNormal);
    }

    /// <summary>
    /// Creates a styled label with Claude theme.
    /// </summary>
    public static void StyleLabel(Label label, bool isTitle = false)
    {
        label.ForeColor = isTitle ? TextPrimary : TextSecondary;
        label.Font = new Font(FontFamily, isTitle ? FontSizeTitle : FontSizeNormal,
            isTitle ? FontStyle.Bold : FontStyle.Regular);
        label.BackColor = Color.Transparent;
    }

    /// <summary>
    /// Creates a styled ListBox with Claude theme.
    /// </summary>
    public static void StyleListBox(ListBox listBox)
    {
        listBox.BackColor = SecondaryBackground;
        listBox.ForeColor = TextPrimary;
        listBox.BorderStyle = BorderStyle.None;
        listBox.Font = new Font(FontFamily, FontSizeNormal);
    }

    /// <summary>
    /// Creates a styled panel with Claude theme.
    /// </summary>
    public static void StylePanel(Panel panel, bool hasBorder = false)
    {
        panel.BackColor = SecondaryBackground;
        if (hasBorder)
        {
            panel.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
