#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared Windows XP visual-style renderer: square corners, hard one-pixel bevels, and vertical linear fills.
/// </summary>
public class RenderWindowsXP : RenderStandard
{
    private readonly Func<Bitmap?> _buttonSprite;

    private static readonly Font XPDefaultFont = new("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point);

    /// <summary>Gets the standard Windows XP user-interface font.</summary>
    /// <remarks>XP themes use Tahoma independently of the host operating system's default UI font.</remarks>
    public static Font DefaultFont => XPDefaultFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="RenderWindowsXP"/> class.
    /// </summary>
    /// <param name="buttonSprite">Factory that returns the XP push-button sprite sheet for ToolStrip buttons.</param>
    protected RenderWindowsXP(Func<Bitmap?> buttonSprite) =>
        _buttonSprite = buttonSprite ?? throw new ArgumentNullException(nameof(buttonSprite));

    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(colorPalette));
        }

        return new KryptonWindowsXPRenderer(colorPalette.ColorTable, _buttonSprite);
    }

    /// <inheritdoc />
    public override GraphicsPath GetBackPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (rect.Width <= 0 || rect.Height <= 0 ||
            palette.GetBorderDraw(state) != InheritBool.True ||
            !CommonHelper.HasABorder(palette.GetBorderDrawBorders(state)) ||
            palette.GetBorderWidth(state) <= 0)
        {
            var path = new GraphicsPath();
            path.AddRectangle(rect);
            return path;
        }

        return base.GetBackPath(context, rect, palette, orientation, state);
    }

    /// <inheritdoc />
    public override IDisposable? DrawBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        VisualOrientation orientation,
        PaletteState state,
        IDisposable? memento)
    {
        if (context == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (path == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        if (palette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0 ||
            CommonHelper.IsOverrideState(state) ||
            palette.GetBackColorStyle(state) != PaletteColorStyle.Linear)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        var top = palette.GetBackColor1(state);
        var bottom = palette.GetBackColor2(state);
        if (top == Color.Empty || bottom == Color.Empty)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        var graphics = context.Graphics;
        var oldSmoothing = graphics.SmoothingMode;
        var oldPixel = graphics.PixelOffsetMode;
        var oldClip = graphics.Clip;

        graphics.SmoothingMode = SmoothingMode.None;
        graphics.PixelOffsetMode = PixelOffsetMode.None;

        try
        {
            graphics.SetClip(path, CombineMode.Intersect);

            using (var brush = new LinearGradientBrush(rect, top, bottom, 90f))
            {
                graphics.FillRectangle(brush, rect);
            }

            if (rect.Width > 1 && rect.Height > 1)
            {
                using (var lightPen = new Pen(WindowsXPRenderHelper.Lighten(top, 0.28f)))
                using (var darkPen = new Pen(WindowsXPRenderHelper.Darken(bottom, 0.30f)))
                {
                    var right = rect.Right - 1;
                    var bottomEdge = rect.Bottom - 1;
                    graphics.DrawLine(lightPen, rect.Left, rect.Top, right, rect.Top);
                    graphics.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, bottomEdge);
                    graphics.DrawLine(darkPen, right, rect.Top, right, bottomEdge);
                    graphics.DrawLine(darkPen, rect.Left, bottomEdge, right, bottomEdge);
                }
            }
        }
        finally
        {
            graphics.Clip = oldClip;
            graphics.SmoothingMode = oldSmoothing;
            graphics.PixelOffsetMode = oldPixel;
        }

        return memento;
    }

    /// <inheritdoc />
    public override void DrawBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (palette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0 ||
            palette.GetBorderWidth(state) <= 0 ||
            palette.GetBorderDraw(state) != InheritBool.True ||
            !CommonHelper.HasABorder(palette.GetBorderDrawBorders(state)))
        {
            return;
        }

        base.DrawBorder(context, rect, palette, orientation, state);
    }
}

/// <summary>Windows XP Luna renderer.</summary>
public sealed class RenderWindowsXPLuna : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPLuna"/> class.</summary>
    public RenderWindowsXPLuna()
        : base(() => WindowsXPThemeResourceBridge.LunaButton)
    {
    }
}

/// <summary>Windows XP Royale renderer.</summary>
public sealed class RenderWindowsXPRoyale : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPRoyale"/> class.</summary>
    public RenderWindowsXPRoyale()
        : base(() => WindowsXPThemeResourceBridge.RoyaleButton)
    {
    }
}

/// <summary>Windows XP Royale Noir renderer.</summary>
public sealed class RenderWindowsXPRoyaleNoir : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPRoyaleNoir"/> class.</summary>
    public RenderWindowsXPRoyaleNoir()
        : base(() => WindowsXPThemeResourceBridge.RoyaleNoirButton)
    {
    }
}

/// <summary>Windows XP Zune renderer.</summary>
public sealed class RenderWindowsXPZune : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPZune"/> class.</summary>
    public RenderWindowsXPZune()
        : base(() => WindowsXPThemeResourceBridge.ZuneButton)
    {
    }
}

/// <summary>Windows XP Embedded renderer.</summary>
public sealed class RenderWindowsXPEmbedded : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPEmbedded"/> class.</summary>
    public RenderWindowsXPEmbedded()
        : base(() => WindowsXPThemeResourceBridge.EmbeddedButton)
    {
    }
}
