#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A Krypton based menu strip.
/// </summary>
[ToolboxBitmap(typeof(MenuStrip)), Description(@"A Krypton based menu strip."), ToolboxItem(true)]
public class KryptonMenuStrip : MenuStrip
{
    #region Instance Fields
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteBackInheritMenuStrip? _inherit;
    private readonly PaletteBack _stateCommon;
    private readonly PaletteBack _stateDisabled;
    private readonly PaletteBack _stateNormal;
    #endregion

    #region Constructor
    /// <summary>
    /// Initialize a new instance of the KryptonMenuStrip class.
    /// </summary>
    public KryptonMenuStrip()
    {
        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create palette storage for per-control overrides, inheriting defaults from current palette ColorTable
        _inherit = new PaletteBackInheritMenuStrip(_palette);
        _stateCommon = new PaletteBack(_inherit, OnNeedPaint);
        _stateDisabled = new PaletteBack(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteBack(_stateCommon, OnNeedPaint);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Raises the RendererChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnRendererChanged(EventArgs e)
    {
        base.OnRendererChanged(e);
        Invalidate();
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateInherit();
                        UpdateAppearance();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }

                UpdateInherit();
                UpdateAppearance();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }
    #endregion

    #region Visual States
    /// <summary>
    /// Gets access to the common menu strip appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common menu strip appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled menu strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled menu strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal menu strip appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal menu strip appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;
    #endregion

    #region Implementation
    private void UpdateInherit()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_inherit != null)
        {
            _inherit.SetPalette(currentPalette);
        }
        else
        {
            _inherit = new PaletteBackInheritMenuStrip(currentPalette);
        }
    }

    private void UpdateAppearance()
    {
        // Force repaint when palette changes
        Invalidate();
    }

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

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateInherit();
            UpdateAppearance();
        }
    }
    #endregion
}

