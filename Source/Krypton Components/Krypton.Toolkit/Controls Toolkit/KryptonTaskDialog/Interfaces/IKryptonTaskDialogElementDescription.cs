#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementDescription
{
    /// <summary>
    /// Description element to display.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Show or hide the description.
    /// </summary>
    public bool ShowDescription { get; set; }
}
