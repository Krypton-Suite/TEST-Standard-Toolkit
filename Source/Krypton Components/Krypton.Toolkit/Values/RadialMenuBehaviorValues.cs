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
/// Storage for radial menu behavior properties.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class RadialMenuBehaviorValues : Storage
{
    #region Instance Fields
    private readonly KryptonRadialMenu _owner;
    private bool _allowDrag;
    private bool _allowFloat;
    private Form? _mdiParent;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RadialMenuBehaviorValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    internal RadialMenuBehaviorValues(KryptonRadialMenu owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _allowDrag = false;
        _allowFloat = false;
        _mdiParent = null;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets a value indicating whether the menu can be dragged.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu can be dragged within its container.")]
    [DefaultValue(false)]
    public bool AllowDrag
    {
        get => _allowDrag;
        set => _allowDrag = value;
    }

    private bool ShouldSerializeAllowDrag() => _allowDrag;

    private void ResetAllowDrag() => AllowDrag = false;

    /// <summary>
    /// Gets or sets a value indicating whether the menu can be floated into its own window.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the menu can be floated into its own window.")]
    [DefaultValue(false)]
    public bool AllowFloat
    {
        get => _allowFloat;
        set => _allowFloat = value;
    }

    private bool ShouldSerializeAllowFloat() => _allowFloat;

    private void ResetAllowFloat() => AllowFloat = false;

    /// <summary>
    /// Gets or sets the MDI parent form for floating windows.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"MDI parent form for floating windows. If set, floating windows will be MDI children.")]
    [DefaultValue(null)]
    public Form? MdiParent
    {
        get => _mdiParent;
        set => _mdiParent = value;
    }

    private bool ShouldSerializeMdiParent() => _mdiParent != null;

    private void ResetMdiParent() => MdiParent = null;

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetAllowDrag();
        ResetAllowFloat();
        ResetMdiParent();
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeAllowDrag()
                                    && !ShouldSerializeAllowFloat()
                                    && !ShouldSerializeMdiParent();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? @"(Default)" : @"(Modified)";
    #endregion
}

