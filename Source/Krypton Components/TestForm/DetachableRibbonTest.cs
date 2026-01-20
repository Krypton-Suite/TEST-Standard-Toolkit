#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Test form demonstrating detachable ribbons (Issue #595).
/// Shows ribbons hosted on UserControls that can be loaded by plugins.
/// </summary>
public partial class DetachableRibbonTest : KryptonForm
{
    private readonly UserControl? _pluginControl;
    private readonly KryptonRibbon? _pluginRibbon;

    public DetachableRibbonTest()
    {
        InitializeComponent();
        CreatePluginRibbonDemo();
    }

    private void CreatePluginRibbonDemo()
    {
        // Simulate a plugin that loads a UserControl with its own ribbon
        _pluginControl = new UserControl
        {
            Dock = DockStyle.Fill,
            BackColor = SystemColors.Control
        };

        // Create a ribbon hosted on the UserControl (detachable ribbon)
        // Position at (0,0) to allow integration with form's custom chrome
        _pluginRibbon = new KryptonRibbon
        {
            Name = "PluginRibbon",
            Dock = DockStyle.Top,
            Location = Point.Empty
        };

        // Add tabs to the plugin ribbon
        var pluginHomeTab = new KryptonRibbonTab
        {
            Text = "Plugin Home"
        };

        var pluginGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Plugin Actions"
        };

        var pluginTriple = new KryptonRibbonGroupTriple();
        var pluginButton1 = new KryptonRibbonGroupButton
        {
            TextLine1 = "Action 1",
            TextLine2 = "Plugin action"
        };
        pluginButton1.Click += (s, e) =>
        {
            MessageBox.Show("Plugin Action 1 clicked!", "Detachable Ribbon Test", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var pluginButton2 = new KryptonRibbonGroupButton
        {
            TextLine1 = "Action 2",
            TextLine2 = "Another action"
        };
        pluginButton2.Click += (s, e) =>
        {
            MessageBox.Show("Plugin Action 2 clicked!", "Detachable Ribbon Test", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        pluginTriple.Items.Add(pluginButton1);
        pluginTriple.Items.Add(pluginButton2);
        pluginGroup.Items.Add(pluginTriple);
        pluginHomeTab.Groups.Add(pluginGroup);
        _pluginRibbon.RibbonTabs.Add(pluginHomeTab);

        // Add a context tab
        var pluginContext = new KryptonRibbonContext
        {
            ContextName = "PluginContext",
            ContextTitle = "Plugin Context"
        };

        var pluginContextTab = new KryptonRibbonTab
        {
            Text = "Plugin Context Tab",
            ContextName = "PluginContext"
        };

        var pluginContextGroup = new KryptonRibbonGroup
        {
            TextLine1 = "Context Actions"
        };

        var pluginContextButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Context Action",
            TextLine2 = "Contextual action"
        };
        pluginContextButton.Click += (s, e) =>
        {
            MessageBox.Show("Context action clicked!", "Detachable Ribbon Test", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var pluginContextTriple = new KryptonRibbonGroupTriple();
        pluginContextTriple.Items.Add(pluginContextButton);
        pluginContextGroup.Items.Add(pluginContextTriple);
        pluginContextTab.Groups.Add(pluginContextGroup);

        _pluginRibbon.RibbonTabs.Add(pluginContextTab);
        _pluginRibbon.RibbonContexts.Add(pluginContext);

        // Add the ribbon to the UserControl
        _pluginControl.Controls.Add(_pluginRibbon);

        // Add a label to show it's working
        var infoLabel = new KryptonLabel
        {
            Text = "This demonstrates detachable ribbons (Issue #595).\n\n" +
                   "The ribbon above is hosted on a UserControl, not directly on the form.\n" +
                   "This allows plugins to host their own ribbons.\n\n" +
                   "The ribbon should integrate with the form's custom chrome if positioned at the top-left.",
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            TextAlign = ContentAlignment.MiddleLeft
        };
        _pluginControl.Controls.Add(infoLabel);
        infoLabel.BringToFront();

        // Add the plugin control to the main panel
        kryptonPanel1.Controls.Add(_pluginControl);
    }

    private void btnLoadPlugin_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Plugin ribbon is already loaded!\n\n" +
                       "This demonstrates how a plugin can host a ribbon on a UserControl, " +
                       "which can then be loaded into the main application.", 
                       "Detachable Ribbon Test", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Information);
    }
}
