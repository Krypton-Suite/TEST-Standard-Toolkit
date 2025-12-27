#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// Specifies the type of file dialog.
/// </summary>
public enum FileDialogType
{
    /// <summary>Open file dialog.</summary>
    Open,
    /// <summary>Save file dialog.</summary>
    Save
}

/// <summary>
/// Displays a Krypton-styled dialog box from which the user can select files.
/// </summary>
[ToolboxItem(false)]
[DefaultEvent(nameof(FileOk))]
[DefaultProperty(nameof(FileName))]
public partial class KryptonCommonFileDialog : KryptonForm
{
    #region Instance Fields

    private FileDialogType _dialogType = FileDialogType.Open;
    private string _title = string.Empty;
    private string _initialDirectory = string.Empty;
    private string _filter = "All Files (*.*)|*.*";
    private int _filterIndex = 1;
    private string _defaultExt = string.Empty;
    private bool _addExtension = true;
    private bool _checkFileExists = false;
    private bool _checkPathExists = true;
    private bool _validateNames = true;
    private bool _restoreDirectory = false;
    private bool _supportMultiDottedExtensions = false;
    private bool _multiselect = false;
    private bool _showPlacesPane = false;
    private bool _showPreviewPane = false;
    private View _viewMode = View.Details;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;

    private string _currentDirectory = string.Empty;
    private string _selectedFile = string.Empty;
    private string[] _selectedFiles = Array.Empty<string>();
    private string _savedDirectory = string.Empty;

    private readonly NavigationHistory _navigationHistory;
    private readonly List<FileFilterItem> _filterItems;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the user clicks on the Open or Save button on a file dialog box.
    /// </summary>
    [Category(@"Action")]
    [Description("Occurs when the user clicks on the Open or Save button.")]
    public event CancelEventHandler? FileOk;

    /// <summary>
    /// Occurs when the current directory changes.
    /// </summary>
    [Category(@"Action")]
    [Description("Occurs when the current directory changes.")]
    public event EventHandler? DirectoryChanged;

    /// <summary>
    /// Occurs when the file selection changes.
    /// </summary>
    [Category(@"Action")]
    [Description("Occurs when the file selection changes.")]
    public event EventHandler? SelectionChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the KryptonCommonFileDialog class.
    /// </summary>
    public KryptonCommonFileDialog()
    {
        _navigationHistory = new NavigationHistory();
        _filterItems = new List<FileFilterItem>();
        InitializeComponent();
        SetupControls();
        InitializeDefaults();
    }

    private void SetupControls()
    {
        Location = new Point(0, 0);

        // Set default size
        Size = new Size(946, 533);

        // Setup view mode combo items
        if (_cmbViewMode != null)
        {
            _cmbViewMode.Items.AddRange(new[] { "Large Icons", "Small Icons", "List", "Details", "Tiles" });
            _cmbViewMode.SelectedIndex = (int)_viewMode;
        }
        
        // Setup browser properties
        if (_browser != null)
        {
            _browser.ViewMode = _viewMode;
            _browser.ShowHiddenFiles = _showHiddenFiles;
            _browser.ShowSystemFiles = _showSystemFiles;
            _browser.SelectionMode = _multiselect ? SelectionMode.MultiExtended : SelectionMode.One;
        }
        
        // Setup button text based on dialog type
        if (_btnAction != null)
        {
            _btnAction.Text = _dialogType == FileDialogType.Open ? "&Open" : "&Save";
        }
        
        // Setup tab order
        if (_btnBack != null) _btnBack.TabIndex = 1;
        if (_btnForward != null) _btnForward.TabIndex = 2;
        if (_btnUp != null) _btnUp.TabIndex = 3;
        if (_btnNewFolder != null) _btnNewFolder.TabIndex = 4;
        if (_cmbViewMode != null) _cmbViewMode.TabIndex = 5;
        if (_breadCrumbPath != null) _breadCrumbPath.TabIndex = 6;
        if (_browser != null) _browser.TabIndex = 7;
        if (_txtFileName != null) _txtFileName.TabIndex = 8;
        if (_cmbFileType != null) _cmbFileType.TabIndex = 9;
        if (_btnAction != null) _btnAction.TabIndex = 10;
        if (_btnCancel != null) _btnCancel.TabIndex = 11;
        
        AcceptButton = _btnAction;
        CancelButton = _btnCancel;
        
        // Ensure InternalPanel is visible and properly configured
        InternalPanel.Visible = true;
        InternalPanel.Dock = DockStyle.Fill;
        
        // Ensure all panels are visible
        if (_mainPanel != null)
        {
            _mainPanel.Visible = true;
            _mainPanel.Dock = DockStyle.Fill;
        }
        
        if (_navigationBar != null)
        {
            _navigationBar.Visible = true;
        }
        
        if (_contentPanel != null)
        {
            _contentPanel.Visible = true;
        }
        
        if (_bottomPanel != null)
        {
            _bottomPanel.Visible = true;
        }
        
        if (_browser != null)
        {
            _browser.Visible = true;
        }
    }

