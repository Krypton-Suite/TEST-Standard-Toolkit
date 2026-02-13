#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Runtime.InteropServices.Marshal;

namespace Krypton.Toolkit;

public static class WindowUtilities
{
    #region Implementation

    /// <summary>
    /// Enables acrylic blur effect on a window using SetWindowCompositionAttribute.
    /// </summary>
    /// <param name="owner">The window to apply acrylic effect to.</param>
    /// <param name="blurColor">The tint color for the acrylic effect (ARGB format).</param>
    /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
    public static void EnableAcrylic(IWin32Window owner, Color blurColor)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        if (owner.Handle == IntPtr.Zero)
        {
            return;
        }

        var accPolicy = new PI.Dwm.AccentPolicy(
            PI.Dwm.DWMACCENTSTATE.ACCENT_ENABLE_ACRYLICBLURBEHIND,
            PI.Dwm.AccentFlags.UserGradientColour,
            ToAbgr(blurColor),
            0);

        IntPtr accentPtr = IntPtr.Zero;
        try
        {
            var accentSize = SizeOf(accPolicy);
            accentPtr = AllocHGlobal(accentSize);
            StructureToPtr(accPolicy, accentPtr, false);
            var data = new PI.WindowCompositionAttribData(PI.Dwm.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                accentPtr, accentSize);

            PI.SetWindowCompositionAttribute(owner.Handle, ref data);
        }
        finally
        {
            if (accentPtr != IntPtr.Zero)
            {
                FreeHGlobal(accentPtr);
            }
        }
    }

    /// <summary>
    /// Disables acrylic blur effect on a window.
    /// </summary>
    /// <param name="owner">The window to disable acrylic effect on.</param>
    /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
    public static void DisableAcrylic(IWin32Window owner)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        if (owner.Handle == IntPtr.Zero)
        {
            return;
        }

        var accPolicy = new PI.Dwm.AccentPolicy(
            PI.Dwm.DWMACCENTSTATE.ACCENT_DISABLED,
            PI.Dwm.AccentFlags.UserGradientColour,
            0,
            0);

        IntPtr accentPtr = IntPtr.Zero;
        try
        {
            var accentSize = SizeOf(accPolicy);
            accentPtr = AllocHGlobal(accentSize);
            StructureToPtr(accPolicy, accentPtr, false);
            var data = new PI.WindowCompositionAttribData(PI.Dwm.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                accentPtr, accentSize);

            PI.SetWindowCompositionAttribute(owner.Handle, ref data);
        }
        finally
        {
            if (accentPtr != IntPtr.Zero)
            {
                FreeHGlobal(accentPtr);
            }
        }
    }

    /// <summary>
    /// Enables the Windows 11 Mica backdrop material on a window.
    /// Requires Windows 11 (build 22000+). No effect on earlier versions.
    /// </summary>
    /// <param name="owner">The window to apply Mica to.</param>
    /// <returns>True if Mica was applied successfully; otherwise false (e.g. on Windows 10 or older).</returns>
    /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
    public static bool EnableMica(IWin32Window owner)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        if (owner.Handle == IntPtr.Zero)
        {
            return false;
        }

        var value = (int)PI.Dwm.DWM_SYSTEMBACKDROP_TYPE.MainWindow;
        return PI.Dwm.DwmSetWindowAttribute(owner.Handle, PI.Dwm.DWMWINDOWATTRIBUTE.SystemBackdropType, ref value, sizeof(int)) == 0;
    }

    /// <summary>
    /// Disables the Windows 11 Mica backdrop on a window (restores default backdrop).
    /// </summary>
    /// <param name="owner">The window to remove Mica from.</param>
    /// <returns>True if the backdrop was reset successfully.</returns>
    /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
    public static bool DisableMica(IWin32Window owner)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        if (owner.Handle == IntPtr.Zero)
        {
            return false;
        }

        var value = (int)PI.Dwm.DWM_SYSTEMBACKDROP_TYPE.None;
        return PI.Dwm.DwmSetWindowAttribute(owner.Handle, PI.Dwm.DWMWINDOWATTRIBUTE.SystemBackdropType, ref value, sizeof(int)) == 0;
    }

    /// <summary>
    /// Converts a Color to ABGR format (Alpha, Blue, Green, Red) for Windows API.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>The color in ABGR format as an integer.</returns>
    private static int ToAbgr(Color color) => ((int)color.A << 24) | ((int)color.B << 16) | ((int)color.G << 8) | color.R;

    #endregion
}