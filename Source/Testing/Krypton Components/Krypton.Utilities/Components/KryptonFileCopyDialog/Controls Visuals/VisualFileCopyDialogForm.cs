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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Utilities;

/// <summary>
/// Visual form for the file copy dialog with progress indication.
/// </summary>
public partial class VisualFileCopyDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly string _sourcePath;
    private readonly string _destinationPath;
    private readonly bool _overwritePrompt;
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isPaused;
    private readonly List<FileCopyItem> _fileList = new();
    private int _currentFileIndex;
    private long _totalBytes;
    private long _copiedBytes;
    private DateTime _startTime;
    private readonly Queue<double> _speedHistory = new();
    private const int MaxSpeedHistorySize = 50;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="VisualFileCopyDialogForm"/> class.
    /// </summary>
    /// <param name="sourcePath">The source path to copy from.</param>
    /// <param name="destinationPath">The destination path to copy to.</param>
    /// <param name="overwritePrompt">Whether to prompt before overwriting files.</param>
    public VisualFileCopyDialogForm(string sourcePath, string destinationPath, bool overwritePrompt)
    {
        InitializeComponent();

        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
        _overwritePrompt = overwritePrompt;

        SetupUI();
        Load += VisualFileCopyDialogForm_Load;
    }

    #endregion

    #region Implementation

    private void SetupUI()
    {
        Text = "Copying files...";
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

    private async void VisualFileCopyDialogForm_Load(object? sender, EventArgs e)
    {
        try
        {
            await PrepareFileListAsync();
            await StartCopyOperationAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _fileList.Add(new FileCopyItem
                {
                    SourcePath = _sourcePath,
                    DestinationPath = _destinationPath,
                    Size = fileInfo.Length
                });
                _totalBytes = fileInfo.Length;
            }
            else if (Directory.Exists(_sourcePath))
            {
                var sourceDir = new DirectoryInfo(_sourcePath);
                var destDir = new DirectoryInfo(_destinationPath);

                BuildFileList(sourceDir, destDir);
                _totalBytes = _fileList.Sum(f => f.Size);
            }
        });

        UpdateOperationText();
    }

    private void BuildFileList(DirectoryInfo sourceDir, DirectoryInfo destDir)
    {
        foreach (var file in sourceDir.GetFiles())
        {
            var destFilePath = Path.Combine(destDir.FullName, file.Name);
            _fileList.Add(new FileCopyItem
            {
                SourcePath = file.FullName,
                DestinationPath = destFilePath,
                Size = file.Length
            });
        }

        foreach (var subDir in sourceDir.GetDirectories())
        {
            var newDestDir = new DirectoryInfo(Path.Combine(destDir.FullName, subDir.Name));
            BuildFileList(subDir, newDestDir);
        }
    }

    private async Task StartCopyOperationAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _startTime = DateTime.Now;
        _currentFileIndex = 0;
        _copiedBytes = 0;
        _isPaused = false;

        try
        {
            if (File.Exists(_sourcePath))
            {
                await CopyFileAsync(_fileList[0], _cancellationTokenSource.Token);
            }
            else if (Directory.Exists(_sourcePath))
            {
                await CopyDirectoryAsync(_cancellationTokenSource.Token);
            }

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
            MessageBox.Show($"Error during copy: {ex.Message}", "Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private async Task CopyDirectoryAsync(CancellationToken cancellationToken)
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

            // Create destination directory if needed
            var destDir = Path.GetDirectoryName(item.DestinationPath);
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            // Check if file exists and prompt for overwrite
            if (File.Exists(item.DestinationPath) && _overwritePrompt)
            {
                var result = MessageBox.Show(
                    $"The file '{Path.GetFileName(item.DestinationPath)}' already exists.\n\nDo you want to replace it?",
                    "Confirm File Replace",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    _cancellationTokenSource?.Cancel();
                    return;
                }

                if (result == DialogResult.No)
                {
                    _copiedBytes += item.Size;
                    UpdateProgress();
                    continue;
                }
            }

            await CopyFileAsync(item, cancellationToken);
        }
    }

    private async Task CopyFileAsync(FileCopyItem item, CancellationToken cancellationToken)
    {
        const int bufferSize = 8192;
        var buffer = new byte[bufferSize];
        long fileBytesCopied = 0;

        using (var sourceStream = new FileStream(item.SourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
        using (var destStream = new FileStream(item.DestinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
        {
            int bytesRead;
            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, bufferSize, cancellationToken)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                while (_isPaused && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(100, cancellationToken);
                }

                await destStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                fileBytesCopied += bytesRead;
                _copiedBytes += bytesRead;

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
            var percentage = (int)((_copiedBytes * 100) / _totalBytes);
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
            lblOperation.Text = $"Copying {itemCount} items from {sourceName} to {destName}";
        }
        else
        {
            lblOperation.Text = $"Copying from {sourceName} to {destName}";
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
        var remainingBytes = _totalBytes - _copiedBytes;
        lblItemsRemaining.Text = $"Items remaining: {remainingItems} ({FormatBytes(remainingBytes)})";

        if (_copiedBytes > 0 && _totalBytes > 0)
        {
            var elapsed = DateTime.Now - _startTime;
            var bytesPerSecond = _copiedBytes / Math.Max(elapsed.TotalSeconds, 0.1);
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
            var bytesPerSecond = _copiedBytes / elapsed.TotalSeconds;
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
        using (var brush = new System.Drawing.SolidBrush(SystemColors.Control))
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

            using (var brush = new System.Drawing.SolidBrush(Color.FromArgb(30, 120, 215)))
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
        // You could also update the chevron icon here
    }

    #endregion

    #region Nested Classes

    private class FileCopyItem
    {
        public string SourcePath { get; set; } = string.Empty;
        public string DestinationPath { get; set; } = string.Empty;
        public long Size { get; set; }
    }

    #endregion
}
