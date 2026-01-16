#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2022 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for progress bar threshold color information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarThresholdValues : Storage
{
    #region Instance Fields

    private readonly KryptonProgressBar _owner;
    private bool _useThresholdColors;
    private bool _autoCalculateThresholds;
    private int _lowThreshold;
    private int _highThreshold;
    private Color _lowThresholdColor;
    private Color _mediumThresholdColor;
    private Color _highThresholdColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarThresholdValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ProgressBarThresholdValues(KryptonProgressBar owner, NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;
        _owner = owner;

        Reset();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !_useThresholdColors &&
                                      !_autoCalculateThresholds &&
                                      _lowThreshold == 33 &&
                                      _highThreshold == 66 &&
                                      _lowThresholdColor == Color.Red &&
                                      _mediumThresholdColor == Color.Orange &&
                                      _highThresholdColor == Color.Green;

    #endregion

    #region UseThresholdColors

    /// <summary>
    /// Gets or sets whether to use threshold-based colors for the progress bar.
    /// When enabled, the color changes based on LowThreshold and HighThreshold values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Whether to use threshold-based colors for the progress bar.")]
    [DefaultValue(false)]
    public bool UseThresholdColors
    {
        get => _useThresholdColors;
        set
        {
            if (_useThresholdColors == value)
            {
                return;
            }

            _useThresholdColors = value;
            _owner.UpdateThresholdColor();
            PerformNeedPaint(true);
        }
    }

    private bool ShouldSerializeUseThresholdColors() => UseThresholdColors;

    /// <summary>
    /// Resets the UseThresholdColors property to its default value.
    /// </summary>
    public void ResetUseThresholdColors() => UseThresholdColors = false;

    #endregion

    #region AutoCalculateThresholds

    /// <summary>
    /// Gets or sets whether to automatically calculate threshold values based on Minimum and Maximum.
    /// When enabled, thresholds are calculated as: LowThreshold = Minimum + (Maximum - Minimum) / 3,
    /// HighThreshold = Minimum + (Maximum - Minimum) * 2 / 3.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Whether to automatically calculate threshold values based on Minimum and Maximum.")]
    [DefaultValue(false)]
    public bool AutoCalculateThresholds
    {
        get => _autoCalculateThresholds;
        set
        {
            if (_autoCalculateThresholds == value)
            {
                return;
            }

            _autoCalculateThresholds = value;
            
            if (_autoCalculateThresholds)
            {
                // Calculate thresholds when enabled
                CalculateThresholds();
            }
            
            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAutoCalculateThresholds() => AutoCalculateThresholds;

    /// <summary>
    /// Resets the AutoCalculateThresholds property to its default value.
    /// </summary>
    public void ResetAutoCalculateThresholds() => AutoCalculateThresholds = false;

    /// <summary>
    /// Calculates threshold values based on the owner's Minimum and Maximum.
    /// </summary>
    internal void CalculateThresholds()
    {
        if (_owner == null)
        {
            return;
        }

        int range = _owner.Maximum - _owner.Minimum;
        if (range > 0)
        {
            // Calculate thresholds: divide range into thirds
            // LowThreshold = Minimum + 1/3 of range
            // HighThreshold = Minimum + 2/3 of range
            _lowThreshold = _owner.Minimum + range / 3;
            _highThreshold = _owner.Minimum + (range * 2) / 3;
            
            // Ensure thresholds are valid and distinct
            if (_lowThreshold >= _highThreshold)
            {
                _lowThreshold = _owner.Minimum + Math.Max(1, range / 3);
                _highThreshold = Math.Min(_owner.Maximum, _lowThreshold + 1);
            }
            
            // Ensure thresholds don't exceed maximum
            if (_lowThreshold > _owner.Maximum)
            {
                _lowThreshold = _owner.Maximum;
            }
            if (_highThreshold > _owner.Maximum)
            {
                _highThreshold = _owner.Maximum;
            }
            
            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
        else
        {
            // Range is zero or invalid, set thresholds to minimum
            _lowThreshold = _owner.Minimum;
            _highThreshold = _owner.Minimum;
        }
    }

    #endregion

    #region LowThreshold

    /// <summary>
    /// Gets or sets the low threshold value. When the progress value is below this threshold, the low threshold color is used.
    /// This property is read-only when AutoCalculateThresholds is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The low threshold value. Progress below this uses the low threshold color. Read-only when AutoCalculateThresholds is enabled.")]
    [DefaultValue(33)]
    public int LowThreshold
    {
        get => _lowThreshold;
        set
        {
            if (_autoCalculateThresholds)
            {
                throw new InvalidOperationException(@"LowThreshold cannot be set when AutoCalculateThresholds is enabled.");
            }

            if (value < 0 || (_owner != null && value > _owner.Maximum))
            {
                throw new ArgumentOutOfRangeException(nameof(LowThreshold), value, @"LowThreshold must be between 0 and Maximum.");
            }

            if (_lowThreshold == value)
            {
                return;
            }

            _lowThreshold = value;

            // Ensure low threshold is less than high threshold
            if (_lowThreshold >= _highThreshold && _owner != null)
            {
                _highThreshold = Math.Min(_owner.Maximum, _lowThreshold + 1);
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThreshold() => LowThreshold != 33;

    /// <summary>
    /// Resets the LowThreshold property to its default value.
    /// </summary>
    public void ResetLowThreshold() => LowThreshold = 33;

    #endregion

    #region HighThreshold

    /// <summary>
    /// Gets or sets the high threshold value. When the progress value is above this threshold, the high threshold color is used.
    /// This property is read-only when AutoCalculateThresholds is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The high threshold value. Progress above this uses the high threshold color. Read-only when AutoCalculateThresholds is enabled.")]
    [DefaultValue(66)]
    public int HighThreshold
    {
        get => _highThreshold;
        set
        {
            if (_autoCalculateThresholds)
            {
                throw new InvalidOperationException(@"HighThreshold cannot be set when AutoCalculateThresholds is enabled.");
            }

            if (value < 0 || (_owner != null && value > _owner.Maximum))
            {
                throw new ArgumentOutOfRangeException(nameof(HighThreshold), value, @"HighThreshold must be between 0 and Maximum.");
            }

            if (_highThreshold == value)
            {
                return;
            }

            _highThreshold = value;

            // Ensure high threshold is greater than low threshold
            if (_highThreshold <= _lowThreshold)
            {
                _lowThreshold = Math.Max(0, _highThreshold - 1);
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThreshold() => HighThreshold != 66;

    /// <summary>
    /// Resets the HighThreshold property to its default value.
    /// </summary>
    public void ResetHighThreshold() => HighThreshold = 66;

    #endregion

    #region LowThresholdColor

    /// <summary>
    /// Gets or sets the color used when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color used when progress is below the low threshold.")]
    [DefaultValue(typeof(Color), nameof(Color.Red))]
    public Color LowThresholdColor
    {
        get => _lowThresholdColor;
        set
        {
            if (_lowThresholdColor == value)
            {
                return;
            }

            _lowThresholdColor = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdColor() => LowThresholdColor != Color.Red;

    /// <summary>
    /// Resets the LowThresholdColor property to its default value.
    /// </summary>
    public void ResetLowThresholdColor() => LowThresholdColor = Color.Red;

    #endregion

    #region MediumThresholdColor

    /// <summary>
    /// Gets or sets the color used when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color used when progress is between low and high thresholds.")]
    [DefaultValue(typeof(Color), nameof(Color.Orange))]
    public Color MediumThresholdColor
    {
        get => _mediumThresholdColor;
        set
        {
            if (_mediumThresholdColor == value)
            {
                return;
            }

            _mediumThresholdColor = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdColor() => MediumThresholdColor != Color.Orange;

    /// <summary>
    /// Resets the MediumThresholdColor property to its default value.
    /// </summary>
    public void ResetMediumThresholdColor() => MediumThresholdColor = Color.Orange;

    #endregion

    #region HighThresholdColor

    /// <summary>
    /// Gets or sets the color used when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color used when progress is above the high threshold.")]
    [DefaultValue(typeof(Color), nameof(Color.Green))]
    public Color HighThresholdColor
    {
        get => _highThresholdColor;
        set
        {
            if (_highThresholdColor == value)
            {
                return;
            }

            _highThresholdColor = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdColor() => HighThresholdColor != Color.Green;

    /// <summary>
    /// Resets the HighThresholdColor property to its default value.
    /// </summary>
    public void ResetHighThresholdColor() => HighThresholdColor = Color.Green;

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetUseThresholdColors();
        ResetAutoCalculateThresholds();
        ResetLowThreshold();
        ResetHighThreshold();
        ResetLowThresholdColor();
        ResetMediumThresholdColor();
        ResetHighThresholdColor();
    }

    #endregion
}
