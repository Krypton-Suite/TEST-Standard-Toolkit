#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Specifies the size of a navigation item in the Backstage View.
/// </summary>
public enum BackstageItemSize
{
    /// <summary>
    /// Small item size (default, compact display).
    /// </summary>
    Small = 0,

    /// <summary>
    /// Large item size (Office-like, more prominent display with larger image area).
    /// </summary>
    Large = 1
}

/// <summary>
/// Specifies the overlay coverage mode for the Backstage View.
/// </summary>
public enum BackstageOverlayMode
{
    /// <summary>
    /// Overlay covers the entire form client area (default).
    /// </summary>
    FullClient = 0,

    /// <summary>
    /// Overlay covers only the area below the ribbon.
    /// </summary>
    BelowRibbon = 1
}

