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
using System.IO;

/// <summary>
/// Provides a Krypton-styled TreeView control for browsing the file system.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(AfterSelect))]
[DefaultProperty(nameof(FileSystemTreeViewValues))]
[DesignerCategory(@"code")]
[Description(@"Displays a hierarchical file system tree with Krypton styling.")]
[Docking(DockingBehavior.Ask)]
public class KryptonFileSystemTreeView : KryptonTreeView
{
    #region Instance Fields

    private const string DUMMY_NODE_KEY = "__DUMMY__";
    private readonly FileSystemTreeViewValues _fileSystemValues;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a directory is being expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a directory is being expanded.")]
    public event EventHandler<DirectoryExpandingEventArgs>? DirectoryExpanding;

    /// <summary>
    /// Occurs when a directory has been expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a directory has been expanded.")]
    public event EventHandler<DirectoryExpandedEventArgs>? DirectoryExpanded;

    /// <summary>
    /// Occurs when an error occurs while loading the file system.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an error occurs while loading the file system.")]
    public event EventHandler<FileSystemErrorEventArgs>? FileSystemError;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonFileSystemTreeView class.
    /// </summary>
    public KryptonFileSystemTreeView()
    {
        // Create the expandable properties object
        _fileSystemValues = new FileSystemTreeViewValues(this);

        // Set default properties
        ShowPlusMinus = true;
        ShowRootLines = true;
        ShowLines = true;

        // Wire up events
        BeforeExpand += OnBeforeExpand;
        AfterSelect += OnAfterSelect;
        
        // Hide dummy nodes - minimal interference with Krypton rendering
        TreeView.DrawNode += OnDrawNode;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the file system TreeView values.
    /// </summary>
    /// <value>
    /// The file system TreeView values.
    /// </value>
    public FileSystemTreeViewValues FileSystemTreeViewValues => _fileSystemValues;

    /// <summary>
    /// Gets or sets the root directory path to display in the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The root directory path to display in the tree view.")]
    [DefaultValue("")]
    public string RootPath
    {
        get => _fileSystemValues.RootPath;
        set => _fileSystemValues.RootPath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether files should be displayed in the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files should be displayed in the tree view.")]
    [DefaultValue(true)]
    public bool ShowFiles
    {
        get => _fileSystemValues.ShowFiles;
        set => _fileSystemValues.ShowFiles = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether hidden files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowHiddenFiles
    {
        get => _fileSystemValues.ShowHiddenFiles;
        set => _fileSystemValues.ShowHiddenFiles = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether system files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowSystemFiles
    {
        get => _fileSystemValues.ShowSystemFiles;
        set => _fileSystemValues.ShowSystemFiles = value;
    }

    /// <summary>
    /// Gets or sets the file filter to apply when showing files (e.g., "*.txt" or "*.txt;*.doc").
    /// </summary>
    [Category(@"Behavior")]
    [Description("The file filter to apply when showing files (e.g., \"*.txt\" or \"*.txt;*.doc\").")]
    [DefaultValue("*.*")]
    public string FileFilter
    {
        get => _fileSystemValues.FileFilter;
        set => _fileSystemValues.FileFilter = value;
    }

    /// <summary>
    /// Gets the full path of the currently selected file or folder.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath => SelectedNode?.Tag as string;

    #endregion

    #region Public Methods

    /// <summary>
    /// Reloads the tree view from the root path.
    /// </summary>
    public void Reload()
    {
        Nodes.Clear();

        // If RootPath is not set or invalid, fall back to drives
        if (string.IsNullOrEmpty(_fileSystemValues.RootPath) || !Directory.Exists(_fileSystemValues.RootPath))
        {
            LoadDriveRoots();
            return;
        }

        try
        {
            TreeNode rootNode = CreateDirectoryNode(_fileSystemValues.RootPath);
            Nodes.Add(rootNode);
            rootNode.Expand();
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(_fileSystemValues.RootPath, ex));
        }
    }

    /// <summary>
    /// Navigates to the specified path in the tree view.
    /// </summary>
    /// <param name="path">The path to navigate to.</param>
    /// <returns>True if the path was found and selected; otherwise, false.</returns>
    public bool NavigateToPath(string path)
    {
        if (string.IsNullOrEmpty(path) || (!Directory.Exists(path) && !File.Exists(path)))
        {
            return false;
        }

        string[] pathParts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        TreeNode? currentNode = null;

        foreach (string part in pathParts)
        {
            if (string.IsNullOrEmpty(part))
            {
                continue;
            }

            TreeNodeCollection nodesToSearch = currentNode?.Nodes ?? Nodes;
            TreeNode? nextNode = null;

            foreach (TreeNode node in nodesToSearch)
            {
                if (node.Text.Equals(part, StringComparison.OrdinalIgnoreCase))
                {
                    nextNode = node;
                    break;
                }
            }

            if (nextNode == null)
            {
                break;
            }

            currentNode = nextNode;
        }

        if (currentNode != null)
        {
            SelectedNode = currentNode;
            currentNode.EnsureVisible();
            return true;
        }

        return false;
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Ensure initial population of the tree when the control is created.
    /// </summary>
    /// <param name="e">Event args.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (!DesignMode)
        {
            Reload();
        }
    }

    /// <summary>
    /// Raises the DirectoryExpanding event.
    /// </summary>
    /// <param name="e">A DirectoryExpandingEventArgs containing the event data.</param>
    protected virtual void OnDirectoryExpanding(DirectoryExpandingEventArgs e) => DirectoryExpanding?.Invoke(this, e);

    /// <summary>
    /// Raises the DirectoryExpanded event.
    /// </summary>
    /// <param name="e">A DirectoryExpandedEventArgs containing the event data.</param>
    protected virtual void OnDirectoryExpanded(DirectoryExpandedEventArgs e) => DirectoryExpanded?.Invoke(this, e);

    /// <summary>
    /// Raises the FileSystemError event.
    /// </summary>
    /// <param name="e">A FileSystemErrorEventArgs containing the event data.</param>
    protected virtual void OnFileSystemError(FileSystemErrorEventArgs e) => FileSystemError?.Invoke(this, e);

    #endregion

    #region Implementation

    private void OnDrawNode(object? sender, DrawTreeNodeEventArgs e)
    {
        // Hide dummy nodes only - let KryptonTreeView handle everything else
        if (e.Node?.Name == DUMMY_NODE_KEY)
        {
            e.DrawDefault = false;
        }
    }

    private TreeNode CreateDirectoryNode(string path)
    {
        string displayName = Path.GetFileName(path);
        if (string.IsNullOrEmpty(displayName))
        {
            displayName = path;
        }

        var node = new TreeNode(displayName)
        {
            Tag = path
        };

        // Add dummy node to enable expansion
        TreeNode dummyNode = new TreeNode(DUMMY_NODE_KEY) 
        { 
            Tag = null,
            Name = DUMMY_NODE_KEY,
            Text = string.Empty
        };
        node.Nodes.Add(dummyNode);

        return node;
    }

    private TreeNode CreateFileNode(string path)
    {
        string displayName = Path.GetFileName(path);
        var node = new TreeNode(displayName)
        {
            Tag = path
        };

        return node;
    }

    private void OnBeforeExpand(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            // Check if this node has a dummy child node
            if (e.Node.Nodes.Count == 1)
            {
                TreeNode firstChild = e.Node.Nodes[0];
                if (firstChild.Name == DUMMY_NODE_KEY)
                {
                    e.Node.Nodes.Clear();
                    
                    var expandingArgs = new DirectoryExpandingEventArgs(path);
                    OnDirectoryExpanding(expandingArgs);

                    if (!expandingArgs.Cancel)
                    {
                        LoadDirectoryNodes(e.Node, path);
                    }
                }
            }
        }
    }

    private void OnAfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is string path)
        {
            OnDirectoryExpanded(new DirectoryExpandedEventArgs(path));
        }
    }

