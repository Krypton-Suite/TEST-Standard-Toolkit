using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class AdditionalControlsForm : KryptonForm
    {
        private readonly List<string> _allItems = new();

        public AdditionalControlsForm()
        {
            InitializeComponent();
            InitializeListBox();
            InitializeSearchBox();
        }

        private void InitializeSearchBox()
        {
            // Configure search box properties
            kryptonSearchBox1.PlaceholderText = "Search fruits...";
            kryptonSearchBox1.ShowSearchButton = true;
            kryptonSearchBox1.ShowClearButton = true;
            kryptonSearchBox1.ClearOnEscape = true;
            kryptonSearchBox1.EnableSuggestions = true;
            kryptonSearchBox1.SuggestionMaxCount = 10;

            // Set up custom suggestions from the listbox items
            kryptonSearchBox1.SetSuggestions(_allItems);

            // Handle text change for real-time filtering
            kryptonSearchBox1.TextChanged += KryptonSearchBox1_TextChanged;

            // Handle search event (Enter key or search button click)
            kryptonSearchBox1.Search += KryptonSearchBox_Search;

            // Handle clear event (clear button, Escape key, or programmatic clear)
            kryptonSearchBox1.Cleared += KryptonSearchBox1_Cleared;

            // Handle suggestion selected event (when user selects from dropdown)
            kryptonSearchBox1.SuggestionSelected += KryptonSearchBox1_SuggestionSelected;
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

        private void KryptonSearchBox2_Search(object? sender, Krypton.Toolkit.SearchEventArgs e)
        {
            // Example: Different search box with different behavior
            kryptonLabel3.Text = $"Advanced search: {e.SearchText}";
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
            kryptonLabel1.Text = $"Selected suggestion: {e.Suggestion ?? kryptonSearchBox1.Text}";
            FilterListBox(kryptonSearchBox1.Text);
        }

        // Example: You can also configure the search box programmatically
        private void ConfigureSearchBoxExample()
        {
            // Disable suggestions if needed
            // kryptonSearchBox1.EnableSuggestions = false;

            // Change maximum suggestion count
            // kryptonSearchBox1.SuggestionMaxCount = 5;

            // Hide search button (search still works with Enter key)
            // kryptonSearchBox1.ShowSearchButton = false;

            // Hide clear button
            // kryptonSearchBox1.ShowClearButton = false;

            // Disable clearing on Escape
            // kryptonSearchBox1.ClearOnEscape = false;

            // Update suggestions dynamically
            // var newSuggestions = new List<string> { "New", "Items" };
            // kryptonSearchBox1.SetSuggestions(newSuggestions);
        }
    }
}
