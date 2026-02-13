#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Krypton.Navigator.Utilities.Internal;

/// <summary>
/// Code folding margin control.
/// </summary>
internal class FoldingMargin : Control
{
    private readonly KryptonCodeEditor _editor;

    public FoldingMargin(KryptonCodeEditor editor)
    {
        _editor = editor;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = SystemColors.Control;
        Cursor = Cursors.Hand;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var rtb = _editor.RichTextBox;
        if (rtb == null || !_editor._enableCodeFolding)
            return;

        var g = e.Graphics;
        _editor.GetFoldingMarginPaletteColors(out var backColor, out var indicatorFillColor, out var indicatorBorderColor, out var indicatorTextColor);
        g.Clear(backColor);

        var firstLine = rtb.GetLineFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0)));
        var lastLine = rtb.GetLineFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, Height)));

        var lineHeight = rtb.Font.Height;
        var yOffset = -rtb.GetPositionFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0))).Y;

        foreach (var block in _editor._foldBlocks)
        {
            if (block.StartLine >= firstLine && block.StartLine <= lastLine)
            {
                var y = yOffset + (block.StartLine - firstLine) * lineHeight + lineHeight / 2;
                var rect = new Rectangle(Width / 2 - 5, (int)y - 5, 10, 10);

                using (var brush = new SolidBrush(indicatorFillColor))
                {
                    g.FillRectangle(brush, rect);
                }

                using (var pen = new Pen(indicatorBorderColor))
                {
                    g.DrawRectangle(pen, rect);

                    var centerX = rect.X + rect.Width / 2;
                    var centerY = rect.Y + rect.Height / 2;
                    g.DrawLine(pen, centerX - 3, centerY, centerX + 3, centerY);

                    if (block.IsFolded)
                    {
                        g.DrawLine(pen, centerX, centerY - 3, centerX, centerY + 3);

                        var indicatorY = yOffset + (block.StartLine + 1 - firstLine) * lineHeight;
                        if (block.StartLine + 1 <= lastLine && indicatorY >= 0)
                        {
                            using (var indicatorBrush = new SolidBrush(indicatorTextColor))
                            {
                                g.DrawString("...", rtb.Font, indicatorBrush, 2, indicatorY);
                            }
                        }
                    }
                }
            }
        }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        var rtb = _editor.RichTextBox;
        if (rtb == null)
            return;

        var firstLine = rtb.GetLineFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0)));
        var lineHeight = rtb.Font.Height;
        var yOffset = -rtb.GetPositionFromCharIndex(rtb.GetCharIndexFromPosition(new Point(0, 0))).Y;
        var clickedLine = firstLine + (int)((e.Y - yOffset) / lineHeight);

        var block = _editor._foldBlocks.FirstOrDefault(b => b.StartLine == clickedLine);
        if (block != null)
        {
            _editor.ToggleFoldBlock(block);
            Invalidate();
        }
    }
}
