#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

using System.IO;
using System.Windows.Forms;

public partial class BrowserControlDemo : KryptonForm
{
    public BrowserControlDemo()
    {
        InitializeComponent();
        SetupControls();
        SetupEvents();
    }

    private void SetupControls()
    {
        // Set initial path
        string initialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        kryptonBrowserControl1.CurrentPath = initialPath;
        kryptonTextBoxCurrentPath.Text = initialPath;

        // Setup view mode combo
        kryptonComboBoxViewMode.Items.AddRange(new[] { "Large Icons", "Small Icons", "List", "Details", "Tiles" });
        kryptonComboBoxViewMode.SelectedIndex = 3; // Details

        // Setup file filter combo
        kryptonComboBoxFileFilter.Items.AddRange(new[]
        {
            "All Files (*.*)",
            "Text Files (*.txt)",
            "Image Files (*.jpg;*.png;*.gif;*.bmp)",
            "Document Files (*.doc;*.docx;*.pdf)",
            "Code Files (*.cs;*.vb;*.js;*.html)"
        });
        kryptonComboBoxFileFilter.SelectedIndex = 0;

        // Setup selection mode combo
        kryptonComboBoxSelectionMode.Items.AddRange(new[] { "Single", "Multi Extended" });
        kryptonComboBoxSelectionMode.SelectedIndex = 0;

        UpdateStatusLabel();
    }

    private void SetupEvents()
    {
        // Browser control events
        kryptonBrowserControl1.PathChanged += BrowserControl_PathChanged;
        kryptonBrowserControl1.SelectionChanged += BrowserControl_SelectionChanged;
        kryptonBrowserControl1.FileSystemError += BrowserControl_FileSystemError;

        // Control events
        kryptonButtonNavigate.Click += ButtonNavigate_Click;
        kryptonButtonRefresh.Click += ButtonRefresh_Click;
        kryptonButtonOpenDialog.Click += ButtonOpenDialog_Click;
        kryptonButtonSaveDialog.Click += ButtonSaveDialog_Click;
        kryptonButtonMultiSelectDialog.Click += ButtonMultiSelectDialog_Click;
        kryptonCheckBoxShowFiles.CheckedChanged += CheckBoxShowFiles_CheckedChanged;
        kryptonCheckBoxShowHiddenFiles.CheckedChanged += CheckBoxShowHiddenFiles_CheckedChanged;
        kryptonCheckBoxShowSystemFiles.CheckedChanged += CheckBoxShowSystemFiles_CheckedChanged;
        kryptonCheckBoxShowTreeView.CheckedChanged += CheckBoxShowTreeView_CheckedChanged;
        kryptonComboBoxViewMode.SelectedIndexChanged += ComboBoxViewMode_SelectedIndexChanged;
        kryptonComboBoxFileFilter.SelectedIndexChanged += ComboBoxFileFilter_SelectedIndexChanged;
        kryptonComboBoxSelectionMode.SelectedIndexChanged += ComboBoxSelectionMode_SelectedIndexChanged;
        kryptonTextBoxCurrentPath.KeyDown += TextBoxCurrentPath_KeyDown;
    }

