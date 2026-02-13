#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Utilities;

/// <summary>
/// Visual form for the file compression dialog with progress indication.
/// </summary>
public partial class VisualFileCompressionDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly string _sourcePath;
    private readonly string _destinationPath;
    private readonly CompressionLevel _compressionLevel;
    private readonly bool _includeBaseDirectory;
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isPaused;
    private readonly List<CompressionItem> _fileList = new();
    private int _currentFileIndex;
    private long _totalBytes;
    private long _compressedBytes;
    private DateTime _startTime;
    private readonly Queue<double> _speedHistory = new();
    private const int MaxSpeedHistorySize = 50;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="VisualFileCompressionDialogForm"/> class.
    /// </summary>
    /// <param name="sourcePath">The source path to compress.</param>
    /// <param name="destinationPath">The destination ZIP file path.</param>
    /// <param name="compressionLevel">The compression level to use.</param>
    /// <param name="includeBaseDirectory">Whether to include the base directory in the archive.</param>
    public VisualFileCompressionDialogForm(string sourcePath, string destinationPath, CompressionLevel compressionLevel, bool includeBaseDirectory)
    {
        InitializeComponent();

        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
        _compressionLevel = compressionLevel;
        _includeBaseDirectory = includeBaseDirectory;

        SetupUI();
        Load += VisualFileCompressionDialogForm_Load;
    }

    #endregion

    #region Implementation

    private void SetupUI()
    {
        Text = "Compressing files...";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = true;

        kbtnPause.Text = "Pause";
        kbtnCancel.Text = "Cancel";
        kbtnDetails.Text = "Fewer details";

        // Initialize progress bar
        kpbProgress.Minimum = 0;
        kpbProgress.Maximum = 100;
        kpbProgress.Value = 0;
        kpbProgress.Style = ProgressBarStyle.Continuous;

        // Initially hide details panel
        pnlDetails.Visible = false;
        kbtnDetails.Text = "More details";
        
        // Setup speed graph paint event
        pnlSpeedGraph.Paint += pnlSpeedGraph_Paint;
    }

    private async void VisualFileCompressionDialogForm_Load(object? sender, EventArgs e)
    {
        try
        {
            await PrepareFileListAsync();
            await StartCompressionOperationAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Compression Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private async Task PrepareFileListAsync()
    {
        await Task.Run(() =>
        {
            if (File.Exists(_sourcePath))
            {
                var fileInfo = new FileInfo(_sourcePath);
                _fileList.Add(new CompressionItem
                {
                    SourcePath = _sourcePath,
                    ArchivePath = Path.GetFileName(_sourcePath),
                    Size = fileInfo.Length
                });
                _totalBytes = fileInfo.Length;
            }
            else if (Directory.Exists(_sourcePath))
            {
                var sourceDir = new DirectoryInfo(_sourcePath);
                BuildFileList(sourceDir, sourceDir.FullName);
                _totalBytes = _fileList.Sum(f => f.Size);
            }
        });

        UpdateOperationText();
    }

    private void BuildFileList(DirectoryInfo sourceDir, string basePath)
    {
        foreach (var file in sourceDir.GetFiles())
        {
            var relativePath = _includeBaseDirectory
                ? file.FullName.Substring(basePath.Length).TrimStart('\\')
                : file.FullName.Substring(basePath.Length).TrimStart('\\');

            _fileList.Add(new CompressionItem
            {
                SourcePath = file.FullName,
                ArchivePath = relativePath.Replace('\\', '/'),
                Size = file.Length
            });
        }

        foreach (var subDir in sourceDir.GetDirectories())
        {
            BuildFileList(subDir, basePath);
        }
    }

    private async Task StartCompressionOperationAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _startTime = DateTime.Now;
        _currentFileIndex = 0;
        _compressedBytes = 0;
        _isPaused = false;

        try
        {
            // Check if destination file exists
            if (File.Exists(_destinationPath))
            {
                var result = MessageBox.Show(
                    $"The file '{Path.GetFileName(_destinationPath)}' already exists.\n\nDo you want to replace it?",
                    "Confirm File Replace",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                if (result == DialogResult.No)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                File.Delete(_destinationPath);
            }

            await CompressFilesAsync(_cancellationTokenSource.Token);

            if (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        catch (OperationCanceledException)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error during compression: {ex.Message}", "Compression Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private async Task CompressFilesAsync(CancellationToken cancellationToken)
    {
        using (var archive = ZipFile.Open(_destinationPath, ZipArchiveMode.Create))
        {
            for (int i = 0; i < _fileList.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                while (_isPaused && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(100, cancellationToken);
                }

                _currentFileIndex = i;
                var item = _fileList[i];

                await CompressFileAsync(archive, item, cancellationToken);
            }
        }
    }

    private async Task CompressFileAsync(ZipArchive archive, CompressionItem item, CancellationToken cancellationToken)
    {
        const int bufferSize = 8192;
        var buffer = new byte[bufferSize];
        long fileBytesProcessed = 0;

        var entry = archive.CreateEntry(item.ArchivePath, _compressionLevel);
        
        using (var sourceStream = new FileStream(item.SourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
        using (var entryStream = entry.Open())
        {
            int bytesRead;
            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, bufferSize, cancellationToken)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                while (_isPaused && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(100, cancellationToken);
                }

                await entryStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                fileBytesProcessed += bytesRead;
                _compressedBytes += bytesRead;

                UpdateProgress();
                UpdateSpeed();
            }
        }
    }

    private void UpdateProgress()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateProgress));
            return;
        }

        if (_totalBytes > 0)
        {
            var percentage = (int)((_compressedBytes * 100) / _totalBytes);
            kpbProgress.Value = Math.Min(percentage, 100);
            lblPercentage.Text = $"{percentage}% complete";
        }

        UpdateOperationText();
        UpdateRemainingInfo();
    }

    private void UpdateOperationText()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateOperationText));
            return;
        }

        var itemCount = _fileList.Count;
        var sourceName = Path.GetFileName(_sourcePath) ?? _sourcePath;
        var destName = Path.GetFileName(_destinationPath) ?? _destinationPath;

        if (itemCount > 1)
        {
            lblOperation.Text = $"Compressing {itemCount} items from {sourceName} to {destName}";
        }
        else
        {
            lblOperation.Text = $"Compressing from {sourceName} to {destName}";
        }
    }

    private void UpdateRemainingInfo()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateRemainingInfo));
            return;
        }

        if (_currentFileIndex < _fileList.Count)
        {
            var currentFile = _fileList[_currentFileIndex];
            lblFileName.Text = $"Name: {Path.GetFileName(currentFile.SourcePath)}";
        }

        var remainingItems = _fileList.Count - _currentFileIndex - 1;
        var remainingBytes = _totalBytes - _compressedBytes;
        lblItemsRemaining.Text = $"Items remaining: {remainingItems} ({FormatBytes(remainingBytes)})";

        if (_compressedBytes > 0 && _totalBytes > 0)
        {
            var elapsed = DateTime.Now - _startTime;
            var bytesPerSecond = _compressedBytes / Math.Max(elapsed.TotalSeconds, 0.1);
            var remainingSeconds = remainingBytes / Math.Max(bytesPerSecond, 1);

            lblSpeed.Text = $"Speed: {FormatBytes((long)bytesPerSecond)}/s";
            lblTimeRemaining.Text = $"Time remaining: About {FormatTime((int)remainingSeconds)}";
        }
    }

    private void UpdateSpeed()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateSpeed));
            return;
        }

        var elapsed = DateTime.Now - _startTime;
        if (elapsed.TotalSeconds > 0)
        {
            var bytesPerSecond = _compressedBytes / elapsed.TotalSeconds;
            _speedHistory.Enqueue(bytesPerSecond);
            if (_speedHistory.Count > MaxSpeedHistorySize)
            {
                _speedHistory.Dequeue();
            }

            // Update speed graph
            var avgSpeed = _speedHistory.Average();
            lblSpeed.Text = $"Speed: {FormatBytes((long)avgSpeed)}/s";
            
            // Draw speed graph
            DrawSpeedGraph();
        }
    }

    private void DrawSpeedGraph()
    {
        if (_speedHistory.Count == 0 || pnlSpeedGraph.Width <= 0 || pnlSpeedGraph.Height <= 0)
        {
            return;
        }

        pnlSpeedGraph.Invalidate();
    }

    private void pnlSpeedGraph_Paint(object? sender, PaintEventArgs e)
    {
        if (_speedHistory.Count == 0)
        {
            return;
        }

        var g = e.Graphics;
        var rect = pnlSpeedGraph.ClientRectangle;
        
        // Find max speed for scaling
        var maxSpeed = _speedHistory.Max();
        if (maxSpeed <= 0)
        {
            return;
        }

        // Draw background
        using (var brush = new SolidBrush(SystemColors.Control))
        {
            g.FillRectangle(brush, rect);
        }

        // Draw speed line
        var points = new List<PointF>();
        var stepX = (float)rect.Width / Math.Max(_speedHistory.Count - 1, 1);
        
        int index = 0;
        foreach (var speed in _speedHistory)
        {
            var x = index * stepX;
            var y = rect.Height - (float)((speed / maxSpeed) * rect.Height);
            points.Add(new PointF(x, y));
            index++;
        }

        if (points.Count > 1)
        {
            using (var pen = new Pen(Color.FromArgb(0, 120, 215), 2))
            {
                g.DrawLines(pen, points.ToArray());
            }

            // Fill area under the line
            var fillPoints = new List<PointF>(points)
            {
                new PointF(points[points.Count - 1].X, rect.Height),
                new PointF(points[0].X, rect.Height)
            };

            using (var brush = new SolidBrush(Color.FromArgb(30, 120, 215)))
            {
                g.FillPolygon(brush, fillPoints.ToArray());
            }
        }
    }

    private static string FormatBytes(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    private static string FormatTime(int seconds)
    {
        if (seconds < 60)
        {
            return $"{seconds} second{(seconds != 1 ? "s" : "")}";
        }

        var minutes = seconds / 60;
        var remainingSeconds = seconds % 60;
        if (remainingSeconds == 0)
        {
            return $"{minutes} minute{(minutes != 1 ? "s" : "")}";
        }
        return $"{minutes} minute{(minutes != 1 ? "s" : "")} {remainingSeconds} second{(remainingSeconds != 1 ? "s" : "")}";
    }

    private void kbtnPause_Click(object? sender, EventArgs e)
    {
        _isPaused = !_isPaused;
        kbtnPause.Text = _isPaused ? "Resume" : "Pause";
    }

    private void kbtnCancel_Click(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void kbtnDetails_Click(object? sender, EventArgs e)
    {
        pnlDetails.Visible = !pnlDetails.Visible;
        kbtnDetails.Text = pnlDetails.Visible ? "Fewer details" : "More details";
    }

    #endregion

    #region Nested Classes

    private class CompressionItem
    {
        public string SourcePath { get; set; } = string.Empty;
        public string ArchivePath { get; set; } = string.Empty;
        public long Size { get; set; }
    }

    #endregion
}
