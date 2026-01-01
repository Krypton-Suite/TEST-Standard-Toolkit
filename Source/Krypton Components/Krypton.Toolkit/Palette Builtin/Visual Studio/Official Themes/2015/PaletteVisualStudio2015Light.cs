#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Provides the Visual Studio 2015 Light theme.</summary>
/// <seealso cref="PaletteVisualStudioBase" />
public class PaletteVisualStudio2015Light : PaletteVisualStudioBase
{
    #region Static Fields

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Images

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #endregion

    #region Identity

    static PaletteVisualStudio2015Light()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2010Blue);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.Gallery2010);
        _radioButtonArray =
        [
            Office2010RadioButtonImageResources.RadioButton2010BlueD,
            Office2010RadioButtonImageResources.RadioButton2010BlueN,
            Office2010RadioButtonImageResources.RadioButton2010BlueT,
            Office2010RadioButtonImageResources.RadioButton2010BlueP,
            Office2010RadioButtonImageResources.RadioButton2010BlueDC,
            Office2010RadioButtonImageResources.RadioButton2010BlueNC,
            Office2010RadioButtonImageResources.RadioButton2010BlueTC,
            Office2010RadioButtonImageResources.RadioButton2010BluePC
        ];
    }

    /// <summary>Initializes a new instance of the <see cref="PaletteVisualStudio2015Light" /> class.</summary>
    public PaletteVisualStudio2015Light()
        : base(
        new PaletteVisualStudio2015Light_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteVisualStudio2015Light);
    }

    /// <summary>Gets the renderer to use for this palette.</summary>
    /// <returns>Renderer to use for drawing palette settings.</returns>
    public override IRenderer GetRenderer() => KryptonManager.RenderVisualStudio2015;

    #endregion
}

