#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides data for suggestion-related events.
/// </summary>
public class KryptonAutoTextSuggestEventArgs : EventArgs
{
    /// <summary>
    /// Gets the suggestion item that triggered the event.
    /// </summary>
    public KryptonAutoTextSuggestItem Item { get; }

    /// <summary>
    /// Gets the control that triggered the event.
    /// </summary>
    public Control Control { get; }

    /// <summary>
    /// Gets or sets whether the event has been handled.
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    /// Initializes a new instance of the KryptonAutoTextSuggestEventArgs class.
    /// </summary>
    /// <param name="item">The suggestion item.</param>
    /// <param name="control">The control.</param>
    public KryptonAutoTextSuggestEventArgs(KryptonAutoTextSuggestItem item, Control control)
    {
        Item = item;
        Control = control;
    }
}

/// <summary>
/// Provides data for suggestion filtering events.
/// </summary>
public class KryptonAutoTextSuggestFilterEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current text input being used for filtering.
    /// </summary>
    public string FilterText { get; }

    /// <summary>
    /// Gets or sets the list of suggestions to display.
    /// </summary>
    public List<KryptonAutoTextSuggestItem> Suggestions { get; set; }

    /// <summary>
    /// Initializes a new instance of the KryptonAutoTextSuggestFilterEventArgs class.
    /// </summary>
    /// <param name="filterText">The filter text.</param>
    /// <param name="suggestions">The initial suggestions list.</param>
    public KryptonAutoTextSuggestFilterEventArgs(string filterText, List<KryptonAutoTextSuggestItem> suggestions)
    {
        FilterText = filterText;
        Suggestions = suggestions;
    }
}

