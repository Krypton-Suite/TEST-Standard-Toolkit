#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *  
 */
#endregion

namespace TestForm;

public partial class RadialMenuTest : KryptonForm
{
    public RadialMenuTest()
    {
        InitializeComponent();
        InitializeRadialMenus();
        SetupEventHandlers();
    }

    private void InitializeRadialMenus()
    {
        // ============================================
        // Radial Menu 1: Basic Example with 2 levels
        // ============================================
        InitializeBasicMenu();

        // ============================================
        // Radial Menu 2: Advanced Example with 3 levels
        // ============================================
        InitializeAdvancedMenu();

        // ============================================
        // Radial Menu 3: Customized Example
        // ============================================
        InitializeCustomizedMenu();
    }

    private void InitializeBasicMenu()
    {
        // Clear existing items
        kryptonRadialMenu1.Items.Clear();

        // Enable dragging and floating for menu 1
        kryptonRadialMenu1.Behavior.AllowDrag = true;
        kryptonRadialMenu1.Behavior.AllowFloat = true;

        // Level 0 (inner ring) - 4 items
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("File", GetSystemIcon("imageres", 1)) { Level = 0 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Edit", GetSystemIcon("imageres", 2)) { Level = 0 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("View", GetSystemIcon("imageres", 3)) { Level = 0 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Help", GetSystemIcon("imageres", 4)) { Level = 0 });

