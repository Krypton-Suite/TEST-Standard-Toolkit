#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonForm Help button functionality.
/// Tests Help button visibility, HelpRequested event, and HelpProvider integration.
/// </summary>
public partial class HelpButtonTest : KryptonForm
{
    private int _helpRequestedCount;
    private string? _lastHelpContext;

    public HelpButtonTest()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        // Set up initial state
        UpdateHelpButtonVisibility();
        UpdateStatusText();
        LogMessage("Help Button Test initialized. Click the Help button (?) in the title bar to test help functionality.");
    }

    private void UpdateHelpButtonVisibility()
    {
        // Help button only shows when HelpButton=true AND MinimizeBox=false AND MaximizeBox=false
        // This is standard Windows Forms behavior
        bool canShowHelp = !MaximizeBox && !MinimizeBox;
        HelpButton = canShowHelp && kchkEnableHelpButton.Checked;

        // Update checkbox states to reflect current form settings
        kchkMaximizeBox.Checked = MaximizeBox;
        kchkMinimizeBox.Checked = MinimizeBox;

        // If HelpButton is enabled but can't show, inform the user
        if (kchkEnableHelpButton.Checked && (MaximizeBox || MinimizeBox))
        {
            LogMessage("⚠ Help button requires both MinimizeBox and MaximizeBox to be false.");
        }
        else if (HelpButton)
        {
            LogMessage("✓ Help button is now visible in the title bar.");
        }
        else
        {
            LogMessage("✗ Help button is hidden.");
        }

        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        var status = $"HelpButton: {HelpButton} | MinimizeBox: {MinimizeBox} | MaximizeBox: {MaximizeBox} | Help Requests: {_helpRequestedCount}";
        klblStatus.Values.Text = status;
    }

    private void LogMessage(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        ktxtLog.Text += $"[{timestamp}] {message}\r\n";
        ktxtLog.SelectionStart = ktxtLog.Text.Length;
        ktxtLog.ScrollToCaret();
    }

    #region Event Handlers

    private void kchkEnableHelpButton_CheckedChanged(object sender, EventArgs e)
    {
        UpdateHelpButtonVisibility();
    }

    private void kchkMaximizeBox_CheckedChanged(object sender, EventArgs e)
    {
        MaximizeBox = kchkMaximizeBox.Checked;
        UpdateHelpButtonVisibility();
    }

    private void kchkMinimizeBox_CheckedChanged(object sender, EventArgs e)
    {
        MinimizeBox = kchkMinimizeBox.Checked;
        UpdateHelpButtonVisibility();
    }

    /// <summary>
    /// Handles HelpRequested event - triggered when Help button is clicked or F1 is pressed.
    /// </summary>
    protected override void OnHelpRequested(HelpEventArgs hlpevent)
    {
        base.OnHelpRequested(hlpevent);

        _helpRequestedCount++;
        _lastHelpContext = GetControlAtMousePosition(hlpevent.MousePos)?.Name ?? "Form";
        
        LogMessage($"🔔 HelpRequested event fired! Context: {_lastHelpContext}, Mouse Position: ({hlpevent.MousePos.X}, {hlpevent.MousePos.Y})");

        // Show help dialog
        ShowHelpDialog(_lastHelpContext);

        // Mark event as handled to prevent default help behavior
        hlpevent.Handled = true;

        UpdateStatusText();
    }

    private Control? GetControlAtMousePosition(Point mousePos)
    {
        // Find the control at the mouse position
        Point clientPos = PointToClient(mousePos);
        return GetChildAtPoint(clientPos, GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled);
    }

    private void ShowHelpDialog(string context)
    {
        var helpForm = new KryptonForm
        {
            Text = "Help Information",
            Size = new Size(500, 300),
            StartPosition = FormStartPosition.CenterParent,
            HelpButton = false,
            MaximizeBox = false,
            MinimizeBox = false,
            FormBorderStyle = FormBorderStyle.FixedDialog
        };

        var content = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(15)
        };

        var titleLabel = new KryptonLabel
        {
            Text = $"Help for: {context}",
            Location = new Point(15, 15),
            Size = new Size(470, 30)
        };
        titleLabel.StateCommon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

        var infoLabel = new KryptonLabel
        {
            Text = GetHelpText(context),
            Location = new Point(15, 50),
            Size = new Size(470, 150)
        };
        infoLabel.StateCommon.LongText.MultiLine = true;
        infoLabel.StateCommon.LongText.MultiLineH = PaletteRelativeAlign.Near;
        infoLabel.StateCommon.LongText.TextH = PaletteRelativeAlign.Near;

        var closeButton = new KryptonButton
        {
            Text = "Close",
            DialogResult = DialogResult.OK,
            Location = new Point(200, 220),
            Size = new Size(100, 30),
            Anchor = AnchorStyles.Bottom
        };

        content.Controls.Add(titleLabel);
        content.Controls.Add(infoLabel);
        content.Controls.Add(closeButton);
        helpForm.Controls.Add(content);
        helpForm.AcceptButton = closeButton;

        helpForm.ShowDialog(this);
    }

    private string GetHelpText(string context)
    {
        return context switch
        {
            "ktxtLog" => "This is a multi-line text box showing help event logs. All help requests are logged here with timestamps.",
            "kbtnTestButton" => "This is a test button. Click it to trigger a help request programmatically, or click the Help button in the title bar and then click this button.",
            "kchkEnableHelpButton" => "Enables or disables the Help button in the form title bar. Note: The Help button only appears when both MinimizeBox and MaximizeBox are false.",
            "kchkMaximizeBox" => "Controls the visibility of the Maximize button. When enabled, the Help button cannot be shown.",
            "kchkMinimizeBox" => "Controls the visibility of the Minimize button. When enabled, the Help button cannot be shown.",
            "Form" => "This is the main form. The Help button appears in the title bar when HelpButton=true and both MinimizeBox and MaximizeBox are false. Click the Help button (?) and then click any control to get context-sensitive help.",
            _ => $"Help information for {context}. This is context-sensitive help that appears when you click the Help button in the title bar and then click this control."
        };
    }

    private void kbtnTestButton_Click(object sender, EventArgs e)
    {
        // Programmatically trigger help for this button
        var helpEventArgs = new HelpEventArgs(PointToScreen(kbtnTestButton.Location));
        OnHelpRequested(helpEventArgs);
    }

    private void kbtnTestHelpProvider_Click(object sender, EventArgs e)
    {
        // Test HelpProvider integration
        TestHelpProviderIntegration();
    }

    private void kbtnClearLog_Click(object sender, EventArgs e)
    {
        ktxtLog.Clear();
        LogMessage("Log cleared.");
    }

    private void TestHelpProviderIntegration()
    {
        LogMessage("Testing HelpProvider integration...");

        // Create a HelpProvider
        using var helpProvider = new KryptonHelpProvider
        {
            HelpNamespace = "https://github.com/Krypton-Suite/Standard-Toolkit/wiki"
        };

        // Set help for the test button
        helpProvider.SetHelpKeyword(kbtnTestButton, "help-button-test");
        helpProvider.SetHelpNavigator(kbtnTestButton, HelpNavigator.Topic);
        helpProvider.SetShowHelp(kbtnTestButton, true);

        LogMessage("✓ HelpProvider configured. Click the Help button in the title bar and then click the 'Test Button' to test HelpProvider integration.");
        LogMessage("  Note: This will open the help file/URL specified in the HelpProvider.");
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        _helpRequestedCount = 0;
        _lastHelpContext = null;
        kchkEnableHelpButton.Checked = false;
        kchkMaximizeBox.Checked = true;
        kchkMinimizeBox.Checked = true;
        ktxtLog.Clear();
        LogMessage("Demo reset to initial state.");
        UpdateHelpButtonVisibility();
    }

    #endregion
}
