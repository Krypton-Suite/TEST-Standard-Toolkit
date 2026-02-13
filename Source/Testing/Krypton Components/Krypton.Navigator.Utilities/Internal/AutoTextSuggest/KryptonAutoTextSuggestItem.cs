#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities.Internal;

/// <summary>
/// Represents a single suggestion item for text completion.
/// </summary>
public class KryptonAutoTextSuggestItem
{
    public string InsertText { get; set; }
    public string DisplayText { get; set; }
    public string? Description { get; set; }
    public object? Tag { get; set; }

    public KryptonAutoTextSuggestItem()
    {
        InsertText = string.Empty;
        DisplayText = string.Empty;
    }

    public KryptonAutoTextSuggestItem(string text)
    {
        InsertText = text;
        DisplayText = text;
    }

    public KryptonAutoTextSuggestItem(string insertText, string displayText)
    {
        InsertText = insertText;
        DisplayText = displayText;
    }

    public KryptonAutoTextSuggestItem(string insertText, string displayText, string description)
    {
        InsertText = insertText;
        DisplayText = displayText;
        Description = description;
    }

    public override string ToString() => DisplayText;
}
