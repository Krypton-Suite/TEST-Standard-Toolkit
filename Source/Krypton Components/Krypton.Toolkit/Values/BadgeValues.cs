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
    #endregion

    #region Instance Fields
    private string? _text;
    private Image? _image;
    private Color _badgeColor;
    private Color _textColor;
    private BadgePosition _position;
    private bool _visible;
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
        _visible = false;
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
                                      (Visible == false);

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
}
