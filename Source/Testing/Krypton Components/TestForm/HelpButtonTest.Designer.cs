#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class HelpButtonTest
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
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearLog = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestHelpProvider = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestButton = new Krypton.Toolkit.KryptonButton();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kchkMinimizeBox = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkMaximizeBox = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkEnableHelpButton = new Krypton.Toolkit.KryptonCheckBox();
            this.klblControls = new Krypton.Toolkit.KryptonLabel();
            this.klblLogTitle = new Krypton.Toolkit.KryptonLabel();
            this.ktxtLog = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ktxtLog)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnReset);
            this.kryptonPanel1.Controls.Add(this.kbtnClearLog);
            this.kryptonPanel1.Controls.Add(this.kbtnTestHelpProvider);
            this.kryptonPanel1.Controls.Add(this.kbtnTestButton);
            this.kryptonPanel1.Controls.Add(this.klblInstructions);
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Controls.Add(this.kchkMinimizeBox);
            this.kryptonPanel1.Controls.Add(this.kchkMaximizeBox);
            this.kryptonPanel1.Controls.Add(this.kchkEnableHelpButton);
            this.kryptonPanel1.Controls.Add(this.klblControls);
            this.kryptonPanel1.Controls.Add(this.kryptonSeparator1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 200);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnReset
            // 
            this.kbtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnReset.Location = new System.Drawing.Point(710, 160);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(75, 25);
            this.kbtnReset.TabIndex = 10;
            this.kbtnReset.Values.Text = "Reset";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            // 
            // kbtnClearLog
            // 
            this.kbtnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClearLog.Location = new System.Drawing.Point(620, 160);
            this.kbtnClearLog.Name = "kbtnClearLog";
            this.kbtnClearLog.Size = new System.Drawing.Size(75, 25);
            this.kbtnClearLog.TabIndex = 9;
            this.kbtnClearLog.Values.Text = "Clear Log";
            this.kbtnClearLog.Click += new System.EventHandler(this.kbtnClearLog_Click);
            // 
            // kbtnTestHelpProvider
            // 
            this.kbtnTestHelpProvider.Location = new System.Drawing.Point(170, 160);
            this.kbtnTestHelpProvider.Name = "kbtnTestHelpProvider";
            this.kbtnTestHelpProvider.Size = new System.Drawing.Size(140, 25);
            this.kbtnTestHelpProvider.TabIndex = 8;
            this.kbtnTestHelpProvider.Values.Text = "Test HelpProvider";
            this.kbtnTestHelpProvider.Click += new System.EventHandler(this.kbtnTestHelpProvider_Click);
            // 
            // kbtnTestButton
            // 
            this.kbtnTestButton.Location = new System.Drawing.Point(20, 160);
            this.kbtnTestButton.Name = "kbtnTestButton";
            this.kbtnTestButton.Size = new System.Drawing.Size(130, 25);
            this.kbtnTestButton.TabIndex = 7;
            this.kbtnTestButton.Values.Text = "Test Button";
            this.kbtnTestButton.Click += new System.EventHandler(this.kbtnTestButton_Click);
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(20, 100);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(760, 50);
            this.klblInstructions.StateCommon.LongText.MultiLine = true;
            this.klblInstructions.StateCommon.LongText.MultiLineH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblInstructions.StateCommon.LongText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblInstructions.TabIndex = 6;
            this.klblInstructions.Values.Text = "Instructions: Enable the Help button by unchecking both MinimizeBox and MaximizeBox, then check Enable Help Button. Click the Help button (?) in the title bar, then click any control to see context-sensitive help.";
            // 
            // klblStatus
            // 
            this.klblStatus.Location = new System.Drawing.Point(20, 70);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(760, 20);
            this.klblStatus.StateCommon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.klblStatus.TabIndex = 5;
            this.klblStatus.Values.Text = "Status: HelpButton: False | MinimizeBox: True | MaximizeBox: True | Help Requests: 0";
            // 
            // kchkMinimizeBox
            // 
            this.kchkMinimizeBox.Checked = true;
            this.kchkMinimizeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkMinimizeBox.Location = new System.Drawing.Point(240, 40);
            this.kchkMinimizeBox.Name = "kchkMinimizeBox";
            this.kchkMinimizeBox.Size = new System.Drawing.Size(100, 20);
            this.kchkMinimizeBox.TabIndex = 4;
            this.kchkMinimizeBox.Values.Text = "MinimizeBox";
            this.kchkMinimizeBox.CheckedChanged += new System.EventHandler(this.kchkMinimizeBox_CheckedChanged);
            // 
            // kchkMaximizeBox
            // 
            this.kchkMaximizeBox.Checked = true;
            this.kchkMaximizeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkMaximizeBox.Location = new System.Drawing.Point(130, 40);
            this.kchkMaximizeBox.Name = "kchkMaximizeBox";
            this.kchkMaximizeBox.Size = new System.Drawing.Size(100, 20);
            this.kchkMaximizeBox.TabIndex = 3;
            this.kchkMaximizeBox.Values.Text = "MaximizeBox";
            this.kchkMaximizeBox.CheckedChanged += new System.EventHandler(this.kchkMaximizeBox_CheckedChanged);
            // 
            // kchkEnableHelpButton
            // 
            this.kchkEnableHelpButton.Location = new System.Drawing.Point(20, 40);
            this.kchkEnableHelpButton.Name = "kchkEnableHelpButton";
            this.kchkEnableHelpButton.Size = new System.Drawing.Size(100, 20);
            this.kchkEnableHelpButton.TabIndex = 2;
            this.kchkEnableHelpButton.Values.Text = "HelpButton";
            this.kchkEnableHelpButton.CheckedChanged += new System.EventHandler(this.kchkEnableHelpButton_CheckedChanged);
            // 
            // klblControls
            // 
            this.klblControls.Location = new System.Drawing.Point(20, 15);
            this.klblControls.Name = "klblControls";
            this.klblControls.Size = new System.Drawing.Size(150, 20);
            this.klblControls.StateCommon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.klblControls.TabIndex = 1;
            this.klblControls.Values.Text = "Form Control Buttons:";
            // 
            // kryptonSeparator1
            // 
            this.kryptonSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonSeparator1.Location = new System.Drawing.Point(0, 195);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Orientation = Krypton.Toolkit.OrientationMode.Horizontal;
            this.kryptonSeparator1.Size = new System.Drawing.Size(800, 5);
            this.kryptonSeparator1.TabIndex = 11;
            // 
            // klblLogTitle
            // 
            this.klblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblLogTitle.Location = new System.Drawing.Point(0, 0);
            this.klblLogTitle.Name = "klblLogTitle";
            this.klblLogTitle.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.klblLogTitle.Size = new System.Drawing.Size(798, 25);
            this.klblLogTitle.StateCommon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.klblLogTitle.TabIndex = 1;
            this.klblLogTitle.Values.Text = "Help Event Log:";
            // 
            // ktxtLog
            // 
            this.ktxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtLog.Location = new System.Drawing.Point(0, 25);
            this.ktxtLog.Name = "ktxtLog";
            this.ktxtLog.ReadOnly = true;
            this.ktxtLog.Size = new System.Drawing.Size(798, 345);
            this.ktxtLog.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.ktxtLog.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ktxtLog.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ktxtLog.StateCommon.Content.Font = new System.Drawing.Font("Consolas", 9F);
            this.ktxtLog.TabIndex = 0;
            this.ktxtLog.Text = "";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 200);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtLog);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblLogTitle);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(800, 400);
            this.kryptonGroupBox1.TabIndex = 1;
            this.kryptonGroupBox1.Values.Heading = "Event Log";
            // 
            // HelpButtonTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.kryptonPanel1);
            this.HelpButton = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "HelpButtonTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Button Test - KryptonForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ktxtLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel klblControls;
        private Krypton.Toolkit.KryptonCheckBox kchkEnableHelpButton;
        private Krypton.Toolkit.KryptonCheckBox kchkMaximizeBox;
        private Krypton.Toolkit.KryptonCheckBox kchkMinimizeBox;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonButton kbtnTestButton;
        private Krypton.Toolkit.KryptonButton kbtnTestHelpProvider;
        private Krypton.Toolkit.KryptonButton kbtnClearLog;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonLabel klblLogTitle;
        private Krypton.Toolkit.KryptonRichTextBox ktxtLog;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
    }
