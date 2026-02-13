#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Krypton.Navigator.Utilities.Internal;

/// <summary>
/// Provides data for suggestion-related events.
/// </summary>
public class KryptonAutoTextSuggestEventArgs : EventArgs
{
    public KryptonAutoTextSuggestItem Item { get; }
    public Control Control { get; }
    public bool Handled { get; set; }

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
    public string FilterText { get; }
    public List<KryptonAutoTextSuggestItem> Suggestions { get; set; }

    public KryptonAutoTextSuggestFilterEventArgs(string filterText, List<KryptonAutoTextSuggestItem> suggestions)
    {
        FilterText = filterText;
        Suggestions = suggestions;
    }
}
