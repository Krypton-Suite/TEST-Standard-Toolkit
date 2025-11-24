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
/// Displays a collection of tab pages that can be used to access multiple pages of information.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(TabControl))]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(TabPages))]
[DesignerCategory(@"code")]
[Description(@"Displays a collection of tab pages that can be used to access multiple pages of information.")]
public class KryptonTabControl : TabControl
{
    #region Instance Fields
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
    private PaletteTripleRedirect? _tabStateCommon;
    private PaletteTriple? _tabStateDisabled;
    private PaletteTriple? _tabStateNormal;
    private PaletteTriple? _tabStateTracking;
    private PaletteTriple? _tabStatePressed;
    private PaletteTriple? _tabStateSelected;
    private TabStyle _tabStyle;
    private TabBorderStyle _tabBorderStyle;
    private IRenderer? _renderer;
    private int _hoveredTabIndex;
    private bool _mouseDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTabControl class.
    /// </summary>
    public KryptonTabControl()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(_palette);

        // Create the palette storage for control background
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Create the palette storage for tabs
        _tabStyle = TabStyle.StandardProfile;
        _tabBorderStyle = TabBorderStyle.SquareEqualSmall;
        _tabStateCommon = new PaletteTripleRedirect(_redirector, GetTabBackStyle(), GetTabBorderStyle(), GetTabContentStyle(), OnNeedPaint);
        _tabStateDisabled = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateNormal = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateTracking = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStatePressed = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateSelected = new PaletteTriple(_tabStateCommon, OnNeedPaint);

        // Initialize renderer
        UpdateRenderer();

        // Enable OwnerDraw for custom tab rendering
        DrawMode = TabDrawMode.OwnerDrawFixed;

        // Hook into events
        DrawItem += OnDrawItem;
        MouseMove += OnMouseMove;
        MouseLeave += OnMouseLeave;
        MouseDown += OnMouseDown;
        MouseUp += OnMouseUp;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Initialize state
        _hoveredTabIndex = -1;
        _mouseDown = false;

        // Update appearance from palette
        UpdateAppearance();
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
            DrawItem -= OnDrawItem;
            MouseMove -= OnMouseMove;
            MouseLeave -= OnMouseLeave;
            MouseDown -= OnMouseDown;
            MouseUp -= OnMouseUp;

            // Clean up palette objects
            _tabStateSelected = null;
            _tabStatePressed = null;
            _tabStateTracking = null;
            _tabStateNormal = null;
            _tabStateDisabled = null;
            _tabStateCommon = null;
            _stateNormal = null;
            _stateDisabled = null;
            _stateCommon = null;
            _redirector = null;
            _palette = null;
            _renderer = null;
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
                        UpdateRedirector();
                        UpdateAppearance();
                        UpdateTabPagesPalette();
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
                UpdateAppearance();
                UpdateTabPagesPalette();
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
    /// Gets and sets the tab control background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab control background style.")]
    public PaletteBackStyle TabControlBackStyle
    {
        get => _stateCommon?.BackStyle ?? PaletteBackStyle.PanelClient;

        set
        {
            if (_stateCommon != null && _stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                UpdateAppearance();
            }
        }
    }

    private bool ShouldSerializeTabControlBackStyle() => TabControlBackStyle != PaletteBackStyle.PanelClient;

