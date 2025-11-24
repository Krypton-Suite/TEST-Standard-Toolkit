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
/// Represents a small rectangular pop-up window that displays a brief description of a control's purpose when the user rests the pointer on the control.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ToolTip))]
[DefaultEvent(nameof(Popup))]
[DefaultProperty(nameof(ToolTipTitle))]
[DesignerCategory(@"code")]
[Description(@"Represents a small rectangular pop-up window that displays a brief description of a control's purpose when the user rests the pointer on the control.")]
public class KryptonToolTip : Component, IExtenderProvider
{
    #region Instance Fields
    private ToolTip? _toolTip;
    private readonly Dictionary<Control, string> _toolTipTexts;
    private readonly Dictionary<Control, string> _toolTipTitles;
    private ToolTipValues? _toolTipValues;
    private VisualPopupToolTip? _visualPopupToolTip;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteRedirect? _redirector;
    private Control? _currentControl;
    private System.Windows.Forms.Timer? _hideTimer;
    private readonly Dictionary<Control, EventHandler> _mouseLeaveHandlers;
    private ToolTipDrawMode _drawMode;
    private Color _stripColor;
    #endregion

    #region Events
    /// <summary>
    /// Occurs before a ToolTip is initially displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs before a ToolTip is initially displayed.")]
    public event PopupEventHandler? Popup;

    /// <summary>
    /// Occurs when a ToolTip is drawn.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a ToolTip is drawn.")]
    public event DrawToolTipEventHandler? Draw;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonToolTip class.
    /// </summary>
    public KryptonToolTip()
    {
        _toolTipTexts = new Dictionary<Control, string>();
        _toolTipTitles = new Dictionary<Control, string>();
        _mouseLeaveHandlers = new Dictionary<Control, EventHandler>();
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;
        _drawMode = ToolTipDrawMode.Normal;
        _stripColor = Color.Empty;

        // Create the underlying ToolTip
        _toolTip = new ToolTip();

        // Create ToolTipValues for customization
        _toolTipValues = new ToolTipValues(null, GetDpiFactor)
        {
            EnableToolTips = true
        };

        // Initialize redirector
        UpdateRedirector();

        // Create hide timer
        _hideTimer = new System.Windows.Forms.Timer
        {
            Interval = AutoPopDelay
        };
        _hideTimer.Tick += OnHideTimerTick;

        // Hook into ToolTip events
        _toolTip.Popup += OnToolTipPopup;
        _toolTip.Draw += OnToolTipDraw;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonToolTip class with a container.
    /// </summary>
    /// <param name="container">The IContainer that contains this component.</param>
    public KryptonToolTip(IContainer container)
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

            if (_toolTip != null)
            {
                _toolTip.Popup -= OnToolTipPopup;
                _toolTip.Draw -= OnToolTipDraw;
                _toolTip.Dispose();
                _toolTip = null;
            }

            // Unhook mouse leave handlers
            foreach (var kvp in _mouseLeaveHandlers)
            {
                if (kvp.Key != null && !kvp.Key.IsDisposed)
                {
                    kvp.Key.MouseLeave -= kvp.Value;
                }
            }
            _mouseLeaveHandlers.Clear();

            _hideTimer?.Stop();
            _hideTimer?.Dispose();
            _hideTimer = null;

            _visualPopupToolTip?.Dispose();
            _visualPopupToolTip = null;
            _toolTipValues = null;
            _redirector = null;
            _palette = null;
            _toolTipTexts.Clear();
            _toolTipTitles.Clear();
        }

