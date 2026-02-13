#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides rendering for Visual Studio 2012 themes.
/// </summary>
public class RenderVisualStudio2012 : RenderVisualStudio
{
    #region Constructor
    static RenderVisualStudio2012()
    {
    }
    #endregion

    #region IRenderer Overrides        
    /// <summary>
    /// Renders the tool strip.
    /// </summary>
    /// <param name="colourPalette">The colour palette.</param>
    /// <returns></returns>
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colourPalette)
    {
        Debug.Assert(colourPalette != null);

        // Validate passed parameter
        if (colourPalette == null)
        {
            throw new ArgumentNullException(nameof(colourPalette));
        }

        var renderer = new KryptonVisualStudio2012Renderer(colourPalette.ColorTable)
        {
            RoundedEdges = colourPalette.ColorTable.UseRoundedEdges != InheritBool.False
        };

        return renderer;
    }
    #endregion
}