    private void ResetTabControlBackStyle() => TabControlBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets and sets the tab style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab style.")]
    [DefaultValue(TabStyle.StandardProfile)]
    public TabStyle TabStyle
    {
        get => _tabStyle;

        set
        {
            if (_tabStyle != value)
            {
                _tabStyle = value;
                if (_tabStateCommon != null)
                {
                    _tabStateCommon.BackStyle = GetTabBackStyle();
                    _tabStateCommon.BorderStyle = GetTabBorderStyle();
                    _tabStateCommon.ContentStyle = GetTabContentStyle();
                }
                // Update all tab state palettes to use new styles
                UpdateTabStateStyles();
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeTabStyle() => TabStyle != TabStyle.StandardProfile;

    private void ResetTabStyle() => TabStyle = TabStyle.StandardProfile;

    /// <summary>
    /// Gets and sets the tab border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab border style.")]
    [DefaultValue(TabBorderStyle.SquareEqualSmall)]
    public TabBorderStyle TabBorderStyle
    {
        get => _tabBorderStyle;

        set
        {
            if (_tabBorderStyle != value)
            {
                _tabBorderStyle = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeTabBorderStyle() => TabBorderStyle != TabBorderStyle.SquareEqualSmall;

    private void ResetTabBorderStyle() => TabBorderStyle = TabBorderStyle.SquareEqualSmall;

    /// <summary>
    /// Gets access to the common tab appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStateCommon => _tabStateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateCommon() => _tabStateCommon != null && !_tabStateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled tab appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStateDisabled => _tabStateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateDisabled() => _tabStateDisabled != null && !_tabStateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal tab appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStateNormal => _tabStateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateNormal() => _tabStateNormal != null && !_tabStateNormal.Back.IsDefault;

    /// <summary>
    /// Gets access to the tracking tab appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStateTracking => _tabStateTracking?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateTracking() => _tabStateTracking != null && !_tabStateTracking.Back.IsDefault;

    /// <summary>
    /// Gets access to the pressed tab appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStatePressed => _tabStatePressed?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStatePressed() => _tabStatePressed != null && !_tabStatePressed.Back.IsDefault;

    /// <summary>
    /// Gets access to the selected tab appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining selected tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack TabStateSelected => _tabStateSelected?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateSelected() => _tabStateSelected != null && !_tabStateSelected.Back.IsDefault;

    /// <summary>
    /// Gets access to the common tab control appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled tab control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal tab control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for TabControl
        // This method is provided for API consistency with other Krypton controls
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the PaintBackground event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Paint parent background for transparency
        if (Parent != null)
        {
            using var brush = new SolidBrush(Parent.BackColor);
            e.Graphics.FillRectangle(brush, e.ClipRectangle);
        }
        else
        {
            // Use system color as fallback
            using var brush = new SolidBrush(SystemColors.Control);
            e.Graphics.FillRectangle(brush, e.ClipRectangle);
        }
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdateAppearance();
    }

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        base.OnSelectedIndexChanged(e);
        // Update tab pages palette when selection changes (in case new pages were added)
        UpdateTabPagesPalette();
    }
    #endregion

    #region Implementation
    private void UpdateRedirector()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_redirector != null)
        {
            _redirector.Target = currentPalette;
        }
        else
        {
            _redirector = new PaletteRedirect(currentPalette);
            if (_stateCommon != null)
            {
                _stateCommon.SetRedirector(_redirector);
            }
            if (_tabStateCommon != null)
            {
                _tabStateCommon.SetRedirector(_redirector);
            }
        }
        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        _renderer = currentPalette?.GetRenderer();

        //Backcolor = Color.Transparent;
    }

    private PaletteBackStyle GetTabBackStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteBackStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteBackStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteBackStyle.TabLowProfile,
            TabStyle.OneNote => PaletteBackStyle.TabOneNote,
            TabStyle.Dock => PaletteBackStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteBackStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteBackStyle.TabCustom1,
            TabStyle.Custom2 => PaletteBackStyle.TabCustom2,
            TabStyle.Custom3 => PaletteBackStyle.TabCustom3,
            _ => PaletteBackStyle.TabStandardProfile
        };
    }

