# KryptonCodeEditor Developer Documentation

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Quick Start](#quick-start)
4. [API Reference](#api-reference)
   - [Properties](#properties)
   - [Methods](#methods)
   - [Events](#events)
5. [Language Support](#language-support)
6. [Theme System](#theme-system)
7. [Token Types](#token-types)
8. [Usage Examples](#usage-examples)
9. [Advanced Topics](#advanced-topics)
10. [Best Practices](#best-practices)

---

## Overview

`KryptonCodeEditor` is a powerful, native Windows Forms code editor control that provides syntax highlighting, line numbering, code folding, auto-completion, and advanced editing features. Built on top of the Krypton Toolkit, it seamlessly integrates with Krypton's theming system while offering extensive customization options.

### Key Capabilities

- **Syntax Highlighting**: Support for 23+ programming languages with customizable color schemes
- **Line Numbering**: Configurable line number margin with customizable width
- **Code Folding**: Collapsible code blocks for better code organization
- **Auto-Completion**: Context-aware code completion with language-specific keywords
- **Theme Support**: 6 built-in themes plus custom theme support
- **Advanced Editing**: Auto-indentation, brace matching, and smart indentation
- **Krypton Integration**: Full integration with Krypton palette system

---

## Features

### Core Features

1. **Syntax Highlighting**
   - Real-time syntax highlighting with debounced updates (300ms)
   - Support for 23+ programming languages
   - 16 different token types for granular highlighting
   - Customizable color schemes per token type

2. **Line Numbering**
   - Toggleable line number display
   - Customizable margin width (minimum 30 pixels)
   - Synchronized scrolling with editor content

3. **Code Folding**
   - Automatic detection of foldable blocks
   - Support for C#, C++, VB.NET, and Python
   - Visual indicators in folding margin
   - Click to expand/collapse blocks

4. **Auto-Completion**
   - Language-specific keyword suggestions
   - Context-aware completion
   - Filterable completion list
   - Toggleable feature

5. **Theme System**
   - 6 built-in themes: Light, Dark, High Contrast, Monokai, Solarized Light, Solarized Dark
   - Custom theme support
   - Per-token color customization
   - Background and foreground color control

6. **Advanced Editing**
   - Auto-indentation on Enter key
   - Tab/Shift+Tab for indentation/unindentation
   - Brace matching highlighting
   - Smart indentation based on language

---

## Quick Start

### Basic Usage

```csharp
using Krypton.Utilities;

// Create editor instance
var editor = new KryptonCodeEditor
{
    Dock = DockStyle.Fill,
    Language = Language.CSharp,
    ShowLineNumbers = true,
    Theme = EditorThemeType.Dark
};

// Set code content
editor.Text = @"public class Example
{
    public void Method()
    {
        Console.WriteLine(""Hello World"");
    }
}";

// Add to form
this.Controls.Add(editor);
```

### Designer Usage

1. Drag `KryptonCodeEditor` from the toolbox onto your form
2. Set properties in the Properties window:
   - `Language`: Select the programming language
   - `Theme`: Choose a theme
   - `ShowLineNumbers`: Enable/disable line numbers
   - `EnableCodeFolding`: Enable/disable code folding
   - `AutoCompleteEnabled`: Enable/disable auto-completion

---

## API Reference

### Properties

#### Text
```csharp
public override string Text { get; set; }
```
Gets or sets the code text content. Setting this property automatically applies syntax highlighting and updates the line number margin.

**Example:**
```csharp
editor.Text = "public class Test { }";
string code = editor.Text;
```

---

#### Language
```csharp
public Language Language { get; set; }
```
Gets or sets the programming language for syntax highlighting. Changing this property updates auto-complete keywords and reapplies syntax highlighting.

**Default:** `Language.None`

**Example:**
```csharp
editor.Language = Language.CSharp;
editor.Language = Language.Python;
```

**Supported Languages:**
- `None`, `CSharp`, `Cpp`, `VbNet`, `Xml`, `Html`, `Css`, `JavaScript`, `TypeScript`, `Python`, `Rust`, `Go`, `Java`, `Php`, `Ruby`, `Swift`, `Kotlin`, `Sql`, `Json`, `Yaml`, `Toml`, `Markdown`, `Batch`, `PowerShell`

---

#### Theme
```csharp
public EditorThemeType Theme { get; set; }
```
Gets or sets the editor theme for syntax highlighting. Changing this property applies the theme and refreshes the display.

**Default:** `EditorThemeType.Light`

**Example:**
```csharp
editor.Theme = EditorThemeType.Dark;
editor.Theme = EditorThemeType.Monokai;
```

**Available Themes:**
- `Light` - Default light theme
- `Dark` - Dark theme optimized for low-light environments
- `HighContrast` - High contrast theme for accessibility
- `Monokai` - Popular Monokai color scheme
- `SolarizedLight` - Solarized light theme
- `SolarizedDark` - Solarized dark theme

---

#### ThemeInstance
```csharp
public EditorTheme ThemeInstance { get; }
```
Gets the current editor theme instance for custom color customization. Use this property to customize individual token colors.

**Example:**
```csharp
// Customize keyword color
editor.ThemeInstance.SetTokenColor(TokenType.Keyword, Color.Cyan);

// Customize background
editor.ThemeInstance.BackgroundColor = Color.FromArgb(30, 30, 30);

// Get current color
Color keywordColor = editor.ThemeInstance.GetTokenColor(TokenType.Keyword);
```

---

#### ShowLineNumbers
```csharp
public bool ShowLineNumbers { get; set; }
```
Gets or sets whether line numbers are displayed in the margin.

**Default:** `true`

**Example:**
```csharp
editor.ShowLineNumbers = true;
```

---

#### LineNumberMarginWidth
```csharp
public int LineNumberMarginWidth { get; set; }
```
Gets or sets the width of the line number margin in pixels. Minimum value is 30 pixels.

**Default:** `50`

**Example:**
```csharp
editor.LineNumberMarginWidth = 60; // Wider margin for large line numbers
```

---

#### EnableCodeFolding
```csharp
public bool EnableCodeFolding { get; set; }
```
Gets or sets whether code folding is enabled. When enabled, foldable blocks are detected and displayed in the folding margin.

**Default:** `false`

**Example:**
```csharp
editor.EnableCodeFolding = true;
```

**Supported Languages for Folding:**
- C# (classes, methods, namespaces)
- C++ (classes, functions, namespaces)
- VB.NET (Sub, Function, If, For, While blocks)
- Python (functions, classes, if/for/while blocks)

---

#### AutoCompleteEnabled
```csharp
public bool AutoCompleteEnabled { get; set; }
```
Gets or sets whether auto-completion is enabled. When enabled, suggestions appear as you type.

**Default:** `true`

**Example:**
```csharp
editor.AutoCompleteEnabled = true;
```

---

#### EditorFont
```csharp
public Font EditorFont { get; set; }
```
Gets or sets the font used for editing code. The font is applied to all text in the editor.

**Default:** `new Font("Consolas", 10F)`

**Example:**
```csharp
editor.EditorFont = new Font("Courier New", 12F);
editor.EditorFont = new Font("Fira Code", 11F); // For ligatures support
```

---

#### RichTextBox
```csharp
public KryptonRichTextBox RichTextBox { get; }
```
Gets the underlying `KryptonRichTextBox` control. Use this property to access advanced RichTextBox features not exposed by KryptonCodeEditor.

**Example:**
```csharp
// Access underlying control for advanced features
editor.RichTextBox.ReadOnly = true;
editor.RichTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
```

---

#### ContainedControl
```csharp
public Control ContainedControl { get; }
```
Gets access to the contained input control. This property implements `IContainedInputControl` interface for Krypton integration.

---

#### SelectedText
```csharp
public string SelectedText { get; set; }
```
Gets or sets the currently selected text in the editor.

**Example:**
```csharp
string selected = editor.SelectedText;
editor.SelectedText = "replacement text";
```

---

#### SelectionStart
```csharp
public int SelectionStart { get; set; }
```
Gets or sets the starting point of text selected in the control.

**Example:**
```csharp
int start = editor.SelectionStart;
editor.SelectionStart = 100; // Move cursor to position 100
```

---

#### SelectionLength
```csharp
public int SelectionLength { get; set; }
```
Gets or sets the number of characters selected in the control.

**Example:**
```csharp
// Select first 50 characters
editor.SelectionStart = 0;
editor.SelectionLength = 50;
```

---

### Methods

#### DesignerComponentFromPoint
```csharp
public Component? DesignerComponentFromPoint(Point pt)
```
Gets a component from the given point. Used for designer support.

**Parameters:**
- `pt`: Point in client coordinates

**Returns:** Component at the point, or null if none

---

#### DesignerMouseLeave
```csharp
public void DesignerMouseLeave()
```
Simulates the mouse leaving the control for designer support.

---

### Events

#### TextChanged
```csharp
public event EventHandler? TextChanged;
```
Occurs when the Text property value changes. This event is raised after syntax highlighting is applied.

**Example:**
```csharp
editor.TextChanged += (sender, e) =>
{
    Console.WriteLine("Text changed!");
    // Your code here
};
```

---

## Language Support

### Supported Languages

The editor supports syntax highlighting for the following languages:

| Language | Enum Value | File Extensions |
|----------|-----------|----------------|
| None | `Language.None` | - |
| C# | `Language.CSharp` | `.cs` |
| C++ | `Language.Cpp` | `.cpp`, `.cxx`, `.cc`, `.h`, `.hpp` |
| VB.NET | `Language.VbNet` | `.vb` |
| XML | `Language.Xml` | `.xml` |
| HTML | `Language.Html` | `.html`, `.htm` |
| CSS | `Language.Css` | `.css` |
| JavaScript | `Language.JavaScript` | `.js` |
| TypeScript | `Language.TypeScript` | `.ts` |
| Python | `Language.Python` | `.py` |
| Rust | `Language.Rust` | `.rs` |
| Go | `Language.Go` | `.go` |
| Java | `Language.Java` | `.java` |
| PHP | `Language.Php` | `.php` |
| Ruby | `Language.Ruby` | `.rb` |
| Swift | `Language.Swift` | `.swift` |
| Kotlin | `Language.Kotlin` | `.kt`, `.kts` |
| SQL | `Language.Sql` | `.sql` |
| JSON | `Language.Json` | `.json` |
| YAML | `Language.Yaml` | `.yaml`, `.yml` |
| TOML | `Language.Toml` | `.toml` |
| Markdown | `Language.Markdown` | `.md` |
| Batch | `Language.Batch` | `.bat`, `.cmd` |
| PowerShell | `Language.PowerShell` | `.ps1` |

### Language-Specific Features

#### C#
- Keywords, types, strings, comments, numbers
- Verbatim strings (`@"..."`)
- XML documentation comments (`///`)
- Attributes (`[Attribute]`)
- Preprocessor directives (`#if`, `#region`, etc.)

#### TypeScript
- Type annotations
- Decorators (`@Component`)
- Template literals
- Type keywords (`string`, `number`, `any`, etc.)

#### Rust
- Macros (`println!`)
- Lifetimes (`'a`)
- Attributes (`#[derive]`)
- String literals with raw strings (`r#"..."#`)

#### Python
- Docstrings (`"""..."""`)
- f-strings (`f"..."`)
- Type hints
- Decorators

---

## Theme System

### Built-in Themes

#### Light Theme
Default light theme with blue keywords, green strings, and gray comments.

#### Dark Theme
Dark theme optimized for low-light environments with bright colors on dark background.

#### High Contrast Theme
High contrast theme designed for accessibility with maximum contrast ratios.

#### Monokai Theme
Popular Monokai color scheme with vibrant colors.

#### Solarized Light/Dark
Solarized color palette themes with carefully selected colors for reduced eye strain.

### Custom Themes

Create custom themes by accessing the `ThemeInstance` property:

```csharp
// Set theme to Custom
editor.Theme = EditorThemeType.Custom;

// Customize colors
var theme = editor.ThemeInstance;
theme.BackgroundColor = Color.FromArgb(30, 30, 30);
theme.ForegroundColor = Color.FromArgb(212, 212, 212);
theme.SetTokenColor(TokenType.Keyword, Color.FromArgb(86, 156, 214));
theme.SetTokenColor(TokenType.String, Color.FromArgb(206, 145, 120));
theme.SetTokenColor(TokenType.Comment, Color.FromArgb(106, 153, 85));
theme.SetTokenColor(TokenType.Number, Color.FromArgb(181, 206, 168));
theme.SetTokenColor(TokenType.Type, Color.FromArgb(78, 201, 176));
theme.SetTokenColor(TokenType.Function, Color.FromArgb(220, 220, 170));

// Apply changes
editor.Invalidate();
```

### Theme Properties

The `EditorTheme` class provides the following properties:

- `BackgroundColor` - Editor background color
- `ForegroundColor` - Default text color
- `LineNumberColor` - Line number text color
- `LineNumberBackgroundColor` - Line number margin background
- `CurrentLineColor` - Current line highlight color
- `SelectionBackgroundColor` - Selected text background color

### Theme Methods

- `GetTokenColor(TokenType)` - Get color for a token type
- `SetTokenColor(TokenType, Color)` - Set color for a token type
- `ApplyPredefinedTheme(EditorThemeType)` - Apply a built-in theme

---

## Token Types

The editor recognizes 16 different token types for syntax highlighting:

| Token Type | Description | Example |
|------------|-------------|---------|
| `Normal` | Normal text | Regular code |
| `Keyword` | Language keywords | `if`, `class`, `return` |
| `String` | String literals | `"Hello World"`, `'text'` |
| `Comment` | Comments | `// comment`, `/* comment */` |
| `Number` | Numeric literals | `123`, `3.14`, `0xFF` |
| `Operator` | Operators | `+`, `-`, `==`, `&&` |
| `Identifier` | Identifiers | Variable names |
| `Preprocessor` | Preprocessor directives | `#include`, `#define` |
| `Type` | Type names | `int`, `string`, `List<T>` |
| `Function` | Function/method names | `Main()`, `GetData()` |
| `Class` | Class names | `MyClass`, `Program` |
| `Variable` | Variables | `$var`, `@instanceVar` |
| `Constant` | Constants | `true`, `false`, `null` |
| `Attribute` | Attributes/annotations | `[Serializable]`, `@Override` |
| `Tag` | XML/HTML tags | `<div>`, `<html>` |
| `Meta` | Metadata/keys | JSON keys, YAML keys, TOML keys |

---

## Usage Examples

### Example 1: Basic Code Editor

```csharp
using Krypton.Utilities;
using System.Windows.Forms;

public partial class CodeEditorForm : Form
{
    private KryptonCodeEditor editor;
    
    public CodeEditorForm()
    {
        InitializeComponent();
        
        editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill,
            Language = Language.CSharp,
            ShowLineNumbers = true,
            Theme = EditorThemeType.Dark,
            EditorFont = new Font("Consolas", 11F)
        };
        
        editor.Text = @"using System;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";
        
        Controls.Add(editor);
    }
}
```

### Example 2: File Editor with Language Detection

```csharp
public class FileEditorForm : Form
{
    private KryptonCodeEditor editor;
    private string currentFile;
    
    public FileEditorForm()
    {
        editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill,
            ShowLineNumbers = true,
            EnableCodeFolding = true,
            AutoCompleteEnabled = true
        };
        
        Controls.Add(editor);
    }
    
    public void LoadFile(string filePath)
    {
        currentFile = filePath;
        editor.Text = File.ReadAllText(filePath);
        
        // Auto-detect language from extension
        var ext = Path.GetExtension(filePath).ToLower();
        editor.Language = ext switch
        {
            ".cs" => Language.CSharp,
            ".js" => Language.JavaScript,
            ".ts" => Language.TypeScript,
            ".py" => Language.Python,
            ".rs" => Language.Rust,
            ".go" => Language.Go,
            ".java" => Language.Java,
            ".php" => Language.Php,
            ".rb" => Language.Ruby,
            ".swift" => Language.Swift,
            ".kt" => Language.Kotlin,
            ".sql" => Language.Sql,
            ".json" => Language.Json,
            ".yaml" or ".yml" => Language.Yaml,
            ".toml" => Language.Toml,
            ".md" => Language.Markdown,
            ".xml" => Language.Xml,
            ".html" or ".htm" => Language.Html,
            ".css" => Language.Css,
            ".bat" or ".cmd" => Language.Batch,
            ".ps1" => Language.PowerShell,
            _ => Language.None
        };
    }
    
    public void SaveFile()
    {
        if (!string.IsNullOrEmpty(currentFile))
        {
            File.WriteAllText(currentFile, editor.Text);
        }
    }
}
```

### Example 3: Custom Theme Editor

```csharp
public class CustomThemeEditor : Form
{
    private KryptonCodeEditor editor;
    private Button btnApplyTheme;
    
    public CustomThemeEditor()
    {
        editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill,
            Language = Language.CSharp,
            Theme = EditorThemeType.Custom
        };
        
        btnApplyTheme = new Button
        {
            Text = "Apply Custom Theme",
            Dock = DockStyle.Bottom
        };
        btnApplyTheme.Click += BtnApplyTheme_Click;
        
        Controls.Add(editor);
        Controls.Add(btnApplyTheme);
    }
    
    private void BtnApplyTheme_Click(object sender, EventArgs e)
    {
        var theme = editor.ThemeInstance;
        
        // Custom dark theme
        theme.BackgroundColor = Color.FromArgb(25, 25, 25);
        theme.ForegroundColor = Color.FromArgb(220, 220, 220);
        theme.LineNumberColor = Color.FromArgb(128, 128, 128);
        theme.LineNumberBackgroundColor = Color.FromArgb(35, 35, 35);
        
        // Token colors
        theme.SetTokenColor(TokenType.Keyword, Color.FromArgb(86, 156, 214));
        theme.SetTokenColor(TokenType.String, Color.FromArgb(206, 145, 120));
        theme.SetTokenColor(TokenType.Comment, Color.FromArgb(106, 153, 85));
        theme.SetTokenColor(TokenType.Number, Color.FromArgb(181, 206, 168));
        theme.SetTokenColor(TokenType.Type, Color.FromArgb(78, 201, 176));
        theme.SetTokenColor(TokenType.Function, Color.FromArgb(220, 220, 170));
        theme.SetTokenColor(TokenType.Class, Color.FromArgb(78, 201, 176));
        
        editor.Invalidate();
    }
}
```

### Example 4: Multi-Language Code Viewer

```csharp
public class CodeViewerForm : Form
{
    private KryptonCodeEditor editor;
    private ComboBox languageCombo;
    
    public CodeViewerForm()
    {
        editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill,
            ShowLineNumbers = true,
            ReadOnly = true // Read-only viewer
        };
        
        languageCombo = new ComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        
        // Populate languages
        languageCombo.Items.AddRange(new[]
        {
            "C#", "C++", "VB.NET", "JavaScript", "TypeScript",
            "Python", "Rust", "Go", "Java", "PHP", "Ruby",
            "Swift", "Kotlin", "SQL", "JSON", "YAML", "XML",
            "HTML", "CSS", "Markdown"
        });
        
        languageCombo.SelectedIndexChanged += LanguageCombo_SelectedIndexChanged;
        
        Controls.Add(editor);
        Controls.Add(languageCombo);
    }
    
    private void LanguageCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        editor.Language = languageCombo.SelectedItem?.ToString() switch
        {
            "C#" => Language.CSharp,
            "C++" => Language.Cpp,
            "VB.NET" => Language.VbNet,
            "JavaScript" => Language.JavaScript,
            "TypeScript" => Language.TypeScript,
            "Python" => Language.Python,
            "Rust" => Language.Rust,
            "Go" => Language.Go,
            "Java" => Language.Java,
            "PHP" => Language.Php,
            "Ruby" => Language.Ruby,
            "Swift" => Language.Swift,
            "Kotlin" => Language.Kotlin,
            "SQL" => Language.Sql,
            "JSON" => Language.Json,
            "YAML" => Language.Yaml,
            "XML" => Language.Xml,
            "HTML" => Language.Html,
            "CSS" => Language.Css,
            "Markdown" => Language.Markdown,
            _ => Language.None
        };
    }
}
```

### Example 5: Theme Switcher

```csharp
public class ThemeSwitcherForm : Form
{
    private KryptonCodeEditor editor;
    private ToolStrip toolStrip;
    
    public ThemeSwitcherForm()
    {
        editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill,
            Language = Language.CSharp
        };
        
        toolStrip = new ToolStrip();
        
        // Add theme buttons
        AddThemeButton("Light", EditorThemeType.Light);
        AddThemeButton("Dark", EditorThemeType.Dark);
        AddThemeButton("Monokai", EditorThemeType.Monokai);
        AddThemeButton("Solarized Light", EditorThemeType.SolarizedLight);
        AddThemeButton("Solarized Dark", EditorThemeType.SolarizedDark);
        AddThemeButton("High Contrast", EditorThemeType.HighContrast);
        
        Controls.Add(editor);
        Controls.Add(toolStrip);
    }
    
    private void AddThemeButton(string text, EditorThemeType themeType)
    {
        var button = new ToolStripButton(text);
        button.Click += (s, e) => editor.Theme = themeType;
        toolStrip.Items.Add(button);
    }
}
```

---

## Advanced Topics

### Performance Considerations

1. **Syntax Highlighting Debouncing**
   - Syntax highlighting is debounced with a 300ms timer
   - Large files may experience slight delays
   - Consider disabling syntax highlighting for very large files (>10,000 lines)

2. **Memory Management**
   - The editor disposes fonts properly
   - Large text content is handled efficiently
   - Consider using `ReadOnly` mode for large files

3. **Event Handling**
   - `TextChanged` event fires after syntax highlighting
   - Use debouncing for custom event handlers if needed

### Extending the Editor

#### Custom Language Support

While the editor supports 23+ languages, you can extend it by modifying the `GetTokenPatterns()` method and adding new pattern methods:

```csharp
// In KryptonCodeEditor.cs, add to GetTokenPatterns():
Language.Custom => GetCustomPatterns(),

// Add pattern method:
private List<(string Pattern, TokenType Type)> GetCustomPatterns()
{
    var keywords = @"\b(keyword1|keyword2)\b";
    var strings = @"""[^""]*""";
    var comments = @"//.*?$";
    
    return new List<(string, TokenType)>
    {
        (comments, TokenType.Comment),
        (strings, TokenType.String),
        (keywords, TokenType.Keyword)
    };
}
```

#### Custom Auto-Complete Keywords

Add custom keywords by modifying `UpdateAutoCompleteKeywords()`:

```csharp
Language.Custom => new[] { "custom", "keywords", "here" },
```

### Integration with Krypton Palette

The editor integrates with Krypton's palette system:

```csharp
// Editor automatically uses Krypton palette colors when theme is not set
// Access palette through GetResolvedPalette() in custom implementations
```

### Keyboard Shortcuts

The editor supports the following keyboard shortcuts:

- **Tab**: Indent selected lines (or insert tab if no selection)
- **Shift+Tab**: Unindent selected lines
- **Enter**: Auto-indent based on previous line
- **Ctrl+A**: Select all
- **Ctrl+C**: Copy
- **Ctrl+V**: Paste
- **Ctrl+X**: Cut
- **Ctrl+Z**: Undo (via RichTextBox)
- **Ctrl+Y**: Redo (via RichTextBox)

---

## Best Practices

### 1. Language Selection

Always set the `Language` property to match the code content:

```csharp
// Good
editor.Language = Language.CSharp;
editor.Text = csharpCode;

// Bad - syntax highlighting won't work correctly
editor.Text = csharpCode;
// Language still set to None
```

### 2. Theme Selection

Choose themes based on environment:

```csharp
// Light environments
editor.Theme = EditorThemeType.Light;

// Dark environments
editor.Theme = EditorThemeType.Dark;

// Accessibility
editor.Theme = EditorThemeType.HighContrast;
```

### 3. Font Selection

Use monospace fonts for code editing:

```csharp
// Good - monospace fonts
editor.EditorFont = new Font("Consolas", 10F);
editor.EditorFont = new Font("Courier New", 10F);
editor.EditorFont = new Font("Fira Code", 10F); // Supports ligatures

// Avoid - proportional fonts
editor.EditorFont = new Font("Arial", 10F); // Not recommended
```

### 4. Large Files

For very large files (>10,000 lines):

```csharp
// Consider disabling features for performance
editor.EnableCodeFolding = false;
editor.AutoCompleteEnabled = false;

// Or use ReadOnly mode for viewing
editor.RichTextBox.ReadOnly = true;
```

### 5. Event Handling

Handle `TextChanged` events efficiently:

```csharp
// Good - debounced handling
private Timer _saveTimer;

editor.TextChanged += (s, e) =>
{
    _saveTimer?.Stop();
    _saveTimer = new Timer { Interval = 1000 };
    _saveTimer.Tick += (s2, e2) =>
    {
        SaveFile();
        _saveTimer.Stop();
    };
    _saveTimer.Start();
};

// Avoid - immediate operations on every keystroke
editor.TextChanged += (s, e) => SaveFile(); // Too frequent!
```

### 6. Resource Cleanup

Dispose fonts when changing `EditorFont`:

```csharp
// The editor handles this automatically, but if you create fonts:
var font = new Font("Consolas", 10F);
editor.EditorFont = font;
// Font is disposed automatically when changed
```

### 7. Custom Themes

When creating custom themes, set all token colors:

```csharp
// Good - comprehensive theme
var theme = editor.ThemeInstance;
theme.SetTokenColor(TokenType.Keyword, Color.Blue);
theme.SetTokenColor(TokenType.String, Color.Green);
theme.SetTokenColor(TokenType.Comment, Color.Gray);
// ... set all token types

// Bad - incomplete theme (some tokens may be invisible)
theme.SetTokenColor(TokenType.Keyword, Color.Blue);
// Other tokens use default colors which may not be visible
```

---

## Troubleshooting

### Syntax Highlighting Not Working

1. **Check Language Property**
   ```csharp
   if (editor.Language == Language.None)
   {
       editor.Language = Language.CSharp; // Set appropriate language
   }
   ```

2. **Verify Text Content**
   ```csharp
   if (string.IsNullOrEmpty(editor.Text))
   {
       // Syntax highlighting requires text content
   }
   ```

3. **Check Theme**
   ```csharp
   // Ensure theme colors are visible
   var keywordColor = editor.ThemeInstance.GetTokenColor(TokenType.Keyword);
   // Should not match background color
   ```

### Performance Issues

1. **Large Files**: Disable code folding and auto-completion
2. **Frequent Updates**: Use debouncing for TextChanged events
3. **Memory**: Consider ReadOnly mode for large files

### Theme Not Applying

1. **Invalidate Control**: Call `editor.Invalidate()` after theme changes
2. **Check ThemeInstance**: Verify `ThemeInstance` is not null
3. **Reapply Syntax Highlighting**: Set `Language` property again

---

## Version History

- **2026**: Initial release with 23+ language support, 6 themes, and advanced features

---

## License

This component is part of the Krypton Toolkit Suite and is licensed under the New BSD 3-Clause License.

---

## Support

For issues, feature requests, or contributions, please visit the [Krypton Suite GitHub Repository](https://github.com/Krypton-Suite/Standard-Toolkit).

---

## Related Documentation

- [Krypton Toolkit Documentation](https://krypton-suite.github.io/Standard-Toolkit-Online-Help/)
- [EditorTheme Class Documentation](./EditorTheme.md)
- [Language Enumeration](./Definitions.md#language-enum)
- [TokenType Enumeration](./Definitions.md#tokentype-enum)

