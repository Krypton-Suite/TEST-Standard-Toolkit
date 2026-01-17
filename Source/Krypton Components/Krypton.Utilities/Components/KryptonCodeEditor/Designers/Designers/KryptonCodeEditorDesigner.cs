#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms.Design;

namespace Krypton.Utilities;

internal class KryptonCodeEditorDesigner : ControlDesigner
{
    #region Instance Fields
    private KryptonCodeEditor? _codeEditor;
    private IDesignerHost? _designerHost;
    private ISelectionService? _selectionService;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize(IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Cast to correct type
        _codeEditor = component as KryptonCodeEditor;

        // Get access to the design services
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
    }

    /// <summary>
    /// Gets the selection rules that indicate the movement capabilities of a component.
    /// </summary>
    public override SelectionRules SelectionRules
    {
        get
        {
            // Start with all edges being sizeable
            var rules = base.SelectionRules;

            // Code editor is always resizable
            return rules;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the code editor specific list
                new KryptonCodeEditorActionList(this)
            };

            return actionLists;
        }
    }

    #endregion
}

