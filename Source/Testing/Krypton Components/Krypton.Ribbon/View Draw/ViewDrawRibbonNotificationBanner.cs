#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Ribbon;

/// <summary>
/// Draws a notification banner below the ribbon tabs.
/// </summary>
internal class ViewDrawRibbonNotificationBanner : ViewDrawPanel
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly NeedPaintHandler _needPaint;
    private RibbonNotificationBannerData? _bannerData;
    private Rectangle _iconRect;
    private Rectangle _headingRect;
    private Rectangle _messageRect;
    private Rectangle _actionButtonRect;
    private Rectangle _dismissButtonRect;
    private bool _actionButtonHot;
    private bool _dismissButtonHot;
    private bool _actionButtonPressed;
    private bool _dismissButtonPressed;
    private IDisposable? _mementoBackground;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonNotificationBanner class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying paint/layout changes.</param>
    public ViewDrawRibbonNotificationBanner([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] NeedPaintHandler needPaint)
        : base()
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(needPaint != null);

        _ribbon = ribbon!;
        _needPaint = needPaint!;
        _bannerData = null;
        Visible = false;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawRibbonNotificationBanner:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _mementoBackground?.Dispose();
            _mementoBackground = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the banner data.
    /// </summary>
    public RibbonNotificationBannerData? BannerData
    {
        get => _bannerData;
        set
        {
            if (_bannerData != value)
            {
                _bannerData = value;
                Visible = _bannerData != null && (!string.IsNullOrEmpty(_bannerData.HeadingText) || !string.IsNullOrEmpty(_bannerData.MessageText));
                PerformNeedPaint(true);
            }
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (_bannerData == null || !Visible)
        {
            return Size.Empty;
        }

        Debug.Assert(context != null);

        // Get default font from ribbon
        Font defaultFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);
        Font headingFont = _bannerData.HeadingFont ?? defaultFont;
        Font messageFont = _bannerData.MessageFont ?? defaultFont;

        int height = 0;
        int width = context.DisplayRectangle.Width;

        // Add padding
        Padding padding = _bannerData.Padding;
        int availableWidth = width - padding.Horizontal;

        // Calculate icon space
        int iconWidth = 0;
        if (_bannerData.Icon != null)
        {
            iconWidth = _bannerData.Icon.Width + _bannerData.IconSpacing;
        }

        // Calculate text widths
        int textAreaWidth = availableWidth - iconWidth;
        if (!string.IsNullOrEmpty(_bannerData.ActionButtonText))
        {
            textAreaWidth -= 100; // Estimate for button
        }
        if (_bannerData.ShowDismissButton)
        {
            textAreaWidth -= 24; // Dismiss button width
        }

        // Calculate heading height
        if (!string.IsNullOrEmpty(_bannerData.HeadingText))
        {
            using var g = CreateGraphics();
            SizeF headingSize = g.MeasureString(_bannerData.HeadingText, headingFont, textAreaWidth);
            height += (int)Math.Ceiling(headingSize.Height);
            if (!string.IsNullOrEmpty(_bannerData.MessageText))
            {
                height += _bannerData.HeadingMessageSpacing;
            }
        }

        // Calculate message height
        if (!string.IsNullOrEmpty(_bannerData.MessageText))
        {
            using var g = CreateGraphics();
            SizeF messageSize = g.MeasureString(_bannerData.MessageText, messageFont, textAreaWidth);
            height += (int)Math.Ceiling(messageSize.Height);
        }

        // Add icon height if present
        if (_bannerData.Icon != null && _bannerData.Icon.Height > height)
        {
            height = _bannerData.Icon.Height;
        }

        // Minimum height based on button height if buttons present
        int minButtonHeight = 0;
        if (!string.IsNullOrEmpty(_bannerData.ActionButtonText) || _bannerData.ShowDismissButton)
        {
            minButtonHeight = 24; // Standard button height
        }

        if (height < minButtonHeight)
        {
            height = minButtonHeight;
        }

        height += padding.Vertical;

        // Apply min/max constraints
        if (_bannerData.MinimumHeight.HasValue && height < _bannerData.MinimumHeight.Value)
        {
            height = _bannerData.MinimumHeight.Value;
        }

        if (_bannerData.MaximumHeight.HasValue && height > _bannerData.MaximumHeight.Value)
        {
            height = _bannerData.MaximumHeight.Value;
        }

        return new Size(width, height);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        if (_bannerData == null || !Visible)
        {
            ClientRectangle = Rectangle.Empty;
            return;
        }

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Calculate layout rectangles
        CalculateLayout();
    }

    private void CalculateLayout()
    {
        if (_bannerData == null)
        {
            return;
        }

        Padding padding = _bannerData.Padding;
        Rectangle contentRect = new Rectangle(
            ClientRectangle.X + padding.Left,
            ClientRectangle.Y + padding.Top,
            ClientRectangle.Width - padding.Horizontal,
            ClientRectangle.Height - padding.Vertical);

        int x = contentRect.X;
        int y = contentRect.Y;
        int availableWidth = contentRect.Width;

        // Icon rectangle
        _iconRect = Rectangle.Empty;
        if (_bannerData.Icon != null)
        {
            int iconY = contentRect.Y;
            switch (_bannerData.IconAlignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    iconY = contentRect.Y;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    iconY = contentRect.Y + (contentRect.Height - _bannerData.Icon.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    iconY = contentRect.Bottom - _bannerData.Icon.Height;
                    break;
            }

            _iconRect = new Rectangle(x, iconY, _bannerData.Icon.Width, _bannerData.Icon.Height);
            x += _bannerData.Icon.Width + _bannerData.IconSpacing;
            availableWidth -= _bannerData.Icon.Width + _bannerData.IconSpacing;
        }

        // Dismiss button rectangle (right side)
        _dismissButtonRect = Rectangle.Empty;
        if (_bannerData.ShowDismissButton)
        {
            int dismissSize = 20;
            _dismissButtonRect = new Rectangle(
                contentRect.Right - dismissSize - 4,
                contentRect.Y + (contentRect.Height - dismissSize) / 2,
                dismissSize,
                dismissSize);
            availableWidth -= dismissSize + 4 + _bannerData.ActionDismissSpacing;
        }

        // Action button rectangle (before dismiss)
        _actionButtonRect = Rectangle.Empty;
        if (!string.IsNullOrEmpty(_bannerData.ActionButtonText))
        {
            // Estimate button size - in real implementation would measure text
            int buttonWidth = 80;
            int buttonHeight = 24;
            _actionButtonRect = new Rectangle(
                contentRect.Right - (_bannerData.ShowDismissButton ? 20 + 4 + _bannerData.ActionDismissSpacing : 0) - buttonWidth,
                contentRect.Y + (contentRect.Height - buttonHeight) / 2,
                buttonWidth,
                buttonHeight);
            availableWidth -= buttonWidth + _bannerData.MessageActionSpacing;
        }

        // Text rectangles
        int textY = contentRect.Y;
        Font defaultFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);
        Font headingFont = _bannerData.HeadingFont ?? defaultFont;
        Font messageFont = _bannerData.MessageFont ?? defaultFont;

        if (!string.IsNullOrEmpty(_bannerData.HeadingText))
        {
            using var g = CreateGraphics();
            SizeF headingSize = g.MeasureString(_bannerData.HeadingText, headingFont, availableWidth);
            _headingRect = new Rectangle(x, textY, availableWidth, (int)Math.Ceiling(headingSize.Height));
            textY += _headingRect.Height;
            if (!string.IsNullOrEmpty(_bannerData.MessageText))
            {
                textY += _bannerData.HeadingMessageSpacing;
            }
        }
        else
        {
            _headingRect = Rectangle.Empty;
        }

        if (!string.IsNullOrEmpty(_bannerData.MessageText))
        {
            using var g = CreateGraphics();
            SizeF messageSize = g.MeasureString(_bannerData.MessageText, messageFont, availableWidth);
            _messageRect = new Rectangle(x, textY, availableWidth, (int)Math.Ceiling(messageSize.Height));
        }
        else
        {
            _messageRect = Rectangle.Empty;
        }
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (_bannerData == null || !Visible)
        {
            return;
        }

        // Render background
        Color backColor = _bannerData.BackgroundColor ?? Color.FromArgb(255, 255, 242, 204); // Default light yellow
        using (var brush = new SolidBrush(backColor))
        {
            context.Graphics.FillRectangle(brush, ClientRectangle);
        }

        // Render border line at bottom
        using (var pen = new Pen(Color.FromArgb(255, 200, 200, 200)))
        {
            context.Graphics.DrawLine(pen, ClientRectangle.Left, ClientRectangle.Bottom - 1, ClientRectangle.Right, ClientRectangle.Bottom - 1);
        }
    }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        if (_bannerData == null || !Visible)
        {
            return;
        }

        // Render icon
        if (_bannerData.Icon != null && !_iconRect.IsEmpty)
        {
            context.Graphics.DrawImage(_bannerData.Icon, _iconRect);
        }

        // Render heading text
        if (!string.IsNullOrEmpty(_bannerData.HeadingText) && !_headingRect.IsEmpty)
        {
            Font defaultFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);
            Font headingFont = _bannerData.HeadingFont ?? defaultFont;
            Color headingColor = _bannerData.HeadingForegroundColor ?? Color.Black;

            using (var brush = new SolidBrush(headingColor))
            using (var format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter })
            {
                format.FormatFlags |= StringFormatFlags.NoWrap;
                context.Graphics.DrawString(_bannerData.HeadingText, headingFont, brush, _headingRect, format);
            }
        }

        // Render message text
        if (!string.IsNullOrEmpty(_bannerData.MessageText) && !_messageRect.IsEmpty)
        {
            Font defaultFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);
            Font messageFont = _bannerData.MessageFont ?? defaultFont;
            Color messageColor = _bannerData.ForegroundColor ?? Color.Black;

            using (var brush = new SolidBrush(messageColor))
            using (var format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter })
            {
                context.Graphics.DrawString(_bannerData.MessageText, messageFont, brush, _messageRect, format);
            }
        }

        // Render action button
        if (!string.IsNullOrEmpty(_bannerData.ActionButtonText) && !_actionButtonRect.IsEmpty)
        {
            RenderButton(context, _actionButtonRect, _bannerData.ActionButtonText, _actionButtonHot, _actionButtonPressed);
        }

        // Render dismiss button
        if (_bannerData.ShowDismissButton && !_dismissButtonRect.IsEmpty)
        {
            RenderDismissButton(context, _dismissButtonRect, _dismissButtonHot, _dismissButtonPressed);
        }
    }

    private void RenderButton(RenderContext context, Rectangle rect, string text, bool hot, bool pressed)
    {
        Color backColor = pressed ? Color.FromArgb(255, 200, 200, 200) : hot ? Color.FromArgb(255, 220, 220, 220) : Color.FromArgb(255, 240, 240, 240);
        using (var brush = new SolidBrush(backColor))
        {
            context.Graphics.FillRectangle(brush, rect);
        }

        using (var pen = new Pen(Color.FromArgb(255, 180, 180, 180)))
        {
            context.Graphics.DrawRectangle(pen, rect);
        }

        using (var brush = new SolidBrush(Color.Black))
        using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
        {
            Font defaultFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);
            context.Graphics.DrawString(text, defaultFont, brush, rect, format);
        }
    }

    private void RenderDismissButton(RenderContext context, Rectangle rect, bool hot, bool pressed)
    {
        Color backColor = pressed ? Color.FromArgb(255, 200, 200, 200) : hot ? Color.FromArgb(255, 220, 220, 220) : Color.Transparent;
        if (backColor != Color.Transparent)
        {
            using (var brush = new SolidBrush(backColor))
            {
                context.Graphics.FillRectangle(brush, rect);
            }
        }

        using (var pen = new Pen(Color.FromArgb(255, 120, 120, 120), 2))
        {
            int margin = 4;
            context.Graphics.DrawLine(pen, rect.Left + margin, rect.Top + margin, rect.Right - margin, rect.Bottom - margin);
            context.Graphics.DrawLine(pen, rect.Right - margin, rect.Top + margin, rect.Left + margin, rect.Bottom - margin);
        }
    }
    #endregion

    #region Implementation
    private void PerformNeedPaint(bool needLayout) => _needPaint(this, new NeedLayoutEventArgs(needLayout));

    private Graphics CreateGraphics() => _ribbon.CreateGraphics();
    #endregion
}
