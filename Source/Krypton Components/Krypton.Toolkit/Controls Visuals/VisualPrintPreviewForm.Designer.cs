namespace Krypton.Toolkit
{
    partial class VisualPrintPreviewForm
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._toolbarPanel = new Krypton.Toolkit.KryptonPanel();
            this._previewControl = new Krypton.Toolkit.KryptonPrintPreviewControl();
            this._btnPrint = new Krypton.Toolkit.KryptonButton();
            this._btnZoomIn = new Krypton.Toolkit.KryptonButton();
            this._btnZoomOut = new Krypton.Toolkit.KryptonButton();
            this._zoomTrackBar = new Krypton.Toolkit.KryptonTrackBar();
            this._btnOnePage = new Krypton.Toolkit.KryptonButton();
            this._btnTwoPages = new Krypton.Toolkit.KryptonButton();
            this._btnThreePages = new Krypton.Toolkit.KryptonButton();
            this._btnFourPages = new Krypton.Toolkit.KryptonButton();
            this._btnSixPages = new Krypton.Toolkit.KryptonButton();
            this._btnClose = new Krypton.Toolkit.KryptonButton();
            this._lblPageInfo = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this._toolbarPanel)).BeginInit();
            this._toolbarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolbarPanel
            // 
            this._toolbarPanel.Controls.Add(this._btnClose);
            this._toolbarPanel.Controls.Add(this._lblPageInfo);
            this._toolbarPanel.Controls.Add(this._btnSixPages);
            this._toolbarPanel.Controls.Add(this._btnFourPages);
            this._toolbarPanel.Controls.Add(this._btnThreePages);
            this._toolbarPanel.Controls.Add(this._btnTwoPages);
            this._toolbarPanel.Controls.Add(this._btnOnePage);
            this._toolbarPanel.Controls.Add(this._zoomTrackBar);
            this._toolbarPanel.Controls.Add(this._btnZoomOut);
            this._toolbarPanel.Controls.Add(this._btnZoomIn);
            this._toolbarPanel.Controls.Add(this._btnPrint);
            this._toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this._toolbarPanel.Name = "_toolbarPanel";
            this._toolbarPanel.Size = new System.Drawing.Size(800, 40);
            this._toolbarPanel.TabIndex = 0;
            // 
            // _previewControl
            // 
            this._previewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewControl.Location = new System.Drawing.Point(0, 40);
            this._previewControl.Name = "_previewControl";
            this._previewControl.Size = new System.Drawing.Size(800, 560);
            this._previewControl.TabIndex = 1;
            this._previewControl.UseAntiAlias = true;
            // 
            // _btnPrint
            // 
            this._btnPrint.Location = new System.Drawing.Point(8, 8);
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new System.Drawing.Size(75, 25);
            this._btnPrint.TabIndex = 0;
            this._btnPrint.Values.Text = "Print...";
            this._btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.Location = new System.Drawing.Point(89, 8);
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(75, 25);
            this._btnZoomIn.TabIndex = 1;
            this._btnZoomIn.Values.Text = "Zoom In";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.Location = new System.Drawing.Point(170, 8);
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(75, 25);
            this._btnZoomOut.TabIndex = 2;
            this._btnZoomOut.Values.Text = "Zoom Out";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _zoomTrackBar
            // 
            this._zoomTrackBar.Location = new System.Drawing.Point(251, 8);
            this._zoomTrackBar.Name = "_zoomTrackBar";
            this._zoomTrackBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this._zoomTrackBar.Size = new System.Drawing.Size(75, 25);
            this._zoomTrackBar.TabIndex = 10;
            this._zoomTrackBar.Minimum = 25;
            this._zoomTrackBar.Maximum = 500;
            this._zoomTrackBar.Value = 100;
            this._zoomTrackBar.ValueChanged += new System.EventHandler(this.ZoomTrackBar_ValueChanged);
            // 
            // _btnOnePage
            // 
            this._btnOnePage.Location = new System.Drawing.Point(332, 8);
            this._btnOnePage.Name = "_btnOnePage";
            this._btnOnePage.Size = new System.Drawing.Size(75, 25);
            this._btnOnePage.TabIndex = 3;
            this._btnOnePage.Values.Text = "One Page";
            this._btnOnePage.Click += new System.EventHandler(this.BtnOnePage_Click);
            // 
            // _btnTwoPages
            // 
            this._btnTwoPages.Location = new System.Drawing.Point(413, 8);
            this._btnTwoPages.Name = "_btnTwoPages";
            this._btnTwoPages.Size = new System.Drawing.Size(75, 25);
            this._btnTwoPages.TabIndex = 4;
            this._btnTwoPages.Values.Text = "Two Pages";
            this._btnTwoPages.Click += new System.EventHandler(this.BtnTwoPages_Click);
            // 
            // _btnThreePages
            // 
            this._btnThreePages.Location = new System.Drawing.Point(494, 8);
            this._btnThreePages.Name = "_btnThreePages";
            this._btnThreePages.Size = new System.Drawing.Size(75, 25);
            this._btnThreePages.TabIndex = 5;
            this._btnThreePages.Values.Text = "Three Pages";
            this._btnThreePages.Click += new System.EventHandler(this.BtnThreePages_Click);
            // 
            // _btnFourPages
            // 
            this._btnFourPages.Location = new System.Drawing.Point(575, 8);
            this._btnFourPages.Name = "_btnFourPages";
            this._btnFourPages.Size = new System.Drawing.Size(75, 25);
            this._btnFourPages.TabIndex = 6;
            this._btnFourPages.Values.Text = "Four Pages";
            this._btnFourPages.Click += new System.EventHandler(this.BtnFourPages_Click);
            // 
            // _btnSixPages
            // 
            this._btnSixPages.Location = new System.Drawing.Point(656, 8);
            this._btnSixPages.Name = "_btnSixPages";
            this._btnSixPages.Size = new System.Drawing.Size(75, 25);
            this._btnSixPages.TabIndex = 7;
            this._btnSixPages.Values.Text = "Six Pages";
            this._btnSixPages.Click += new System.EventHandler(this.BtnSixPages_Click);
            // 
            // _lblPageInfo
            // 
            this._lblPageInfo.Location = new System.Drawing.Point(737, 11);
            this._lblPageInfo.Name = "_lblPageInfo";
            this._lblPageInfo.Size = new System.Drawing.Size(100, 20);
            this._lblPageInfo.TabIndex = 8;
            this._lblPageInfo.Text = "Page 1 of 1";
            this._lblPageInfo.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.Location = new System.Drawing.Point(798, 8);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(75, 25);
            this._btnClose.TabIndex = 9;
            this._btnClose.Values.Text = "Close";
            this._btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // VisualPrintPreviewForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this._previewControl);
            this.Controls.Add(this._toolbarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "VisualPrintPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            ((System.ComponentModel.ISupportInitialize)(this._toolbarPanel)).EndInit();
            this._toolbarPanel.ResumeLayout(false);
            this._toolbarPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel _toolbarPanel;
        private KryptonPrintPreviewControl _previewControl;
        private KryptonButton _btnPrint;
        private KryptonButton _btnZoomIn;
        private KryptonButton _btnZoomOut;
        private KryptonTrackBar _zoomTrackBar;
        private KryptonButton _btnOnePage;
        private KryptonButton _btnTwoPages;
        private KryptonButton _btnThreePages;
        private KryptonButton _btnFourPages;
        private KryptonButton _btnSixPages;
        private KryptonButton _btnClose;
        private KryptonLabel _lblPageInfo;
    }
}
