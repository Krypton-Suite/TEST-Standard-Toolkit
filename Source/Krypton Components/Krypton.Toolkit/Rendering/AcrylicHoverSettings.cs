#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;

namespace Krypton.Toolkit;

/// <summary>
/// Defines performance profiles for Acrylic hover effects.
/// </summary>
public enum AcrylicHoverQuality
{
    /// <summary>
    /// High quality rendering with full gradient effects.
    /// </summary>
    HighQuality,

    /// <summary>
    /// Balanced quality and performance.
    /// </summary>
    Balanced,

    /// <summary>
    /// Performance optimized with simplified effects.
    /// </summary>
    Performance
}

/// <summary>
/// Settings for Acrylic hover effects on interactive elements.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class AcrylicHoverSettings
{
    #region Instance Fields
    private bool _enabled = true;
    private float _intensity = 1.0f;
    private Color? _lightColor;
    private Color? _darkColor;
    private AcrylicHoverQuality _quality = AcrylicHoverQuality.Balanced;
    private bool _enableAnimation = false;
    private int _animationDuration = 200;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the AcrylicHoverSettings class.
    /// </summary>
    public AcrylicHoverSettings()
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => 
        $"Enabled:{Enabled} Intensity:{Intensity} Quality:{Quality}";
    #endregion

    #region Enabled
    /// <summary>
    /// Gets or sets whether Acrylic hover effects are enabled.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Enable or disable Acrylic hover effects globally.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    private bool ShouldSerializeEnabled() => !_enabled;

    /// <summary>
    /// Resets the Enabled property to its default value.
    /// </summary>
    public void ResetEnabled() => _enabled = true;
    #endregion

    #region Intensity
    /// <summary>
    /// Gets or sets the intensity of the Acrylic hover effect (0.0 to 2.0).
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Intensity of the Acrylic hover effect. Range: 0.0 (disabled) to 2.0 (maximum).")]
    [DefaultValue(1.0f)]
    public float Intensity
    {
        get => _intensity;
        set => _intensity = Math.Max(0.0f, Math.Min(2.0f, value));
    }

    private bool ShouldSerializeIntensity() => Math.Abs(_intensity - 1.0f) > 0.001f;

    /// <summary>
    /// Resets the Intensity property to its default value.
    /// </summary>
    public void ResetIntensity() => _intensity = 1.0f;
    #endregion

    #region LightColor
    /// <summary>
    /// Gets or sets a custom light color for the center of the hover effect.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Custom light color for the center of the hover effect. Null uses automatic calculation.")]
    [DefaultValue(null)]
    public Color? LightColor
    {
        get => _lightColor;
        set => _lightColor = value;
    }

    private bool ShouldSerializeLightColor() => _lightColor.HasValue;

    /// <summary>
    /// Resets the LightColor property to its default value.
    /// </summary>
    public void ResetLightColor() => _lightColor = null;
    #endregion

    #region DarkColor
    /// <summary>
    /// Gets or sets a custom dark color for the edges of the hover effect.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Custom dark color for the edges of the hover effect. Null uses automatic calculation.")]
    [DefaultValue(null)]
    public Color? DarkColor
    {
        get => _darkColor;
        set => _darkColor = value;
    }

    private bool ShouldSerializeDarkColor() => _darkColor.HasValue;

    /// <summary>
    /// Resets the DarkColor property to its default value.
    /// </summary>
    public void ResetDarkColor() => _darkColor = null;
    #endregion

    #region Quality
    /// <summary>
    /// Gets or sets the quality profile for Acrylic hover effects.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Quality profile for Acrylic hover effects. Higher quality uses more resources.")]
    [DefaultValue(AcrylicHoverQuality.Balanced)]
    public AcrylicHoverQuality Quality
    {
        get => _quality;
        set => _quality = value;
    }

    private bool ShouldSerializeQuality() => _quality != AcrylicHoverQuality.Balanced;

    /// <summary>
    /// Resets the Quality property to its default value.
    /// </summary>
    public void ResetQuality() => _quality = AcrylicHoverQuality.Balanced;
    #endregion

    #region EnableAnimation
    /// <summary>
    /// Gets or sets whether smooth animation transitions are enabled.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Enable smooth animation transitions when entering/exiting hover state.")]
    [DefaultValue(false)]
    public bool EnableAnimation
    {
        get => _enableAnimation;
        set => _enableAnimation = value;
    }

    private bool ShouldSerializeEnableAnimation() => _enableAnimation;

    /// <summary>
    /// Resets the EnableAnimation property to its default value.
    /// </summary>
    public void ResetEnableAnimation() => _enableAnimation = false;
    #endregion

    #region AnimationDuration
    /// <summary>
    /// Gets or sets the duration of animation transitions in milliseconds.
    /// </summary>
    [Category(@"Acrylic Hover")]
    [Description(@"Duration of animation transitions in milliseconds. Only used when EnableAnimation is true.")]
    [DefaultValue(200)]
    public int AnimationDuration
    {
        get => _animationDuration;
        set => _animationDuration = Math.Max(0, Math.Min(1000, value));
    }

    private bool ShouldSerializeAnimationDuration() => _animationDuration != 200;

    /// <summary>
    /// Resets the AnimationDuration property to its default value.
    /// </summary>
    public void ResetAnimationDuration() => _animationDuration = 200;
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all settings are at their default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => 
        _enabled == true &&
        Math.Abs(_intensity - 1.0f) < 0.001f &&
        !_lightColor.HasValue &&
        !_darkColor.HasValue &&
        _quality == AcrylicHoverQuality.Balanced &&
        !_enableAnimation &&
        _animationDuration == 200;

    /// <summary>
    /// Resets all settings to their default values.
    /// </summary>
    public void Reset()
    {
        ResetEnabled();
        ResetIntensity();
        ResetLightColor();
        ResetDarkColor();
        ResetQuality();
        ResetEnableAnimation();
        ResetAnimationDuration();
    }
    #endregion
}

