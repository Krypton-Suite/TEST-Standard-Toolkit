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
/// Provides data for the DirectoryExpanded event.
/// </summary>
public class DirectoryExpandedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the DirectoryExpandedEventArgs class.
    /// </summary>
    /// <param name="path">The path of the directory that was expanded.</param>
    public DirectoryExpandedEventArgs(string path)
    {
        Path = path;
    }

    /// <summary>
    /// Gets the path of the directory that was expanded.
    /// </summary>
    public string Path { get; }
}

