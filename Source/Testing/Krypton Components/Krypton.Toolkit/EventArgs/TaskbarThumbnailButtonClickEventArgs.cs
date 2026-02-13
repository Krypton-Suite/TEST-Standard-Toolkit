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
/// Provides data for the TaskbarThumbnailButtonClick event.
/// </summary>
public class TaskbarThumbnailButtonClickEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarThumbnailButtonClickEventArgs class.
    /// </summary>
    /// <param name="buttonId">ID of the clicked button.</param>
    public TaskbarThumbnailButtonClickEventArgs(uint buttonId)
    {
        ButtonId = buttonId;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the ID of the clicked button.
    /// </summary>
    public uint ButtonId { get; }
    #endregion
}
