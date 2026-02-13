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

namespace Krypton.Toolkit;

/// <summary>
/// Provides pop-up or online Help for controls.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(HelpProvider))]
[DefaultEvent(nameof(HelpRequested))]
[DefaultProperty(nameof(HelpNamespace))]
[DesignerCategory(@"code")]
[Description(@"Provides pop-up or online Help for controls.")]
public class KryptonHelpProvider : Component, IExtenderProvider
{
    #region Instance Fields
    private HelpProvider? _helpProvider;
    private string _helpNamespace;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the user requests help for a control.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user requests help for a control.")]
    public event HelpEventHandler? HelpRequested;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHelpProvider class.
    /// </summary>
    public KryptonHelpProvider()
    {
        _helpNamespace = string.Empty;
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create the underlying HelpProvider
        _helpProvider = new HelpProvider();

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonHelpProvider class with a container.
    /// </summary>
    /// <param name="container">The IContainer that contains this component.</param>
    public KryptonHelpProvider(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            // Dispose the underlying HelpProvider
            _helpProvider?.Dispose();
            _helpProvider = null;
            _palette = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets or sets a value specifying the name of the Help file associated with this object.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The name of the Help file associated with this object.")]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string HelpNamespace
    {
        get => _helpNamespace;

        set
        {
            if (_helpNamespace != value)
            {
                _helpNamespace = value ?? string.Empty;
                if (_helpProvider != null)
                {
                    _helpProvider.HelpNamespace = _helpNamespace;
                }
            }
        }
    }

    /// <summary>
    /// Gets the underlying HelpProvider instance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HelpProvider? HelpProvider => _helpProvider;

    /// <summary>
    /// Returns the current Help keyword for the specified control.
    /// </summary>
    /// <param name="ctl">The Control from which to retrieve the Help keyword.</param>
    /// <returns>The Help keyword associated with this control, or null if the HelpProvider is currently configured to display the entire Help file or to provide help navigation support.</returns>
    public string? GetHelpKeyword(Control ctl) => _helpProvider?.GetHelpKeyword(ctl);

    /// <summary>
    /// Returns the current Help navigation type for the specified control.
    /// </summary>
    /// <param name="ctl">The Control from which to retrieve the Help navigation.</param>
    /// <returns>One of the HelpNavigator values.</returns>
    public HelpNavigator GetHelpNavigator(Control ctl) => _helpProvider?.GetHelpNavigator(ctl) ?? HelpNavigator.TableOfContents;

    /// <summary>
    /// Returns a value indicating whether Help is displayed for the specified control.
    /// </summary>
    /// <param name="ctl">The Control for which Help is requested.</param>
    /// <returns>true if Help is displayed for the control; otherwise, false.</returns>
    public bool GetShowHelp(Control ctl) => _helpProvider?.GetShowHelp(ctl) ?? false;

    /// <summary>
    /// Returns the Help string for the specified control.
    /// </summary>
    /// <param name="ctl">The Control from which to retrieve the Help string.</param>
    /// <returns>The Help string associated with this control.</returns>
    public string? GetHelpString(Control ctl) => _helpProvider?.GetHelpString(ctl);

    /// <summary>
    /// Specifies a Help keyword for the specified control.
    /// </summary>
    /// <param name="ctl">The Control to associate the Help keyword with.</param>
    /// <param name="keyword">The Help keyword to associate with the control.</param>
    public void SetHelpKeyword(Control ctl, string? keyword)
    {
        _helpProvider?.SetHelpKeyword(ctl, keyword);
    }

    /// <summary>
    /// Specifies the Help command to use when the user invokes Help for the specified control.
    /// </summary>
    /// <param name="ctl">The Control for which to set the Help command.</param>
    /// <param name="navigator">One of the HelpNavigator values.</param>
    public void SetHelpNavigator(Control ctl, HelpNavigator navigator)
    {
        _helpProvider?.SetHelpNavigator(ctl, navigator);
    }

    /// <summary>
    /// Specifies whether Help is displayed for the specified control.
    /// </summary>
    /// <param name="ctl">The Control for which Help is turned on or off.</param>
    /// <param name="value">true if Help is displayed for the control; otherwise, false.</param>
    public void SetShowHelp(Control ctl, bool value)
    {
        _helpProvider?.SetShowHelp(ctl, value);
    }

    /// <summary>
    /// Specifies the Help string associated with the specified control.
    /// </summary>
    /// <param name="ctl">The Control to associate the Help string with.</param>
    /// <param name="helpString">The Help string to display when the user invokes Help for the specified control.</param>
    public void SetHelpString(Control ctl, string? helpString)
    {
        _helpProvider?.SetHelpString(ctl, helpString);
    }

    /// <summary>
    /// Raises the HelpRequested event.
    /// </summary>
    /// <param name="e">A HelpEventArgs that contains the event data.</param>
    protected virtual void OnHelpRequested(HelpEventArgs e)
    {
        HelpRequested?.Invoke(this, e);
    }

    #region IExtenderProvider
    /// <summary>
    /// Specifies whether this object can provide its extender properties to the specified object.
    /// </summary>
    /// <param name="extendee">The Object to receive the extender properties.</param>
    /// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
    public bool CanExtend(object extendee) => _helpProvider?.CanExtend(extendee) ?? false;
    #endregion
    #endregion

    #region Implementation
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
        }
    }
    #endregion
}