    private PaletteBorderStyle GetTabBorderStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteBorderStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteBorderStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteBorderStyle.TabLowProfile,
            TabStyle.OneNote => PaletteBorderStyle.TabOneNote,
            TabStyle.Dock => PaletteBorderStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteBorderStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteBorderStyle.TabCustom1,
            TabStyle.Custom2 => PaletteBorderStyle.TabCustom2,
            TabStyle.Custom3 => PaletteBorderStyle.TabCustom3,
            _ => PaletteBorderStyle.TabStandardProfile
        };
    }

    private PaletteContentStyle GetTabContentStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteContentStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteContentStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteContentStyle.TabLowProfile,
            TabStyle.OneNote => PaletteContentStyle.TabOneNote,
            TabStyle.Dock => PaletteContentStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteContentStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteContentStyle.TabCustom1,
            TabStyle.Custom2 => PaletteContentStyle.TabCustom2,
            TabStyle.Custom3 => PaletteContentStyle.TabCustom3,
            _ => PaletteContentStyle.TabStandardProfile
        };
    }

    private void UpdateAppearance()
    {
        if (_stateNormal == null || _stateDisabled == null)
        {
            return;
        }

        // Set background to transparent so the content area shows through
        base.BackColor = Color.Transparent;
    }

    private void UpdateTabPagesPalette()
    {
        // Update all KryptonTabPage instances to use the same palette
        foreach (TabPage tabPage in TabPages)
        {
            if (tabPage is KryptonTabPage kryptonTabPage)
            {
                kryptonTabPage.PaletteMode = PaletteMode;
                if (PaletteMode == PaletteMode.Custom)
                {
                    kryptonTabPage.Palette = Palette;
                }
            }
        }
    }

    private void UpdateTabStateStyles()
    {
        // Update all tab state palettes to use the new tab styles
        if (_tabStateCommon != null)
        {
            _tabStateCommon.BackStyle = GetTabBackStyle();
            _tabStateCommon.BorderStyle = GetTabBorderStyle();
            _tabStateCommon.ContentStyle = GetTabContentStyle();
        }
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateAppearance();
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
            UpdateAppearance();
            UpdateTabPagesPalette();
        }
    }

    private void OnDrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= TabPages.Count || _renderer == null || _tabStateCommon == null)
        {
            return;
        }

        var tabPage = TabPages[e.Index];
        var isSelected = e.Index == SelectedIndex;
        var isHovered = e.Index == _hoveredTabIndex;
        var isPressed = _mouseDown && isHovered;
        var isDisabled = !Enabled || !tabPage.Enabled;

        // Determine the palette state
        PaletteState paletteState;
        PaletteTriple? tabPalette;
        if (isDisabled)
        {
            paletteState = PaletteState.Disabled;
            tabPalette = _tabStateDisabled;
        }
        else if (isSelected)
        {
            paletteState = PaletteState.CheckedNormal;
            tabPalette = _tabStateSelected;
        }
        else if (isPressed)
        {
            paletteState = PaletteState.Pressed;
            tabPalette = _tabStatePressed;
        }
        else if (isHovered)
        {
            paletteState = PaletteState.Tracking;
            tabPalette = _tabStateTracking;
        }
        else
        {
            paletteState = PaletteState.Normal;
            tabPalette = _tabStateNormal;
        }

        if (tabPalette == null)
        {
            return;
        }

        // Create render context
        var context = new RenderContext(this, e.Graphics, e.Bounds, _renderer);

        // Determine orientation based on tab alignment
        var orientation = Alignment switch
        {
            TabAlignment.Top => VisualOrientation.Top,
            TabAlignment.Bottom => VisualOrientation.Bottom,
            TabAlignment.Left => VisualOrientation.Left,
            TabAlignment.Right => VisualOrientation.Right,
            _ => VisualOrientation.Top
        };

        // Get the proper tab path shape for background
        GraphicsPath? tabBackPath = null;
        if (tabPalette.Back.GetBackDraw(paletteState) == InheritBool.True)
        {
            tabBackPath = _renderer.RenderTabBorder.GetTabBackPath(context, e.Bounds, tabPalette.Border, orientation, paletteState, _tabBorderStyle);
        }

        // Draw tab background
        if (tabBackPath != null)
        {
            using (tabBackPath)
            {
                using var memento = _renderer.RenderStandardBack.DrawBack(context, e.Bounds, tabBackPath, tabPalette.Back, orientation, paletteState, null);
            }
        }

        // Draw tab border
        if (tabPalette.Border.GetBorderDraw(paletteState) == InheritBool.True)
        {
            _renderer.RenderTabBorder.DrawTabBorder(context, e.Bounds, tabPalette.Border, orientation, paletteState, _tabBorderStyle);
        }

        // Draw tab text and image
        var textRect = e.Bounds;
        textRect.Inflate(-4, -2);

        // Draw image if present
        if (tabPage.ImageIndex >= 0 && ImageList != null && tabPage.ImageIndex < ImageList.Images.Count)
        {
            var image = ImageList.Images[tabPage.ImageIndex];
            var imageRect = new Rectangle(textRect.Left, textRect.Top + (textRect.Height - image.Height) / 2, image.Width, image.Height);
            e.Graphics.DrawImage(image, imageRect);
            textRect.X += image.Width + 4;
            textRect.Width -= image.Width + 4;
        }
        else if (!string.IsNullOrEmpty(tabPage.ImageKey) && ImageList != null && ImageList.Images.ContainsKey(tabPage.ImageKey))
        {
            var image = ImageList.Images[tabPage.ImageKey];
            var imageRect = new Rectangle(textRect.Left, textRect.Top + (textRect.Height - image.Height) / 2, image.Width, image.Height);
            e.Graphics.DrawImage(image, imageRect);
            textRect.X += image.Width + 4;
            textRect.Width -= image.Width + 4;
        }

        // Draw text
        if (!string.IsNullOrEmpty(tabPage.Text))
        {
            // Get text color from content palette
            var contentTextColor = tabPalette.Content.GetContentShortTextColor1(paletteState);
            if (contentTextColor == Color.Empty)
            {
                contentTextColor = ForeColor;
            }

            TextRenderer.DrawText(e.Graphics, tabPage.Text, Font, textRect, contentTextColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        // Draw focus rectangle if needed
        if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
        {
            ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
        }
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        var newHoveredIndex = GetTabIndexAt(e.Location);
        if (newHoveredIndex != _hoveredTabIndex)
        {
            var oldIndex = _hoveredTabIndex;
            _hoveredTabIndex = newHoveredIndex;
            
            // Invalidate old and new tabs
            if (oldIndex >= 0 && oldIndex < TabCount)
            {
                Invalidate(GetTabRect(oldIndex));
            }
            if (_hoveredTabIndex >= 0 && _hoveredTabIndex < TabCount)
            {
                Invalidate(GetTabRect(_hoveredTabIndex));
            }
        }
    }

    private void OnMouseLeave(object? sender, EventArgs e)
    {
        if (_hoveredTabIndex >= 0)
        {
            var oldIndex = _hoveredTabIndex;
            _hoveredTabIndex = -1;
            if (oldIndex >= 0 && oldIndex < TabCount)
            {
                Invalidate(GetTabRect(oldIndex));
            }
        }
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        _mouseDown = true;
        var tabIndex = GetTabIndexAt(e.Location);
        if (tabIndex >= 0 && tabIndex < TabCount)
        {
            Invalidate(GetTabRect(tabIndex));
        }
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        _mouseDown = false;
        var tabIndex = GetTabIndexAt(e.Location);
        if (tabIndex >= 0 && tabIndex < TabCount)
        {
            Invalidate(GetTabRect(tabIndex));
        }
    }

    private int GetTabIndexAt(Point location)
    {
        for (var i = 0; i < TabCount; i++)
        {
            if (GetTabRect(i).Contains(location))
            {
                return i;
            }
        }
        return -1;
    }
    #endregion
}

