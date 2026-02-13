namespace TestForm
{
    partial class DetachableRibbonTest
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
            this.btnLoadPlugin = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnLoadPlugin);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1200, 600);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnLoadPlugin
            // 
            this.btnLoadPlugin.Location = new System.Drawing.Point(12, 12);
            this.btnLoadPlugin.Name = "btnLoadPlugin";
            this.btnLoadPlugin.Size = new System.Drawing.Size(120, 25);
            this.btnLoadPlugin.TabIndex = 0;
            this.btnLoadPlugin.Values.Text = "Load Plugin";
            this.btnLoadPlugin.Click += new System.EventHandler(this.btnLoadPlugin_Click);
            // 
            // DetachableRibbonTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DetachableRibbonTest";
            this.Text = "Detachable Ribbon Test (Issue #595)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnLoadPlugin;
    }
}
