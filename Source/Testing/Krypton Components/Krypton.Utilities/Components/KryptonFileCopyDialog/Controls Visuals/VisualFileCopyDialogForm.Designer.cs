using System.Windows.Forms;

using Krypton.Toolkit;

namespace Krypton.Utilities
{
    partial class VisualFileCopyDialogForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnPause = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.pnlDetails = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileName = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblTimeRemaining = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblItemsRemaining = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblSpeed = new Krypton.Toolkit.KryptonWrapLabel();
            this.pnlSpeedGraph = new System.Windows.Forms.Panel();
            this.kbtnDetails = new Krypton.Toolkit.KryptonButton();
            this.kpbProgress = new Krypton.Toolkit.KryptonProgressBar();
            this.lblOperation = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblPercentage = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 200);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.kbtnPause, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnCancel, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 49);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kbtnPause
            // 
            this.kbtnPause.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnPause.AutoSize = true;
            this.kbtnPause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnPause.Location = new System.Drawing.Point(10, 13);
            this.kbtnPause.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnPause.Name = "kbtnPause";
            this.kbtnPause.Size = new System.Drawing.Size(45, 22);
            this.kbtnPause.TabIndex = 0;
            this.kbtnPause.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPause.Values.Text = "Pause";
            this.kbtnPause.Click += new System.EventHandler(this.kbtnPause_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnCancel.AutoSize = true;
            this.kbtnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnCancel.Location = new System.Drawing.Point(545, 13);
            this.kbtnCancel.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(45, 22);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(600, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.pnlDetails);
            this.kryptonPanel2.Controls.Add(this.kbtnDetails);
            this.kryptonPanel2.Controls.Add(this.pnlSpeedGraph);
            this.kryptonPanel2.Controls.Add(this.kpbProgress);
            this.kryptonPanel2.Controls.Add(this.lblPercentage);
            this.kryptonPanel2.Controls.Add(this.lblOperation);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(15);
            this.kryptonPanel2.Size = new System.Drawing.Size(600, 200);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.tableLayoutPanel2);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetails.Location = new System.Drawing.Point(15, 100);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(570, 85);
            this.pnlDetails.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblFileName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTimeRemaining, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblItemsRemaining, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblSpeed, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(570, 85);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileName.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblFileName.Location = new System.Drawing.Point(8, 5);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(554, 20);
            this.lblFileName.Text = "Name:";
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimeRemaining.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblTimeRemaining.Location = new System.Drawing.Point(8, 25);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(554, 20);
            this.lblTimeRemaining.Text = "Time remaining:";
            // 
            // lblItemsRemaining
            // 
            this.lblItemsRemaining.AutoSize = true;
            this.lblItemsRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemsRemaining.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblItemsRemaining.Location = new System.Drawing.Point(8, 45);
            this.lblItemsRemaining.Name = "lblItemsRemaining";
            this.lblItemsRemaining.Size = new System.Drawing.Size(554, 20);
            this.lblItemsRemaining.Text = "Items remaining:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSpeed.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblSpeed.Location = new System.Drawing.Point(8, 65);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(554, 20);
            this.lblSpeed.Text = "Speed:";
            // 
            // pnlSpeedGraph
            // 
            this.pnlSpeedGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSpeedGraph.BackColor = System.Drawing.Color.Transparent;
            this.pnlSpeedGraph.Location = new System.Drawing.Point(15, 75);
            this.pnlSpeedGraph.Name = "pnlSpeedGraph";
            this.pnlSpeedGraph.Size = new System.Drawing.Size(570, 20);
            this.pnlSpeedGraph.TabIndex = 4;
            this.pnlSpeedGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSpeedGraph_Paint);
            // 
            // kbtnDetails
            // 
            this.kbtnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnDetails.AutoSize = true;
            this.kbtnDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnDetails.Location = new System.Drawing.Point(15, 170);
            this.kbtnDetails.Margin = new System.Windows.Forms.Padding(0);
            this.kbtnDetails.Name = "kbtnDetails";
            this.kbtnDetails.Size = new System.Drawing.Size(75, 22);
            this.kbtnDetails.TabIndex = 3;
            this.kbtnDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDetails.Values.Text = "More details";
            this.kbtnDetails.Click += new System.EventHandler(this.kbtnDetails_Click);
            // 
            // kpbProgress
            // 
            this.kpbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kpbProgress.Location = new System.Drawing.Point(15, 50);
            this.kpbProgress.Name = "kpbProgress";
            this.kpbProgress.Size = new System.Drawing.Size(570, 23);
            this.kpbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.kpbProgress.TabIndex = 2;
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOperation.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblOperation.Location = new System.Drawing.Point(15, 15);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(570, 20);
            this.lblOperation.Text = "Copying items...";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPercentage.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblPercentage.Location = new System.Drawing.Point(15, 35);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(570, 20);
            this.lblPercentage.Text = "0% complete";
            // 
            // VisualFileCopyDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 250);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualFileCopyDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copying files...";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnCancel;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonProgressBar kpbProgress;
        private KryptonWrapLabel lblOperation;
        private KryptonWrapLabel lblPercentage;
        private KryptonButton kbtnPause;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonButton kbtnDetails;
        private Panel pnlSpeedGraph;
        private KryptonPanel pnlDetails;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonWrapLabel lblFileName;
        private KryptonWrapLabel lblTimeRemaining;
        private KryptonWrapLabel lblItemsRemaining;
        private KryptonWrapLabel lblSpeed;
    }
}
