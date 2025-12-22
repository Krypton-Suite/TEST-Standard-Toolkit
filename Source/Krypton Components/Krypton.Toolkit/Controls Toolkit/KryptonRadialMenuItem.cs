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
/// Represents a single item in a radial menu.
/// </summary>
public class KryptonRadialMenuItem
{
    #region Instance Fields
    private string _text;
    private Image? _image;
    private object? _tag;
    private bool _enabled;
    private int _level;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the menu item is clicked.
    /// </summary>
    public event EventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRadialMenuItem class.
    /// </summary>
    public KryptonRadialMenuItem()
    {
        _text = string.Empty;
        _image = null;
        _tag = null;
        _enabled = true;
        _level = 0;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonRadialMenuItem class.
    /// </summary>
    /// <param name="text">Text to display on the menu item.</param>
    public KryptonRadialMenuItem(string text)
    {
        _text = text;
        _image = null;
        _tag = null;
        _enabled = true;
        _level = 0;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonRadialMenuItem class.
    /// </summary>
    /// <param name="text">Text to display on the menu item.</param>
    /// <param name="image">Image to display on the menu item.</param>
    public KryptonRadialMenuItem(string text, Image? image)
    {
        _text = text;
        _image = image;
        _tag = null;
        _enabled = true;
        _level = 0;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the text displayed on the menu item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the menu item.")]
    [DefaultValue("")]
    public string Text
    {
        get => _text;
        set => _text = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the image displayed on the menu item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image displayed on the menu item.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set => _image = value;
    }

    /// <summary>
    /// Gets or sets user-defined data associated with the menu item.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the menu item.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag
    {
        get => _tag;
        set => _tag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the menu item is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu item is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    /// <summary>
    /// Gets or sets the level (ring) of the menu item. Level 0 is the innermost ring.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The level (ring) of the menu item. Level 0 is the innermost ring.")]
    [DefaultValue(0)]
    public int Level
    {
        get => _level;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Level), @"Level cannot be less than zero");
            }
            _level = value;
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    public virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
    #endregion
}