        // Level 1 (outer ring) - 8 items
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("New", GetSystemIcon("shell32", 1)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Open", GetSystemIcon("shell32", 4)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Save", GetSystemIcon("shell32", 259)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Print", GetSystemIcon("shell32", 6)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Cut", GetSystemIcon("shell32", 258)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Copy", GetSystemIcon("shell32", 259)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Paste", GetSystemIcon("shell32", 260)) { Level = 1 });
        kryptonRadialMenu1.Items.Add(new KryptonRadialMenuItem("Delete", GetSystemIcon("shell32", 132)) { Level = 1, Enabled = false });

        // Force repaint
        kryptonRadialMenu1.Invalidate();
    }

    private void InitializeAdvancedMenu()
    {
        // Clear existing items
        kryptonRadialMenu2.Items.Clear();

        // Level 0 (inner ring) - 6 items
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("Home", GetSystemIcon("imageres", 101)) { Level = 0 });
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("Insert", GetSystemIcon("imageres", 102)) { Level = 0 });
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("Design", GetSystemIcon("imageres", 103)) { Level = 0 });
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("Layout", GetSystemIcon("imageres", 104)) { Level = 0 });
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("Review", GetSystemIcon("imageres", 105)) { Level = 0 });
        kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem("View", GetSystemIcon("imageres", 106)) { Level = 0 });

        // Level 1 (middle ring) - 12 items
        for (int i = 1; i <= 12; i++)
        {
            kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem($"Item {i}", GetSystemIcon("shell32", i)) { Level = 1 });
        }

        // Level 2 (outer ring) - 18 items
        for (int i = 1; i <= 18; i++)
        {
            bool enabled = i % 3 != 0; // Disable every 3rd item
            kryptonRadialMenu2.Items.Add(new KryptonRadialMenuItem($"Action {i}", GetSystemIcon("shell32", 100 + i)) 
            { 
                Level = 2,
                Enabled = enabled
            });
        }

        // Force repaint
        kryptonRadialMenu2.Invalidate();
    }

    private void InitializeCustomizedMenu()
    {
        // Clear existing items
        kryptonRadialMenu3.Items.Clear();

        // Enable dragging and floating for menu 3
        kryptonRadialMenu3.Behavior.AllowDrag = true;
        kryptonRadialMenu3.Behavior.AllowFloat = true;

        // Level 0 (inner ring) - 3 items
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Start", GetSystemIcon("imageres", 107)) { Level = 0 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Settings", GetSystemIcon("imageres", 108)) { Level = 0 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Exit", GetSystemIcon("imageres", 109)) { Level = 0 });

        // Level 1 (outer ring) - 6 items
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option A", GetSystemIcon("shell32", 200)) { Level = 1 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option B", GetSystemIcon("shell32", 201)) { Level = 1 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option C", GetSystemIcon("shell32", 202)) { Level = 1 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option D", GetSystemIcon("shell32", 203)) { Level = 1 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option E", GetSystemIcon("shell32", 204)) { Level = 1 });
        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem("Option F", GetSystemIcon("shell32", 205)) { Level = 1 });

        // Force repaint
        kryptonRadialMenu3.Invalidate();
    }

    private Image? GetSystemIcon(string dllName, int index)
    {
        try
        {
            // Try to load system icons - fallback to null if not available
            // In a real scenario, you might want to use actual icon loading
            return null; // Simplified for this example
        }
        catch
        {
            return null;
        }
    }

    private void SetupEventHandlers()
    {
        // Menu 1 events
        kryptonRadialMenu1.ItemClick += RadialMenu1_ItemClick;
        kryptonRadialMenu1.ItemHover += RadialMenu1_ItemHover;
        kryptonRadialMenu1.MenuFloated += RadialMenu1_MenuFloated;
        kryptonRadialMenu1.MenuReturned += RadialMenu1_MenuReturned;

        // Menu 2 events
        kryptonRadialMenu2.ItemClick += RadialMenu2_ItemClick;
        kryptonRadialMenu2.ItemHover += RadialMenu2_ItemHover;

        // Menu 3 events
        kryptonRadialMenu3.ItemClick += RadialMenu3_ItemClick;
        kryptonRadialMenu3.ItemHover += RadialMenu3_ItemHover;
        kryptonRadialMenu3.MenuFloated += RadialMenu3_MenuFloated;
        kryptonRadialMenu3.MenuReturned += RadialMenu3_MenuReturned;

        // Property controls
        numericUpDownInnerRadius.ValueChanged += NumericUpDown_ValueChanged;
        numericUpDownRingThickness.ValueChanged += NumericUpDown_ValueChanged;
        numericUpDownCenterRadius.ValueChanged += NumericUpDown_ValueChanged;
        numericUpDownStartAngle.ValueChanged += NumericUpDown_ValueChanged;
    }

    private void RadialMenu1_ItemClick(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus1.Text = $"Clicked: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus1.StateCommon.ShortText.Color1 = Color.Green;
    }

    private void RadialMenu1_ItemHover(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus1.Text = $"Hovering: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus1.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void RadialMenu2_ItemClick(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus2.Text = $"Clicked: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus2.StateCommon.ShortText.Color1 = Color.Green;
        
        if (!e.Item.Enabled)
        {
            KryptonMessageBox.Show(this, $"Item '{e.Item.Text}' is disabled!", "Disabled Item", 
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void RadialMenu2_ItemHover(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus2.Text = $"Hovering: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus2.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void RadialMenu3_ItemClick(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus3.Text = $"Clicked: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus3.StateCommon.ShortText.Color1 = Color.Green;
        
        if (e.Item.Text == "Exit")
        {
            if (KryptonMessageBox.Show(this, "Are you sure you want to exit?", "Exit Confirmation",
                KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void RadialMenu3_ItemHover(object? sender, KryptonRadialMenuItemEventArgs e)
    {
        kryptonLabelStatus3.Text = $"Hovering: {e.Item.Text} (Level {e.Item.Level})";
        kryptonLabelStatus3.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void NumericUpDown_ValueChanged(object? sender, EventArgs e)
    {
        if (sender == numericUpDownInnerRadius)
        {
            kryptonRadialMenu3.Layout.InnerRadius = (int)numericUpDownInnerRadius.Value;
        }
        else if (sender == numericUpDownRingThickness)
        {
            kryptonRadialMenu3.Layout.RingThickness = (int)numericUpDownRingThickness.Value;
        }
        else if (sender == numericUpDownCenterRadius)
        {
            kryptonRadialMenu3.Layout.CenterRadius = (int)numericUpDownCenterRadius.Value;
        }
        else if (sender == numericUpDownStartAngle)
        {
            kryptonRadialMenu3.Layout.StartAngle = (float)numericUpDownStartAngle.Value;
        }
    }

    private void KryptonButtonReset_Click(object? sender, EventArgs e)
    {
        numericUpDownInnerRadius.Value = 20;
        numericUpDownRingThickness.Value = 60;
        numericUpDownCenterRadius.Value = 15;
        numericUpDownStartAngle.Value = -90;
    }

    private void KryptonButtonAddItem_Click(object? sender, EventArgs e)
    {
        int level = (int)numericUpDownItemLevel.Value;
        string text = kryptonTextBoxItemText.Text;
        
        if (string.IsNullOrWhiteSpace(text))
        {
            text = $"Item {kryptonRadialMenu3.Items.Count + 1}";
        }

        kryptonRadialMenu3.Items.Add(new KryptonRadialMenuItem(text) { Level = level });
        kryptonRadialMenu3.Invalidate();
    }

    private void KryptonButtonClearItems_Click(object? sender, EventArgs e)
    {
        kryptonRadialMenu3.Items.Clear();
        kryptonRadialMenu3.Invalidate();
    }

    private void KryptonButtonRefresh_Click(object? sender, EventArgs e)
    {
        InitializeCustomizedMenu();
        kryptonRadialMenu3.Invalidate();
    }

    private void RadialMenu1_MenuFloated(object? sender, EventArgs e)
    {
        kryptonLabelStatus1.Text = "Menu floated into its own window";
        kryptonLabelStatus1.StateCommon.ShortText.Color1 = Color.Orange;
    }

    private void RadialMenu1_MenuReturned(object? sender, EventArgs e)
    {
        kryptonLabelStatus1.Text = "Menu returned to original location";
        kryptonLabelStatus1.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void RadialMenu3_MenuFloated(object? sender, EventArgs e)
    {
        kryptonLabelStatus3.Text = "Menu floated into its own window";
        kryptonLabelStatus3.StateCommon.ShortText.Color1 = Color.Orange;
    }

    private void RadialMenu3_MenuReturned(object? sender, EventArgs e)
    {
        kryptonLabelStatus3.Text = "Menu returned to original location";
        kryptonLabelStatus3.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void KryptonButtonFloat_Click(object? sender, EventArgs e)
    {
        if (!kryptonRadialMenu3.IsFloating)
        {
            kryptonRadialMenu3.Float();
        }
    }

    private void KryptonButtonReturn_Click(object? sender, EventArgs e)
    {
        if (kryptonRadialMenu3.IsFloating)
        {
            kryptonRadialMenu3.ReturnFromFloat();
        }
    }
}

