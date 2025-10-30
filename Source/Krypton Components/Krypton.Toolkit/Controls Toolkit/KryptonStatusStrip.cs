﻿#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxBitmap(typeof(StatusStrip)), Description(@"A Krypton based status strip."), ToolboxItem(true)]
public class KryptonStatusStrip : StatusStrip
{
    #region Instance Fields
    private readonly PaletteBack _stateCommon;
    private readonly PaletteBack _stateDisabled;
    private readonly PaletteBack _stateNormal;
    #endregion

    #region Properties
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ToolStripProgressBar[] ProgressBars { get; set; }
    #endregion

    #region Constructor
    public KryptonStatusStrip()
    {
        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;

        // Create palette storage for per-control overrides, inheriting defaults from current palette ColorTable
        var inherit = new PaletteBackInheritStatusStrip();
        _stateCommon = new PaletteBack(inherit, OnNeedPaint);
        _stateDisabled = new PaletteBack(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteBack(_stateCommon, OnNeedPaint);
    }

    #endregion

    #region Overrides
    protected override void OnRendererChanged(EventArgs e)
    {
        if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
        {
            if (ProgressBars != null)
            {
                foreach (ToolStripProgressBar progressBar in ProgressBars)
                {
                    progressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
                }
            }
        }

        base.OnRendererChanged(e);
        Invalidate();
    }
    #endregion

    #region Visual States
    /// <summary>
    /// Gets access to the common status strip appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common status strip appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled status strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled status strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal status strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal status strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;
    #endregion

    #region Implementation
    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (!IsDisposed)
        {
            if (e != null && e.NeedLayout)
            {
                PerformLayout();
            }
            Invalidate();
        }
    }
    #endregion
}