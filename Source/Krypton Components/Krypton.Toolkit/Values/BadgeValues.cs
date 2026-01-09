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
/// Storage for badge value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeValues : Storage
{
    #region Static Fields
    private const string DEFAULT_TEXT = "";
    private static readonly Color _defaultBadgeColor = Color.Red;
    private static readonly Color _defaultTextColor = Color.White;
    private static readonly Font? _defaultFont = null; // null means use default font
    private const int DEFAULT_BADGE_DIAMETER = 0; // 0 means auto-size
    private const bool DEFAULT_AUTO_SHOW_HIDE_BADGE = false;
    #endregion

    #region Instance Fields
    private string? _text;
    private Image? _image;
    private Color _badgeColor;
    private Color _textColor;
    private BadgePosition _position;
    private BadgeShape _shape;
    private BadgeAnimation _animation;
    private Font? _font;
    private bool _visible;
    private int _badgeDiameter;
    private bool _autoShowHideBadge;
    private readonly BadgeBorderValues _border;
    private readonly BadgeOverflowValues _overflow;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the BadgeValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public BadgeValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set initial values
        _text = DEFAULT_TEXT;
        _image = null;
        _badgeColor = _defaultBadgeColor;
        _textColor = _defaultTextColor;
        _position = BadgePosition.TopRight;
        _shape = BadgeShape.Circle;
        _animation = BadgeAnimation.None;
        _font = _defaultFont;
        _visible = false;
        _badgeDiameter = DEFAULT_BADGE_DIAMETER;
        _autoShowHideBadge = DEFAULT_AUTO_SHOW_HIDE_BADGE;
        
        // Initialize nested expandable objects
        _border = new BadgeBorderValues(needPaint);
        _overflow = new BadgeOverflowValues(needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Text == DEFAULT_TEXT) &&
                                      (Image == null) &&
                                      (BadgeColor == _defaultBadgeColor) &&
                                      (TextColor == _defaultTextColor) &&
                                      (Position == BadgePosition.TopRight) &&
                                      (Shape == BadgeShape.Circle) &&
                                      (Animation == BadgeAnimation.None) &&
                                      (Font == _defaultFont) &&
                                      (Visible == false) &&
                                      (BadgeDiameter == DEFAULT_BADGE_DIAMETER) &&
                                      (AutoShowHideBadge == DEFAULT_AUTO_SHOW_HIDE_BADGE) &&
                                      Border.IsDefault &&
                                      Overflow.IsDefault;

    #endregion

    #region Text
    /// <summary>
    /// Gets and sets the badge text.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The text to display on the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string Text
    {
        get => _text ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set
        {
            if (_text != value)
            {
                _text = value;
                
                // Update visibility if auto-show/hide is enabled
                if (_autoShowHideBadge)
                {
                    UpdateVisibilityFromContent();
                }
                
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeText() => Text != DEFAULT_TEXT;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public void ResetText() => Text = DEFAULT_TEXT;
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the badge image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The image to display on the badge. If set, the image will be displayed instead of text.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (_image != value)
            {
                _image = value;
                
                // Update visibility if auto-show/hide is enabled
                if (_autoShowHideBadge)
                {
                    UpdateVisibilityFromContent();
                }
                
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeImage() => Image != null;

    /// <summary>
    /// Resets the Image property to its default value.
    /// </summary>
    public void ResetImage() => Image = null;
    #endregion

    #region BadgeColor
    /// <summary>
    /// Gets and sets the badge background color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The background color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color BadgeColor
    {
        get => _badgeColor;
        set
        {
            if (_badgeColor != value)
            {
                _badgeColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeColor() => BadgeColor != _defaultBadgeColor;

    /// <summary>
    /// Resets the BadgeColor property to its default value.
    /// </summary>
    public void ResetBadgeColor() => BadgeColor = _defaultBadgeColor;
    #endregion

    #region TextColor
    /// <summary>
    /// Gets and sets the badge text color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The text color of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [KryptonDefaultColor]
    public Color TextColor
    {
        get => _textColor;
        set
        {
            if (_textColor != value)
            {
                _textColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeTextColor() => TextColor != _defaultTextColor;

    /// <summary>
    /// Resets the TextColor property to its default value.
    /// </summary>
    public void ResetTextColor() => TextColor = _defaultTextColor;
    #endregion

    #region Position
    /// <summary>
    /// Gets and sets the badge position on the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The position of the badge on the button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgePosition.TopRight)]
    public BadgePosition Position
    {
        get => _position;
        set
        {
            if (_position != value)
            {
                _position = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePosition() => Position != BadgePosition.TopRight;

    /// <summary>
    /// Resets the Position property to its default value.
    /// </summary>
    public void ResetPosition() => Position = BadgePosition.TopRight;
    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets whether the badge is visible.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Whether the badge is visible.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool Visible
    {
        get => _visible;
        set
        {
            // If AutoShowHideBadge is enabled, ignore manual changes to Visible
            // Visibility is automatically managed based on content
            if (_autoShowHideBadge)
            {
                return;
            }
            
            if (_visible != value)
            {
                _visible = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeVisible() => Visible != false;

    /// <summary>
    /// Resets the Visible property to its default value.
    /// </summary>
    public void ResetVisible() => Visible = false;
    #endregion

    #region Font
    /// <summary>
    /// Gets and sets the badge text font.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The font used to display badge text. If null, uses default font (Segoe UI 7.5pt Bold).")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Font? Font
    {
        get => _font;
        set
        {
            if (_font != value)
            {
                _font = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeFont() => Font != null;

    /// <summary>
    /// Resets the Font property to its default value.
    /// </summary>
    public void ResetFont() => Font = null;
    #endregion

    #region Shape
    /// <summary>
    /// Gets and sets the badge shape.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The shape of the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgeShape.Circle)]
    public BadgeShape Shape
    {
        get => _shape;
        set
        {
            if (_shape != value)
            {
                _shape = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShape() => Shape != BadgeShape.Circle;

    /// <summary>
    /// Resets the Shape property to its default value.
    /// </summary>
    public void ResetShape() => Shape = BadgeShape.Circle;
    #endregion

    #region Animation
    /// <summary>
    /// Gets and sets the badge animation type.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The animation type for the badge.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgeAnimation.None)]
    public BadgeAnimation Animation
    {
        get => _animation;
        set
        {
            if (_animation != value)
            {
                _animation = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAnimation() => Animation != BadgeAnimation.None;

    /// <summary>
    /// Resets the Animation property to its default value.
    /// </summary>
    public void ResetAnimation() => Animation = BadgeAnimation.None;
    #endregion

    #region Border
    /// <summary>
    /// Gets access to the badge border values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Badge border values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeBorderValues Border => _border;

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Resets the Border property to its default value.
    /// </summary>
    public void ResetBorder() => Border.Reset();
    #endregion

    #region BadgeDiameter
    /// <summary>
    /// Gets and sets the badge diameter (size for all shapes). 0 means auto-size based on content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The diameter/size of the badge for all shapes. 0 means auto-size based on content.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
    public int BadgeDiameter
    {
        get => _badgeDiameter;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_badgeDiameter != value)
            {
                _badgeDiameter = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeDiameter() => BadgeDiameter != DEFAULT_BADGE_DIAMETER;

    /// <summary>
    /// Resets the BadgeDiameter property to its default value.
    /// </summary>
    public void ResetBadgeDiameter() => BadgeDiameter = DEFAULT_BADGE_DIAMETER;
    #endregion

    #region Overflow
    /// <summary>
    /// Gets access to the badge overflow values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Badge overflow values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeOverflowValues Overflow => _overflow;

    private bool ShouldSerializeOverflow() => !Overflow.IsDefault;

    /// <summary>
    /// Resets the Overflow property to its default value.
    /// </summary>
    public void ResetOverflow() => Overflow.Reset();
    #endregion

    #region AutoShowHideBadge
    /// <summary>
    /// Gets and sets whether the badge should automatically show when it has content (text or image) and hide when empty.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When enabled, the badge automatically shows when it has content (text or image) and hides when empty.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool AutoShowHideBadge
    {
        get => _autoShowHideBadge;
        set
        {
            if (_autoShowHideBadge != value)
            {
                _autoShowHideBadge = value;
                
                // If enabled, update visibility based on current content
                if (value)
                {
                    UpdateVisibilityFromContent();
                }
                
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeAutoShowHideBadge() => AutoShowHideBadge != DEFAULT_AUTO_SHOW_HIDE_BADGE;

    /// <summary>
    /// Resets the AutoShowHideBadge property to its default value.
    /// </summary>
    public void ResetAutoShowHideBadge() => AutoShowHideBadge = DEFAULT_AUTO_SHOW_HIDE_BADGE;

    /// <summary>
    /// Updates the Visible property based on whether the badge has content.
    /// </summary>
    private void UpdateVisibilityFromContent()
    {
        bool hasContent = !string.IsNullOrEmpty(Text) || Image != null;
        // Use the property setter to ensure proper notification
        if (_visible != hasContent)
        {
            _visible = hasContent;
            // Don't call PerformNeedPaint here to avoid recursion - it will be called by the property setter that triggered this update
        }
    }
    #endregion
}
