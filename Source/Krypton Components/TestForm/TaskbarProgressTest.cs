#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating taskbar progress functionality on KryptonForm.
/// </summary>
public partial class TaskbarProgressTest : KryptonForm
{
    private readonly Timer _progressTimer;
    private int _progressValue;
    private readonly Timer _syncProgressTimer;

    public TaskbarProgressTest()
    {
        InitializeComponent();
        InitializeTaskbarProgress();
    }

    private void InitializeTaskbarProgress()
    {
        // Set form icon
        Icon = SystemIcons.Application;

        // Setup examples
        SetupBasicExamples();
        SetupInteractiveExamples();
        SetupStateExamples();
        SetupSyncWithTaskbarExample();

        // Setup property grid - show Taskbar property which contains OverlayIcon, Progress, and JumpList
        propertyGrid.SelectedObject = this;

        // Initialize progress timer
        _progressTimer = new Timer { Interval = 100 };
        _progressTimer.Tick += ProgressTimer_Tick;

        // Initialize sync progress timer
        _syncProgressTimer = new Timer { Interval = 150 };
        _syncProgressTimer.Tick += SyncProgressTimer_Tick;
    }

    private void SetupBasicExamples()
    {
        // Example 1: Set normal progress
        lblExample1.Text = "Example 1: Set normal progress (0-100%)";
        btnSetNormalProgress.Text = "Set Normal Progress";
        btnSetNormalProgress.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.Normal;
            Taskbar.Progress.Maximum = 100;
            Taskbar.Progress.Value = 50;
        };

        // Example 2: Indeterminate progress
        lblExample2.Text = "Example 2: Indeterminate progress";
        btnSetIndeterminate.Text = "Set Indeterminate";
        btnSetIndeterminate.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.Indeterminate;
        };
    }

    private void SetupInteractiveExamples()
    {
        // Example 3: Animated progress
        lblExample3.Text = "Example 3: Animated progress";
        btnStartAnimation.Text = "Start Animation";
        btnStartAnimation.Click += BtnStartAnimation_Click;

        btnStopAnimation.Text = "Stop Animation";
        btnStopAnimation.Click += BtnStopAnimation_Click;

        btnResetProgress.Text = "Reset Progress";
        btnResetProgress.Click += BtnResetProgress_Click;

        UpdateProgressDisplay();
    }

    private void SetupStateExamples()
    {
        // Example 4: Different states
        lblExample4.Text = "Example 4: Progress states";
        btnSetNormal.Text = "Normal";
        btnSetNormal.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.Normal;
            Taskbar.Progress.Maximum = 100;
            Taskbar.Progress.Value = 75;
        };

        btnSetError.Text = "Error";
        btnSetError.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.Error;
            Taskbar.Progress.Maximum = 100;
            Taskbar.Progress.Value = 50;
        };

        btnSetPaused.Text = "Paused";
        btnSetPaused.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.Paused;
            Taskbar.Progress.Maximum = 100;
            Taskbar.Progress.Value = 60;
        };

        btnSetNoProgress.Text = "No Progress";
        btnSetNoProgress.Click += (s, e) =>
        {
            Taskbar.Progress.State = TaskbarProgressState.NoProgress;
        };
    }

    private void SetupSyncWithTaskbarExample()
    {
        // Example 5: SyncWithTaskbar on KryptonProgressBar
        lblExample5.Text = "Example 5: KryptonProgressBar with SyncWithTaskbar";
        
        // Configure the progress bar
        syncProgressBar.Maximum = 100;
        syncProgressBar.Value = 0;
        syncProgressBar.SyncWithTaskbar = true; // Enable automatic sync
        
        // Buttons to control sync
        btnEnableSync.Text = "Enable Sync";
        btnEnableSync.Click += (s, e) =>
        {
            syncProgressBar.SyncWithTaskbar = true;
            UpdateSyncStatus();
        };

        btnDisableSync.Text = "Disable Sync";
        btnDisableSync.Click += (s, e) =>
        {
            syncProgressBar.SyncWithTaskbar = false;
            UpdateSyncStatus();
        };

        btnIncrementSync.Text = "Increment";
        btnIncrementSync.Click += (s, e) =>
        {
            syncProgressBar.Increment(10);
            UpdateSyncStatus();
        };

        btnResetSync.Text = "Reset";
        btnResetSync.Click += (s, e) =>
        {
            syncProgressBar.Value = 0;
            UpdateSyncStatus();
        };

        btnAnimateSync.Text = "Animate";
        btnAnimateSync.Click += (s, e) =>
        {
            if (_syncProgressTimer.Enabled)
            {
                _syncProgressTimer.Stop();
                btnAnimateSync.Text = "Animate";
            }
            else
            {
                _syncProgressTimer.Start();
                btnAnimateSync.Text = "Stop";
            }
        };

        // Initial status update
        UpdateSyncStatus();
    }

    private void SyncProgressTimer_Tick(object? sender, EventArgs e)
    {
        if (syncProgressBar.Value >= syncProgressBar.Maximum)
        {
            syncProgressBar.Value = 0;
        }
        else
        {
            syncProgressBar.Increment(5);
        }
        UpdateSyncStatus();
    }

    private void UpdateSyncStatus()
    {
        lblSyncStatus.Text = $"Progress Bar: {syncProgressBar.Value} / {syncProgressBar.Maximum} | Sync: {(syncProgressBar.SyncWithTaskbar ? "Enabled" : "Disabled")}";
    }

    private void BtnStartAnimation_Click(object? sender, EventArgs e)
    {
        Taskbar.Progress.State = TaskbarProgressState.Normal;
        Taskbar.Progress.Maximum = 100;
        _progressValue = 0;
        _progressTimer.Start();
        btnStartAnimation.Enabled = false;
        btnStopAnimation.Enabled = true;
    }

    private void BtnStopAnimation_Click(object? sender, EventArgs e)
    {
        _progressTimer.Stop();
        btnStartAnimation.Enabled = true;
        btnStopAnimation.Enabled = false;
    }

    private void BtnResetProgress_Click(object? sender, EventArgs e)
    {
        _progressTimer.Stop();
        Taskbar.Progress.Reset();
        _progressValue = 0;
        btnStartAnimation.Enabled = true;
        btnStopAnimation.Enabled = false;
        UpdateProgressDisplay();
    }

    private void ProgressTimer_Tick(object? sender, EventArgs e)
    {
        _progressValue++;
        if (_progressValue > 100)
        {
            _progressValue = 0;
        }

        Taskbar.Progress.Value = (ulong)_progressValue;
        UpdateProgressDisplay();
    }

    private void UpdateProgressDisplay()
    {
        lblProgressStatus.Text = $"Progress: {Taskbar.Progress.Value} / {Taskbar.Progress.Maximum} ({Taskbar.Progress.State})";
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _progressTimer?.Stop();
            _progressTimer?.Dispose();
            _syncProgressTimer?.Stop();
            _syncProgressTimer?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
