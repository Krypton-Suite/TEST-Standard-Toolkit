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
using System.Windows.Forms;

/// <summary>
/// Provides a comprehensive file explorer control with navigation toolbar, address bar, and status bar.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultEvent(nameof(PathChanged))]
[DefaultProperty(nameof(ExplorerValues))]
[DesignerCategory(@"code")]
[Description(@"A comprehensive file explorer control with navigation toolbar, address bar, and status bar.")]
[Docking(DockingBehavior.Ask)]
public class KryptonExplorerBrowser : KryptonPanel
{
    #region Instance Fields

    private KryptonPanel? _toolbarPanel;
    private KryptonPanel? _contentPanel;
    private KryptonPanel? _statusPanel;
    private KryptonSplitContainer? _splitContainer;
    private KryptonFileSystemTreeView? _treeView;
    //private KryptonFileSystemListView? _listView;
    private KryptonSplitterPanel? _treeViewPanel;
    private KryptonSplitterPanel? _listViewPanel;

    private KryptonButton? _btnBack;
    private KryptonButton? _btnForward;
    private KryptonButton? _btnUp;
    private KryptonButton? _btnRefresh;
    private KryptonButton? _btnViewDetails;
    private KryptonButton? _btnViewList;
    private KryptonButton? _btnViewLargeIcons;
    private KryptonButton? _btnViewSmallIcons;
    private KryptonTextBox? _addressBar;
    private KryptonSearchBox? _searchBox;
    private KryptonLabel? _statusLabel;

    private readonly ExplorerBrowserValues _explorerValues;
    private readonly NavigationValues _navigationValues;
    private readonly DisplayValues _displayValues;

    private readonly Stack<string> _backHistory;
    private readonly Stack<string> _forwardHistory;
    internal string _currentPath = string.Empty;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the current path changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the current path changes.")]
    public event EventHandler? PathChanged;

    /// <summary>
    /// Occurs when the file selection changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the file selection changes.")]
    public event EventHandler? SelectionChanged;

    /// <summary>
    /// Occurs when a file system error occurs.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a file system error occurs.")]
    public event EventHandler<FileSystemErrorEventArgs>? FileSystemError;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonExplorerBrowser class.
    /// </summary>
    public KryptonExplorerBrowser()
    {
        _backHistory = new Stack<string>();
        _forwardHistory = new Stack<string>();
        
        _explorerValues = new ExplorerBrowserValues(this);
        _navigationValues = new NavigationValues(this);
        _displayValues = new DisplayValues(this);

        InitializeComponent();
        SetupControls();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets access to the explorer browser values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Groups file system explorer browser properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ExplorerBrowserValues ExplorerValues => _explorerValues;

    /// <summary>
    /// Gets access to the navigation values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Groups navigation-related properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public NavigationValues NavigationValues => _navigationValues;

    /// <summary>
    /// Gets access to the display values.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Groups display-related properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public DisplayValues DisplayValues => _displayValues;

    /// <summary>
    /// Gets the tree view control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFileSystemTreeView? TreeView => _treeView;

    /*/// <summary>
    /// Gets the list view control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFileSystemListView? ListView => _listView;

    /// <summary>
    /// Gets the currently selected file path.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath => _listView?.SelectedPath;

    /// <summary>
    /// Gets an array of selected file paths.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SelectedPaths => _listView?.SelectedPaths ?? Array.Empty<string>();*/

    #endregion

    #region Public Methods

    /// <summary>
    /// Navigates to the specified path.
    /// </summary>
    /// <param name="path">The path to navigate to.</param>
    public void NavigateTo(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        if (!Directory.Exists(path))
        {
            OnFileSystemError(new FileSystemErrorEventArgs(path, new DirectoryNotFoundException($"Directory not found: {path}")));
            return;
        }

        // Add current path to back history
        if (!string.IsNullOrEmpty(_currentPath) && _currentPath != path)
        {
            _backHistory.Push(_currentPath);
            _forwardHistory.Clear();
            UpdateNavigationButtons();
        }

        _currentPath = path;
        //_navigationValues._currentPath = path;

        // Update tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = path;
            _treeView.NavigateToPath(path);
        }

        // Update list view
        //_listView?.CurrentPath = path;

        // Update address bar
        _addressBar?.Text = path;

        // Update status
        UpdateStatus();

        OnPathChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Navigates back in history.
    /// </summary>
    public void NavigateBack()
    {
        if (_backHistory.Count > 0)
        {
            string path = _backHistory.Pop();
            _forwardHistory.Push(_currentPath);
            NavigateTo(path);
        }
    }

    /// <summary>
    /// Navigates forward in history.
    /// </summary>
    public void NavigateForward()
    {
        if (_forwardHistory.Count > 0)
        {
            string path = _forwardHistory.Pop();
            _backHistory.Push(_currentPath);
            NavigateTo(path);
        }
    }

    /// <summary>
    /// Navigates up one directory level.
    /// </summary>
    public void NavigateUp()
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            return;
        }

