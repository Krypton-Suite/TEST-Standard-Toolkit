#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a base for Windows 10 Acrylic palettes.
/// </summary>
/// <seealso cref="PaletteMicrosoft365Base" />
public abstract class PaletteWindows10AcrylicBase : PaletteMicrosoft365Base
{
    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteWindows10AcrylicBase"/> class.
    /// </summary>
    /// <param name="scheme">The color scheme.</param>
    /// <param name="checkBoxList">The check box list.</param>
    /// <param name="galleryButtonList">The gallery button list.</param>
    /// <param name="radioButtonArray">The radio button array.</param>
    protected PaletteWindows10AcrylicBase(
        [DisallowNull] KryptonColorSchemeBase scheme,
        [DisallowNull] ImageList checkBoxList,
        [DisallowNull] ImageList galleryButtonList,
        [DisallowNull] Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
    }

    #endregion Constructor

    #region Renderer

    /// <summary>
    /// Gets the renderer to use for this palette.
    /// </summary>
    /// <returns>
    /// Renderer to use for drawing palette settings.
    /// </returns>
    public override IRenderer GetRenderer() => KryptonManager.RenderMicrosoft365;

    #endregion Renderer
}

