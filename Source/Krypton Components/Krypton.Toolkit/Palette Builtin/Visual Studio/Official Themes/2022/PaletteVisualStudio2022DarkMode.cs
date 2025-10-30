﻿#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public abstract class PaletteVisualStudio2022DarkMode : PaletteVisualStudioBase
{
    [System.Obsolete("Color[] constructor is deprecated and will be removed in V110. Use KryptonColorSchemeBase overload.", false)]
    protected PaletteVisualStudio2022DarkMode(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image?[] radioButtonArray, Color[] trackBarColors)
        : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
    {
        ThemeName = nameof(PaletteVisualStudio2022DarkMode);
    }
}