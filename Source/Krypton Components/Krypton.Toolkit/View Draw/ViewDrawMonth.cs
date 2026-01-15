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
/// Extends the ViewComposite by organising and drawing an individual month.
/// </summary>
public class ViewDrawMonth : ViewLayoutStack,
    IContentValues
{
    #region Type Definitions
    /// <summary>
    /// Collection for managing ButtonSpecCalendar instances.
    /// </summary>
    public class CalendarButtonSpecCollection : ButtonSpecCollection<ButtonSpecCalendar>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the CalendarButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public CalendarButtonSpecCollection(ViewDrawMonth owner)
            : base(owner)
        {
        }
        #endregion
    }

    /// <summary>
    /// Palette content wrapper that handles RTL text alignment for the month calendar header.
    /// </summary>
    private class MonthHeaderPaletteContent : PaletteContentInherit
    {
        private readonly IPaletteContent _inherit;
        private readonly Control _control;

        public MonthHeaderPaletteContent(IPaletteContent inherit, Control control)
        {
            _inherit = inherit;
            _control = control;
        }

        public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            // In RTL mode, align header text to the right (Far)
            if (CommonHelper.IsRightToLeftLayout(_control))
            {
                return PaletteRelativeAlign.Far;
            }

            // Use the inherited alignment (typically Center for calendar headers)
            return _inherit.GetContentShortTextH(state);
        }

        // Delegate all other methods to the inherited palette
        public override InheritBool GetContentDraw(PaletteState state) => _inherit.GetContentDraw(state);
        public override InheritBool GetContentDrawFocus(PaletteState state) => _inherit.GetContentDrawFocus(state);
        public override PaletteRelativeAlign GetContentImageH(PaletteState state) => _inherit.GetContentImageH(state);
        public override PaletteRelativeAlign GetContentImageV(PaletteState state) => _inherit.GetContentImageV(state);
        public override PaletteImageEffect GetContentImageEffect(PaletteState state) => _inherit.GetContentImageEffect(state);
        public override Color GetContentImageColorMap(PaletteState state) => _inherit.GetContentImageColorMap(state);
        public override Color GetContentImageColorTo(PaletteState state) => _inherit.GetContentImageColorTo(state);
        public override Font? GetContentShortTextFont(PaletteState state) => _inherit.GetContentShortTextFont(state);
        public override Font? GetContentShortTextNewFont(PaletteState state) => _inherit.GetContentShortTextNewFont(state);
        public override PaletteTextHint GetContentShortTextHint(PaletteState state) => _inherit.GetContentShortTextHint(state);
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => _inherit.GetContentShortTextPrefix(state);
        public override InheritBool GetContentShortTextMultiLine(PaletteState state) => _inherit.GetContentShortTextMultiLine(state);
        public override PaletteTextTrim GetContentShortTextTrim(PaletteState state) => _inherit.GetContentShortTextTrim(state);
        public override PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _inherit.GetContentShortTextV(state);
        public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => _inherit.GetContentShortTextMultiLineH(state);
        public override Color GetContentShortTextColor1(PaletteState state) => _inherit.GetContentShortTextColor1(state);
        public override Color GetContentShortTextColor2(PaletteState state) => _inherit.GetContentShortTextColor2(state);
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) => _inherit.GetContentShortTextColorStyle(state);
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) => _inherit.GetContentShortTextColorAlign(state);
        public override float GetContentShortTextColorAngle(PaletteState state) => _inherit.GetContentShortTextColorAngle(state);
        public override Image? GetContentShortTextImage(PaletteState state) => _inherit.GetContentShortTextImage(state);
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) => _inherit.GetContentShortTextImageStyle(state);
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) => _inherit.GetContentShortTextImageAlign(state);
        public override Font? GetContentLongTextFont(PaletteState state) => _inherit.GetContentLongTextFont(state);
        public override Font? GetContentLongTextNewFont(PaletteState state) => _inherit.GetContentLongTextNewFont(state);
        public override PaletteTextHint GetContentLongTextHint(PaletteState state) => _inherit.GetContentLongTextHint(state);
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => _inherit.GetContentLongTextPrefix(state);
        public override InheritBool GetContentLongTextMultiLine(PaletteState state) => _inherit.GetContentLongTextMultiLine(state);
        public override PaletteTextTrim GetContentLongTextTrim(PaletteState state) => _inherit.GetContentLongTextTrim(state);
        public override PaletteRelativeAlign GetContentLongTextH(PaletteState state) => _inherit.GetContentLongTextH(state);
        public override PaletteRelativeAlign GetContentLongTextV(PaletteState state) => _inherit.GetContentLongTextV(state);
        public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => _inherit.GetContentLongTextMultiLineH(state);
        public override Color GetContentLongTextColor1(PaletteState state) => _inherit.GetContentLongTextColor1(state);
        public override Color GetContentLongTextColor2(PaletteState state) => _inherit.GetContentLongTextColor2(state);
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) => _inherit.GetContentLongTextColorStyle(state);
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) => _inherit.GetContentLongTextColorAlign(state);
        public override float GetContentLongTextColorAngle(PaletteState state) => _inherit.GetContentLongTextColorAngle(state);
        public override Image? GetContentLongTextImage(PaletteState state) => _inherit.GetContentLongTextImage(state);
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) => _inherit.GetContentLongTextImageStyle(state);
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) => _inherit.GetContentLongTextImageAlign(state);
        public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => _inherit.GetBorderContentPadding(owningForm, state);
        public override int GetContentAdjacentGap(PaletteState state) => _inherit.GetContentAdjacentGap(state);
        public override PaletteContentStyle GetContentStyle() => _inherit.GetContentStyle();
    }
    #endregion

    #region Instance Fields
    private readonly IKryptonMonthCalendar _calendar;
    private readonly ViewLayoutMonths _months;
    private readonly ViewDrawDocker _drawHeader;
    private readonly PaletteBorderInheritForced _borderForced;
    private readonly ViewDrawContent _drawContent;
    private readonly ViewDrawMonthDayNames _drawMonthDayNames;
    private readonly ViewDrawBorderEdge _drawBorderEdge;
    private readonly ViewLayoutWeekCorner _drawWeekCorner;
    private readonly ViewDrawWeekNumbers _drawWeekNumbers;
    private readonly ViewLayoutStack _numberStack;
    private readonly PaletteBorderEdgeRedirect _borderEdgeRedirect;
    private readonly PaletteBorderEdge _borderEdge;
    private readonly ButtonSpecManagerDraw _buttonManager;
    private readonly CalendarButtonSpecCollection? _buttonSpecs;
    private readonly ButtonSpecCalendar _arrowPrev;
    private readonly ButtonSpecCalendar _arrowNext;
    private string _header;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMonth class.
    /// </summary>
    /// <param name="calendar">Reference to calendar provider.</param>
    /// <param name="months">Reference to months instance.</param>
    /// <param name="redirector">Redirector for getting values.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    public ViewDrawMonth(IKryptonMonthCalendar calendar, 
        ViewLayoutMonths months,
        PaletteRedirect redirector,
        NeedPaintHandler needPaintDelegate)
        : base(false)
    {
        _calendar = calendar;
        _months = months;

        // Add a header for showing the month/year value
        // Wrap the header content palette to handle RTL text alignment
        var headerContentPalette = new MonthHeaderPaletteContent(_calendar.StateNormal.Header.Content, _calendar.CalendarControl);
        _drawContent = new ViewDrawContent(headerContentPalette, this, VisualOrientation.Top);
        _borderForced = new PaletteBorderInheritForced(_calendar.StateNormal.Header.Border);
        _borderForced.ForceBorderEdges(PaletteDrawBorders.None);
        _drawHeader = new ViewDrawDocker(_calendar.StateNormal.Header.Back, _borderForced, null!)
        {
            { _drawContent, ViewDockStyle.Fill }
        };
        Add(_drawHeader);

        // Create the left/right arrows for moving the months
        _arrowPrev = new ButtonSpecCalendar(this, PaletteButtonSpecStyle.Previous, RelativeEdgeAlign.Near);
        _arrowNext = new ButtonSpecCalendar(this, PaletteButtonSpecStyle.Next, RelativeEdgeAlign.Far);
        _arrowPrev.Click += OnPrevMonth;
        _arrowNext.Click += OnNextMonth;
        _buttonSpecs = new CalendarButtonSpecCollection(this)
        {
            _arrowPrev,
            _arrowNext
        };

        // Using a button spec manager to add the buttons to the header
        _buttonManager = new ButtonSpecManagerDraw(_calendar.CalendarControl, redirector, null, _buttonSpecs,
            [_drawHeader],
            [_calendar.StateCommon],
            [PaletteMetricInt.HeaderButtonEdgeInsetCalendar],
            [PaletteMetricPadding.None],
            _calendar.GetToolStripDelegate, needPaintDelegate);

        // Create stacks for holding display items
        var namesStack = new ViewLayoutStack(true);
        var weeksStack = new ViewLayoutStack(true);
        var daysStack = new ViewLayoutStack(false);
        _numberStack = new ViewLayoutStack(false);
        weeksStack.Add(_numberStack);
        weeksStack.Add(daysStack);

        // Add day names
        _drawMonthDayNames = new ViewDrawMonthDayNames(_calendar, _months);
        _drawWeekCorner = new ViewLayoutWeekCorner(_calendar, _months, _calendar.StateNormal.Header.Border);
        namesStack.Add(_drawWeekCorner);
        namesStack.Add(_drawMonthDayNames);
        Add(namesStack);
        Add(weeksStack);

        // Add border between week numbers and days area
        _borderEdgeRedirect = new PaletteBorderEdgeRedirect(_calendar.StateNormal.Header.Border, null);
        _borderEdge = new PaletteBorderEdge(_borderEdgeRedirect, null);
        _drawBorderEdge = new ViewDrawBorderEdge(_borderEdge, Orientation.Vertical);
        _drawWeekNumbers = new ViewDrawWeekNumbers(_calendar, _months);
        var borderLeftDock = new ViewLayoutDocker
        {
            { _drawWeekNumbers, ViewDockStyle.Left },
            { new ViewLayoutSeparator(0, 4), ViewDockStyle.Top },
            { _drawBorderEdge, ViewDockStyle.Fill },
            { new ViewLayoutSeparator(0, 4), ViewDockStyle.Bottom }
        };
        _numberStack.Add(borderLeftDock);

        // Add border between day names and individual days
        var borderEdgeRedirect = new PaletteBorderEdgeRedirect(_calendar.StateNormal.Header.Border, null);
        var borderEdge = new PaletteBorderEdge(borderEdgeRedirect, null);
        var drawBorderEdge = new ViewDrawBorderEdge(borderEdge, Orientation.Horizontal);
        var borderTopDock = new ViewLayoutDocker
        {
            { new ViewLayoutSeparator(4, 1), ViewDockStyle.Left },
            { drawBorderEdge, ViewDockStyle.Fill },
            { new ViewLayoutSeparator(4, 1), ViewDockStyle.Right },
            { new ViewLayoutSeparator(1, 3), ViewDockStyle.Bottom }
        };
        daysStack.Add(borderTopDock);

        // Add the actual individual days
        ViewDrawMonthDays = new ViewDrawMonthDays(_calendar, _months);
        daysStack.Add(ViewDrawMonthDays);

        // Adding buttons manually means we have to ask for buttons to be created
        _buttonManager.RecreateButtons();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMonth:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the days draw element.
    /// </summary>
    public ViewDrawMonthDays ViewDrawMonthDays { get; }

    /// <summary>
    /// Gets and sets the enabled state of the view.
    /// </summary>
    public override bool Enabled
    {
        get => base.Enabled;

        set
        {
            _drawContent.Enabled = value;
            _drawHeader.Enabled = value;
            _drawMonthDayNames.Enabled = value;
            _drawBorderEdge.Enabled = value;
            ViewDrawMonthDays.Enabled = value;
            base.Enabled = value;
        }
    }

    /// <summary>
    /// Is this the first month in the group.
    /// </summary>
    public bool FirstMonth
    {
        set 
        { 
            ViewDrawMonthDays.FirstMonth = value;
            _drawWeekNumbers.FirstMonth = value;
        }
    }

    /// <summary>
    /// Is this the last month in the group.
    /// </summary>
    public bool LastMonth
    {
        set 
        {
            ViewDrawMonthDays.LastMonth = value;
            _drawWeekNumbers.LastMonth = value;
        }
    }

    /// <summary>
    /// Gets and sets the month that this view element is used to draw.
    /// </summary>
    public DateTime Month
    {
        set
        {
            _header = value.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
            ViewDrawMonthDays.Month = value;
            _drawWeekNumbers.Month = value;
        }
    }

    /// <summary>
    /// Update the visible state of the navigation buttons.
    /// </summary>
    /// <param name="prev">Show the previous button.</param>
    /// <param name="next">Show the next button.</param>
    public void UpdateButtons(bool prev, bool next)
    {
        _arrowPrev.Visible = prev;
        _arrowNext.Visible = next;
        _buttonManager.RefreshButtons();
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        UpdateWeekNumberViews();
        return base.GetPreferredSize(context);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        UpdateWeekNumberViews();
        base.Layout(context);
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => _header;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <summary>
    /// Gets the scale factor for the overlay image (used when scale mode is Percentage or ProportionalToMain).
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor (0.0 to 2.0).</returns>
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <summary>
    /// Gets the fixed size for the overlay image (used when scale mode is FixedSize).
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size.</returns>
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    #endregion

    #region Implementation
    private void UpdateWeekNumberViews()
    {
        // Update display of week numbers views
        var showWeekNumbers = _months.ShowWeekNumbers;
        _drawWeekCorner.Visible = showWeekNumbers;
        _numberStack.Visible = showWeekNumbers;
    }

    private void OnNextMonth(object? sender, EventArgs e) => _months.NextMonth();

    private void OnPrevMonth(object? sender, EventArgs e) => _months.PrevMonth();

    #endregion
}