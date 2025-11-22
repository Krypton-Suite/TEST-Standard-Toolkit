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
                 ControlStyles.Selectable, true);

        _value = 0;
        _maximum = 5;
        _hoveredValue = -1;
        _readOnly = false;
        _starColor = Color.Gold;
        _emptyStarColor = Color.LightGray;
        _starSize = new Size(20, 20);
        _starSpacing = 4;

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

    #region Protected Overrides
    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

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
            Color starFillColor = isFilled ? _starColor : _emptyStarColor;

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