    private void LoadDirectoryNodes(TreeNode parentNode, string directoryPath)
    {
        try
        {
            // Get directories
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (string dir in directories)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    bool isHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                    bool isSystem = (dirInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                    if ((isHidden && !_fileSystemValues.ShowHiddenFiles) || (isSystem && !_fileSystemValues.ShowSystemFiles))
                    {
                        continue;
                    }

                    TreeNode dirNode = CreateDirectoryNode(dir);
                    parentNode.Nodes.Add(dirNode);
                }
                catch (UnauthorizedAccessException ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(dir, ex));
                }
            }

            // Get files
            if (_fileSystemValues.ShowFiles)
            {
                string[] files = Directory.GetFiles(directoryPath, _fileSystemValues.FileFilter);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        bool isHidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        bool isSystem = (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                        if ((isHidden && !_fileSystemValues.ShowHiddenFiles) || (isSystem && !_fileSystemValues.ShowSystemFiles))
                        {
                            continue;
                        }

                        TreeNode fileNode = CreateFileNode(file);
                        parentNode.Nodes.Add(fileNode);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        OnFileSystemError(new FileSystemErrorEventArgs(file, ex));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(directoryPath, ex));
        }
    }

    private void LoadDriveRoots()
    {
        try
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady)
                {
                    continue;
                }

                string path = drive.RootDirectory.FullName;
                TreeNode driveNode = CreateDirectoryNode(path);
                driveNode.Text = $"{path} ({drive.DriveType})";
                Nodes.Add(driveNode);
            }
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(string.Empty, ex));
        }
    }

    #endregion
}