    private void BrowserControl_PathChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null)
        {
            kryptonTextBoxCurrentPath.Text = kryptonBrowserControl1.CurrentPath;
            UpdateStatusLabel();
        }
    }

    private void BrowserControl_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateSelectedFilesList();
        UpdateStatusLabel();
    }

    private void BrowserControl_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        KryptonMessageBox.Show(
            $"File system error occurred:\n\nPath: {e.Path}\nError: {e.Exception.Message}",
            "File System Error",
            KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Error);
    }

    private void ButtonNavigate_Click(object? sender, EventArgs e)
    {
        string path = kryptonTextBoxCurrentPath.Text.Trim();
        if (!string.IsNullOrEmpty(path))
        {
            if (Directory.Exists(path))
            {
                kryptonBrowserControl1.NavigateTo(path);
            }
            else if (File.Exists(path))
            {
                string? directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory))
                {
                    kryptonBrowserControl1.NavigateTo(directory);
                }
            }
            else
            {
                KryptonMessageBox.Show(
                    $"The path does not exist:\n{path}",
                    "Path Not Found",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Warning);
            }
        }
    }

    private void ButtonRefresh_Click(object? sender, EventArgs e)
    {
        kryptonBrowserControl1.Refresh();
        UpdateStatusLabel();
    }

    private void ButtonOpenDialog_Click(object? sender, EventArgs e)
    {
        using KryptonCommonFileDialog dialog = new(FileDialogType.Open)
        {
            Title = "Open File - Krypton Common File Dialog Demo",
            InitialDirectory = kryptonBrowserControl1.CurrentPath,
            Filter = "All Files (*.*)|*.*|Text Files (*.txt)|*.txt|Image Files (*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp",
            FilterIndex = 1,
            CheckFileExists = true,
            CheckPathExists = true
        };

        dialog.FileOk += (s, args) =>
        {
            if (!args.Cancel)
            {
                string? directory = Path.GetDirectoryName(dialog.FileName);
                if (!string.IsNullOrEmpty(directory))
                {
                    kryptonBrowserControl1.NavigateTo(directory);
                }
            }
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            KryptonMessageBox.Show(
                $"Selected file:\n{dialog.FileName}\n\nSafe file name:\n{dialog.SafeFileName}",
                "File Selected",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
    }

    private void ButtonSaveDialog_Click(object? sender, EventArgs e)
    {
        using KryptonCommonFileDialog dialog = new(FileDialogType.Save)
        {
            Title = "Save File - Krypton Common File Dialog Demo",
            InitialDirectory = kryptonBrowserControl1.CurrentPath,
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            FilterIndex = 1,
            DefaultExt = ".txt",
            AddExtension = true,
            CheckPathExists = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            KryptonMessageBox.Show(
                $"Save file:\n{dialog.FileName}\n\nSafe file name:\n{dialog.SafeFileName}",
                "File Selected for Save",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
    }

    private void ButtonMultiSelectDialog_Click(object? sender, EventArgs e)
    {
        using KryptonCommonFileDialog dialog = new(FileDialogType.Open)
        {
            Title = "Select Multiple Files - Krypton Common File Dialog Demo",
            InitialDirectory = kryptonBrowserControl1.CurrentPath,
            Filter = "All Files (*.*)|*.*|Text Files (*.txt)|*.txt|Image Files (*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp",
            FilterIndex = 1,
            Multiselect = true,
            CheckFileExists = true,
            CheckPathExists = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            string message = $"Selected {dialog.FileNames.Length} file(s):\n\n";
            foreach (string fileName in dialog.FileNames)
            {
                message += $"{Path.GetFileName(fileName)}\n";
            }

            KryptonMessageBox.Show(
                message,
                "Files Selected",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
    }

    private void CheckBoxShowFiles_CheckedChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null)
        {
            kryptonBrowserControl1.ShowFiles = kryptonCheckBoxShowFiles.Checked;
        }
    }

    private void CheckBoxShowHiddenFiles_CheckedChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null)
        {
            kryptonBrowserControl1.ShowHiddenFiles = kryptonCheckBoxShowHiddenFiles.Checked;
        }
    }

    private void CheckBoxShowSystemFiles_CheckedChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null)
        {
            kryptonBrowserControl1.ShowSystemFiles = kryptonCheckBoxShowSystemFiles.Checked;
        }
    }

    private void CheckBoxShowTreeView_CheckedChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null)
        {
            kryptonBrowserControl1.ShowTreeView = kryptonCheckBoxShowTreeView.Checked;
        }
    }

    private void ComboBoxViewMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null && kryptonComboBoxViewMode.SelectedIndex >= 0)
        {
            kryptonBrowserControl1.ViewMode = (View)kryptonComboBoxViewMode.SelectedIndex;
        }
    }

    private void ComboBoxFileFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 == null || kryptonComboBoxFileFilter.SelectedIndex < 0)
            return;

        string filter = kryptonComboBoxFileFilter.SelectedIndex switch
        {
            0 => "*.*",
            1 => "*.txt",
            2 => "*.jpg;*.png;*.gif;*.bmp",
            3 => "*.doc;*.docx;*.pdf",
            4 => "*.cs;*.vb;*.js;*.html",
            _ => "*.*"
        };

        kryptonBrowserControl1.FileFilter = filter;
    }

    private void ComboBoxSelectionMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kryptonBrowserControl1 != null && kryptonComboBoxSelectionMode.SelectedIndex >= 0)
        {
            kryptonBrowserControl1.SelectionMode = kryptonComboBoxSelectionMode.SelectedIndex == 0
                ? SelectionMode.One
                : SelectionMode.MultiExtended;
        }
    }

    private void TextBoxCurrentPath_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ButtonNavigate_Click(sender, e);
            e.Handled = true;
        }
    }

    private void UpdateStatusLabel()
    {
        if (kryptonBrowserControl1 == null)
            return;

        string path = kryptonBrowserControl1.CurrentPath;
        int selectedCount = kryptonBrowserControl1.SelectedPaths.Length;

        string status = $"Current Path: {path}";
        if (selectedCount > 0)
        {
            status += $" | Selected: {selectedCount} item(s)";
        }

        kryptonLabelStatus.Text = status;
    }

    private void UpdateSelectedFilesList()
    {
        kryptonListBoxSelectedFiles.Items.Clear();

        if (kryptonBrowserControl1 != null)
        {
            string[] selectedPaths = kryptonBrowserControl1.SelectedPaths;
            foreach (string path in selectedPaths)
            {
                kryptonListBoxSelectedFiles.Items.Add(path);
            }
        }
    }

    private void kryptonButtonClose_Click(object? sender, EventArgs e)
    {
        Close();
    }
}

