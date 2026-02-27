#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public static class WindowUtilities
{
    #region Implementation

    /// <summary>
    /// Enables the Acrylic (frosted glass) effect on the given window. Requires Windows 10 1803 (build 17134) or later.
    /// </summary>
    /// <param name="owner">The window to apply Acrylic to.</param>
    /// <param name="tintColor">Tint color for the acrylic effect.</param>
    public static void EnableAcrylic(IWin32Window owner, Color tintColor)
    {
        ArgumentNullException.ThrowIfNull(owner);

        if (!IsAcrylicSupported())
        {
            return;
        }

        PI.Dwm.Windows10EnableAcrylic(owner.Handle, true, tintColor);
    }

    /// <summary>
    /// Disables the Acrylic effect on the given window.
    /// </summary>
    /// <param name="owner">The window to remove Acrylic from.</param>
    public static void DisableAcrylic(IWin32Window owner)
    {
        ArgumentNullException.ThrowIfNull(owner);

        PI.Dwm.Windows10EnableAcrylic(owner.Handle, false, Color.Empty);
    }

    /// <summary>
    /// Gets whether the Acrylic effect is supported on the current OS (Windows 10 1803 / build 17134 or later).
    /// </summary>
    public static bool IsAcrylicSupported()
    {
        var info = OSUtilities.OsVersionInfo;
        return info.MajorVersion >= 10 && info.BuildNumber >= 17134;
    }

    #endregion
}