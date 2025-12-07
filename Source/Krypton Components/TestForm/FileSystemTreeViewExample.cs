#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

namespace TestForm;

public partial class FileSystemTreeViewExample : KryptonForm
{
    public FileSystemTreeViewExample()
    {
        InitializeComponent();

        // Initialize with common folders
        InitializeRootPaths();
        
        // Setup initial state
        kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        UpdateStatusLabel();

        // Hook up events
        kryptonFileSystemTreeView1.AfterSelect += OnTreeViewAfterSelect;
        kryptonFileSystemTreeView1.DirectoryExpanding += OnDirectoryExpanding;
        kryptonFileSystemTreeView1.DirectoryExpanded += OnDirectoryExpanded;
        kryptonFileSystemTreeView1.FileSystemError += OnFileSystemError;
    }

    private void InitializeRootPaths()
    {
        // Populate root path combo with common folders
        kryptonComboBoxRootPath.Items.Clear();
        
        // Add special folders
        kryptonComboBoxRootPath.Items.Add("My Documents");
        kryptonComboBoxRootPath.Items.Add("My Computer (C:\\)");
        kryptonComboBoxRootPath.Items.Add("Desktop");
        kryptonComboBoxRootPath.Items.Add("Program Files");
        kryptonComboBoxRootPath.Items.Add("Windows");
        kryptonComboBoxRootPath.Items.Add("Temp");
        
        // Add drive letters
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                string label = $"{drive.Name} ({drive.VolumeLabel})";
                kryptonComboBoxRootPath.Items.Add(label);
            }
        }

        kryptonComboBoxRootPath.SelectedIndex = 0;
    }

    private void OnTreeViewAfterSelect(object? sender, TreeViewEventArgs e)
    {
        UpdateStatusLabel();
        
        if (kryptonFileSystemTreeView1.SelectedPath != null)
        {
            kryptonTextBoxSelectedPath.Text = kryptonFileSystemTreeView1.SelectedPath;
            
            // Update file info if it's a file
            if (File.Exists(kryptonFileSystemTreeView1.SelectedPath))
            {
                var fileInfo = new FileInfo(kryptonFileSystemTreeView1.SelectedPath);
                kryptonLabelFileInfo.Text = $"Size: {fileInfo.Length:N0} bytes | Modified: {fileInfo.LastWriteTime}";
            }
            else if (Directory.Exists(kryptonFileSystemTreeView1.SelectedPath))
            {
                var dirInfo = new DirectoryInfo(kryptonFileSystemTreeView1.SelectedPath);
                int fileCount = 0;
                int dirCount = 0;
                
                try
                {
                    fileCount = dirInfo.GetFiles().Length;
                    dirCount = dirInfo.GetDirectories().Length;
                }
                catch
                {
                    // Ignore access errors
                }
                
                kryptonLabelFileInfo.Text = $"Files: {fileCount} | Folders: {dirCount} | Modified: {dirInfo.LastWriteTime}";
            }
            else
            {
                kryptonLabelFileInfo.Text = string.Empty;
            }
        }
        else
        {
            kryptonTextBoxSelectedPath.Clear();
            kryptonLabelFileInfo.Text = string.Empty;
        }
    }

    private void OnDirectoryExpanding(object? sender, DirectoryExpandingEventArgs e)
    {
        // Could show a loading indicator here
    }

    private void OnDirectoryExpanded(object? sender, DirectoryExpandedEventArgs e)
    {
        UpdateStatusLabel();
    }

    private void OnFileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        // Show error in status label
        kryptonLabelStatus.Text = $"Error: {e.Exception.Message}";
    }

    private void UpdateStatusLabel()
    {
        if (kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath != null && Directory.Exists(kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath))
        {
            kryptonLabelStatus.Text = $"Root: {kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath}";
        }
        else
        {
            kryptonLabelStatus.Text = "No root path set";
        }
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void kbtnBrowseRootPath_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonFolderBrowserDialog
        {
            Title = "Select the root folder for the file system tree view:",
            SelectedPath = kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath = dialog.SelectedPath;
            kryptonComboBoxRootPath.Text = dialog.SelectedPath;
            UpdateStatusLabel();
        }
    }

    private void kryptonComboBoxRootPath_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? selectedItem = kryptonComboBoxRootPath.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedItem))
        {
            return;
        }

        string path = selectedItem switch
        {
            "My Documents" => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "My Computer (C:\\)" => @"C:\",
            "Desktop" => Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "Program Files" => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Windows" => Environment.GetFolderPath(Environment.SpecialFolder.Windows),
            "Temp" => Path.GetTempPath(),
            _ => selectedItem.Length >= 3 && selectedItem[1] == ':' ? selectedItem.Substring(0, 3) : selectedItem
        };

        if (Directory.Exists(path))
        {
            kryptonFileSystemTreeView1.FileSystemTreeViewValues.RootPath = path;
            UpdateStatusLabel();
        }
    }

    private void kryptonCheckBoxShowFiles_CheckedChanged(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.FileSystemTreeViewValues.ShowFiles = kryptonCheckBoxShowFiles.Checked;
        kryptonTextBoxFileFilter.Enabled = kryptonCheckBoxShowFiles.Checked;
    }

    private void kryptonCheckBoxShowHidden_CheckedChanged(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.FileSystemTreeViewValues.ShowHiddenFiles = kryptonCheckBoxShowHidden.Checked;
    }

    private void kryptonCheckBoxShowSystem_CheckedChanged(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.FileSystemTreeViewValues.ShowSystemFiles = kryptonCheckBoxShowSystem.Checked;
    }

    private void kryptonTextBoxFileFilter_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(kryptonTextBoxFileFilter.Text))
        {
            kryptonFileSystemTreeView1.FileSystemTreeViewValues.FileFilter = kryptonTextBoxFileFilter.Text;
        }
    }

    private void kbtnReload_Click(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.Reload();
        UpdateStatusLabel();
    }

    private void kbtnRefreshSelected_Click(object sender, EventArgs e)
    {
        //kryptonFileSystemTreeView1.RefreshSelected();
    }

    private void kbtnNavigate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(kryptonTextBoxNavigate.Text))
        {
            return;
        }

        if (kryptonFileSystemTreeView1.NavigateToPath(kryptonTextBoxNavigate.Text))
        {
            kryptonLabelStatus.Text = $"Navigated to: {kryptonTextBoxNavigate.Text}";
        }
        else
        {
            KryptonMessageBox.Show(this, 
                $"Could not navigate to:\n{kryptonTextBoxNavigate.Text}\n\nPlease ensure the path exists.",
                "Navigation Failed",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Warning);
        }
    }

    private void kbtnExpandAll_Click(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.BeginUpdate();
        try
        {
            kryptonFileSystemTreeView1.ExpandAll();
        }
        finally
        {
            kryptonFileSystemTreeView1.EndUpdate();
        }
    }

    private void kbtnCollapseAll_Click(object sender, EventArgs e)
    {
        kryptonFileSystemTreeView1.CollapseAll();
    }
}