        DirectoryInfo? parent = Directory.GetParent(_currentPath);
        if (parent != null)
        {
            NavigateTo(parent.FullName);
        }
    }

    /// <summary>
    /// Refreshes the current view.
    /// </summary>
    public new void Refresh()
    {
        _treeView?.Reload();
        /*if (_listView != null)
        {
            _listView.Refresh();
        }*/
        UpdateStatus();
        base.Refresh();
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Raises the PathChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPathChanged(EventArgs e)
    {
        PathChanged?.Invoke(this, e);
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
    /// Raises the FileSystemError event.
    /// </summary>
    /// <param name="e">A FileSystemErrorEventArgs containing the event data.</param>
    protected virtual void OnFileSystemError(FileSystemErrorEventArgs e)
    {
        FileSystemError?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        _splitContainer?.Enabled = Enabled;
        _treeView?.Enabled = Enabled;
        //_listView?.Enabled = Enabled;
        _toolbarPanel?.Enabled = Enabled;
        _statusPanel?.Enabled = Enabled;
    }

    #endregion

    #region Implementation

    private void InitializeComponent()
    {
        SuspendLayout();

        // Create toolbar panel
        _toolbarPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 60,
            Padding = new Padding(4)
        };

        // Create navigation buttons
        _btnBack = new KryptonButton
        {
            Text = "←",
            Size = new Size(32, 32),
            Location = new Point(4, 4)
        };
        _btnBack.Click += BtnBack_Click;

        _btnForward = new KryptonButton
        {
            Text = "→",
            Size = new Size(32, 32),
            Location = new Point(40, 4)
        };
        _btnForward.Click += BtnForward_Click;

        _btnUp = new KryptonButton
        {
            Text = "↑",
            Size = new Size(32, 32),
            Location = new Point(76, 4)
        };
        _btnUp.Click += BtnUp_Click;

        _btnRefresh = new KryptonButton
        {
            Text = "↻",
            Size = new Size(32, 32),
            Location = new Point(112, 4)
        };
        _btnRefresh.Click += BtnRefresh_Click;

        // Create view buttons
        _btnViewDetails = new KryptonButton
        {
            Text = "Details",
            Size = new Size(60, 32),
            Location = new Point(152, 4)
        };
        _btnViewDetails.Click += BtnViewDetails_Click;

        _btnViewList = new KryptonButton
        {
            Text = "List",
            Size = new Size(60, 32),
            Location = new Point(216, 4)
        };
        _btnViewList.Click += BtnViewList_Click;

        _btnViewLargeIcons = new KryptonButton
        {
            Text = "Large",
            Size = new Size(60, 32),
            Location = new Point(280, 4)
        };
        _btnViewLargeIcons.Click += BtnViewLargeIcons_Click;

        _btnViewSmallIcons = new KryptonButton
        {
            Text = "Small",
            Size = new Size(60, 32),
            Location = new Point(344, 4)
        };
        _btnViewSmallIcons.Click += BtnViewSmallIcons_Click;

        // Create address bar
        _addressBar = new KryptonTextBox
        {
            Location = new Point(4, 40),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Width = _toolbarPanel.Width - 8
        };
        _addressBar.KeyDown += AddressBar_KeyDown;

        // Create search box
        _searchBox = new KryptonSearchBox
        {
            Location = new Point(412, 4),
            Size = new Size(200, 32),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };

        // Add controls to toolbar
        _toolbarPanel.Controls.Add(_btnBack);
        _toolbarPanel.Controls.Add(_btnForward);
        _toolbarPanel.Controls.Add(_btnUp);
        _toolbarPanel.Controls.Add(_btnRefresh);
        _toolbarPanel.Controls.Add(_btnViewDetails);
        _toolbarPanel.Controls.Add(_btnViewList);
        _toolbarPanel.Controls.Add(_btnViewLargeIcons);
        _toolbarPanel.Controls.Add(_btnViewSmallIcons);
        _toolbarPanel.Controls.Add(_addressBar);
        _toolbarPanel.Controls.Add(_searchBox);

        // Create content panel
        _contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };

        // Create split container
        _splitContainer = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterDistance = 200,
            FixedPanel = FixedPanel.Panel1,
            Panel1MinSize = 100,
            Panel2MinSize = 100
        };

        // Get panel references
        _treeViewPanel = _splitContainer.Panel1;
        _listViewPanel = _splitContainer.Panel2;

        // Create tree view
        _treeView = new KryptonFileSystemTreeView
        {
            Dock = DockStyle.Fill
        };
        _treeView.AfterSelect += TreeView_AfterSelect;
        _treeView.FileSystemError += TreeView_FileSystemError;

        // Create list view
        /*_listView = new KryptonFileSystemListView
        {
            Dock = DockStyle.Fill,
            View = View.Details
        };
        _listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
        _listView.DoubleClick += ListView_DoubleClick;
        _listView.PathChanged += ListView_PathChanged;
        _listView.FileSystemError += ListView_FileSystemError;*/

        // Add controls to panels
        _treeViewPanel.Controls.Add(_treeView);
        //_listViewPanel.Controls.Add(_listView);

        // Add split container to content panel
        _contentPanel.Controls.Add(_splitContainer);

        // Create status panel
        _statusPanel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = 24,
            Padding = new Padding(4, 2, 4, 2)
        };

        _statusLabel = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = "Ready"
        };
        _statusPanel.Controls.Add(_statusLabel);

        // Add panels to main control
        Controls.Add(_contentPanel);
        Controls.Add(_toolbarPanel);
        Controls.Add(_statusPanel);

        ResumeLayout(false);
    }

    private void SetupControls()
    {
        // Set initial path
        if (string.IsNullOrEmpty(_currentPath))
        {
            _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //_navigationValues._currentPath = _currentPath;
        }

        // Configure tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = _currentPath;
            _treeView.FileSystemTreeViewValues.ShowFiles = _explorerValues.ShowFiles;
            _treeView.FileSystemTreeViewValues.ShowHiddenFiles = _explorerValues.ShowHiddenFiles;
            _treeView.FileSystemTreeViewValues.ShowSystemFiles = _explorerValues.ShowSystemFiles;
            _treeView.FileSystemTreeViewValues.FileFilter = _explorerValues.FileFilter;
        }

        // Configure list view
        /*if (_listView != null)
        {
            _listView.View = _displayValues.ViewMode;
            _listView.MultiSelect = _displayValues.SelectionMode != SelectionMode.One;
            _listView.FileSystemListViewValues.ShowFiles = _explorerValues.ShowFiles;
            _listView.FileSystemListViewValues.ShowHiddenFiles = _explorerValues.ShowHiddenFiles;
            _listView.FileSystemListViewValues.ShowSystemFiles = _explorerValues.ShowSystemFiles;
            _listView.FileSystemListViewValues.FileFilter = _explorerValues.FileFilter;
            _listView.CurrentPath = _currentPath;
        }*/

        // Configure UI visibility
        _toolbarPanel?.Visible = _displayValues.ShowToolbar;
        _statusPanel?.Visible = _displayValues.ShowStatusBar;
        _splitContainer?.Panel1Collapsed = !_displayValues.ShowTreeView;

        // Update address bar
        _addressBar?.Text = _currentPath;

        // Update navigation buttons
        UpdateNavigationButtons();
        UpdateStatus();
    }

    private void UpdateNavigationButtons()
    {
        _btnBack?.Enabled = _backHistory.Count > 0;
        _btnForward?.Enabled = _forwardHistory.Count > 0;
        _btnUp?.Enabled = !string.IsNullOrEmpty(_currentPath) && Directory.GetParent(_currentPath) != null;
    }

    private void UpdateStatus()
    {
        if (_statusLabel == null || string.IsNullOrEmpty(_currentPath))
        {
            return;
        }

        try
        {
            if (Directory.Exists(_currentPath))
            {
                string[] files = Directory.GetFiles(_currentPath);
                string[] dirs = Directory.GetDirectories(_currentPath);
                _statusLabel.Text = $"{dirs.Length} folder(s), {files.Length} file(s)";
            }
            else
            {
                _statusLabel.Text = "Ready";
            }
        }
        catch
        {
            _statusLabel.Text = "Ready";
        }
    }

    private void BtnBack_Click(object? sender, EventArgs e)
    {
        NavigateBack();
    }

    private void BtnForward_Click(object? sender, EventArgs e)
    {
        NavigateForward();
    }

    private void BtnUp_Click(object? sender, EventArgs e)
    {
        NavigateUp();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        Refresh();
    }

    private void BtnViewDetails_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.Details;
    }

    private void BtnViewList_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.List;
    }

    private void BtnViewLargeIcons_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.LargeIcon;
    }

    private void BtnViewSmallIcons_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.SmallIcon;
    }

    private void AddressBar_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && _addressBar != null)
        {
            NavigateTo(_addressBar.Text);
        }
    }

    private void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            NavigateTo(path);
        }
    }

    private void TreeView_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        OnFileSystemError(e);
    }

    private void ListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        OnSelectionChanged(EventArgs.Empty);
    }

    private void ListView_DoubleClick(object? sender, EventArgs e)
    {
        /*if (_listView?.SelectedItems.Count > 0)
        {
            string? path = _listView.SelectedItems[0].Tag as string;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                NavigateTo(path);
            }
        }*/
    }

    private void ListView_PathChanged(object? sender, EventArgs e)
    {
        /*if (_listView is KryptonFileSystemListView fsListView)
        {
            string newPath = fsListView.CurrentPath;
            if (_currentPath != newPath)
            {
                NavigateTo(newPath);
            }
        }*/
    }

    private void ListView_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        OnFileSystemError(e);
    }

    #endregion
}

