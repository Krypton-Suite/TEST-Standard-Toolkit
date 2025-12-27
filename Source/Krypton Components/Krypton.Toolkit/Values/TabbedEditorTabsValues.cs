#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using System.Collections.ObjectModel;
using System.ComponentModel;

/// <summary>
/// Groups tabbed editor tab and selection properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TabbedEditorTabsValues : Storage
{
    #region Instance Fields

    private readonly KryptonTabbedEditor _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TabbedEditorTabsValues"/> class.
    /// </summary>
    /// <param name="owner">The owner control.</param>
    internal TabbedEditorTabsValues(KryptonTabbedEditor owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets access to the underlying KryptonTabControl.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTabControl TabControl => _owner.TabControl;

    /// <summary>
    /// Gets the collection of tab pages in this tabbed editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TabControl.TabPageCollection TabPages => _owner.TabPages;

    /// <summary>
    /// Gets the collection of editor controls (KryptonRichTextBox) in this tabbed editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReadOnlyCollection<KryptonRichTextBox> EditorControls => _owner.EditorControls;

    /// <summary>
    /// Gets or sets the index of the currently selected tab page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The index of the currently selected tab page.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _owner.SelectedIndex;
        set => _owner.SelectedIndex = value;
    }

    /// <summary>
    /// Gets or sets the currently selected tab page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The currently selected tab page.")]
    [DefaultValue(null)]
    public TabPage? SelectedTab
    {
        get => _owner.SelectedTab;
        set => _owner.SelectedTab = value;
    }

    /// <summary>
    /// Gets the currently selected editor control (KryptonRichTextBox).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRichTextBox? SelectedEditor => _owner.SelectedEditor;

    public override bool IsDefault => SelectedIndex == -1 && SelectedTab == null;

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => $"Tabs ({TabPages.Count})";
}
