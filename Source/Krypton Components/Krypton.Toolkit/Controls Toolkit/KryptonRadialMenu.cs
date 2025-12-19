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
/// Displays a radial menu with multiple levels/rings using Krypton renderer.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRadialMenu), "ToolboxBitmaps.KryptonRadialMenu.bmp")]
[DefaultEvent(nameof(ItemClick))]
[DefaultProperty(nameof(Items))]
[DesignerCategory(@"code")]
[Description(@"Displays a radial menu with multiple levels/rings.")]
public class KryptonRadialMenu : VisualControl
{
    #region Type Definitions
    private class RadialMenuItemInfo
    {
        public KryptonRadialMenuItem Item { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float InnerRadius { get; set; }
        public float OuterRadius { get; set; }
        public RectangleF Bounds { get; set; }
        public PointF Center { get; set; }
    }
    #endregion

    #region Static Fields
    private static MethodInfo? _miPTB;
    #endregion

    #region Instance Fields
    private readonly List<KryptonRadialMenuItem> _items;
    private readonly List<RadialMenuItemInfo> _itemInfos;
    private KryptonRadialMenuItem? _hoveredItem;
    private KryptonRadialMenuItem? _pressedItem;
    private readonly PaletteRedirect _redirector;
    private readonly RadialMenuLayoutValues _layout;
    private readonly RadialMenuBehaviorValues _behavior;
    private bool _isDragging;
    private Point _dragStartPoint;
    private Point _dragOffset;
    private VisualRadialMenuFloatingForm? _floatingWindow;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a menu item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a menu item is clicked.")]
    public event EventHandler<KryptonRadialMenuItemEventArgs>? ItemClick;

    /// <summary>
    /// Occurs when a menu item is hovered.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a menu item is hovered.")]
    public event EventHandler<KryptonRadialMenuItemEventArgs>? ItemHover;

    /// <summary>
    /// Occurs when the menu is being dragged.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the menu is being dragged.")]
    public event EventHandler<MouseEventArgs>? MenuDragging;

    /// <summary>
    /// Occurs when the menu is floated into its own window.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the menu is floated into its own window.")]
    public event EventHandler? MenuFloated;