    /// <summary>
    /// Initializes a new instance of the KryptonCommonFileDialog class with specified dialog type.
    /// </summary>
    /// <param name="dialogType">The type of dialog (Open or Save).</param>
    public KryptonCommonFileDialog(FileDialogType dialogType) : this()
    {
        _dialogType = dialogType;
        UpdateDialogType();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the file dialog box title.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description("The file dialog box title.")]
    public string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value ?? string.Empty;
                Text = _title;
            }
        }
    }

    /// <summary>
    /// Gets or sets the initial directory displayed by the file dialog box.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description("The initial directory displayed by the file dialog box.")]
    public string InitialDirectory
    {
        get => _initialDirectory;
        set => _initialDirectory = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the current file name filter string, which determines the choices that appear in the 'Files of type' box.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("All Files (*.*)|*.*")]
    [Description("The current file name filter string, which determines the choices that appear in the 'Files of type' box.")]
    public string Filter
    {
        get => _filter;
        set
        {
            if (_filter != value)
            {
                _filter = value ?? "All Files (*.*)|*.*";
                ParseFilter();
                UpdateFileTypeComboBox();
            }
        }
    }

    /// <summary>
    /// Gets or sets the index of the filter currently selected in the file dialog box.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(1)]
    [Description("The index of the filter currently selected in the file dialog box.")]
    public int FilterIndex
    {
        get => _filterIndex;
        set
        {
            if (_filterIndex != value && value >= 1 && value <= _filterItems.Count)
            {
                _filterIndex = value;
                if (_cmbFileType != null)
                {
                    _cmbFileType.SelectedIndex = value - 1;
                }
                UpdateBrowserFilter();
            }
        }
    }

    /// <summary>
    /// Gets or sets the default file name extension.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description("The default file name extension.")]
    public string DefaultExt
    {
        get => _defaultExt;
        set => _defaultExt = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.")]
    public bool AddExtension
    {
        get => _addExtension;
        set => _addExtension = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
    public bool CheckFileExists
    {
        get => _checkFileExists;
        set => _checkFileExists = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.")]
    public bool CheckPathExists
    {
        get => _checkPathExists;
        set => _checkPathExists = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box validates file names.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box validates file names.")]
    public bool ValidateNames
    {
        get => _validateNames;
        set => _validateNames = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box restores the current directory before closing.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box restores the current directory before closing.")]
    public bool RestoreDirectory
    {
        get => _restoreDirectory;
        set => _restoreDirectory = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box supports displaying and saving files that have multiple file name extensions.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box supports displaying and saving files that have multiple file name extensions.")]
    public bool SupportMultiDottedExtensions
    {
        get => _supportMultiDottedExtensions;
        set => _supportMultiDottedExtensions = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box allows multiple files to be selected.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box allows multiple files to be selected.")]
    public bool Multiselect
    {
        get => _multiselect;
        set
        {
            if (_multiselect != value)
            {
                _multiselect = value;
                if (_browser != null)
                {
                    _browser.SelectionMode = value ? SelectionMode.MultiExtended : SelectionMode.One;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the places pane is visible.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the places pane is visible.")]
    public bool ShowPlacesPane
    {
        get => _showPlacesPane;
        set => _showPlacesPane = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the preview pane is visible.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the preview pane is visible.")]
    public bool ShowPreviewPane
    {
        get => _showPreviewPane;
        set => _showPreviewPane = value;
    }

    /// <summary>
    /// Gets or sets the view mode for the file list.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(View.Details)]
    [Description("Gets or sets the view mode for the file list.")]
    public View ViewMode
    {
        get => _viewMode;
        set
        {
            if (_viewMode != value)
            {
                _viewMode = value;
                if (_browser != null)
                {
                    _browser.ViewMode = value;
                }
                if (_cmbViewMode != null)
                {
                    _cmbViewMode.SelectedIndex = (int)value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether hidden files are displayed.")]
    public bool ShowHiddenFiles
    {
        get => _showHiddenFiles;
        set
        {
            if (_showHiddenFiles != value)
            {
                _showHiddenFiles = value;
                if (_browser != null)
                {
                    _browser.ShowHiddenFiles = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether system files are displayed.")]
    public bool ShowSystemFiles
    {
        get => _showSystemFiles;
        set
        {
            if (_showSystemFiles != value)
            {
                _showSystemFiles = value;
                if (_browser != null)
                {
                    _browser.ShowSystemFiles = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a string containing the file name selected in the file dialog box.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FileName
    {
        get => _selectedFile;
        set => _selectedFile = value ?? string.Empty;
    }

    /// <summary>
    /// Gets the file names of all selected files in the dialog box.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] FileNames => _multiselect ? _selectedFiles : new[] { _selectedFile };

    /// <summary>
    /// Gets the file name and extension for the file selected in the dialog box. The file name does not include the path.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SafeFileName => Path.GetFileName(_selectedFile);

    /// <summary>
    /// Gets an array of file names and extensions for all the selected files in the dialog box. The file names do not include the path.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SafeFileNames => FileNames.Select(Path.GetFileName).ToArray();

    /// <summary>
    /// Gets the current directory path.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CurrentDirectory => _currentDirectory;

    /// <summary>
    /// Gets a value indicating whether navigation back is possible.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanNavigateBack => _navigationHistory.CanNavigateBack;

    /// <summary>
    /// Gets a value indicating whether navigation forward is possible.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanNavigateForward => _navigationHistory.CanNavigateForward;

    #endregion

    #region Visual State Properties

    /// <summary>
    /// Gets access to the common file dialog appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common file dialog appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new PaletteFormRedirect? StateCommon => base.StateCommon;

    private bool ShouldSerializeStateCommon() => StateCommon is { IsDefault: false };

    /// <summary>
    /// Gets access to the inactive file dialog appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining inactive file dialog appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new PaletteForm? StateInactive => base.StateInactive;

    private bool ShouldSerializeStateInactive() => StateInactive is { IsDefault: false };

    /// <summary>
    /// Gets access to the active file dialog appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active file dialog appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new PaletteForm? StateActive => base.StateActive;

    private bool ShouldSerializeStateActive() => StateActive is { IsDefault: false };

    #endregion

    #region Public Methods

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        _title = string.Empty;
        _initialDirectory = string.Empty;
        _filter = "All Files (*.*)|*.*";
        _filterIndex = 1;
        _defaultExt = string.Empty;
        _addExtension = true;
        _checkFileExists = false;
        _checkPathExists = true;
        _validateNames = true;
        _restoreDirectory = false;
        _supportMultiDottedExtensions = false;
        _multiselect = false;
        _showPlacesPane = false;
        _showPreviewPane = false;
        _viewMode = View.Details;
        _showHiddenFiles = false;
        _showSystemFiles = false;
        _selectedFile = string.Empty;
        _selectedFiles = Array.Empty<string>();
    }

    /// <summary>
    /// Runs a common dialog box.
    /// </summary>
    /// <returns>DialogResult.OK if the user clicks OK; otherwise, DialogResult.Cancel.</returns>
    public new DialogResult ShowDialog()
    {
        return ShowDialog(null);
    }

    /// <summary>
    /// Runs a common dialog box with the specified owner.
    /// </summary>
    /// <param name="owner">Any object that implements IWin32Window that represents the top-level window that will own the modal dialog box.</param>
    /// <returns>DialogResult.OK if the user clicks OK; otherwise, DialogResult.Cancel.</returns>
    public new DialogResult ShowDialog(IWin32Window? owner)
    {
        // Save current directory if needed
        if (_restoreDirectory)
        {
            _savedDirectory = Directory.GetCurrentDirectory();
        }

        // Ensure controls are created and visible
        if (_navigationBar == null || _browser == null || _selectionArea == null || _buttonArea == null)
        {
            InitializeComponent();
        }

        // Initialize dialog
        InitializeDialog();

        // Ensure all controls are visible
        EnsureControlsVisible();

        // Show dialog
        DialogResult result = base.ShowDialog(owner);

        // Restore directory if needed
        if (_restoreDirectory && !string.IsNullOrEmpty(_savedDirectory))
        {
            try
            {
                Directory.SetCurrentDirectory(_savedDirectory);
            }
            catch
            {
                // Ignore restore errors
            }
        }

        return result;
    }
    
    private void EnsureControlsVisible()
    {
        if (_mainPanel != null)
        {
            _mainPanel.Visible = true;
        }
        
        if (_navigationBar != null)
        {
            _navigationBar.Visible = true;
        }
        
        if (_browser != null)
        {
            _browser.Visible = true;
        }
        
        if (_selectionArea != null)
        {
            _selectionArea.Visible = true;
        }
        
        if (_buttonArea != null)
        {
            _buttonArea.Visible = true;
        }
        
        // Force layout
        PerformLayout();
        Invalidate(true);
    }

    /// <summary>
    /// Opens the file selected by the user with read-only permission.
    /// </summary>
    /// <returns>A Stream that specifies the read-only file selected by the user.</returns>
    /// <exception cref="ArgumentNullException">The file name is null.</exception>
    public Stream OpenFile()
    {
        if (string.IsNullOrEmpty(_selectedFile))
        {
            throw new ArgumentNullException(nameof(FileName), "File name cannot be null.");
        }

        return new FileStream(_selectedFile, FileMode.Open, FileAccess.Read);
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Raises the FileOk event.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    protected virtual void OnFileOk(CancelEventArgs e)
    {
        FileOk?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the Load event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        // Force InternalPanel to be visible and properly configured
        if (InternalPanel != null)
        {
            InternalPanel.Visible = true;
            InternalPanel.Dock = DockStyle.Fill;
            InternalPanel.BringToFront();
        }
        
        // Ensure main panel is visible and properly configured
        if (_mainPanel != null)
        {
            _mainPanel.Visible = true;
            _mainPanel.Dock = DockStyle.Fill;
            _mainPanel.BringToFront();
        }
        
        // Ensure all child panels are visible
        if (_navigationBar != null)
        {
            _navigationBar.Visible = true;
            _navigationBar.BringToFront();
        }
        
        if (_contentPanel != null)
        {
            _contentPanel.Visible = true;
            _contentPanel.Dock = DockStyle.Fill;
        }
        
        if (_bottomPanel != null)
        {
            _bottomPanel.Visible = true;
            _bottomPanel.Dock = DockStyle.Bottom;
        }
        
        if (_browser != null)
        {
            _browser.Visible = true;
        }
        
        InitializeDialog();
        
        // Force a complete layout refresh
        SuspendLayout();
        ResumeLayout(true);
        PerformLayout();
        Invalidate(true);
        Update();
    }

    /// <summary>
    /// Raises the DirectoryChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnDirectoryChanged(EventArgs e)
    {
        DirectoryChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the SelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the FormClosing event.
    /// </summary>
    /// <param name="e">A FormClosingEventArgs containing the event data.</param>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing && DialogResult == DialogResult.OK)
        {
            // Validate before closing
            if (!ValidateSelection())
            {
                e.Cancel = true;
                return;
            }
        }

        base.OnFormClosing(e);
    }

    /// <summary>
    /// Processes a command key.
    /// </summary>
    /// <param name="msg">A Message, passed by reference, that represents the Win32 message to process.</param>
    /// <param name="keyData">One of the Keys values that represents the key to process.</param>
    /// <returns>true if the character was processed by the control; otherwise, false.</returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Handle keyboard shortcuts
        if (keyData == (Keys.Alt | Keys.Left))
        {
            if (_btnBack != null && _btnBack.Enabled)
            {
                _btnBack.PerformClick();
                return true;
            }
        }
        else if (keyData == (Keys.Alt | Keys.Right))
        {
            if (_btnForward != null && _btnForward.Enabled)
            {
                _btnForward.PerformClick();
                return true;
            }
        }
        else if (keyData == (Keys.Alt | Keys.Up))
        {
            if (_btnUp != null && _btnUp.Enabled)
            {
                _btnUp.PerformClick();
                return true;
            }
        }
        else if (keyData == Keys.F5)
        {
            if (_browser != null)
            {
                _browser.Refresh();
                return true;
            }
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    #endregion

    #region Implementation

    private void InitializeDefaults()
    {
        // Set default title based on dialog type
        UpdateDialogType();
    }

    private void UpdateDialogType()
    {
        if (string.IsNullOrEmpty(_title))
        {
            _title = _dialogType == FileDialogType.Open ? "Open" : "Save As";
            Text = _title;
        }

        if (_btnAction != null)
        {
            _btnAction.Text = _dialogType == FileDialogType.Open ? "&Open" : "&Save";
            _btnAction.Visible = true;
        }
    }

    private void NavigationBar_Resize(object? sender, EventArgs e)
    {
        if (_breadCrumbPath != null && _navigationBar != null)
        {
            _breadCrumbPath.Width = _navigationBar.Width - 260;
        }
    }
    
    private void SelectionArea_Resize(object? sender, EventArgs e)
    {
        if (_selectionArea != null)
        {
            if (_txtFileName != null)
            {
                _txtFileName.Width = _selectionArea.Width - 100;
            }
            
            if (_cmbFileType != null)
            {
                _cmbFileType.Width = _selectionArea.Width - 100;
            }
        }
    }
    
    private void ButtonArea_Layout(object? sender, LayoutEventArgs e)
    {
        if (_buttonArea != null && _btnAction != null && _btnCancel != null)
        {
            _btnCancel.Location = new Point(_buttonArea.Width - 87, 13);
            _btnAction.Location = new Point(_buttonArea.Width - 170, 13);
        }
    }

    private void InitializeDialog()
    {
        // Parse filter
        ParseFilter();
        UpdateFileTypeComboBox();

        // Set initial directory
        string initialDir = !string.IsNullOrEmpty(_initialDirectory) && Directory.Exists(_initialDirectory)
            ? _initialDirectory
            : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        _currentDirectory = initialDir;
        if (_browser != null)
        {
            _browser.CurrentPath = initialDir;
        }

        UpdateBreadCrumbPath(initialDir);

        // Set file name if provided
        if (!string.IsNullOrEmpty(_selectedFile))
        {
            if (_txtFileName != null)
            {
                _txtFileName.Text = Path.GetFileName(_selectedFile);
            }
        }

        // Update navigation buttons
        UpdateNavigationButtons();
    }

    private void ParseFilter()
    {
        _filterItems.Clear();

        if (string.IsNullOrEmpty(_filter))
        {
            _filterItems.Add(new FileFilterItem("All Files", "*.*"));
            return;
        }

        string[] parts = _filter.Split('|');
        for (int i = 0; i < parts.Length; i += 2)
        {
            string description = parts[i];
            string pattern = i + 1 < parts.Length ? parts[i + 1] : "*.*";
            _filterItems.Add(new FileFilterItem(description, pattern));
        }

        // Ensure filter index is valid
        if (_filterIndex < 1 || _filterIndex > _filterItems.Count)
        {
            _filterIndex = 1;
        }
    }

    private void UpdateFileTypeComboBox()
    {
        if (_cmbFileType == null)
            return;

        _cmbFileType.Items.Clear();
        foreach (FileFilterItem item in _filterItems)
        {
            _cmbFileType.Items.Add(item.Description);
        }

        if (_filterIndex >= 1 && _filterIndex <= _filterItems.Count)
        {
            _cmbFileType.SelectedIndex = _filterIndex - 1;
        }
        else if (_cmbFileType.Items.Count > 0)
        {
            _cmbFileType.SelectedIndex = 0;
        }

        UpdateBrowserFilter();
    }

    private void UpdateBrowserFilter()
    {
        if (_browser == null || _filterIndex < 1 || _filterIndex > _filterItems.Count)
            return;

        FileFilterItem selectedFilter = _filterItems[_filterIndex - 1];
        _browser.FileFilter = selectedFilter.Pattern;
    }

    private void UpdateNavigationButtons()
    {
        if (_btnBack != null)
        {
            _btnBack.Enabled = _navigationHistory.CanNavigateBack;
        }

        if (_btnForward != null)
        {
            _btnForward.Enabled = _navigationHistory.CanNavigateForward;
        }

        if (_btnUp != null && _browser != null)
        {
            string currentPath = _browser.CurrentPath;
            _btnUp.Enabled = !string.IsNullOrEmpty(currentPath) && Directory.GetParent(currentPath) != null;
        }
    }

    private bool ValidateSelection()
    {
        if (_multiselect)
        {
            string[] selectedPaths = _browser?.SelectedPaths ?? Array.Empty<string>();
            if (selectedPaths.Length == 0)
            {
                KryptonMessageBox.Show("Please select at least one file.", _title,
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                return false;
            }

            var validFiles = new List<string>();
            foreach (string path in selectedPaths)
            {
                if (File.Exists(path))
                {
                    validFiles.Add(path);
                }
            }

            if (validFiles.Count == 0)
            {
                KryptonMessageBox.Show("Please select valid files.", _title,
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return false;
            }

            _selectedFiles = validFiles.ToArray();
            _selectedFile = validFiles[0];
        }
        else
        {
            string fileName = _txtFileName?.Text.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(fileName))
            {
                if (_browser?.SelectedPath != null && File.Exists(_browser.SelectedPath))
                {
                    fileName = Path.GetFileName(_browser.SelectedPath);
                }
                else
                {
                    KryptonMessageBox.Show("Please enter or select a file name.", _title,
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    return false;
                }
            }

            string fullPath = Path.Combine(_currentDirectory, fileName);

            if (_dialogType == FileDialogType.Open)
            {
                if (_checkFileExists && !File.Exists(fullPath))
                {
                    KryptonMessageBox.Show($"The file '{fileName}' does not exist.", _title,
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                    return false;
                }
            }
            else // Save
            {
                if (_checkFileExists && File.Exists(fullPath))
                {
                    DialogResult result = KryptonMessageBox.Show(
                        $"The file '{fileName}' already exists.\nDo you want to replace it?",
                        _title,
                        KryptonMessageBoxButtons.YesNo,
                        KryptonMessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                // Add extension if needed
                if (_addExtension && !string.IsNullOrEmpty(_defaultExt))
                {
                    if (!fileName.EndsWith(_defaultExt, StringComparison.OrdinalIgnoreCase))
                    {
                        fileName += _defaultExt;
                        fullPath = Path.Combine(_currentDirectory, fileName);
                    }
                }
            }

            // Validate file name
            if (_validateNames)
            {
                try
                {
                    char[] invalidChars = Path.GetInvalidFileNameChars();
                    if (fileName.IndexOfAny(invalidChars) >= 0)
                    {
                        KryptonMessageBox.Show($"The file name contains invalid characters.",
                            _title, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch
                {
                    KryptonMessageBox.Show("The file name is invalid.", _title,
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                    return false;
                }
            }

            _selectedFile = fullPath;
            _selectedFiles = new[] { fullPath };
        }

        // Fire FileOk event
        CancelEventArgs e = new CancelEventArgs();
        OnFileOk(e);

        return !e.Cancel;
    }

    #endregion

    #region Event Handlers

    private void BtnBack_Click(object? sender, EventArgs e)
    {
        string? path = _navigationHistory.NavigateBack();
        if (path != null && _browser != null)
        {
            _browser.NavigateTo(path);
        }
    }

    private void BtnForward_Click(object? sender, EventArgs e)
    {
        string? path = _navigationHistory.NavigateForward();
        if (path != null && _browser != null)
        {
            _browser.NavigateTo(path);
        }
    }

    private void BtnUp_Click(object? sender, EventArgs e)
    {
        if (_browser == null)
            return;

        string currentPath = _browser.CurrentPath;
        DirectoryInfo? parent = Directory.GetParent(currentPath);
        if (parent != null)
        {
            _browser.NavigateTo(parent.FullName);
        }
    }

    private void BtnNewFolder_Click(object? sender, EventArgs e)
    {
        // TODO: Implement new folder creation
        KryptonMessageBox.Show("New folder functionality not yet implemented.", _title,
            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void CmbViewMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_cmbViewMode != null && _browser != null)
        {
            View newView = (View)_cmbViewMode.SelectedIndex;
            _browser.ViewMode = newView;
            _viewMode = newView;
        }
    }

    private void BreadCrumbPath_SelectedItemChanged(object? sender, EventArgs e)
    {
        if (_breadCrumbPath?.SelectedItem == null || _browser == null)
            return;

        // Get the path from the selected breadcrumb item
        string? path = GetPathFromBreadCrumbItem(_breadCrumbPath.SelectedItem);
        if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
        {
            _browser.NavigateTo(path);
        }
    }
    
    private string? GetPathFromBreadCrumbItem(KryptonBreadCrumbItem item)
    {
        if (item == null || _breadCrumbPath == null)
            return null;

        // Build path from root to selected item
        var pathParts = new List<string>();
        KryptonBreadCrumbItem? current = item;
        
        while (current != null && current != _breadCrumbPath.RootItem)
        {
            string itemText = current.ShortText ?? string.Empty;
            if (!string.IsNullOrEmpty(itemText))
            {
                pathParts.Insert(0, itemText);
            }
            current = current.Parent;
        }
        
        if (pathParts.Count == 0)
            return null;
        
        // Handle Windows drive letters (e.g., "C:")
        if (pathParts[0].EndsWith(":", StringComparison.Ordinal))
        {
            string fullPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathParts);
            // Ensure it ends with backslash for drive root
            if (pathParts.Count == 1)
            {
                fullPath += Path.DirectorySeparatorChar;
            }
            return fullPath;
        }
        
        // For Unix-style absolute paths, start from root
        return Path.DirectorySeparatorChar + string.Join(Path.DirectorySeparatorChar.ToString(), pathParts);
    }
    
    private void UpdateBreadCrumbPath(string path)
    {
        if (_breadCrumbPath == null || string.IsNullOrEmpty(path))
            return;

        try
        {
            // Clear existing items
            _breadCrumbPath.RootItem.Items.Clear();
            
            // Build breadcrumb hierarchy from path
            if (Path.IsPathRooted(path))
            {
                // Handle Windows drive letters (e.g., "C:\")
                if (path.Length >= 3 && path[1] == ':' && path[2] == Path.DirectorySeparatorChar)
                {
                    string drive = path.Substring(0, 2);
                    KryptonBreadCrumbItem driveItem = new KryptonBreadCrumbItem(drive);
                    _breadCrumbPath.RootItem.Items.Add(driveItem);
                    
                    // Add path segments
                    string remainingPath = path.Substring(3);
                    if (!string.IsNullOrEmpty(remainingPath))
                    {
                        BuildBreadCrumbHierarchy(driveItem, remainingPath);
                        _breadCrumbPath.SelectedItem = GetLastBreadCrumbItem(driveItem);
                    }
                    else
                    {
                        _breadCrumbPath.SelectedItem = driveItem;
                    }
                }
                else
                {
                    // Unix-style absolute path
                    BuildBreadCrumbHierarchy(_breadCrumbPath.RootItem, path.TrimStart(Path.DirectorySeparatorChar));
                    _breadCrumbPath.SelectedItem = GetLastBreadCrumbItem(_breadCrumbPath.RootItem);
                }
            }
            else
            {
                // Relative path
                BuildBreadCrumbHierarchy(_breadCrumbPath.RootItem, path);
                _breadCrumbPath.SelectedItem = GetLastBreadCrumbItem(_breadCrumbPath.RootItem);
            }
        }
        catch
        {
            // If path parsing fails, just set root item text
            _breadCrumbPath.RootItem.ShortText = path;
        }
    }
    
    private void BuildBreadCrumbHierarchy(KryptonBreadCrumbItem parent, string path)
    {
        if (string.IsNullOrEmpty(path))
            return;

        string[] segments = path.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, 
            StringSplitOptions.RemoveEmptyEntries);
        
        KryptonBreadCrumbItem? currentParent = parent;
        foreach (string segment in segments)
        {
            if (string.IsNullOrEmpty(segment))
                continue;
                
            KryptonBreadCrumbItem item = new KryptonBreadCrumbItem(segment);
            currentParent?.Items.Add(item);
            currentParent = item;
        }
    }
    
    private KryptonBreadCrumbItem? GetLastBreadCrumbItem(KryptonBreadCrumbItem item)
    {
        if (item.Items.Count == 0)
            return item;
            
        return GetLastBreadCrumbItem(item.Items[item.Items.Count - 1]);
    }

    private void Browser_PathChanged(object? sender, EventArgs e)
    {
        if (_browser == null)
            return;

        string newPath = _browser.CurrentPath;
        if (_currentDirectory != newPath)
        {
            _navigationHistory.NavigateTo(_currentDirectory);
            _currentDirectory = newPath;

            UpdateBreadCrumbPath(newPath);

            UpdateNavigationButtons();
            OnDirectoryChanged(EventArgs.Empty);
        }
    }

    private void Browser_SelectionChanged(object? sender, EventArgs e)
    {
        if (_browser == null)
            return;

        string? selectedPath = _browser.SelectedPath;
        if (!string.IsNullOrEmpty(selectedPath) && File.Exists(selectedPath))
        {
            if (_txtFileName != null)
            {
                _txtFileName.Text = Path.GetFileName(selectedPath);
            }
        }

        OnSelectionChanged(EventArgs.Empty);
    }

    private void Browser_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        KryptonMessageBox.Show($"File system error: {e.Exception.Message}", _title,
            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
    }

    private void CmbFileType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_cmbFileType != null && _cmbFileType.SelectedIndex >= 0)
        {
            _filterIndex = _cmbFileType.SelectedIndex + 1;
            UpdateBrowserFilter();
        }
    }

    private void TxtFileName_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && _btnAction != null)
        {
            _btnAction.PerformClick();
            e.Handled = true;
        }
    }

    private void BtnAction_Click(object? sender, EventArgs e)
    {
        if (ValidateSelection())
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    #endregion

    #region Helper Classes

    private class NavigationHistory
    {
        private readonly Stack<string> _backStack = new();
        private readonly Stack<string> _forwardStack = new();
        private string _currentPath = string.Empty;

        public void NavigateTo(string path)
        {
            if (!string.IsNullOrEmpty(_currentPath) && _currentPath != path)
            {
                _backStack.Push(_currentPath);
                _forwardStack.Clear();
            }
            _currentPath = path;
        }

        public string? NavigateBack()
        {
            if (_backStack.Count > 0)
            {
                _forwardStack.Push(_currentPath);
                _currentPath = _backStack.Pop();
                return _currentPath;
            }
            return null;
        }

        public string? NavigateForward()
        {
            if (_forwardStack.Count > 0)
            {
                _backStack.Push(_currentPath);
                _currentPath = _forwardStack.Pop();
                return _currentPath;
            }
            return null;
        }

        public bool CanNavigateBack => _backStack.Count > 0;
        public bool CanNavigateForward => _forwardStack.Count > 0;
        public string CurrentPath => _currentPath;
    }

    private class FileFilterItem
    {
        public string Description { get; }
        public string Pattern { get; }

        public FileFilterItem(string description, string pattern)
        {
            Description = description;
            Pattern = pattern;
        }
    }

    #endregion
}

