#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing;

namespace Krypton.Utilities;

/// <summary>
/// Renders a QR code module matrix to a bitmap.
/// </summary>
internal static class QRCodeBitmapRenderer
{
    /// <summary>
    /// Renders the QR code matrix to a bitmap.
    /// </summary>
    /// <param name="matrix">The module matrix (true = dark).</param>
    /// <param name="moduleSize">Pixels per module.</param>
    /// <param name="darkColor">Color for dark modules.</param>
    /// <param name="lightColor">Color for light modules.</param>
    /// <param name="showBorder">Whether to add a quiet zone (4 modules).</param>
    /// <returns>A bitmap of the QR code.</returns>
    public static Bitmap Render(bool[,] matrix, int moduleSize, Color darkColor, Color lightColor, bool showBorder)
    {
        int border = showBorder ? moduleSize * 4 : 0;
        int size = matrix.GetLength(0);
        int pixelSize = size * moduleSize + border * 2;

        var bmp = new Bitmap(pixelSize, pixelSize);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(lightColor);
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (matrix[row, col])
                    {
                        using var brush = new SolidBrush(darkColor);
                        g.FillRectangle(brush, border + col * moduleSize, border + row * moduleSize, moduleSize, moduleSize);
                    }
                }
            }
        }
        return bmp;
    }
}
