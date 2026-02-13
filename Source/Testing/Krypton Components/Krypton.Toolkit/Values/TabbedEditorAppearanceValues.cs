#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using System.ComponentModel;

/// <summary>
/// Groups tabbed editor appearance properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TabbedEditorAppearanceValues : Storage
{
    #region Instance Fields

    private readonly KryptonTabbedEditor _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TabbedEditorAppearanceValues"/> class.
    /// </summary>
    /// <param name="owner">The owner control.</param>
    internal TabbedEditorAppearanceValues(KryptonTabbedEditor owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets and sets the tabbed editor background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tabbed editor background style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle TabbedEditorBackStyle
    {
        get => _owner.TabbedEditorBackStyle;
        set => _owner.TabbedEditorBackStyle = value;
    }

    /// <summary>
    /// Gets or sets the tab style for the tab control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab style.")]
    [DefaultValue(TabStyle.StandardProfile)]
    public TabStyle TabStyle
    {
        get => _owner.TabStyle;
        set => _owner.TabStyle = value;
    }

    /// <summary>
    /// Gets or sets the tab border style for the tab control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab border style.")]
    [DefaultValue(TabBorderStyle.SquareEqualSmall)]
    public TabBorderStyle TabBorderStyle
    {
        get => _owner.TabBorderStyle;
        set => _owner.TabBorderStyle = value;
    }

    /// <summary>
    /// Gets or sets the alignment of the tabs on the tab control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The alignment of the tabs on the tab control.")]
    [DefaultValue(TabAlignment.Top)]
    public TabAlignment Alignment
    {
        get => _owner.Alignment;
        set => _owner.Alignment = value;
    }

    /// <summary>
    /// Gets or sets the appearance of the control's tabs.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The appearance of the control's tabs.")]
    [DefaultValue(TabAppearance.Normal)]
    public TabAppearance Appearance
    {
        get => _owner.Appearance;
        set => _owner.Appearance = value;
    }

    /// <summary>
    /// Gets or sets the way that the control's tabs are sized.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The way that the control's tabs are sized.")]
    [DefaultValue(TabSizeMode.Normal)]
    public TabSizeMode SizeMode
    {
        get => _owner.SizeMode;
        set => _owner.SizeMode = value;
    }

    /// <summary>
    /// Gets or sets whether the tab control shows a tooltip for each tab.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the tab control shows a tooltip for each tab.")]
    [DefaultValue(false)]
    public bool ShowToolTips
    {
        get => _owner.ShowToolTips;
        set => _owner.ShowToolTips = value;
    }

    public override bool IsDefault => TabbedEditorBackStyle == PaletteBackStyle.PanelClient &&
                                      TabStyle == TabStyle.StandardProfile &&
                                      TabBorderStyle == TabBorderStyle.SquareEqualSmall &&
                                      Alignment == TabAlignment.Top &&
                                      Appearance == TabAppearance.Normal &&
                                      SizeMode == TabSizeMode.Normal &&
                                      !ShowToolTips;

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => "Tabbed Editor Appearance";
}
