#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using Krypton.Toolkit;

namespace Krypton.Utilities;

/// <summary>
/// A control that displays a QR code. Generates QR codes natively without external dependencies.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultProperty(nameof(Content))]
[DefaultEvent(nameof(ContentChanged))]
[DesignerCategory(@"code")]
[Description(@"Displays a QR code generated from the specified content. Uses native generation without external packages.")]
public class KryptonQRCode : KryptonPanel
{
    #region Instance Fields

    private string _content = string.Empty;
    private bool[,]? _moduleMatrix;
    private QRErrorCorrectionLevel _errorCorrectionLevel = QRErrorCorrectionLevel.M;
    private int _moduleSize = 4;
    private Color _darkColor = Color.Black;
    private Color _lightColor = Color.White;
    private bool _showBorder = true;

    #endregion

    #region Events

    /// <summary>Occurs when the content changes and the QR code is regenerated.</summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the content changes.")]
    public event EventHandler? ContentChanged;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the content to encode in the QR code.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"The text or data to encode in the QR code (UTF-8).")]
    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value ?? string.Empty;
                Regenerate();
                ContentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the error correction level.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(QRErrorCorrectionLevel.M)]
    [Description(@"The error correction level. Higher levels allow more data recovery but reduce capacity.")]
    public QRErrorCorrectionLevel ErrorCorrectionLevel
    {
        get => _errorCorrectionLevel;
        set
        {
            if (_errorCorrectionLevel != value)
            {
                _errorCorrectionLevel = value;
                Regenerate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of each QR module in pixels.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(4)]
    [Description(@"The size of each QR code module (pixel) in the rendered image.")]
    public int ModuleSize
    {
        get => _moduleSize;
        set
        {
            if (_moduleSize != value && value >= 1 && value <= 20)
            {
                _moduleSize = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the color for dark (filled) modules.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The color for dark modules in the QR code.")]
    public Color DarkColor
    {
        get => _darkColor;
        set
        {
            if (_darkColor != value)
            {
                _darkColor = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the color for light (empty) modules.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The color for light modules in the QR code.")]
    public Color LightColor
    {
        get => _lightColor;
        set
        {
            if (_lightColor != value)
            {
                _lightColor = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether to show a border (quiet zone) around the QR code.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(true)]
    [Description(@"Whether to show a quiet zone (white border) around the QR code.")]
    public bool ShowBorder
    {
        get => _showBorder;
        set
        {
            if (_showBorder != value)
            {
                _showBorder = value;
                Invalidate();
            }
        }
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonQRCode" /> class.</summary>
    public KryptonQRCode()
    {
        Size = new Size(120, 120);
        MinimumSize = new Size(50, 50);
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Generates a bitmap of the current QR code.
    /// </summary>
    /// <returns>A bitmap of the QR code, or null if no content.</returns>
    public Bitmap? GetBitmap()
    {
        if (_moduleMatrix == null) return null;
        return QRCodeBitmapRenderer.Render(_moduleMatrix, _moduleSize, _darkColor, _lightColor, _showBorder);
    }

    /// <summary>
    /// Generates a QR code bitmap for the given content.
    /// </summary>
    /// <param name="content">The content to encode.</param>
    /// <param name="moduleSize">The module size in pixels.</param>
    /// <param name="eccLevel">The error correction level.</param>
    /// <param name="darkColor">Color for dark modules.</param>
    /// <param name="lightColor">Color for light modules.</param>
    /// <returns>A bitmap of the QR code.</returns>
    public static Bitmap GenerateBitmap(string content, int moduleSize = 4, QRErrorCorrectionLevel eccLevel = QRErrorCorrectionLevel.M, Color? darkColor = null, Color? lightColor = null)
    {
        bool[,] matrix = QRCodeGeneratorCore.Generate(content, eccLevel);
        return QRCodeBitmapRenderer.Render(matrix, moduleSize, darkColor ?? Color.Black, lightColor ?? Color.White, true);
    }

    /// <summary>
    /// Saves the current QR code to a file.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <param name="format">The image format (e.g. PNG).</param>
    public void SaveToFile(string path, System.Drawing.Imaging.ImageFormat format)
    {
        using Bitmap? bmp = GetBitmap();
        if (bmp != null)
        {
            bmp.Save(path, format);
        }
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (_moduleMatrix == null) return;

        Graphics g = e.Graphics;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = PixelOffsetMode.Half;

        int border = _showBorder ? _moduleSize * 4 : 0;
        int matrixSize = _moduleMatrix.GetLength(0);
        int pixelSize = matrixSize * _moduleSize;

        int x = (Width - pixelSize - border * 2) / 2 + border;
        int y = (Height - pixelSize - border * 2) / 2 + border;

        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                using var brush = new SolidBrush(_moduleMatrix[row, col] ? _darkColor : _lightColor);
                g.FillRectangle(brush, x + col * _moduleSize, y + row * _moduleSize, _moduleSize, _moduleSize);
            }
        }
    }

    #endregion

    #region Implementation

    private void Regenerate()
    {
        if (string.IsNullOrEmpty(_content))
        {
            _moduleMatrix = null;
        }
        else
        {
            try
            {
                _moduleMatrix = QRCodeGeneratorCore.Generate(_content, _errorCorrectionLevel);
            }
            catch (ArgumentException)
            {
                _moduleMatrix = null;
            }
        }
        Invalidate();
    }

    #endregion
}
