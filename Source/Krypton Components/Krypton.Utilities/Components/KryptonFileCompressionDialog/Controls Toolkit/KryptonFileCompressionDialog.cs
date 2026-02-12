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
using System.IO.Compression;
using System.Windows.Forms;

namespace Krypton.Utilities;

/// <summary>
/// Provides a Krypton-styled file compression dialog with progress indication.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class KryptonFileCompressionDialog : Component
{
    #region Instance Fields

    private VisualFileCompressionDialogForm? _dialogForm;
    private string? _sourcePath;
    private string? _destinationPath;
    private bool _showUI = true;
    private CompressionLevel _compressionLevel = CompressionLevel.Optimal;
    private bool _includeBaseDirectory = false;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the source path (file or directory) to compress.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The source path (file or directory) to compress.")]
    public string? SourcePath
    {
        get => _sourcePath;
        set => _sourcePath = value;
    }

    /// <summary>
    /// Gets or sets the destination ZIP file path.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    [Description(@"The destination ZIP file path.")]
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
    /// Gets or sets the compression level to use.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(CompressionLevel.Optimal)]
    [Description(@"The compression level to use (Fastest, Optimal, or NoCompression).")]
    public CompressionLevel CompressionLevel
    {
        get => _compressionLevel;
        set => _compressionLevel = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include the base directory in the archive.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Indicates whether to include the base directory in the archive.")]
    public bool IncludeBaseDirectory
    {
        get => _includeBaseDirectory;
        set => _includeBaseDirectory = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the file compression dialog and performs the compression operation.
    /// </summary>
    /// <param name="owner">The owner window for the dialog.</param>
    /// <returns>The result of the compression operation.</returns>
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

        // Ensure destination has .zip extension
        if (!_destinationPath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            _destinationPath += ".zip";
        }

        if (!_showUI)
        {
            // Perform compression without UI
            try
            {
                if (File.Exists(_sourcePath))
                {
                    CompressFile(_sourcePath, _destinationPath, _compressionLevel);
                }
                else if (Directory.Exists(_sourcePath))
                {
                    ZipFile.CreateFromDirectory(_sourcePath, _destinationPath, _compressionLevel, _includeBaseDirectory);
                }
                return DialogResult.OK;
            }
            catch
            {
                return DialogResult.Cancel;
            }
        }

        _dialogForm = new VisualFileCompressionDialogForm(_sourcePath, _destinationPath, _compressionLevel, _includeBaseDirectory);
        return _dialogForm.ShowDialog(owner);
    }

    /// <summary>
    /// Compresses a single file into a ZIP archive.
    /// </summary>
    private static void CompressFile(string sourceFile, string destinationZip, CompressionLevel compressionLevel)
    {
        using (var archive = ZipFile.Open(destinationZip, ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile), compressionLevel);
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
