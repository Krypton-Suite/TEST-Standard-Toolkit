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

using Timer = System.Windows.Forms.Timer;
using GraphicsPath = System.Drawing.Drawing2D.GraphicsPath;
using ColorMatrix = System.Drawing.Imaging.ColorMatrix;
using ImageAttributes = System.Drawing.Imaging.ImageAttributes;
using ColorMatrixFlag = System.Drawing.Imaging.ColorMatrixFlag;
using ColorAdjustType = System.Drawing.Imaging.ColorAdjustType;

namespace Krypton.Toolkit;

/// <summary>
/// View element that can draw a badge.
/// </summary>
public class ViewDrawBadge : ViewLeaf
{
    #region Instance Fields
    private readonly BadgeValues _badgeValues;
    private readonly Control _control;
    private Timer? _animationTimer;
    private float _animationOpacity = 1.0f;
    private float _animationScale = 1.0f;
    private bool _animationDirection = true; // true = increasing, false = decreasing
    private const int DEFAULT_BADGE_SIZE = 18;
    private const int BADGE_MIN_SIZE = 16;
    private const int BADGE_OFFSET = 3;
    private const int ANIMATION_INTERVAL = 50; // ms between animation frames
    private const float FADE_MIN_OPACITY = 0.3f;
    private const float FADE_MAX_OPACITY = 1.0f;
    private const float PULSE_MIN_SCALE = 0.85f;
    private const float PULSE_MAX_SCALE = 1.0f;
    private const float PULSE_MIN_OPACITY = 0.6f;
    private const float PULSE_MAX_OPACITY = 1.0f;
    private const float ANIMATION_STEP = 0.05f;
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
        _animationOpacity = 1.0f;
        _animationScale = 1.0f;
        _animationDirection = true;
        
