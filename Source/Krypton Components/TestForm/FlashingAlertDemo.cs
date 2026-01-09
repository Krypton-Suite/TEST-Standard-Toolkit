#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Utilities;

namespace TestForm;

public partial class FlashingAlertDemo : KryptonForm
{
    private readonly FlashingAlertSettings _settings1 = new();
    private readonly FlashingAlertSettings _settings2 = new();
    private readonly FlashingAlertSettings _settings3 = new();

    public FlashingAlertDemo()
    {
        InitializeComponent();
        InitializeSettings();
        UpdateStatusDisplay();
    }

    private void InitializeSettings()
    {
        // Settings for label 1: Red text on yellow background, fast
        _settings1.FlashForeColor = Color.Red;
        _settings1.FlashBackColor = Color.Yellow;
        _settings1.Interval = 300;

        // Settings for label 2: White text on blue background, medium
        _settings2.FlashForeColor = Color.White;
        _settings2.FlashBackColor = Color.Blue;
        _settings2.Interval = 500;

        // Settings for label 3: Black text on orange background, slow
        _settings3.FlashForeColor = Color.Black;
        _settings3.FlashBackColor = Color.Orange;
        _settings3.Interval = 800;
    }

    private void UpdateStatusDisplay()
    {
        lblStatus1.Text = statusLabel1.IsFlashing() ? @"Flashing" : @"Stopped";
        lblStatus2.Text = statusLabel2.IsFlashing() ? @"Flashing" : @"Stopped";
        lblStatus3.Text = statusLabel3.IsFlashing() ? @"Flashing" : @"Stopped";
        lblStatus4.Text = statusLabel4.IsFlashing() ? @"Flashing" : @"Stopped";
    }

    #region Label 1 Controls

    private void btnStart1_Click(object? sender, EventArgs e)
    {
        statusLabel1.StartFlashing(_settings1);
        UpdateStatusDisplay();
    }

    private void btnStop1_Click(object? sender, EventArgs e)
    {
        statusLabel1.StopFlashing();
        UpdateStatusDisplay();
    }

    private void btnToggle1_Click(object? sender, EventArgs e)
    {
        if (statusLabel1.IsFlashing())
        {
            statusLabel1.StopFlashing();
        }
        else
        {
            statusLabel1.StartFlashing(_settings1);
        }
        UpdateStatusDisplay();
    }

    #endregion

    #region Label 2 Controls

    private void btnStart2_Click(object? sender, EventArgs e)
    {
        statusLabel2.StartFlashing(_settings2);
        UpdateStatusDisplay();
    }

    private void btnStop2_Click(object? sender, EventArgs e)
    {
        statusLabel2.StopFlashing();
        UpdateStatusDisplay();
    }

    private void btnToggle2_Click(object? sender, EventArgs e)
    {
        if (statusLabel2.IsFlashing())
        {
            statusLabel2.StopFlashing();
        }
        else
        {
            statusLabel2.StartFlashing(_settings2);
        }
        UpdateStatusDisplay();
    }

    #endregion

    #region Label 3 Controls

    private void btnStart3_Click(object? sender, EventArgs e)
    {
        statusLabel3.StartFlashing(_settings3);
        UpdateStatusDisplay();
    }

    private void btnStop3_Click(object? sender, EventArgs e)
    {
        statusLabel3.StopFlashing();
        UpdateStatusDisplay();
    }

    private void btnToggle3_Click(object? sender, EventArgs e)
    {
        if (statusLabel3.IsFlashing())
        {
            statusLabel3.StopFlashing();
        }
        else
        {
            statusLabel3.StartFlashing(_settings3);
        }
        UpdateStatusDisplay();
    }

    #endregion

    #region Label 4 Controls (Direct Method)

    private void btnStart4_Click(object? sender, EventArgs e)
    {
        statusLabel4.StartFlashing(Color.White, Color.DarkRed, 400);
        UpdateStatusDisplay();
    }

    private void btnStop4_Click(object? sender, EventArgs e)
    {
        statusLabel4.StopFlashing();
        UpdateStatusDisplay();
    }

    private void btnToggle4_Click(object? sender, EventArgs e)
    {
        if (statusLabel4.IsFlashing())
        {
            statusLabel4.StopFlashing();
        }
        else
        {
            statusLabel4.StartFlashing(Color.White, Color.DarkRed, 400);
        }
        UpdateStatusDisplay();
    }

    #endregion

    #region Global Controls

    private void btnStartAll_Click(object? sender, EventArgs e)
    {
        statusLabel1.StartFlashing(_settings1);
        statusLabel2.StartFlashing(_settings2);
        statusLabel3.StartFlashing(_settings3);
        statusLabel4.StartFlashing(Color.White, Color.DarkRed, 400);
        UpdateStatusDisplay();
    }

    private void btnStopAll_Click(object? sender, EventArgs e)
    {
        statusLabel1.StopFlashing();
        statusLabel2.StopFlashing();
        statusLabel3.StopFlashing();
        statusLabel4.StopFlashing();
        UpdateStatusDisplay();
    }

    private void btnChangeInterval1_Click(object? sender, EventArgs e)
    {
        if (statusLabel1.IsFlashing())
        {
            var newInterval = _settings1.Interval == 300 ? 1000 : 300;
            _settings1.Interval = newInterval;
            statusLabel1.SetFlashInterval(newInterval);
            lblInterval1.Text = $@"Interval: {newInterval}ms";
        }
    }

    private void btnChangeInterval2_Click(object? sender, EventArgs e)
    {
        if (statusLabel2.IsFlashing())
        {
            var newInterval = _settings2.Interval == 500 ? 1000 : 500;
            _settings2.Interval = newInterval;
            statusLabel2.SetFlashInterval(newInterval);
            lblInterval2.Text = $@"Interval: {newInterval}ms";
        }
    }

    private void btnChangeInterval3_Click(object? sender, EventArgs e)
    {
        if (statusLabel3.IsFlashing())
        {
            var newInterval = _settings3.Interval == 800 ? 200 : 800;
            _settings3.Interval = newInterval;
            statusLabel3.SetFlashInterval(newInterval);
            lblInterval3.Text = $@"Interval: {newInterval}ms";
        }
    }

    private void timer1_Tick(object? sender, EventArgs e)
    {
        UpdateStatusDisplay();
    }

    #endregion
}
