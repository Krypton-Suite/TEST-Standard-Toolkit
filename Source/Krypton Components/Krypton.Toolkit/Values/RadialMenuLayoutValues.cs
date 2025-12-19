#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for radial menu layout properties.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class RadialMenuLayoutValues : Storage
{
    #region Instance Fields
    private readonly KryptonRadialMenu _owner;
    private int _innerRadius;
    private int _ringThickness;
    private int _centerRadius;
    private float _startAngle;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RadialMenuLayoutValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    internal RadialMenuLayoutValues(KryptonRadialMenu owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _innerRadius = 20;
        _ringThickness = 60;
        _centerRadius = 15;
        _startAngle = -90f;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the inner radius of the first ring (in pixels).
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Inner radius of the first ring in pixels.")]
    [DefaultValue(20)]
    public int InnerRadius
    {
        get => _innerRadius;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(InnerRadius), @"Inner radius cannot be less than zero");
            }
            if (_innerRadius != value)
            {
                _innerRadius = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeInnerRadius() => _innerRadius != 20;

    private void ResetInnerRadius() => InnerRadius = 20;

    /// <summary>
    /// Gets or sets the thickness of each ring (in pixels).
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Thickness of each ring in pixels.")]
    [DefaultValue(60)]
    public int RingThickness
    {
        get => _ringThickness;
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(RingThickness), @"Ring thickness must be at least 1");
            }
            if (_ringThickness != value)
            {
                _ringThickness = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeRingThickness() => _ringThickness != 60;

    private void ResetRingThickness() => RingThickness = 60;

    /// <summary>
    /// Gets or sets the radius of the center circle (in pixels).
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Radius of the center circle in pixels.")]
    [DefaultValue(15)]
    public int CenterRadius
    {
        get => _centerRadius;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(CenterRadius), @"Center radius cannot be less than zero");
            }
            if (_centerRadius != value)
            {
                _centerRadius = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeCenterRadius() => _centerRadius != 15;

    private void ResetCenterRadius() => CenterRadius = 15;

    /// <summary>
    /// Gets or sets the starting angle in degrees (0 is right, 90 is bottom, -90 is top).
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Starting angle in degrees (0 is right, 90 is bottom, -90 is top).")]
    [DefaultValue(-90f)]
    public float StartAngle
    {
        get => _startAngle;
        set
        {
            if (_startAngle != value)
            {
                _startAngle = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeStartAngle() => Math.Abs(_startAngle - (-90f)) > 0.01f;

    private void ResetStartAngle() => StartAngle = -90f;

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetInnerRadius();
        ResetRingThickness();
        ResetCenterRadius();
        ResetStartAngle();
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeInnerRadius()
                                   && !ShouldSerializeRingThickness()
                                   && !ShouldSerializeCenterRadius()
                                   && !ShouldSerializeStartAngle();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? @"(Default)" : @"(Modified)";
    #endregion
}

