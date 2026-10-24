#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Luna Olive Green (HomeStead) palette.</summary>
public sealed class PaletteWindowsXPLunaOlive : PaletteWindowsXPLunaBase
{
    /// <summary>Initialize a new instance of the <see cref="PaletteWindowsXPLunaOlive"/> class.</summary>
    public PaletteWindowsXPLunaOlive() => ThemeName = nameof(PaletteWindowsXPLunaOlive);

    /// <inheritdoc />
    protected override LunaPalette Luna => LunaColors.Olive;
}