        base.Dispose(disposing);
    }

    private float GetDpiFactor() => 96F / 96F; // Default DPI factor
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
                        UpdateRedirector();
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

                UpdateRedirector();
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
    /// Gets access to the ToolTip values.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Control ToolTip Text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues => _toolTipValues ?? throw new ObjectDisposedException(nameof(KryptonToolTip));

    private bool ShouldSerializeToolTipValues() => _toolTipValues != null && !_toolTipValues.IsDefault;

    /// <summary>
    /// Gets or sets a value indicating whether the ToolTip is active.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the ToolTip is active.")]
    [DefaultValue(true)]
    public bool Active
    {
        get => _toolTip?.Active ?? false;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.Active = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the initial delay for the ToolTip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The initial delay for the ToolTip.")]
    [DefaultValue(500)]
    public int InitialDelay
    {
        get => _toolTip?.InitialDelay ?? 500;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.InitialDelay = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The period of time the ToolTip remains visible if the pointer is stationary on a control.")]
    [DefaultValue(5000)]
    public int AutoPopDelay
    {
        get => _toolTip?.AutoPopDelay ?? 5000;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.AutoPopDelay = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the delay that must pass before subsequent ToolTip windows appear as the pointer moves from one control to another.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The delay that must pass before subsequent ToolTip windows appear as the pointer moves from one control to another.")]
    [DefaultValue(100)]
    public int ReshowDelay
    {
        get => _toolTip?.ReshowDelay ?? 100;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.ReshowDelay = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a ToolTip window is shown, even when its parent control is not active.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether a ToolTip window is shown, even when its parent control is not active.")]
    [DefaultValue(false)]
    public bool ShowAlways
    {
        get => _toolTip?.ShowAlways ?? false;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.ShowAlways = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines how ToolTips are drawn.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines how ToolTips are drawn.")]
    [DefaultValue(ToolTipDrawMode.Normal)]
    public ToolTipDrawMode DrawMode
    {
        get => _drawMode;
        set => _drawMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ToolTip uses a balloon window.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the ToolTip uses a balloon window.")]
    [DefaultValue(false)]
    public bool IsBalloon
    {
        get => _toolTip?.IsBalloon ?? false;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.IsBalloon = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the background color for the ToolTip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The background color for the ToolTip.")]
    [DefaultValue(typeof(Color), "Info")]
    public Color BackColor
    {
        get => _toolTip?.BackColor ?? SystemColors.Info;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.BackColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the foreground color for the ToolTip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The foreground color for the ToolTip.")]
    [DefaultValue(typeof(Color), "InfoText")]
    public Color ForeColor
    {
        get => _toolTip?.ForeColor ?? SystemColors.InfoText;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.ForeColor = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines the strip of color that appears on the left side of the ToolTip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The strip of color that appears on the left side of the ToolTip.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color StripColor
    {
        get => _stripColor;
        set => _stripColor = value;
    }

    /// <summary>
    /// Gets or sets the ToolTip title.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The ToolTip title.")]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string ToolTipTitle
    {
        get => _toolTip?.ToolTipTitle ?? string.Empty;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.ToolTipTitle = value ?? string.Empty;
            }
        }
    }

    /// <summary>
    /// Gets or sets the icon that appears on the ToolTip.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The icon that appears on the ToolTip.")]
    [DefaultValue(ToolTipIcon.None)]
    public ToolTipIcon ToolTipIcon
    {
        get => _toolTip?.ToolTipIcon ?? ToolTipIcon.None;
        set
        {
            if (_toolTip != null)
            {
                _toolTip.ToolTipIcon = value;
            }
        }
    }

    /// <summary>
    /// Gets the underlying ToolTip instance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTip? ToolTip => _toolTip;

    /// <summary>
    /// Associates ToolTip text with the specified control.
    /// </summary>
    /// <param name="control">The Control to associate the ToolTip text with.</param>
    /// <param name="caption">The ToolTip text to display when the pointer is on the control.</param>
    public void SetToolTip(Control control, string? caption)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        if (caption == null)
        {
            caption = string.Empty;
        }

        // Unhook previous mouse leave handler if exists
        if (_mouseLeaveHandlers.TryGetValue(control, out var oldHandler))
        {
            control.MouseLeave -= oldHandler;
            _mouseLeaveHandlers.Remove(control);
        }

        // Store the tooltip text
        if (string.IsNullOrEmpty(caption))
        {
            _toolTipTexts.Remove(control);
        }
        else
        {
            _toolTipTexts[control] = caption;

            // Hook into mouse leave to hide tooltip
            EventHandler mouseLeaveHandler = (s, e) => HideToolTip();
            control.MouseLeave += mouseLeaveHandler;
            _mouseLeaveHandlers[control] = mouseLeaveHandler;
        }

        // Set on underlying ToolTip (but we'll intercept and replace with Krypton tooltip)
        _toolTip?.SetToolTip(control, caption);
    }

    /// <summary>
    /// Retrieves the ToolTip text associated with the specified control.
    /// </summary>
    /// <param name="control">The Control for which to retrieve the ToolTip text.</param>
    /// <returns>A String containing the ToolTip text for the specified control.</returns>
    public string GetToolTip(Control control)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        return _toolTipTexts.TryGetValue(control, out var text) ? text : _toolTip?.GetToolTip(control) ?? string.Empty;
    }

    /// <summary>
    /// Removes all ToolTip text currently associated with the ToolTip component.
    /// </summary>
    public void RemoveAll()
    {
        // Unhook all mouse leave handlers
        foreach (var kvp in _mouseLeaveHandlers)
        {
            if (kvp.Key != null && !kvp.Key.IsDisposed)
            {
                kvp.Key.MouseLeave -= kvp.Value;
            }
        }
        _mouseLeaveHandlers.Clear();

        _toolTipTexts.Clear();
        _toolTipTitles.Clear();
        _toolTip?.RemoveAll();
    }

    /// <summary>
    /// Hides the currently displayed tooltip.
    /// </summary>
    public void HideToolTip()
    {
        _hideTimer?.Stop();
        _visualPopupToolTip?.Dispose();
        _visualPopupToolTip = null;
        _currentControl = null;
    }

    #region IExtenderProvider
    /// <summary>
    /// Specifies whether this object can provide its extender properties to the specified object.
    /// </summary>
    /// <param name="extendee">The Object to receive the extender properties.</param>
    /// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
    public bool CanExtend(object extendee) => _toolTip?.CanExtend(extendee) ?? false;
    #endregion
    #endregion

    #region Implementation
    private void UpdateRedirector()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        _redirector = new PaletteRedirect(currentPalette);
    }

    private void OnToolTipPopup(object? sender, PopupEventArgs e)
    {
        // Store the current control
        _currentControl = e.AssociatedControl;

        // Get the tooltip text for this control
        var toolTipText = GetToolTip(e.AssociatedControl);
        var toolTipTitle = _toolTipTitles.TryGetValue(e.AssociatedControl, out var title) ? title : ToolTipTitle;

        // If there's no text, cancel the tooltip
        if (string.IsNullOrEmpty(toolTipText) && string.IsNullOrEmpty(toolTipTitle))
        {
            e.Cancel = true;
            return;
        }

        // Cancel the standard tooltip - we'll show a Krypton one instead
        e.Cancel = true;

        // Raise the Popup event
        var popupArgs = new PopupEventArgs(e.AssociatedWindow, e.AssociatedControl, e.IsBalloon, e.ToolTipSize);
        Popup?.Invoke(this, popupArgs);

        // If the event handler cancelled, don't show tooltip
        if (popupArgs.Cancel)
        {
            return;
        }

        // Show the Krypton tooltip
        ShowKryptonToolTip(e.AssociatedControl, toolTipText, toolTipTitle, Control.MousePosition);

        // Start timer to hide tooltip after AutoPopDelay
        if (_hideTimer != null)
        {
            _hideTimer.Interval = AutoPopDelay;
            _hideTimer.Stop();
            _hideTimer.Start();
        }
    }

    private void OnToolTipDraw(object? sender, DrawToolTipEventArgs e)
    {
        // Raise the Draw event
        Draw?.Invoke(this, e);
    }

    private void ShowKryptonToolTip(Control control, string text, string title, Point mousePosition)
    {
        if (control == null || control.IsDisposed || !control.Visible)
        {
            return;
        }

        if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(title))
        {
            return;
        }

        // Dispose any existing tooltip
        _visualPopupToolTip?.Dispose();

        // Get the current palette and create redirector if needed
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (currentPalette == null)
        {
            return;
        }

        if (_redirector == null)
        {
            UpdateRedirector();
        }

        var renderer = currentPalette.GetRenderer();
        if (renderer == null || _redirector == null || _toolTipValues == null)
        {
            return;
        }

        // Update ToolTipValues with the text and title
        _toolTipValues.Heading = title;
        _toolTipValues.Description = text;

        // Create a simple ViewBase wrapper for the control
        var viewTarget = new ViewLayoutNull
        {
            OwningControl = control,
            ClientRectangle = control.ClientRectangle
        };

        // Create the Krypton tooltip popup
        _visualPopupToolTip = new VisualPopupToolTip(_redirector,
            _toolTipValues,
            renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            CommonHelper.ContentStyleFromLabelStyle(_toolTipValues.ToolTipStyle),
            _toolTipValues.ToolTipShadow);

        _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

        // Show the tooltip relative to the control
        try
        {
            var controlMousePosition = control.PointToClient(mousePosition);
            _visualPopupToolTip.ShowRelativeTo(viewTarget, controlMousePosition);
        }
        catch
        {
            // Control might have been disposed, just dispose the tooltip
            _visualPopupToolTip?.Dispose();
            _visualPopupToolTip = null;
        }
    }

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popup)
        {
            popup.Disposed -= OnVisualPopupToolTipDisposed;
        }

        _visualPopupToolTip = null;
        _currentControl = null;
        _hideTimer?.Stop();
    }

    private void OnHideTimerTick(object? sender, EventArgs e)
    {
        _hideTimer?.Stop();
        HideToolTip();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
        }
    }
    #endregion
}

