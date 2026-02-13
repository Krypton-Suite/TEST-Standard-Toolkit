#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides settings for flashing alert functionality on ToolStripStatusLabel controls.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FlashingAlertSettings
{
    /// <summary>
    /// Gets or sets the foreground color to use when flashing.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The foreground color to use when flashing.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color FlashForeColor { get; set; } = Color.Empty;

    /// <summary>
    /// Gets or sets the background color to use when flashing.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The background color to use when flashing.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color FlashBackColor { get; set; } = Color.Empty;

    /// <summary>
    /// Gets or sets the flash interval in milliseconds.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The flash interval in milliseconds.")]
    [DefaultValue(500)]
    public int Interval { get; set; } = 500;

    /// <summary>
    /// Returns a string representation of the settings.
    /// </summary>
    public override string ToString()
    {
        if (FlashForeColor == Color.Empty && FlashBackColor == Color.Empty)
        {
            return $@"Interval: {Interval}ms";
        }

        var foreColor = FlashForeColor != Color.Empty ? FlashForeColor.Name : @"Default";
        var backColor = FlashBackColor != Color.Empty ? FlashBackColor.Name : @"Default";
        return $@"Fore: {foreColor}, Back: {backColor}, Interval: {Interval}ms";
    }
}

/// <summary>
/// Provides extension methods for adding flashing alert functionality to ToolStripStatusLabel controls.
/// </summary>
public static class ToolStripStatusLabelExtensions
{
    #region Private Fields

    private static readonly Dictionary<ToolStripStatusLabel, FlashingState> _flashingStates = new();

    #endregion

    #region Public Methods

    /// <summary>
    /// Starts flashing the ToolStripStatusLabel using the specified settings.
    /// </summary>
    /// <param name="label">The ToolStripStatusLabel to flash.</param>
    /// <param name="settings">The flashing alert settings to use.</param>
    /// <exception cref="ArgumentNullException">Thrown when label or settings is null.</exception>
    public static void StartFlashing(this ToolStripStatusLabel label, FlashingAlertSettings settings)
    {
        if (label == null)
        {
            throw new ArgumentNullException(nameof(label));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        var foreColor = settings.FlashForeColor != Color.Empty ? (Color?)settings.FlashForeColor : null;
        var backColor = settings.FlashBackColor != Color.Empty ? (Color?)settings.FlashBackColor : null;
        StartFlashing(label, foreColor, backColor, settings.Interval);
    }

    /// <summary>
    /// Starts flashing the ToolStripStatusLabel with the specified colors and interval.
    /// </summary>
    /// <param name="label">The ToolStripStatusLabel to flash.</param>
    /// <param name="flashForeColor">The foreground color to use when flashing. If null, uses the original foreground color.</param>
    /// <param name="flashBackColor">The background color to use when flashing. If null, uses the original background color.</param>
    /// <param name="interval">The flash interval in milliseconds. Default is 500ms.</param>
    /// <exception cref="ArgumentNullException">Thrown when label is null.</exception>
    public static void StartFlashing(this ToolStripStatusLabel label, Color? flashForeColor = null, Color? flashBackColor = null, int interval = 500)
    {
        if (label == null)
        {
            throw new ArgumentNullException(nameof(label));
        }

        if (interval <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(interval), @"Interval must be greater than zero.");
        }

        // Stop any existing flashing
        StopFlashing(label);

        // Store original colors
        var originalForeColor = label.ForeColor;
        var originalBackColor = label.BackColor;

        // Create flashing state
        var state = new FlashingState
        {
            OriginalForeColor = originalForeColor,
            OriginalBackColor = originalBackColor,
            FlashForeColor = flashForeColor ?? originalForeColor,
            FlashBackColor = flashBackColor ?? originalBackColor,
            IsFlashing = true
        };

        // Create and start timer
        state.Timer = new System.Windows.Forms.Timer
        {
            Interval = interval
        };
        state.Timer.Tick += (sender, e) => OnFlashTimerTick(label);
        state.Timer.Start();

        // Hook up disposal
        label.Disposed += OnLabelDisposed;

        // Store state
        _flashingStates[label] = state;

        // Apply initial flash state
        ApplyFlashState(label, true);
    }

