#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.IO.Compression;
using System.Text;
using Krypton.Toolkit;
using Krypton.Utilities;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonFileCopyDialog and KryptonFileCompressionDialog
/// with configurable options, path selection, and various operation modes.
/// </summary>
public partial class FileOperationDialogsDemo : KryptonForm
{
    private string? _demoSourcePath;
    private string? _demoDestPath;
    private readonly KryptonErrorProvider _errorProvider = new();

    public FileOperationDialogsDemo()
    {
        InitializeComponent();
        _errorProvider.ContainerControl = this;
        _errorProvider.BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError;
        SetDefaultPaths();
    }

    private void SetDefaultPaths()
    {
        var temp = Path.GetTempPath();
        ktbCopySource.Text = Path.Combine(temp, "KryptonFileCopyDemo_Source");
        ktbCopyDest.Text = Path.Combine(temp, "KryptonFileCopyDemo_Dest");
        ktbCompressSource.Text = Path.Combine(temp, "KryptonCompressDemo_Source");
        ktbCompressDest.Text = Path.Combine(temp, "KryptonCompressDemo_Archive.zip");
    }

    private void BtnCreateDemoData_Click(object? sender, EventArgs e)
    {
        _demoSourcePath = ktbCopySource.Text?.Trim();
        _demoDestPath = ktbCompressDest.Text?.Trim();

        if (string.IsNullOrEmpty(_demoSourcePath))
        {
            _errorProvider.SetError(ktbCopySource, "Enter or select a source path.");
            return;
        }

        _errorProvider.SetError(ktbCopySource, string.Empty);

        try
        {
            if (Directory.Exists(_demoSourcePath))
            {
                try { Directory.Delete(_demoSourcePath, true); } catch { }
            }

            Directory.CreateDirectory(_demoSourcePath);

            // Create a few files and a subfolder
            for (int i = 1; i <= 5; i++)
            {
                var filePath = Path.Combine(_demoSourcePath, $"DemoFile{i}.txt");
                File.WriteAllText(filePath, $"Demo content for file {i}\n" + new string('X', 500), Encoding.UTF8);
            }

            var subDir = Path.Combine(_demoSourcePath, "SubFolder");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(subDir, "NestedFile.txt"), "Nested file content", Encoding.UTF8);

            ktbCompressSource.Text = _demoSourcePath;
            var dirName = Path.GetFileName(_demoSourcePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            ktbCompressDest.Text = Path.Combine(Path.GetDirectoryName(_demoSourcePath) ?? Path.GetTempPath(), $"{dirName}.zip");

            MessageBox.Show(
                $"Demo data created at:\n{_demoSourcePath}\n\n7 files in total (5 in root + 1 in SubFolder).\n\nCompression source and destination have been updated.",
                "Demo Data Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to create demo data:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnBrowseCopySource_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonFolderBrowserDialog
        {
            Description = "Select source folder (or use Open File for single file)",
            SelectedPath = ktbCopySource.Text,
            ShowNewFolderButton = true
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCopySource.Text = dlg.SelectedPath;
            _errorProvider.SetError(ktbCopySource, string.Empty);
        }
    }

    private void BtnBrowseCopySourceFile_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonOpenFileDialog
        {
            Title = "Select source file",
            CheckFileExists = true
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCopySource.Text = dlg.FileName;
            _errorProvider.SetError(ktbCopySource, string.Empty);
        }
    }

    private void BtnBrowseCopyDest_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonFolderBrowserDialog
        {
            Description = "Select destination folder",
            SelectedPath = ktbCopyDest.Text,
            ShowNewFolderButton = true
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCopyDest.Text = dlg.SelectedPath;
            _errorProvider.SetError(ktbCopyDest, string.Empty);
        }
    }

    private void BtnCopyWithUI_Click(object? sender, EventArgs e)
    {
        if (!ValidateCopyPaths(out var source, out var dest)) return;

        using var dialog = new KryptonFileCopyDialog
        {
            SourcePath = source,
            DestinationPath = dest,
            ShowUI = true,
            OverwritePrompt = kchkCopyOverwritePrompt.Checked
        };

        var result = dialog.ShowDialog(this);
        ShowResult("Copy", result);
    }

