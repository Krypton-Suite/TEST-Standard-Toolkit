#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using System.Collections.ObjectModel;

namespace Krypton.Toolkit;

/// <summary>
/// Provides a tabbed editor control with KryptonRichTextBox controls in each tab page.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(TabControl))]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(TabPages))]
[DesignerCategory(@"code")]
[Description(@"Provides a tabbed editor control with KryptonRichTextBox controls in each tab page.")]
public class KryptonTabbedEditor : VisualPanel
{
    #region Instance Fields
    private readonly PaletteDoubleRedirect _stateCommon;
    private readonly PaletteDouble? _stateDisabled;
    private readonly PaletteDouble? _stateNormal;
    private readonly KryptonTabControl _tabControl;
    private readonly Collection<KryptonRichTextBox> _editorControls;
    private readonly TabbedEditorAppearanceValues _appearanceValues;
    private readonly TabbedEditorTabsValues _tabsValues;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTabbedEditor class.
    /// </summary>
    public KryptonTabbedEditor()
    {
        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(Redirector!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, OnPaletteNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnPaletteNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnPaletteNeedPaint);

        // Create the internal tab control
        _tabControl = new KryptonTabControl
        {
            Dock = DockStyle.Fill
        };
        _tabControl.SelectedIndexChanged += OnTabControlSelectedIndexChanged;
        _tabControl.TabButtonSpecClick += OnTabControlTabButtonSpecClick;

        // Create collection to track editor controls
        _editorControls = new Collection<KryptonRichTextBox>();

        // Add tab control to this panel
        Controls.Add(_tabControl);

        // Update appearance from palette
        UpdateAppearance();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from tab control events
            _tabControl.SelectedIndexChanged -= OnTabControlSelectedIndexChanged;
            _tabControl.TabButtonSpecClick -= OnTabControlTabButtonSpecClick;

            // Clear editor controls collection
            _editorControls.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the tabbed editor appearance values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tabbed editor appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TabbedEditorAppearanceValues AppearanceValues => _appearanceValues;

    private bool ShouldSerializeAppearanceValues() => !_appearanceValues.IsDefault;

    /// <summary>
    /// Gets and sets the tabbed editor background style.
    /// </summary>
    [Browsable(false)]
    [Category(@"Visuals")]
    [Description(@"Tabbed editor background style.")]
    public PaletteBackStyle TabbedEditorBackStyle
    {
        get => _stateCommon.BackStyle;

        set
        {
            if (_stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeTabbedEditorBackStyle() => TabbedEditorBackStyle != PaletteBackStyle.PanelClient;

    private void ResetTabbedEditorBackStyle() => TabbedEditorBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common tabbed editor appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tabbed editor appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon.Back;

    private bool ShouldSerializeStateCommon() => !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled tabbed editor appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tabbed editor appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled!.Back;

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal tabbed editor appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tabbed editor appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal!.Back;

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;

    /// <summary>
    /// Gets access to the tabbed editor tabs and selection values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Tabbed editor tabs and selection settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TabbedEditorTabsValues TabsValues => _tabsValues;

    private bool ShouldSerializeTabsValues() => !_tabsValues.IsDefault;

    /// <summary>
    /// Gets access to the underlying KryptonTabControl.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTabControl TabControl => _tabControl;

    /// <summary>
    /// Gets the collection of tab pages in this tabbed editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TabControl.TabPageCollection TabPages => _tabControl.TabPages;

    /// <summary>
    /// Gets the collection of editor controls (KryptonRichTextBox) in this tabbed editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReadOnlyCollection<KryptonRichTextBox> EditorControls => new ReadOnlyCollection<KryptonRichTextBox>(_editorControls);

    /// <summary>
    /// Gets or sets the index of the currently selected tab page.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int SelectedIndex
    {
        get => _tabControl.SelectedIndex;
        set => _tabControl.SelectedIndex = value;
    }

    /// <summary>
    /// Gets or sets the currently selected tab page.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public TabPage? SelectedTab
    {
        get => _tabControl.SelectedTab;
        set => _tabControl.SelectedTab = value;
    }

    /// <summary>
    /// Gets the currently selected editor control (KryptonRichTextBox).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRichTextBox? SelectedEditor
    {
        get
        {
            if (SelectedIndex >= 0 && SelectedIndex < _editorControls.Count)
            {
                return _editorControls[SelectedIndex];
            }
            return null;
        }
    }

    /// <summary>
    /// Gets or sets the alignment of the tabs on the tab control.
    /// </summary>
    [Browsable(false)]
    [Category(@"Appearance")]
    [Description(@"The alignment of the tabs on the tab control.")]
    [DefaultValue(TabAlignment.Top)]
    public TabAlignment Alignment
    {
        get => _tabControl.Alignment;
        set => _tabControl.Alignment = value;
    }

    /// <summary>
    /// Gets or sets the appearance of the control's tabs.
    /// </summary>
    [Browsable(false)]
    [Category(@"Appearance")]
    [Description(@"The appearance of the control's tabs.")]
    [DefaultValue(TabAppearance.Normal)]
    public TabAppearance Appearance
    {
        get => _tabControl.Appearance;
        set => _tabControl.Appearance = value;
    }

    /// <summary>
    /// Gets or sets the way that the control's tabs are sized.
    /// </summary>
    [Browsable(false)]
    [Category(@"Appearance")]
    [Description(@"The way that the control's tabs are sized.")]
    [DefaultValue(TabSizeMode.Normal)]
    public TabSizeMode SizeMode
    {
        get => _tabControl.SizeMode;
        set => _tabControl.SizeMode = value;
    }

    /// <summary>
    /// Gets or sets whether the tab control shows a tooltip for each tab.
    /// </summary>
    [Browsable(false)]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the tab control shows a tooltip for each tab.")]
    [DefaultValue(false)]
    public bool ShowToolTips
    {
        get => _tabControl.ShowToolTips;
        set => _tabControl.ShowToolTips = value;
    }

    /// <summary>
    /// Gets or sets the tab style for the tab control.
    /// </summary>
    [Browsable(false)]
    [Category(@"Visuals")]
    [Description(@"Tab style.")]
    [DefaultValue(TabStyle.StandardProfile)]
    public TabStyle TabStyle
    {
        get => _tabControl.TabStyle;
        set => _tabControl.TabStyle = value;
    }

    /// <summary>
    /// Gets or sets the tab border style for the tab control.
    /// </summary>
    [Browsable(false)]
    [Category(@"Visuals")]
    [Description(@"Tab border style.")]
    [DefaultValue(TabBorderStyle.SquareEqualSmall)]
    public TabBorderStyle TabBorderStyle
    {
        get => _tabControl.TabBorderStyle;
        set => _tabControl.TabBorderStyle = value;
    }

    /// <summary>
    /// Adds a new tab page with a KryptonRichTextBox editor.
    /// </summary>
    /// <param name="tabText">The text to display on the tab.</param>
    /// <returns>The newly created KryptonRichTextBox editor control.</returns>
    public KryptonRichTextBox AddTab(string tabText)
    {
        return AddTab(tabText, null);
    }

    /// <summary>
    /// Adds a new tab page with a KryptonRichTextBox editor.
    /// </summary>
    /// <param name="tabText">The text to display on the tab.</param>
    /// <param name="initialText">The initial text content for the editor.</param>
    /// <returns>The newly created KryptonRichTextBox editor control.</returns>
    public KryptonRichTextBox AddTab(string tabText, string? initialText)
    {
        // Create a new tab page
        var tabPage = new KryptonTabPage(tabText);

        // Create a new RichTextBox editor
        var editor = new KryptonRichTextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            AcceptsTab = true,
            WordWrap = true,
            ScrollBars = RichTextBoxScrollBars.Both
        };

        // Set initial text if provided
        if (!string.IsNullOrEmpty(initialText))
        {
            editor.Text = initialText;
        }

        // Add editor to tab page
        tabPage.Controls.Add(editor);

        // Add tab page to tab control
        _tabControl.TabPages.Add(tabPage);

        // Track the editor control
        _editorControls.Add(editor);

        // Add close button to the tab
        var closeButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipTitle = "Close",
            ToolTipBody = $"Close {tabText}"
        };
        _tabControl.SetTabButtonSpec(_tabControl.TabPages.Count - 1, closeButton);

        return editor;
    }

    /// <summary>
    /// Removes a tab page and its associated editor control.
    /// </summary>
    /// <param name="index">The index of the tab page to remove.</param>
    public void RemoveTab(int index)
    {
        if (index >= 0 && index < _tabControl.TabPages.Count)
        {
            // Remove button spec if present
            _tabControl.RemoveTabButtonSpec(index);

            // Remove the editor control from tracking
            if (index < _editorControls.Count)
            {
                _editorControls.RemoveAt(index);
            }

            // Remove the tab page
            _tabControl.TabPages.RemoveAt(index);
        }
    }

    /// <summary>
    /// Removes a tab page and its associated editor control.
    /// </summary>
    /// <param name="tabPage">The tab page to remove.</param>
    public void RemoveTab(TabPage tabPage)
    {
        var index = _tabControl.TabPages.IndexOf(tabPage);
        if (index >= 0)
        {
            RemoveTab(index);
        }
    }

    /// <summary>
    /// Removes all tab pages and their associated editor controls.
    /// </summary>
    public void ClearTabs()
    {
        // Remove all button specs
        for (var i = 0; i < _tabControl.TabPages.Count; i++)
        {
            _tabControl.RemoveTabButtonSpec(i);
        }

        _editorControls.Clear();
        _tabControl.TabPages.Clear();
    }

    /// <summary>
    /// Gets the editor control (KryptonRichTextBox) for a specific tab page.
    /// </summary>
    /// <param name="index">The index of the tab page.</param>
    /// <returns>The KryptonRichTextBox editor control, or null if not found.</returns>
    public KryptonRichTextBox? GetEditor(int index)
    {
        if (index >= 0 && index < _editorControls.Count)
        {
            return _editorControls[index];
        }
        return null;
    }

    /// <summary>
    /// Gets the editor control (KryptonRichTextBox) for a specific tab page.
    /// </summary>
    /// <param name="tabPage">The tab page.</param>
    /// <returns>The KryptonRichTextBox editor control, or null if not found.</returns>
    public KryptonRichTextBox? GetEditor(TabPage tabPage)
    {
        var index = _tabControl.TabPages.IndexOf(tabPage);
        return index >= 0 ? GetEditor(index) : null;
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for TabbedEditor
        // This method is provided for API consistency with other Krypton controls
    }
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the SelectedIndex property has changed.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the SelectedIndex property has changed.")]
    public event EventHandler? SelectedIndexChanged;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdateAppearance();
    }
    #endregion

    #region Implementation
    private void UpdateAppearance()
    {
        if (_stateNormal == null || _stateDisabled == null)
        {
            return;
        }

        var paletteState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        var paletteBack = Enabled ? _stateNormal.Back : _stateDisabled.Back;

        // Get background color from palette
        if (paletteBack.GetBackDraw(paletteState) == InheritBool.True)
        {
            var backColor1 = paletteBack.GetBackColor1(paletteState);
            if (backColor1 != Color.Empty)
            {
                base.BackColor = backColor1;
            }
        }

        // Update tab control palette to match
        _tabControl.PaletteMode = PaletteMode;
        if (PaletteMode == PaletteMode.Custom)
        {
            _tabControl.Palette = Palette;
        }
    }

    private void OnTabControlSelectedIndexChanged(object? sender, EventArgs e)
    {
        SelectedIndexChanged?.Invoke(this, e);
    }

    private void OnTabControlTabButtonSpecClick(object? sender, TabButtonSpecClickEventArgs e)
    {
        // Default behavior: close the tab when close button is clicked
        if (e.ButtonSpec.Type == PaletteButtonSpecStyle.Close)
        {
            RemoveTab(e.TabIndex);
        }
    }

    private void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        // Update appearance when palette changes
        UpdateAppearance();
        PerformNeedPaint(e.NeedLayout);
    }
    #endregion
}
