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
/// Provides a rating control that displays stars and allows users to select a rating value.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Description(@"Provides a rating control that displays stars and allows users to select a rating value.")]
public class KryptonRating : Control
{
    #region Instance Fields
    private int _value;
    private int _maximum;
    private int _hoveredValue;
    private bool _readOnly;
    private Color _starColor;
    private Color _emptyStarColor;
    private Size _starSize;
    private int _starSpacing;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
    private PaletteDouble? _stateTracking;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the Value property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Value property changes.")]
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRating class.
    /// </summary>
    public KryptonRating()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.Selectable, true);

        _value = 0;
        _maximum = 5;
        _hoveredValue = -1;
        _readOnly = false;
        _starColor = Color.Gold;
        _emptyStarColor = Color.LightGray;
        _starSize = new Size(20, 20);
        _starSpacing = 4;

        // Set transparent background by default
        BackColor = Color.Transparent;

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(_palette);

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateTracking = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Set default size
        Size = new Size(_maximum * (_starSize.Width + _starSpacing) - _starSpacing + 4, _starSize.Height + 4);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the current rating value.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The current rating value.")]
    [DefaultValue(0)]
    public int Value
    {
        get => _value;
        set
        {
            value = Math.Max(0, Math.Min(value, _maximum));
            if (_value != value)
            {
                _value = value;
                OnValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum rating value (number of stars).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The maximum rating value (number of stars).")]
    [DefaultValue(5)]
    public int Maximum
    {
        get => _maximum;
        set
        {
            if (value < 1)
            {
                value = 1;
            }

            if (_maximum != value)
            {
                _maximum = value;
                if (_value > _maximum)
                {
                    Value = _maximum;
                }

                // Recalculate size
                Size = new Size(_maximum * (_starSize.Width + _starSpacing) - _starSpacing + 4, _starSize.Height + 4);
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is read-only.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control is read-only.")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            if (_readOnly != value)
            {
                _readOnly = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the color of filled stars.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The color of filled stars.")]
    [DefaultValue(typeof(Color), "Gold")]
    public Color StarColor
    {
        get => _starColor;
        set
        {
            if (_starColor != value)
            {
                _starColor = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the color of empty stars.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The color of empty stars.")]
    [DefaultValue(typeof(Color), "LightGray")]
    public Color EmptyStarColor
    {
        get => _emptyStarColor;
        set
        {
            if (_emptyStarColor != value)
            {
                _emptyStarColor = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of each star.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The size of each star.")]
    [DefaultValue(typeof(Size), "20, 20")]
    public Size StarSize
    {
        get => _starSize;
        set
        {
            if (_starSize != value)
            {
                _starSize = value;
                if (_starSize.Width < 8)
                {
                    _starSize.Width = 8;
                }

                if (_starSize.Height < 8)
                {
                    _starSize.Height = 8;
                }

                // Recalculate size
                Size = new Size(_maximum * (_starSize.Width + _starSpacing) - _starSpacing + 4, _starSize.Height + 4);
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the spacing between stars.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The spacing between stars.")]
    [DefaultValue(4)]
    public int StarSpacing
    {
        get => _starSpacing;
        set
        {
            if (_starSpacing != value)
            {
                _starSpacing = value;
                // Recalculate size
                Size = new Size(_maximum * (_starSize.Width + _starSpacing) - _starSpacing + 4, _starSize.Height + 4);
                Invalidate();
            }
        }
    }
    #endregion

    #region Palette Properties
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

            // Clean up palette objects
            _stateTracking = null;
            _stateNormal = null;
            _stateDisabled = null;
            _stateCommon = null;
            _redirector = null;
            _palette = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Palette Properties
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
                        Invalidate();
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
                Invalidate();
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
    /// Gets access to the common rating appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common rating appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonRating));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonRating));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonRating));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;

    /// <summary>
    /// Gets access to the tracking (hover) rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking (hover) rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateTracking => _stateTracking?.Back ?? throw new ObjectDisposedException(nameof(KryptonRating));

    private bool ShouldSerializeStateTracking() => _stateTracking != null && !_stateTracking.Back.IsDefault;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Draw background if not transparent
        if (BackColor != Color.Transparent)
        {
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }
        }

        int startX = 2;
        int startY = (Height - _starSize.Height) / 2;

        int displayValue = _hoveredValue >= 0 && !_readOnly ? _hoveredValue : _value;

        for (int i = 0; i < _maximum; i++)
        {
            Rectangle starRect = new Rectangle(
                startX + i * (_starSize.Width + _starSpacing),
                startY,
                _starSize.Width,
                _starSize.Height);

            bool isFilled = i < displayValue;
            Color starFillColor = GetStarColor(isFilled, i < _value);

            DrawStar(g, starRect, starFillColor);
        }
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_readOnly)
        {
            return;
        }

        int oldHoveredValue = _hoveredValue;
        _hoveredValue = GetStarIndexFromPoint(e.Location);

        if (_hoveredValue != oldHoveredValue)
        {
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_hoveredValue >= 0)
        {
            _hoveredValue = -1;
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the MouseClick event.
    /// </summary>
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (_readOnly || e.Button != MouseButtons.Left)
        {
            return;
        }

        int clickedStar = GetStarIndexFromPoint(e.Location);
        if (clickedStar >= 0)
        {
            Value = clickedStar + 1; // Star index is 0-based, value is 1-based
        }
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
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
        }
    }

    private Color GetStarColor(bool isFilled, bool isSelected)
    {
        // Determine the palette state
        PaletteState paletteState;
        PaletteBack? paletteBack = null;

        if (!Enabled)
        {
            paletteState = PaletteState.Disabled;
            paletteBack = _stateDisabled?.Back;
        }
        else if (_hoveredValue >= 0 && !_readOnly)
        {
            paletteState = PaletteState.Tracking;
            paletteBack = _stateTracking?.Back;
        }
        else
        {
            paletteState = PaletteState.Normal;
            paletteBack = _stateNormal?.Back;
        }

        // Try to get color from palette
        Color? paletteColor = null;
        if (paletteBack != null)
        {
            if (isFilled)
            {
                // For filled stars, try Color1 first, then Color2
                var color1 = paletteBack.GetBackColor1(paletteState);
                var color2 = paletteBack.GetBackColor2(paletteState);
                paletteColor = color1 != Color.Empty ? color1 : (color2 != Color.Empty ? color2 : null);
            }
            else
            {
                // For empty stars, try Color2 first, then a lighter version of Color1
                var color2 = paletteBack.GetBackColor2(paletteState);
                var color1 = paletteBack.GetBackColor1(paletteState);
                paletteColor = color2 != Color.Empty ? color2 : (color1 != Color.Empty ? ControlPaint.Light(color1, 0.7f) : null);
            }
        }

        // Fall back to direct color properties if palette doesn't provide colors
        if (paletteColor.HasValue && paletteColor.Value != Color.Empty)
        {
            return paletteColor.Value;
        }

        // Use direct color properties as fallback
        return isFilled ? _starColor : _emptyStarColor;
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
            Invalidate();
        }
    }

    private int GetStarIndexFromPoint(Point point)
    {
        int startX = 2;
        int startY = (Height - _starSize.Height) / 2;

        for (int i = 0; i < _maximum; i++)
        {
            Rectangle starRect = new Rectangle(
                startX + i * (_starSize.Width + _starSpacing),
                startY,
                _starSize.Width,
                _starSize.Height);

            if (starRect.Contains(point))
            {
                return i;
            }
        }

        return -1;
    }

    private void DrawStar(Graphics g, Rectangle rect, Color fillColor)
    {
        // Create a simple star path
        PointF center = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        float radius = Math.Min(rect.Width, rect.Height) / 2f - 2;
        float innerRadius = radius * 0.4f;

        PointF[] points = new PointF[10];
        double angle = -Math.PI / 2; // Start at top
        double angleStep = Math.PI / 5; // 5 points for outer, 5 for inner

        for (int i = 0; i < 10; i++)
        {
            float r = (i % 2 == 0) ? radius : innerRadius;
            points[i] = new PointF(
                center.X + (float)(r * Math.Cos(angle)),
                center.Y + (float)(r * Math.Sin(angle)));
            angle += angleStep;
        }

        using (var brush = new SolidBrush(fillColor))
        using (var path = new System.Drawing.Drawing2D.GraphicsPath())
        {
            path.AddPolygon(points);
            g.FillPath(brush, path);

            // Draw outline
            using (var pen = new Pen(Color.FromArgb(128, fillColor), 1))
            {
                g.DrawPath(pen, path);
            }
        }
    }
    #endregion
}

