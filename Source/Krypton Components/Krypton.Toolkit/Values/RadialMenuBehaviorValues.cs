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
/// Storage for radial menu behavior properties.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class RadialMenuBehaviorValues : Storage
{
    #region Instance Fields
    private readonly KryptonRadialMenu _owner;
    private bool _allowDrag;
    private bool _allowFloat;
    private Form? _mdiParent;
    private bool _enableAnimations;
    private int _animationDuration;
    private float _hoverScaleFactor;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RadialMenuBehaviorValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    internal RadialMenuBehaviorValues(KryptonRadialMenu owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _allowDrag = false;
        _allowFloat = false;
        _mdiParent = null;
        _enableAnimations = true;
        _animationDuration = 300;
        _hoverScaleFactor = 1.1f;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets a value indicating whether the menu can be dragged.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu can be dragged within its container.")]
    [DefaultValue(false)]
    public bool AllowDrag
    {
        get => _allowDrag;
        set => _allowDrag = value;
    }

    private bool ShouldSerializeAllowDrag() => _allowDrag;

    private void ResetAllowDrag() => AllowDrag = false;

    /// <summary>
    /// Gets or sets a value indicating whether the menu can be floated into its own window.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu can be floated into its own window.")]
    [DefaultValue(false)]
    public bool AllowFloat
    {
        get => _allowFloat;
        set => _allowFloat = value;
    }

    private bool ShouldSerializeAllowFloat() => _allowFloat;

    private void ResetAllowFloat() => AllowFloat = false;

    /// <summary>
    /// Gets or sets the MDI parent form for floating windows.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"MDI parent form for floating windows. If set, floating windows will be MDI children.")]
    [DefaultValue(null)]
    public Form? MdiParent
    {
        get => _mdiParent;
        set => _mdiParent = value;
    }

    private bool ShouldSerializeMdiParent() => _mdiParent != null;

    private void ResetMdiParent() => MdiParent = null;

    /// <summary>
    /// Gets or sets a value indicating whether animations are enabled.
    /// </summary>
    [Category(@"Animation")]
    [Description(@"Indicates whether animations are enabled for menu items.")]
    [DefaultValue(true)]
    public bool EnableAnimations
    {
        get => _enableAnimations;
        set
        {
            if (_enableAnimations != value)
            {
                _enableAnimations = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeEnableAnimations() => !_enableAnimations;

    private void ResetEnableAnimations() => EnableAnimations = true;

    /// <summary>
    /// Gets or sets the animation duration in milliseconds.
    /// </summary>
    [Category(@"Animation")]
    [Description(@"Animation duration in milliseconds.")]
    [DefaultValue(300)]
    public int AnimationDuration
    {
        get => _animationDuration;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(AnimationDuration), @"Animation duration cannot be less than zero");
            }
            if (_animationDuration != value)
            {
                _animationDuration = value;
            }
        }
    }

    private bool ShouldSerializeAnimationDuration() => _animationDuration != 300;

    private void ResetAnimationDuration() => AnimationDuration = 300;

    /// <summary>
    /// Gets or sets the scale factor for hover animations (1.0 = no scaling).
    /// </summary>
    [Category(@"Animation")]
    [Description(@"Scale factor for hover animations (1.0 = no scaling).")]
    [DefaultValue(1.1f)]
    public float HoverScaleFactor
    {
        get => _hoverScaleFactor;
        set
        {
            if (value < 1.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(HoverScaleFactor), @"Hover scale factor must be at least 1.0");
            }
            if (_hoverScaleFactor != value)
            {
                _hoverScaleFactor = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeHoverScaleFactor() => Math.Abs(_hoverScaleFactor - 1.1f) > 0.01f;

    private void ResetHoverScaleFactor() => HoverScaleFactor = 1.1f;

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetAllowDrag();
        ResetAllowFloat();
        ResetMdiParent();
        ResetEnableAnimations();
        ResetAnimationDuration();
        ResetHoverScaleFactor();
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeAllowDrag()
                                    && !ShouldSerializeAllowFloat()
                                    && !ShouldSerializeMdiParent()
                                    && !ShouldSerializeEnableAnimations()
                                    && !ShouldSerializeAnimationDuration()
                                    && !ShouldSerializeHoverScaleFactor();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? @"(Default)" : @"(Modified)";
    #endregion
}

