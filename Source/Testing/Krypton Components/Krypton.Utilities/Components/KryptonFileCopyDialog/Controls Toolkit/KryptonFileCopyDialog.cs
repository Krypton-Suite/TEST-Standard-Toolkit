#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Krypton.Utilities;

/// <summary>
/// Provides a Krypton-styled file copy dialog with progress indication.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class KryptonFileCopyDialog : Component
{
    #region Instance Fields

    private VisualFileCopyDialogForm? _dialogForm;
    private string? _sourcePath;
    private string? _destinationPath;
    private bool _showUI = true;
    private bool _overwritePrompt = true;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the source path (file or directory) to copy from.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The source path (file or directory) to copy from.")]
    public string? SourcePath
    {
        get => _sourcePath;
        set => _sourcePath = value;
    }

    /// <summary>
    /// Gets or sets the destination path (file or directory) to copy to.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The destination path (file or directory) to copy to.")]
    public string? DestinationPath
    {
        get => _destinationPath;
        set => _destinationPath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the UI dialog.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Indicates whether to show the progress dialog UI.")]
    public bool ShowUI
    {
        get => _showUI;
        set => _showUI = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to prompt before overwriting existing files.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Indicates whether to prompt before overwriting existing files.")]
    public bool OverwritePrompt
    {
        get => _overwritePrompt;
        set => _overwritePrompt = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the file copy dialog and performs the copy operation.
    /// </summary>
    /// <param name="owner">The owner window for the dialog.</param>
    /// <returns>The result of the copy operation.</returns>
    public DialogResult ShowDialog(IWin32Window? owner = null)
    {
        if (string.IsNullOrEmpty(_sourcePath))
        {
            throw new InvalidOperationException("SourcePath must be set before showing the dialog.");
        }

        if (string.IsNullOrEmpty(_destinationPath))
        {
            throw new InvalidOperationException("DestinationPath must be set before showing the dialog.");
        }

        if (!_showUI)
        {
            // Perform copy without UI
            try
            {
                if (File.Exists(_sourcePath))
                {
                    File.Copy(_sourcePath, _destinationPath, !_overwritePrompt);
                }
                else if (Directory.Exists(_sourcePath))
                {
                    CopyDirectory(_sourcePath, _destinationPath, !_overwritePrompt);
                }
                return DialogResult.OK;
            }
            catch
            {
                return DialogResult.Cancel;
            }
        }

        _dialogForm = new VisualFileCopyDialogForm(_sourcePath, _destinationPath, _overwritePrompt);
        return _dialogForm.ShowDialog(owner);
    }

    /// <summary>
    /// Copies a directory recursively.
    /// </summary>
    private static void CopyDirectory(string sourceDir, string destDir, bool overwrite)
    {
        var dir = new DirectoryInfo(sourceDir);
        var dirs = dir.GetDirectories();

        Directory.CreateDirectory(destDir);

        foreach (var file in dir.GetFiles())
        {
            var targetFilePath = Path.Combine(destDir, file.Name);
            file.CopyTo(targetFilePath, overwrite);
        }

        foreach (var subDir in dirs)
        {
            var newDestinationDir = Path.Combine(destDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, overwrite);
        }
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Releases the unmanaged resources used by the Component and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dialogForm?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}
