#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class TaskbarThumbnailButtonsTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBasic = new Krypton.Toolkit.KryptonGroupBox();
            this.lblExample1 = new Krypton.Toolkit.KryptonLabel();
            this.grpStateExamples = new Krypton.Toolkit.KryptonGroupBox();
            this.btnShowNextPrev = new Krypton.Toolkit.KryptonButton();
            this.btnHideNextPrev = new Krypton.Toolkit.KryptonButton();
            this.btnTogglePauseDisabled = new Krypton.Toolkit.KryptonButton();
            this.lblExample3 = new Krypton.Toolkit.KryptonLabel();
            this.lblExample2 = new Krypton.Toolkit.KryptonLabel();
            this.grpClickStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.lblClickStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblHint = new Krypton.Toolkit.KryptonLabel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lblPropertyGrid = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasic.Panel)).BeginInit();
            this.grpBasic.Panel.SuspendLayout();
            this.grpBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples.Panel)).BeginInit();
            this.grpStateExamples.Panel.SuspendLayout();
            this.grpStateExamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpClickStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpClickStatus.Panel)).BeginInit();
            this.grpClickStatus.Panel.SuspendLayout();
            this.grpClickStatus.SuspendLayout();
            this.SuspendLayout();
            //
            // grpBasic
            //
            this.grpBasic.Location = new System.Drawing.Point(12, 12);
            this.grpBasic.Name = "grpBasic";
            this.grpBasic.Size = new System.Drawing.Size(380, 80);
            this.grpBasic.TabIndex = 0;
            this.grpBasic.Values.Heading = "Basic Example";
            //
            // grpBasic.Panel
            //
            this.grpBasic.Panel.Controls.Add(this.lblExample1);
            //
            // lblExample1
            //
            this.lblExample1.Location = new System.Drawing.Point(15, 20);
            this.lblExample1.Name = "lblExample1";
            this.lblExample1.Size = new System.Drawing.Size(350, 45);
            this.lblExample1.TabIndex = 0;
            this.lblExample1.Values.Text = "Example 1: Media-style thumbnail buttons (Play, Pause, Stop, Next, Previous). Minimize this form or hover over the taskbar button to see them.";
            this.lblExample1.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.lblExample1.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            //
            // grpStateExamples
            //
            this.grpStateExamples.Location = new System.Drawing.Point(12, 98);
            this.grpStateExamples.Name = "grpStateExamples";
            this.grpStateExamples.Size = new System.Drawing.Size(380, 160);
            this.grpStateExamples.TabIndex = 1;
            this.grpStateExamples.Values.Heading = "Button State Examples";
            //
            // grpStateExamples.Panel
            //
            this.grpStateExamples.Panel.Controls.Add(this.btnShowNextPrev);
            this.grpStateExamples.Panel.Controls.Add(this.btnHideNextPrev);
            this.grpStateExamples.Panel.Controls.Add(this.btnTogglePauseDisabled);
            this.grpStateExamples.Panel.Controls.Add(this.lblExample3);
            this.grpStateExamples.Panel.Controls.Add(this.lblExample2);
            //
            // btnShowNextPrev
            //
            this.btnShowNextPrev.Location = new System.Drawing.Point(200, 110);
            this.btnShowNextPrev.Name = "btnShowNextPrev";
            this.btnShowNextPrev.Size = new System.Drawing.Size(160, 35);
            this.btnShowNextPrev.TabIndex = 4;
            this.btnShowNextPrev.Values.Text = "Show Next/Prev";
            //
            // btnHideNextPrev
            //
            this.btnHideNextPrev.Location = new System.Drawing.Point(15, 110);
            this.btnHideNextPrev.Name = "btnHideNextPrev";
            this.btnHideNextPrev.Size = new System.Drawing.Size(160, 35);
            this.btnHideNextPrev.TabIndex = 3;
            this.btnHideNextPrev.Values.Text = "Hide Next/Prev";
            //
            // btnTogglePauseDisabled
            //
            this.btnTogglePauseDisabled.Location = new System.Drawing.Point(15, 65);
            this.btnTogglePauseDisabled.Name = "btnTogglePauseDisabled";
            this.btnTogglePauseDisabled.Size = new System.Drawing.Size(160, 35);
            this.btnTogglePauseDisabled.TabIndex = 2;
            this.btnTogglePauseDisabled.Values.Text = "Toggle Pause Disabled";
            //
            // lblExample3
            //
            this.lblExample3.Location = new System.Drawing.Point(15, 90);
            this.lblExample3.Name = "lblExample3";
            this.lblExample3.Size = new System.Drawing.Size(350, 20);
            this.lblExample3.TabIndex = 1;
            this.lblExample3.Values.Text = "Example 3: Hide or show Next/Previous buttons";
            //
            // lblExample2
            //
            this.lblExample2.Location = new System.Drawing.Point(15, 45);
            this.lblExample2.Name = "lblExample2";
            this.lblExample2.Size = new System.Drawing.Size(350, 20);
            this.lblExample2.TabIndex = 0;
            this.lblExample2.Values.Text = "Example 2: Toggle Pause button disabled state";
            //
            // grpClickStatus
            //
            this.grpClickStatus.Location = new System.Drawing.Point(12, 264);
            this.grpClickStatus.Name = "grpClickStatus";
            this.grpClickStatus.Size = new System.Drawing.Size(380, 80);
            this.grpClickStatus.TabIndex = 2;
            this.grpClickStatus.Values.Heading = "Click Feedback";
            //
            // grpClickStatus.Panel
            //
            this.grpClickStatus.Panel.Controls.Add(this.lblClickStatus);
            //
            // lblClickStatus
            //
            this.lblClickStatus.Location = new System.Drawing.Point(15, 25);
            this.lblClickStatus.Name = "lblClickStatus";
            this.lblClickStatus.Size = new System.Drawing.Size(350, 20);
            this.lblClickStatus.TabIndex = 0;
            this.lblClickStatus.Values.Text = "Click a thumbnail button (minimize form, hover taskbar) to see feedback here.";
            //
            // lblHint
            //
            this.lblHint.Location = new System.Drawing.Point(12, 354);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(380, 35);
            this.lblHint.TabIndex = 3;
            this.lblHint.Values.Text = "Minimize this form, then hover over its taskbar button to see the thumbnail preview with buttons. Click them to trigger actions.";
            this.lblHint.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.lblHint.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            //
            // propertyGrid
            //
            this.propertyGrid.Location = new System.Drawing.Point(398, 40);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(350, 580);
            this.propertyGrid.TabIndex = 4;
            //
            // lblPropertyGrid
            //
            this.lblPropertyGrid.Location = new System.Drawing.Point(398, 12);
            this.lblPropertyGrid.Name = "lblPropertyGrid";
            this.lblPropertyGrid.Size = new System.Drawing.Size(350, 22);
            this.lblPropertyGrid.TabIndex = 5;
            this.lblPropertyGrid.Values.Text = "Property Grid (Taskbar.ThumbnailButtons - expandable)";
            //
            // TaskbarThumbnailButtonsTest
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 632);
            this.Controls.Add(this.lblPropertyGrid);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.grpClickStatus);
            this.Controls.Add(this.grpStateExamples);
            this.Controls.Add(this.grpBasic);
            this.Name = "TaskbarThumbnailButtonsTest";
            this.Text = "Taskbar Thumbnail Buttons Test - KryptonForm";
            ((System.ComponentModel.ISupportInitialize)(this.grpBasic.Panel)).EndInit();
            this.grpBasic.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBasic)).EndInit();
            this.grpBasic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples.Panel)).EndInit();
            this.grpStateExamples.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples)).EndInit();
            this.grpStateExamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpClickStatus.Panel)).EndInit();
            this.grpClickStatus.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpClickStatus)).EndInit();
            this.grpClickStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpBasic;
        private Krypton.Toolkit.KryptonLabel lblExample1;
        private Krypton.Toolkit.KryptonGroupBox grpStateExamples;
        private Krypton.Toolkit.KryptonButton btnShowNextPrev;
        private Krypton.Toolkit.KryptonButton btnHideNextPrev;
        private Krypton.Toolkit.KryptonButton btnTogglePauseDisabled;
        private Krypton.Toolkit.KryptonLabel lblExample3;
        private Krypton.Toolkit.KryptonLabel lblExample2;
        private Krypton.Toolkit.KryptonGroupBox grpClickStatus;
        private Krypton.Toolkit.KryptonLabel lblClickStatus;
        private Krypton.Toolkit.KryptonLabel lblHint;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Krypton.Toolkit.KryptonLabel lblPropertyGrid;
    }
}
