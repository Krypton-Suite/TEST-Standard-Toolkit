#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

partial class KryptonCommonFileDialog
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer? components = null;

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
        this._mainPanel = new Krypton.Toolkit.KryptonPanel();
        this._navigationBar = new Krypton.Toolkit.KryptonPanel();
        this._btnBack = new Krypton.Toolkit.KryptonButton();
        this._btnForward = new Krypton.Toolkit.KryptonButton();
        this._btnUp = new Krypton.Toolkit.KryptonButton();
        this._btnNewFolder = new Krypton.Toolkit.KryptonButton();
        this._cmbViewMode = new Krypton.Toolkit.KryptonComboBox();
        this._breadCrumbPath = new Krypton.Toolkit.KryptonBreadCrumb();
        this._contentPanel = new Krypton.Toolkit.KryptonPanel();
        this._browser = new Krypton.Toolkit.KryptonBrowserControl();
        this._bottomPanel = new Krypton.Toolkit.KryptonPanel();
        this._selectionArea = new Krypton.Toolkit.KryptonPanel();
        this._lblFileName = new Krypton.Toolkit.KryptonLabel();
        this._txtFileName = new Krypton.Toolkit.KryptonTextBox();
        this._lblFileType = new Krypton.Toolkit.KryptonLabel();
        this._cmbFileType = new Krypton.Toolkit.KryptonComboBox();
        this._buttonArea = new Krypton.Toolkit.KryptonPanel();
        this._btnAction = new Krypton.Toolkit.KryptonButton();
        this._btnCancel = new Krypton.Toolkit.KryptonButton();
        ((System.ComponentModel.ISupportInitialize)(this._mainPanel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this._navigationBar)).BeginInit();
        this._navigationBar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._contentPanel)).BeginInit();
        this._contentPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._bottomPanel)).BeginInit();
        this._bottomPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._selectionArea)).BeginInit();
        this._selectionArea.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._buttonArea)).BeginInit();
        this._buttonArea.SuspendLayout();
        this.SuspendLayout();
        // 
        // _mainPanel
        // 
        this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this._mainPanel.Location = new System.Drawing.Point(0, 0);
        this._mainPanel.Margin = new System.Windows.Forms.Padding(0);
        this._mainPanel.Name = "_mainPanel";
        this._mainPanel.Padding = new System.Windows.Forms.Padding(0);
        this._mainPanel.Size = new System.Drawing.Size(800, 600);
        this._mainPanel.TabIndex = 0;
        this._mainPanel.Visible = true;
        // 
        // _navigationBar
        // 
        this._navigationBar.Controls.Add(this._btnBack);
        this._navigationBar.Controls.Add(this._btnForward);
        this._navigationBar.Controls.Add(this._btnUp);
        this._navigationBar.Controls.Add(this._btnNewFolder);
        this._navigationBar.Controls.Add(this._cmbViewMode);
        this._navigationBar.Controls.Add(this._breadCrumbPath);
        this._navigationBar.Dock = System.Windows.Forms.DockStyle.Top;
        this._navigationBar.Location = new System.Drawing.Point(0, 0);
        this._navigationBar.Margin = new System.Windows.Forms.Padding(0);
        this._navigationBar.Name = "_navigationBar";
        this._navigationBar.Padding = new System.Windows.Forms.Padding(4);
        this._navigationBar.Size = new System.Drawing.Size(800, 40);
        this._navigationBar.TabIndex = 0;
        this._navigationBar.Visible = true;
        this._navigationBar.Resize += new System.EventHandler(this.NavigationBar_Resize);
        // 
        // _btnBack
        // 
        this._btnBack.Enabled = false;
        this._btnBack.Location = new System.Drawing.Point(4, 4);
        this._btnBack.Name = "_btnBack";
        this._btnBack.Size = new System.Drawing.Size(32, 32);
        this._btnBack.TabIndex = 1;
        this._btnBack.Values.Text = "←";
        this._btnBack.Click += new System.EventHandler(this.BtnBack_Click);
        // 
        // _btnForward
        // 
        this._btnForward.Enabled = false;
        this._btnForward.Location = new System.Drawing.Point(40, 4);
        this._btnForward.Name = "_btnForward";
        this._btnForward.Size = new System.Drawing.Size(32, 32);
        this._btnForward.TabIndex = 2;
        this._btnForward.Values.Text = "→";
        this._btnForward.Click += new System.EventHandler(this.BtnForward_Click);
        // 
        // _btnUp
        // 
        this._btnUp.Location = new System.Drawing.Point(76, 4);
        this._btnUp.Name = "_btnUp";
        this._btnUp.Size = new System.Drawing.Size(32, 32);
        this._btnUp.TabIndex = 3;
        this._btnUp.Values.Text = "↑";
        this._btnUp.Click += new System.EventHandler(this.BtnUp_Click);
        // 
        // _btnNewFolder
        // 
        this._btnNewFolder.Location = new System.Drawing.Point(112, 4);
        this._btnNewFolder.Name = "_btnNewFolder";
        this._btnNewFolder.Size = new System.Drawing.Size(32, 32);
        this._btnNewFolder.TabIndex = 4;
        this._btnNewFolder.Values.Text = "📁";
        this._btnNewFolder.Click += new System.EventHandler(this.BtnNewFolder_Click);
        // 
        // _cmbViewMode
        // 
        this._cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this._cmbViewMode.Location = new System.Drawing.Point(148, 7);
        this._cmbViewMode.Name = "_cmbViewMode";
        this._cmbViewMode.Size = new System.Drawing.Size(100, 25);
        this._cmbViewMode.TabIndex = 5;
        this._cmbViewMode.SelectedIndexChanged += new System.EventHandler(this.CmbViewMode_SelectedIndexChanged);
        // 
        // _breadCrumbPath
        // 
        this._breadCrumbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this._breadCrumbPath.Location = new System.Drawing.Point(256, 7);
        this._breadCrumbPath.Name = "_breadCrumbPath";
        this._breadCrumbPath.Size = new System.Drawing.Size(540, 25);
        this._breadCrumbPath.TabIndex = 6;
        this._breadCrumbPath.SelectedItemChanged += new System.EventHandler(this.BreadCrumbPath_SelectedItemChanged);
        // 
        // _contentPanel
        // 
        this._contentPanel.Controls.Add(this._browser);
        this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this._contentPanel.Location = new System.Drawing.Point(0, 40);
        this._contentPanel.Margin = new System.Windows.Forms.Padding(0);
        this._contentPanel.Name = "_contentPanel";
        this._contentPanel.Padding = new System.Windows.Forms.Padding(0);
        this._contentPanel.Size = new System.Drawing.Size(800, 430);
        this._contentPanel.TabIndex = 1;
        this._contentPanel.Visible = true;
        // 
        // _browser
        // 
        this._browser.Dock = System.Windows.Forms.DockStyle.Fill;
        this._browser.Location = new System.Drawing.Point(0, 0);
        this._browser.Name = "_browser";
        this._browser.SelectionMode = System.Windows.Forms.SelectionMode.One;
        this._browser.ShowFiles = true;
        this._browser.ShowHiddenFiles = false;
        this._browser.ShowSystemFiles = false;
        this._browser.ShowTreeView = true;
        this._browser.Size = new System.Drawing.Size(800, 430);
        this._browser.TabIndex = 7;
        this._browser.ViewMode = System.Windows.Forms.View.Details;
        this._browser.Visible = true;
        this._browser.FileSystemError += new System.EventHandler<Krypton.Toolkit.FileSystemErrorEventArgs>(this.Browser_FileSystemError);
        this._browser.PathChanged += new System.EventHandler(this.Browser_PathChanged);
        this._browser.SelectionChanged += new System.EventHandler(this.Browser_SelectionChanged);
        // 
        // _bottomPanel
        // 
        this._bottomPanel.Controls.Add(this._selectionArea);
        this._bottomPanel.Controls.Add(this._buttonArea);
        this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this._bottomPanel.Location = new System.Drawing.Point(0, 470);
        this._bottomPanel.Margin = new System.Windows.Forms.Padding(0);
        this._bottomPanel.Name = "_bottomPanel";
        this._bottomPanel.Padding = new System.Windows.Forms.Padding(0);
        this._bottomPanel.Size = new System.Drawing.Size(800, 130);
        this._bottomPanel.TabIndex = 2;
        this._bottomPanel.Visible = true;
        // 
        // _selectionArea
        // 
        this._selectionArea.Controls.Add(this._lblFileName);
        this._selectionArea.Controls.Add(this._txtFileName);
        this._selectionArea.Controls.Add(this._lblFileType);
        this._selectionArea.Controls.Add(this._cmbFileType);
        this._selectionArea.Dock = System.Windows.Forms.DockStyle.Top;
        this._selectionArea.Location = new System.Drawing.Point(0, 0);
        this._selectionArea.Margin = new System.Windows.Forms.Padding(0);
        this._selectionArea.Name = "_selectionArea";
        this._selectionArea.Padding = new System.Windows.Forms.Padding(8);
        this._selectionArea.Size = new System.Drawing.Size(800, 80);
        this._selectionArea.TabIndex = 0;
        this._selectionArea.Resize += new System.EventHandler(this.SelectionArea_Resize);
        // 
        // _lblFileName
        // 
        this._lblFileName.Location = new System.Drawing.Point(8, 12);
        this._lblFileName.Name = "_lblFileName";
        this._lblFileName.Size = new System.Drawing.Size(80, 20);
        this._lblFileName.TabIndex = 0;
        this._lblFileName.Values.Text = "File &name:";
        // 
        // _txtFileName
        // 
        this._txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this._txtFileName.Location = new System.Drawing.Point(92, 10);
        this._txtFileName.Name = "_txtFileName";
        this._txtFileName.Size = new System.Drawing.Size(700, 23);
        this._txtFileName.TabIndex = 8;
        this._txtFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFileName_KeyDown);
        // 
        // _lblFileType
        // 
        this._lblFileType.Location = new System.Drawing.Point(8, 42);
        this._lblFileType.Name = "_lblFileType";
        this._lblFileType.Size = new System.Drawing.Size(80, 20);
        this._lblFileType.TabIndex = 2;
        this._lblFileType.Values.Text = "Files of &type:";
        // 
        // _cmbFileType
        // 
        this._cmbFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this._cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this._cmbFileType.Location = new System.Drawing.Point(92, 40);
        this._cmbFileType.Name = "_cmbFileType";
        this._cmbFileType.Size = new System.Drawing.Size(700, 25);
        this._cmbFileType.TabIndex = 9;
        this._cmbFileType.SelectedIndexChanged += new System.EventHandler(this.CmbFileType_SelectedIndexChanged);
        // 
        // _buttonArea
        // 
        this._buttonArea.Controls.Add(this._btnAction);
        this._buttonArea.Controls.Add(this._btnCancel);
        this._buttonArea.Dock = System.Windows.Forms.DockStyle.Fill;
        this._buttonArea.Location = new System.Drawing.Point(0, 80);
        this._buttonArea.Margin = new System.Windows.Forms.Padding(0);
        this._buttonArea.MinimumSize = new System.Drawing.Size(0, 50);
        this._buttonArea.Name = "_buttonArea";
        this._buttonArea.Padding = new System.Windows.Forms.Padding(8);
        this._buttonArea.Size = new System.Drawing.Size(800, 50);
        this._buttonArea.TabIndex = 1;
        this._buttonArea.Layout += new System.Windows.Forms.LayoutEventHandler(this.ButtonArea_Layout);
        // 
        // _btnAction
        // 
        this._btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this._btnAction.DialogResult = System.Windows.Forms.DialogResult.OK;
        this._btnAction.Location = new System.Drawing.Point(630, 13);
        this._btnAction.Name = "_btnAction";
        this._btnAction.Size = new System.Drawing.Size(75, 23);
        this._btnAction.TabIndex = 10;
        this._btnAction.Values.Text = "&Open";
        this._btnAction.Click += new System.EventHandler(this.BtnAction_Click);
        // 
        // _btnCancel
        // 
        this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this._btnCancel.Location = new System.Drawing.Point(713, 13);
        this._btnCancel.Name = "_btnCancel";
        this._btnCancel.Size = new System.Drawing.Size(75, 23);
        this._btnCancel.TabIndex = 11;
        this._btnCancel.Values.Text = "&Cancel";
        this._btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
        // 
        // KryptonCommonFileDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        this.KeyPreview = true;
        this.MaximizeBox = false;
        this.MinimumSize = new System.Drawing.Size(600, 400);
        this.MinimizeBox = false;
        this.Name = "KryptonCommonFileDialog";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "KryptonCommonFileDialog";
        ((System.ComponentModel.ISupportInitialize)(this._mainPanel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this._navigationBar)).EndInit();
        this._navigationBar.ResumeLayout(false);
        this._navigationBar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this._contentPanel)).EndInit();
        this._contentPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this._bottomPanel)).EndInit();
        this._bottomPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this._selectionArea)).EndInit();
        this._selectionArea.ResumeLayout(false);
        this._selectionArea.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this._buttonArea)).EndInit();
        this._buttonArea.ResumeLayout(false);
        
        // Add child panels to main panel in correct order (important for docking)
        this._mainPanel.Controls.Add(this._navigationBar);
        this._mainPanel.Controls.Add(this._bottomPanel);
        this._mainPanel.Controls.Add(this._contentPanel);
        
        // Add main panel to Controls BEFORE ResumeLayout
        // This routes to InternalPanel.Controls for KryptonForm
        this.Controls.Add(this._mainPanel);
        
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private KryptonPanel _mainPanel;
    private KryptonPanel _navigationBar;
    private KryptonButton _btnBack;
    private KryptonButton _btnForward;
    private KryptonButton _btnUp;
    private KryptonButton _btnNewFolder;
    private KryptonComboBox _cmbViewMode;
    private KryptonBreadCrumb _breadCrumbPath;
    private KryptonPanel _contentPanel;
    private KryptonBrowserControl _browser;
    private KryptonPanel _bottomPanel;
    private KryptonPanel _selectionArea;
    private KryptonLabel _lblFileName;
    private KryptonTextBox _txtFileName;
    private KryptonLabel _lblFileType;
    private KryptonComboBox _cmbFileType;
    private KryptonPanel _buttonArea;
    private KryptonButton _btnAction;
    private KryptonButton _btnCancel;
}

