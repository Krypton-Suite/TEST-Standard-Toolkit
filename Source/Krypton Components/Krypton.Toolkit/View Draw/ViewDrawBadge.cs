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
/// View element that can draw a badge.
/// </summary>
public class ViewDrawBadge : ViewLeaf
{
    #region Instance Fields
    private readonly BadgeValues _badgeValues;
    private readonly Control _control;
    private const int DEFAULT_BADGE_SIZE = 18;
    private const int BADGE_MIN_SIZE = 16;
    private const int BADGE_OFFSET = 3;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawBadge class.
    /// </summary>
    /// <param name="badgeValues">Source for badge values.</param>
    /// <param name="control">Control instance for DPI awareness.</param>
    public ViewDrawBadge(BadgeValues badgeValues, Control control)
    {
        _badgeValues = badgeValues;
        _control = control;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawBadge:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Badge has no preferred size - it's positioned absolutely
        return Size.Empty;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Only layout if badge is visible and has content (text or image)
        if (!_badgeValues.Visible || (string.IsNullOrEmpty(_badgeValues.Text) && _badgeValues.Image == null))
        {
            ClientRectangle = Rectangle.Empty;
            return;
        }

        // Calculate badge size based on text
        Size badgeSize = CalculateBadgeSize(context!);

        // Calculate position based on BadgePosition enum relative to parent's ClientRectangle
        // Use the display rectangle which should be the button's client rectangle
        Rectangle parentRect = context!.DisplayRectangle;
        Point badgeLocation = CalculateBadgeLocation(parentRect, badgeSize);

        // Set the client rectangle for the badge (relative to parent)
        ClientRectangle = new Rectangle(badgeLocation, badgeSize);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Only render if visible and has content (text or image)
        if (!Visible || !_badgeValues.Visible || (string.IsNullOrEmpty(_badgeValues.Text) && _badgeValues.Image == null))
        {
            return;
        }

        RenderBadge(context!);
    }
    #endregion

    #region Implementation
    private Size CalculateBadgeSize(ViewLayoutContext context)
    {
        // If image is provided, use image size with padding
        if (_badgeValues.Image != null)
        {
            int padding = 4; // Padding around image
            // For images, use a reasonable badge size based on the image
            int imageMax = Math.Max(_badgeValues.Image.Width, _badgeValues.Image.Height);
            int size = Math.Max(BADGE_MIN_SIZE, Math.Min(imageMax + padding, 32)); // Cap at 32px for reasonable badge size
            return new Size(size, size);
        }

        // Otherwise calculate based on text
        string text = _badgeValues.Text ?? "";
        
        if (string.IsNullOrEmpty(text))
        {
            return new Size(DEFAULT_BADGE_SIZE, DEFAULT_BADGE_SIZE);
        }

        // Use the graphics from the context to measure text
        using var g = context.Control?.CreateGraphics();
        if (g == null)
        {
            return new Size(DEFAULT_BADGE_SIZE, DEFAULT_BADGE_SIZE);
        }

        // Use a default font for measurement
        using var font = new Font("Segoe UI", 7.5f, FontStyle.Bold, GraphicsUnit.Point);
        SizeF textSize = g.MeasureString(text, font);
        
        // Calculate badge size - circular, so use the larger dimension plus padding
        int diameter = Math.Max(BADGE_MIN_SIZE, (int)Math.Max(textSize.Width, textSize.Height) + 8);
        return new Size(diameter, diameter);
    }

    private Point CalculateBadgeLocation(Rectangle parentRect, Size badgeSize)
    {
        int offset = BADGE_OFFSET;
        Point location;

        switch (_badgeValues.Position)
        {
            case BadgePosition.TopRight:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Top + offset);
                break;
            case BadgePosition.TopLeft:
                location = new Point(parentRect.Left + offset, parentRect.Top + offset);
                break;
            case BadgePosition.BottomRight:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Bottom - badgeSize.Height - offset);
                break;
            case BadgePosition.BottomLeft:
                location = new Point(parentRect.Left + offset, parentRect.Bottom - badgeSize.Height - offset);
                break;
            default:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Top + offset);
                break;
        }

        return location;
    }

    private void RenderBadge(RenderContext context)
    {
        Rectangle badgeRect = ClientRectangle;

        if (badgeRect.IsEmpty || badgeRect.Width <= 0 || badgeRect.Height <= 0)
        {
            return;
        }

        Graphics g = context.Graphics;

        // Enable anti-aliasing for smoother rendering
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        // Draw the circular badge background
        using (var badgeBrush = new SolidBrush(_badgeValues.BadgeColor))
        {
            g.FillEllipse(badgeBrush, badgeRect);
        }

        // Draw image if provided, otherwise draw text
        if (_badgeValues.Image != null)
        {
            // Scale image to fit within badge with padding
            int padding = 4;
            int maxImageSize = Math.Min(badgeRect.Width, badgeRect.Height) - padding;
            
            // Calculate scaled size maintaining aspect ratio
            int imageWidth = _badgeValues.Image.Width;
            int imageHeight = _badgeValues.Image.Height;
            float scale = Math.Min((float)maxImageSize / imageWidth, (float)maxImageSize / imageHeight);
            int scaledWidth = (int)(imageWidth * scale);
            int scaledHeight = (int)(imageHeight * scale);
            
            // Center the image in the badge
            int imageX = badgeRect.Left + (badgeRect.Width - scaledWidth) / 2;
            int imageY = badgeRect.Top + (badgeRect.Height - scaledHeight) / 2;
            Rectangle imageRect = new Rectangle(imageX, imageY, scaledWidth, scaledHeight);
            
            // Draw the image with high quality interpolation
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(_badgeValues.Image, imageRect);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
        }
        else
        {
            // Draw the badge text
            string text = _badgeValues.Text ?? "";
            if (!string.IsNullOrEmpty(text))
            {
                using var textFont = new Font("Segoe UI", 7.5f, FontStyle.Bold, GraphicsUnit.Point);
                using var textBrush = new SolidBrush(_badgeValues.TextColor);
                using var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                };

                g.DrawString(text, textFont, textBrush, badgeRect, stringFormat);
            }
        }
    }
    #endregion
}
