namespace TestForm
{
    partial class FlashingAlertDemo
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
            this.statusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpLabel1 = new Krypton.Toolkit.KryptonGroupBox();
            this.lblStatus1 = new Krypton.Toolkit.KryptonLabel();
            this.lblInterval1 = new Krypton.Toolkit.KryptonLabel();
            this.btnToggle1 = new Krypton.Toolkit.KryptonButton();
            this.btnStop1 = new Krypton.Toolkit.KryptonButton();
            this.btnStart1 = new Krypton.Toolkit.KryptonButton();
            this.btnChangeInterval1 = new Krypton.Toolkit.KryptonButton();
            this.grpLabel2 = new Krypton.Toolkit.KryptonGroupBox();
            this.lblStatus2 = new Krypton.Toolkit.KryptonLabel();
            this.lblInterval2 = new Krypton.Toolkit.KryptonLabel();
            this.btnToggle2 = new Krypton.Toolkit.KryptonButton();
            this.btnStop2 = new Krypton.Toolkit.KryptonButton();
            this.btnStart2 = new Krypton.Toolkit.KryptonButton();
            this.btnChangeInterval2 = new Krypton.Toolkit.KryptonButton();
            this.grpLabel3 = new Krypton.Toolkit.KryptonGroupBox();
            this.lblStatus3 = new Krypton.Toolkit.KryptonLabel();
            this.lblInterval3 = new Krypton.Toolkit.KryptonLabel();
            this.btnToggle3 = new Krypton.Toolkit.KryptonButton();
            this.btnStop3 = new Krypton.Toolkit.KryptonButton();
            this.btnStart3 = new Krypton.Toolkit.KryptonButton();
            this.btnChangeInterval3 = new Krypton.Toolkit.KryptonButton();
            this.grpLabel4 = new Krypton.Toolkit.KryptonGroupBox();
            this.lblStatus4 = new Krypton.Toolkit.KryptonLabel();
            this.btnToggle4 = new Krypton.Toolkit.KryptonButton();
            this.btnStop4 = new Krypton.Toolkit.KryptonButton();
            this.btnStart4 = new Krypton.Toolkit.KryptonButton();
            this.grpGlobalControls = new Krypton.Toolkit.KryptonGroupBox();
            this.btnStopAll = new Krypton.Toolkit.KryptonButton();
            this.btnStartAll = new Krypton.Toolkit.KryptonButton();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel1.Panel)).BeginInit();
            this.grpLabel1.Panel.SuspendLayout();
            this.grpLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel2.Panel)).BeginInit();
            this.grpLabel2.Panel.SuspendLayout();
            this.grpLabel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel3.Panel)).BeginInit();
            this.grpLabel3.Panel.SuspendLayout();
            this.grpLabel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel4.Panel)).BeginInit();
            this.grpLabel4.Panel.SuspendLayout();
            this.grpLabel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpGlobalControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpGlobalControls.Panel)).BeginInit();
            this.grpGlobalControls.Panel.SuspendLayout();
            this.grpGlobalControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1,
            this.statusLabel2,
            this.statusLabel3,
            this.statusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel1
            // 
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(195, 17);
            this.statusLabel1.Text = "Label 1: Red on Yellow (Fast - 300ms)";
            // 
            // statusLabel2
            // 
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(195, 17);
            this.statusLabel2.Text = "Label 2: White on Blue (Medium - 500ms)";
            // 
            // statusLabel3
            // 
            this.statusLabel3.Name = "statusLabel3";
            this.statusLabel3.Size = new System.Drawing.Size(195, 17);
            this.statusLabel3.Text = "Label 3: Black on Orange (Slow - 800ms)";
            // 
            // statusLabel4
            // 
            this.statusLabel4.Name = "statusLabel4";
            this.statusLabel4.Size = new System.Drawing.Size(195, 17);
            this.statusLabel4.Text = "Label 4: White on DarkRed (Direct Method)";
            // 
            // grpLabel1
            // 
            this.grpLabel1.Location = new System.Drawing.Point(12, 60);
            this.grpLabel1.Name = "grpLabel1";
            this.grpLabel1.Size = new System.Drawing.Size(380, 120);
            this.grpLabel1.TabIndex = 1;
            this.grpLabel1.Values.Heading = "Label 1 - Using FlashingAlertSettings (Red/Yellow, 300ms)";
            // 
            // grpLabel1.Panel
            // 
            this.grpLabel1.Panel.Controls.Add(this.lblStatus1);
            this.grpLabel1.Panel.Controls.Add(this.lblInterval1);
            this.grpLabel1.Panel.Controls.Add(this.btnToggle1);
            this.grpLabel1.Panel.Controls.Add(this.btnStop1);
            this.grpLabel1.Panel.Controls.Add(this.btnStart1);
            this.grpLabel1.Panel.Controls.Add(this.btnChangeInterval1);
            // 
            // lblStatus1
            // 
            this.lblStatus1.Location = new System.Drawing.Point(200, 20);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(50, 20);
            this.lblStatus1.TabIndex = 5;
            this.lblStatus1.Values.Text = "Stopped";
            // 
            // lblInterval1
            // 
            this.lblInterval1.Location = new System.Drawing.Point(200, 50);
            this.lblInterval1.Name = "lblInterval1";
            this.lblInterval1.Size = new System.Drawing.Size(100, 20);
            this.lblInterval1.TabIndex = 4;
            this.lblInterval1.Values.Text = "Interval: 300ms";
            // 
            // btnToggle1
            // 
            this.btnToggle1.Location = new System.Drawing.Point(15, 80);
            this.btnToggle1.Name = "btnToggle1";
            this.btnToggle1.Size = new System.Drawing.Size(110, 25);
            this.btnToggle1.TabIndex = 3;
            this.btnToggle1.Values.Text = "Toggle";
            this.btnToggle1.Click += new System.EventHandler(this.btnToggle1_Click);
            // 
            // btnStop1
            // 
            this.btnStop1.Location = new System.Drawing.Point(140, 50);
            this.btnStop1.Name = "btnStop1";
            this.btnStop1.Size = new System.Drawing.Size(50, 25);
            this.btnStop1.TabIndex = 2;
            this.btnStop1.Values.Text = "Stop";
            this.btnStop1.Click += new System.EventHandler(this.btnStop1_Click);
            // 
            // btnStart1
            // 
            this.btnStart1.Location = new System.Drawing.Point(15, 50);
            this.btnStart1.Name = "btnStart1";
            this.btnStart1.Size = new System.Drawing.Size(110, 25);
            this.btnStart1.TabIndex = 1;
            this.btnStart1.Values.Text = "Start";
            this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // btnChangeInterval1
            // 
            this.btnChangeInterval1.Location = new System.Drawing.Point(140, 80);
            this.btnChangeInterval1.Name = "btnChangeInterval1";
            this.btnChangeInterval1.Size = new System.Drawing.Size(110, 25);
            this.btnChangeInterval1.TabIndex = 0;
            this.btnChangeInterval1.Values.Text = "Change Interval";
            this.btnChangeInterval1.Click += new System.EventHandler(this.btnChangeInterval1_Click);
            // 
            // grpLabel2
            // 
            this.grpLabel2.Location = new System.Drawing.Point(408, 60);
            this.grpLabel2.Name = "grpLabel2";
            this.grpLabel2.Size = new System.Drawing.Size(380, 120);
            this.grpLabel2.TabIndex = 2;
            this.grpLabel2.Values.Heading = "Label 2 - Using FlashingAlertSettings (White/Blue, 500ms)";
            // 
            // grpLabel2.Panel
            // 
            this.grpLabel2.Panel.Controls.Add(this.lblStatus2);
            this.grpLabel2.Panel.Controls.Add(this.lblInterval2);
            this.grpLabel2.Panel.Controls.Add(this.btnToggle2);
            this.grpLabel2.Panel.Controls.Add(this.btnStop2);
            this.grpLabel2.Panel.Controls.Add(this.btnStart2);
            this.grpLabel2.Panel.Controls.Add(this.btnChangeInterval2);
            // 
            // lblStatus2
            // 
            this.lblStatus2.Location = new System.Drawing.Point(200, 20);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(50, 20);
            this.lblStatus2.TabIndex = 5;
            this.lblStatus2.Values.Text = "Stopped";
            // 
            // lblInterval2
            // 
            this.lblInterval2.Location = new System.Drawing.Point(200, 50);
            this.lblInterval2.Name = "lblInterval2";
            this.lblInterval2.Size = new System.Drawing.Size(100, 20);
            this.lblInterval2.TabIndex = 4;
            this.lblInterval2.Values.Text = "Interval: 500ms";
            // 
            // btnToggle2
            // 
            this.btnToggle2.Location = new System.Drawing.Point(15, 80);
            this.btnToggle2.Name = "btnToggle2";
            this.btnToggle2.Size = new System.Drawing.Size(110, 25);
            this.btnToggle2.TabIndex = 3;
            this.btnToggle2.Values.Text = "Toggle";
            this.btnToggle2.Click += new System.EventHandler(this.btnToggle2_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.Location = new System.Drawing.Point(140, 50);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(50, 25);
            this.btnStop2.TabIndex = 2;
            this.btnStop2.Values.Text = "Stop";
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // btnStart2
            // 
            this.btnStart2.Location = new System.Drawing.Point(15, 50);
            this.btnStart2.Name = "btnStart2";
            this.btnStart2.Size = new System.Drawing.Size(110, 25);
            this.btnStart2.TabIndex = 1;
            this.btnStart2.Values.Text = "Start";
            this.btnStart2.Click += new System.EventHandler(this.btnStart2_Click);
            // 
            // btnChangeInterval2
            // 
            this.btnChangeInterval2.Location = new System.Drawing.Point(140, 80);
            this.btnChangeInterval2.Name = "btnChangeInterval2";
            this.btnChangeInterval2.Size = new System.Drawing.Size(110, 25);
            this.btnChangeInterval2.TabIndex = 0;
            this.btnChangeInterval2.Values.Text = "Change Interval";
            this.btnChangeInterval2.Click += new System.EventHandler(this.btnChangeInterval2_Click);
            // 
            // grpLabel3
            // 
            this.grpLabel3.Location = new System.Drawing.Point(12, 186);
            this.grpLabel3.Name = "grpLabel3";
            this.grpLabel3.Size = new System.Drawing.Size(380, 120);
            this.grpLabel3.TabIndex = 3;
            this.grpLabel3.Values.Heading = "Label 3 - Using FlashingAlertSettings (Black/Orange, 800ms)";
            // 
            // grpLabel3.Panel
            // 
            this.grpLabel3.Panel.Controls.Add(this.lblStatus3);
            this.grpLabel3.Panel.Controls.Add(this.lblInterval3);
            this.grpLabel3.Panel.Controls.Add(this.btnToggle3);
            this.grpLabel3.Panel.Controls.Add(this.btnStop3);
            this.grpLabel3.Panel.Controls.Add(this.btnStart3);
            this.grpLabel3.Panel.Controls.Add(this.btnChangeInterval3);
            // 
            // lblStatus3
            // 
            this.lblStatus3.Location = new System.Drawing.Point(200, 20);
            this.lblStatus3.Name = "lblStatus3";
            this.lblStatus3.Size = new System.Drawing.Size(50, 20);
            this.lblStatus3.TabIndex = 5;
            this.lblStatus3.Values.Text = "Stopped";
            // 
            // lblInterval3
            // 
            this.lblInterval3.Location = new System.Drawing.Point(200, 50);
            this.lblInterval3.Name = "lblInterval3";
            this.lblInterval3.Size = new System.Drawing.Size(100, 20);
            this.lblInterval3.TabIndex = 4;
            this.lblInterval3.Values.Text = "Interval: 800ms";
            // 
            // btnToggle3
            // 
            this.btnToggle3.Location = new System.Drawing.Point(15, 80);
            this.btnToggle3.Name = "btnToggle3";
            this.btnToggle3.Size = new System.Drawing.Size(110, 25);
            this.btnToggle3.TabIndex = 3;
            this.btnToggle3.Values.Text = "Toggle";
            this.btnToggle3.Click += new System.EventHandler(this.btnToggle3_Click);
            // 
            // btnStop3
            // 
            this.btnStop3.Location = new System.Drawing.Point(140, 50);
            this.btnStop3.Name = "btnStop3";
            this.btnStop3.Size = new System.Drawing.Size(50, 25);
            this.btnStop3.TabIndex = 2;
            this.btnStop3.Values.Text = "Stop";
            this.btnStop3.Click += new System.EventHandler(this.btnStop3_Click);
            // 
            // btnStart3
            // 
            this.btnStart3.Location = new System.Drawing.Point(15, 50);
            this.btnStart3.Name = "btnStart3";
            this.btnStart3.Size = new System.Drawing.Size(110, 25);
            this.btnStart3.TabIndex = 1;
            this.btnStart3.Values.Text = "Start";
            this.btnStart3.Click += new System.EventHandler(this.btnStart3_Click);
            // 
            // btnChangeInterval3
            // 
            this.btnChangeInterval3.Location = new System.Drawing.Point(140, 80);
            this.btnChangeInterval3.Name = "btnChangeInterval3";
            this.btnChangeInterval3.Size = new System.Drawing.Size(110, 25);
            this.btnChangeInterval3.TabIndex = 0;
            this.btnChangeInterval3.Values.Text = "Change Interval";
            this.btnChangeInterval3.Click += new System.EventHandler(this.btnChangeInterval3_Click);
            // 
            // grpLabel4
            // 
            this.grpLabel4.Location = new System.Drawing.Point(408, 186);
            this.grpLabel4.Name = "grpLabel4";
            this.grpLabel4.Size = new System.Drawing.Size(380, 120);
            this.grpLabel4.TabIndex = 4;
            this.grpLabel4.Values.Heading = "Label 4 - Using Direct Method (White/DarkRed, 400ms)";
            // 
            // grpLabel4.Panel
            // 
            this.grpLabel4.Panel.Controls.Add(this.lblStatus4);
            this.grpLabel4.Panel.Controls.Add(this.btnToggle4);
            this.grpLabel4.Panel.Controls.Add(this.btnStop4);
            this.grpLabel4.Panel.Controls.Add(this.btnStart4);
            // 
            // lblStatus4
            // 
            this.lblStatus4.Location = new System.Drawing.Point(200, 20);
            this.lblStatus4.Name = "lblStatus4";
            this.lblStatus4.Size = new System.Drawing.Size(50, 20);
            this.lblStatus4.TabIndex = 3;
            this.lblStatus4.Values.Text = "Stopped";
            // 
            // btnToggle4
            // 
            this.btnToggle4.Location = new System.Drawing.Point(15, 80);
            this.btnToggle4.Name = "btnToggle4";
            this.btnToggle4.Size = new System.Drawing.Size(110, 25);
            this.btnToggle4.TabIndex = 2;
            this.btnToggle4.Values.Text = "Toggle";
            this.btnToggle4.Click += new System.EventHandler(this.btnToggle4_Click);
            // 
            // btnStop4
            // 
            this.btnStop4.Location = new System.Drawing.Point(140, 50);
            this.btnStop4.Name = "btnStop4";
            this.btnStop4.Size = new System.Drawing.Size(50, 25);
            this.btnStop4.TabIndex = 1;
            this.btnStop4.Values.Text = "Stop";
            this.btnStop4.Click += new System.EventHandler(this.btnStop4_Click);
            // 
            // btnStart4
            // 
            this.btnStart4.Location = new System.Drawing.Point(15, 50);
            this.btnStart4.Name = "btnStart4";
            this.btnStart4.Size = new System.Drawing.Size(110, 25);
            this.btnStart4.TabIndex = 0;
            this.btnStart4.Values.Text = "Start";
            this.btnStart4.Click += new System.EventHandler(this.btnStart4_Click);
            // 
            // grpGlobalControls
            // 
            this.grpGlobalControls.Location = new System.Drawing.Point(12, 312);
            this.grpGlobalControls.Name = "grpGlobalControls";
            this.grpGlobalControls.Size = new System.Drawing.Size(776, 60);
            this.grpGlobalControls.TabIndex = 5;
            this.grpGlobalControls.Values.Heading = "Global Controls";
            // 
            // grpGlobalControls.Panel
            // 
            this.grpGlobalControls.Panel.Controls.Add(this.btnStopAll);
            this.grpGlobalControls.Panel.Controls.Add(this.btnStartAll);
            // 
            // btnStopAll
            // 
            this.btnStopAll.Location = new System.Drawing.Point(200, 20);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(150, 30);
            this.btnStopAll.TabIndex = 1;
            this.btnStopAll.Values.Text = "Stop All";
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Location = new System.Drawing.Point(15, 20);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(150, 30);
            this.btnStartAll.TabIndex = 0;
            this.btnStartAll.Values.Text = "Start All";
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(12, 12);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(776, 40);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Values.Text = "This demo showcases the flashing alert feature for ToolStripStatusLabel controls. " +
    "Use the buttons below to start/stop flashing on individual labels or all labels " +
    "at once. Labels 1-3 use FlashingAlertSettings objects, while Label 4 uses direct" +
    " method calls.";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FlashingAlertDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.grpGlobalControls);
            this.Controls.Add(this.grpLabel4);
            this.Controls.Add(this.grpLabel3);
            this.Controls.Add(this.grpLabel2);
            this.Controls.Add(this.grpLabel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FlashingAlertDemo";
            this.Text = "Flashing Alert Demo";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel1)).EndInit();
            this.grpLabel1.Panel.ResumeLayout(false);
            this.grpLabel1.Panel.PerformLayout();
            this.grpLabel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel2)).EndInit();
            this.grpLabel2.Panel.ResumeLayout(false);
            this.grpLabel2.Panel.PerformLayout();
            this.grpLabel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel3)).EndInit();
            this.grpLabel3.Panel.ResumeLayout(false);
            this.grpLabel3.Panel.PerformLayout();
            this.grpLabel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLabel4)).EndInit();
            this.grpLabel4.Panel.ResumeLayout(false);
            this.grpLabel4.Panel.PerformLayout();
            this.grpLabel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpGlobalControls)).EndInit();
            this.grpGlobalControls.Panel.ResumeLayout(false);
            this.grpGlobalControls.Panel.PerformLayout();
            this.grpGlobalControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonStatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel4;
        private Krypton.Toolkit.KryptonGroupBox grpLabel1;
        private Krypton.Toolkit.KryptonButton btnStart1;
        private Krypton.Toolkit.KryptonButton btnChangeInterval1;
        private Krypton.Toolkit.KryptonButton btnStop1;
        private Krypton.Toolkit.KryptonButton btnToggle1;
        private Krypton.Toolkit.KryptonLabel lblInterval1;
        private Krypton.Toolkit.KryptonLabel lblStatus1;
        private Krypton.Toolkit.KryptonGroupBox grpLabel2;
        private Krypton.Toolkit.KryptonButton btnStart2;
        private Krypton.Toolkit.KryptonButton btnChangeInterval2;
        private Krypton.Toolkit.KryptonButton btnStop2;
        private Krypton.Toolkit.KryptonButton btnToggle2;
        private Krypton.Toolkit.KryptonLabel lblInterval2;
        private Krypton.Toolkit.KryptonLabel lblStatus2;
        private Krypton.Toolkit.KryptonGroupBox grpLabel3;
        private Krypton.Toolkit.KryptonButton btnStart3;
        private Krypton.Toolkit.KryptonButton btnChangeInterval3;
        private Krypton.Toolkit.KryptonButton btnStop3;
        private Krypton.Toolkit.KryptonButton btnToggle3;
        private Krypton.Toolkit.KryptonLabel lblInterval3;
        private Krypton.Toolkit.KryptonLabel lblStatus3;
        private Krypton.Toolkit.KryptonGroupBox grpLabel4;
        private Krypton.Toolkit.KryptonButton btnStart4;
        private Krypton.Toolkit.KryptonButton btnStop4;
        private Krypton.Toolkit.KryptonButton btnToggle4;
        private Krypton.Toolkit.KryptonLabel lblStatus4;
        private Krypton.Toolkit.KryptonGroupBox grpGlobalControls;
        private Krypton.Toolkit.KryptonButton btnStartAll;
        private Krypton.Toolkit.KryptonButton btnStopAll;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private System.Windows.Forms.Timer timer1;
    }
}
