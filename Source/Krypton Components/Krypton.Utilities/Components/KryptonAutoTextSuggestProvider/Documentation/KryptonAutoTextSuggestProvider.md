# KryptonAutoTextSuggestProvider

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Getting Started](#getting-started)
4. [API Reference](#api-reference)
5. [Configuration](#configuration)
6. [Event Handling](#event-handling)
7. [Usage Examples](#usage-examples)
8. [Advanced Scenarios](#advanced-scenarios)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)

---

## Overview

`KryptonAutoTextSuggestProvider` is a component that provides automatic text suggestion functionality for text controls in Windows Forms applications. It displays a popup list of suggestions as the user types, allowing them to quickly complete text input by selecting from filtered suggestions.

### Key Capabilities

- **Automatic Suggestions**: Displays suggestions automatically as the user types
- **Multiple Match Modes**: Supports StartsWith, Contains, and Fuzzy matching algorithms
- **Configurable Behavior**: Customizable delay, minimum characters, case sensitivity, and more
- **Keyboard Navigation**: Full keyboard support (Arrow keys, Enter, Escape)
- **Event-Driven**: Extensible through events for custom filtering and selection handling
- **Krypton Integration**: Fully integrated with Krypton Toolkit's theming and styling system

### Supported Controls

The provider can attach to the following text controls:
- `System.Windows.Forms.TextBox`
- `Krypton.Toolkit.KryptonTextBox`
- `System.Windows.Forms.RichTextBox`

---

## Features

### 1. Intelligent Word Detection

The provider automatically detects word boundaries at the cursor position, extracting the current word being typed for filtering suggestions. Word characters include letters, digits, underscores (`_`), and periods (`.`).

### 2. Multiple Match Modes

Three different matching algorithms are available:

- **StartsWith**: Matches items that begin with the filter text (default)
- **Contains**: Matches items that contain the filter text anywhere
- **Fuzzy**: Matches items where pattern characters appear in order (e.g., "js" matches "JavaScript")

### 3. Configurable Popup Behavior

- **Show Delay**: Configurable delay before showing suggestions (default: 300ms)
- **Minimum Characters**: Minimum number of characters before showing suggestions (default: 1)
- **Maximum Visible Items**: Limits the number of items shown in the popup (default: 8)
- **Popup Width**: Customizable width of the suggestion popup (default: 250px)

### 4. Keyboard Navigation

- **Arrow Down**: Navigate to next suggestion
- **Arrow Up**: Navigate to previous suggestion
- **Enter**: Select and apply the highlighted suggestion
- **Escape**: Close the suggestion popup

### 5. Mouse Support

- **Double-Click**: Select and apply a suggestion
- **Click**: Navigate to a suggestion (selection changes)

### 6. Smart Popup Positioning

The popup automatically adjusts its position to stay within screen bounds:
- Positions below the control by default
- Moves above if there's not enough space below
- Adjusts horizontally to stay within screen boundaries

---

## Getting Started

### Basic Setup

1. **Add the Component**

   Drag `KryptonAutoTextSuggestProvider` from the toolbox onto your form, or create it programmatically:

   ```csharp
   var provider = new KryptonAutoTextSuggestProvider();
   ```

2. **Attach to a Control**

   ```csharp
   provider.AttachedControl = myTextBox;
   ```

3. **Add Suggestions**

   ```csharp
   provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Apple"));
   provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Banana"));
   provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Cherry"));
   ```

4. **Configure (Optional)**

   ```csharp
   provider.MinCharsToShow = 2;
   provider.ShowDelay = 250;
   provider.MatchMode = KryptonAutoTextSuggestMatchMode.Contains;
   ```

### Complete Example

```csharp
using Krypton.Utilities;

public partial class MyForm : KryptonForm
{
    private KryptonAutoTextSuggestProvider _provider;
    private KryptonTextBox _textBox;

    public MyForm()
    {
        InitializeComponent();
        SetupAutoSuggest();
    }

    private void SetupAutoSuggest()
    {
        // Create provider
        _provider = new KryptonAutoTextSuggestProvider
        {
            AttachedControl = _textBox,
            MinCharsToShow = 1,
            ShowDelay = 300,
            MaxVisibleItems = 8,
            PopupWidth = 300,
            MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith
        };

        // Add suggestions
        var fruits = new[] { "Apple", "Apricot", "Avocado", "Banana", "Blueberry" };
        foreach (var fruit in fruits)
        {
            _provider.Suggestions.Add(new KryptonAutoTextSuggestItem(fruit));
        }

        // Handle selection event
        _provider.SuggestionSelected += Provider_SuggestionSelected;
    }

    private void Provider_SuggestionSelected(object? sender, KryptonAutoTextSuggestEventArgs e)
    {
        // Custom handling when a suggestion is selected
        MessageBox.Show($"Selected: {e.Item.DisplayText}");
    }
}
```

---

## API Reference

### KryptonAutoTextSuggestProvider

The main component class that provides text suggestion functionality.

#### Namespace

```csharp
namespace Krypton.Utilities;
```

#### Inheritance

```csharp
public class KryptonAutoTextSuggestProvider : Component
```

#### Constructors

##### `KryptonAutoTextSuggestProvider()`

Creates a new instance with default settings.

**Default Values:**
- `MinCharsToShow = 1`
- `ShowDelay = 300`
- `MaxVisibleItems = 8`
- `PopupWidth = 250`
- `CaseSensitive = false`
- `MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith`
- `Enabled = true`

**Example:**
```csharp
var provider = new KryptonAutoTextSuggestProvider();
```

##### `KryptonAutoTextSuggestProvider(IContainer container)`

Creates a new instance and adds it to the specified container.

**Parameters:**
- `container` (IContainer): The container to add the component to

**Example:**
```csharp
var provider = new KryptonAutoTextSuggestProvider(components);
```

#### Properties

##### `AttachedControl`

Gets or sets the control to attach suggestions to.

**Type:** `Control?`

**Category:** Behavior

**Default Value:** `null`

**Description:** The text control that will receive suggestion functionality. Supported controls include `TextBox`, `KryptonTextBox`, and `RichTextBox`.

**Example:**
```csharp
provider.AttachedControl = myKryptonTextBox;
```

**Notes:**
- Setting this property automatically detaches from any previously attached control
- Setting to `null` detaches from the current control
- The provider hooks into `TextChanged`, `KeyDown`, and `LostFocus` events

##### `Suggestions`

Gets the collection of suggestions.

**Type:** `List<KryptonAutoTextSuggestItem>`

**Category:** Data

**Description:** The list of suggestion items that will be filtered and displayed. This is a read-only property that returns the internal list, allowing you to add, remove, or modify items.

**Example:**
```csharp
// Add items
provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Apple"));
provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Banana"));

// Clear all
provider.Suggestions.Clear();

// Count items
int count = provider.Suggestions.Count;
```

##### `MinCharsToShow`

Gets or sets the minimum number of characters before showing suggestions.

**Type:** `int`

**Category:** Behavior

**Default Value:** `1`

**Description:** The minimum number of characters the user must type before the suggestion popup appears.

**Example:**
```csharp
provider.MinCharsToShow = 2; // Show suggestions only after 2 characters
```

**Notes:**
- Must be a non-negative integer
- Setting to `0` will show suggestions immediately (not recommended)
- Higher values reduce the number of suggestions shown but improve performance

##### `ShowDelay`

Gets or sets the delay in milliseconds before showing suggestions.

**Type:** `int`

**Category:** Behavior

**Default Value:** `300`

**Description:** The delay (in milliseconds) after the user stops typing before the suggestion popup appears. This prevents the popup from appearing on every keystroke.

**Example:**
```csharp
provider.ShowDelay = 500; // Wait 500ms after typing stops
```

**Notes:**
- Timer resets on each keystroke
- Lower values provide faster feedback but may cause flickering
- Recommended range: 200-500ms

##### `MaxVisibleItems`

Gets or sets the maximum number of visible items in the popup.

**Type:** `int`

**Category:** Appearance

**Default Value:** `8`

**Description:** The maximum number of suggestion items displayed in the popup at once. If more items match, the list becomes scrollable.

**Example:**
```csharp
provider.MaxVisibleItems = 10; // Show up to 10 items
```

**Notes:**
- Popup height is automatically calculated based on this value
- Higher values may make the popup too large for smaller screens
- Recommended range: 5-15 items

##### `PopupWidth`

Gets or sets the width of the popup.

**Type:** `int`

**Category:** Appearance

**Default Value:** `250`

**Description:** The width (in pixels) of the suggestion popup window.

**Example:**
```csharp
provider.PopupWidth = 400; // Wider popup for longer text
```

**Notes:**
- Popup automatically adjusts to stay within screen bounds
- Consider the length of your suggestion text when setting this value
- Minimum recommended: 200px

##### `CaseSensitive`

Gets or sets whether matching is case sensitive.

**Type:** `bool`

**Category:** Behavior

**Default Value:** `false`

**Description:** When `true`, matching is case-sensitive. When `false` (default), matching is case-insensitive.

**Example:**
```csharp
provider.CaseSensitive = true; // "Apple" won't match "apple"
```

**Notes:**
- Applies to all match modes
- Case-insensitive matching is more user-friendly for most scenarios
- Case-sensitive matching is useful for code completion scenarios

##### `MatchMode`

Gets or sets the match mode for filtering suggestions.

**Type:** `KryptonAutoTextSuggestMatchMode`

**Category:** Behavior

**Default Value:** `KryptonAutoTextSuggestMatchMode.StartsWith`

**Description:** The algorithm used to filter suggestions based on user input.

**Example:**
```csharp
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Contains;
```

**Available Modes:**
- `StartsWith`: Matches items beginning with the filter text
- `Contains`: Matches items containing the filter text anywhere
- `Fuzzy`: Matches items where pattern characters appear in order

**Notes:**
- `StartsWith` is fastest and most predictable
- `Contains` provides more flexible matching
- `Fuzzy` allows typos and partial matches (e.g., "js" matches "JavaScript")

##### `IsPopupVisible`

Gets whether the popup is currently visible.

**Type:** `bool`

**Category:** (Browsable: false)

**Description:** Returns `true` if the suggestion popup is currently visible and not disposed.

**Example:**
```csharp
if (provider.IsPopupVisible)
{
    // Popup is showing
}
```

**Notes:**
- Read-only property
- Useful for conditional logic based on popup state

##### `Enabled`

Gets or sets whether suggestions are enabled.

**Type:** `bool`

**Category:** Behavior

**Default Value:** `true`

**Description:** When `false`, the provider will not show suggestions or respond to user input. When `true`, normal operation resumes.

**Example:**
```csharp
provider.Enabled = false; // Temporarily disable suggestions
```

**Notes:**
- Setting to `false` immediately hides any visible popup
- Useful for temporarily disabling suggestions without detaching the control

#### Methods

##### `ShowSuggestions()`

Shows the suggestion popup immediately, bypassing the delay timer.

**Returns:** `void`

**Description:** Forces the suggestion popup to appear immediately, filtering suggestions based on the current text in the attached control.

**Example:**
```csharp
provider.ShowSuggestions(); // Show popup now
```

**Notes:**
- Respects `Enabled` property
- Requires `AttachedControl` to be set
- Filters based on current word at cursor position

##### `HideSuggestions()`

Hides the suggestion popup.

**Returns:** `void`

**Description:** Closes and disposes the suggestion popup if it's currently visible.

**Example:**
```csharp
provider.HideSuggestions(); // Hide popup
```

**Notes:**
- Safe to call even if popup is not visible
- Clears the internal popup reference

#### Events

##### `SuggestionSelected`

Occurs when a suggestion is selected and applied.

**Event Type:** `EventHandler<KryptonAutoTextSuggestEventArgs>`

**Category:** Action

**Description:** Raised when the user selects a suggestion (via Enter key, double-click, or programmatically). The event arguments contain the selected item and the control.

**Event Arguments:** `KryptonAutoTextSuggestEventArgs`

**Example:**
```csharp
provider.SuggestionSelected += (sender, e) =>
{
    Console.WriteLine($"Selected: {e.Item.DisplayText}");
    Console.WriteLine($"Will insert: {e.Item.InsertText}");
    Console.WriteLine($"Control: {e.Control.Name}");
    
    // Prevent default insertion
    e.Handled = true;
    
    // Custom insertion logic
    if (e.Control is KryptonTextBox textBox)
    {
        textBox.Text = e.Item.InsertText;
    }
};
```

**Notes:**
- Set `e.Handled = true` to prevent default text insertion
- Default behavior replaces the current word with `InsertText`
- Event is raised before text is applied

##### `Filtering`

Occurs when suggestions are being filtered.

**Event Type:** `EventHandler<KryptonAutoTextSuggestFilterEventArgs>`

**Category:** Action

**Description:** Raised during the filtering process, allowing you to modify the filtered suggestions list before it's displayed.

**Event Arguments:** `KryptonAutoTextSuggestFilterEventArgs`

**Example:**
```csharp
provider.Filtering += (sender, e) =>
{
    // Get the filter text
    string filter = e.FilterText;
    
    // Modify the suggestions list
    e.Suggestions = e.Suggestions
        .OrderBy(s => s.DisplayText.Length) // Sort by length
        .Take(5) // Limit to 5 items
        .ToList();
    
    // Or add custom filtering logic
    if (filter.StartsWith("special:"))
    {
        e.Suggestions.Clear();
        e.Suggestions.Add(new KryptonAutoTextSuggestItem("Special Item"));
    }
};
```

**Notes:**
- Modify `e.Suggestions` to change what's displayed
- Filtering occurs after built-in matching but before display
- Useful for custom ranking, limiting results, or adding special items

---

### KryptonAutoTextSuggestItem

Represents a single suggestion item.

#### Namespace

```csharp
namespace Krypton.Utilities;
```

#### Inheritance

```csharp
public class KryptonAutoTextSuggestItem
```

#### Properties

##### `InsertText`

Gets or sets the text to insert when this suggestion is selected.

**Type:** `string`

**Description:** The actual text that will be inserted into the control when this suggestion is selected. This may differ from `DisplayText`.

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem("C#", "C# (.NET)", "Microsoft's language");
// InsertText = "C#"
// DisplayText = "C# (.NET)"
```

##### `DisplayText`

Gets or sets the display text shown in the suggestion list.

**Type:** `string`

**Description:** The text displayed in the suggestion popup. This is what the user sees and what is used for matching.

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem("js", "JavaScript", "Web scripting language");
// DisplayText = "JavaScript" (shown in popup)
// InsertText = "js" (inserted when selected)
```

##### `Description`

Gets or sets optional description text.

**Type:** `string?`

**Description:** Optional description or tooltip text associated with this item. Currently not displayed in the default popup but available for custom implementations.

**Example:**
```csharp
item.Description = "High-level interpreted programming language";
```

##### `Tag`

Gets or sets user-defined data associated with this item.

**Type:** `object?`

**Description:** A general-purpose property for storing custom data with the suggestion item.

**Example:**
```csharp
item.Tag = new { Category = "Fruit", Color = "Red" };
```

#### Constructors

##### `KryptonAutoTextSuggestItem()`

Creates an empty item with empty strings.

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem();
item.InsertText = "Apple";
item.DisplayText = "Apple";
```

##### `KryptonAutoTextSuggestItem(string text)`

Creates an item where both `InsertText` and `DisplayText` are set to the same value.

**Parameters:**
- `text` (string): The text for both insertion and display

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem("Apple");
// InsertText = "Apple"
// DisplayText = "Apple"
```

##### `KryptonAutoTextSuggestItem(string insertText, string displayText)`

Creates an item with different insert and display text.

**Parameters:**
- `insertText` (string): The text to insert
- `displayText` (string): The text to display

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem("js", "JavaScript");
// InsertText = "js"
// DisplayText = "JavaScript"
```

##### `KryptonAutoTextSuggestItem(string insertText, string displayText, string description)`

Creates an item with insert text, display text, and description.

**Parameters:**
- `insertText` (string): The text to insert
- `displayText` (string): The text to display
- `description` (string): The description text

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem(
    "C#", 
    "C# (.NET)", 
    "Microsoft's object-oriented programming language"
);
```

#### Methods

##### `ToString()`

Returns the display text.

**Returns:** `string`

**Description:** Overrides `ToString()` to return `DisplayText`, making it easy to display items in lists or debug output.

**Example:**
```csharp
var item = new KryptonAutoTextSuggestItem("Apple");
Console.WriteLine(item); // Outputs: "Apple"
```

---

### KryptonAutoTextSuggestEventArgs

Provides data for the `SuggestionSelected` event.

#### Namespace

```csharp
namespace Krypton.Utilities;
```

#### Inheritance

```csharp
public class KryptonAutoTextSuggestEventArgs : EventArgs
```

#### Properties

##### `Item`

Gets the suggestion item that triggered the event.

**Type:** `KryptonAutoTextSuggestItem`

**Description:** The suggestion item that was selected by the user.

**Example:**
```csharp
provider.SuggestionSelected += (sender, e) =>
{
    var selectedItem = e.Item;
    string displayText = e.Item.DisplayText;
    string insertText = e.Item.InsertText;
};
```

##### `Control`

Gets the control that triggered the event.

**Type:** `Control`

**Description:** The text control where the suggestion was selected.

**Example:**
```csharp
provider.SuggestionSelected += (sender, e) =>
{
    if (e.Control is KryptonTextBox textBox)
    {
        // Handle KryptonTextBox specifically
    }
};
```

##### `Handled`

Gets or sets whether the event has been handled.

**Type:** `bool`

**Description:** Set to `true` to prevent the default text insertion behavior. When `false` (default), the provider will automatically insert the `InsertText` into the control.

**Example:**
```csharp
provider.SuggestionSelected += (sender, e) =>
{
    // Custom insertion logic
    e.Handled = true;
    
    if (e.Control is KryptonTextBox textBox)
    {
        // Custom logic here
        textBox.Text = $"Prefix: {e.Item.InsertText}";
    }
};
```

---

### KryptonAutoTextSuggestFilterEventArgs

Provides data for the `Filtering` event.

#### Namespace

```csharp
namespace Krypton.Utilities;
```

#### Inheritance

```csharp
public class KryptonAutoTextSuggestFilterEventArgs : EventArgs
```

#### Properties

##### `FilterText`

Gets the current text input being used for filtering.

**Type:** `string`

**Description:** The word at the cursor position that is being used to filter suggestions.

**Example:**
```csharp
provider.Filtering += (sender, e) =>
{
    string userInput = e.FilterText; // e.g., "app"
    
    // Custom filtering based on input
    if (userInput == "app")
    {
        // Show special suggestions
    }
};
```

##### `Suggestions`

Gets or sets the list of suggestions to display.

**Type:** `List<KryptonAutoTextSuggestItem>`

**Description:** The filtered list of suggestions. Modify this list to change what's displayed in the popup.

**Example:**
```csharp
provider.Filtering += (sender, e) =>
{
    // Sort by display text length
    e.Suggestions = e.Suggestions
        .OrderBy(s => s.DisplayText.Length)
        .ToList();
    
    // Limit to top 5
    if (e.Suggestions.Count > 5)
    {
        e.Suggestions = e.Suggestions.Take(5).ToList();
    }
    
    // Add a special item at the beginning
    e.Suggestions.Insert(0, new KryptonAutoTextSuggestItem("Custom Item"));
};
```

---

### KryptonAutoTextSuggestMatchMode

Specifies the match mode for filtering suggestions.

#### Namespace

```csharp
namespace Krypton.Utilities;
```

#### Values

##### `StartsWith`

Match items that start with the filter text.

**Description:** The default mode. Matches items where `DisplayText` begins with the filter text. Fastest and most predictable.

**Example:**
```csharp
provider.MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith;

// Filter: "app"
// Matches: "Apple", "Application", "Appetizer"
// Doesn't match: "Pineapple", "Snap"
```

##### `Contains`

Match items that contain the filter text.

**Description:** Matches items where `DisplayText` contains the filter text anywhere within it. More flexible than `StartsWith`.

**Example:**
```csharp
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Contains;

// Filter: "app"
// Matches: "Apple", "Application", "Pineapple", "Snap"
```

##### `Fuzzy`

Fuzzy match - pattern characters appear in order.

**Description:** Matches items where the pattern characters appear in order within the text, but not necessarily consecutively. Useful for handling typos and abbreviations.

**Example:**
```csharp
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Fuzzy;

// Filter: "js"
// Matches: "JavaScript", "JSX", "JSON"
// (pattern "j" then "s" appears in order)

// Filter: "py"
// Matches: "Python", "Typed", "Happy"
```

**Algorithm Details:**
- Pattern characters must appear in order
- Characters don't need to be consecutive
- Case-insensitive (unless `CaseSensitive` is `true`)

---

## Configuration

### Recommended Settings by Use Case

#### Code Completion

```csharp
provider.MinCharsToShow = 1;
provider.ShowDelay = 200;
provider.MaxVisibleItems = 10;
provider.PopupWidth = 400;
provider.CaseSensitive = true;
provider.MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith;
```

#### Search/Autocomplete

```csharp
provider.MinCharsToShow = 2;
provider.ShowDelay = 300;
provider.MaxVisibleItems = 8;
provider.PopupWidth = 300;
provider.CaseSensitive = false;
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Contains;
```

#### Flexible Matching (with typos)

```csharp
provider.MinCharsToShow = 2;
provider.ShowDelay = 400;
provider.MaxVisibleItems = 8;
provider.PopupWidth = 350;
provider.CaseSensitive = false;
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Fuzzy;
```

---

## Event Handling

### Handling Selection Events

```csharp
provider.SuggestionSelected += (sender, e) =>
{
    // Log the selection
    Console.WriteLine($"User selected: {e.Item.DisplayText}");
    
    // Access the item properties
    string insertText = e.Item.InsertText;
    string displayText = e.Item.DisplayText;
    object? tag = e.Item.Tag;
    
    // Access the control
    Control control = e.Control;
    
    // Prevent default insertion and do custom logic
    e.Handled = true;
    
    // Custom insertion
    if (control is KryptonTextBox textBox)
    {
        // Insert with formatting
        textBox.Text = $"[{insertText}]";
    }
};
```

### Custom Filtering

```csharp
provider.Filtering += (sender, e) =>
{
    string filter = e.FilterText.ToLowerInvariant();
    
    // Custom ranking: prioritize shorter matches
    e.Suggestions = e.Suggestions
        .OrderBy(s => s.DisplayText.Length)
        .ThenBy(s => s.DisplayText)
        .ToList();
    
    // Add a "Create new..." option if no exact match
    if (!e.Suggestions.Any(s => 
        s.DisplayText.Equals(filter, StringComparison.OrdinalIgnoreCase)))
    {
        e.Suggestions.Insert(0, new KryptonAutoTextSuggestItem(
            filter,
            $"Create '{filter}'...",
            "Create a new item"
        ));
    }
    
    // Limit results
    if (e.Suggestions.Count > 10)
    {
        e.Suggestions = e.Suggestions.Take(10).ToList();
    }
};
```

---

## Usage Examples

### Example 1: Simple Fruit List

```csharp
var provider = new KryptonAutoTextSuggestProvider
{
    AttachedControl = fruitTextBox,
    MinCharsToShow = 1,
    MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith
};

var fruits = new[] { "Apple", "Banana", "Cherry", "Date", "Elderberry" };
foreach (var fruit in fruits)
{
    provider.Suggestions.Add(new KryptonAutoTextSuggestItem(fruit));
}
```

### Example 2: Programming Languages with Descriptions

```csharp
var provider = new KryptonAutoTextSuggestProvider
{
    AttachedControl = languageTextBox,
    MatchMode = KryptonAutoTextSuggestMatchMode.Fuzzy
};

var languages = new[]
{
    new KryptonAutoTextSuggestItem("C#", "C# (.NET)", "Microsoft's language"),
    new KryptonAutoTextSuggestItem("js", "JavaScript", "Web scripting"),
    new KryptonAutoTextSuggestItem("py", "Python", "High-level language"),
    new KryptonAutoTextSuggestItem("java", "Java", "Object-oriented language")
};

foreach (var lang in languages)
{
    provider.Suggestions.Add(lang);
}
```

### Example 3: Dynamic Suggestions from Database

```csharp
var provider = new KryptonAutoTextSuggestProvider
{
    AttachedControl = searchTextBox,
    MinCharsToShow = 2,
    ShowDelay = 500,
    MatchMode = KryptonAutoTextSuggestMatchMode.Contains
};

// Load suggestions from database
provider.Filtering += async (sender, e) =>
{
    if (e.FilterText.Length >= 2)
    {
        // Query database
        var results = await Database.SearchAsync(e.FilterText);
        
        // Update suggestions
        e.Suggestions.Clear();
        foreach (var result in results)
        {
            e.Suggestions.Add(new KryptonAutoTextSuggestItem(
                result.Id.ToString(),
                result.Name,
                result.Description
            ));
        }
    }
};
```

### Example 4: Multiple Controls with Shared Provider

```csharp
// Create shared suggestions
var sharedSuggestions = new List<KryptonAutoTextSuggestItem>
{
    new KryptonAutoTextSuggestItem("USA", "United States"),
    new KryptonAutoTextSuggestItem("UK", "United Kingdom"),
    new KryptonAutoTextSuggestItem("CA", "Canada")
};

// Provider 1
var provider1 = new KryptonAutoTextSuggestProvider
{
    AttachedControl = countryTextBox1
};
provider1.Suggestions.AddRange(sharedSuggestions);

// Provider 2
var provider2 = new KryptonAutoTextSuggestProvider
{
    AttachedControl = countryTextBox2
};
provider2.Suggestions.AddRange(sharedSuggestions);
```

### Example 5: Conditional Suggestions Based on Context

```csharp
provider.Filtering += (sender, e) =>
{
    // Get context from form
    var category = categoryComboBox.SelectedItem?.ToString();
    
    // Filter based on category
    if (!string.IsNullOrEmpty(category))
    {
        e.Suggestions = e.Suggestions
            .Where(s => s.Tag is ItemData data && data.Category == category)
            .ToList();
    }
    
    // Add category-specific suggestions
    if (category == "Fruits")
    {
        e.Suggestions.Add(new KryptonAutoTextSuggestItem("Mango"));
    }
};
```

---

## Advanced Scenarios

### Custom Word Boundary Detection

The provider uses `IsWordChar()` to detect word boundaries. Word characters include:
- Letters (A-Z, a-z)
- Digits (0-9)
- Underscore (_)
- Period (.)

To customize word detection, you would need to modify the provider source code.

### Programmatic Selection

While there's no public method to programmatically select a suggestion, you can simulate it:

```csharp
// Find matching item
var item = provider.Suggestions.FirstOrDefault(s => 
    s.DisplayText.Equals("Apple", StringComparison.OrdinalIgnoreCase));

if (item != null)
{
    // Trigger selection event
    provider.ApplySuggestion(item);
}
```

### Dynamic Suggestion Updates

```csharp
// Update suggestions based on external events
void OnDataChanged(object sender, EventArgs e)
{
    provider.Suggestions.Clear();
    
    // Reload from source
    var newSuggestions = LoadSuggestionsFromSource();
    provider.Suggestions.AddRange(newSuggestions);
    
    // Refresh popup if visible
    if (provider.IsPopupVisible)
    {
        provider.HideSuggestions();
        provider.ShowSuggestions();
    }
}
```

### Integration with Validation

```csharp
provider.SuggestionSelected += (sender, e) =>
{
    // Validate before applying
    if (!IsValidSuggestion(e.Item))
    {
        e.Handled = true;
        MessageBox.Show("Invalid selection");
        return;
    }
    
    // Apply with validation
    if (e.Control is KryptonTextBox textBox)
    {
        textBox.Text = e.Item.InsertText;
        ValidateControl(textBox);
    }
};
```

---

## Best Practices

### 1. Performance Optimization

- **Limit Suggestions**: Keep the total number of suggestions reasonable (under 1000 items)
- **Use Appropriate Match Mode**: `StartsWith` is fastest, `Fuzzy` is slowest
- **Set Minimum Characters**: Require at least 2-3 characters before showing suggestions
- **Adjust Delay**: Balance between responsiveness and performance (200-400ms recommended)

### 2. User Experience

- **Clear Display Text**: Use descriptive `DisplayText` that users can easily recognize
- **Consistent Formatting**: Keep suggestion formatting consistent
- **Reasonable Popup Size**: Don't make the popup too large (5-10 items visible)
- **Keyboard Support**: Ensure users understand keyboard navigation (document in UI)

### 3. Code Organization

- **Separate Configuration**: Create suggestion lists in separate methods or classes
- **Use Tag Property**: Store metadata in `Tag` for custom filtering/display
- **Event Handlers**: Keep event handlers focused and avoid complex logic
- **Dispose Properly**: Ensure provider is disposed when form closes

### 4. Error Handling

```csharp
provider.SuggestionSelected += (sender, e) =>
{
    try
    {
        // Your logic here
    }
    catch (Exception ex)
    {
        // Log error
        Logger.LogError(ex);
        
        // Prevent default insertion on error
        e.Handled = true;
        
        // Show user-friendly message
        MessageBox.Show("An error occurred while applying the suggestion.");
    }
};
```

### 5. Testing

- Test with various input scenarios (empty, single char, long text)
- Test keyboard navigation thoroughly
- Test with different screen positions and sizes
- Test with rapid typing (delay behavior)
- Test with very long suggestion lists

---

## Troubleshooting

### Popup Not Appearing

**Possible Causes:**
1. `Enabled` is set to `false`
2. `AttachedControl` is not set
3. `MinCharsToShow` is greater than typed characters
4. No matching suggestions in the list
5. Control doesn't have focus

**Solutions:**
```csharp
// Check enabled state
if (!provider.Enabled) provider.Enabled = true;

// Verify attachment
if (provider.AttachedControl == null)
    provider.AttachedControl = myTextBox;

// Check minimum characters
provider.MinCharsToShow = 1; // Lower if needed

// Verify suggestions exist
if (provider.Suggestions.Count == 0)
    provider.Suggestions.Add(new KryptonAutoTextSuggestItem("Test"));

// Ensure control has focus
myTextBox.Focus();
```

### Suggestions Not Filtering Correctly

**Possible Causes:**
1. Wrong `MatchMode` for use case
2. `CaseSensitive` setting mismatch
3. `DisplayText` doesn't match expected format

**Solutions:**
```csharp
// Try different match mode
provider.MatchMode = KryptonAutoTextSuggestMatchMode.Contains;

// Check case sensitivity
provider.CaseSensitive = false; // Usually better for user input

// Verify DisplayText format
item.DisplayText = "Expected Format"; // Check this matches filter expectations
```

### Popup Position Issues

**Possible Causes:**
1. Control is near screen edge
2. Multiple monitors
3. DPI scaling issues

**Solutions:**
- The popup automatically adjusts position, but if issues persist:
- Ensure control is fully visible on screen
- Check screen bounds and DPI settings
- Consider setting a fixed `PopupWidth` that fits your screen

### Performance Issues

**Possible Causes:**
1. Too many suggestions
2. Slow filtering logic in `Filtering` event
3. `ShowDelay` too low

**Solutions:**
```csharp
// Limit suggestions
if (provider.Suggestions.Count > 1000)
{
    // Consider pagination or lazy loading
}

// Optimize Filtering event handler
provider.Filtering += (sender, e) =>
{
    // Keep logic simple and fast
    e.Suggestions = e.Suggestions.Take(20).ToList(); // Limit results
};

// Increase delay
provider.ShowDelay = 400; // Give more time for filtering
```

### Text Not Inserting

**Possible Causes:**
1. `Handled` is set to `true` in event handler
2. Control type not supported
3. Control is read-only

**Solutions:**
```csharp
// Check if event is handled
provider.SuggestionSelected += (sender, e) =>
{
    // Don't set Handled = true unless you're doing custom insertion
    // e.Handled = true; // Remove this line
};

// Verify control type
if (provider.AttachedControl is TextBox || 
    provider.AttachedControl is KryptonTextBox ||
    provider.AttachedControl is RichTextBox)
{
    // Supported
}

// Check read-only state
if (myTextBox.ReadOnly)
    myTextBox.ReadOnly = false;
```

---

## Additional Resources

### Related Classes

- `KryptonTextBox`: The primary Krypton text control
- `VisualPopup`: Base class for the suggestion popup
- `KryptonListBox`: Used internally to display suggestions

### Integration Points

The provider integrates with:
- Krypton Palette System (theming)
- Krypton View Manager (layout)
- Krypton Renderer (rendering)

### Threading Considerations

⚠️ **Important**: All operations must be performed on the UI thread. The provider is not thread-safe and should only be accessed from the thread that created it.