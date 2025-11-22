#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A true split button that combines a main button action with a dropdown menu.
/// Clicking the main button area triggers the default action, while clicking the dropdown arrow shows a menu.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"A true split button that combines a main button action with a dropdown menu.")]
public class KryptonSplitButton : KryptonDropButton
{
    #region Events
    /// <summary>
    /// Occurs when the dropdown arrow portion of the split button is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the dropdown arrow portion of the split button is clicked.")]
    public event EventHandler? DropDownClick;

    /// <summary>
    /// Raises the DropDownClick event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnDropDownClick(EventArgs e) => DropDownClick?.Invoke(this, e);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitButton class.
    /// </summary>
    public KryptonSplitButton()
    {
        // Ensure split button is always enabled
        Splitter = true;
        _drawButton.DropDown = true;

        // Hook into the DropDown event to raise DropDownClick
        base.DropDown += OnBaseDropDown;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets a value indicating whether the split button shows a dropdown arrow.
    /// For KryptonSplitButton, this is always true and cannot be changed.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public new bool Splitter
    {
        get => base.Splitter;
        set
        {
            // Split button always has splitter enabled
            if (!value)
            {
                base.Splitter = true;
            }
        }
    }

    private bool ShouldSerializeSplitter() => false;

    /// <summary>
    /// Gets or sets a value indicating whether the button shows a dropdown.
    /// For KryptonSplitButton, this is always true and cannot be changed.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public new bool DropDown
    {
        get => _drawButton.DropDown;
        set
        {
            // Split button always has dropdown enabled
            if (!value)
            {
                _drawButton.DropDown = true;
            }
        }
    }

    private bool ShouldSerializeDropDown() => false;

    /// <summary>
    /// Gets the rectangle that represents the dropdown arrow area of the split button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle DropDownRectangle => _drawButton.SplitRectangle;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Only raise Click if the click was on the main button area, not the dropdown
        // The base class handles this logic via SplitRectangle
        base.OnClick(e);
    }
    #endregion

    #region Implementation
    private void OnBaseDropDown(object? sender, ContextPositionMenuArgs e)
    {
        // Raise the DropDownClick event for split button specific handling
        // This provides a simpler event signature than the base DropDown event
        OnDropDownClick(EventArgs.Empty);
    }
    #endregion
}

