namespace TestForm
{
    partial class AdditionalControlsForm
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
            this.kryptonToolStripContainer1 = new Krypton.Toolkit.KryptonToolStripContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.kryptonToolStripContainer1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonToolStripContainer1
            // 
            // 
            // kryptonToolStripContainer1.ContentPanel
            // 
            this.kryptonToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.kryptonToolStripContainer1.Location = new System.Drawing.Point(27, 66);
            this.kryptonToolStripContainer1.Name = "kryptonToolStripContainer1";
            this.kryptonToolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.kryptonToolStripContainer1.TabIndex = 9;
            this.kryptonToolStripContainer1.Text = "kryptonToolStripContainer1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(203, 66);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // AdditionalControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.kryptonToolStripContainer1);
            this.FormValues.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.Name = "AdditionalControlsForm";
            this.Text = "AdditionalControlsForm";
            this.kryptonToolStripContainer1.ResumeLayout(false);
            this.kryptonToolStripContainer1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonToolStripContainer kryptonToolStripContainer1;
        private ToolStripContainer toolStripContainer1;
    }
}