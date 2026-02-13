#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a single suggestion item for text completion.
/// </summary>
public class KryptonTextSuggestionItem
{
    /// <summary>
    /// Gets or sets the text to insert when this suggestion is selected.
    /// </summary>
    public string InsertText { get; set; }

    /// <summary>
    /// Gets or sets the display text shown in the suggestion list.
    /// </summary>
    public string DisplayText { get; set; }

    /// <summary>
    /// Gets or sets optional description text for tooltip or additional info.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets user-defined data associated with this item.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// Initializes a new instance of the KryptonTextSuggestionItem class.
    /// </summary>
    public KryptonTextSuggestionItem()
    {
        InsertText = string.Empty;
        DisplayText = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the KryptonTextSuggestionItem class.
    /// </summary>
    /// <param name="text">The text for both insertion and display.</param>
    public KryptonTextSuggestionItem(string text)
    {
        InsertText = text;
        DisplayText = text;
    }

    /// <summary>
    /// Initializes a new instance of the KryptonTextSuggestionItem class.
    /// </summary>
    /// <param name="insertText">The text to insert.</param>
    /// <param name="displayText">The text to display.</param>
    public KryptonTextSuggestionItem(string insertText, string displayText)
    {
        InsertText = insertText;
        DisplayText = displayText;
    }

    /// <summary>
    /// Initializes a new instance of the KryptonTextSuggestionItem class.
    /// </summary>
    /// <param name="insertText">The text to insert.</param>
    /// <param name="displayText">The text to display.</param>
    /// <param name="description">The description text.</param>
    public KryptonTextSuggestionItem(string insertText, string displayText, string description)
    {
        InsertText = insertText;
        DisplayText = displayText;
        Description = description;
    }

    /// <summary>
    /// Returns a string representation of this item.
    /// </summary>
    /// <returns>The display text.</returns>
    public override string ToString() => DisplayText;
}