    /// <summary>
    /// Stops flashing the ToolStripStatusLabel and restores original colors.
    /// </summary>
    /// <param name="label">The ToolStripStatusLabel to stop flashing.</param>
    public static void StopFlashing(this ToolStripStatusLabel label)
    {
        if (label == null)
        {
            return;
        }

        if (!_flashingStates.TryGetValue(label, out var state))
        {
            return;
        }

        // Stop timer
        state.Timer?.Stop();
        state.Timer?.Dispose();
        state.Timer = null;

        // Restore original colors
        if (!label.IsDisposed)
        {
            label.ForeColor = state.OriginalForeColor;
            label.BackColor = state.OriginalBackColor;
        }

        // Remove state
        _flashingStates.Remove(label);

        // Unhook disposal
        label.Disposed -= OnLabelDisposed;
    }

    /// <summary>
    /// Gets a value indicating whether the ToolStripStatusLabel is currently flashing.
    /// </summary>
    /// <param name="label">The ToolStripStatusLabel to check.</param>
    /// <returns>True if the label is flashing; otherwise, false.</returns>
    public static bool IsFlashing(this ToolStripStatusLabel label)
    {
        if (label == null)
        {
            return false;
        }

        return _flashingStates.TryGetValue(label, out var state) && state.IsFlashing;
    }

    /// <summary>
    /// Sets the flash interval for a currently flashing ToolStripStatusLabel.
    /// </summary>
    /// <param name="label">The ToolStripStatusLabel to update.</param>
    /// <param name="interval">The new flash interval in milliseconds.</param>
    /// <exception cref="InvalidOperationException">Thrown when the label is not currently flashing.</exception>
    public static void SetFlashInterval(this ToolStripStatusLabel label, int interval)
    {
        if (label == null)
        {
            throw new ArgumentNullException(nameof(label));
        }

        if (interval <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(interval), @"Interval must be greater than zero.");
        }

        if (!_flashingStates.TryGetValue(label, out var state) || state.Timer == null)
        {
            throw new InvalidOperationException(@"The label is not currently flashing.");
        }

        state.Timer.Interval = interval;
    }

    #endregion

    #region Private Methods

    private static void OnFlashTimerTick(ToolStripStatusLabel label)
    {
        if (label.IsDisposed || !_flashingStates.TryGetValue(label, out var state))
        {
            StopFlashing(label);
            return;
        }

        // Toggle flash state
        state.IsFlashing = !state.IsFlashing;
        ApplyFlashState(label, state.IsFlashing);
    }

    private static void ApplyFlashState(ToolStripStatusLabel label, bool flashOn)
    {
        if (label.IsDisposed || !_flashingStates.TryGetValue(label, out var state))
        {
            return;
        }

        if (flashOn)
        {
            label.ForeColor = state.FlashForeColor;
            label.BackColor = state.FlashBackColor;
        }
        else
        {
            label.ForeColor = state.OriginalForeColor;
            label.BackColor = state.OriginalBackColor;
        }
    }

    private static void OnLabelDisposed(object? sender, EventArgs e)
    {
        if (sender is ToolStripStatusLabel label)
        {
            StopFlashing(label);
        }
    }

    #endregion

    #region Private Classes

    private class FlashingState
    {
        public Color OriginalForeColor { get; set; }
        public Color OriginalBackColor { get; set; }
        public Color FlashForeColor { get; set; }
        public Color FlashBackColor { get; set; }
        public System.Windows.Forms.Timer? Timer { get; set; }
        public bool IsFlashing { get; set; }
    }

    #endregion
}