        // Setup animation timer if needed
        UpdateAnimationTimer();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawBadge:{Id}";

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Stop and dispose animation timer
            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= OnAnimationTick;
                _animationTimer.Dispose();
                _animationTimer = null;
            }
        }

        base.Dispose(disposing);
    }

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

        // Update animation timer if needed
        UpdateAnimationTimer();

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

        // Use the badge font or default font for measurement
        Font measureFont = _badgeValues.Font ?? new Font("Segoe UI", 7.5f, FontStyle.Bold, GraphicsUnit.Point);
        using (measureFont)
        {
            SizeF textSize = g.MeasureString(text, measureFont);
            
            // For non-circle shapes, we might want different sizing
            int padding = _badgeValues.Shape == BadgeShape.Circle ? 8 : 6;
            int diameter = Math.Max(BADGE_MIN_SIZE, (int)Math.Max(textSize.Width, textSize.Height) + padding);
            return new Size(diameter, diameter);
        }
        
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

        // Apply animation scale if pulsing
        Rectangle drawRect = badgeRect;
        if (_badgeValues.Animation == BadgeAnimation.Pulse && _animationScale != 1.0f)
        {
            int scaledWidth = (int)(badgeRect.Width * _animationScale);
            int scaledHeight = (int)(badgeRect.Height * _animationScale);
            int offsetX = (badgeRect.Width - scaledWidth) / 2;
            int offsetY = (badgeRect.Height - scaledHeight) / 2;
            drawRect = new Rectangle(badgeRect.X + offsetX, badgeRect.Y + offsetY, scaledWidth, scaledHeight);
        }

        // Calculate opacity based on animation
        float opacity = GetAnimationOpacity();
        Color badgeColor = _badgeValues.BadgeColor;
        if (opacity < 1.0f)
        {
            badgeColor = Color.FromArgb((int)(opacity * 255), badgeColor.R, badgeColor.G, badgeColor.B);
        }

        // Draw the badge background based on shape
        using (var badgeBrush = new SolidBrush(badgeColor))
        {
            switch (_badgeValues.Shape)
            {
                case BadgeShape.Circle:
                    g.FillEllipse(badgeBrush, drawRect);
                    break;
                case BadgeShape.Square:
                    g.FillRectangle(badgeBrush, drawRect);
                    break;
                case BadgeShape.RoundedRectangle:
                    int radius = Math.Min(drawRect.Width, drawRect.Height) / 4;
                    FillRoundedRectangle(g, badgeBrush, drawRect, radius);
                    break;
            }
        }

        // Draw image if provided, otherwise draw text
        if (_badgeValues.Image != null)
        {
            // Calculate opacity for image
            ColorMatrix? colorMatrix = null;
            ImageAttributes? imageAttributes = null;
            if (opacity < 1.0f)
            {
                colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, opacity, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                });
                imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }

            // Scale image to fit within badge with padding
            int padding = 4;
            int maxImageSize = Math.Min(drawRect.Width, drawRect.Height) - padding;
            
            // Calculate scaled size maintaining aspect ratio
            int imageWidth = _badgeValues.Image.Width;
            int imageHeight = _badgeValues.Image.Height;
            float scale = Math.Min((float)maxImageSize / imageWidth, (float)maxImageSize / imageHeight);
            int scaledWidth = (int)(imageWidth * scale);
            int scaledHeight = (int)(imageHeight * scale);
            
            // Center the image in the badge
            int imageX = drawRect.Left + (drawRect.Width - scaledWidth) / 2;
            int imageY = drawRect.Top + (drawRect.Height - scaledHeight) / 2;
            Rectangle imageRect = new Rectangle(imageX, imageY, scaledWidth, scaledHeight);
            
            // Draw the image with high quality interpolation
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            
            if (imageAttributes != null)
            {
                g.DrawImage(_badgeValues.Image, imageRect, 0, 0, imageWidth, imageHeight, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
                colorMatrix = null;
            }
            else
            {
                g.DrawImage(_badgeValues.Image, imageRect);
            }
            
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
        }
        else
        {
            // Draw the badge text
            string text = _badgeValues.Text ?? "";
            if (!string.IsNullOrEmpty(text))
            {
                Font textFont = _badgeValues.Font ?? new Font("Segoe UI", 7.5f, FontStyle.Bold, GraphicsUnit.Point);
                Color textColor = _badgeValues.TextColor;
                if (opacity < 1.0f)
                {
                    textColor = Color.FromArgb((int)(opacity * 255), textColor.R, textColor.G, textColor.B);
                }
                
                using (textFont)
                using (var textBrush = new SolidBrush(textColor))
                using (var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                {
                    g.DrawString(text, textFont, textBrush, drawRect, stringFormat);
                }
            }
        }
    }

    private void FillRoundedRectangle(Graphics g, Brush brush, Rectangle rect, int radius)
    {
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            g.FillPath(brush, path);
        }
    }

    private float GetAnimationOpacity()
    {
        return _badgeValues.Animation switch
        {
            BadgeAnimation.FadeInOut => _animationOpacity,
            BadgeAnimation.Pulse => _animationOpacity,
            _ => 1.0f
        };
    }

    private void UpdateAnimationTimer()
    {
        // Stop existing timer
        if (_animationTimer != null)
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
            _animationTimer.Dispose();
            _animationTimer = null;
        }

        // Start new timer if animation is enabled
        if (_badgeValues.Animation != BadgeAnimation.None && _badgeValues.Visible)
        {
            _animationTimer = new Timer
            {
                Interval = ANIMATION_INTERVAL
            };
            _animationTimer.Tick += OnAnimationTick;
            
            // Reset animation state
            _animationOpacity = _badgeValues.Animation == BadgeAnimation.FadeInOut ? FADE_MIN_OPACITY : PULSE_MAX_OPACITY;
            _animationScale = PULSE_MAX_SCALE;
            _animationDirection = true;
            
            _animationTimer.Start();
        }
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_badgeValues.Animation == BadgeAnimation.None || !_badgeValues.Visible)
        {
            UpdateAnimationTimer();
            return;
        }

        bool needsUpdate = false;

        switch (_badgeValues.Animation)
        {
            case BadgeAnimation.FadeInOut:
                if (_animationDirection)
                {
                    _animationOpacity += ANIMATION_STEP;
                    if (_animationOpacity >= FADE_MAX_OPACITY)
                    {
                        _animationOpacity = FADE_MAX_OPACITY;
                        _animationDirection = false;
                    }
                }
                else
                {
                    _animationOpacity -= ANIMATION_STEP;
                    if (_animationOpacity <= FADE_MIN_OPACITY)
                    {
                        _animationOpacity = FADE_MIN_OPACITY;
                        _animationDirection = true;
                    }
                }
                needsUpdate = true;
                break;

            case BadgeAnimation.Pulse:
                if (_animationDirection)
                {
                    _animationScale -= ANIMATION_STEP * 0.15f;
                    _animationOpacity -= ANIMATION_STEP * 0.4f;
                    if (_animationScale <= PULSE_MIN_SCALE)
                    {
                        _animationScale = PULSE_MIN_SCALE;
                        _animationOpacity = PULSE_MIN_OPACITY;
                        _animationDirection = false;
                    }
                }
                else
                {
                    _animationScale += ANIMATION_STEP * 0.15f;
                    _animationOpacity += ANIMATION_STEP * 0.4f;
                    if (_animationScale >= PULSE_MAX_SCALE)
                    {
                        _animationScale = PULSE_MAX_SCALE;
                        _animationOpacity = PULSE_MAX_OPACITY;
                        _animationDirection = true;
                    }
                }
                needsUpdate = true;
                break;
        }

        if (needsUpdate && _control != null && !_control.IsDisposed && _control.IsHandleCreated)
        {
            _control.Invalidate();
        }
    }
    #endregion
}
