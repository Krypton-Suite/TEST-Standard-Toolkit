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
    private int _thresholdValue;
    private bool _useOppositeTextColors;
    private readonly ProgressBarThresholdCommonBase _commonBase;
    private readonly ProgressBarThresholdRegionAppearance _low;
    private readonly ProgressBarThresholdRegionAppearance _medium;
    private readonly ProgressBarThresholdRegionAppearance _high;

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

        _commonBase = new ProgressBarThresholdCommonBase();
        _low = new ProgressBarThresholdRegionAppearance(this, Color.Red);
        _medium = new ProgressBarThresholdRegionAppearance(this, Color.Orange);
        _high = new ProgressBarThresholdRegionAppearance(this, Color.Green);

        Reset();
    }

    /// <summary>
    /// Notifies the owner that a region's colours or images changed. Called by ProgressBarThresholdRegionAppearance.
    /// </summary>
    internal void NotifyRegionChanged(ProgressBarThresholdRegionAppearance? region)
    {
        _owner.UpdateThresholdColor();
        PerformNeedPaint(true);
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
                                      _thresholdValue == 50 &&
                                      _low.IsDefault &&
                                      _medium.IsDefault &&
                                      _high.IsDefault &&
                                      _commonBase.IsDefault;

    #endregion

    #region UseThresholdColors

    /// <summary>
    /// Gets or sets whether to use threshold-based colors for the progress bar.
    /// When enabled, the color changes based on ThresholdValue (Low color below threshold, High color at or above threshold).
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
    /// Gets or sets whether to automatically calculate threshold value based on Minimum and Maximum.
    /// When enabled, threshold is calculated as: ThresholdValue = Minimum + (Maximum - Minimum) / 2 (midpoint).
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
    /// Calculates threshold value based on the owner's Minimum and Maximum.
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
            // Calculate threshold as midpoint: Minimum + (Maximum - Minimum) / 2
            _thresholdValue = _owner.Minimum + range / 2;
            
            // Ensure threshold is within valid range
            if (_thresholdValue < _owner.Minimum)
            {
                _thresholdValue = _owner.Minimum;
            }
            if (_thresholdValue > _owner.Maximum)
            {
                _thresholdValue = _owner.Maximum;
            }
            
            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
        else
        {
            // Range is zero or invalid, set threshold to minimum
            _thresholdValue = _owner.Minimum;
        }
    }

    #endregion

    #region ThresholdValue

    /// <summary>
    /// Gets or sets the threshold value. When the progress value is below this threshold, the low threshold color is used.
    /// When the progress value is at or above this threshold, the high threshold color is used.
    /// This property is read-only when AutoCalculateThresholds is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The threshold value. Progress below this uses the low threshold color; at or above uses the high threshold color. Read-only when AutoCalculateThresholds is enabled.")]
    [DefaultValue(50)]
    public int ThresholdValue
    {
        get => _thresholdValue;
        set
        {
            if (_autoCalculateThresholds)
            {
                throw new InvalidOperationException(@"ThresholdValue cannot be set when AutoCalculateThresholds is enabled.");
            }

            if (value < 0 || (_owner != null && value > _owner.Maximum))
            {
                throw new ArgumentOutOfRangeException(nameof(ThresholdValue), value, @"ThresholdValue must be between 0 and Maximum.");
            }

            if (_thresholdValue == value)
            {
                return;
            }

            _thresholdValue = value;

            if (_useThresholdColors)
            {
                _owner.UpdateThresholdColor();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeThresholdValue() => ThresholdValue != 50;

    /// <summary>
    /// Resets the ThresholdValue property to its default value.
    /// </summary>
    public void ResetThresholdValue() => ThresholdValue = 50;

    #endregion

    #region CommonBase

    /// <summary>
    /// Gets the common base template for colours and images. Set values here, then call AssignFromCommonBaseToLow/Medium/High/All to copy to regions.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Common base template. Set colours and images here, then assign to Low, Medium, or High via AssignFromCommonBaseToLow/Medium/High/All.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarThresholdCommonBase CommonBase => _commonBase;

    private bool ShouldSerializeCommonBase() => !_commonBase.IsDefault;

    /// <summary>
    /// Resets the CommonBase to its default value.
    /// </summary>
    public void ResetCommonBase() => _commonBase.Reset();

    #endregion

    #region Low

    /// <summary>
    /// Gets the colours and images used when progress is below the threshold value.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Colours and images when progress is below the threshold value.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarThresholdRegionAppearance Low => _low;

    private bool ShouldSerializeLow() => !_low.IsDefault;

    /// <summary>
    /// Resets the Low region to its default value.
    /// </summary>
    public void ResetLow() => _low.Reset();

    #endregion

    #region Medium

    /// <summary>
    /// Gets the colours and images used when progress is between low and high thresholds. (Note: Medium region is not currently used with single ThresholdValue.)
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Colours and images when progress is between low and high thresholds. (Note: Medium region is not currently used with single ThresholdValue.)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarThresholdRegionAppearance Medium => _medium;

    private bool ShouldSerializeMedium() => !_medium.IsDefault;

    /// <summary>
    /// Resets the Medium region to its default value.
    /// </summary>
    public void ResetMedium() => _medium.Reset();

    #endregion

    #region High

    /// <summary>
    /// Gets the colours and images used when progress is at or above the threshold value.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Colours and images when progress is at or above the threshold value.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarThresholdRegionAppearance High => _high;

    private bool ShouldSerializeHigh() => !_high.IsDefault;

    /// <summary>
    /// Resets the High region to its default value.
    /// </summary>
    public void ResetHigh() => _high.Reset();

    #endregion

    #region AssignFromCommonBase

    /// <summary>
    /// Copies CommonBase colours and images to the Low region.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Copies the common base colours and images to the Low threshold region.")]
    public void AssignFromCommonBaseToLow() => _low.AssignFrom(_commonBase);

    /// <summary>
    /// Copies CommonBase colours and images to the Medium region.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Copies the common base colours and images to the Medium threshold region.")]
    public void AssignFromCommonBaseToMedium() => _medium.AssignFrom(_commonBase);

    /// <summary>
    /// Copies CommonBase colours and images to the High region.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Copies the common base colours and images to the High threshold region.")]
    public void AssignFromCommonBaseToHigh() => _high.AssignFrom(_commonBase);

    /// <summary>
    /// Copies CommonBase colours and images to all three regions (Low, Medium, High).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Copies the common base colours and images to all threshold regions.")]
    public void AssignFromCommonBaseToAll()
    {
        _low.AssignFrom(_commonBase);
        _medium.AssignFrom(_commonBase);
        _high.AssignFrom(_commonBase);
    }

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
                _low.EnableOppositeTextColors();
                _medium.EnableOppositeTextColors();
                _high.EnableOppositeTextColors();
            }
            else
            {
                _low.RestoreOriginalTextColor();
                _medium.RestoreOriginalTextColor();
                _high.RestoreOriginalTextColor();
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
        ResetThresholdValue();
        ResetCommonBase();
        ResetLow();
        ResetMedium();
        ResetHigh();
    }

    #endregion
}
