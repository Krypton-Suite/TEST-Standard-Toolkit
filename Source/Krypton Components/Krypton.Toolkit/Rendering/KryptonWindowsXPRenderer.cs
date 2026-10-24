#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// ToolStrip/MenuStrip renderer for Windows XP visual styles.
/// </summary>
public sealed class KryptonWindowsXPRenderer : KryptonProfessionalRenderer
{
    private readonly Func<Bitmap?> _buttonSprite;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonWindowsXPRenderer"/> class.
    /// </summary>
    /// <param name="kct">Source of palette values.</param>
    /// <param name="buttonSprite">Factory for the XP push-button sprite sheet.</param>
    public KryptonWindowsXPRenderer([DisallowNull] KryptonColorTable kct, Func<Bitmap?> buttonSprite)
        : base(kct) =>
        _buttonSprite = buttonSprite ?? throw new ArgumentNullException(nameof(buttonSprite));

    /// <inheritdoc />
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        using (var brush = new SolidBrush(KCT.ToolStripGradientBegin))
        {
            e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.ToolStrip.Size));
        }
    }

    /// <inheritdoc />
    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripMenuItem { Selected: true })
        {
            var bounds = new Rectangle(Point.Empty, e.Item.Size);
            using (var brush = new SolidBrush(KCT.MenuItemSelected))
            using (var pen = new Pen(KCT.MenuItemBorder))
            {
                e.Graphics.FillRectangle(brush, bounds);
                if (bounds.Width > 1 && bounds.Height > 1)
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, bounds.Width - 1, bounds.Height - 1);
                }
            }

            return;
        }

        base.OnRenderMenuItemBackground(e);
    }

    /// <inheritdoc />
    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripButton button && e.Item.Width > 0 && e.Item.Height > 0)
        {
            var state = !button.Enabled
                ? 4
                : button.Pressed
                    ? 2
                    : button.Selected
                        ? 1
                        : button.Checked
                            ? 3
                            : 0;
            var sprite = _buttonSprite();
            if (sprite != null)
            {
                WindowsXPThemeResourceBridge.DrawSpriteSlice(e.Graphics, sprite,
                    new Rectangle(Point.Empty, e.Item.Size), state, 5);
                return;
            }
        }

        base.OnRenderButtonBackground(e);
    }
}
