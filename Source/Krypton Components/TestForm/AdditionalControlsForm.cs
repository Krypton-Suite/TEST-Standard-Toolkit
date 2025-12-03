using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm
{
    public partial class AdditionalControlsForm : KryptonForm
    {
        private readonly List<string> _allItems = new();
        private DataTable? _dataTable;
        private readonly List<object> _richSuggestions = new();

        public AdditionalControlsForm()
        {
            InitializeComponent();
            InitializeListBox();
            InitializeDataGridView();
            InitializeSearchBoxes();
        }

        private void InitializeDataGridView()
        {
            // Create a DataTable with sample fruit data
            _dataTable = new DataTable();
            _dataTable.Columns.Add("Name", typeof(string));
            _dataTable.Columns.Add("Color", typeof(string));
            _dataTable.Columns.Add("Season", typeof(string));

            // Populate with sample data
            _dataTable.Rows.Add("Apple", "Red/Green", "Fall");
            _dataTable.Rows.Add("Banana", "Yellow", "Year-round");
            _dataTable.Rows.Add("Cherry", "Red", "Summer");
            _dataTable.Rows.Add("Date", "Brown", "Fall");
            _dataTable.Rows.Add("Elderberry", "Purple", "Summer");
            _dataTable.Rows.Add("Fig", "Purple/Green", "Summer");
            _dataTable.Rows.Add("Grape", "Purple/Green", "Fall");
            _dataTable.Rows.Add("Honeydew", "Green", "Summer");
            _dataTable.Rows.Add("Kiwi", "Brown/Green", "Year-round");
            _dataTable.Rows.Add("Lemon", "Yellow", "Year-round");
            _dataTable.Rows.Add("Mango", "Orange/Yellow", "Summer");
            _dataTable.Rows.Add("Orange", "Orange", "Winter");
            _dataTable.Rows.Add("Papaya", "Orange", "Year-round");
            _dataTable.Rows.Add("Quince", "Yellow", "Fall");
            _dataTable.Rows.Add("Raspberry", "Red", "Summer");
            _dataTable.Rows.Add("Strawberry", "Red", "Spring");
            _dataTable.Rows.Add("Tangerine", "Orange", "Winter");
            _dataTable.Rows.Add("Watermelon", "Green/Red", "Summer");
            _dataTable.Rows.Add("Apricot", "Orange", "Summer");
            _dataTable.Rows.Add("Blueberry", "Blue", "Summer");

            // Bind to DataGridView
            kryptonDataGridView1.DataSource = _dataTable;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void InitializeSearchBoxes()
        {
            // ============================================
            // SearchBox 1: Basic ListBox suggestions with search history
            // ============================================
            kryptonSearchBox1.Values.PlaceholderText = "Search fruits (ListBox, with history)...";
            kryptonSearchBox1.Values.ShowSearchButton = true;
            kryptonSearchBox1.Values.ShowClearButton = true;
            kryptonSearchBox1.Values.ClearOnEscape = true;
            kryptonSearchBox1.Values.EnableSuggestions = true;
            kryptonSearchBox1.Values.SuggestionMaxCount = 8;
            kryptonSearchBox1.Values.SuggestionDisplayType = SuggestionDisplayType.ListBox;
            
            // Enable search history
            kryptonSearchBox1.Values.EnableSearchHistory = true;
            kryptonSearchBox1.Values.SearchHistoryMaxCount = 10;
            
            // Set minimum search length (show suggestions after 1 character)
            kryptonSearchBox1.Values.MinimumSearchLength = 1;

            // Set up custom suggestions from the listbox items
            kryptonSearchBox1.SetSuggestions(_allItems);

            // Handle events
            kryptonSearchBox1.TextChanged += KryptonSearchBox1_TextChanged;
            kryptonSearchBox1.Search += KryptonSearchBox_Search;
            kryptonSearchBox1.Cleared += KryptonSearchBox1_Cleared;
            kryptonSearchBox1.SuggestionSelected += KryptonSearchBox1_SuggestionSelected;

            // ============================================
            // SearchBox 2: DataGridView highlighting (no suggestions)
            // ============================================
            kryptonSearchBox2.Values.PlaceholderText = "Search DataGridView (highlights matches)...";
            kryptonSearchBox2.Values.ShowSearchButton = true;
            kryptonSearchBox2.Values.ShowClearButton = true;
            kryptonSearchBox2.Values.ClearOnEscape = true;
            kryptonSearchBox2.Values.EnableSuggestions = false;

            kryptonSearchBox2.TextChanged += KryptonSearchBox2_TextChanged;
            kryptonSearchBox2.Search += KryptonSearchBox2_Search;
            kryptonSearchBox2.Cleared += KryptonSearchBox2_Cleared;

            // ============================================
            // SearchBox 3: Rich suggestions with icons (ListBox)
            // ============================================
            InitializeRichSuggestions();
            kryptonSearchBox3.Values.PlaceholderText = "Rich suggestions (icons, descriptions)...";
            kryptonSearchBox3.Values.ShowSearchButton = true;
            kryptonSearchBox3.Values.ShowClearButton = true;
            kryptonSearchBox3.Values.EnableSuggestions = true;
            kryptonSearchBox3.Values.SuggestionMaxCount = 8;
            kryptonSearchBox3.Values.SuggestionDisplayType = SuggestionDisplayType.ListBox;
            kryptonSearchBox3.Values.MinimumSearchLength = 1;
            
            // Set rich suggestions (with icons and descriptions)
            kryptonSearchBox3.SetRichSuggestions(_richSuggestions);
            
            kryptonSearchBox3.SuggestionSelected += KryptonSearchBox3_SuggestionSelected;

            // ============================================
            // SearchBox 4: DataGridView with multiple columns
            // ============================================
            InitializeDataGridViewSuggestions();
            kryptonSearchBox4.Values.PlaceholderText = "Multi-column DataGridView suggestions...";
            kryptonSearchBox4.Values.ShowSearchButton = true;
            kryptonSearchBox4.Values.ShowClearButton = true;
            kryptonSearchBox4.Values.EnableSuggestions = true;
            kryptonSearchBox4.Values.SuggestionMaxCount = 8;
            kryptonSearchBox4.Values.SuggestionDisplayType = SuggestionDisplayType.DataGridView;
            kryptonSearchBox4.Values.MinimumSearchLength = 1;
            
            // Set up multi-column DataGridView
            var columns = new List<SuggestionColumnDefinition>
            {
                new SuggestionColumnDefinition("Name", "Name", 
                    obj => obj is FruitItem fi ? fi.Name : (obj is IContentValues cv ? cv.GetShortText() : obj?.ToString())),
                new SuggestionColumnDefinition("Color", "Color", 
                    obj => obj is FruitItem fi ? fi.Color : string.Empty)
                {
                    Width = 100,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                },
                new SuggestionColumnDefinition("Season", "Season", 
                    obj => obj is FruitItem fi ? fi.Season : string.Empty)
                {
                    Width = 80,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                }
            };
            kryptonSearchBox4.SetDataGridViewColumns(columns);
            
            // Set rich suggestions for DataGridView (with icons and descriptions)
            kryptonSearchBox4.SetRichSuggestions(_richSuggestions);
            
            kryptonSearchBox4.SuggestionSelected += KryptonSearchBox4_SuggestionSelected;

            // ============================================
            // SearchBox 5: Custom filtering
            // ============================================
            kryptonSearchBox5.Values.PlaceholderText = "Custom filter (starts with only)...";
            kryptonSearchBox5.Values.ShowSearchButton = true;
            kryptonSearchBox5.Values.ShowClearButton = true;
            kryptonSearchBox5.Values.EnableSuggestions = true;
            kryptonSearchBox5.Values.SuggestionMaxCount = 8;
            kryptonSearchBox5.Values.SuggestionDisplayType = SuggestionDisplayType.ListBox;
            kryptonSearchBox5.Values.MinimumSearchLength = 1;
            
            // Set custom filter (only show items that start with search text)
            kryptonSearchBox5.CustomFilter = (searchText, suggestions) =>
            {
                var searchLower = searchText.ToLower();
                return suggestions.Where(s =>
                {
                    string? text = null;
                    if (s is IContentValues cv)
                    {
                        text = cv.GetShortText();
                    }
                    else
                    {
                        text = s?.ToString();
                    }
                    return !string.IsNullOrEmpty(text) && text.ToLower().StartsWith(searchLower);
                });
            };
            
            kryptonSearchBox5.SetSuggestions(_allItems);
            kryptonSearchBox5.SuggestionSelected += KryptonSearchBox5_SuggestionSelected;
        }

        private void InitializeRichSuggestions()
        {
            // Create rich suggestions with icons and descriptions
            // Using fruit data from DataGridView
            var fruitData = new[]
            {
                new { Name = "Apple", Color = "Red/Green", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Banana", Color = "Yellow", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Cherry", Color = "Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Date", Color = "Brown", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Elderberry", Color = "Purple", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Fig", Color = "Purple/Green", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Grape", Color = "Purple/Green", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Honeydew", Color = "Green", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Kiwi", Color = "Brown/Green", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Lemon", Color = "Yellow", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Mango", Color = "Orange/Yellow", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Orange", Color = "Orange", Season = "Winter", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Papaya", Color = "Orange", Season = "Year-round", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Quince", Color = "Yellow", Season = "Fall", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Raspberry", Color = "Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Strawberry", Color = "Red", Season = "Spring", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Tangerine", Color = "Orange", Season = "Winter", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Watermelon", Color = "Green/Red", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Apricot", Color = "Orange", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar },
                new { Name = "Blueberry", Color = "Blue", Season = "Summer", Icon = ImageresIconID.ApplicationCalendar }
            };

            foreach (var fruit in fruitData)
            {
                try
                {
                    // Try to get an icon (using a simple approach - you can use actual fruit icons if available)
                    Image? icon = null;
                    try
                    {
                        var iconObj = GraphicsExtensions.ExtractIconFromImageres((int)fruit.Icon, IconSize.Small);
                        if (iconObj != null)
                        {
                            icon = iconObj.ToBitmap();
                            iconObj.Dispose();
                        }
                    }
                    catch
                    {
                        // Fallback if icon extraction fails
                    }

                    // Create KryptonListItem with icon and description
                    var item = new KryptonListItem(
                        fruit.Name,
                        $"{fruit.Color} - {fruit.Season}",
                        icon
                    );
                    
                    // Store additional data in Tag
                    item.Tag = new FruitItem { Name = fruit.Name, Color = fruit.Color, Season = fruit.Season };
                    
                    _richSuggestions.Add(item);
                }
                catch
                {
                    // Fallback to simple string if icon creation fails
                    _richSuggestions.Add(new FruitItem { Name = fruit.Name, Color = fruit.Color, Season = fruit.Season });
                }
            }
        }

        private void InitializeDataGridViewSuggestions()
        {
            // This is already done in InitializeRichSuggestions
            // The same rich suggestions are used for DataGridView
        }

        // Helper class for fruit data
        private class FruitItem : IContentValues
        {
            public string Name { get; set; } = string.Empty;
            public string Color { get; set; } = string.Empty;
            public string Season { get; set; } = string.Empty;

            public string GetShortText() => Name;
            public string GetLongText() => $"{Color} - {Season}";
            public Image? GetImage(PaletteState state) => null;
            public Color GetImageTransparentColor(PaletteState state) => this.GetImageTransparentColor(state);
        }

        private void InitializeListBox()
        {
            // Populate listbox with sample data
            _allItems.AddRange(new[]
            {
                "Apple", "Banana", "Cherry", "Date", "Elderberry",
                "Fig", "Grape", "Honeydew", "Kiwi", "Lemon",
                "Mango", "Orange", "Papaya", "Quince", "Raspberry",
                "Strawberry", "Tangerine", "Watermelon", "Apricot", "Blueberry",
                "Coconut", "Dragonfruit", "Grapefruit", "Lime", "Peach",
                "Pear", "Pineapple", "Plum", "Pomegranate", "Blackberry"
            });

            kryptonListBox1.Items.AddRange(_allItems.ToArray());
            kryptonLabel4.Text = $"Total items: {_allItems.Count}";
        }

        private void KryptonSearchBox1_TextChanged(object? sender, EventArgs e)
        {
            // Real-time filtering as user types
            FilterListBox(kryptonSearchBox1.Text);
        }

        private void FilterListBox(string searchText)
        {
            kryptonListBox1.BeginUpdate();
            kryptonListBox1.Items.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all items when search is empty
                kryptonListBox1.Items.AddRange(_allItems.ToArray());
                kryptonLabel4.Text = $"Total items: {_allItems.Count}";
            }
            else
            {
                // Filter items that contain the search text (case-insensitive)
                // Using IndexOf for compatibility with older .NET Framework versions
                var searchTextLower = searchText.ToLower();
                var filtered = _allItems.Where(item => 
                    item.ToLower().IndexOf(searchTextLower) >= 0).ToList();
                
                kryptonListBox1.Items.AddRange(filtered.ToArray());
                kryptonLabel4.Text = $"Found {filtered.Count} of {_allItems.Count} items";
            }

            kryptonListBox1.EndUpdate();
        }

        private void KryptonSearchBox_Search(object? sender, Krypton.Toolkit.SearchEventArgs e)
        {
            // Example: Handle search event
            kryptonLabel1.Text = $"Searching for: {e.SearchText}";
            
            // Filter the listbox
            FilterListBox(e.SearchText);
            
            if (!string.IsNullOrEmpty(e.SearchText))
            {
                kryptonLabel2.Text = $"Found {kryptonListBox1.Items.Count} matching items";
            }
            else
            {
                kryptonLabel2.Text = "Showing all items";
            }
        }

        private void KryptonSearchBox2_TextChanged(object? sender, EventArgs e)
        {
            // Real-time highlighting in DataGridView as user types
            HighlightDataGridView(kryptonSearchBox2.Text);
        }

        private void KryptonSearchBox2_Search(object? sender, Krypton.Toolkit.SearchEventArgs e)
        {
            // Highlight search results in DataGridView
            HighlightDataGridView(e.SearchText);
            kryptonLabel3.Text = string.IsNullOrEmpty(e.SearchText) 
                ? "DataGridView search cleared" 
                : $"Highlighting: {e.SearchText}";
        }

        private void KryptonSearchBox2_Cleared(object? sender, EventArgs e)
        {
            // Clear highlighting when search is cleared
            HighlightDataGridView(string.Empty);
            kryptonLabel3.Text = "DataGridView search cleared";
        }

        private void HighlightDataGridView(string searchText)
        {
            if (kryptonDataGridView1 != null)
            {
                // Use KryptonDataGridView's built-in HighlightSearch method
                // Empty string clears the highlighting
                kryptonDataGridView1.HighlightSearch(searchText);
            }
        }

        private void KryptonButton1_Click(object? sender, EventArgs e)
        {
            // Example: Programmatically trigger search
            kryptonSearchBox1.PerformSearch();
        }

        private void KryptonSearchBox1_Cleared(object? sender, EventArgs e)
        {
            // Handle clear event - update UI when search is cleared
            FilterListBox(string.Empty);
            kryptonLabel1.Text = "Search cleared";
            kryptonLabel2.Text = "Showing all items";
        }

        private void KryptonButton2_Click(object? sender, EventArgs e)
        {
            // Example: Clear search programmatically
            // The Cleared event will be raised automatically
            kryptonSearchBox1.Clear();
        }

        private void KryptonSearchBox1_SuggestionSelected(object? sender, Krypton.Toolkit.SuggestionSelectedEventArgs e)
        {
            // Example: Handle suggestion selection from the dropdown
            // The text is already set in the search box, but we can perform additional actions
            kryptonLabel1.Text = $"Selected: {e.Suggestion ?? kryptonSearchBox1.Text}";
            FilterListBox(kryptonSearchBox1.Text);
            
            // Show search history count
            if (kryptonSearchBox1.Values.EnableSearchHistory)
            {
                kryptonLabel2.Text = $"History: {kryptonSearchBox1.Values.SearchHistory.Count} items";
            }
        }

        private void KryptonSearchBox3_SuggestionSelected(object? sender, Krypton.Toolkit.SuggestionSelectedEventArgs e)
        {
            // Handle rich suggestion selection
            string displayText = e.Suggestion ?? kryptonSearchBox3.Text;
            
            // Access the full object if needed
            if (e.SuggestionObject is KryptonListItem kli)
            {
                displayText = $"{kli.ShortText} - {kli.LongText}";
            }
            else if (e.SuggestionObject is FruitItem fi)
            {
                displayText = $"{fi.Name} ({fi.Color}, {fi.Season})";
            }
            
            kryptonLabel5.Text = $"Rich suggestion: {displayText}";
        }

        private void KryptonSearchBox4_SuggestionSelected(object? sender, Krypton.Toolkit.SuggestionSelectedEventArgs e)
        {
            // Handle DataGridView multi-column suggestion selection
            string displayText = e.Suggestion ?? kryptonSearchBox4.Text;
            
            if (e.SuggestionObject is FruitItem fi)
            {
                displayText = $"{fi.Name} | {fi.Color} | {fi.Season}";
            }
            
            kryptonLabel6.Text = $"Multi-column: {displayText}";
        }

        private void KryptonSearchBox5_SuggestionSelected(object? sender, Krypton.Toolkit.SuggestionSelectedEventArgs e)
        {
            // Handle custom filter suggestion selection
            kryptonLabel7.Text = $"Custom filter: {e.Suggestion ?? kryptonSearchBox5.Text}";
        }
    }
}
