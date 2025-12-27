#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using static Krypton.Toolkit.KryptonForm;

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonFormValues : Storage
{
    #region Instance Fields

    private bool _isInAdministratorMode;

    private bool _useDropShadow;

    private KryptonForm _owner;

    private PaletteRelativeAlign _formTitleAlign;

    private KryptonFormTitleStyle _titleStyle;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormValues"/> class.
    /// </summary>
    public KryptonFormValues(KryptonForm owner)
    {
        _owner = owner;
    }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public override bool IsDefault => IsInAdministratorMode == false &&
                                      UseDropShadow == false &&
                                      //ButtonSpec.Equals(new FormButtonSpecCollection(_owner)) &&
                                      FormTitleAlign == PaletteRelativeAlign.Near &&
                                      TitleStyle == KryptonFormTitleStyle.Inherit;

    #endregion

    #region Public

    /// <summary>Gets or sets a value indicating whether this instance is in administrator mode.</summary>
    /// <value><c>true</c> if this instance is in administrator mode; otherwise, <c>false</c>.</value>
    [Category(@"Appearance")]
    [Description(@"Is the user currently an administrator.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInAdministratorMode
    {
        get => _isInAdministratorMode;
        private set => _isInAdministratorMode = value;
    }

    /// <summary>
    /// Allows the use of drop shadow around the form.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Allows the use of drop shadow around the form.")]
    [DefaultValue(false)]
    [Obsolete("Deprecated - Only use if you are using Windows 7, 8 or 8.1.")]
    public bool UseDropShadow
    {
        get => _useDropShadow;

        set
        {
            _useDropShadow = value;

            UpdateDropShadowDraw(_useDropShadow);
        }
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormButtonSpecCollection ButtonSpecs { get; internal set; }

    /// <summary>
    /// Gets access to the minimize button spec.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecFormWindowMin ButtonSpecMin { get; internal set; }

    /// <summary>
    /// Gets access to the minimize button spec.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecFormWindowMax ButtonSpecMax { get; internal set; }

    /// <summary>
    /// Gets access to the minimize button spec.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecFormWindowClose ButtonSpecClose { get; internal set; }

    /// <summary>
    /// Gets and sets a value indicating if the border should be inert to changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool InertForm { get; set; }

    /// <summary>
    /// Gets and sets the header edge to display the button against.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The Form Title position, relative to available space")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteRelativeAlign.Near)]
    public PaletteRelativeAlign FormTitleAlign
    {
        get => _formTitleAlign;

        set
        {
            if (_formTitleAlign != value)
            {
                _formTitleAlign = value;
                PerformNeedPaint(true);
            }
        }
    }
    private bool ShouldSerializeFormTitleAlign() => _formTitleAlign != PaletteRelativeAlign.Near;
    private void ResetFormTitleAlign() => _formTitleAlign = PaletteRelativeAlign.Near;

    /// <summary>Arranges the current window title alignment.</summary>
    /// <value>The current window title alignment.</value>
    [Category(@"Appearance")]
    [DefaultValue(KryptonFormTitleStyle.Inherit),
     Description(@"Arranges the current window title alignment.")]
    public KryptonFormTitleStyle TitleStyle
    {
        get => _titleStyle;
        set
        {
            _titleStyle = value;
            UpdateTitleStyle(value);
        }
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Calls the method that draws the drop shadow around the form.
    /// </summary>
    /// <param name="useDropShadow">Use drop shadow user input value.</param>
    public void UpdateDropShadowDraw(bool useDropShadow)
    {
        if (useDropShadow)
        {
            DrawDropShadow();
        }

        _owner.Invalidate();
    }

    /// <summary>
    /// A wrapper that draws the drop shadow around the form.
    /// </summary>
    /// <returns>The shadow around the form.</returns>
    private void DrawDropShadow()
    {
        // Redraw
        _owner.Invalidate();
    }

    /// <summary>Updates the title style.</summary>
    /// <param name="titleStyle">The title style.</param>
    private void UpdateTitleStyle(KryptonFormTitleStyle titleStyle)
    {
        switch (titleStyle)
        {
            case KryptonFormTitleStyle.Inherit:
                FormTitleAlign = PaletteRelativeAlign.Inherit;
                break;
            case KryptonFormTitleStyle.Classic:
                FormTitleAlign = PaletteRelativeAlign.Near;
                break;
            case KryptonFormTitleStyle.Modern:
                FormTitleAlign = PaletteRelativeAlign.Center;
                break;
        }
    }

    public void Reset()
    {
        IsInAdministratorMode = false;
        UseDropShadow = false;
        FormTitleAlign = PaletteRelativeAlign.Near;
    }

    #endregion
}
