#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class RichTextBoxFormattingTest
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
            this.krtbRichTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.klblTitle = new Krypton.Toolkit.KryptonLabel();
            this.kcmbPalette = new Krypton.Toolkit.KryptonComboBox();
            this.klblPalette = new Krypton.Toolkit.KryptonLabel();
            this.kcmbInputControlStyle = new Krypton.Toolkit.KryptonComboBox();
            this.klblInputControlStyle = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnLoadSample = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadPlainText = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadLongRtf = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadMinimalRtf = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadComplexFormatting = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadBlackTextTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnVerifyFormatting = new Krypton.Toolkit.KryptonButton();
            this.kbtnPerformanceTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnClear = new Krypton.Toolkit.KryptonButton();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.krtbRichTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1000, 700);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // krtbRichTextBox
            // 
            this.krtbRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbRichTextBox.Location = new System.Drawing.Point(0, 130);
            this.krtbRichTextBox.Multiline = true;
            this.krtbRichTextBox.Name = "krtbRichTextBox";
            this.krtbRichTextBox.Size = new System.Drawing.Size(1000, 500);
            this.krtbRichTextBox.TabIndex = 2;
            this.krtbRichTextBox.WordWrap = true;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.klblInstructions);
            this.kryptonPanel2.Controls.Add(this.klblTitle);
            this.kryptonPanel2.Controls.Add(this.kcmbPalette);
            this.kryptonPanel2.Controls.Add(this.klblPalette);
            this.kryptonPanel2.Controls.Add(this.kcmbInputControlStyle);
            this.kryptonPanel2.Controls.Add(this.klblInputControlStyle);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1000, 130);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // klblTitle
            // 
            this.klblTitle.Location = new System.Drawing.Point(12, 12);
            this.klblTitle.Name = "klblTitle";
            this.klblTitle.Size = new System.Drawing.Size(976, 24);
            this.klblTitle.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.klblTitle.TabIndex = 0;
            this.klblTitle.Values.Text = "KryptonRichTextBox Formatting Preservation Test (Issue #2832)";
            // 
            // kcmbPalette
            // 
            this.kcmbPalette.DropDownWidth = 300;
            this.kcmbPalette.Location = new System.Drawing.Point(80, 45);
            this.kcmbPalette.Name = "kcmbPalette";
            this.kcmbPalette.Size = new System.Drawing.Size(300, 25);
            this.kcmbPalette.TabIndex = 1;
            this.kcmbPalette.SelectedIndexChanged += new System.EventHandler(this.KcmbPalette_SelectedIndexChanged);
            // 
            // klblPalette
            // 
            this.klblPalette.Location = new System.Drawing.Point(12, 47);
            this.klblPalette.Name = "klblPalette";
            this.klblPalette.Size = new System.Drawing.Size(62, 22);
            this.klblPalette.TabIndex = 2;
            this.klblPalette.Values.Text = "Palette:";
            // 
            // kcmbInputControlStyle
            // 
            this.kcmbInputControlStyle.DropDownWidth = 200;
            this.kcmbInputControlStyle.Location = new System.Drawing.Point(130, 75);
            this.kcmbInputControlStyle.Name = "kcmbInputControlStyle";
            this.kcmbInputControlStyle.Size = new System.Drawing.Size(200, 25);
            this.kcmbInputControlStyle.TabIndex = 4;
            this.kcmbInputControlStyle.SelectedIndexChanged += new System.EventHandler(this.KcmbInputControlStyle_SelectedIndexChanged);
            // 
            // klblInputControlStyle
            // 
            this.klblInputControlStyle.Location = new System.Drawing.Point(12, 77);
            this.klblInputControlStyle.Name = "klblInputControlStyle";
            this.klblInputControlStyle.Size = new System.Drawing.Size(112, 22);
            this.klblInputControlStyle.TabIndex = 5;
            this.klblInputControlStyle.Values.Text = "InputControlStyle:";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.klblStatus);
            this.kryptonPanel3.Controls.Add(this.kbtnPerformanceTest);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadBlackTextTest);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadComplexFormatting);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadMinimalRtf);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadLongRtf);
            this.kryptonPanel3.Controls.Add(this.kbtnClear);
            this.kryptonPanel3.Controls.Add(this.kbtnVerifyFormatting);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadPlainText);
            this.kryptonPanel3.Controls.Add(this.kbtnLoadSample);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 630);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(1000, 100);
            this.kryptonPanel3.TabIndex = 1;
            // 
            // kbtnLoadSample
            // 
            this.kbtnLoadSample.Location = new System.Drawing.Point(12, 10);
            this.kbtnLoadSample.Name = "kbtnLoadSample";
            this.kbtnLoadSample.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadSample.TabIndex = 0;
            this.kbtnLoadSample.Values.Text = "Load Sample RTF";
            this.kbtnLoadSample.Click += new System.EventHandler(this.KbtnLoadSample_Click);
            // 
            // kbtnLoadPlainText
            // 
            this.kbtnLoadPlainText.Location = new System.Drawing.Point(138, 10);
            this.kbtnLoadPlainText.Name = "kbtnLoadPlainText";
            this.kbtnLoadPlainText.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadPlainText.TabIndex = 1;
            this.kbtnLoadPlainText.Values.Text = "Load Plain Text";
            this.kbtnLoadPlainText.Click += new System.EventHandler(this.KbtnLoadPlainText_Click);
            // 
            // kbtnLoadLongRtf
            // 
            this.kbtnLoadLongRtf.Location = new System.Drawing.Point(264, 10);
            this.kbtnLoadLongRtf.Name = "kbtnLoadLongRtf";
            this.kbtnLoadLongRtf.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadLongRtf.TabIndex = 5;
            this.kbtnLoadLongRtf.Values.Text = "Load Long RTF";
            this.kbtnLoadLongRtf.Click += new System.EventHandler(this.KbtnLoadLongRtf_Click);
            // 
            // kbtnLoadMinimalRtf
            // 
            this.kbtnLoadMinimalRtf.Location = new System.Drawing.Point(390, 10);
            this.kbtnLoadMinimalRtf.Name = "kbtnLoadMinimalRtf";
            this.kbtnLoadMinimalRtf.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadMinimalRtf.TabIndex = 6;
            this.kbtnLoadMinimalRtf.Values.Text = "Load Minimal RTF";
            this.kbtnLoadMinimalRtf.Click += new System.EventHandler(this.KbtnLoadMinimalRtf_Click);
            // 
            // kbtnLoadComplexFormatting
            // 
            this.kbtnLoadComplexFormatting.Location = new System.Drawing.Point(516, 10);
            this.kbtnLoadComplexFormatting.Name = "kbtnLoadComplexFormatting";
            this.kbtnLoadComplexFormatting.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadComplexFormatting.TabIndex = 7;
            this.kbtnLoadComplexFormatting.Values.Text = "Load Complex";
            this.kbtnLoadComplexFormatting.Click += new System.EventHandler(this.KbtnLoadComplexFormatting_Click);
            // 
            // kbtnLoadBlackTextTest
            // 
            this.kbtnLoadBlackTextTest.Location = new System.Drawing.Point(642, 10);
            this.kbtnLoadBlackTextTest.Name = "kbtnLoadBlackTextTest";
            this.kbtnLoadBlackTextTest.Size = new System.Drawing.Size(120, 25);
            this.kbtnLoadBlackTextTest.TabIndex = 9;
            this.kbtnLoadBlackTextTest.Values.Text = "Load Black Text";
            this.kbtnLoadBlackTextTest.Click += new System.EventHandler(this.KbtnLoadBlackTextTest_Click);
            // 
            // kbtnVerifyFormatting
            // 
            this.kbtnVerifyFormatting.Location = new System.Drawing.Point(12, 41);
            this.kbtnVerifyFormatting.Name = "kbtnVerifyFormatting";
            this.kbtnVerifyFormatting.Size = new System.Drawing.Size(120, 25);
            this.kbtnVerifyFormatting.TabIndex = 2;
            this.kbtnVerifyFormatting.Values.Text = "Verify Formatting";
            this.kbtnVerifyFormatting.Click += new System.EventHandler(this.KbtnVerifyFormatting_Click);
            // 
            // kbtnPerformanceTest
            // 
            this.kbtnPerformanceTest.Location = new System.Drawing.Point(138, 41);
            this.kbtnPerformanceTest.Name = "kbtnPerformanceTest";
            this.kbtnPerformanceTest.Size = new System.Drawing.Size(150, 25);
            this.kbtnPerformanceTest.TabIndex = 8;
            this.kbtnPerformanceTest.Values.Text = "Performance Test";
            this.kbtnPerformanceTest.Click += new System.EventHandler(this.KbtnPerformanceTest_Click);
            // 
            // kbtnClear
            // 
            this.kbtnClear.Location = new System.Drawing.Point(294, 41);
            this.kbtnClear.Name = "kbtnClear";
            this.kbtnClear.Size = new System.Drawing.Size(100, 25);
            this.kbtnClear.TabIndex = 3;
            this.kbtnClear.Values.Text = "Clear";
            this.kbtnClear.Click += new System.EventHandler(this.KbtnClear_Click);
            // 
            // klblStatus
            // 
            this.klblStatus.Location = new System.Drawing.Point(12, 72);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(976, 22);
            this.klblStatus.TabIndex = 4;
            this.klblStatus.Values.Text = "Status: Ready";
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(400, 47);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(588, 22);
            this.klblInstructions.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this.klblInstructions.TabIndex = 3;
            this.klblInstructions.Values.Text = "Instructions: Use buttons below to load different RTF content types. Change palette/style to test formatting preservation and performance.";
            // 
            // RichTextBoxFormattingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "RichTextBoxFormattingTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RichTextBox Formatting Preservation Test - Issue #2832";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonLabel klblTitle;
        private Krypton.Toolkit.KryptonComboBox kcmbPalette;
        private Krypton.Toolkit.KryptonLabel klblPalette;
        private Krypton.Toolkit.KryptonComboBox kcmbInputControlStyle;
        private Krypton.Toolkit.KryptonLabel klblInputControlStyle;
        private Krypton.Toolkit.KryptonRichTextBox krtbRichTextBox;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonButton kbtnLoadSample;
        private Krypton.Toolkit.KryptonButton kbtnLoadPlainText;
        private Krypton.Toolkit.KryptonButton kbtnLoadLongRtf;
        private Krypton.Toolkit.KryptonButton kbtnLoadMinimalRtf;
        private Krypton.Toolkit.KryptonButton kbtnLoadComplexFormatting;
        private Krypton.Toolkit.KryptonButton kbtnLoadBlackTextTest;
        private Krypton.Toolkit.KryptonButton kbtnVerifyFormatting;
        private Krypton.Toolkit.KryptonButton kbtnPerformanceTest;
        private Krypton.Toolkit.KryptonButton kbtnClear;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
    }
}
