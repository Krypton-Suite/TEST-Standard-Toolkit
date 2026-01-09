#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Contains data for customizing a ribbon notification banner.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class RibbonNotificationBannerData
{
    #region Instance Fields
    private string _headingText = string.Empty;
    private string _messageText = string.Empty;
    private Image? _icon;
    private Color? _backgroundColor;
    private Color? _foregroundColor;
    private Color? _headingForegroundColor;
    private Font? _headingFont;
    private Font? _messageFont;
    private string _actionButtonText = string.Empty;
    private Image? _actionButtonImage;
    private bool _showDismissButton = true;
    private Padding _padding = new Padding(12, 8, 12, 8);
    private int _iconSpacing = 12;
    private int _headingMessageSpacing = 4;
    private int _messageActionSpacing = 12;
    private int _actionDismissSpacing = 8;
    private ContentAlignment _iconAlignment = ContentAlignment.MiddleLeft;
    private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
    private int? _minimumHeight;
    private int? _maximumHeight;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonNotificationBannerData class.
    /// </summary>
    public RibbonNotificationBannerData()
    {
    }

    /// <summary>
    /// Initialize a new instance of the RibbonNotificationBannerData class.
    /// </summary>
    /// <param name="headingText">The heading text.</param>
    /// <param name="messageText">The message text.</param>
    public RibbonNotificationBannerData(string headingText, string messageText)
    {
        _headingText = headingText;
        _messageText = messageText;
    }
    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the heading text displayed in bold.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The heading text displayed in bold.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string HeadingText
    {
        get => _headingText;
        set => _headingText = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the message text displayed below the heading.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The message text displayed below the heading.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string MessageText
    {
        get => _messageText;
        set => _messageText = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the icon displayed on the left side of the banner.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The icon displayed on the left side of the banner.")]
    [DefaultValue(null)]
    public Image? Icon
    {
        get => _icon;
        set => _icon = value;
    }

    /// <summary>
    /// Gets or sets the background color of the banner. Null uses default.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The background color of the banner. Null uses default.")]
    [DefaultValue(null)]
    public Color? BackgroundColor
    {
        get => _backgroundColor;
        set => _backgroundColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the message text. Null uses default.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The foreground color for the message text. Null uses default.")]
    [DefaultValue(null)]
    public Color? ForegroundColor
    {
        get => _foregroundColor;
        set => _foregroundColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the heading text. Null uses default.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The foreground color for the heading text. Null uses default.")]
    [DefaultValue(null)]
    public Color? HeadingForegroundColor
    {
        get => _headingForegroundColor;
        set => _headingForegroundColor = value;
    }

    /// <summary>
    /// Gets or sets the font for the heading text. Null uses default.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The font for the heading text. Null uses default.")]
    [DefaultValue(null)]
    public Font? HeadingFont
    {
        get => _headingFont;
        set => _headingFont = value;
    }

    /// <summary>
    /// Gets or sets the font for the message text. Null uses default.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The font for the message text. Null uses default.")]
    [DefaultValue(null)]
    public Font? MessageFont
    {
        get => _messageFont;
        set => _messageFont = value;
    }

    /// <summary>
    /// Gets or sets the text for the action button. Empty string hides the button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The text for the action button. Empty string hides the button.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string ActionButtonText
    {
        get => _actionButtonText;
        set => _actionButtonText = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the image for the action button. Null uses text only.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The image for the action button. Null uses text only.")]
    [DefaultValue(null)]
    public Image? ActionButtonImage
    {
        get => _actionButtonImage;
        set => _actionButtonImage = value;
    }

    /// <summary>
    /// Gets or sets whether to show the dismiss (X) button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Whether to show the dismiss (X) button.")]
    [DefaultValue(true)]
    public bool ShowDismissButton
    {
        get => _showDismissButton;
        set => _showDismissButton = value;
    }

    /// <summary>
    /// Gets or sets the padding around the banner content.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The padding around the banner content.")]
    [DefaultValue(typeof(Padding), "12,8,12,8")]
    public Padding Padding
    {
        get => _padding;
        set => _padding = value;
    }

    /// <summary>
    /// Gets or sets the spacing between the icon and text.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The spacing between the icon and text.")]
    [DefaultValue(12)]
    public int IconSpacing
    {
        get => _iconSpacing;
        set => _iconSpacing = Math.Max(0, value);
    }

    /// <summary>
    /// Gets or sets the spacing between the heading and message text.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The spacing between the heading and message text.")]
    [DefaultValue(4)]
    public int HeadingMessageSpacing
    {
        get => _headingMessageSpacing;
        set => _headingMessageSpacing = Math.Max(0, value);
    }

    /// <summary>
    /// Gets or sets the spacing between the message text and action button.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The spacing between the message text and action button.")]
    [DefaultValue(12)]
    public int MessageActionSpacing
    {
        get => _messageActionSpacing;
        set => _messageActionSpacing = Math.Max(0, value);
    }

    /// <summary>
    /// Gets or sets the spacing between the action button and dismiss button.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The spacing between the action button and dismiss button.")]
    [DefaultValue(8)]
    public int ActionDismissSpacing
    {
        get => _actionDismissSpacing;
        set => _actionDismissSpacing = Math.Max(0, value);
    }

    /// <summary>
    /// Gets or sets the alignment of the icon.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The alignment of the icon.")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment IconAlignment
    {
        get => _iconAlignment;
        set => _iconAlignment = value;
    }

    /// <summary>
    /// Gets or sets the alignment of the text content.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The alignment of the text content.")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment TextAlignment
    {
        get => _textAlignment;
        set => _textAlignment = value;
    }

    /// <summary>
    /// Gets or sets the minimum height of the banner. Null uses default.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The minimum height of the banner. Null uses default.")]
    [DefaultValue(null)]
    public int? MinimumHeight
    {
        get => _minimumHeight;
        set => _minimumHeight = value.HasValue && value.Value > 0 ? value : null;
    }

    /// <summary>
    /// Gets or sets the maximum height of the banner. Null uses default.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The maximum height of the banner. Null uses default.")]
    [DefaultValue(null)]
    public int? MaximumHeight
    {
        get => _maximumHeight;
        set => _maximumHeight = value.HasValue && value.Value > 0 ? value : null;
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        _headingText = string.Empty;
        _messageText = string.Empty;
        _icon = null;
        _backgroundColor = null;
        _foregroundColor = null;
        _headingForegroundColor = null;
        _headingFont = null;
        _messageFont = null;
        _actionButtonText = string.Empty;
        _actionButtonImage = null;
        _showDismissButton = true;
        _padding = new Padding(12, 8, 12, 8);
        _iconSpacing = 12;
        _headingMessageSpacing = 4;
        _messageActionSpacing = 12;
        _actionDismissSpacing = 8;
        _iconAlignment = ContentAlignment.MiddleLeft;
        _textAlignment = ContentAlignment.MiddleLeft;
        _minimumHeight = null;
        _maximumHeight = null;
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    public override string ToString()
    {
        if (!string.IsNullOrEmpty(_headingText))
        {
            return _headingText;
        }

        if (!string.IsNullOrEmpty(_messageText))
        {
            return _messageText;
        }

        return "(Empty)";
    }
    #endregion
}
