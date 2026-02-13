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
/// Provides accessibility information for KryptonLinkWrapLabel control.
/// Since KryptonLinkWrapLabel inherits directly from LinkLabel, we delegate to the base LinkLabel's accessibility object.
/// </summary>
internal class KryptonLinkWrapLabelAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonLinkWrapLabel _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkWrapLabelAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonLinkWrapLabel control that owns this accessible object.</param>
    public KryptonLinkWrapLabelAccessibleObject(KryptonLinkWrapLabel owner)
        : base(owner)
    {
        _owner = owner;
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets the accessible name of the control.
    /// </summary>
    public override string? Name
    {
        get
        {
            // Since KryptonLinkWrapLabel inherits from LinkLabel, delegate to base LinkLabel's accessibility
            // Cast to LinkLabel to access its accessibility object
            var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
            if (linkLabelAccessible?.Name != null)
            {
                return linkLabelAccessible.Name;
            }

            // Fall back to base implementation
            return base.Name ?? _owner.Name;
        }
    }

    /// <summary>
    /// Gets the accessible description of the control.
    /// </summary>
    public override string? Description
    {
        get
        {
            // Delegate to base LinkLabel's accessibility description
            var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
            if (linkLabelAccessible?.Description != null)
            {
                return linkLabelAccessible.Description;
            }

            // Fall back to base implementation
            return base.Description;
        }
    }

    /// <summary>
    /// Gets the accessible role of the control.
    /// </summary>
    public override AccessibleRole Role
    {
        get
        {
            // Delegate to base LinkLabel's accessibility role
            var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
            if (linkLabelAccessible != null)
            {
                var role = linkLabelAccessible.Role;
                // Ensure we have a valid role (legacy TFMs might return Default)
                if (role != AccessibleRole.Default && role != AccessibleRole.None)
                {
                    return role;
                }
            }

            // Fall back to Link role for LinkLabel controls
            return AccessibleRole.Link;
        }
    }

    /// <summary>
    /// Gets the state of this accessible object.
    /// </summary>
    public override AccessibleStates State
    {
        get
        {
            // Delegate to base LinkLabel's accessibility state
            var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
            if (linkLabelAccessible != null)
            {
                return linkLabelAccessible.State;
            }

            // Fall back to base implementation
            return base.State;
        }
    }

    /// <summary>
    /// Gets the value of the accessible object.
    /// </summary>
    public override string? Value
    {
        get
        {
            // Delegate to base LinkLabel's accessibility value
            var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
            if (linkLabelAccessible?.Value != null)
            {
                return linkLabelAccessible.Value;
            }

            // Fall back to control's text
            return _owner.Text;
        }
    }

    /// <summary>
    /// Performs the default action associated with this accessible object.
    /// </summary>
    public override void DoDefaultAction()
    {
        // Delegate to base LinkLabel's default action
        var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
        if (linkLabelAccessible != null)
        {
            linkLabelAccessible.DoDefaultAction();
        }
        else
        {
            base.DoDefaultAction();
        }
    }

    /// <summary>
    /// Retrieves the child accessible object corresponding to the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the child accessible object.</param>
    /// <returns>An AccessibleObject that represents the child accessible object corresponding to the specified index.</returns>
    public override AccessibleObject? GetChild(int index)
    {
        // Delegate to base LinkLabel's children (which includes link accessible objects)
        var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
        if (linkLabelAccessible != null)
        {
            return linkLabelAccessible.GetChild(index);
        }

        return base.GetChild(index);
    }

    /// <summary>
    /// Retrieves the number of children belonging to an accessible object.
    /// </summary>
    /// <returns>The number of children belonging to an accessible object.</returns>
    public override int GetChildCount()
    {
        // Delegate to base LinkLabel's child count (includes individual links)
        var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
        if (linkLabelAccessible != null)
        {
            return linkLabelAccessible.GetChildCount();
        }

        return base.GetChildCount();
    }

    /// <summary>
    /// Navigates to another accessible object.
    /// </summary>
    /// <param name="direction">One of the NavigateDirection values.</param>
    /// <returns>An AccessibleObject representing one of the NavigateDirection values.</returns>
    public override AccessibleObject? Navigate(AccessibleNavigation direction)
    {
        // Delegate to base LinkLabel's navigation
        var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
        if (linkLabelAccessible != null)
        {
            return linkLabelAccessible.Navigate(direction);
        }

        return base.Navigate(direction);
    }

    /// <summary>
    /// Selects this accessible object.
    /// </summary>
    /// <param name="flags">One of the AccessibleSelection values.</param>
    public override void Select(AccessibleSelection flags)
    {
        // Delegate to base LinkLabel's selection
        var linkLabelAccessible = ((LinkLabel)_owner).AccessibilityObject;
        if (linkLabelAccessible != null)
        {
            linkLabelAccessible.Select(flags);
        }
        else
        {
            base.Select(flags);
        }
    }
    #endregion
}
