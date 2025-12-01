#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
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
public class KryptonSearchBox : KryptonTextBox
{
    #region Instance Fields
    private ButtonSpecAny? _searchButton;
    private ButtonSpecAny? _clearButton;
    private bool _showSearchButton;
    private bool _showClearButton;
    private bool _clearOnEscape;
    private bool _enableSuggestions;
    private int _suggestionMaxCount;
    private readonly List<string> _suggestions;
    private SuggestionPopup? _suggestionPopup;
    private int _selectedSuggestionIndex;
    private bool _isNavigatingSuggestions;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the search is triggered (Enter key pressed or search button clicked).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the search is triggered.")]
    public event EventHandler<SearchEventArgs>? Search;

    /// <summary>
    /// Occurs when the search text is cleared (clear button clicked, Escape key pressed, or programmatically cleared).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the search text is cleared.")]
    public event EventHandler? Cleared;

    /// <summary>
    /// Occurs when a suggestion is selected from the suggestion list.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a suggestion is selected from the suggestion list.")]
    public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

    /// <summary>
    /// Raises the Search event.
    /// </summary>
    /// <param name="e">A SearchEventArgs that contains the event data.</param>
    protected virtual void OnSearch(SearchEventArgs e) => Search?.Invoke(this, e);

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnCleared(EventArgs e) => Cleared?.Invoke(this, e);

