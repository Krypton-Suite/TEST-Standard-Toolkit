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
    partial class TaskbarProgressTest
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
            this.grpBasicExamples = new Krypton.Toolkit.KryptonGroupBox();
            this.btnSetNormalProgress = new Krypton.Toolkit.KryptonButton();
            this.lblExample1 = new Krypton.Toolkit.KryptonLabel();
            this.btnSetIndeterminate = new Krypton.Toolkit.KryptonButton();
            this.lblExample2 = new Krypton.Toolkit.KryptonLabel();
            this.grpInteractiveExamples = new Krypton.Toolkit.KryptonGroupBox();
            this.btnResetProgress = new Krypton.Toolkit.KryptonButton();
            this.btnStopAnimation = new Krypton.Toolkit.KryptonButton();
            this.btnStartAnimation = new Krypton.Toolkit.KryptonButton();
            this.lblProgressStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblExample3 = new Krypton.Toolkit.KryptonLabel();
            this.grpStateExamples = new Krypton.Toolkit.KryptonGroupBox();
            this.btnSetNoProgress = new Krypton.Toolkit.KryptonButton();
            this.btnSetPaused = new Krypton.Toolkit.KryptonButton();
            this.btnSetError = new Krypton.Toolkit.KryptonButton();
            this.btnSetNormal = new Krypton.Toolkit.KryptonButton();
            this.lblExample4 = new Krypton.Toolkit.KryptonLabel();
            this.grpSyncWithTaskbar = new Krypton.Toolkit.KryptonGroupBox();
            this.btnAnimateSync = new Krypton.Toolkit.KryptonButton();
            this.btnResetSync = new Krypton.Toolkit.KryptonButton();
            this.btnIncrementSync = new Krypton.Toolkit.KryptonButton();
            this.btnDisableSync = new Krypton.Toolkit.KryptonButton();
            this.btnEnableSync = new Krypton.Toolkit.KryptonButton();
            this.syncProgressBar = new Krypton.Toolkit.KryptonProgressBar();
            this.lblSyncStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblExample5 = new Krypton.Toolkit.KryptonLabel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lblPropertyGrid = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicExamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicExamples.Panel)).BeginInit();
            this.grpBasicExamples.Panel.SuspendLayout();
            this.grpBasicExamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInteractiveExamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInteractiveExamples.Panel)).BeginInit();
            this.grpInteractiveExamples.Panel.SuspendLayout();
            this.grpInteractiveExamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples.Panel)).BeginInit();
            this.grpStateExamples.Panel.SuspendLayout();
            this.grpStateExamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSyncWithTaskbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSyncWithTaskbar.Panel)).BeginInit();
            this.grpSyncWithTaskbar.Panel.SuspendLayout();
            this.grpSyncWithTaskbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBasicExamples
            // 
            this.grpBasicExamples.Location = new System.Drawing.Point(12, 12);
            this.grpBasicExamples.Name = "grpBasicExamples";
            this.grpBasicExamples.Size = new System.Drawing.Size(380, 120);
            this.grpBasicExamples.TabIndex = 0;
            this.grpBasicExamples.Values.Heading = "Basic Examples";
            // 
            // grpBasicExamples.Panel
            // 
            this.grpBasicExamples.Panel.Controls.Add(this.btnSetIndeterminate);
            this.grpBasicExamples.Panel.Controls.Add(this.lblExample2);
            this.grpBasicExamples.Panel.Controls.Add(this.btnSetNormalProgress);
            this.grpBasicExamples.Panel.Controls.Add(this.lblExample1);
            // 
            // btnSetNormalProgress
            // 
            this.btnSetNormalProgress.Location = new System.Drawing.Point(15, 50);
            this.btnSetNormalProgress.Name = "btnSetNormalProgress";
            this.btnSetNormalProgress.Size = new System.Drawing.Size(160, 35);
            this.btnSetNormalProgress.TabIndex = 1;
            this.btnSetNormalProgress.Values.Text = "Set Normal Progress";
            // 
            // lblExample1
            // 
            this.lblExample1.Location = new System.Drawing.Point(15, 20);
            this.lblExample1.Name = "lblExample1";
            this.lblExample1.Size = new System.Drawing.Size(350, 20);
            this.lblExample1.TabIndex = 0;
            this.lblExample1.Values.Text = "Example 1: Set normal progress (0-100%)";
            // 
            // btnSetIndeterminate
            // 
            this.btnSetIndeterminate.Location = new System.Drawing.Point(200, 50);
            this.btnSetIndeterminate.Name = "btnSetIndeterminate";
            this.btnSetIndeterminate.Size = new System.Drawing.Size(160, 35);
            this.btnSetIndeterminate.TabIndex = 3;
            this.btnSetIndeterminate.Values.Text = "Set Indeterminate";
            // 
            // lblExample2
            // 
            this.lblExample2.Location = new System.Drawing.Point(15, 90);
            this.lblExample2.Name = "lblExample2";
            this.lblExample2.Size = new System.Drawing.Size(350, 20);
            this.lblExample2.TabIndex = 2;
            this.lblExample2.Values.Text = "Example 2: Indeterminate progress";
            // 
            // grpInteractiveExamples
            // 
            this.grpInteractiveExamples.Location = new System.Drawing.Point(12, 138);
            this.grpInteractiveExamples.Name = "grpInteractiveExamples";
            this.grpInteractiveExamples.Size = new System.Drawing.Size(380, 150);
            this.grpInteractiveExamples.TabIndex = 1;
            this.grpInteractiveExamples.Values.Heading = "Interactive Examples";
            // 
            // grpInteractiveExamples.Panel
            // 
            this.grpInteractiveExamples.Panel.Controls.Add(this.btnResetProgress);
            this.grpInteractiveExamples.Panel.Controls.Add(this.btnStopAnimation);
            this.grpInteractiveExamples.Panel.Controls.Add(this.btnStartAnimation);
            this.grpInteractiveExamples.Panel.Controls.Add(this.lblProgressStatus);
            this.grpInteractiveExamples.Panel.Controls.Add(this.lblExample3);
            // 
            // btnResetProgress
            // 
            this.btnResetProgress.Location = new System.Drawing.Point(265, 100);
            this.btnResetProgress.Name = "btnResetProgress";
            this.btnResetProgress.Size = new System.Drawing.Size(95, 35);
            this.btnResetProgress.TabIndex = 4;
            this.btnResetProgress.Values.Text = "Reset Progress";
            // 
            // btnStopAnimation
            // 
            this.btnStopAnimation.Enabled = false;
            this.btnStopAnimation.Location = new System.Drawing.Point(140, 100);
            this.btnStopAnimation.Name = "btnStopAnimation";
            this.btnStopAnimation.Size = new System.Drawing.Size(95, 35);
            this.btnStopAnimation.TabIndex = 3;
            this.btnStopAnimation.Values.Text = "Stop Animation";
            // 
            // btnStartAnimation
            // 
            this.btnStartAnimation.Location = new System.Drawing.Point(15, 100);
            this.btnStartAnimation.Name = "btnStartAnimation";
            this.btnStartAnimation.Size = new System.Drawing.Size(95, 35);
            this.btnStartAnimation.TabIndex = 2;
            this.btnStartAnimation.Values.Text = "Start Animation";
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.Location = new System.Drawing.Point(15, 60);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(350, 20);
            this.lblProgressStatus.TabIndex = 1;
            this.lblProgressStatus.Values.Text = "Progress: 0 / 100 (NoProgress)";
            // 
            // lblExample3
            // 
            this.lblExample3.Location = new System.Drawing.Point(15, 20);
            this.lblExample3.Name = "lblExample3";
            this.lblExample3.Size = new System.Drawing.Size(350, 20);
            this.lblExample3.TabIndex = 0;
            this.lblExample3.Values.Text = "Example 3: Animated progress";
            // 
            // grpStateExamples
            // 
            this.grpStateExamples.Location = new System.Drawing.Point(398, 12);
            this.grpStateExamples.Name = "grpStateExamples";
            this.grpStateExamples.Size = new System.Drawing.Size(380, 150);
            this.grpStateExamples.TabIndex = 2;
            this.grpStateExamples.Values.Heading = "Progress State Examples";
            // 
            // grpStateExamples.Panel
            // 
            this.grpStateExamples.Panel.Controls.Add(this.btnSetNoProgress);
            this.grpStateExamples.Panel.Controls.Add(this.btnSetPaused);
            this.grpStateExamples.Panel.Controls.Add(this.btnSetError);
            this.grpStateExamples.Panel.Controls.Add(this.btnSetNormal);
            this.grpStateExamples.Panel.Controls.Add(this.lblExample4);
            // 
            // btnSetNoProgress
            // 
            this.btnSetNoProgress.Location = new System.Drawing.Point(200, 100);
            this.btnSetNoProgress.Name = "btnSetNoProgress";
            this.btnSetNoProgress.Size = new System.Drawing.Size(160, 35);
            this.btnSetNoProgress.TabIndex = 4;
            this.btnSetNoProgress.Values.Text = "No Progress";
            // 
            // btnSetPaused
            // 
            this.btnSetPaused.Location = new System.Drawing.Point(15, 100);
            this.btnSetPaused.Name = "btnSetPaused";
            this.btnSetPaused.Size = new System.Drawing.Size(160, 35);
            this.btnSetPaused.TabIndex = 3;
            this.btnSetPaused.Values.Text = "Paused";
            // 
            // btnSetError
            // 
            this.btnSetError.Location = new System.Drawing.Point(200, 50);
            this.btnSetError.Name = "btnSetError";
            this.btnSetError.Size = new System.Drawing.Size(160, 35);
            this.btnSetError.TabIndex = 2;
            this.btnSetError.Values.Text = "Error";
            // 
            // btnSetNormal
            // 
            this.btnSetNormal.Location = new System.Drawing.Point(15, 50);
            this.btnSetNormal.Name = "btnSetNormal";
            this.btnSetNormal.Size = new System.Drawing.Size(160, 35);
            this.btnSetNormal.TabIndex = 1;
            this.btnSetNormal.Values.Text = "Normal";
            // 
            // lblExample4
            // 
            this.lblExample4.Location = new System.Drawing.Point(15, 20);
            this.lblExample4.Name = "lblExample4";
            this.lblExample4.Size = new System.Drawing.Size(350, 20);
            this.lblExample4.TabIndex = 0;
            this.lblExample4.Values.Text = "Example 4: Progress states";
            // 
            // grpSyncWithTaskbar
            // 
            this.grpSyncWithTaskbar.Location = new System.Drawing.Point(398, 168);
            this.grpSyncWithTaskbar.Name = "grpSyncWithTaskbar";
            this.grpSyncWithTaskbar.Size = new System.Drawing.Size(380, 180);
            this.grpSyncWithTaskbar.TabIndex = 5;
            this.grpSyncWithTaskbar.Values.Heading = "SyncWithTaskbar Example";
            // 
            // grpSyncWithTaskbar.Panel
            // 
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.btnAnimateSync);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.btnResetSync);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.btnIncrementSync);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.btnDisableSync);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.btnEnableSync);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.syncProgressBar);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.lblSyncStatus);
            this.grpSyncWithTaskbar.Panel.Controls.Add(this.lblExample5);
            // 
            // btnAnimateSync
            // 
            this.btnAnimateSync.Location = new System.Drawing.Point(200, 130);
            this.btnAnimateSync.Name = "btnAnimateSync";
            this.btnAnimateSync.Size = new System.Drawing.Size(160, 35);
            this.btnAnimateSync.TabIndex = 7;
            this.btnAnimateSync.Values.Text = "Animate";
            // 
            // btnResetSync
            // 
            this.btnResetSync.Location = new System.Drawing.Point(15, 130);
            this.btnResetSync.Name = "btnResetSync";
            this.btnResetSync.Size = new System.Drawing.Size(160, 35);
            this.btnResetSync.TabIndex = 6;
            this.btnResetSync.Values.Text = "Reset";
            // 
            // btnIncrementSync
            // 
            this.btnIncrementSync.Location = new System.Drawing.Point(200, 90);
            this.btnIncrementSync.Name = "btnIncrementSync";
            this.btnIncrementSync.Size = new System.Drawing.Size(160, 35);
            this.btnIncrementSync.TabIndex = 5;
            this.btnIncrementSync.Values.Text = "Increment";
            // 
            // btnDisableSync
            // 
            this.btnDisableSync.Location = new System.Drawing.Point(200, 50);
            this.btnDisableSync.Name = "btnDisableSync";
            this.btnDisableSync.Size = new System.Drawing.Size(160, 35);
            this.btnDisableSync.TabIndex = 4;
            this.btnDisableSync.Values.Text = "Disable Sync";
            // 
            // btnEnableSync
            // 
            this.btnEnableSync.Location = new System.Drawing.Point(15, 50);
            this.btnEnableSync.Name = "btnEnableSync";
            this.btnEnableSync.Size = new System.Drawing.Size(160, 35);
            this.btnEnableSync.TabIndex = 3;
            this.btnEnableSync.Values.Text = "Enable Sync";
            // 
            // syncProgressBar
            // 
            this.syncProgressBar.Location = new System.Drawing.Point(15, 90);
            this.syncProgressBar.Name = "syncProgressBar";
            this.syncProgressBar.Size = new System.Drawing.Size(160, 30);
            this.syncProgressBar.TabIndex = 2;
            // 
            // lblSyncStatus
            // 
            this.lblSyncStatus.Location = new System.Drawing.Point(15, 60);
            this.lblSyncStatus.Name = "lblSyncStatus";
            this.lblSyncStatus.Size = new System.Drawing.Size(350, 20);
            this.lblSyncStatus.TabIndex = 1;
            this.lblSyncStatus.Values.Text = "Progress Bar: 0 / 100 | Sync: Disabled";
            // 
            // lblExample5
            // 
            this.lblExample5.Location = new System.Drawing.Point(15, 20);
            this.lblExample5.Name = "lblExample5";
            this.lblExample5.Size = new System.Drawing.Size(350, 20);
            this.lblExample5.TabIndex = 0;
            this.lblExample5.Values.Text = "Example 5: KryptonProgressBar with SyncWithTaskbar";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(784, 40);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(350, 580);
            this.propertyGrid.TabIndex = 3;
            // 
            // lblPropertyGrid
            // 
            this.lblPropertyGrid.Location = new System.Drawing.Point(784, 12);
            this.lblPropertyGrid.Name = "lblPropertyGrid";
            this.lblPropertyGrid.Size = new System.Drawing.Size(350, 22);
            this.lblPropertyGrid.TabIndex = 4;
            this.lblPropertyGrid.Values.Text = "Property Grid (Taskbar properties are expandable - contains OverlayIcon, Progress, JumpList)";
            // 
            // TaskbarProgressTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 632);
            this.Controls.Add(this.lblPropertyGrid);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.grpSyncWithTaskbar);
            this.Controls.Add(this.grpStateExamples);
            this.Controls.Add(this.grpInteractiveExamples);
            this.Controls.Add(this.grpBasicExamples);
            this.Name = "TaskbarProgressTest";
            this.Text = "Taskbar Progress Test - KryptonForm";
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicExamples.Panel)).EndInit();
            this.grpBasicExamples.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicExamples)).EndInit();
            this.grpBasicExamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInteractiveExamples.Panel)).EndInit();
            this.grpInteractiveExamples.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInteractiveExamples)).EndInit();
            this.grpInteractiveExamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples.Panel)).EndInit();
            this.grpStateExamples.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStateExamples)).EndInit();
            this.grpStateExamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSyncWithTaskbar.Panel)).EndInit();
            this.grpSyncWithTaskbar.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSyncWithTaskbar)).EndInit();
            this.grpSyncWithTaskbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpBasicExamples;
        private Krypton.Toolkit.KryptonButton btnSetNormalProgress;
        private Krypton.Toolkit.KryptonLabel lblExample1;
        private Krypton.Toolkit.KryptonButton btnSetIndeterminate;
        private Krypton.Toolkit.KryptonLabel lblExample2;
        private Krypton.Toolkit.KryptonGroupBox grpInteractiveExamples;
        private Krypton.Toolkit.KryptonButton btnResetProgress;
        private Krypton.Toolkit.KryptonButton btnStopAnimation;
        private Krypton.Toolkit.KryptonButton btnStartAnimation;
        private Krypton.Toolkit.KryptonLabel lblProgressStatus;
        private Krypton.Toolkit.KryptonLabel lblExample3;
        private Krypton.Toolkit.KryptonGroupBox grpStateExamples;
        private Krypton.Toolkit.KryptonButton btnSetNoProgress;
        private Krypton.Toolkit.KryptonButton btnSetPaused;
        private Krypton.Toolkit.KryptonButton btnSetError;
        private Krypton.Toolkit.KryptonButton btnSetNormal;
        private Krypton.Toolkit.KryptonLabel lblExample4;
        private Krypton.Toolkit.KryptonGroupBox grpSyncWithTaskbar;
        private Krypton.Toolkit.KryptonButton btnAnimateSync;
        private Krypton.Toolkit.KryptonButton btnResetSync;
        private Krypton.Toolkit.KryptonButton btnIncrementSync;
        private Krypton.Toolkit.KryptonButton btnDisableSync;
        private Krypton.Toolkit.KryptonButton btnEnableSync;
        private Krypton.Toolkit.KryptonProgressBar syncProgressBar;
        private Krypton.Toolkit.KryptonLabel lblSyncStatus;
        private Krypton.Toolkit.KryptonLabel lblExample5;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Krypton.Toolkit.KryptonLabel lblPropertyGrid;
    }
}
