# KryptonCodeEditor API Summary

## Class: KryptonCodeEditor

**Namespace:** `Krypton.Utilities`  
**Inheritance:** `VisualControlBase`, `IContainedInputControl`  
**Assembly:** Krypton.Utilities

---

## Constructors

### KryptonCodeEditor()
Creates a new instance of the KryptonCodeEditor class.

**Remarks:**
- Initializes with default Consolas 10pt font
- Light theme is applied by default
- Line numbers are enabled by default
- Code folding is disabled by default
- Auto-completion is enabled by default

---

## Properties

### Text
```csharp
public override string Text { get; set; }
```
Gets or sets the code text content.

**Remarks:**
- Setting this property automatically applies syntax highlighting
- Updates the line number margin
- Raises the `TextChanged` event

---

### Language
```csharp
public Language Language { get; set; }
```
Gets or sets the programming language for syntax highlighting.

**Default:** `Language.None`

**Remarks:**
- Changing this property updates auto-complete keywords
- Reapplies syntax highlighting
- Supported languages: CSharp, Cpp, VbNet, Xml, Html, Css, JavaScript, TypeScript, Python, Rust, Go, Java, Php, Ruby, Swift, Kotlin, Sql, Json, Yaml, Toml, Markdown, Batch, PowerShell

---

### Theme
```csharp
public EditorThemeType Theme { get; set; }
```
Gets or sets the editor theme for syntax highlighting.

**Default:** `EditorThemeType.Light`

**Remarks:**
- Changing this property applies the theme immediately
- Invalidates the control to refresh display
- Available themes: Light, Dark, HighContrast, Monokai, SolarizedLight, SolarizedDark

---

### ThemeInstance
```csharp
public EditorTheme ThemeInstance { get; }
```
Gets the current editor theme instance for custom color customization.

**Remarks:**
- Read-only property
- Use `SetTokenColor()` to customize individual token colors
- Use `GetTokenColor()` to retrieve current token colors
- Modify `BackgroundColor`, `ForegroundColor`, etc. for theme customization

---

### ShowLineNumbers
```csharp
public bool ShowLineNumbers { get; set; }
```
Gets or sets whether line numbers are displayed in the margin.

**Default:** `true`

---

### LineNumberMarginWidth
```csharp
public int LineNumberMarginWidth { get; set; }
```
Gets or sets the width of the line number margin in pixels.

**Default:** `50`  
**Minimum:** `30`

---

### EnableCodeFolding
```csharp
public bool EnableCodeFolding { get; set; }
```
Gets or sets whether code folding is enabled.

**Default:** `false`

**Remarks:**
- When enabled, foldable blocks are detected automatically
- Supported languages: CSharp, Cpp, VbNet, Python
- Folding margin becomes visible when enabled

---

### AutoCompleteEnabled
```csharp
public bool AutoCompleteEnabled { get; set; }
```
Gets or sets whether auto-completion is enabled.

**Default:** `true`

**Remarks:**
- Auto-completion suggestions appear as you type
- Language-specific keywords are used
- Triggered on letter/digit/underscore characters and certain operators

---

### EditorFont
```csharp
public Font EditorFont { get; set; }
```
Gets or sets the font used for editing code.

**Default:** `new Font("Consolas", 10F)`

**Remarks:**
- Font is applied to all text in the editor
- Previous font is disposed when changed
- Recommended: Use monospace fonts (Consolas, Courier New, Fira Code)

---

### RichTextBox
```csharp
public KryptonRichTextBox RichTextBox { get; }
```
Gets the underlying KryptonRichTextBox control.

**Remarks:**
- Read-only property
- Provides access to advanced RichTextBox features
- Use for features not directly exposed by KryptonCodeEditor

---

### ContainedControl
```csharp
public Control ContainedControl { get; }
```
Gets access to the contained input control.

**Remarks:**
- Implements `IContainedInputControl` interface
- Returns the underlying RichTextBox
- Used for Krypton integration

---

### SelectedText
```csharp
public string SelectedText { get; set; }
```
Gets or sets the currently selected text.

**Remarks:**
- Setting this property replaces the current selection
- Getting returns empty string if no selection

---

### SelectionStart
```csharp
public int SelectionStart { get; set; }
```
Gets or sets the starting point of text selected in the control.

**Remarks:**
- Zero-based index
- Setting moves the cursor to the specified position
- Use with `SelectionLength` to select a range

---

### SelectionLength
```csharp
public int SelectionLength { get; set; }
```
Gets or sets the number of characters selected in the control.

**Remarks:**
- Zero if no selection
- Use with `SelectionStart` to select a range

---

## Methods

