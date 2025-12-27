#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specifies the root display mode for the file system tree view.
/// </summary>
public enum FileSystemRootMode
{
    /// <summary>
    /// Displays Desktop as root with special folders (Computer, Network, Recycle Bin, etc.) and drives, similar to Windows Explorer.
    /// </summary>
    Desktop,

    /// <summary>
    /// Displays Computer as root with all drives.
    /// </summary>
    Computer,

    /// <summary>
    /// Displays all drives directly as root nodes.
    /// </summary>
    Drives,

    /// <summary>
    /// Uses the custom RootPath property to determine the root directory.
    /// </summary>
    CustomPath
}

