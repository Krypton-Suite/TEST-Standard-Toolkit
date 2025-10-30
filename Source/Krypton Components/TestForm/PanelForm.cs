﻿namespace TestForm;

using System.Data;

public partial class PanelForm : KryptonForm
{

    DataTable dt;

    public PanelForm()
    {
        InitializeComponent();

        kryptonDataGridView1.Columns.Clear();
        kryptonDataGridView1.AutoGenerateColumns = false;

        Column12 = new KryptonDataGridViewRatingColumn()
        {
            DataPropertyName = "Images",
            HeaderText = "Images int",
            RatingMaximum = 10,
            SortMode = DataGridViewColumnSortMode.Automatic
        };

        kryptonDataGridView1.Columns.Add(Column12);

        dt = new();
        dt.Columns.Add("Images", typeof(byte));

        dt.Rows.Add(((byte)1));
        dt.Rows.Add(((byte)2));
        dt.Rows.Add(((byte)3));
        dt.Rows.Add(((byte)4));

        kryptonDataGridView1.DataSource = dt;

        propertyGrid1.SelectedObject = null;
        propertyGrid1.SelectedObject = this;

        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuSeparator());
        var file = new KryptonContextMenuItem("File");
        items.Items.Add(file);
        SystemMenuValues.MenuItems.Add(items);

        items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem("Open"));
        items.Items.Add(new KryptonContextMenuItem("Close"));
        items.Items.Add(new KryptonContextMenuItem("Delete"));
        file.Items.Add(items);

    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        kryptonDataGridView1.InvalidateColumn(0);
        return;

        //MessageBox.Show(

        //    $"IsWindowsEleven: {OSUtilities.IsWindowsEleven}" +
        //    $"PlatformID: {OSUtilities.OsVersionInfo.PlatformId}"
        //    );
    }

    private void kryptonButton2_Click(object sender, EventArgs e)
    {
    }
}