    /// <summary>
    /// Raises the SuggestionSelected event.
    /// </summary>
    /// <param name="e">A SuggestionSelectedEventArgs that contains the event data.</param>
    protected virtual void OnSuggestionSelected(SuggestionSelectedEventArgs e) => SuggestionSelected?.Invoke(this, e);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSearchBox class.
    /// </summary>
    public KryptonSearchBox()
    {
        // Defaults
        _showSearchButton = true;
        _showClearButton = true;
        _clearOnEscape = true;
        _enableSuggestions = true;
        _suggestionMaxCount = 10;
        _suggestions = new List<string>();
        _selectedSuggestionIndex = -1;

        // Disable standard auto-complete
        base.AutoCompleteMode = AutoCompleteMode.None;
        base.AutoCompleteSource = AutoCompleteSource.None;

        // Enable button spec tooltips
        AllowButtonSpecToolTips = true;

        // Hook into text box events
        TextChanged += OnTextChangedInternal;
        KeyDown += OnKeyDownInternal;
        KeyUp += OnKeyUpInternal;
        LostFocus += OnLostFocusInternal;

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
        ButtonSpecs.Add(_searchButton);
        ButtonSpecs.Add(_clearButton);

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
            TextChanged -= OnTextChangedInternal;
            KeyDown -= OnKeyDownInternal;
            KeyUp -= OnKeyUpInternal;
            LostFocus -= OnLostFocusInternal;

            if (_searchButton != null)
            {
                _searchButton.Click -= OnSearchButtonClick;
            }

            if (_clearButton != null)
            {
                _clearButton.Click -= OnClearButtonClick;
            }

            HideSuggestions();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public

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
        get => CueHint.CueHintText;
        set => CueHint.CueHintText = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether custom suggestions are enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether custom suggestions are enabled.")]
    [DefaultValue(true)]
    public bool EnableSuggestions
    {
        get => _enableSuggestions;
        set
        {
            if (_enableSuggestions != value)
            {
                _enableSuggestions = value;
                if (!value)
                {
                    HideSuggestions();
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of suggestions to display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of suggestions to display.")]
    [DefaultValue(10)]
    public int SuggestionMaxCount
    {
        get => _suggestionMaxCount;
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            _suggestionMaxCount = value;
        }
    }

    /// <summary>
    /// Gets the collection of suggestion strings.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of suggestion strings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> Suggestions => _suggestions;

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
    /// Clears the search text.
    /// </summary>
    public new void Clear()
    {
        base.Clear();
        Focus();
        UpdateClearButtonVisibility();
        HideSuggestions();
        OnCleared(EventArgs.Empty);
    }

    /// <summary>
    /// Triggers the search event.
    /// </summary>
    public void PerformSearch()
    {
        HideSuggestions();
        OnSearch(new SearchEventArgs(Text));
    }

    /// <summary>
    /// Sets the suggestions from a collection of strings.
    /// </summary>
    /// <param name="suggestions">The collection of suggestion strings.</param>
    public void SetSuggestions(IEnumerable<string> suggestions)
    {
        if (suggestions == null)
        {
            throw new ArgumentNullException(nameof(suggestions));
        }

        _suggestions.Clear();
        _suggestions.AddRange(suggestions);
    }
    #endregion

    #region Implementation
    private void OnTextChangedInternal(object? sender, EventArgs e)
    {
        UpdateClearButtonVisibility();
        
        if (_enableSuggestions && !_isNavigatingSuggestions)
        {
            UpdateSuggestions();
        }
    }

    private void OnKeyDownInternal(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (_suggestionPopup != null && _suggestionPopup.Visible && _selectedSuggestionIndex >= 0)
            {
                // Select the highlighted suggestion
                SelectSuggestion(_selectedSuggestionIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                // Perform search
                e.Handled = true;
                e.SuppressKeyPress = true;
                PerformSearch();
            }
        }
        else if (e.KeyCode == Keys.Escape)
        {
            if (_suggestionPopup != null && _suggestionPopup.Visible)
            {
                // Hide suggestions
                HideSuggestions();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (_clearOnEscape)
            {
                // Clear text
                e.Handled = true;
                e.SuppressKeyPress = true;
                Clear();
            }
        }
        else if (e.KeyCode == Keys.Down)
        {
            if (_suggestionPopup != null && _suggestionPopup.Visible)
            {
                NavigateSuggestions(1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        else if (e.KeyCode == Keys.Up)
        {
            if (_suggestionPopup != null && _suggestionPopup.Visible)
            {
                NavigateSuggestions(-1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }

    private void OnKeyUpInternal(object? sender, KeyEventArgs e)
    {
        _isNavigatingSuggestions = false;
    }

    private void OnLostFocusInternal(object? sender, EventArgs e)
    {
        // Hide suggestions when focus is lost (with a small delay to allow clicking on suggestions)
        if (_suggestionPopup != null && _suggestionPopup.Visible)
        {
            // Check if focus is going to the popup or its child controls
            if (_suggestionPopup.ContainsFocus())
            {
                // Focus is going to the popup, don't hide
                return;
            }

            // Use a timer to delay hiding, allowing click events to process
            var timer = new System.Windows.Forms.Timer { Interval = 200 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                timer.Dispose();
                
                // Double-check that focus hasn't moved to the popup
                if (_suggestionPopup != null && !_suggestionPopup.ContainsFocus())
                {
                    HideSuggestions();
                }
            };
            timer.Start();
        }
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
        if (_clearButton != null)
        {
            _clearButton.Visible = _showClearButton && !string.IsNullOrEmpty(Text);
        }
    }

    private void UpdateSuggestions()
    {
        if (!_enableSuggestions || string.IsNullOrEmpty(Text))
        {
            HideSuggestions();
            return;
        }

        var searchText = Text.ToLower();
        var filtered = _suggestions
            .Where(s => !string.IsNullOrEmpty(s) && s.ToLower().IndexOf(searchText) >= 0)
            .Take(_suggestionMaxCount)
            .ToList();

        if (filtered.Count == 0)
        {
            HideSuggestions();
            return;
        }

        ShowSuggestions(filtered);
    }

    private void ShowSuggestions(List<string> suggestions)
    {
        if (_suggestionPopup == null)
        {
            _suggestionPopup = new SuggestionPopup(this);
            _suggestionPopup.SuggestionSelected += OnSuggestionPopupSelected;
        }

        _suggestionPopup.SetSuggestions(suggestions);
        _selectedSuggestionIndex = -1;

        // Position the popup below the search box
        var screenPoint = PointToScreen(new Point(0, Height));
        _suggestionPopup.Show(screenPoint, Width);
    }

    private void HideSuggestions()
    {
        if (_suggestionPopup != null)
        {
            _suggestionPopup.Hide();
            _selectedSuggestionIndex = -1;
        }
    }

    private void NavigateSuggestions(int direction)
    {
        if (_suggestionPopup == null || !_suggestionPopup.Visible)
        {
            return;
        }

        _isNavigatingSuggestions = true;
        var count = _suggestionPopup.SuggestionCount;

        if (count == 0)
        {
            return;
        }

        _selectedSuggestionIndex += direction;

        if (_selectedSuggestionIndex < 0)
        {
            _selectedSuggestionIndex = count - 1;
        }
        else if (_selectedSuggestionIndex >= count)
        {
            _selectedSuggestionIndex = 0;
        }

        _suggestionPopup.HighlightIndex(_selectedSuggestionIndex);
    }

        private void SelectSuggestion(int index)
        {
            if (_suggestionPopup == null || index < 0)
            {
                return;
            }

            var suggestion = _suggestionPopup.GetSuggestion(index);
            if (suggestion != null)
            {
                _isNavigatingSuggestions = true;
                Text = suggestion;
                SelectionStart = Text.Length;
                SelectionLength = 0;
                _isNavigatingSuggestions = false;

                HideSuggestions();
                var args = new SuggestionSelectedEventArgs(index) { Suggestion = suggestion };
                OnSuggestionSelected(args);
            }
        }

        private void OnSuggestionPopupSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            if (e.Suggestion != null)
            {
                _isNavigatingSuggestions = true;
                Text = e.Suggestion;
                SelectionStart = Text.Length;
                SelectionLength = 0;
                _isNavigatingSuggestions = false;

                HideSuggestions();
                OnSuggestionSelected(e);
            }
        }

    private Image? GetSearchIcon()
    {
        try
        {
            var icon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.ApplicationCalendar, IconSize.Small);
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

    #region SuggestionPopup
    private class SuggestionPopup : KryptonForm
    {
        private KryptonListBox? _listBox;
        private readonly List<string> _suggestions;
        private int _highlightedIndex;

        public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

        public int SuggestionCount => _suggestions.Count;

        public SuggestionPopup(KryptonSearchBox owner)
        {
            _suggestions = new List<string>();
            _highlightedIndex = -1;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.White;
            Padding = new Padding(1);
            
            // Prevent the form from stealing focus
            SetStyle(ControlStyles.Selectable, false);

            _listBox = new KryptonListBox
            {
                Dock = DockStyle.Fill
            };
            // Set border to not draw - use DrawBorders property instead
            _listBox.StateCommon.Border.DrawBorders = PaletteDrawBorders.None;
            _listBox.MouseClick += OnListBoxMouseClick;
            _listBox.DoubleClick += OnListBoxDoubleClick;

            Controls.Add(_listBox);

            // Inherit palette from owner
            if (owner.PaletteMode != PaletteMode.Custom)
            {
                PaletteMode = owner.PaletteMode;
            }
            else if (owner.LocalCustomPalette != null)
            {
                LocalCustomPalette = owner.LocalCustomPalette;
            }
        }

        public void SetSuggestions(List<string> suggestions)
        {
            _suggestions.Clear();
            _suggestions.AddRange(suggestions);

            if (_listBox != null)
            {
                _listBox.BeginUpdate();
                _listBox.Items.Clear();
                _listBox.Items.AddRange(suggestions.ToArray());
                _listBox.EndUpdate();
            }

            _highlightedIndex = -1;
        }

        public void HighlightIndex(int index)
        {
            if (_listBox != null && index >= 0 && index < _listBox.Items.Count)
            {
                _listBox.SelectedIndex = index;
                _listBox.TopIndex = Math.Max(0, index - 2);
            }
        }

        public string? GetSuggestion(int index)
        {
            if (index >= 0 && index < _suggestions.Count)
            {
                return _suggestions[index];
            }
            return null;
        }

        public void Show(Point location, int width)
        {
            if (_listBox == null)
            {
                return;
            }

            // Calculate height based on item count (max 8 items visible)
            // Use GetItemHeight(0) to get the height of the first item, or default to 20 if no items
            var itemHeight = _suggestions.Count > 0 ? _listBox.GetItemHeight(0) : 20;
            var itemCount = Math.Min(_suggestions.Count, 8);
            var height = (itemCount * itemHeight) + 2;

            Size = new Size(width, height);
            Location = location;

            // Show the form without activating it (doesn't steal focus)
            PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        }

        private void OnListBoxMouseClick(object? sender, MouseEventArgs e)
        {
            if (_listBox != null && e.Button == MouseButtons.Left)
            {
                var index = _listBox.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    OnSuggestionSelected(index);
                }
            }
        }

        private void OnListBoxDoubleClick(object? sender, EventArgs e)
        {
            if (_listBox != null && _listBox.SelectedIndex >= 0)
            {
                OnSuggestionSelected(_listBox.SelectedIndex);
            }
        }

        private void OnSuggestionSelected(int index)
        {
            var suggestion = GetSuggestion(index);
            var args = new SuggestionSelectedEventArgs(index) { Suggestion = suggestion };
            SuggestionSelected?.Invoke(this, args);
            Hide();
        }

        public bool ContainsFocus()
        {
            if (ContainsFocus)
            {
                return true;
            }

            // Check if any child control has focus
            return _listBox != null && _listBox.Focused;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_listBox != null)
                {
                    _listBox.MouseClick -= OnListBoxMouseClick;
                    _listBox.DoubleClick -= OnListBoxDoubleClick;
                    _listBox.Dispose();
                    _listBox = null;
                }
            }
            base.Dispose(disposing);
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

/// <summary>
/// Provides data for the SuggestionSelected event.
/// </summary>
public class SuggestionSelectedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the SuggestionSelectedEventArgs class.
    /// </summary>
    /// <param name="index">The index of the selected suggestion.</param>
    public SuggestionSelectedEventArgs(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Gets the index of the selected suggestion.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the selected suggestion text.
    /// </summary>
    public string? Suggestion { get; set; }
}