/// <summary>
/// Groups explorer browser specific properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ExplorerBrowserValues : Storage
{
    #region Instance Fields

    private bool _showFiles = true;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;
    private string _fileFilter = "*.*";

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ExplorerBrowserValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal ExplorerBrowserValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets or sets a value indicating whether files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files are displayed.")]
    [DefaultValue(true)]
    public bool ShowFiles
    {
        get => _showFiles;
        set
        {
            if (_showFiles != value)
            {
                _showFiles = value;
                UpdateControls();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether hidden files are displayed.")]
    [DefaultValue(false)]
    public bool ShowHiddenFiles
    {
        get => _showHiddenFiles;
        set
        {
            if (_showHiddenFiles != value)
            {
                _showHiddenFiles = value;
                UpdateControls();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether system files are displayed.")]
    [DefaultValue(false)]
    public bool ShowSystemFiles
    {
        get => _showSystemFiles;
        set
        {
            if (_showSystemFiles != value)
            {
                _showSystemFiles = value;
                UpdateControls();
            }
        }
    }

    /// <summary>
    /// Gets or sets the file filter pattern (e.g., "*.txt" or "*.txt;*.doc").
    /// </summary>
    [Category(@"Behavior")]
    [Description("The file filter pattern (e.g., \"*.txt\" or \"*.txt;*.doc\").")]
    [DefaultValue("*.*")]
    public string FileFilter
    {
        get => _fileFilter;
        set
        {
            if (_fileFilter != value)
            {
                _fileFilter = value ?? "*.*";
                UpdateControls();
            }
        }
    }

    public override bool IsDefault => throw new NotImplementedException();

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => "Explorer Browser Values";

    private void UpdateControls()
    {
        /*if (_owner._treeView != null)
        {
            _owner._treeView.FileSystemTreeViewValues.ShowFiles = _showFiles;
            _owner._treeView.FileSystemTreeViewValues.ShowHiddenFiles = _showHiddenFiles;
            _owner._treeView.FileSystemTreeViewValues.ShowSystemFiles = _showSystemFiles;
            _owner._treeView.FileSystemTreeViewValues.FileFilter = _fileFilter;
        }
        if (_owner._listView != null)
        {
            _owner._listView.FileSystemListViewValues.ShowFiles = _showFiles;
            _owner._listView.FileSystemListViewValues.ShowHiddenFiles = _showHiddenFiles;
            _owner._listView.FileSystemListViewValues.ShowSystemFiles = _showSystemFiles;
            _owner._listView.FileSystemListViewValues.FileFilter = _fileFilter;
        }*/
        _owner.Refresh();
    }
}

/// <summary>
/// Groups navigation-related properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NavigationValues : Storage
{
    #region Instance Fields

    private string _currentPath = string.Empty;
    private int _maxHistorySize = 50;

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal NavigationValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets or sets the current directory path.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The current directory path being displayed.")]
    [DefaultValue("")]
    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            if (_currentPath != value)
            {
                _currentPath = value ?? string.Empty;
                _owner.NavigateTo(_currentPath);
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of items in navigation history.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of items in navigation history.")]
    [DefaultValue(50)]
    public int MaxHistorySize
    {
        get => _maxHistorySize;
        set
        {
            if (_maxHistorySize != value && value > 0)
            {
                _maxHistorySize = value;
            }
        }
    }

    public override bool IsDefault => throw new NotImplementedException();

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => "Navigation Values";
}

/// <summary>
/// Groups display-related properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DisplayValues : Storage
{
    #region Instance Fields

    private View _viewMode = View.Details;
    private SelectionMode _selectionMode = SelectionMode.One;
    private bool _showToolbar = true;
    private bool _showStatusBar = true;
    private bool _showTreeView = true;

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal DisplayValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets or sets the view mode for the list view.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The view mode for the list view.")]
    [DefaultValue(View.Details)]
    public View ViewMode
    {
        get => _viewMode;
        set
        {
            if (_viewMode != value)
            {
                _viewMode = value;
                /*if (_owner._listView != null)
                {
                    _owner._listView.View = value;
                }*/
            }
        }
    }

    /// <summary>
    /// Gets or sets the selection mode for the list view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The selection mode for the list view.")]
    [DefaultValue(SelectionMode.One)]
    public SelectionMode SelectionMode
    {
        get => _selectionMode;
        set
        {
            if (_selectionMode != value)
            {
                _selectionMode = value;
                /*if (_owner._listView != null)
                {
                    _owner._listView.MultiSelect = value != SelectionMode.One;
                }*/
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the toolbar is visible.")]
    [DefaultValue(true)]
    public bool ShowToolbar
    {
        get => _showToolbar;
        set
        {
            if (_showToolbar != value)
            {
                _showToolbar = value;
                /*if (_owner._toolbarPanel != null)
                {
                    _owner._toolbarPanel.Visible = value;
                }*/
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the status bar is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the status bar is visible.")]
    [DefaultValue(true)]
    public bool ShowStatusBar
    {
        get => _showStatusBar;
        set
        {
            if (_showStatusBar != value)
            {
                _showStatusBar = value;
                /*if (_owner._statusPanel != null)
                {
                    _owner._statusPanel.Visible = value;
                }*/
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tree view is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the tree view is visible.")]
    [DefaultValue(true)]
    public bool ShowTreeView
    {
        get => _showTreeView;
        set
        {
            if (_showTreeView != value)
            {
                _showTreeView = value;
                /*if (_owner._splitContainer != null)
                {
                    _owner._splitContainer.Panel1Collapsed = !value;
                }*/
            }
        }
    }

    public override bool IsDefault => throw new NotImplementedException();

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => "Display Values";
}

