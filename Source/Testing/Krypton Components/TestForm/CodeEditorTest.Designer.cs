#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class CodeEditorTest
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kceEditor = new Krypton.Utilities.KryptonCodeEditor();
            this.kpnlTop = new Krypton.Toolkit.KryptonPanel();
            this.kpgbOptions = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlOptions = new Krypton.Toolkit.KryptonPanel();
            this.kchkAutoComplete = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkCodeFolding = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowLineNumbers = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbLanguage = new Krypton.Toolkit.KryptonComboBox();
            this.klblLanguage = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kpgbFile = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlFile = new Krypton.Toolkit.KryptonPanel();
            this.kbtnSaveFile = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadFile = new Krypton.Toolkit.KryptonButton();
            this.kpnlBottom = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).BeginInit();
            this.kpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions.Panel)).BeginInit();
            this.kpgbOptions.Panel.SuspendLayout();
            this.kpgbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).BeginInit();
            this.kpnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile.Panel)).BeginInit();
            this.kpgbFile.Panel.SuspendLayout();
            this.kpgbFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFile)).BeginInit();
            this.kpnlFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).BeginInit();
            this.kpnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kceEditor);
            this.kpnlMain.Controls.Add(this.kpnlTop);
            this.kpnlMain.Controls.Add(this.kpnlBottom);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlMain.Size = new System.Drawing.Size(1000, 700);
            this.kpnlMain.TabIndex = 0;
            // 
            // kceEditor
            // 
            this.kceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kceEditor.EnableCodeFolding = false;
            this.kceEditor.Language = Krypton.Utilities.Language.None;
            this.kceEditor.Location = new System.Drawing.Point(0, 80);
            this.kceEditor.Name = "kceEditor";
            this.kceEditor.ShowLineNumbers = true;
            this.kceEditor.Size = new System.Drawing.Size(1000, 570);
            this.kceEditor.TabIndex = 1;
            this.kceEditor.Text = "";
            // 
            // kpnlTop
            // 
            this.kpnlTop.Controls.Add(this.kpgbOptions);
            this.kpnlTop.Controls.Add(this.kpgbFile);
            this.kpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlTop.Location = new System.Drawing.Point(0, 0);
            this.kpnlTop.Name = "kpnlTop";
            this.kpnlTop.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlTop.Size = new System.Drawing.Size(1000, 80);
            this.kpnlTop.TabIndex = 0;
            // 
            // kpgbOptions
            // 
            this.kpgbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgbOptions.Location = new System.Drawing.Point(200, 0);
            this.kpgbOptions.Name = "kpgbOptions";
            // 
            // kpgbOptions.Panel
            // 
            this.kpgbOptions.Panel.Controls.Add(this.kpnlOptions);
            this.kpgbOptions.Size = new System.Drawing.Size(800, 80);
            this.kpgbOptions.TabIndex = 1;
            this.kpgbOptions.Values.Heading = "Editor Options";
            // 
            // kpnlOptions
            // 
            this.kpnlOptions.Controls.Add(this.kchkAutoComplete);
            this.kpnlOptions.Controls.Add(this.kchkCodeFolding);
            this.kpnlOptions.Controls.Add(this.kchkShowLineNumbers);
            this.kpnlOptions.Controls.Add(this.kcmbTheme);
            this.kpnlOptions.Controls.Add(this.klblTheme);
            this.kpnlOptions.Controls.Add(this.kcmbLanguage);
            this.kpnlOptions.Controls.Add(this.klblLanguage);
            this.kpnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlOptions.Location = new System.Drawing.Point(0, 0);
            this.kpnlOptions.Name = "kpnlOptions";
            this.kpnlOptions.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlOptions.Size = new System.Drawing.Size(796, 55);
            this.kpnlOptions.TabIndex = 0;
            // 
            // kchkAutoComplete
            // 
            this.kchkAutoComplete.Location = new System.Drawing.Point(600, 15);
            this.kchkAutoComplete.Name = "kchkAutoComplete";
            this.kchkAutoComplete.Size = new System.Drawing.Size(120, 25);
            this.kchkAutoComplete.TabIndex = 4;
            this.kchkAutoComplete.Values.Text = "Auto-Complete";
            this.kchkAutoComplete.CheckedChanged += new System.EventHandler(this.kchkAutoComplete_CheckedChanged);
            // 
            // kchkCodeFolding
            // 
            this.kchkCodeFolding.Location = new System.Drawing.Point(450, 15);
            this.kchkCodeFolding.Name = "kchkCodeFolding";
            this.kchkCodeFolding.Size = new System.Drawing.Size(120, 25);
            this.kchkCodeFolding.TabIndex = 3;
            this.kchkCodeFolding.Values.Text = "Code Folding";
            this.kchkCodeFolding.CheckedChanged += new System.EventHandler(this.kchkCodeFolding_CheckedChanged);
            // 
            // kchkShowLineNumbers
            // 
            this.kchkShowLineNumbers.Checked = true;
            this.kchkShowLineNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkShowLineNumbers.Location = new System.Drawing.Point(300, 15);
            this.kchkShowLineNumbers.Name = "kchkShowLineNumbers";
            this.kchkShowLineNumbers.Size = new System.Drawing.Size(120, 25);
            this.kchkShowLineNumbers.TabIndex = 2;
            this.kchkShowLineNumbers.Values.Text = "Line Numbers";
            this.kchkShowLineNumbers.CheckedChanged += new System.EventHandler(this.kchkShowLineNumbers_CheckedChanged);
            // 
            // kcmbLanguage
            // 
            this.kcmbLanguage.DropDownWidth = 150;
            this.kcmbLanguage.Location = new System.Drawing.Point(100, 15);
            this.kcmbLanguage.Name = "kcmbLanguage";
            this.kcmbLanguage.Size = new System.Drawing.Size(150, 25);
            this.kcmbLanguage.TabIndex = 1;
            this.kcmbLanguage.SelectedIndexChanged += new System.EventHandler(this.kcmbLanguage_SelectedIndexChanged);
            // 
            // klblLanguage
            // 
            this.klblLanguage.Location = new System.Drawing.Point(10, 17);
            this.klblLanguage.Name = "klblLanguage";
            this.klblLanguage.Size = new System.Drawing.Size(90, 23);
            this.klblLanguage.TabIndex = 0;
            this.klblLanguage.Values.Text = "Language:";
            // 
            // kcmbTheme
            // 
            this.kcmbTheme.DropDownWidth = 150;
            this.kcmbTheme.Location = new System.Drawing.Point(100, 45);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(150, 25);
            this.kcmbTheme.TabIndex = 5;
            this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
            // 
            // klblTheme
            // 
            this.klblTheme.Location = new System.Drawing.Point(10, 47);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(90, 23);
            this.klblTheme.TabIndex = 6;
            this.klblTheme.Values.Text = "Theme:";
            // 
            // kpgbFile
            // 
            this.kpgbFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.kpgbFile.Location = new System.Drawing.Point(0, 0);
            this.kpgbFile.Name = "kpgbFile";
            // 
            // kpgbFile.Panel
            // 
            this.kpgbFile.Panel.Controls.Add(this.kpnlFile);
            this.kpgbFile.Size = new System.Drawing.Size(200, 80);
            this.kpgbFile.TabIndex = 0;
            this.kpgbFile.Values.Heading = "File";
            // 
            // kpnlFile
            // 
            this.kpnlFile.Controls.Add(this.kbtnSaveFile);
            this.kpnlFile.Controls.Add(this.kbtnLoadFile);
            this.kpnlFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlFile.Location = new System.Drawing.Point(0, 0);
            this.kpnlFile.Name = "kpnlFile";
            this.kpnlFile.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlFile.Size = new System.Drawing.Size(196, 55);
            this.kpnlFile.TabIndex = 0;
            // 
            // kbtnSaveFile
            // 
            this.kbtnSaveFile.Location = new System.Drawing.Point(100, 10);
            this.kbtnSaveFile.Name = "kbtnSaveFile";
            this.kbtnSaveFile.Size = new System.Drawing.Size(90, 25);
            this.kbtnSaveFile.TabIndex = 1;
            this.kbtnSaveFile.Values.Text = "Save...";
            this.kbtnSaveFile.Click += new System.EventHandler(this.kbtnSaveFile_Click);
            // 
            // kbtnLoadFile
            // 
            this.kbtnLoadFile.Location = new System.Drawing.Point(10, 10);
            this.kbtnLoadFile.Name = "kbtnLoadFile";
            this.kbtnLoadFile.Size = new System.Drawing.Size(90, 25);
            this.kbtnLoadFile.TabIndex = 0;
            this.kbtnLoadFile.Values.Text = "Load...";
            this.kbtnLoadFile.Click += new System.EventHandler(this.kbtnLoadFile_Click);
            // 
            // kpnlBottom
            // 
            this.kpnlBottom.Controls.Add(this.kbtnClose);
            this.kpnlBottom.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlBottom.Location = new System.Drawing.Point(0, 650);
            this.kpnlBottom.Name = "kpnlBottom";
            this.kpnlBottom.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlBottom.Size = new System.Drawing.Size(1000, 50);
            this.kpnlBottom.TabIndex = 2;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(900, 12);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 25);
            this.kbtnClose.TabIndex = 1;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1000, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // CodeEditorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.kpnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "CodeEditorTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Editor Test";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).EndInit();
            this.kpnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions.Panel)).EndInit();
            this.kpgbOptions.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions)).EndInit();
            this.kpgbOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).EndInit();
            this.kpnlOptions.ResumeLayout(false);
            this.kpnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile.Panel)).EndInit();
            this.kpgbFile.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile)).EndInit();
            this.kpgbFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFile)).EndInit();
            this.kpnlFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).EndInit();
            this.kpnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Utilities.KryptonCodeEditor kceEditor;
        private Krypton.Toolkit.KryptonPanel kpnlTop;
        private Krypton.Toolkit.KryptonGroupBox kpgbOptions;
        private Krypton.Toolkit.KryptonPanel kpnlOptions;
        private Krypton.Toolkit.KryptonComboBox kcmbLanguage;
        private Krypton.Toolkit.KryptonLabel klblLanguage;
        private Krypton.Toolkit.KryptonComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonCheckBox kchkShowLineNumbers;
        private Krypton.Toolkit.KryptonCheckBox kchkCodeFolding;
        private Krypton.Toolkit.KryptonCheckBox kchkAutoComplete;
        private Krypton.Toolkit.KryptonGroupBox kpgbFile;
        private Krypton.Toolkit.KryptonPanel kpnlFile;
        private Krypton.Toolkit.KryptonButton kbtnLoadFile;
        private Krypton.Toolkit.KryptonButton kbtnSaveFile;
        private Krypton.Toolkit.KryptonPanel kpnlBottom;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
    }
}