    private void BtnCopySilent_Click(object? sender, EventArgs e)
    {
        if (!ValidateCopyPaths(out var source, out var dest)) return;

        using var dialog = new KryptonFileCopyDialog
        {
            SourcePath = source,
            DestinationPath = dest,
            ShowUI = false,
            OverwritePrompt = false
        };

        try
        {
            var result = dialog.ShowDialog(this);
            ShowResult("Copy (silent)", result);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Copy failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnBrowseCompressSource_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonFolderBrowserDialog
        {
            Description = "Select folder to compress",
            SelectedPath = ktbCompressSource.Text,
            ShowNewFolderButton = true
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCompressSource.Text = dlg.SelectedPath;
            _errorProvider.SetError(ktbCompressSource, string.Empty);
        }
    }

    private void BtnBrowseCompressSourceFile_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonOpenFileDialog
        {
            Title = "Select file to compress",
            CheckFileExists = true
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCompressSource.Text = dlg.FileName;
            _errorProvider.SetError(ktbCompressSource, string.Empty);
        }
    }

    private void BtnBrowseCompressDest_Click(object? sender, EventArgs e)
    {
        using var dlg = new KryptonSaveFileDialog
        {
            Title = "Save ZIP archive as",
            Filter = "ZIP files (*.zip)|*.zip|All files (*.*)|*.*",
            FileName = Path.GetFileName(ktbCompressDest.Text),
            DefaultExt = "zip"
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            ktbCompressDest.Text = dlg.FileName;
            _errorProvider.SetError(ktbCompressDest, string.Empty);
        }
    }

    private void BtnCompressWithUI_Click(object? sender, EventArgs e)
    {
        if (!ValidateCompressPaths(out var source, out var dest)) return;

        var level = GetSelectedCompressionLevel();
        using var dialog = new KryptonFileCompressionDialog
        {
            SourcePath = source,
            DestinationPath = dest,
            ShowUI = true,
            CompressionLevel = level,
            IncludeBaseDirectory = kchkIncludeBaseDir.Checked
        };

        var result = dialog.ShowDialog(this);
        ShowResult("Compression", result);
    }

    private void BtnCompressSilent_Click(object? sender, EventArgs e)
    {
        if (!ValidateCompressPaths(out var source, out var dest)) return;

        var level = GetSelectedCompressionLevel();
        using var dialog = new KryptonFileCompressionDialog
        {
            SourcePath = source,
            DestinationPath = dest,
            ShowUI = false,
            CompressionLevel = level,
            IncludeBaseDirectory = kchkIncludeBaseDir.Checked
        };

        try
        {
            var result = dialog.ShowDialog(this);
            ShowResult("Compression (silent)", result);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Compression failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private CompressionLevel GetSelectedCompressionLevel()
    {
        if (krbCompressFastest.Checked) return CompressionLevel.Fastest;
        if (krbCompressNone.Checked) return CompressionLevel.NoCompression;
        return CompressionLevel.Optimal;
    }

    private bool ValidateCopyPaths(out string source, out string dest)
    {
        source = ktbCopySource.Text?.Trim() ?? string.Empty;
        dest = ktbCopyDest.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(source))
        {
            _errorProvider.SetError(ktbCopySource, "Source path is required.");
            return false;
        }
        _errorProvider.SetError(ktbCopySource, string.Empty);

        if (string.IsNullOrEmpty(dest))
        {
            _errorProvider.SetError(ktbCopyDest, "Destination path is required.");
            return false;
        }
        _errorProvider.SetError(ktbCopyDest, string.Empty);

        if (!File.Exists(source) && !Directory.Exists(source))
        {
            _errorProvider.SetError(ktbCopySource, "Source path does not exist. Create demo data first or choose an existing path.");
            return false;
        }
        _errorProvider.SetError(ktbCopySource, string.Empty);

        return true;
    }

    private bool ValidateCompressPaths(out string source, out string dest)
    {
        source = ktbCompressSource.Text?.Trim() ?? string.Empty;
        dest = ktbCompressDest.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(source))
        {
            _errorProvider.SetError(ktbCompressSource, "Source path is required.");
            return false;
        }
        _errorProvider.SetError(ktbCompressSource, string.Empty);

        if (string.IsNullOrEmpty(dest))
        {
            _errorProvider.SetError(ktbCompressDest, "Destination ZIP path is required.");
            return false;
        }
        _errorProvider.SetError(ktbCompressDest, string.Empty);

        if (!File.Exists(source) && !Directory.Exists(source))
        {
            _errorProvider.SetError(ktbCompressSource, "Source path does not exist. Create demo data first or choose an existing path.");
            return false;
        }
        _errorProvider.SetError(ktbCompressSource, string.Empty);

        return true;
    }

    private static void ShowResult(string operation, DialogResult result)
    {
        if (result == DialogResult.OK)
            MessageBox.Show($"{operation} completed successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show($"{operation} was cancelled or did not complete.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _errorProvider?.Clear();
        _errorProvider?.Dispose();
        base.OnFormClosed(e);
    }
}
