#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class TabbedEditorTest
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonTabbedEditor1 = new Krypton.Toolkit.KryptonTabbedEditor();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnAddTab = new Krypton.Toolkit.KryptonButton();
            this.ktxtInitialText = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kbtnCloseTab = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearTabs = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnSelectAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnPaste = new Krypton.Toolkit.KryptonButton();
            this.kbtnCut = new Krypton.Toolkit.KryptonButton();
            this.kbtnCopy = new Krypton.Toolkit.KryptonButton();
            this.kbtnRedo = new Krypton.Toolkit.KryptonButton();
            this.kbtnUndo = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnFindText = new Krypton.Toolkit.KryptonButton();
            this.ktxtFindText = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kbtnGetAllText = new Krypton.Toolkit.KryptonButton();
            this.kbtnGetSelectedText = new Krypton.Toolkit.KryptonButton();
            this.kbtnSetText = new Krypton.Toolkit.KryptonButton();
            this.ktxtSetText = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox4 = new Krypton.Toolkit.KryptonGroupBox();
            this.kcbTabAlignment = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.kcbTabStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabelEditorInfo = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4.Panel)).BeginInit();
            this.kryptonGroupBox4.Panel.SuspendLayout();
            this.kryptonGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcbTabAlignment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcbTabStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonTabbedEditor1);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1200, 700);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonTabbedEditor1
            // 
            this.kryptonTabbedEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTabbedEditor1.Location = new System.Drawing.Point(0, 0);
            this.kryptonTabbedEditor1.Name = "kryptonTabbedEditor1";
            this.kryptonTabbedEditor1.Size = new System.Drawing.Size(1200, 500);
            this.kryptonTabbedEditor1.TabIndex = 0;
            this.kryptonTabbedEditor1.SelectedIndexChanged += new System.EventHandler(this.KryptonTabbedEditor1_SelectedIndexChanged);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox4);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 500);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1200, 170);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kbtnClearTabs);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kbtnCloseTab);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kbtnAddTab);
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtInitialText);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(300, 170);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Tab Management";
            // 
            // kbtnAddTab
            // 
            this.kbtnAddTab.Location = new System.Drawing.Point(6, 6);
            this.kbtnAddTab.Name = "kbtnAddTab";
            this.kbtnAddTab.Size = new System.Drawing.Size(90, 25);
            this.kbtnAddTab.TabIndex = 0;
            this.kbtnAddTab.Values.Text = "Add Tab";
            this.kbtnAddTab.Click += new System.EventHandler(this.KbtnAddTab_Click);
            // 
            // ktxtInitialText
            // 
            this.ktxtInitialText.Location = new System.Drawing.Point(102, 30);
            this.ktxtInitialText.Name = "ktxtInitialText";
            this.ktxtInitialText.Size = new System.Drawing.Size(192, 23);
            this.ktxtInitialText.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(6, 33);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Initial Text:";
            // 
            // kbtnCloseTab
            // 
            this.kbtnCloseTab.Location = new System.Drawing.Point(102, 6);
            this.kbtnCloseTab.Name = "kbtnCloseTab";
            this.kbtnCloseTab.Size = new System.Drawing.Size(90, 25);
            this.kbtnCloseTab.TabIndex = 3;
            this.kbtnCloseTab.Values.Text = "Close Tab";
            this.kbtnCloseTab.Click += new System.EventHandler(this.KbtnCloseTab_Click);
            // 
            // kbtnClearTabs
            // 
            this.kbtnClearTabs.Location = new System.Drawing.Point(198, 6);
            this.kbtnClearTabs.Name = "kbtnClearTabs";
            this.kbtnClearTabs.Size = new System.Drawing.Size(96, 25);
            this.kbtnClearTabs.TabIndex = 4;
            this.kbtnClearTabs.Values.Text = "Clear All";
            this.kbtnClearTabs.Click += new System.EventHandler(this.KbtnClearTabs_Click);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(300, 0);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnSelectAll);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPaste);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnCut);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnCopy);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnRedo);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnUndo);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(300, 170);
            this.kryptonGroupBox2.TabIndex = 1;
            this.kryptonGroupBox2.Values.Heading = "Editor Operations";
            // 
            // kbtnSelectAll
            // 
            this.kbtnSelectAll.Location = new System.Drawing.Point(198, 59);
            this.kbtnSelectAll.Name = "kbtnSelectAll";
            this.kbtnSelectAll.Size = new System.Drawing.Size(90, 25);
            this.kbtnSelectAll.TabIndex = 5;
            this.kbtnSelectAll.Values.Text = "Select All";
            this.kbtnSelectAll.Click += new System.EventHandler(this.KbtnSelectAll_Click);
            // 
            // kbtnPaste
            // 
            this.kbtnPaste.Location = new System.Drawing.Point(102, 59);
            this.kbtnPaste.Name = "kbtnPaste";
            this.kbtnPaste.Size = new System.Drawing.Size(90, 25);
            this.kbtnPaste.TabIndex = 4;
            this.kbtnPaste.Values.Text = "Paste";
            this.kbtnPaste.Click += new System.EventHandler(this.KbtnPaste_Click);
            // 
            // kbtnCut
            // 
            this.kbtnCut.Location = new System.Drawing.Point(6, 59);
            this.kbtnCut.Name = "kbtnCut";
            this.kbtnCut.Size = new System.Drawing.Size(90, 25);
            this.kbtnCut.TabIndex = 3;
            this.kbtnCut.Values.Text = "Cut";
            this.kbtnCut.Click += new System.EventHandler(this.KbtnCut_Click);
            // 
            // kbtnCopy
            // 
            this.kbtnCopy.Location = new System.Drawing.Point(198, 30);
            this.kbtnCopy.Name = "kbtnCopy";
            this.kbtnCopy.Size = new System.Drawing.Size(90, 25);
            this.kbtnCopy.TabIndex = 2;
            this.kbtnCopy.Values.Text = "Copy";
            this.kbtnCopy.Click += new System.EventHandler(this.KbtnCopy_Click);
            // 
            // kbtnRedo
            // 
            this.kbtnRedo.Location = new System.Drawing.Point(102, 30);
            this.kbtnRedo.Name = "kbtnRedo";
            this.kbtnRedo.Size = new System.Drawing.Size(90, 25);
            this.kbtnRedo.TabIndex = 1;
            this.kbtnRedo.Values.Text = "Redo";
            this.kbtnRedo.Click += new System.EventHandler(this.KbtnRedo_Click);
            // 
            // kbtnUndo
            // 
            this.kbtnUndo.Location = new System.Drawing.Point(6, 30);
            this.kbtnUndo.Name = "kbtnUndo";
            this.kbtnUndo.Size = new System.Drawing.Size(90, 25);
            this.kbtnUndo.TabIndex = 0;
            this.kbtnUndo.Values.Text = "Undo";
            this.kbtnUndo.Click += new System.EventHandler(this.KbtnUndo_Click);
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonGroupBox3.Location = new System.Drawing.Point(600, 0);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnFindText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.ktxtFindText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnGetAllText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnGetSelectedText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnSetText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.ktxtSetText);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(300, 170);
            this.kryptonGroupBox3.TabIndex = 2;
            this.kryptonGroupBox3.Values.Heading = "Text Operations";
            // 
            // kbtnFindText
            // 
            this.kbtnFindText.Location = new System.Drawing.Point(198, 59);
            this.kbtnFindText.Name = "kbtnFindText";
            this.kbtnFindText.Size = new System.Drawing.Size(90, 25);
            this.kbtnFindText.TabIndex = 7;
            this.kbtnFindText.Values.Text = "Find";
            this.kbtnFindText.Click += new System.EventHandler(this.KbtnFindText_Click);
            // 
            // ktxtFindText
            // 
            this.ktxtFindText.Location = new System.Drawing.Point(102, 59);
            this.ktxtFindText.Name = "ktxtFindText";
            this.ktxtFindText.Size = new System.Drawing.Size(90, 23);
            this.ktxtFindText.TabIndex = 6;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(6, 62);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Find:";
            // 
            // kbtnGetAllText
            // 
            this.kbtnGetAllText.Location = new System.Drawing.Point(102, 30);
            this.kbtnGetAllText.Name = "kbtnGetAllText";
            this.kbtnGetAllText.Size = new System.Drawing.Size(90, 25);
            this.kbtnGetAllText.TabIndex = 4;
            this.kbtnGetAllText.Values.Text = "Get All Text";
            this.kbtnGetAllText.Click += new System.EventHandler(this.KbtnGetAllText_Click);
            // 
            // kbtnGetSelectedText
            // 
            this.kbtnGetSelectedText.Location = new System.Drawing.Point(6, 30);
            this.kbtnGetSelectedText.Name = "kbtnGetSelectedText";
            this.kbtnGetSelectedText.Size = new System.Drawing.Size(90, 25);
            this.kbtnGetSelectedText.TabIndex = 3;
            this.kbtnGetSelectedText.Values.Text = "Get Selected";
            this.kbtnGetSelectedText.Click += new System.EventHandler(this.KbtnGetSelectedText_Click);
            // 
            // kbtnSetText
            // 
            this.kbtnSetText.Location = new System.Drawing.Point(198, 30);
            this.kbtnSetText.Name = "kbtnSetText";
            this.kbtnSetText.Size = new System.Drawing.Size(90, 25);
            this.kbtnSetText.TabIndex = 2;
            this.kbtnSetText.Values.Text = "Set Text";
            this.kbtnSetText.Click += new System.EventHandler(this.KbtnSetText_Click);
            // 
            // ktxtSetText
            // 
            this.ktxtSetText.Location = new System.Drawing.Point(102, 6);
            this.ktxtSetText.Name = "ktxtSetText";
            this.ktxtSetText.Size = new System.Drawing.Size(192, 23);
            this.ktxtSetText.TabIndex = 1;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(6, 9);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "Set Text:";
            // 
            // kryptonGroupBox4
            // 
            this.kryptonGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox4.Location = new System.Drawing.Point(900, 0);
            this.kryptonGroupBox4.Name = "kryptonGroupBox4";
            // 
            // kryptonGroupBox4.Panel
            // 
            this.kryptonGroupBox4.Panel.Controls.Add(this.kcbTabAlignment);
            this.kryptonGroupBox4.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox4.Panel.Controls.Add(this.kcbTabStyle);
            this.kryptonGroupBox4.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox4.Size = new System.Drawing.Size(300, 170);
            this.kryptonGroupBox4.TabIndex = 3;
            this.kryptonGroupBox4.Values.Heading = "Appearance";
            // 
            // kcbTabAlignment
            // 
            this.kcbTabAlignment.DropDownWidth = 121;
            this.kcbTabAlignment.Items.AddRange(new object[] {
            "Top",
            "Bottom",
            "Left",
            "Right"});
            this.kcbTabAlignment.Location = new System.Drawing.Point(102, 33);
            this.kcbTabAlignment.Name = "kcbTabAlignment";
            this.kcbTabAlignment.Size = new System.Drawing.Size(192, 21);
            this.kcbTabAlignment.TabIndex = 3;
            this.kcbTabAlignment.SelectedIndexChanged += new System.EventHandler(this.KcbTabAlignment_SelectedIndexChanged);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(6, 36);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel5.TabIndex = 2;
            this.kryptonLabel5.Values.Text = "Alignment:";
            // 
            // kcbTabStyle
            // 
            this.kcbTabStyle.DropDownWidth = 121;
            this.kcbTabStyle.Items.AddRange(new object[] {
            "HighProfile",
            "StandardProfile",
            "LowProfile",
            "OneNote",
            "Dock",
            "DockAutoHidden",
            "Custom1",
            "Custom2",
            "Custom3"});
            this.kcbTabStyle.Location = new System.Drawing.Point(102, 6);
            this.kcbTabStyle.Name = "kcbTabStyle";
            this.kcbTabStyle.Size = new System.Drawing.Size(192, 21);
            this.kcbTabStyle.TabIndex = 1;
            this.kcbTabStyle.SelectedIndexChanged += new System.EventHandler(this.KcbTabStyle_SelectedIndexChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(6, 9);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Values.Text = "Tab Style:";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonLabelEditorInfo);
            this.kryptonPanel3.Controls.Add(this.kryptonLabelStatus);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 670);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(1200, 30);
            this.kryptonPanel3.TabIndex = 2;
            // 
            // kryptonLabelEditorInfo
            // 
            this.kryptonLabelEditorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabelEditorInfo.Location = new System.Drawing.Point(600, 6);
            this.kryptonLabelEditorInfo.Name = "kryptonLabelEditorInfo";
            this.kryptonLabelEditorInfo.Size = new System.Drawing.Size(594, 20);
            this.kryptonLabelEditorInfo.TabIndex = 1;
            this.kryptonLabelEditorInfo.Values.Text = "Editor Info";
            // 
            // kryptonLabelStatus
            // 
            this.kryptonLabelStatus.Location = new System.Drawing.Point(6, 6);
            this.kryptonLabelStatus.Name = "kryptonLabelStatus";
            this.kryptonLabelStatus.Size = new System.Drawing.Size(588, 20);
            this.kryptonLabelStatus.TabIndex = 0;
            this.kryptonLabelStatus.Values.Text = "Status";
            // 
            // TabbedEditorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "TabbedEditorTest";
            this.Text = "KryptonTabbedEditor - Comprehensive Example";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            this.kryptonGroupBox3.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4.Panel)).EndInit();
            this.kryptonGroupBox4.Panel.ResumeLayout(false);
            this.kryptonGroupBox4.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).EndInit();
            this.kryptonGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kcbTabAlignment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcbTabStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonTabbedEditor kryptonTabbedEditor1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonButton kbtnAddTab;
        private Krypton.Toolkit.KryptonTextBox ktxtInitialText;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonButton kbtnCloseTab;
        private Krypton.Toolkit.KryptonButton kbtnClearTabs;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonButton kbtnUndo;
        private Krypton.Toolkit.KryptonButton kbtnRedo;
        private Krypton.Toolkit.KryptonButton kbtnCopy;
        private Krypton.Toolkit.KryptonButton kbtnCut;
        private Krypton.Toolkit.KryptonButton kbtnPaste;
        private Krypton.Toolkit.KryptonButton kbtnSelectAll;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox ktxtSetText;
        private Krypton.Toolkit.KryptonButton kbtnSetText;
        private Krypton.Toolkit.KryptonButton kbtnGetSelectedText;
        private Krypton.Toolkit.KryptonButton kbtnGetAllText;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox ktxtFindText;
        private Krypton.Toolkit.KryptonButton kbtnFindText;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox4;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonComboBox kcbTabStyle;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonComboBox kcbTabAlignment;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonLabel kryptonLabelStatus;
        private Krypton.Toolkit.KryptonLabel kryptonLabelEditorInfo;
    }
}
