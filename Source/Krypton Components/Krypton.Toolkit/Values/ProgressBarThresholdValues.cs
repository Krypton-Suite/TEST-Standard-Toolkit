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
    private Color _lowThresholdColor2;
    private Color _mediumThresholdColor2;
    private Color _highThresholdColor2;
    private PaletteColorStyle _lowThresholdColorStyle;
    private PaletteColorStyle _mediumThresholdColorStyle;
    private PaletteColorStyle _highThresholdColorStyle;
    private PaletteRectangleAlign _lowThresholdColorAlign;
    private PaletteRectangleAlign _mediumThresholdColorAlign;
    private PaletteRectangleAlign _highThresholdColorAlign;
    private float _lowThresholdColorAngle;
    private float _mediumThresholdColorAngle;
    private float _highThresholdColorAngle;
    private bool _useOppositeTextColors;
    private Color _lowThresholdTextColor;
    private Color _mediumThresholdTextColor;
    private Color _highThresholdTextColor;
    private Color _originalLowThresholdTextColor;
    private Color _originalMediumThresholdTextColor;
    private Color _originalHighThresholdTextColor;
    private Color _lowThresholdTextColor2;
    private Color _mediumThresholdTextColor2;
    private Color _highThresholdTextColor2;
    private PaletteColorStyle _lowThresholdTextColorStyle;
    private PaletteColorStyle _mediumThresholdTextColorStyle;
    private PaletteColorStyle _highThresholdTextColorStyle;
    private PaletteRectangleAlign _lowThresholdTextColorAlign;
    private PaletteRectangleAlign _mediumThresholdTextColorAlign;
    private PaletteRectangleAlign _highThresholdTextColorAlign;
    private float _lowThresholdTextColorAngle;
    private float _mediumThresholdTextColorAngle;
    private float _highThresholdTextColorAngle;

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

        // Initialize text colors to Empty
        _lowThresholdTextColor = Color.Empty;
        _mediumThresholdTextColor = Color.Empty;
        _highThresholdTextColor = Color.Empty;
        _originalLowThresholdTextColor = Color.Empty;
        _originalMediumThresholdTextColor = Color.Empty;
        _originalHighThresholdTextColor = Color.Empty;
        _lowThresholdTextColor2 = Color.Empty;
        _mediumThresholdTextColor2 = Color.Empty;
        _highThresholdTextColor2 = Color.Empty;
        _lowThresholdTextColorStyle = PaletteColorStyle.Inherit;
        _mediumThresholdTextColorStyle = PaletteColorStyle.Inherit;
        _highThresholdTextColorStyle = PaletteColorStyle.Inherit;
        _lowThresholdTextColorAlign = PaletteRectangleAlign.Inherit;
        _mediumThresholdTextColorAlign = PaletteRectangleAlign.Inherit;
        _highThresholdTextColorAlign = PaletteRectangleAlign.Inherit;
        _lowThresholdTextColorAngle = -1f;
        _mediumThresholdTextColorAngle = -1f;
        _highThresholdTextColorAngle = -1f;
        _lowThresholdColor2 = Color.Empty;
        _mediumThresholdColor2 = Color.Empty;
        _highThresholdColor2 = Color.Empty;
        _lowThresholdColorStyle = PaletteColorStyle.Inherit;
        _mediumThresholdColorStyle = PaletteColorStyle.Inherit;
        _highThresholdColorStyle = PaletteColorStyle.Inherit;
        _lowThresholdColorAlign = PaletteRectangleAlign.Inherit;
        _mediumThresholdColorAlign = PaletteRectangleAlign.Inherit;
        _highThresholdColorAlign = PaletteRectangleAlign.Inherit;
        _lowThresholdColorAngle = -1f;
        _mediumThresholdColorAngle = -1f;
        _highThresholdColorAngle = -1f;

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
                                      !_useOppositeTextColors &&
                                      _lowThreshold == 33 &&
                                      _highThreshold == 66 &&
                                      _lowThresholdColor == Color.Red &&
                                      _mediumThresholdColor == Color.Orange &&
                                      _highThresholdColor == Color.Green &&
                                      _lowThresholdTextColor == Color.Empty &&
                                      _mediumThresholdTextColor == Color.Empty &&
                                      _highThresholdTextColor == Color.Empty &&
                                      _lowThresholdTextColor2 == Color.Empty &&
                                      _mediumThresholdTextColor2 == Color.Empty &&
                                      _highThresholdTextColor2 == Color.Empty &&
                                      _lowThresholdTextColorStyle == PaletteColorStyle.Inherit &&
                                      _mediumThresholdTextColorStyle == PaletteColorStyle.Inherit &&
                                      _highThresholdTextColorStyle == PaletteColorStyle.Inherit &&
                                      _lowThresholdTextColorAlign == PaletteRectangleAlign.Inherit &&
                                      _mediumThresholdTextColorAlign == PaletteRectangleAlign.Inherit &&
                                      _highThresholdTextColorAlign == PaletteRectangleAlign.Inherit &&
                                      Math.Abs(_lowThresholdTextColorAngle - (-1f)) < 0.001f &&
                                      Math.Abs(_mediumThresholdTextColorAngle - (-1f)) < 0.001f &&
                                      Math.Abs(_highThresholdTextColorAngle - (-1f)) < 0.001f &&
                                      _lowThresholdColor2 == Color.Empty &&
                                      _mediumThresholdColor2 == Color.Empty &&
                                      _highThresholdColor2 == Color.Empty &&
                                      _lowThresholdColorStyle == PaletteColorStyle.Inherit &&
                                      _mediumThresholdColorStyle == PaletteColorStyle.Inherit &&
                                      _highThresholdColorStyle == PaletteColorStyle.Inherit &&
                                      _lowThresholdColorAlign == PaletteRectangleAlign.Inherit &&
                                      _mediumThresholdColorAlign == PaletteRectangleAlign.Inherit &&
                                      _highThresholdColorAlign == PaletteRectangleAlign.Inherit &&
                                      Math.Abs(_lowThresholdColorAngle - (-1f)) < 0.001f &&
                                      Math.Abs(_mediumThresholdColorAngle - (-1f)) < 0.001f &&
                                      Math.Abs(_highThresholdColorAngle - (-1f)) < 0.001f;

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

            // Update opposite text color if enabled
            if (_useOppositeTextColors)
            {
                _lowThresholdTextColor = GetOppositeColor(_lowThresholdColor);
            }

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

            // Update opposite text color if enabled
            if (_useOppositeTextColors)
            {
                _mediumThresholdTextColor = GetOppositeColor(_mediumThresholdColor);
            }

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

            // Update opposite text color if enabled
            if (_useOppositeTextColors)
            {
                _highThresholdTextColor = GetOppositeColor(_highThresholdColor);
            }

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

    #region LowThresholdColor2

    /// <summary>
    /// Gets or sets the second color used when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second color used when progress is below the low threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color LowThresholdColor2
    {
        get => _lowThresholdColor2;
        set
        {
            if (_lowThresholdColor2 == value)
            {
                return;
            }

            _lowThresholdColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdColor2() => LowThresholdColor2 != Color.Empty;

    /// <summary>
    /// Resets the LowThresholdColor2 property to its default value.
    /// </summary>
    public void ResetLowThresholdColor2() => LowThresholdColor2 = Color.Empty;

    #endregion

    #region LowThresholdColorStyle

    /// <summary>
    /// Gets or sets the color drawing style when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style when progress is below the low threshold.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle LowThresholdColorStyle
    {
        get => _lowThresholdColorStyle;
        set
        {
            if (_lowThresholdColorStyle == value)
            {
                return;
            }

            _lowThresholdColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdColorStyle() => LowThresholdColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the LowThresholdColorStyle property to its default value.
    /// </summary>
    public void ResetLowThresholdColorStyle() => LowThresholdColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region LowThresholdColorAlign

    /// <summary>
    /// Gets or sets the color alignment when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style when progress is below the low threshold.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign LowThresholdColorAlign
    {
        get => _lowThresholdColorAlign;
        set
        {
            if (_lowThresholdColorAlign == value)
            {
                return;
            }

            _lowThresholdColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdColorAlign() => LowThresholdColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the LowThresholdColorAlign property to its default value.
    /// </summary>
    public void ResetLowThresholdColorAlign() => LowThresholdColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region LowThresholdColorAngle

    /// <summary>
    /// Gets or sets the color angle when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle when progress is below the low threshold.")]
    [DefaultValue(-1f)]
    public float LowThresholdColorAngle
    {
        get => _lowThresholdColorAngle;
        set
        {
            if (Math.Abs(_lowThresholdColorAngle - value) < 0.001f)
            {
                return;
            }

            _lowThresholdColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdColorAngle() => Math.Abs(LowThresholdColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the LowThresholdColorAngle property to its default value.
    /// </summary>
    public void ResetLowThresholdColorAngle() => LowThresholdColorAngle = -1f;

    #endregion

    #region MediumThresholdColor2

    /// <summary>
    /// Gets or sets the second color used when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second color used when progress is between low and high thresholds. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color MediumThresholdColor2
    {
        get => _mediumThresholdColor2;
        set
        {
            if (_mediumThresholdColor2 == value)
            {
                return;
            }

            _mediumThresholdColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdColor2() => MediumThresholdColor2 != Color.Empty;

    /// <summary>
    /// Resets the MediumThresholdColor2 property to its default value.
    /// </summary>
    public void ResetMediumThresholdColor2() => MediumThresholdColor2 = Color.Empty;

    #endregion

    #region MediumThresholdColorStyle

    /// <summary>
    /// Gets or sets the color drawing style when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style when progress is between low and high thresholds.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle MediumThresholdColorStyle
    {
        get => _mediumThresholdColorStyle;
        set
        {
            if (_mediumThresholdColorStyle == value)
            {
                return;
            }

            _mediumThresholdColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdColorStyle() => MediumThresholdColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the MediumThresholdColorStyle property to its default value.
    /// </summary>
    public void ResetMediumThresholdColorStyle() => MediumThresholdColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region MediumThresholdColorAlign

    /// <summary>
    /// Gets or sets the color alignment when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style when progress is between low and high thresholds.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign MediumThresholdColorAlign
    {
        get => _mediumThresholdColorAlign;
        set
        {
            if (_mediumThresholdColorAlign == value)
            {
                return;
            }

            _mediumThresholdColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdColorAlign() => MediumThresholdColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the MediumThresholdColorAlign property to its default value.
    /// </summary>
    public void ResetMediumThresholdColorAlign() => MediumThresholdColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region MediumThresholdColorAngle

    /// <summary>
    /// Gets or sets the color angle when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle when progress is between low and high thresholds.")]
    [DefaultValue(-1f)]
    public float MediumThresholdColorAngle
    {
        get => _mediumThresholdColorAngle;
        set
        {
            if (Math.Abs(_mediumThresholdColorAngle - value) < 0.001f)
            {
                return;
            }

            _mediumThresholdColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdColorAngle() => Math.Abs(MediumThresholdColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the MediumThresholdColorAngle property to its default value.
    /// </summary>
    public void ResetMediumThresholdColorAngle() => MediumThresholdColorAngle = -1f;

    #endregion

    #region HighThresholdColor2

    /// <summary>
    /// Gets or sets the second color used when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second color used when progress is above the high threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color HighThresholdColor2
    {
        get => _highThresholdColor2;
        set
        {
            if (_highThresholdColor2 == value)
            {
                return;
            }

            _highThresholdColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdColor2() => HighThresholdColor2 != Color.Empty;

    /// <summary>
    /// Resets the HighThresholdColor2 property to its default value.
    /// </summary>
    public void ResetHighThresholdColor2() => HighThresholdColor2 = Color.Empty;

    #endregion

    #region HighThresholdColorStyle

    /// <summary>
    /// Gets or sets the color drawing style when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style when progress is above the high threshold.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle HighThresholdColorStyle
    {
        get => _highThresholdColorStyle;
        set
        {
            if (_highThresholdColorStyle == value)
            {
                return;
            }

            _highThresholdColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdColorStyle() => HighThresholdColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the HighThresholdColorStyle property to its default value.
    /// </summary>
    public void ResetHighThresholdColorStyle() => HighThresholdColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region HighThresholdColorAlign

    /// <summary>
    /// Gets or sets the color alignment when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style when progress is above the high threshold.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign HighThresholdColorAlign
    {
        get => _highThresholdColorAlign;
        set
        {
            if (_highThresholdColorAlign == value)
            {
                return;
            }

            _highThresholdColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdColorAlign() => HighThresholdColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the HighThresholdColorAlign property to its default value.
    /// </summary>
    public void ResetHighThresholdColorAlign() => HighThresholdColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region HighThresholdColorAngle

    /// <summary>
    /// Gets or sets the color angle when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle when progress is above the high threshold.")]
    [DefaultValue(-1f)]
    public float HighThresholdColorAngle
    {
        get => _highThresholdColorAngle;
        set
        {
            if (Math.Abs(_highThresholdColorAngle - value) < 0.001f)
            {
                return;
            }

            _highThresholdColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdColorAngle() => Math.Abs(HighThresholdColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the HighThresholdColorAngle property to its default value.
    /// </summary>
    public void ResetHighThresholdColorAngle() => HighThresholdColorAngle = -1f;

    #endregion

    #region UseOppositeTextColors

    /// <summary>
    /// Gets or sets whether to automatically set text colors to the opposite of threshold colors.
    /// When enabled, text colors are calculated as the inverse/complement of the threshold colors.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Whether to automatically set text colors to the opposite of threshold colors.")]
    [DefaultValue(false)]
    public bool UseOppositeTextColors
    {
        get => _useOppositeTextColors;
        set
        {
            if (_useOppositeTextColors == value)
            {
                return;
            }

            _useOppositeTextColors = value;

            if (_useOppositeTextColors)
            {
                // Store original values before calculating opposite colors
                _originalLowThresholdTextColor = _lowThresholdTextColor;
                _originalMediumThresholdTextColor = _mediumThresholdTextColor;
                _originalHighThresholdTextColor = _highThresholdTextColor;

                // Calculate opposite colors when enabled
                CalculateOppositeTextColors();
            }
            else
            {
                // Restore original values when disabled
                _lowThresholdTextColor = _originalLowThresholdTextColor;
                _mediumThresholdTextColor = _originalMediumThresholdTextColor;
                _highThresholdTextColor = _originalHighThresholdTextColor;

                // Clear original values
                _originalLowThresholdTextColor = Color.Empty;
                _originalMediumThresholdTextColor = Color.Empty;
                _originalHighThresholdTextColor = Color.Empty;
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeUseOppositeTextColors() => UseOppositeTextColors;

    /// <summary>
    /// Resets the UseOppositeTextColors property to its default value.
    /// </summary>
    public void ResetUseOppositeTextColors() => UseOppositeTextColors = false;

    /// <summary>
    /// Calculates opposite text colors based on threshold colors.
    /// </summary>
    private void CalculateOppositeTextColors()
    {
        _lowThresholdTextColor = GetOppositeColor(_lowThresholdColor);
        _mediumThresholdTextColor = GetOppositeColor(_mediumThresholdColor);
        _highThresholdTextColor = GetOppositeColor(_highThresholdColor);
    }

    /// <summary>
    /// Gets the opposite/inverse color of the given color.
    /// </summary>
    private static Color GetOppositeColor(Color color)
    {
        // Calculate inverse color (255 - each component)
        return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
    }

    #endregion

    #region LowThresholdTextColor

    /// <summary>
    /// Gets or sets the text color used when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text color used when progress is below the low threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    public Color LowThresholdTextColor
    {
        get => _lowThresholdTextColor;
        set
        {
            if (_lowThresholdTextColor == value)
            {
                return;
            }

            // If UseOppositeTextColors is enabled, store the user's value as the original
            // so it can be restored when UseOppositeTextColors is disabled
            if (_useOppositeTextColors)
            {
                _originalLowThresholdTextColor = value;
                // If user sets to Empty, use calculated opposite; otherwise respect user's choice
                _lowThresholdTextColor = value == Color.Empty ? GetOppositeColor(_lowThresholdColor) : value;
            }
            else
            {
                _lowThresholdTextColor = value;
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdTextColor() => LowThresholdTextColor != Color.Empty;

    /// <summary>
    /// Resets the LowThresholdTextColor property to its default value.
    /// </summary>
    public void ResetLowThresholdTextColor() => LowThresholdTextColor = Color.Empty;

    #endregion

    #region MediumThresholdTextColor

    /// <summary>
    /// Gets or sets the text color used when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text color used when progress is between low and high thresholds. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    public Color MediumThresholdTextColor
    {
        get => _mediumThresholdTextColor;
        set
        {
            if (_mediumThresholdTextColor == value)
            {
                return;
            }

            // If UseOppositeTextColors is enabled, store the user's value as the original
            // so it can be restored when UseOppositeTextColors is disabled
            if (_useOppositeTextColors)
            {
                _originalMediumThresholdTextColor = value;
                // If user sets to Empty, use calculated opposite; otherwise respect user's choice
                _mediumThresholdTextColor = value == Color.Empty ? GetOppositeColor(_mediumThresholdColor) : value;
            }
            else
            {
                _mediumThresholdTextColor = value;
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdTextColor() => MediumThresholdTextColor != Color.Empty;

    /// <summary>
    /// Resets the MediumThresholdTextColor property to its default value.
    /// </summary>
    public void ResetMediumThresholdTextColor() => MediumThresholdTextColor = Color.Empty;

    #endregion

    #region HighThresholdTextColor

    /// <summary>
    /// Gets or sets the text color used when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text color used when progress is above the high threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    public Color HighThresholdTextColor
    {
        get => _highThresholdTextColor;
        set
        {
            if (_highThresholdTextColor == value)
            {
                return;
            }

            // If UseOppositeTextColors is enabled, store the user's value as the original
            // so it can be restored when UseOppositeTextColors is disabled
            if (_useOppositeTextColors)
            {
                _originalHighThresholdTextColor = value;
                // If user sets to Empty, use calculated opposite; otherwise respect user's choice
                _highThresholdTextColor = value == Color.Empty ? GetOppositeColor(_highThresholdColor) : value;
            }
            else
            {
                _highThresholdTextColor = value;
            }

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdTextColor() => HighThresholdTextColor != Color.Empty;

    /// <summary>
    /// Resets the HighThresholdTextColor property to its default value.
    /// </summary>
    public void ResetHighThresholdTextColor() => HighThresholdTextColor = Color.Empty;

    #endregion

    #region LowThresholdTextColor2

    /// <summary>
    /// Gets or sets the second text color used when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second text color used when progress is below the low threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color LowThresholdTextColor2
    {
        get => _lowThresholdTextColor2;
        set
        {
            if (_lowThresholdTextColor2 == value)
            {
                return;
            }

            _lowThresholdTextColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdTextColor2() => LowThresholdTextColor2 != Color.Empty;

    /// <summary>
    /// Resets the LowThresholdTextColor2 property to its default value.
    /// </summary>
    public void ResetLowThresholdTextColor2() => LowThresholdTextColor2 = Color.Empty;

    #endregion

    #region LowThresholdTextColorStyle

    /// <summary>
    /// Gets or sets the color drawing style for the text when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style for the text when progress is below the low threshold.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle LowThresholdTextColorStyle
    {
        get => _lowThresholdTextColorStyle;
        set
        {
            if (_lowThresholdTextColorStyle == value)
            {
                return;
            }

            _lowThresholdTextColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdTextColorStyle() => LowThresholdTextColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the LowThresholdTextColorStyle property to its default value.
    /// </summary>
    public void ResetLowThresholdTextColorStyle() => LowThresholdTextColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region LowThresholdTextColorAlign

    /// <summary>
    /// Gets or sets the color alignment for the text when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style for the text when progress is below the low threshold.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign LowThresholdTextColorAlign
    {
        get => _lowThresholdTextColorAlign;
        set
        {
            if (_lowThresholdTextColorAlign == value)
            {
                return;
            }

            _lowThresholdTextColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdTextColorAlign() => LowThresholdTextColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the LowThresholdTextColorAlign property to its default value.
    /// </summary>
    public void ResetLowThresholdTextColorAlign() => LowThresholdTextColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region LowThresholdTextColorAngle

    /// <summary>
    /// Gets or sets the color angle for the text when the progress value is below the low threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle for the text when progress is below the low threshold.")]
    [DefaultValue(-1f)]
    public float LowThresholdTextColorAngle
    {
        get => _lowThresholdTextColorAngle;
        set
        {
            if (Math.Abs(_lowThresholdTextColorAngle - value) < 0.001f)
            {
                return;
            }

            _lowThresholdTextColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeLowThresholdTextColorAngle() => Math.Abs(LowThresholdTextColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the LowThresholdTextColorAngle property to its default value.
    /// </summary>
    public void ResetLowThresholdTextColorAngle() => LowThresholdTextColorAngle = -1f;

    #endregion

    #region MediumThresholdTextColor2

    /// <summary>
    /// Gets or sets the second text color used when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second text color used when progress is between low and high thresholds. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color MediumThresholdTextColor2
    {
        get => _mediumThresholdTextColor2;
        set
        {
            if (_mediumThresholdTextColor2 == value)
            {
                return;
            }

            _mediumThresholdTextColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdTextColor2() => MediumThresholdTextColor2 != Color.Empty;

    /// <summary>
    /// Resets the MediumThresholdTextColor2 property to its default value.
    /// </summary>
    public void ResetMediumThresholdTextColor2() => MediumThresholdTextColor2 = Color.Empty;

    #endregion

    #region MediumThresholdTextColorStyle

    /// <summary>
    /// Gets or sets the color drawing style for the text when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style for the text when progress is between low and high thresholds.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle MediumThresholdTextColorStyle
    {
        get => _mediumThresholdTextColorStyle;
        set
        {
            if (_mediumThresholdTextColorStyle == value)
            {
                return;
            }

            _mediumThresholdTextColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdTextColorStyle() => MediumThresholdTextColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the MediumThresholdTextColorStyle property to its default value.
    /// </summary>
    public void ResetMediumThresholdTextColorStyle() => MediumThresholdTextColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region MediumThresholdTextColorAlign

    /// <summary>
    /// Gets or sets the color alignment for the text when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style for the text when progress is between low and high thresholds.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign MediumThresholdTextColorAlign
    {
        get => _mediumThresholdTextColorAlign;
        set
        {
            if (_mediumThresholdTextColorAlign == value)
            {
                return;
            }

            _mediumThresholdTextColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdTextColorAlign() => MediumThresholdTextColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the MediumThresholdTextColorAlign property to its default value.
    /// </summary>
    public void ResetMediumThresholdTextColorAlign() => MediumThresholdTextColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region MediumThresholdTextColorAngle

    /// <summary>
    /// Gets or sets the color angle for the text when the progress value is between the low and high thresholds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle for the text when progress is between low and high thresholds.")]
    [DefaultValue(-1f)]
    public float MediumThresholdTextColorAngle
    {
        get => _mediumThresholdTextColorAngle;
        set
        {
            if (Math.Abs(_mediumThresholdTextColorAngle - value) < 0.001f)
            {
                return;
            }

            _mediumThresholdTextColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeMediumThresholdTextColorAngle() => Math.Abs(MediumThresholdTextColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the MediumThresholdTextColorAngle property to its default value.
    /// </summary>
    public void ResetMediumThresholdTextColorAngle() => MediumThresholdTextColorAngle = -1f;

    #endregion

    #region HighThresholdTextColor2

    /// <summary>
    /// Gets or sets the second text color used when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Second text color used when progress is above the high threshold. Empty uses default.")]
    [DefaultValue(typeof(Color), nameof(Color.Empty))]
    [KryptonDefaultColor]
    public Color HighThresholdTextColor2
    {
        get => _highThresholdTextColor2;
        set
        {
            if (_highThresholdTextColor2 == value)
            {
                return;
            }

            _highThresholdTextColor2 = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdTextColor2() => HighThresholdTextColor2 != Color.Empty;

    /// <summary>
    /// Resets the HighThresholdTextColor2 property to its default value.
    /// </summary>
    public void ResetHighThresholdTextColor2() => HighThresholdTextColor2 = Color.Empty;

    #endregion

    #region HighThresholdTextColorStyle

    /// <summary>
    /// Gets or sets the color drawing style for the text when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color drawing style for the text when progress is above the high threshold.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    public PaletteColorStyle HighThresholdTextColorStyle
    {
        get => _highThresholdTextColorStyle;
        set
        {
            if (_highThresholdTextColorStyle == value)
            {
                return;
            }

            _highThresholdTextColorStyle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdTextColorStyle() => HighThresholdTextColorStyle != PaletteColorStyle.Inherit;

    /// <summary>
    /// Resets the HighThresholdTextColorStyle property to its default value.
    /// </summary>
    public void ResetHighThresholdTextColorStyle() => HighThresholdTextColorStyle = PaletteColorStyle.Inherit;

    #endregion

    #region HighThresholdTextColorAlign

    /// <summary>
    /// Gets or sets the color alignment for the text when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color alignment style for the text when progress is above the high threshold.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    public PaletteRectangleAlign HighThresholdTextColorAlign
    {
        get => _highThresholdTextColorAlign;
        set
        {
            if (_highThresholdTextColorAlign == value)
            {
                return;
            }

            _highThresholdTextColorAlign = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdTextColorAlign() => HighThresholdTextColorAlign != PaletteRectangleAlign.Inherit;

    /// <summary>
    /// Resets the HighThresholdTextColorAlign property to its default value.
    /// </summary>
    public void ResetHighThresholdTextColorAlign() => HighThresholdTextColorAlign = PaletteRectangleAlign.Inherit;

    #endregion

    #region HighThresholdTextColorAngle

    /// <summary>
    /// Gets or sets the color angle for the text when the progress value is above the high threshold.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Color angle for the text when progress is above the high threshold.")]
    [DefaultValue(-1f)]
    public float HighThresholdTextColorAngle
    {
        get => _highThresholdTextColorAngle;
        set
        {
            if (Math.Abs(_highThresholdTextColorAngle - value) < 0.001f)
            {
                return;
            }

            _highThresholdTextColorAngle = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHighThresholdTextColorAngle() => Math.Abs(HighThresholdTextColorAngle - (-1f)) > 0.001f;

    /// <summary>
    /// Resets the HighThresholdTextColorAngle property to its default value.
    /// </summary>
    public void ResetHighThresholdTextColorAngle() => HighThresholdTextColorAngle = -1f;

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetUseThresholdColors();
        ResetAutoCalculateThresholds();
        ResetUseOppositeTextColors();
        ResetLowThreshold();
        ResetHighThreshold();
        ResetLowThresholdColor();
        ResetMediumThresholdColor();
        ResetHighThresholdColor();
        ResetLowThresholdColor2();
        ResetMediumThresholdColor2();
        ResetHighThresholdColor2();
        ResetLowThresholdColorStyle();
        ResetMediumThresholdColorStyle();
        ResetHighThresholdColorStyle();
        ResetLowThresholdColorAlign();
        ResetMediumThresholdColorAlign();
        ResetHighThresholdColorAlign();
        ResetLowThresholdColorAngle();
        ResetMediumThresholdColorAngle();
        ResetHighThresholdColorAngle();
        ResetLowThresholdTextColor();
        ResetMediumThresholdTextColor();
        ResetHighThresholdTextColor();
        ResetLowThresholdTextColor2();
        ResetMediumThresholdTextColor2();
        ResetHighThresholdTextColor2();
        ResetLowThresholdTextColorStyle();
        ResetMediumThresholdTextColorStyle();
        ResetHighThresholdTextColorStyle();
        ResetLowThresholdTextColorAlign();
        ResetMediumThresholdTextColorAlign();
        ResetHighThresholdTextColorAlign();
        ResetLowThresholdTextColorAngle();
        ResetMediumThresholdTextColorAngle();
        ResetHighThresholdTextColorAngle();
        _originalLowThresholdTextColor = Color.Empty;
        _originalMediumThresholdTextColor = Color.Empty;
        _originalHighThresholdTextColor = Color.Empty;
    }

    #endregion
}
