namespace TestForm
{
    partial class GitHubIssueReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new Krypton.Toolkit.KryptonPanel();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblSummary = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbSummary = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblDescription = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbDescription = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblStepsToReproduce = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbStepsToReproduce = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblExpectedBehavior = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbExpectedBehavior = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblActualBehavior = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbActualBehavior = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblOs = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbOs = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblOsVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbOsVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblFrameworkVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbFrameworkVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblToolkitVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbToolkitVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblAdditionalInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbAdditionalInfo = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblAreasAffected = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbAreasAffected = new Krypton.Toolkit.KryptonComboBox();
            this.kgrpGitHub = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpGitHub = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblOwner = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbOwner = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblRepo = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbRepo = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblToken = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbToken = new Krypton.Toolkit.KryptonTextBox();
            this.pnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnCreate = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpGitHub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpGitHub.Panel)).BeginInit();
            this.kgrpGitHub.Panel.SuspendLayout();
            this.kgrpGitHub.SuspendLayout();
            this.tlpGitHub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMain
            //
            this.pnlMain.Controls.Add(this.pnlScroll);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(584, 561);
            this.pnlMain.TabIndex = 0;
            //
            // pnlScroll
            //
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Controls.Add(this.tlpContent);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(12);
            this.pnlScroll.Size = new System.Drawing.Size(584, 506);
            this.pnlScroll.TabIndex = 0;
            //
            // tlpContent
            //
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.kwlblSummary, 0, 0);
            this.tlpContent.Controls.Add(this.ktbSummary, 1, 0);
            this.tlpContent.Controls.Add(this.kwlblDescription, 0, 1);
            this.tlpContent.Controls.Add(this.krtbDescription, 1, 1);
            this.tlpContent.Controls.Add(this.kwlblStepsToReproduce, 0, 2);
            this.tlpContent.Controls.Add(this.krtbStepsToReproduce, 1, 2);
            this.tlpContent.Controls.Add(this.kwlblExpectedBehavior, 0, 3);
            this.tlpContent.Controls.Add(this.krtbExpectedBehavior, 1, 3);
            this.tlpContent.Controls.Add(this.kwlblActualBehavior, 0, 4);
            this.tlpContent.Controls.Add(this.krtbActualBehavior, 1, 4);
            this.tlpContent.Controls.Add(this.kwlblOs, 0, 5);
            this.tlpContent.Controls.Add(this.ktbOs, 1, 5);
            this.tlpContent.Controls.Add(this.kwlblOsVersion, 0, 6);
            this.tlpContent.Controls.Add(this.ktbOsVersion, 1, 6);
            this.tlpContent.Controls.Add(this.kwlblFrameworkVersion, 0, 7);
            this.tlpContent.Controls.Add(this.ktbFrameworkVersion, 1, 7);
            this.tlpContent.Controls.Add(this.kwlblToolkitVersion, 0, 8);
            this.tlpContent.Controls.Add(this.ktbToolkitVersion, 1, 8);
            this.tlpContent.Controls.Add(this.kwlblAdditionalInfo, 0, 9);
            this.tlpContent.Controls.Add(this.krtbAdditionalInfo, 1, 9);
            this.tlpContent.Controls.Add(this.kwlblAreasAffected, 0, 10);
            this.tlpContent.Controls.Add(this.kcmbAreasAffected, 1, 10);
            this.tlpContent.Controls.Add(this.kgrpGitHub, 0, 11);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpContent.Location = new System.Drawing.Point(12, 12);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 12;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpContent.Size = new System.Drawing.Size(550, 656);
            this.tlpContent.TabIndex = 0;
            //
            // kwlblSummary
            //
            this.kwlblSummary.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblSummary.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblSummary.Location = new System.Drawing.Point(0, 4);
            this.kwlblSummary.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblSummary.Name = "kwlblSummary";
            this.kwlblSummary.Size = new System.Drawing.Size(132, 20);
            this.kwlblSummary.Text = "Summary:";
            this.kwlblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbSummary
            //
            this.ktbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbSummary.Location = new System.Drawing.Point(140, 2);
            this.ktbSummary.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbSummary.Name = "ktbSummary";
            this.ktbSummary.Size = new System.Drawing.Size(410, 23);
            this.ktbSummary.TabIndex = 0;
            //
            // kwlblDescription
            //
            this.kwlblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kwlblDescription.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblDescription.Location = new System.Drawing.Point(0, 18);
            this.kwlblDescription.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblDescription.Name = "kwlblDescription";
            this.kwlblDescription.Size = new System.Drawing.Size(132, 40);
            this.kwlblDescription.Text = "Description:";
            this.kwlblDescription.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // krtbDescription
            //
            this.krtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbDescription.Location = new System.Drawing.Point(140, 30);
            this.krtbDescription.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbDescription.Name = "krtbDescription";
            this.krtbDescription.Size = new System.Drawing.Size(410, 52);
            this.krtbDescription.TabIndex = 1;
            //
            // kwlblStepsToReproduce
            //
            this.kwlblStepsToReproduce.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kwlblStepsToReproduce.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblStepsToReproduce.Location = new System.Drawing.Point(0, 94);
            this.kwlblStepsToReproduce.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblStepsToReproduce.Name = "kwlblStepsToReproduce";
            this.kwlblStepsToReproduce.Size = new System.Drawing.Size(132, 40);
            this.kwlblStepsToReproduce.Text = "Steps to Reproduce:";
            this.kwlblStepsToReproduce.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // krtbStepsToReproduce
            //
            this.krtbStepsToReproduce.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbStepsToReproduce.Location = new System.Drawing.Point(140, 86);
            this.krtbStepsToReproduce.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbStepsToReproduce.Name = "krtbStepsToReproduce";
            this.krtbStepsToReproduce.Size = new System.Drawing.Size(410, 52);
            this.krtbStepsToReproduce.TabIndex = 2;
            //
            // kwlblExpectedBehavior
            //
            this.kwlblExpectedBehavior.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kwlblExpectedBehavior.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblExpectedBehavior.Location = new System.Drawing.Point(0, 170);
            this.kwlblExpectedBehavior.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblExpectedBehavior.Name = "kwlblExpectedBehavior";
            this.kwlblExpectedBehavior.Size = new System.Drawing.Size(132, 40);
            this.kwlblExpectedBehavior.Text = "Expected Behavior:";
            this.kwlblExpectedBehavior.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // krtbExpectedBehavior
            //
            this.krtbExpectedBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbExpectedBehavior.Location = new System.Drawing.Point(140, 142);
            this.krtbExpectedBehavior.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbExpectedBehavior.Name = "krtbExpectedBehavior";
            this.krtbExpectedBehavior.Size = new System.Drawing.Size(410, 52);
            this.krtbExpectedBehavior.TabIndex = 3;
            //
            // kwlblActualBehavior
            //
            this.kwlblActualBehavior.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kwlblActualBehavior.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblActualBehavior.Location = new System.Drawing.Point(0, 246);
            this.kwlblActualBehavior.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblActualBehavior.Name = "kwlblActualBehavior";
            this.kwlblActualBehavior.Size = new System.Drawing.Size(132, 40);
            this.kwlblActualBehavior.Text = "Actual Behavior:";
            this.kwlblActualBehavior.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // krtbActualBehavior
            //
            this.krtbActualBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbActualBehavior.Location = new System.Drawing.Point(140, 198);
            this.krtbActualBehavior.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbActualBehavior.Name = "krtbActualBehavior";
            this.krtbActualBehavior.Size = new System.Drawing.Size(410, 52);
            this.krtbActualBehavior.TabIndex = 4;
            //
            // kwlblOs
            //
            this.kwlblOs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblOs.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblOs.Location = new System.Drawing.Point(0, 258);
            this.kwlblOs.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblOs.Name = "kwlblOs";
            this.kwlblOs.Size = new System.Drawing.Size(132, 20);
            this.kwlblOs.Text = "Operating System:";
            this.kwlblOs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbOs
            //
            this.ktbOs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbOs.Location = new System.Drawing.Point(140, 256);
            this.ktbOs.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbOs.Name = "ktbOs";
            this.ktbOs.Size = new System.Drawing.Size(410, 23);
            this.ktbOs.TabIndex = 5;
            //
            // kwlblOsVersion
            //
            this.kwlblOsVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblOsVersion.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblOsVersion.Location = new System.Drawing.Point(0, 286);
            this.kwlblOsVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblOsVersion.Name = "kwlblOsVersion";
            this.kwlblOsVersion.Size = new System.Drawing.Size(132, 20);
            this.kwlblOsVersion.Text = "OS Version:";
            this.kwlblOsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbOsVersion
            //
            this.ktbOsVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbOsVersion.Location = new System.Drawing.Point(140, 284);
            this.ktbOsVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbOsVersion.Name = "ktbOsVersion";
            this.ktbOsVersion.Size = new System.Drawing.Size(410, 23);
            this.ktbOsVersion.TabIndex = 6;
            //
            // kwlblFrameworkVersion
            //
            this.kwlblFrameworkVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblFrameworkVersion.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblFrameworkVersion.Location = new System.Drawing.Point(0, 314);
            this.kwlblFrameworkVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblFrameworkVersion.Name = "kwlblFrameworkVersion";
            this.kwlblFrameworkVersion.Size = new System.Drawing.Size(132, 20);
            this.kwlblFrameworkVersion.Text = "Framework/.NET:";
            this.kwlblFrameworkVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbFrameworkVersion
            //
            this.ktbFrameworkVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbFrameworkVersion.Location = new System.Drawing.Point(140, 312);
            this.ktbFrameworkVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbFrameworkVersion.Name = "ktbFrameworkVersion";
            this.ktbFrameworkVersion.Size = new System.Drawing.Size(410, 23);
            this.ktbFrameworkVersion.TabIndex = 7;
            //
            // kwlblToolkitVersion
            //
            this.kwlblToolkitVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblToolkitVersion.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblToolkitVersion.Location = new System.Drawing.Point(0, 342);
            this.kwlblToolkitVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblToolkitVersion.Name = "kwlblToolkitVersion";
            this.kwlblToolkitVersion.Size = new System.Drawing.Size(132, 20);
            this.kwlblToolkitVersion.Text = "Toolkit Version:";
            this.kwlblToolkitVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbToolkitVersion
            //
            this.ktbToolkitVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbToolkitVersion.Location = new System.Drawing.Point(140, 340);
            this.ktbToolkitVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbToolkitVersion.Name = "ktbToolkitVersion";
            this.ktbToolkitVersion.Size = new System.Drawing.Size(410, 23);
            this.ktbToolkitVersion.TabIndex = 8;
            //
            // kwlblAdditionalInfo
            //
            this.kwlblAdditionalInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kwlblAdditionalInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblAdditionalInfo.Location = new System.Drawing.Point(0, 370);
            this.kwlblAdditionalInfo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblAdditionalInfo.Name = "kwlblAdditionalInfo";
            this.kwlblAdditionalInfo.Size = new System.Drawing.Size(132, 40);
            this.kwlblAdditionalInfo.Text = "Additional Information:";
            this.kwlblAdditionalInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // krtbAdditionalInfo
            //
            this.krtbAdditionalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbAdditionalInfo.Location = new System.Drawing.Point(140, 366);
            this.krtbAdditionalInfo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbAdditionalInfo.Name = "krtbAdditionalInfo";
            this.krtbAdditionalInfo.Size = new System.Drawing.Size(410, 52);
            this.krtbAdditionalInfo.TabIndex = 9;
            //
            // kwlblAreasAffected
            //
            this.kwlblAreasAffected.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblAreasAffected.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblAreasAffected.Location = new System.Drawing.Point(0, 426);
            this.kwlblAreasAffected.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblAreasAffected.Name = "kwlblAreasAffected";
            this.kwlblAreasAffected.Size = new System.Drawing.Size(132, 20);
            this.kwlblAreasAffected.Text = "Areas Affected:";
            this.kwlblAreasAffected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // kcmbAreasAffected
            //
            this.kcmbAreasAffected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.kcmbAreasAffected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAreasAffected.DropDownWidth = 250;
            this.kcmbAreasAffected.Items.AddRange(new object[] { "Docking", "Navigator", "Ribbon", "Toolkit", "Workspace" });
            this.kcmbAreasAffected.Location = new System.Drawing.Point(140, 424);
            this.kcmbAreasAffected.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.kcmbAreasAffected.Name = "kcmbAreasAffected";
            this.kcmbAreasAffected.Size = new System.Drawing.Size(410, 23);
            this.kcmbAreasAffected.TabIndex = 10;
            //
            // kgrpGitHub
            //
            this.tlpContent.Controls.Add(this.kgrpGitHub, 0, 11);
            this.tlpContent.SetColumnSpan(this.kgrpGitHub, 2);
            this.kgrpGitHub.Location = new System.Drawing.Point(0, 454);
            this.kgrpGitHub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.kgrpGitHub.Name = "kgrpGitHub";
            //
            // kgrpGitHub.Panel
            //
            this.kgrpGitHub.Panel.Controls.Add(this.tlpGitHub);
            this.kgrpGitHub.Size = new System.Drawing.Size(550, 202);
            this.kgrpGitHub.TabIndex = 11;
            this.kgrpGitHub.Values.Heading = "GitHub Configuration";
            //
            // tlpGitHub
            //
            this.tlpGitHub.ColumnCount = 2;
            this.tlpGitHub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpGitHub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGitHub.Controls.Add(this.kwlblOwner, 0, 0);
            this.tlpGitHub.Controls.Add(this.ktbOwner, 1, 0);
            this.tlpGitHub.Controls.Add(this.kwlblRepo, 0, 1);
            this.tlpGitHub.Controls.Add(this.ktbRepo, 1, 1);
            this.tlpGitHub.Controls.Add(this.kwlblToken, 0, 2);
            this.tlpGitHub.Controls.Add(this.ktbToken, 1, 2);
            this.tlpGitHub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGitHub.Location = new System.Drawing.Point(0, 0);
            this.tlpGitHub.Name = "tlpGitHub";
            this.tlpGitHub.Padding = new System.Windows.Forms.Padding(8);
            this.tlpGitHub.RowCount = 3;
            this.tlpGitHub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpGitHub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpGitHub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpGitHub.Size = new System.Drawing.Size(546, 198);
            this.tlpGitHub.TabIndex = 0;
            //
            // kwlblOwner
            //
            this.kwlblOwner.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblOwner.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblOwner.Location = new System.Drawing.Point(8, 4);
            this.kwlblOwner.Name = "kwlblOwner";
            this.kwlblOwner.Size = new System.Drawing.Size(84, 20);
            this.kwlblOwner.Text = "Owner:";
            this.kwlblOwner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbOwner
            //
            this.ktbOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbOwner.Location = new System.Drawing.Point(108, 2);
            this.ktbOwner.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbOwner.Name = "ktbOwner";
            this.ktbOwner.Size = new System.Drawing.Size(438, 23);
            this.ktbOwner.TabIndex = 0;
            //
            // kwlblRepo
            //
            this.kwlblRepo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblRepo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblRepo.Location = new System.Drawing.Point(8, 32);
            this.kwlblRepo.Name = "kwlblRepo";
            this.kwlblRepo.Size = new System.Drawing.Size(84, 20);
            this.kwlblRepo.Text = "Repository:";
            this.kwlblRepo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbRepo
            //
            this.ktbRepo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbRepo.Location = new System.Drawing.Point(108, 30);
            this.ktbRepo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbRepo.Name = "ktbRepo";
            this.ktbRepo.Size = new System.Drawing.Size(438, 23);
            this.ktbRepo.TabIndex = 1;
            //
            // kwlblToken
            //
            this.kwlblToken.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblToken.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblToken.Location = new System.Drawing.Point(8, 60);
            this.kwlblToken.Name = "kwlblToken";
            this.kwlblToken.Size = new System.Drawing.Size(84, 20);
            this.kwlblToken.Text = "PAT (token):";
            this.kwlblToken.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ktbToken
            //
            this.ktbToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbToken.Location = new System.Drawing.Point(108, 58);
            this.ktbToken.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbToken.Name = "ktbToken";
            this.ktbToken.PasswordChar = '●';
            this.ktbToken.Size = new System.Drawing.Size(438, 23);
            this.ktbToken.TabIndex = 2;
            this.ktbToken.UseSystemPasswordChar = true;
            //
            // pnlButtons
            //
            this.pnlButtons.Controls.Add(this.kbtnCreate);
            this.pnlButtons.Controls.Add(this.kbtnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 506);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.pnlButtons.Size = new System.Drawing.Size(584, 55);
            this.pnlButtons.TabIndex = 1;
            //
            // kbtnCreate
            //
            this.kbtnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCreate.Location = new System.Drawing.Point(333, 15);
            this.kbtnCreate.Name = "kbtnCreate";
            this.kbtnCreate.Size = new System.Drawing.Size(120, 28);
            this.kbtnCreate.TabIndex = 0;
            this.kbtnCreate.Values.Text = "Create on GitHub";
            this.kbtnCreate.Click += new System.EventHandler(this.kbtnCreate_Click);
            //
            // kbtnCancel
            //
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.Location = new System.Drawing.Point(459, 15);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(113, 28);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.Text = "Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            //
            // GitHubIssueReportForm
            //
            this.AcceptButton = this.kbtnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "GitHubIssueReportForm";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Bug Report on GitHub";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpGitHub.Panel)).EndInit();
            this.kgrpGitHub.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgrpGitHub)).EndInit();
            this.kgrpGitHub.ResumeLayout(false);
            this.tlpGitHub.ResumeLayout(false);
            this.tlpGitHub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlMain;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private Krypton.Toolkit.KryptonWrapLabel kwlblSummary;
        private Krypton.Toolkit.KryptonTextBox ktbSummary;
        private Krypton.Toolkit.KryptonWrapLabel kwlblDescription;
        private Krypton.Toolkit.KryptonRichTextBox krtbDescription;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStepsToReproduce;
        private Krypton.Toolkit.KryptonRichTextBox krtbStepsToReproduce;
        private Krypton.Toolkit.KryptonWrapLabel kwlblExpectedBehavior;
        private Krypton.Toolkit.KryptonRichTextBox krtbExpectedBehavior;
        private Krypton.Toolkit.KryptonWrapLabel kwlblActualBehavior;
        private Krypton.Toolkit.KryptonRichTextBox krtbActualBehavior;
        private Krypton.Toolkit.KryptonWrapLabel kwlblOs;
        private Krypton.Toolkit.KryptonTextBox ktbOs;
        private Krypton.Toolkit.KryptonWrapLabel kwlblOsVersion;
        private Krypton.Toolkit.KryptonTextBox ktbOsVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblFrameworkVersion;
        private Krypton.Toolkit.KryptonTextBox ktbFrameworkVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblToolkitVersion;
        private Krypton.Toolkit.KryptonTextBox ktbToolkitVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblAdditionalInfo;
        private Krypton.Toolkit.KryptonRichTextBox krtbAdditionalInfo;
        private Krypton.Toolkit.KryptonWrapLabel kwlblAreasAffected;
        private Krypton.Toolkit.KryptonComboBox kcmbAreasAffected;
        private Krypton.Toolkit.KryptonGroupBox kgrpGitHub;
        private System.Windows.Forms.TableLayoutPanel tlpGitHub;
        private Krypton.Toolkit.KryptonWrapLabel kwlblOwner;
        private Krypton.Toolkit.KryptonTextBox ktbOwner;
        private Krypton.Toolkit.KryptonWrapLabel kwlblRepo;
        private Krypton.Toolkit.KryptonTextBox ktbRepo;
        private Krypton.Toolkit.KryptonWrapLabel kwlblToken;
        private Krypton.Toolkit.KryptonTextBox ktbToken;
        private Krypton.Toolkit.KryptonPanel pnlButtons;
        private Krypton.Toolkit.KryptonButton kbtnCreate;
        private Krypton.Toolkit.KryptonButton kbtnCancel;
    }
}
