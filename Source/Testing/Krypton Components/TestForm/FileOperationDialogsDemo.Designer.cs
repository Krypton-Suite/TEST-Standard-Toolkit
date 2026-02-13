namespace TestForm
{
    partial class FileOperationDialogsDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kwlblTitle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kgrpDemoData = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnCreateDemoData = new Krypton.Toolkit.KryptonButton();
            this.kgrpCopy = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpCopy = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblCopySource = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbCopySource = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowseCopySource = new Krypton.Toolkit.KryptonButton();
            this.kbtnBrowseCopySourceFile = new Krypton.Toolkit.KryptonButton();
            this.kwlblCopyDest = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbCopyDest = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowseCopyDest = new Krypton.Toolkit.KryptonButton();
            this.kchkCopyOverwritePrompt = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnCopyWithUI = new Krypton.Toolkit.KryptonButton();
            this.kbtnCopySilent = new Krypton.Toolkit.KryptonButton();
            this.kgrpCompress = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpCompress = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblCompressSource = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbCompressSource = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowseCompressSource = new Krypton.Toolkit.KryptonButton();
            this.kbtnBrowseCompressSourceFile = new Krypton.Toolkit.KryptonButton();
            this.kwlblCompressDest = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbCompressDest = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowseCompressDest = new Krypton.Toolkit.KryptonButton();
            this.kwlblCompressionLevel = new Krypton.Toolkit.KryptonWrapLabel();
            this.krbCompressOptimal = new Krypton.Toolkit.KryptonRadioButton();
            this.krbCompressFastest = new Krypton.Toolkit.KryptonRadioButton();
            this.krbCompressNone = new Krypton.Toolkit.KryptonRadioButton();
            this.kchkIncludeBaseDir = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnCompressWithUI = new Krypton.Toolkit.KryptonButton();
            this.kbtnCompressSilent = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpDemoData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpDemoData.Panel)).BeginInit();
            this.kgrpDemoData.Panel.SuspendLayout();
            this.kgrpDemoData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCopy.Panel)).BeginInit();
            this.kgrpCopy.Panel.SuspendLayout();
            this.kgrpCopy.SuspendLayout();
            this.tlpCopy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCompress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCompress.Panel)).BeginInit();
            this.kgrpCompress.Panel.SuspendLayout();
            this.kgrpCompress.SuspendLayout();
            this.tlpCompress.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.kwlblTitle);
            this.kryptonPanel1.Controls.Add(this.kgrpDemoData);
            this.kryptonPanel1.Controls.Add(this.kgrpCopy);
            this.kryptonPanel1.Controls.Add(this.kgrpCompress);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(624, 580);
            this.kryptonPanel1.TabIndex = 0;
            //
            // kwlblTitle
            //
            this.kwlblTitle.AutoSize = true;
            this.kwlblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblTitle.Location = new System.Drawing.Point(12, 12);
            this.kwlblTitle.Name = "kwlblTitle";
            this.kwlblTitle.Size = new System.Drawing.Size(380, 25);
            this.kwlblTitle.Text = "File Operation Dialogs Demo (Copy & Compression)";
            //
            // kgrpDemoData
            //
            this.kgrpDemoData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpDemoData.Location = new System.Drawing.Point(12, 45);
            this.kgrpDemoData.Name = "kgrpDemoData";
            this.kgrpDemoData.Panel.Controls.Add(this.kbtnCreateDemoData);
            this.kgrpDemoData.Size = new System.Drawing.Size(600, 55);
            this.kgrpDemoData.TabIndex = 1;
            this.kgrpDemoData.Values.Heading = "Demo data";
            //
            // kbtnCreateDemoData
            //
            this.kbtnCreateDemoData.Location = new System.Drawing.Point(10, 15);
            this.kbtnCreateDemoData.Name = "kbtnCreateDemoData";
            this.kbtnCreateDemoData.Size = new System.Drawing.Size(180, 28);
            this.kbtnCreateDemoData.TabIndex = 0;
            this.kbtnCreateDemoData.Values.Text = "Create demo data (temp folder + files)";
            this.kbtnCreateDemoData.Click += new System.EventHandler(this.BtnCreateDemoData_Click);
            //
            // kgrpCopy
            //
            this.kgrpCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpCopy.Location = new System.Drawing.Point(12, 108);
            this.kgrpCopy.Name = "kgrpCopy";
            this.kgrpCopy.Panel.Controls.Add(this.tlpCopy);
            this.kgrpCopy.Size = new System.Drawing.Size(600, 200);
            this.kgrpCopy.TabIndex = 2;
            this.kgrpCopy.Values.Heading = "KryptonFileCopyDialog";
            //
            // tlpCopy
            //
            this.tlpCopy.ColumnCount = 3;
            this.tlpCopy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpCopy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCopy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpCopy.Controls.Add(this.kwlblCopySource, 0, 0);
            this.tlpCopy.Controls.Add(this.ktbCopySource, 1, 0);
            this.tlpCopy.Controls.Add(this.kbtnBrowseCopySource, 2, 0);
            this.tlpCopy.Controls.Add(this.kbtnBrowseCopySourceFile, 2, 1);
            this.tlpCopy.Controls.Add(this.kwlblCopyDest, 0, 2);
            this.tlpCopy.Controls.Add(this.ktbCopyDest, 1, 2);
            this.tlpCopy.Controls.Add(this.kbtnBrowseCopyDest, 2, 2);
            this.tlpCopy.Controls.Add(this.kchkCopyOverwritePrompt, 1, 3);
            this.tlpCopy.Controls.Add(this.kbtnCopyWithUI, 1, 4);
            this.tlpCopy.Controls.Add(this.kbtnCopySilent, 2, 4);
            this.tlpCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCopy.Location = new System.Drawing.Point(0, 0);
            this.tlpCopy.Name = "tlpCopy";
            this.tlpCopy.Padding = new System.Windows.Forms.Padding(8);
            this.tlpCopy.RowCount = 5;
            this.tlpCopy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCopy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCopy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCopy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCopy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCopy.Size = new System.Drawing.Size(596, 176);
            this.tlpCopy.TabIndex = 0;
            //
            // kwlblCopySource
            //
            this.kwlblCopySource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCopySource.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCopySource.Location = new System.Drawing.Point(11, 18);
            this.kwlblCopySource.Name = "kwlblCopySource";
            this.kwlblCopySource.Size = new System.Drawing.Size(80, 18);
            this.kwlblCopySource.Text = "Source:";
            //
            // ktbCopySource
            //
            this.ktbCopySource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbCopySource.Location = new System.Drawing.Point(108, 14);
            this.ktbCopySource.Name = "ktbCopySource";
            this.ktbCopySource.Size = new System.Drawing.Size(308, 23);
            this.ktbCopySource.TabIndex = 0;
            //
            // kbtnBrowseCopySource
            //
            this.kbtnBrowseCopySource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCopySource.Location = new System.Drawing.Point(422, 12);
            this.kbtnBrowseCopySource.Name = "kbtnBrowseCopySource";
            this.kbtnBrowseCopySource.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCopySource.TabIndex = 1;
            this.kbtnBrowseCopySource.Values.Text = "Folder...";
            this.kbtnBrowseCopySource.Click += new System.EventHandler(this.BtnBrowseCopySource_Click);
            //
            // kbtnBrowseCopySourceFile
            //
            this.kbtnBrowseCopySourceFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCopySourceFile.Location = new System.Drawing.Point(513, 40);
            this.kbtnBrowseCopySourceFile.Name = "kbtnBrowseCopySourceFile";
            this.kbtnBrowseCopySourceFile.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCopySourceFile.TabIndex = 2;
            this.kbtnBrowseCopySourceFile.Values.Text = "File...";
            this.kbtnBrowseCopySourceFile.Click += new System.EventHandler(this.BtnBrowseCopySourceFile_Click);
            //
            // kwlblCopyDest
            //
            this.kwlblCopyDest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCopyDest.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCopyDest.Location = new System.Drawing.Point(11, 74);
            this.kwlblCopyDest.Name = "kwlblCopyDest";
            this.kwlblCopyDest.Size = new System.Drawing.Size(90, 18);
            this.kwlblCopyDest.Text = "Destination:";
            //
            // ktbCopyDest
            //
            this.ktbCopyDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbCopyDest.Location = new System.Drawing.Point(108, 70);
            this.ktbCopyDest.Name = "ktbCopyDest";
            this.ktbCopyDest.Size = new System.Drawing.Size(308, 23);
            this.ktbCopyDest.TabIndex = 3;
            //
            // kbtnBrowseCopyDest
            //
            this.kbtnBrowseCopyDest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCopyDest.Location = new System.Drawing.Point(422, 68);
            this.kbtnBrowseCopyDest.Name = "kbtnBrowseCopyDest";
            this.kbtnBrowseCopyDest.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCopyDest.TabIndex = 4;
            this.kbtnBrowseCopyDest.Values.Text = "Browse...";
            this.kbtnBrowseCopyDest.Click += new System.EventHandler(this.BtnBrowseCopyDest_Click);
            //
            // kchkCopyOverwritePrompt
            //
            this.kchkCopyOverwritePrompt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkCopyOverwritePrompt.Checked = true;
            this.kchkCopyOverwritePrompt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkCopyOverwritePrompt.Location = new System.Drawing.Point(108, 104);
            this.kchkCopyOverwritePrompt.Name = "kchkCopyOverwritePrompt";
            this.kchkCopyOverwritePrompt.Size = new System.Drawing.Size(200, 22);
            this.kchkCopyOverwritePrompt.TabIndex = 5;
            this.kchkCopyOverwritePrompt.Values.Text = "Overwrite prompt";
            //
            // kbtnCopyWithUI
            //
            this.kbtnCopyWithUI.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnCopyWithUI.Location = new System.Drawing.Point(108, 142);
            this.kbtnCopyWithUI.Name = "kbtnCopyWithUI";
            this.kbtnCopyWithUI.Size = new System.Drawing.Size(140, 28);
            this.kbtnCopyWithUI.TabIndex = 6;
            this.kbtnCopyWithUI.Values.Text = "Copy with progress UI";
            this.kbtnCopyWithUI.Click += new System.EventHandler(this.BtnCopyWithUI_Click);
            //
            // kbtnCopySilent
            //
            this.kbtnCopySilent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnCopySilent.Location = new System.Drawing.Point(422, 142);
            this.kbtnCopySilent.Name = "kbtnCopySilent";
            this.kbtnCopySilent.Size = new System.Drawing.Size(120, 28);
            this.kbtnCopySilent.TabIndex = 7;
            this.kbtnCopySilent.Values.Text = "Copy (silent)";
            this.kbtnCopySilent.Click += new System.EventHandler(this.BtnCopySilent_Click);
            //
            // kgrpCompress
            //
            this.kgrpCompress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.kgrpCompress.Location = new System.Drawing.Point(12, 314);
            this.kgrpCompress.Name = "kgrpCompress";
            this.kgrpCompress.Panel.Controls.Add(this.tlpCompress);
            this.kgrpCompress.Size = new System.Drawing.Size(600, 220);
            this.kgrpCompress.TabIndex = 3;
            this.kgrpCompress.Values.Heading = "KryptonFileCompressionDialog";
            //
            // tlpCompress
            //
            this.tlpCompress.ColumnCount = 3;
            this.tlpCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpCompress.Controls.Add(this.kwlblCompressSource, 0, 0);
            this.tlpCompress.Controls.Add(this.ktbCompressSource, 1, 0);
            this.tlpCompress.Controls.Add(this.kbtnBrowseCompressSource, 2, 0);
            this.tlpCompress.Controls.Add(this.kbtnBrowseCompressSourceFile, 2, 1);
            this.tlpCompress.Controls.Add(this.kwlblCompressDest, 0, 2);
            this.tlpCompress.Controls.Add(this.ktbCompressDest, 1, 2);
            this.tlpCompress.Controls.Add(this.kbtnBrowseCompressDest, 2, 2);
            this.tlpCompress.Controls.Add(this.kwlblCompressionLevel, 0, 3);
            this.tlpCompress.Controls.Add(this.krbCompressOptimal, 1, 3);
            this.tlpCompress.Controls.Add(this.krbCompressFastest, 2, 3);
            this.tlpCompress.Controls.Add(this.krbCompressNone, 1, 4);
            this.tlpCompress.Controls.Add(this.kchkIncludeBaseDir, 2, 4);
            this.tlpCompress.Controls.Add(this.kbtnCompressWithUI, 1, 5);
            this.tlpCompress.Controls.Add(this.kbtnCompressSilent, 2, 5);
            this.tlpCompress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCompress.Location = new System.Drawing.Point(0, 0);
            this.tlpCompress.Name = "tlpCompress";
            this.tlpCompress.Padding = new System.Windows.Forms.Padding(8);
            this.tlpCompress.RowCount = 6;
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCompress.Size = new System.Drawing.Size(596, 196);
            this.tlpCompress.TabIndex = 0;
            //
            // kwlblCompressSource
            //
            this.kwlblCompressSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCompressSource.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCompressSource.Location = new System.Drawing.Point(11, 18);
            this.kwlblCompressSource.Name = "kwlblCompressSource";
            this.kwlblCompressSource.Size = new System.Drawing.Size(80, 18);
            this.kwlblCompressSource.Text = "Source:";
            //
            // ktbCompressSource
            //
            this.ktbCompressSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbCompressSource.Location = new System.Drawing.Point(108, 14);
            this.ktbCompressSource.Name = "ktbCompressSource";
            this.ktbCompressSource.Size = new System.Drawing.Size(308, 23);
            this.ktbCompressSource.TabIndex = 0;
            //
            // kbtnBrowseCompressSource
            //
            this.kbtnBrowseCompressSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCompressSource.Location = new System.Drawing.Point(422, 12);
            this.kbtnBrowseCompressSource.Name = "kbtnBrowseCompressSource";
            this.kbtnBrowseCompressSource.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCompressSource.TabIndex = 1;
            this.kbtnBrowseCompressSource.Values.Text = "Folder...";
            this.kbtnBrowseCompressSource.Click += new System.EventHandler(this.BtnBrowseCompressSource_Click);
            //
            // kbtnBrowseCompressSourceFile
            //
            this.kbtnBrowseCompressSourceFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCompressSourceFile.Location = new System.Drawing.Point(513, 40);
            this.kbtnBrowseCompressSourceFile.Name = "kbtnBrowseCompressSourceFile";
            this.kbtnBrowseCompressSourceFile.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCompressSourceFile.TabIndex = 2;
            this.kbtnBrowseCompressSourceFile.Values.Text = "File...";
            this.kbtnBrowseCompressSourceFile.Click += new System.EventHandler(this.BtnBrowseCompressSourceFile_Click);
            //
            // kwlblCompressDest
            //
            this.kwlblCompressDest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCompressDest.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCompressDest.Location = new System.Drawing.Point(11, 74);
            this.kwlblCompressDest.Name = "kwlblCompressDest";
            this.kwlblCompressDest.Size = new System.Drawing.Size(90, 18);
            this.kwlblCompressDest.Text = "ZIP path:";
            //
            // ktbCompressDest
            //
            this.ktbCompressDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbCompressDest.Location = new System.Drawing.Point(108, 70);
            this.ktbCompressDest.Name = "ktbCompressDest";
            this.ktbCompressDest.Size = new System.Drawing.Size(308, 23);
            this.ktbCompressDest.TabIndex = 3;
            //
            // kbtnBrowseCompressDest
            //
            this.kbtnBrowseCompressDest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnBrowseCompressDest.Location = new System.Drawing.Point(422, 68);
            this.kbtnBrowseCompressDest.Name = "kbtnBrowseCompressDest";
            this.kbtnBrowseCompressDest.Size = new System.Drawing.Size(85, 25);
            this.kbtnBrowseCompressDest.TabIndex = 4;
            this.kbtnBrowseCompressDest.Values.Text = "Browse...";
            this.kbtnBrowseCompressDest.Click += new System.EventHandler(this.BtnBrowseCompressDest_Click);
            //
            // kwlblCompressionLevel
            //
            this.kwlblCompressionLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCompressionLevel.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCompressionLevel.Location = new System.Drawing.Point(11, 106);
            this.kwlblCompressionLevel.Name = "kwlblCompressionLevel";
            this.kwlblCompressionLevel.Size = new System.Drawing.Size(90, 18);
            this.kwlblCompressionLevel.Text = "Level:";
            //
            // krbCompressOptimal
            //
            this.krbCompressOptimal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.krbCompressOptimal.Checked = true;
            this.krbCompressOptimal.Location = new System.Drawing.Point(108, 103);
            this.krbCompressOptimal.Name = "krbCompressOptimal";
            this.krbCompressOptimal.Size = new System.Drawing.Size(120, 22);
            this.krbCompressOptimal.TabIndex = 5;
            this.krbCompressOptimal.Values.Text = "Optimal";
            //
            // krbCompressFastest
            //
            this.krbCompressFastest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.krbCompressFastest.Location = new System.Drawing.Point(422, 103);
            this.krbCompressFastest.Name = "krbCompressFastest";
            this.krbCompressFastest.Size = new System.Drawing.Size(100, 22);
            this.krbCompressFastest.TabIndex = 6;
            this.krbCompressFastest.Values.Text = "Fastest";
            //
            // krbCompressNone
            //
            this.krbCompressNone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.krbCompressNone.Location = new System.Drawing.Point(108, 131);
            this.krbCompressNone.Name = "krbCompressNone";
            this.krbCompressNone.Size = new System.Drawing.Size(120, 22);
            this.krbCompressNone.TabIndex = 7;
            this.krbCompressNone.Values.Text = "No compression";
            //
            // kchkIncludeBaseDir
            //
            this.kchkIncludeBaseDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkIncludeBaseDir.Location = new System.Drawing.Point(422, 131);
            this.kchkIncludeBaseDir.Name = "kchkIncludeBaseDir";
            this.kchkIncludeBaseDir.Size = new System.Drawing.Size(150, 22);
            this.kchkIncludeBaseDir.TabIndex = 8;
            this.kchkIncludeBaseDir.Values.Text = "Include base directory";
            //
            // kbtnCompressWithUI
            //
            this.kbtnCompressWithUI.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnCompressWithUI.Location = new System.Drawing.Point(108, 162);
            this.kbtnCompressWithUI.Name = "kbtnCompressWithUI";
            this.kbtnCompressWithUI.Size = new System.Drawing.Size(160, 28);
            this.kbtnCompressWithUI.TabIndex = 9;
            this.kbtnCompressWithUI.Values.Text = "Compress with progress UI";
            this.kbtnCompressWithUI.Click += new System.EventHandler(this.BtnCompressWithUI_Click);
            //
            // kbtnCompressSilent
            //
            this.kbtnCompressSilent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnCompressSilent.Location = new System.Drawing.Point(422, 162);
            this.kbtnCompressSilent.Name = "kbtnCompressSilent";
            this.kbtnCompressSilent.Size = new System.Drawing.Size(130, 28);
            this.kbtnCompressSilent.TabIndex = 10;
            this.kbtnCompressSilent.Values.Text = "Compress (silent)";
            this.kbtnCompressSilent.Click += new System.EventHandler(this.BtnCompressSilent_Click);
            //
            // kbtnClose
            //
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(537, 545);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(75, 25);
            this.kbtnClose.TabIndex = 4;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            //
            // FileOperationDialogsDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 580);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileOperationDialogsDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Operation Dialogs Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpDemoData.Panel)).EndInit();
            this.kgrpDemoData.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpDemoData)).EndInit();
            this.kgrpDemoData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCopy.Panel)).EndInit();
            this.kgrpCopy.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCopy)).EndInit();
            this.kgrpCopy.ResumeLayout(false);
            this.tlpCopy.ResumeLayout(false);
            this.tlpCopy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCompress.Panel)).EndInit();
            this.kgrpCompress.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpCompress)).EndInit();
            this.kgrpCompress.ResumeLayout(false);
            this.tlpCompress.ResumeLayout(false);
            this.tlpCompress.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblTitle;
        private Krypton.Toolkit.KryptonGroupBox kgrpDemoData;
        private Krypton.Toolkit.KryptonButton kbtnCreateDemoData;
        private Krypton.Toolkit.KryptonGroupBox kgrpCopy;
        private System.Windows.Forms.TableLayoutPanel tlpCopy;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCopySource;
        private Krypton.Toolkit.KryptonTextBox ktbCopySource;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCopySource;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCopySourceFile;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCopyDest;
        private Krypton.Toolkit.KryptonTextBox ktbCopyDest;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCopyDest;
        private Krypton.Toolkit.KryptonCheckBox kchkCopyOverwritePrompt;
        private Krypton.Toolkit.KryptonButton kbtnCopyWithUI;
        private Krypton.Toolkit.KryptonButton kbtnCopySilent;
        private Krypton.Toolkit.KryptonGroupBox kgrpCompress;
        private System.Windows.Forms.TableLayoutPanel tlpCompress;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCompressSource;
        private Krypton.Toolkit.KryptonTextBox ktbCompressSource;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCompressSource;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCompressSourceFile;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCompressDest;
        private Krypton.Toolkit.KryptonTextBox ktbCompressDest;
        private Krypton.Toolkit.KryptonButton kbtnBrowseCompressDest;
        private Krypton.Toolkit.KryptonWrapLabel kwlblCompressionLevel;
        private Krypton.Toolkit.KryptonRadioButton krbCompressOptimal;
        private Krypton.Toolkit.KryptonRadioButton krbCompressFastest;
        private Krypton.Toolkit.KryptonRadioButton krbCompressNone;
        private Krypton.Toolkit.KryptonCheckBox kchkIncludeBaseDir;
        private Krypton.Toolkit.KryptonButton kbtnCompressWithUI;
        private Krypton.Toolkit.KryptonButton kbtnCompressSilent;
        private Krypton.Toolkit.KryptonButton kbtnClose;
    }
}
