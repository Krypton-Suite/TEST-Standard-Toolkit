#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class TableLayoutPanelTest : KryptonForm
{
    private int _controlCounter;

    public TableLayoutPanelTest()
    {
        InitializeComponent();
        InitializeComboBoxes();
        InitializeTable();
        UpdateUIState();
    }

    private void InitializeTable()
    {
        // Set up initial table structure: 3 rows, 3 columns
        ktlpDemo.ColumnCount = 3;
        ktlpDemo.RowCount = 3;
        
        // Set column styles
        ktlpDemo.ColumnStyles.Clear();
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        
        // Set row styles
        ktlpDemo.RowStyles.Clear();
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        
        // Add some initial controls
        AddControlToCell(0, 0, "Cell (0,0)");
        AddControlToCell(1, 1, "Cell (1,1)");
        AddControlToCell(2, 2, "Cell (2,2)");
    }

    private void InitializeComboBoxes()
    {
        // Populate PanelBackStyle combo
        kcmbPanelBackStyle.Items.Clear();
        kcmbPanelBackStyle.Items.Add("PanelClient");
        kcmbPanelBackStyle.Items.Add("PanelAlternate");
        kcmbPanelBackStyle.Items.Add("PanelPrimary");
        kcmbPanelBackStyle.Items.Add("PanelSecondary");
        kcmbPanelBackStyle.SelectedIndex = 0; // "PanelClient"

        // Populate PaletteMode combo
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0; // "Global"

        // Populate SizeType combo
        kcmbSizeType.Items.Clear();
        kcmbSizeType.Items.Add("AutoSize");
        kcmbSizeType.Items.Add("Percent");
        kcmbSizeType.Items.Add("Absolute");
        kcmbSizeType.SelectedIndex = 1; // "Percent"

        // Populate GrowStyle combo
        kcmbGrowStyle.Items.Clear();
        kcmbGrowStyle.Items.Add("FixedSize");
        kcmbGrowStyle.Items.Add("AddRows");
        kcmbGrowStyle.Items.Add("AddColumns");
        kcmbGrowStyle.SelectedIndex = 0; // "FixedSize"
    }

    private void AddControlToCell(int column, int row, string text)
    {
        var button = new KryptonButton
        {
            Text = text,
            Dock = DockStyle.Fill,
            Margin = new Padding(5)
        };
        button.Click += (s, e) => 
        {
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {button.Text} clicked");
        };

        ktlpDemo.Controls.Add(button, column, row);
        _controlCounter++;
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added control to cell ({column}, {row})");
    }

    private void UpdateUIState()
    {
        if (ktlpDemo != null)
        {
            klblColumnCount.Text = $"Column Count: {ktlpDemo.ColumnCount}";
            klblRowCount.Text = $"Row Count: {ktlpDemo.RowCount}";
            klblControlCount.Text = $"Control Count: {ktlpDemo.Controls.Count}";
            klblPanelBackStyleStatus.Text = $"Panel Back Style: {ktlpDemo.PanelBackStyle}";
            klblGrowStyleStatus.Text = $"Grow Style: {ktlpDemo.GrowStyle}";
        }
    }

    private void kbtnAddControl_Click(object sender, EventArgs e)
    {
        try
        {
            var column = (int)knudColumn.Value;
            var row = (int)knudRow.Value;
            
            if (column >= ktlpDemo.ColumnCount)
            {
                ktlpDemo.ColumnCount = column + 1;
                ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / ktlpDemo.ColumnCount));
                // Redistribute percentages
                for (int i = 0; i < ktlpDemo.ColumnCount; i++)
                {
                    ktlpDemo.ColumnStyles[i].SizeType = SizeType.Percent;
                    ktlpDemo.ColumnStyles[i].Width = 100F / ktlpDemo.ColumnCount;
                }
            }
            
            if (row >= ktlpDemo.RowCount)
            {
                ktlpDemo.RowCount = row + 1;
                ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / ktlpDemo.RowCount));
                // Redistribute percentages
                for (int i = 0; i < ktlpDemo.RowCount; i++)
                {
                    ktlpDemo.RowStyles[i].SizeType = SizeType.Percent;
                    ktlpDemo.RowStyles[i].Height = 100F / ktlpDemo.RowCount;
                }
            }
            
            AddControlToCell(column, row, $"Control {++_controlCounter}");
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, 
                $"Error adding control:\n{ex.Message}", 
                "Error", 
                KryptonMessageBoxButtons.OK, 
                KryptonMessageBoxIcon.Error);
        }
    }

    private void kbtnClear_Click(object sender, EventArgs e)
    {
        ktlpDemo.Controls.Clear();
        _controlCounter = 0;
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Cleared all controls");
    }

    private void kbtnResetTable_Click(object sender, EventArgs e)
    {
        ktlpDemo.Controls.Clear();
        ktlpDemo.ColumnCount = 3;
        ktlpDemo.RowCount = 3;
        
        ktlpDemo.ColumnStyles.Clear();
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        
        ktlpDemo.RowStyles.Clear();
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        
        _controlCounter = 0;
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Reset table to 3x3");
    }

    private void kbtnAddSampleGrid_Click(object sender, EventArgs e)
    {
        // Create a sample 3x3 grid
        ktlpDemo.Controls.Clear();
        ktlpDemo.ColumnCount = 3;
        ktlpDemo.RowCount = 3;
        
        ktlpDemo.ColumnStyles.Clear();
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        
        ktlpDemo.RowStyles.Clear();
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        
        _controlCounter = 0;
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                AddControlToCell(col, row, $"({col},{row})");
            }
        }
        
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Created 3x3 sample grid");
    }

    private void knudColumns_ValueChanged(object sender, EventArgs e)
    {
        var newCount = (int)knudColumns.Value;
        if (newCount != ktlpDemo.ColumnCount)
        {
            var oldCount = ktlpDemo.ColumnCount;
            ktlpDemo.ColumnCount = newCount;
            
            // Adjust column styles
            if (newCount > oldCount)
            {
                for (int i = oldCount; i < newCount; i++)
                {
                    ktlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / newCount));
                }
            }
            else
            {
                // Remove excess styles
                while (ktlpDemo.ColumnStyles.Count > newCount)
                {
                    ktlpDemo.ColumnStyles.RemoveAt(ktlpDemo.ColumnStyles.Count - 1);
                }
            }
            
            // Redistribute percentages
            for (int i = 0; i < ktlpDemo.ColumnCount; i++)
            {
                ktlpDemo.ColumnStyles[i].SizeType = SizeType.Percent;
                ktlpDemo.ColumnStyles[i].Width = 100F / ktlpDemo.ColumnCount;
            }
            
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Column count changed to {newCount}");
        }
    }

    private void knudRows_ValueChanged(object sender, EventArgs e)
    {
        var newCount = (int)knudRows.Value;
        if (newCount != ktlpDemo.RowCount)
        {
            var oldCount = ktlpDemo.RowCount;
            ktlpDemo.RowCount = newCount;
            
            // Adjust row styles
            if (newCount > oldCount)
            {
                for (int i = oldCount; i < newCount; i++)
                {
                    ktlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / newCount));
                }
            }
            else
            {
                // Remove excess styles
                while (ktlpDemo.RowStyles.Count > newCount)
                {
                    ktlpDemo.RowStyles.RemoveAt(ktlpDemo.RowStyles.Count - 1);
                }
            }
            
            // Redistribute percentages
            for (int i = 0; i < ktlpDemo.RowCount; i++)
            {
                ktlpDemo.RowStyles[i].SizeType = SizeType.Percent;
                ktlpDemo.RowStyles[i].Height = 100F / ktlpDemo.RowCount;
            }
            
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Row count changed to {newCount}");
        }
    }

    private void knudPadding_ValueChanged(object sender, EventArgs e)
    {
        if (ktlpDemo != null)
        {
            var padding = (int)knudPadding.Value;
            ktlpDemo.Padding = new Padding(padding);
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Padding: {padding}");
        }
    }

    private void knudCellPadding_ValueChanged(object sender, EventArgs e)
    {
        if (ktlpDemo != null)
        {
            var cellPadding = (int)knudCellPadding.Value;
            ktlpDemo.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            // Note: CellPadding is not directly settable, but we can simulate with margins
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Cell padding simulation: {cellPadding}");
        }
    }

    private void kcmbPanelBackStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ktlpDemo == null)
        {
            return;
        }

        var selected = kcmbPanelBackStyle.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        ktlpDemo.PanelBackStyle = selected switch
        {
            "PanelClient" => PaletteBackStyle.PanelClient,
            "PanelAlternate" => PaletteBackStyle.PanelAlternate,
            _ => PaletteBackStyle.PanelClient
        };

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Panel Back Style: {ktlpDemo.PanelBackStyle}");
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ktlpDemo == null)
        {
            return;
        }

        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        ktlpDemo.PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Palette Mode: {ktlpDemo.PaletteMode}");
    }

    private void kcmbGrowStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ktlpDemo == null)
        {
            return;
        }

        var selected = kcmbGrowStyle.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        ktlpDemo.GrowStyle = selected switch
        {
            "FixedSize" => TableLayoutPanelGrowStyle.FixedSize,
            "AddRows" => TableLayoutPanelGrowStyle.AddRows,
            "AddColumns" => TableLayoutPanelGrowStyle.AddColumns,
            _ => TableLayoutPanelGrowStyle.FixedSize
        };

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Grow Style: {ktlpDemo.GrowStyle}");
    }

    private void AddEventToList(string message)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message)));
            return;
        }

        klstEvents.Items.Add(message);
        
        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        if (klstEvents.Items.Count > 500)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}