    /// <summary>
    /// Occurs when the menu is returned from floating window.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the menu is returned from floating window.")]
    public event EventHandler? MenuReturned;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRadialMenu class.
    /// </summary>
    public KryptonRadialMenu()
    {
        // Set default control styles
        SetStyle(ControlStyles.Selectable, false);
        SetStyle(ControlStyles.UserMouse, true);

        // Create collections
        _items = new List<KryptonRadialMenuItem>();
        _itemInfos = new List<RadialMenuItemInfo>();

        // Create palette redirector
        _redirector = new PaletteRedirect(Redirector);

        // Create palette storage for Client level (inner ring)
        StateCommonClient = new PaletteTripleRedirect(_redirector, PaletteBackStyle.ControlClient,
            PaletteBorderStyle.ControlClient, PaletteContentStyle.LabelNormalPanel, NeedPaintDelegate);
        // Set center alignment for radial menu items
        StateCommonClient.Content.ShortText.TextH = PaletteRelativeAlign.Center;
        StateCommonClient.Content.ShortText.TextV = PaletteRelativeAlign.Center;
        StateDisabledClient = new PaletteTriple(StateCommonClient, NeedPaintDelegate);
        StateNormalClient = new PaletteTriple(StateCommonClient, NeedPaintDelegate);
        StateTrackingClient = new PaletteTriple(StateCommonClient, NeedPaintDelegate);
        StatePressedClient = new PaletteTriple(StateCommonClient, NeedPaintDelegate);

        // Create palette storage for Alternate level (outer rings)
        StateCommonAlternate = new PaletteTripleRedirect(_redirector, PaletteBackStyle.ControlAlternate,
            PaletteBorderStyle.ControlAlternate, PaletteContentStyle.LabelNormalPanel, NeedPaintDelegate);
        // Set center alignment for radial menu items
        StateCommonAlternate.Content.ShortText.TextH = PaletteRelativeAlign.Center;
        StateCommonAlternate.Content.ShortText.TextV = PaletteRelativeAlign.Center;
        StateDisabledAlternate = new PaletteTriple(StateCommonAlternate, NeedPaintDelegate);
        StateNormalAlternate = new PaletteTriple(StateCommonAlternate, NeedPaintDelegate);
        StateTrackingAlternate = new PaletteTriple(StateCommonAlternate, NeedPaintDelegate);
        StatePressedAlternate = new PaletteTriple(StateCommonAlternate, NeedPaintDelegate);

        // Create layout and behavior objects
        _layout = new RadialMenuLayoutValues(this);
        _behavior = new RadialMenuBehaviorValues(this);

        // Set default values
        _hoveredItem = null;
        _pressedItem = null;
        _isDragging = false;
        _floatingWindow = null;

        // Create view manager
        ViewManager = new ViewManager(this, new ViewLayoutNull());
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of items
            foreach (var item in _items)
            {
                item.Click -= OnItemClick;
            }
            _items.Clear();
            _itemInfos.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the collection of menu items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Collection of menu items in the radial menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<KryptonRadialMenuItem> Items => _items;

    /// <summary>
    /// Gets access to the layout properties.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Layout properties for the radial menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new RadialMenuLayoutValues Layout => _layout;

    private bool ShouldSerializeLayout() => !_layout.IsDefault;

    private void ResetLayout() => _layout.Reset();

    /// <summary>
    /// Gets access to the behavior properties.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Behavior properties for the radial menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadialMenuBehaviorValues Behavior => _behavior;

    private bool ShouldSerializeBehavior() => !_behavior.IsDefault;

    private void ResetBehavior() => _behavior.Reset();

    /// <summary>
    /// Gets a value indicating whether the menu is currently floating in its own window.
    /// </summary>
    [Browsable(false)]
    public bool IsFloating => _floatingWindow is { IsDisposed: false };

    /// <summary>
    /// Floats the menu into its own window.
    /// </summary>
    /// <returns>The floating window containing the menu, or null if floating is not allowed.</returns>
    public VisualRadialMenuFloatingForm? Float()
    {
        if (!_behavior.AllowFloat || _floatingWindow != null)
        {
            return null;
        }

        Form? owner = _behavior.MdiParent ?? FindForm();
        _floatingWindow = new VisualRadialMenuFloatingForm(this, owner);

        _floatingWindow.WindowReturning += FloatingWindow_WindowReturning;
        _floatingWindow.Show();

        OnMenuFloated(EventArgs.Empty);
        return _floatingWindow;
    }

    /// <summary>
    /// Returns the menu from floating window to its original location.
    /// </summary>
    public void ReturnFromFloat()
    {
        if (_floatingWindow != null && !_floatingWindow.IsDisposed)
        {
            _floatingWindow.ReturnToOriginalLocation();
            _floatingWindow.Dispose();
            _floatingWindow = null;
        }
    }

    /// <summary>
    /// Gets access to the common Client level appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common Client level appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommonClient { get; }

    private bool ShouldSerializeStateCommonClient() => !StateCommonClient.IsDefault;

    /// <summary>
    /// Gets access to the disabled Client level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled Client level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabledClient { get; }

    private bool ShouldSerializeStateDisabledClient() => !StateDisabledClient.IsDefault;

    /// <summary>
    /// Gets access to the normal Client level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal Client level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormalClient { get; }

    private bool ShouldSerializeStateNormalClient() => !StateNormalClient.IsDefault;

    /// <summary>
    /// Gets access to the tracking Client level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking Client level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTrackingClient { get; }

    private bool ShouldSerializeStateTrackingClient() => !StateTrackingClient.IsDefault;

    /// <summary>
    /// Gets access to the pressed Client level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed Client level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressedClient { get; }

    private bool ShouldSerializeStatePressedClient() => !StatePressedClient.IsDefault;

    /// <summary>
    /// Gets access to the common Alternate level appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common Alternate level appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommonAlternate { get; }

    private bool ShouldSerializeStateCommonAlternate() => !StateCommonAlternate.IsDefault;

    /// <summary>
    /// Gets access to the disabled Alternate level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled Alternate level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabledAlternate { get; }

    private bool ShouldSerializeStateDisabledAlternate() => !StateDisabledAlternate.IsDefault;

    /// <summary>
    /// Gets access to the normal Alternate level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal Alternate level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormalAlternate { get; }

    private bool ShouldSerializeStateNormalAlternate() => !StateNormalAlternate.IsDefault;

    /// <summary>
    /// Gets access to the tracking Alternate level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking Alternate level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTrackingAlternate { get; }

    private bool ShouldSerializeStateTrackingAlternate() => !StateTrackingAlternate.IsDefault;

    /// <summary>
    /// Gets access to the pressed Alternate level appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed Alternate level appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressedAlternate { get; }

    private bool ShouldSerializeStatePressedAlternate() => !StatePressedAlternate.IsDefault;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(300, 300);

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (!IsDisposed && !Disposing)
        {
            // Paint background - support transparency
            PaintTransparentBackground(e);

            // Update item information
            UpdateItemInfo();

            // Get renderer
            IRenderer? renderer = Renderer;
            if (renderer == null)
            {
                return;
            }

            // Create render context
            using var context = new RenderContext(GetViewManager(), this, this, e.Graphics,
                e.ClipRectangle, renderer);

            // Draw center circle
            DrawCenter(context, renderer);

            // Draw menu items
            DrawItems(context, renderer);
        }
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        // Handle dragging
        if (_isDragging && _behavior.AllowDrag && Parent != null)
        {
            Point newLocation = Parent.PointToClient(PointToScreen(e.Location));
            newLocation.Offset(-_dragOffset.X, -_dragOffset.Y);
            Location = newLocation;
            OnMenuDragging(e);
            return;
        }

        // Find item under mouse
        var hitItem = HitTest(e.Location);

        if (_hoveredItem != hitItem)
        {
            _hoveredItem = hitItem;
            Invalidate();

            if (hitItem != null)
            {
                OnItemHover(new KryptonRadialMenuItemEventArgs(hitItem));
            }
        }
    }

    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (e.Button == MouseButtons.Left)
        {
            // Check if mouse is in drag region (center circle)
            if (_behavior.AllowDrag && IsInDragRegion(e.Location))
            {
                _isDragging = true;
                _dragStartPoint = e.Location;
                Point screenPoint = PointToScreen(e.Location);
                _dragOffset = new Point(e.X, e.Y);
                
                // Temporarily clear anchor to allow dragging
                if (Parent != null)
                {
                    Anchor = AnchorStyles.None;
                }
                
                Capture = true;
                Cursor = Cursors.SizeAll;
                return;
            }

            _pressedItem = HitTest(e.Location);
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the MouseUp event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (e.Button == MouseButtons.Left)
        {
            if (_isDragging)
            {
                _isDragging = false;
                Capture = false;
                Cursor = Cursors.Default;

                // Check if we should float the window
                if (_behavior.AllowFloat && Parent != null)
                {
                    Rectangle parentBounds = Parent.RectangleToScreen(Parent.ClientRectangle);
                    Point screenPoint = PointToScreen(e.Location);
                    
                    // If dragged outside parent bounds, float the window
                    if (!parentBounds.Contains(screenPoint))
                    {
                        Float();
                    }
                }
                return;
            }

            if (_pressedItem != null)
            {
                var hitItem = HitTest(e.Location);
                if (hitItem == _pressedItem && hitItem.Enabled)
                {
                    OnItemClick(new KryptonRadialMenuItemEventArgs(hitItem));
                    hitItem.OnClick(EventArgs.Empty);
                }
                _pressedItem = null;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_hoveredItem != null)
        {
            _hoveredItem = null;
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        // Update the redirector with latest palette
        _redirector.Target = Redirector.Target;

        // Update the state common redirectors
        StateCommonClient.SetRedirector(_redirector);
        StateCommonAlternate.SetRedirector(_redirector);

        base.OnPaletteChanged(e);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the ItemClick event.
    /// </summary>
    /// <param name="e">A KryptonRadialMenuItemEventArgs containing the event data.</param>
    protected virtual void OnItemClick(KryptonRadialMenuItemEventArgs e) => ItemClick?.Invoke(this, e);

    /// <summary>
    /// Raises the ItemHover event.
    /// </summary>
    /// <param name="e">A KryptonRadialMenuItemEventArgs containing the event data.</param>
    protected virtual void OnItemHover(KryptonRadialMenuItemEventArgs e) => ItemHover?.Invoke(this, e);

    /// <summary>
    /// Raises the MenuDragging event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    protected virtual void OnMenuDragging(MouseEventArgs e) => MenuDragging?.Invoke(this, e);

    /// <summary>
    /// Raises the MenuFloated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnMenuFloated(EventArgs e) => MenuFloated?.Invoke(this, e);

    /// <summary>
    /// Raises the MenuReturned event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnMenuReturned(EventArgs e) => MenuReturned?.Invoke(this, e);
    #endregion

    #region Private
    private void UpdateItemInfo()
    {
        _itemInfos.Clear();

        if (_items.Count == 0)
        {
            return;
        }

        // Group items by level
        var itemsByLevel = _items.GroupBy(item => item.Level).OrderBy(g => g.Key).ToList();
        int maxLevel = itemsByLevel.Max(g => g.Key);

        PointF center = new PointF(Width / 2f, Height / 2f);
        float currentInnerRadius = _layout.InnerRadius;

        foreach (var levelGroup in itemsByLevel)
        {
            int level = levelGroup.Key;
            var levelItems = levelGroup.ToList();
            int itemCount = levelItems.Count;

            if (itemCount == 0)
            {
                continue;
            }

            // Calculate angles for items in this level
            float sweepAngle = 360f / itemCount;
            float currentAngle = _layout.StartAngle;

            foreach (var item in levelItems)
            {
                float outerRadius = currentInnerRadius + _layout.RingThickness;

                var info = new RadialMenuItemInfo
                {
                    Item = item,
                    StartAngle = currentAngle,
                    SweepAngle = sweepAngle,
                    InnerRadius = currentInnerRadius,
                    OuterRadius = outerRadius,
                    Center = center
                };

                // Calculate bounding rectangle for hit testing
                CalculateBounds(info);

                _itemInfos.Add(info);

                currentAngle += sweepAngle;
            }

            // Move to next level
            currentInnerRadius = currentInnerRadius + _layout.RingThickness;
        }
    }

    private void CalculateBounds(RadialMenuItemInfo info)
    {
        // Create a rectangle that encompasses the pie slice
        float radius = info.OuterRadius;
        RectangleF outerRect = new RectangleF(
            info.Center.X - radius,
            info.Center.Y - radius,
            radius * 2,
            radius * 2);

        // For hit testing, we'll use a simpler bounding box
        // The actual hit test will use angle/distance calculation
        info.Bounds = outerRect;
    }

    private void DrawCenter(RenderContext context, IRenderer renderer)
    {
        if (_layout.CenterRadius <= 0)
        {
            return;
        }

        PointF center = new PointF(Width / 2f, Height / 2f);
        Rectangle centerRect = new Rectangle(
            (int)(center.X - _layout.CenterRadius),
            (int)(center.Y - _layout.CenterRadius),
            _layout.CenterRadius * 2,
            _layout.CenterRadius * 2);

        // Draw center circle using client palette
        using var path = new GraphicsPath();
        path.AddEllipse(centerRect);

        PaletteState state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        IPaletteTriple palette = state == PaletteState.Disabled ? StateDisabledClient : StateNormalClient;
        renderer.RenderStandardBack.DrawBack(context, centerRect, path, palette.PaletteBack, VisualOrientation.Top, state, null);
        if (palette.PaletteBorder != null)
        {
            renderer.RenderStandardBorder.DrawBorder(context, centerRect, palette.PaletteBorder, VisualOrientation.Top,
                state);
        }
    }

    private void DrawItems(RenderContext context, IRenderer renderer)
    {
        foreach (var info in _itemInfos)
        {
            DrawMenuItem(context, renderer, info);
        }
    }

    private void DrawMenuItem(RenderContext context, IRenderer renderer, RadialMenuItemInfo info)
    {
        var item = info.Item;
        bool isHovered = _hoveredItem == item;
        bool isPressed = _pressedItem == item;

        // Determine state
        PaletteState state = PaletteState.Normal;
        if (!item.Enabled)
        {
            state = PaletteState.Disabled;
        }
        else if (isPressed)
        {
            state = PaletteState.Pressed;
        }
        else if (isHovered)
        {
            state = PaletteState.Tracking;
        }

        // Select palette based on level and state
        IPaletteTriple palette;
        if (item.Level > 0)
        {
            // Use Alternate palette for outer rings
            palette = state switch
            {
                PaletteState.Disabled => StateDisabledAlternate,
                PaletteState.Pressed => StatePressedAlternate,
                PaletteState.Tracking => StateTrackingAlternate,
                _ => StateNormalAlternate
            };
        }
        else
        {
            // Use Client palette for inner ring
            palette = state switch
            {
                PaletteState.Disabled => StateDisabledClient,
                PaletteState.Pressed => StatePressedClient,
                PaletteState.Tracking => StateTrackingClient,
                _ => StateNormalClient
            };
        }

        // Create pie slice path
        using var path = CreatePieSlicePath(info);

        // Draw background
        Rectangle boundsRect = Rectangle.Round(info.Bounds);
        renderer.RenderStandardBack.DrawBack(context, boundsRect, path, palette.PaletteBack, VisualOrientation.Top, state, null);

        // Draw border
        if (palette.PaletteBorder != null)
        {
            renderer.RenderStandardBorder.DrawBorder(context, boundsRect, palette.PaletteBorder, VisualOrientation.Top,
                state);
        }

        // Draw content (text and image)
        DrawMenuItemContent(context, renderer, info, palette.PaletteContent, state);
    }

    private GraphicsPath CreatePieSlicePath(RadialMenuItemInfo info)
    {
        var path = new GraphicsPath();

        // Convert angles to radians
        float startRad = (float)(info.StartAngle * Math.PI / 180.0);
        float sweepRad = (float)(info.SweepAngle * Math.PI / 180.0);
        float endRad = startRad + sweepRad;

        // Calculate points
        PointF center = info.Center;
        float innerRadius = info.InnerRadius;
        float outerRadius = info.OuterRadius;

        // Inner arc start point
        PointF innerStart = new PointF(
            center.X + (float)(innerRadius * Math.Cos(startRad)),
            center.Y + (float)(innerRadius * Math.Sin(startRad)));

        // Outer arc start point
        PointF outerStart = new PointF(
            center.X + (float)(outerRadius * Math.Cos(startRad)),
            center.Y + (float)(outerRadius * Math.Sin(startRad)));

        // Outer arc end point
        PointF outerEnd = new PointF(
            center.X + (float)(outerRadius * Math.Cos(endRad)),
            center.Y + (float)(outerRadius * Math.Sin(endRad)));

        // Inner arc end point
        PointF innerEnd = new PointF(
            center.X + (float)(innerRadius * Math.Cos(endRad)),
            center.Y + (float)(innerRadius * Math.Sin(endRad)));

        // Create rectangle for arcs
        RectangleF outerRect = new RectangleF(
            center.X - outerRadius,
            center.Y - outerRadius,
            outerRadius * 2,
            outerRadius * 2);

        RectangleF innerRect = new RectangleF(
            center.X - innerRadius,
            center.Y - innerRadius,
            innerRadius * 2,
            innerRadius * 2);

        // Build path: outer arc -> line to inner end -> inner arc (reverse) -> line to outer start
        path.AddArc(outerRect, info.StartAngle, info.SweepAngle);
        path.AddLine(outerEnd, innerEnd);
        path.AddArc(innerRect, info.StartAngle + info.SweepAngle, -info.SweepAngle);
        path.CloseFigure();

        return path;
    }

    private void DrawMenuItemContent(RenderContext context, IRenderer renderer, RadialMenuItemInfo info, IPaletteContent? paletteContent, PaletteState state)
    {
        var item = info.Item;
        if (string.IsNullOrEmpty(item.Text) && item.Image == null)
        {
            return;
        }

        // Calculate center of the pie slice for content
        float midAngle = info.StartAngle + (info.SweepAngle / 2f);
        float midRadius = (info.InnerRadius + info.OuterRadius) / 2f;
        float midRad = (float)(midAngle * Math.PI / 180.0);

        PointF contentCenter = new PointF(
            info.Center.X + (float)(midRadius * Math.Cos(midRad)),
            info.Center.Y + (float)(midRadius * Math.Sin(midRad)));

        // Calculate content rectangle size based on arc length and ring thickness
        // Arc length = radius * angle (in radians)
        float arcLength = midRadius * (float)(info.SweepAngle * Math.PI / 180.0);
        float ringThickness = info.OuterRadius - info.InnerRadius;
        
        // Use a reasonable size that fits within the pie slice
        // Width: arc length, Height: ring thickness, but ensure minimum sizes
        int contentWidth = Math.Max((int)arcLength, 40);
        int contentHeight = Math.Max((int)ringThickness, 20);
        
        Rectangle contentRect = new Rectangle(
            (int)(contentCenter.X - contentWidth / 2),
            (int)(contentCenter.Y - contentHeight / 2),
            contentWidth,
            contentHeight);

        // Create content values
        var contentValues = new FixedContentValue(item.Text, null, item.Image, GlobalStaticValues.EMPTY_COLOR);

        // Layout content (alignment is already set to Center in StateCommonClient/Alternate)
        using var layoutContext = new ViewLayoutContext(GetViewManager(), this, this, Renderer);
        IDisposable? memento = renderer.RenderStandardContent.LayoutContent(
            layoutContext, contentRect, paletteContent, contentValues, VisualOrientation.Top, state);

        if (memento != null)
        {
            // Save graphics state
            GraphicsState gs = context.Graphics.Save();

            try
            {
                // Rotate graphics to align text with radial direction
                context.Graphics.TranslateTransform(contentCenter.X, contentCenter.Y);
                context.Graphics.RotateTransform(midAngle + 90f); // +90 to align along radius
                context.Graphics.TranslateTransform(-contentCenter.X, -contentCenter.Y);

                // Draw content
                renderer.RenderStandardContent.DrawContent(
                    context, contentRect, paletteContent, memento, VisualOrientation.Top, state, false);
            }
            finally
            {
                context.Graphics.Restore(gs);
                memento.Dispose();
            }
        }
    }

    private KryptonRadialMenuItem? HitTest(Point point)
    {
        PointF center = new PointF(Width / 2f, Height / 2f);
        PointF pointF = new PointF(point.X, point.Y);

        // Calculate distance from center
        float dx = pointF.X - center.X;
        float dy = pointF.Y - center.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        // Check if point is within center circle
        if (distance <= _layout.CenterRadius)
        {
            return null;
        }

        // Calculate angle
        float angle = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
        // Normalize to 0-360 range
        if (angle < 0)
        {
            angle += 360f;
        }
        // Adjust for start angle
        angle = (angle - _layout.StartAngle + 360f) % 360f;

        // Check each item
            foreach (var info in _itemInfos)
            {
                if (distance >= info.InnerRadius && distance <= info.OuterRadius)
                {
                    // Normalize item start angle
                    float itemStart = (info.StartAngle - _layout.StartAngle + 360f) % 360f;
                float itemEnd = (itemStart + info.SweepAngle) % 360f;

                // Check if angle is within item's sweep
                bool inRange;
                if (itemEnd > itemStart)
                {
                    inRange = angle >= itemStart && angle <= itemEnd;
                }
                else
                {
                    // Wraps around 360
                    inRange = angle >= itemStart || angle <= itemEnd;
                }

                if (inRange)
                {
                    return info.Item;
                }
            }
        }

        return null;
    }

    private void OnItemClick(object? sender, EventArgs e)
    {
        if (sender is KryptonRadialMenuItem item)
        {
            OnItemClick(new KryptonRadialMenuItemEventArgs(item));
        }
    }

    private bool IsInDragRegion(Point point)
    {
        if (_layout.CenterRadius <= 0)
        {
            return false;
        }

        PointF center = new PointF(Width / 2f, Height / 2f);
        float dx = point.X - center.X;
        float dy = point.Y - center.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        return distance <= _layout.CenterRadius;
    }

    private void FloatingWindow_WindowReturning(object? sender, EventArgs e)
    {
        _floatingWindow = null;
        OnMenuReturned(EventArgs.Empty);
    }

    private void PaintTransparentBackground(PaintEventArgs? e)
    {
        // Check if background should be transparent
        if (BackColor == Color.Transparent && Parent != null)
        {
            // Only grab the required reference once
            if (_miPTB == null)
            {
                // Use reflection so we can call the Windows Forms internal method for painting parent background
                _miPTB = typeof(Control).GetMethod(nameof(PaintTransparentBackground),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null, CallingConventions.HasThis,
                    [typeof(PaintEventArgs), typeof(Rectangle), typeof(Region)],
                    null);
            }

            try
            {
                _ = _miPTB?.Invoke(this, [e, ClientRectangle, null!]);
            }
            catch
            {
                // If reflection fails, fall back to painting parent's background color
                _miPTB = null;
                using var brush = new SolidBrush(Parent.BackColor);
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
        else
        {
            // Paint solid background
            using var brush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }
    #endregion
}

/// <summary>
/// Provides data for radial menu item events.
/// </summary>
public class KryptonRadialMenuItemEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the KryptonRadialMenuItemEventArgs class.
    /// </summary>
    /// <param name="item">The menu item that triggered the event.</param>
    public KryptonRadialMenuItemEventArgs(KryptonRadialMenuItem item)
    {
        Item = item;
    }

    /// <summary>
    /// Gets the menu item that triggered the event.
    /// </summary>
    public KryptonRadialMenuItem Item { get; }
}