### DesignerComponentFromPoint(Point)
```csharp
public Component? DesignerComponentFromPoint(Point pt)
```
Gets a component from the given point.

**Parameters:**
- `pt`: Point in client coordinates

**Returns:** Component at the point, or null if none

**Remarks:**
- Used for designer support
- Returns null if control is disposed

---

### DesignerMouseLeave()
```csharp
public void DesignerMouseLeave()
```
Simulates the mouse leaving the control for designer support.

**Remarks:**
- Used by the Visual Studio designer
- Informs tracking elements that focus is lost

---

## Events

### TextChanged
```csharp
public event EventHandler? TextChanged;
```
Occurs when the Text property value changes.

**Remarks:**
- Raised after syntax highlighting is applied
- Fires on every text modification
- Consider debouncing for performance-critical handlers

**Example:**
```csharp
editor.TextChanged += (sender, e) =>
{
    // Handle text change
    Console.WriteLine("Text changed!");
};
```

---

## Related Classes

### EditorTheme

**Properties:**
- `BackgroundColor` - Editor background color
- `ForegroundColor` - Default text color
- `LineNumberColor` - Line number text color
- `LineNumberBackgroundColor` - Line number margin background
- `CurrentLineColor` - Current line highlight color
- `SelectionBackgroundColor` - Selected text background color

**Methods:**
- `GetTokenColor(TokenType)` - Get color for token type
- `SetTokenColor(TokenType, Color)` - Set color for token type
- `ApplyPredefinedTheme(EditorThemeType)` - Apply built-in theme

---

### Language Enumeration

**Values:**
- `None` - No syntax highlighting
- `CSharp` - C# language
- `Cpp` - C++ language
- `VbNet` - Visual Basic .NET
- `Xml` - XML markup
- `Html` - HTML markup
- `Css` - Cascading Style Sheets
- `JavaScript` - JavaScript
- `TypeScript` - TypeScript
- `Python` - Python
- `Rust` - Rust
- `Go` - Go language
- `Java` - Java
- `Php` - PHP
- `Ruby` - Ruby
- `Swift` - Swift
- `Kotlin` - Kotlin
- `Sql` - SQL
- `Json` - JSON
- `Yaml` - YAML
- `Toml` - TOML
- `Markdown` - Markdown
- `Batch` - Windows Batch
- `PowerShell` - PowerShell

---

### EditorThemeType Enumeration

**Values:**
- `Light` - Default light theme
- `Dark` - Dark theme
- `HighContrast` - High contrast theme
- `Monokai` - Monokai color scheme
- `SolarizedLight` - Solarized light theme
- `SolarizedDark` - Solarized dark theme
- `Custom` - Custom theme (use ThemeInstance)

---

### TokenType Enumeration

**Values:**
- `Normal` - Normal text
- `Keyword` - Language keywords
- `String` - String literals
- `Comment` - Comments
- `Number` - Numeric literals
- `Operator` - Operators
- `Identifier` - Identifiers
- `Preprocessor` - Preprocessor directives
- `Type` - Type names
- `Function` - Function/method names
- `Class` - Class names
- `Variable` - Variables
- `Constant` - Constants
- `Attribute` - Attributes/annotations
- `Tag` - XML/HTML tags
- `Meta` - Metadata/keys (YAML, TOML, JSON keys)

---

## Usage Patterns

### Pattern 1: Basic Editor Setup
```csharp
var editor = new KryptonCodeEditor
{
    Language = Language.CSharp,
    Theme = EditorThemeType.Dark,
    ShowLineNumbers = true
};
```

### Pattern 2: Custom Theme
```csharp
editor.Theme = EditorThemeType.Custom;
editor.ThemeInstance.SetTokenColor(TokenType.Keyword, Color.Cyan);
editor.ThemeInstance.BackgroundColor = Color.Black;
```

### Pattern 3: File Editor
```csharp
editor.Text = File.ReadAllText(filePath);
editor.Language = DetectLanguageFromExtension(filePath);
```

### Pattern 4: Read-Only Viewer
```csharp
editor.RichTextBox.ReadOnly = true;
editor.ShowLineNumbers = true;
editor.EnableCodeFolding = true;
```

---

## Performance Notes

- Syntax highlighting is debounced (300ms delay)
- Large files (>10,000 lines) may experience performance degradation
- Consider disabling code folding and auto-completion for very large files
- Use `ReadOnly` mode for viewing large files

---

## Thread Safety

**Not thread-safe.** All operations must be performed on the UI thread.

---

## See Also

- [Full Documentation](./KryptonCodeEditor.md)
- [Quick Reference](./QuickReference.md)
- [EditorTheme Class](../General/EditorTheme.cs)
- [Language Enumeration](../General/Definitions.cs)

