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

using System.Data;

namespace Krypton.Toolkit;

/// <summary>
/// Contains properties for configuring all aspects of the KryptonSearchBox control.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonSearchBoxValues : Storage
{
    #region Instance Fields
    private bool _showSearchButton;
    private bool _showClearButton;
    private bool _enableSuggestions;
    private int _suggestionMaxCount;
    private SuggestionDisplayType _suggestionDisplayType;
    private int _minimumSearchLength;
    private bool _enableSearchHistory;
    private int _searchHistoryMaxCount;
    private bool _clearOnEscape;
    private string _placeholderText;
    private Func<string, IEnumerable<object>, IEnumerable<object>>? _customFilter;
    private KryptonSearchBox? _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSearchBoxValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KryptonSearchBoxValues(KryptonSearchBox? owner)
    {
        _owner = owner;
        // Button defaults
        _showSearchButton = true;
        _showClearButton = true;
        // Suggestion defaults
        _enableSuggestions = true;
        _suggestionMaxCount = 10;
        _suggestionDisplayType = SuggestionDisplayType.ListBox;
        _minimumSearchLength = 0;
        // History defaults
        _enableSearchHistory = false;
        _searchHistoryMaxCount = 10;
        // Behavior defaults
        _clearOnEscape = true;
        _placeholderText = string.Empty;
        _customFilter = null;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => ShowSearchButton && 
                                      ShowClearButton &&
                                      EnableSuggestions && 
                                      SuggestionMaxCount == 10 && 
                                      SuggestionDisplayType == SuggestionDisplayType.ListBox &&
                                      MinimumSearchLength == 0 &&
                                      !EnableSearchHistory && 
                                      SearchHistoryMaxCount == 10 &&
                                      ClearOnEscape &&
                                      string.IsNullOrEmpty(PlaceholderText) &&
                                      CustomFilter == null;
    #endregion

    #region Button Properties
    /// <summary>
    /// Gets or sets a value indicating whether the search button is displayed.
    /// </summary>
    [Category(@"Buttons")]
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
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the clear button is displayed when text is entered.
    /// </summary>
    [Category(@"Buttons")]
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
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region Suggestion Properties
    /// <summary>
    /// Gets or sets a value indicating whether custom suggestions are enabled.
    /// </summary>
    [Category(@"Suggestions")]
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
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of suggestions to display.
    /// </summary>
    [Category(@"Suggestions")]
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
            if (_suggestionMaxCount != value)
            {
                _suggestionMaxCount = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the type of control used to display suggestions.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"The type of control used to display suggestions (ListBox or DataGridView).")]
    [DefaultValue(SuggestionDisplayType.ListBox)]
    public SuggestionDisplayType SuggestionDisplayType
    {
        get => _suggestionDisplayType;
        set
        {
            if (_suggestionDisplayType != value)
            {
                _suggestionDisplayType = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum number of characters required before showing suggestions.
    /// </summary>
    [Category(@"Suggestions")]
    [Description(@"The minimum number of characters required before showing suggestions.")]
    [DefaultValue(0)]
    public int MinimumSearchLength
    {
        get => _minimumSearchLength;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (_minimumSearchLength != value)
            {
                _minimumSearchLength = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region History Properties
    /// <summary>
    /// Gets or sets a value indicating whether search history is enabled.
    /// </summary>
    [Category(@"History")]
    [Description(@"Indicates whether search history is enabled.")]
    [DefaultValue(false)]
    public bool EnableSearchHistory
    {
        get => _enableSearchHistory;
        set
        {
            if (_enableSearchHistory != value)
            {
                _enableSearchHistory = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of search history items to remember.
    /// </summary>
    [Category(@"History")]
    [Description(@"The maximum number of search history items to remember.")]
    [DefaultValue(10)]
    public int SearchHistoryMaxCount
    {
        get => _searchHistoryMaxCount;
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            if (_searchHistoryMaxCount != value)
            {
                _searchHistoryMaxCount = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
    }
    #endregion

    #region Behavior Properties
    /// <summary>
    /// Gets or sets a value indicating whether pressing Escape clears the text.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether pressing Escape clears the text.")]
    [DefaultValue(true)]
    public bool ClearOnEscape
    {
        get => _clearOnEscape;
        set
        {
            if (_clearOnEscape != value)
            {
                _clearOnEscape = value;
                _owner?.OnSearchBoxValuesChanged();
            }
        }
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
        get => _placeholderText;
        set
        {
            if (_placeholderText != value)
            {
                _placeholderText = value ?? string.Empty;
                _owner?.OnPlaceholderTextChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets a custom filter function for suggestions.
    /// If set, this function will be used instead of the default filtering logic.
    /// The function receives the search text and the collection of suggestion objects, and returns the filtered collection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<string, IEnumerable<object>, IEnumerable<object>>? CustomFilter
    {
        get => _customFilter;
        set => _customFilter = value;
    }
    #endregion

    #region Data Properties
    /// <summary>
    /// Gets the collection of suggestion strings.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of suggestion strings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> Suggestions => _owner?._suggestions ?? new List<string>();

    /// <summary>
    /// Gets the collection of search history items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of search history items.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> SearchHistory => _owner?._searchHistory.AsReadOnly() ?? Array.Empty<string>().ToList().AsReadOnly();

    /// <summary>
    /// Gets the collection of column definitions for DataGridView suggestion display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Column definitions for DataGridView suggestion display. Only used when SuggestionDisplayType is DataGridView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<SuggestionColumnDefinition> DataGridViewColumns => _owner?._dataGridViewColumns ?? new List<SuggestionColumnDefinition>();
    #endregion

    #region Button References
    /// <summary>
    /// Gets access to the search button specification.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Access to the search button specification.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? SearchButton => _owner?._searchButton;

    /// <summary>
    /// Gets access to the clear button specification.
    /// </summary>
    [Category(@"Buttons")]
    [Description(@"Access to the clear button specification.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny? ClearButton => _owner?._clearButton;
    #endregion

    #region Implementation
    internal void SetOwner(KryptonSearchBox owner) => _owner = owner;
    #endregion
}

/// <summary>
/// Specifies the type of control used to display suggestions.
/// </summary>
public enum SuggestionDisplayType
{
    /// <summary>
    /// Display suggestions using a KryptonListBox.
    /// </summary>
    ListBox,

    /// <summary>
    /// Display suggestions using a KryptonDataGridView.
    /// </summary>
    DataGridView
}

/// <summary>
/// Represents a column definition for DataGridView suggestion display.
/// </summary>
public class SuggestionColumnDefinition
{
    /// <summary>
    /// Gets or sets the column name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data property name (for binding).
    /// </summary>
    public string DataPropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the header text.
    /// </summary>
    public string HeaderText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the column width (0 = auto-size).
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the auto-size mode.
    /// </summary>
    public DataGridViewAutoSizeColumnMode AutoSizeMode { get; set; } = DataGridViewAutoSizeColumnMode.Fill;

    /// <summary>
    /// Gets or sets a function to extract the column value from a suggestion object.
    /// </summary>
    public Func<object, object?>? ValueExtractor { get; set; }

    /// <summary>
    /// Initializes a new instance of the SuggestionColumnDefinition class.
    /// </summary>
    public SuggestionColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SuggestionColumnDefinition class.
    /// </summary>
    /// <param name="name">The column name.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="valueExtractor">Function to extract the column value.</param>
    public SuggestionColumnDefinition(string name, string headerText, Func<object, object?>? valueExtractor = null)
    {
        Name = name;
        DataPropertyName = name;
        HeaderText = headerText;
        ValueExtractor = valueExtractor;
    }
}

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
    internal ButtonSpecAny? _searchButton;
    internal ButtonSpecAny? _clearButton;
    internal readonly List<string> _suggestions;
    private readonly List<object> _richSuggestions; // For IContentValues support
    private SuggestionPopup? _suggestionPopup;
    private int _selectedSuggestionIndex;
    private bool _isNavigatingSuggestions;
    internal readonly List<string> _searchHistory;
    internal readonly List<SuggestionColumnDefinition> _dataGridViewColumns;
    
    // Property value group
    private readonly KryptonSearchBoxValues _values;
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
        // Initialize property value group
        _values = new KryptonSearchBoxValues(this);
        
        // Defaults
        _suggestions = new List<string>();
        _richSuggestions = new List<object>();
        _selectedSuggestionIndex = -1;
        _searchHistory = new List<string>();
        _dataGridViewColumns = new List<SuggestionColumnDefinition>();
        
        // Default column for DataGridView (single "Suggestion" column)
        _dataGridViewColumns.Add(new SuggestionColumnDefinition("Suggestion", "Suggestion", 
            obj => obj is IContentValues cv ? cv.GetShortText() : obj?.ToString()));

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
        
        // Initialize placeholder text
        OnPlaceholderTextChanged();
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
    /// Gets access to the search box configuration values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Search box settings for buttons, suggestions, and history.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonSearchBoxValues Values => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    /// <summary>
    /// Gets or sets a custom filter function for suggestions.
    /// If set, this function will be used instead of the default filtering logic.
    /// The function receives the search text and the collection of suggestion objects, and returns the filtered collection.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<string, IEnumerable<object>, IEnumerable<object>>? CustomFilter
    {
        get => _values.CustomFilter;
        set => _values.CustomFilter = value;
    }

    /// <summary>
    /// Gets the collection of column definitions for DataGridView suggestion display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Column definitions for DataGridView suggestion display. Only used when SuggestionDisplayType is DataGridView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<SuggestionColumnDefinition> DataGridViewColumns => _dataGridViewColumns;

    /// <summary>
    /// Sets the column definitions for DataGridView suggestion display.
    /// </summary>
    /// <param name="columns">The column definitions.</param>
    public void SetDataGridViewColumns(IEnumerable<SuggestionColumnDefinition> columns)
    {
        if (columns == null)
        {
            throw new ArgumentNullException(nameof(columns));
        }

        _dataGridViewColumns.Clear();
        _dataGridViewColumns.AddRange(columns);
        
        // Recreate popup if it exists to apply new columns
        if (_suggestionPopup != null)
        {
            _suggestionPopup.Dispose();
            _suggestionPopup = null;
        }
    }

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
        if (!string.IsNullOrEmpty(Text))
        {
            // Add to search history if enabled
            if (_values.EnableSearchHistory)
            {
                AddToSearchHistory(Text);
            }
        }
        
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

    /// <summary>
    /// Adds a search term to the search history.
    /// </summary>
    /// <param name="searchText">The search text to add.</param>
    public void AddToSearchHistory(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return;
        }

        // Remove if already exists (to move to top)
        _searchHistory.Remove(searchText);

        // Add to beginning
        _searchHistory.Insert(0, searchText);

        // Trim to max count
        while (_searchHistory.Count > _values.SearchHistoryMaxCount)
        {
            _searchHistory.RemoveAt(_searchHistory.Count - 1);
        }
    }

    /// <summary>
    /// Clears the search history.
    /// </summary>
    public void ClearSearchHistory()
    {
        _searchHistory.Clear();
    }

    /// <summary>
    /// Sets rich suggestions that support IContentValues (icons, descriptions, etc.).
    /// </summary>
    /// <param name="suggestions">Collection of suggestion objects (can be strings or IContentValues).</param>
    public void SetRichSuggestions(IEnumerable<object> suggestions)
    {
        if (suggestions == null)
        {
            throw new ArgumentNullException(nameof(suggestions));
        }

        _richSuggestions.Clear();
        _richSuggestions.AddRange(suggestions);
    }

    /// <summary>
    /// Adds a rich suggestion item.
    /// </summary>
    /// <param name="suggestion">The suggestion object (string or IContentValues).</param>
    public void AddRichSuggestion(object suggestion)
    {
        if (suggestion != null)
        {
            _richSuggestions.Add(suggestion);
        }
    }

    /// <summary>
    /// Clears all rich suggestions.
    /// </summary>
    public void ClearRichSuggestions()
    {
        _richSuggestions.Clear();
    }
    #endregion

    #region Implementation
    private void OnTextChangedInternal(object? sender, EventArgs e)
    {
        UpdateClearButtonVisibility();
        
        if (_values.EnableSuggestions && !_isNavigatingSuggestions)
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
            else if (_values.ClearOnEscape)
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
            if (_suggestionPopup.HasFocus())
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
                if (_suggestionPopup != null && !_suggestionPopup.HasFocus())
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
            _clearButton.Visible = _values.ShowClearButton && !string.IsNullOrEmpty(Text);
        }
    }

    internal void OnSearchBoxValuesChanged()
    {
        // Update button visibility
        if (_searchButton != null)
        {
            _searchButton.Visible = _values.ShowSearchButton;
        }
        UpdateClearButtonVisibility();
        
        // Handle suggestion changes
        if (!_values.EnableSuggestions)
        {
            HideSuggestions();
        }
        
        // Dispose existing popup if display type changed
        if (_suggestionPopup != null)
        {
            _suggestionPopup.Dispose();
            _suggestionPopup = null;
        }
        
        // Trim history if max count changed
        while (_searchHistory.Count > _values.SearchHistoryMaxCount)
        {
            _searchHistory.RemoveAt(_searchHistory.Count - 1);
        }
    }

    internal void OnPlaceholderTextChanged()
    {
        CueHint.CueHintText = _values.PlaceholderText;
    }

    private void UpdateSuggestions()
    {
        if (!_values.EnableSuggestions || string.IsNullOrEmpty(Text))
        {
            HideSuggestions();
            return;
        }

        // Check minimum search length
        if (Text.Length < _values.MinimumSearchLength)
        {
            HideSuggestions();
            return;
        }

        var searchText = Text.ToLower();
        List<object> filtered;

        // Use custom filter if provided
        if (_values.CustomFilter != null)
        {
            // Combine string suggestions and rich suggestions
            var allSuggestions = new List<object>();
            allSuggestions.AddRange(_suggestions);
            allSuggestions.AddRange(_richSuggestions);
            
            filtered = _values.CustomFilter(searchText, allSuggestions)
                .Take(_values.SuggestionMaxCount)
                .ToList();
        }
        else
        {
            // Default filtering logic
            var stringFiltered = _suggestions
                .Where(s => !string.IsNullOrEmpty(s) && s.ToLower().IndexOf(searchText) >= 0)
                .Take(_values.SuggestionMaxCount)
                .Cast<object>()
                .ToList();

            // Also filter rich suggestions (IContentValues)
            var richFiltered = _richSuggestions
                .Where(item =>
                {
                    if (item is IContentValues contentValues)
                    {
                        var shortText = contentValues.GetShortText() ?? string.Empty;
                        var longText = contentValues.GetLongText() ?? string.Empty;
                        return shortText.ToLower().IndexOf(searchText) >= 0 ||
                               longText.ToLower().IndexOf(searchText) >= 0;
                    }
                    return item.ToString()?.ToLower().IndexOf(searchText) >= 0;
                })
                .Take(_values.SuggestionMaxCount - stringFiltered.Count)
                .ToList();

            filtered = new List<object>();
            filtered.AddRange(stringFiltered);
            filtered.AddRange(richFiltered);
        }

        // Add search history if enabled and no other suggestions
        if (filtered.Count == 0 && _values.EnableSearchHistory && _searchHistory.Count > 0)
        {
            filtered = _searchHistory
                .Where(h => !string.IsNullOrEmpty(h) && h.ToLower().IndexOf(searchText) >= 0)
                .Take(_values.SuggestionMaxCount)
                .Cast<object>()
                .ToList();
        }

        if (filtered.Count == 0)
        {
            HideSuggestions();
            return;
        }

        ShowSuggestions(filtered);
    }

    private void ShowSuggestions(List<object> suggestions)
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
        
        // Explicitly maintain focus on the search box
        BeginInvoke(new Action(() =>
        {
            if (!Focused && CanFocus)
            {
                Focus();
            }
        }));
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

            var suggestionObject = _suggestionPopup.GetSuggestion(index);
            var suggestionText = _suggestionPopup.GetSuggestionText(index);
            
            if (!string.IsNullOrEmpty(suggestionText))
            {
                _isNavigatingSuggestions = true;
                Text = suggestionText;
                SelectionStart = Text.Length;
                SelectionLength = 0;
                _isNavigatingSuggestions = false;

                HideSuggestions();
                var args = new SuggestionSelectedEventArgs(index)
                {
                    Suggestion = suggestionText,
                    SuggestionObject = suggestionObject
                };
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
        private KryptonDataGridView? _dataGridView;
        private readonly List<object> _suggestions;
        private int _highlightedIndex;
        private DataTable? _dataTable;
        private readonly SuggestionDisplayType _displayType;
        private readonly KryptonSearchBox _owner;

        public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

        public int SuggestionCount => _suggestions.Count;

        public SuggestionPopup(KryptonSearchBox owner)
        {
            _suggestions = new List<object>();
            _highlightedIndex = -1;
            _displayType = owner._values.SuggestionDisplayType;
            _owner = owner;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.White;
            Padding = new Padding(1);
            
            // Prevent the form from stealing focus
            SetStyle(ControlStyles.Selectable, false);
            
            // Prevent activation
            SetStyle(ControlStyles.UserPaint, true);

            // Create the appropriate control based on display type
            if (_displayType == SuggestionDisplayType.DataGridView)
            {
                // Create DataTable for suggestions with columns from definitions
                _dataTable = new DataTable();
                
                // Add columns based on owner's column definitions
                foreach (var colDef in _owner.DataGridViewColumns)
                {
                    _dataTable.Columns.Add(colDef.DataPropertyName, typeof(object));
                }

                _dataGridView = new KryptonDataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeRows = false,
                    ColumnHeadersVisible = _owner.DataGridViewColumns.Count > 1, // Show headers if multiple columns
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    ShowCellToolTips = false,
                    ScrollBars = ScrollBars.None,
                    TabStop = false
                };

                // Add columns based on definitions
                foreach (var colDef in _owner.DataGridViewColumns)
                {
                    var column = new DataGridViewTextBoxColumn
                    {
                        Name = colDef.Name,
                        DataPropertyName = colDef.DataPropertyName,
                        HeaderText = colDef.HeaderText,
                        AutoSizeMode = colDef.AutoSizeMode,
                        ReadOnly = true
                    };
                    
                    if (colDef.Width > 0)
                    {
                        column.Width = colDef.Width;
                    }
                    
                    _dataGridView.Columns.Add(column);
                }
                
                _dataGridView.DataSource = _dataTable;

                // Hide outer borders - KryptonDataGridView already sets BorderStyle.None in constructor
                // We can use HideOuterBorders to hide cell borders if needed
                _dataGridView.HideOuterBorders = true;

                // Handle mouse events
                _dataGridView.CellMouseDown += OnDataGridViewCellMouseDown;
                _dataGridView.CellMouseClick += OnDataGridViewCellMouseClick;
                _dataGridView.CellDoubleClick += OnDataGridViewCellDoubleClick;
                _dataGridView.KeyDown += OnDataGridViewKeyDown;

                Controls.Add(_dataGridView);
            }
            else
            {
                // Use ListBox
                _listBox = new KryptonListBox
                {
                    Dock = DockStyle.Fill
                };
                // Set border to not draw - use DrawBorders property instead
                _listBox.StateCommon.Border.DrawBorders = PaletteDrawBorders.None;
                _listBox.MouseDown += OnListBoxMouseDown;
                _listBox.MouseClick += OnListBoxMouseClick;
                _listBox.DoubleClick += OnListBoxDoubleClick;

                Controls.Add(_listBox);
            }

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

        public void SetSuggestions(List<object> suggestions)
        {
            _suggestions.Clear();
            _suggestions.AddRange(suggestions);

            if (_displayType == SuggestionDisplayType.DataGridView)
            {
                if (_dataTable != null && _dataGridView != null)
                {
                    _dataTable.Rows.Clear();
                    foreach (var suggestion in suggestions)
                    {
                        // Create row with values for each column
                        var rowValues = new object[_owner.DataGridViewColumns.Count];
                        for (int i = 0; i < _owner.DataGridViewColumns.Count; i++)
                        {
                            var colDef = _owner.DataGridViewColumns[i];
                            if (colDef.ValueExtractor != null)
                            {
                                rowValues[i] = colDef.ValueExtractor(suggestion) ?? string.Empty;
                            }
                            else
                            {
                                // Default: extract text
                                rowValues[i] = GetSuggestionText(suggestion);
                            }
                        }
                        _dataTable.Rows.Add(rowValues);
                    }
                }
            }
            else
            {
                if (_listBox != null)
                {
                    _listBox.BeginUpdate();
                    _listBox.Items.Clear();
                    // Add items - can be strings or IContentValues
                    foreach (var suggestion in suggestions)
                    {
                        _listBox.Items.Add(suggestion);
                    }
                    _listBox.EndUpdate();
                }
            }

            _highlightedIndex = -1;
        }

        private string GetSuggestionText(object suggestion)
        {
            if (suggestion is IContentValues contentValues)
            {
                return contentValues.GetShortText() ?? string.Empty;
            }
            return suggestion.ToString() ?? string.Empty;
        }

        public void HighlightIndex(int index)
        {
            if (_displayType == SuggestionDisplayType.DataGridView)
            {
                if (_dataGridView != null && index >= 0 && index < _dataGridView.Rows.Count)
                {
                    _dataGridView.ClearSelection();
                    _dataGridView.Rows[index].Selected = true;
                    _dataGridView.CurrentCell = _dataGridView.Rows[index].Cells[0];
                    
                    // Scroll to make the selected row visible
                    _dataGridView.FirstDisplayedScrollingRowIndex = Math.Max(0, Math.Min(index, _dataGridView.RowCount - 1));
                }
            }
            else
            {
                if (_listBox != null && index >= 0 && index < _listBox.Items.Count)
                {
                    _listBox.SelectedIndex = index;
                    _listBox.TopIndex = Math.Max(0, index - 2);
                }
            }
        }

        public object? GetSuggestion(int index)
        {
            if (index >= 0 && index < _suggestions.Count)
            {
                return _suggestions[index];
            }
            return null;
        }

        public string? GetSuggestionText(int index)
        {
            var suggestion = GetSuggestion(index);
            if (suggestion == null)
            {
                return null;
            }

            if (suggestion is IContentValues contentValues)
            {
                return contentValues.GetShortText();
            }
            return suggestion.ToString();
        }

        public void Show(Point location, int width)
        {
            int height;

            if (_displayType == SuggestionDisplayType.DataGridView)
            {
                if (_dataGridView == null)
                {
                    return;
                }

                // Calculate height based on row count (max 8 rows visible)
                var rowHeight = _dataGridView.Rows.Count > 0 ? _dataGridView.Rows[0].Height : 22;
                var rowCount = Math.Min(_suggestions.Count, 8);
                height = (rowCount * rowHeight) + 2;
            }
            else
            {
                if (_listBox == null)
                {
                    return;
                }

                // Calculate height based on item count (max 8 items visible)
                var itemHeight = _suggestions.Count > 0 ? _listBox.GetItemHeight(0) : 20;
                var itemCount = Math.Min(_suggestions.Count, 8);
                height = (itemCount * itemHeight) + 2;
            }

            Size = new Size(width, height);
            Location = location;

            // Create handle if needed
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            // Show the form without activating it (doesn't steal focus)
            if (Handle != IntPtr.Zero)
            {
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!IsDisposed && value)
            {
                // Prevent the form from activating when it becomes visible
                if (IsHandleCreated && Handle != IntPtr.Zero)
                {
                    PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
                }
                return;
            }
            base.SetVisibleCore(value);
        }

        protected override void WndProc(ref Message m)
        {
            // Prevent activation messages from stealing focus
            if (m.Msg == PI.WM_.ACTIVATE || m.Msg == PI.WM_.MOUSEACTIVATE)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        private void OnListBoxMouseDown(object? sender, MouseEventArgs e)
        {
            if (_listBox != null && e.Button == MouseButtons.Left)
            {
                var index = _listBox.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    // Single click selects the suggestion immediately
                    OnSuggestionSelected(index);
                }
            }
        }

        private void OnListBoxMouseClick(object? sender, MouseEventArgs e)
        {
            // Also handle MouseClick as a fallback
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
            // Also handle double click for consistency
            if (_listBox != null && _listBox.SelectedIndex >= 0)
            {
                OnSuggestionSelected(_listBox.SelectedIndex);
            }
        }

        private void OnDataGridViewCellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dataGridView != null && e.Button == MouseButtons.Left && e.RowIndex >= 0)
            {
                // Single click selects the suggestion immediately
                OnSuggestionSelected(e.RowIndex);
            }
        }

        private void OnDataGridViewCellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            // Also handle MouseClick as a fallback
            if (_dataGridView != null && e.Button == MouseButtons.Left && e.RowIndex >= 0)
            {
                OnSuggestionSelected(e.RowIndex);
            }
        }

        private void OnDataGridViewCellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Also handle double click for consistency
            if (_dataGridView != null && e.RowIndex >= 0)
            {
                OnSuggestionSelected(e.RowIndex);
            }
        }

        private void OnDataGridViewKeyDown(object? sender, KeyEventArgs e)
        {
            // Handle Enter key to select the current row
            if (e.KeyCode == Keys.Enter && _dataGridView != null && _dataGridView.CurrentRow != null)
            {
                OnSuggestionSelected(_dataGridView.CurrentRow.Index);
                e.Handled = true;
            }
        }

        private void OnSuggestionSelected(int index)
        {
            var suggestionObject = GetSuggestion(index);
            var suggestionText = GetSuggestionText(index);
            var args = new SuggestionSelectedEventArgs(index)
            {
                Suggestion = suggestionText,
                SuggestionObject = suggestionObject
            };
            SuggestionSelected?.Invoke(this, args);
            Hide();
        }

        public bool HasFocus()
        {
            if (ContainsFocus)
            {
                return true;
            }

            // Check if any child control has focus
            if (_displayType == SuggestionDisplayType.DataGridView)
            {
                return _dataGridView != null && _dataGridView.Focused;
            }
            else
            {
                return _listBox != null && _listBox.Focused;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_listBox != null)
                {
                    _listBox.MouseDown -= OnListBoxMouseDown;
                    _listBox.MouseClick -= OnListBoxMouseClick;
                    _listBox.DoubleClick -= OnListBoxDoubleClick;
                    _listBox.Dispose();
                    _listBox = null;
                }

                if (_dataGridView != null)
                {
                    _dataGridView.CellMouseDown -= OnDataGridViewCellMouseDown;
                    _dataGridView.CellMouseClick -= OnDataGridViewCellMouseClick;
                    _dataGridView.CellDoubleClick -= OnDataGridViewCellDoubleClick;
                    _dataGridView.KeyDown -= OnDataGridViewKeyDown;
                    _dataGridView.Dispose();
                    _dataGridView = null;
                }

                if (_dataTable != null)
                {
                    _dataTable.Dispose();
                    _dataTable = null;
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
    /// Gets the selected suggestion text (for backward compatibility).
    /// </summary>
    public string? Suggestion { get; set; }

    /// <summary>
    /// Gets the selected suggestion object (can be string or IContentValues).
    /// </summary>
    public object? SuggestionObject { get; set; }
}


