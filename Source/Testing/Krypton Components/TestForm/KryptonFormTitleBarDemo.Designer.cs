#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonFormTitleBarDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTableLayoutPanel1 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.kbtnInsertStandardItems = new Krypton.Toolkit.KryptonButton();
            this.kbtnAddButton = new Krypton.Toolkit.KryptonButton();
            this.kbtnRemoveLast = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnRebuild = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnToggleHomeVisible = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleHomeEnabled = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleSaveCommand = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge2 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnDetachTitleBar = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleRtl = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge3 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kbtnExit = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTableLayoutPanel2 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.klbLog = new Krypton.Toolkit.KryptonListBox();
            this.kbtnClearLog = new Krypton.Toolkit.KryptonButton();
            this.kryptonStatusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            this.klblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanel1.Controls.Add(this.kryptonStatusStrip1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(920, 580);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            this.kryptonSplitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonGroupBox1);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonGroupBox2);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(920, 558);
            this.kryptonSplitContainer1.SplitterDistance = 280;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            this.kryptonGroupBox1.Size = new System.Drawing.Size(920, 280);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Title Bar Controls";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonTableLayoutPanel1);
            // 
            // kryptonTableLayoutPanel1
            // 
            this.kryptonTableLayoutPanel1.ColumnCount = 5;
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnInsertStandardItems, 0, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnAddButton, 1, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnRemoveLast, 2, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnClearAll, 3, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnRebuild, 4, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonBorderEdge1, 0, 1);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnToggleHomeVisible, 0, 2);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnToggleHomeEnabled, 1, 2);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnToggleSaveCommand, 2, 2);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonBorderEdge2, 0, 3);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnDetachTitleBar, 0, 4);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnToggleRtl, 1, 4);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonBorderEdge3, 0, 5);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonLabel1, 0, 6);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonThemeComboBox1, 1, 6);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kbtnExit, 4, 6);
            this.kryptonTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonTableLayoutPanel1.Name = "kryptonTableLayoutPanel1";
            this.kryptonTableLayoutPanel1.RowCount = 7;
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.kryptonTableLayoutPanel1.Size = new System.Drawing.Size(916, 256);
            this.kryptonTableLayoutPanel1.TabIndex = 0;
            // 
            // kbtnInsertStandardItems
            // 
            this.kbtnInsertStandardItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnInsertStandardItems.Location = new System.Drawing.Point(3, 3);
            this.kbtnInsertStandardItems.Name = "kbtnInsertStandardItems";
            this.kbtnInsertStandardItems.Size = new System.Drawing.Size(176, 26);
            this.kbtnInsertStandardItems.TabIndex = 0;
            this.kbtnInsertStandardItems.Values.Text = "Insert Standard Items";
            this.kbtnInsertStandardItems.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnInsertStandardItems.ToolTipValues.Heading = "Insert Standard Items";
            this.kbtnInsertStandardItems.ToolTipValues.Description = "Adds standard file and edit buttons (New, Open, Save, Cut, Copy, Paste, Print, etc.).";
            this.kbtnInsertStandardItems.ToolTipValues.EnableToolTips = true;
            this.kbtnInsertStandardItems.Click += new System.EventHandler(this.kbtnInsertStandardItems_Click);
            // 
            // kbtnAddButton
            // 
            this.kbtnAddButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnAddButton.Location = new System.Drawing.Point(185, 3);
            this.kbtnAddButton.Name = "kbtnAddButton";
            this.kbtnAddButton.Size = new System.Drawing.Size(223, 26);
            this.kbtnAddButton.TabIndex = 0;
            this.kbtnAddButton.Values.Text = "Add Button";
            this.kbtnAddButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAddButton.ToolTipValues.Heading = "Add Button";
            this.kbtnAddButton.ToolTipValues.Description = "Adds a new generic ButtonSpecAny to the title bar at runtime.";
            this.kbtnAddButton.ToolTipValues.EnableToolTips = true;
            this.kbtnAddButton.Click += new System.EventHandler(this.kbtnAddButton_Click);
            // 
            // kbtnRemoveLast
            // 
            this.kbtnRemoveLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnRemoveLast.Location = new System.Drawing.Point(232, 3);
            this.kbtnRemoveLast.Name = "kbtnRemoveLast";
            this.kbtnRemoveLast.Size = new System.Drawing.Size(223, 26);
            this.kbtnRemoveLast.TabIndex = 1;
            this.kbtnRemoveLast.Values.Text = "Remove Last";
            this.kbtnRemoveLast.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRemoveLast.ToolTipValues.Heading = "Remove Last";
            this.kbtnRemoveLast.ToolTipValues.Description = "Removes the last ButtonSpecAny from the title bar.";
            this.kbtnRemoveLast.ToolTipValues.EnableToolTips = true;
            this.kbtnRemoveLast.Click += new System.EventHandler(this.kbtnRemoveLast_Click);
            // 
            // kbtnClearAll
            // 
            this.kbtnClearAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnClearAll.Location = new System.Drawing.Point(461, 3);
            this.kbtnClearAll.Name = "kbtnClearAll";
            this.kbtnClearAll.Size = new System.Drawing.Size(223, 26);
            this.kbtnClearAll.TabIndex = 2;
            this.kbtnClearAll.Values.Text = "Clear All";
            this.kbtnClearAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClearAll.ToolTipValues.Heading = "Clear All";
            this.kbtnClearAll.ToolTipValues.Description = "Removes every button from the title bar at runtime.";
            this.kbtnClearAll.ToolTipValues.EnableToolTips = true;
            this.kbtnClearAll.Click += new System.EventHandler(this.kbtnClearAll_Click);
            // 
            // kbtnRebuild
            // 
            this.kbtnRebuild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnRebuild.Location = new System.Drawing.Point(690, 3);
            this.kbtnRebuild.Name = "kbtnRebuild";
            this.kbtnRebuild.Size = new System.Drawing.Size(223, 26);
            this.kbtnRebuild.TabIndex = 3;
            this.kbtnRebuild.Values.Text = "Rebuild Defaults";
            this.kbtnRebuild.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRebuild.ToolTipValues.Heading = "Rebuild Defaults";
            this.kbtnRebuild.ToolTipValues.Description = "Restores all four default buttons (Home, Save, Pin, Options).";
            this.kbtnRebuild.ToolTipValues.EnableToolTips = true;
            this.kbtnRebuild.Click += new System.EventHandler(this.kbtnRebuild_Click);
            // 
            // kryptonBorderEdge1  (separator row 1)
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 32);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(916, 10);
            this.kryptonBorderEdge1.TabIndex = 4;
            this.kryptonBorderEdge1.Text = "";
            this.kryptonTableLayoutPanel1.SetColumnSpan(this.kryptonBorderEdge1, 5);
            // 
            // kbtnToggleHomeVisible
            // 
            this.kbtnToggleHomeVisible.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnToggleHomeVisible.Location = new System.Drawing.Point(3, 45);
            this.kbtnToggleHomeVisible.Name = "kbtnToggleHomeVisible";
            this.kbtnToggleHomeVisible.Size = new System.Drawing.Size(223, 26);
            this.kbtnToggleHomeVisible.TabIndex = 5;
            this.kbtnToggleHomeVisible.Values.Text = "Toggle Home Visible";
            this.kbtnToggleHomeVisible.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleHomeVisible.ToolTipValues.Heading = "Toggle Home Visible";
            this.kbtnToggleHomeVisible.ToolTipValues.Description = "Toggles the Visible property of the Home button spec.";
            this.kbtnToggleHomeVisible.ToolTipValues.EnableToolTips = true;
            this.kbtnToggleHomeVisible.Click += new System.EventHandler(this.kbtnToggleHomeVisible_Click);
            // 
            // kbtnToggleHomeEnabled
            // 
            this.kbtnToggleHomeEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnToggleHomeEnabled.Location = new System.Drawing.Point(232, 45);
            this.kbtnToggleHomeEnabled.Name = "kbtnToggleHomeEnabled";
            this.kbtnToggleHomeEnabled.Size = new System.Drawing.Size(223, 26);
            this.kbtnToggleHomeEnabled.TabIndex = 6;
            this.kbtnToggleHomeEnabled.Values.Text = "Toggle Home Enabled";
            this.kbtnToggleHomeEnabled.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleHomeEnabled.ToolTipValues.Heading = "Toggle Home Enabled";
            this.kbtnToggleHomeEnabled.ToolTipValues.Description = "Toggles the Enabled (ButtonEnabled) property of the Home button spec.";
            this.kbtnToggleHomeEnabled.ToolTipValues.EnableToolTips = true;
            this.kbtnToggleHomeEnabled.Click += new System.EventHandler(this.kbtnToggleHomeEnabled_Click);
            // 
            // kbtnToggleSaveCommand
            // 
            this.kbtnToggleSaveCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnToggleSaveCommand.Location = new System.Drawing.Point(461, 45);
            this.kbtnToggleSaveCommand.Name = "kbtnToggleSaveCommand";
            this.kbtnToggleSaveCommand.Size = new System.Drawing.Size(223, 26);
            this.kbtnToggleSaveCommand.TabIndex = 7;
            this.kbtnToggleSaveCommand.Values.Text = "Toggle Save Command";
            this.kbtnToggleSaveCommand.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleSaveCommand.ToolTipValues.Heading = "Toggle Save Command";
            this.kbtnToggleSaveCommand.ToolTipValues.Description = "Enables/disables the KryptonCommand that drives the Save button spec.";
            this.kbtnToggleSaveCommand.ToolTipValues.EnableToolTips = true;
            this.kbtnToggleSaveCommand.Click += new System.EventHandler(this.kbtnToggleSaveCommand_Click);
            // 
            // kryptonBorderEdge2  (separator row 2)
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(0, 74);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(916, 10);
            this.kryptonBorderEdge2.TabIndex = 8;
            this.kryptonBorderEdge2.Text = "";
            this.kryptonTableLayoutPanel1.SetColumnSpan(this.kryptonBorderEdge2, 5);
            // 
            // kbtnDetachTitleBar
            // 
            this.kbtnDetachTitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnDetachTitleBar.Location = new System.Drawing.Point(3, 87);
            this.kbtnDetachTitleBar.Name = "kbtnDetachTitleBar";
            this.kbtnDetachTitleBar.Size = new System.Drawing.Size(223, 26);
            this.kbtnDetachTitleBar.TabIndex = 9;
            this.kbtnDetachTitleBar.Values.Text = "Detach TitleBar";
            this.kbtnDetachTitleBar.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDetachTitleBar.ToolTipValues.Heading = "Attach / Detach TitleBar";
            this.kbtnDetachTitleBar.ToolTipValues.Description = "Toggles the KryptonFormTitleBar component on the KryptonForm.TitleBar property.";
            this.kbtnDetachTitleBar.ToolTipValues.EnableToolTips = true;
            this.kbtnDetachTitleBar.Click += new System.EventHandler(this.kbtnDetachTitleBar_Click);
            // 
            // kbtnToggleRtl
            // 
            this.kbtnToggleRtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnToggleRtl.Location = new System.Drawing.Point(232, 87);
            this.kbtnToggleRtl.Name = "kbtnToggleRtl";
            this.kbtnToggleRtl.Size = new System.Drawing.Size(223, 26);
            this.kbtnToggleRtl.TabIndex = 10;
            this.kbtnToggleRtl.Values.Text = "Toggle RTL Layout";
            this.kbtnToggleRtl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRtl.ToolTipValues.Heading = "Toggle RTL Layout";
            this.kbtnToggleRtl.ToolTipValues.Description = "Flips RightToLeft / RightToLeftLayout; title bar buttons mirror automatically.";
            this.kbtnToggleRtl.ToolTipValues.EnableToolTips = true;
            this.kbtnToggleRtl.Click += new System.EventHandler(this.kbtnToggleRtl_Click);
            // 
            // kryptonBorderEdge3  (separator row 3)
            // 
            this.kryptonBorderEdge3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonBorderEdge3.Location = new System.Drawing.Point(0, 116);
            this.kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            this.kryptonBorderEdge3.Size = new System.Drawing.Size(916, 10);
            this.kryptonBorderEdge3.TabIndex = 11;
            this.kryptonBorderEdge3.Text = "";
            this.kryptonTableLayoutPanel1.SetColumnSpan(this.kryptonBorderEdge3, 5);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 132);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel1.TabIndex = 12;
            this.kryptonLabel1.Values.Text = "Theme:";
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonThemeComboBox1.DropDownWidth = 270;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(232, 130);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(452, 21);
            this.kryptonThemeComboBox1.TabIndex = 13;
            this.kryptonTableLayoutPanel1.SetColumnSpan(this.kryptonThemeComboBox1, 2);
            this.kryptonThemeComboBox1.SelectedPaletteChanged += new System.EventHandler<Krypton.Toolkit.PaletteLayoutEventArgs>(this.kthemeCombo_SelectedPaletteChanged);
            // 
            // kbtnExit
            // 
            this.kbtnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnExit.Location = new System.Drawing.Point(690, 129);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(223, 26);
            this.kbtnExit.TabIndex = 14;
            this.kbtnExit.Values.Text = "Close";
            this.kbtnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExit.Click += new System.EventHandler((s, e) => Close());
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            this.kryptonGroupBox2.Size = new System.Drawing.Size(920, 274);
            this.kryptonGroupBox2.TabIndex = 1;
            this.kryptonGroupBox2.Values.Heading = "Event Log";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.kryptonTableLayoutPanel2);
            // 
            // kryptonTableLayoutPanel2
            // 
            this.kryptonTableLayoutPanel2.ColumnCount = 1;
            this.kryptonTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.kryptonTableLayoutPanel2.Controls.Add(this.klbLog, 0, 0);
            this.kryptonTableLayoutPanel2.Controls.Add(this.kbtnClearLog, 0, 1);
            this.kryptonTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonTableLayoutPanel2.Name = "kryptonTableLayoutPanel2";
            this.kryptonTableLayoutPanel2.RowCount = 2;
            this.kryptonTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.kryptonTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.kryptonTableLayoutPanel2.Size = new System.Drawing.Size(916, 250);
            this.kryptonTableLayoutPanel2.TabIndex = 0;
            // 
            // klbLog
            // 
            this.klbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klbLog.Location = new System.Drawing.Point(3, 3);
            this.klbLog.Name = "klbLog";
            this.klbLog.Size = new System.Drawing.Size(910, 210);
            this.klbLog.TabIndex = 0;
            // 
            // kbtnClearLog
            // 
            this.kbtnClearLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnClearLog.Location = new System.Drawing.Point(803, 219);
            this.kbtnClearLog.Name = "kbtnClearLog";
            this.kbtnClearLog.Size = new System.Drawing.Size(110, 28);
            this.kbtnClearLog.TabIndex = 1;
            this.kbtnClearLog.Values.Text = "Clear Log";
            this.kbtnClearLog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClearLog.Click += new System.EventHandler(this.kbtnClearLog_Click);
            // 
            // kryptonStatusStrip1
            // 
            this.kryptonStatusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonStatusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.klblStatus});
            this.kryptonStatusStrip1.Location = new System.Drawing.Point(0, 558);
            this.kryptonStatusStrip1.Name = "kryptonStatusStrip1";
            this.kryptonStatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.kryptonStatusStrip1.Size = new System.Drawing.Size(920, 22);
            this.kryptonStatusStrip1.TabIndex = 1;
            // 
            // klblStatus
            // 
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(39, 17);
            this.klblStatus.Text = "Ready.";
            // 
            // KryptonFormTitleBarDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 580);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(760, 500);
            this.Name = "KryptonFormTitleBarDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonFormTitleBar Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonTableLayoutPanel kryptonTableLayoutPanel1;
        private Krypton.Toolkit.KryptonButton kbtnInsertStandardItems;
        private Krypton.Toolkit.KryptonButton kbtnAddButton;
        private Krypton.Toolkit.KryptonButton kbtnRemoveLast;
        private Krypton.Toolkit.KryptonButton kbtnClearAll;
        private Krypton.Toolkit.KryptonButton kbtnRebuild;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonButton kbtnToggleHomeVisible;
        private Krypton.Toolkit.KryptonButton kbtnToggleHomeEnabled;
        private Krypton.Toolkit.KryptonButton kbtnToggleSaveCommand;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private Krypton.Toolkit.KryptonButton kbtnDetachTitleBar;
        private Krypton.Toolkit.KryptonButton kbtnToggleRtl;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonTableLayoutPanel kryptonTableLayoutPanel2;
        private Krypton.Toolkit.KryptonListBox klbLog;
        private Krypton.Toolkit.KryptonButton kbtnClearLog;
        private Krypton.Toolkit.KryptonStatusStrip kryptonStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel klblStatus;
    }
}
