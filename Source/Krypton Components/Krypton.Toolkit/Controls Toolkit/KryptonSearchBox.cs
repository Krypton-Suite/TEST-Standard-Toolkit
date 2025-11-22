#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a modern search input control with search icon and clear button.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(Search))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Provides a modern search input control with search icon and clear button.")]
public class KryptonSearchBox : UserControl
{
    #region Instance Fields
    private KryptonTextBox? _textBox;
    private ButtonSpecAny? _searchButton;
    private ButtonSpecAny? _clearButton;
    private bool _showSearchButton;
    private bool _showClearButton;
    private bool _clearOnEscape;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the search is triggered (Enter key pressed or search button clicked).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the search is triggered.")]
    public event EventHandler<SearchEventArgs>? Search;

    /// <summary>
    /// Raises the Search event.
    /// </summary>
    /// <param name="e">A SearchEventArgs that contains the event data.</param>
    protected virtual void OnSearch(SearchEventArgs e) => Search?.Invoke(this, e);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSearchBox class.
    /// </summary>
    public KryptonSearchBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor, true);

        // Defaults
        _showSearchButton = true;
        _showClearButton = true;
        _clearOnEscape = true;

        // Create the internal text box
        _textBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill
        };

        // Hook into text box events
        _textBox.TextChanged += OnTextBoxTextChanged;
        _textBox.KeyDown += OnTextBoxKeyDown;
        _textBox.Enter += OnTextBoxEnter;
        _textBox.Leave += OnTextBoxLeave;

        // Create search button (positioned on the right, first)
        _searchButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Style = PaletteButtonStyle.ButtonSpec,
            Edge = PaletteRelativeEdgeAlign.Far,
            Image = GetSearchIcon(),
            ToolTipTitle = @"Search",
            ToolTipBody = @"Click to search or press Enter"
        };
        _searchButton.Click += OnSearchButtonClick;

        // Create clear button (positioned on the right, after search button)
        _clearButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Style = PaletteButtonStyle.ButtonSpec,
            Edge = PaletteRelativeEdgeAlign.Far,
            Image = GetClearIcon(),
            ToolTipTitle = @"Clear",
            ToolTipBody = @"Clear the search text",
            Visible = false
        };
        _clearButton.Click += OnClearButtonClick;

        // Add buttons to text box
        _textBox.ButtonSpecs.Add(_searchButton);
        _textBox.ButtonSpecs.Add(_clearButton);

        // Add text box to controls
        Controls.Add(_textBox);

        // Update clear button visibility
        UpdateClearButtonVisibility();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_textBox != null)
            {
                _textBox.TextChanged -= OnTextBoxTextChanged;
                _textBox.KeyDown -= OnTextBoxKeyDown;
                _textBox.Enter -= OnTextBoxEnter;
                _textBox.Leave -= OnTextBoxLeave;
            }

            if (_searchButton != null)
            {
                _searchButton.Click -= OnSearchButtonClick;
            }

            if (_clearButton != null)
            {
                _clearButton.Click -= OnClearButtonClick;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => _textBox?.Text ?? string.Empty;
        set
        {
            if (_textBox != null && _textBox.Text != value)
            {
                _textBox.Text = value;
                UpdateClearButtonVisibility();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the search button is displayed.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the search button is displayed.")]
    [DefaultValue(true)]
    public bool ShowSearchButton
    {
        get => _showSearchButton;
        set
        {
            if (_showSearchButton != value)
            {
                _showSearchButton = value;
                if (_searchButton != null)
                {
                    _searchButton.Visible = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the clear button is displayed when text is entered.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the clear button is displayed when text is entered.")]
    [DefaultValue(true)]
    public bool ShowClearButton
    {
        get => _showClearButton;
        set
        {
            if (_showClearButton != value)
            {
                _showClearButton = value;
                UpdateClearButtonVisibility();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether pressing Escape clears the text.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether pressing Escape clears the text.")]
    [DefaultValue(true)]
    public bool ClearOnEscape
    {
        get => _clearOnEscape;
        set => _clearOnEscape = value;
    }

    /// <summary>
    /// Gets or sets the placeholder text (watermark) displayed when the text box is empty.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The placeholder text displayed when the text box is empty.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string PlaceholderText
    {
        get => _textBox?.CueHint.CueHintText ?? string.Empty;
        set
        {
            if (_textBox != null)
            {
                _textBox.CueHint.CueHintText = value;
            }
        }
    }

    /// <summary>
    /// Gets access to the underlying KryptonTextBox control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTextBox? TextBox => _textBox;

    /// <summary>
    /// Gets access to the search button specification.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? SearchButton => _searchButton;

    /// <summary>
    /// Gets access to the clear button specification.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? ClearButton => _clearButton;

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _textBox?.PaletteMode ?? PaletteMode.Global;
        set
        {
            if (_textBox != null)
            {
                _textBox.PaletteMode = value;
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
    public KryptonCustomPaletteBase? Palette
    {
        get => _textBox?.LocalCustomPalette;
        set
        {
            if (_textBox != null)
            {
                _textBox.LocalCustomPalette = value;
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && Palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        Palette = null;
    }

    /// <summary>
    /// Gets access to the common textbox appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common textbox appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect? StateCommon => _textBox?.StateCommon;

    /// <summary>
    /// Gets access to the disabled textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates? StateDisabled => _textBox?.StateDisabled;

    /// <summary>
    /// Gets access to the normal textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates? StateNormal => _textBox?.StateNormal;

    /// <summary>
    /// Gets access to the active textbox appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active textbox appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates? StateActive => _textBox?.StateActive;

    /// <summary>
    /// Clears the search text.
    /// </summary>
    public void Clear()
    {
        Text = string.Empty;
        _textBox?.Focus();
    }

    /// <summary>
    /// Triggers the search event.
    /// </summary>
    public void PerformSearch()
    {
        OnSearch(new SearchEventArgs(Text));
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        if (_textBox != null)
        {
            _textBox.Enabled = Enabled;
        }
    }

    /// <summary>
    /// Raises the FontChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        if (_textBox != null)
        {
            _textBox.Font = Font;
        }
    }

    /// <summary>
    /// Raises the BackColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e)
    {
        base.OnBackColorChanged(e);
        if (_textBox != null)
        {
            _textBox.BackColor = BackColor;
        }
    }

    /// <summary>
    /// Raises the ForeColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnForeColorChanged(EventArgs e)
    {
        base.OnForeColorChanged(e);
        if (_textBox != null)
        {
            _textBox.ForeColor = ForeColor;
        }
    }
    #endregion

    #region Implementation
    private void OnTextBoxTextChanged(object? sender, EventArgs e)
    {
        UpdateClearButtonVisibility();
        OnTextChanged(e);
    }

    private void OnTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            PerformSearch();
        }
        else if (e.KeyCode == Keys.Escape && _clearOnEscape)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            Clear();
        }
    }

    private void OnTextBoxEnter(object? sender, EventArgs e)
    {
        OnEnter(e);
    }

    private void OnTextBoxLeave(object? sender, EventArgs e)
    {
        OnLeave(e);
    }

    private void OnSearchButtonClick(object? sender, EventArgs e)
    {
        PerformSearch();
    }

    private void OnClearButtonClick(object? sender, EventArgs e)
    {
        Clear();
    }

    private void UpdateClearButtonVisibility()
    {
        if (_clearButton != null && _textBox != null)
        {
            _clearButton.Visible = _showClearButton && !string.IsNullOrEmpty(_textBox.Text);
        }
    }

    private Image? GetSearchIcon()
    {
        try
        {
            var icon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.ActionSearch, IconSize.Small);
            return icon?.ToBitmap();
        }
        catch
        {
            // Fallback to a simple search icon or null
            return null;
        }
    }

    private Image? GetClearIcon()
    {
        try
        {
            var icon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.ActionClear, IconSize.Small);
            return icon?.ToBitmap();
        }
        catch
        {
            // Fallback to a simple clear icon or null
            return null;
        }
    }
    #endregion
}

/// <summary>
/// Provides data for the Search event.
/// </summary>
public class SearchEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the SearchEventArgs class.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    public SearchEventArgs(string searchText)
    {
        SearchText = searchText;
    }

    /// <summary>
    /// Gets the search text.
    /// </summary>
    public string SearchText { get; }
}

