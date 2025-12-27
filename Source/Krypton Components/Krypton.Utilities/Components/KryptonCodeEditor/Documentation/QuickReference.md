# KryptonCodeEditor Quick Reference

## Quick Start

```csharp
var editor = new KryptonCodeEditor
{
    Language = Language.CSharp,
    Theme = EditorThemeType.Dark,
    ShowLineNumbers = true
};
editor.Text = "public class Test { }";
```

## Properties Cheat Sheet

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | `string` | `""` | Code content |
| `Language` | `Language` | `None` | Syntax highlighting language |
| `Theme` | `EditorThemeType` | `Light` | Color theme |
| `ShowLineNumbers` | `bool` | `true` | Display line numbers |
| `LineNumberMarginWidth` | `int` | `50` | Line number margin width (min 30) |
| `EnableCodeFolding` | `bool` | `false` | Enable code folding |
| `AutoCompleteEnabled` | `bool` | `true` | Enable auto-completion |
| `EditorFont` | `Font` | `Consolas, 10pt` | Editor font |
| `SelectedText` | `string` | - | Selected text |
| `SelectionStart` | `int` | - | Selection start position |
| `SelectionLength` | `int` | - | Selection length |
| `RichTextBox` | `KryptonRichTextBox` | - | Underlying control |
| `ThemeInstance` | `EditorTheme` | - | Theme customization |

## Languages

```csharp
Language.None, Language.CSharp, Language.Cpp, Language.VbNet,
Language.Xml, Language.Html, Language.Css, Language.JavaScript,
Language.TypeScript, Language.Python, Language.Rust, Language.Go,
Language.Java, Language.Php, Language.Ruby, Language.Swift,
Language.Kotlin, Language.Sql, Language.Json, Language.Yaml,
Language.Toml, Language.Markdown, Language.Batch, Language.PowerShell
```

## Themes

```csharp
EditorThemeType.Light, EditorThemeType.Dark, EditorThemeType.HighContrast,
EditorThemeType.Monokai, EditorThemeType.SolarizedLight, EditorThemeType.SolarizedDark
```

## Token Types

```csharp
TokenType.Normal, TokenType.Keyword, TokenType.String, TokenType.Comment,
TokenType.Number, TokenType.Operator, TokenType.Identifier, TokenType.Preprocessor,
TokenType.Type, TokenType.Function, TokenType.Class, TokenType.Variable,
TokenType.Constant, TokenType.Attribute, TokenType.Tag, TokenType.Meta
```

## Common Patterns

### Load File with Auto-Detection

```csharp
var ext = Path.GetExtension(filePath).ToLower();
editor.Language = ext switch
{
    ".cs" => Language.CSharp,
    ".js" => Language.JavaScript,
    ".py" => Language.Python,
    _ => Language.None
};
editor.Text = File.ReadAllText(filePath);
```

### Custom Theme

```csharp
editor.Theme = EditorThemeType.Custom;
var theme = editor.ThemeInstance;
theme.BackgroundColor = Color.FromArgb(30, 30, 30);
theme.SetTokenColor(TokenType.Keyword, Color.Cyan);
editor.Invalidate();
```

### Selection Operations

```csharp
// Select all
editor.SelectionStart = 0;
editor.SelectionLength = editor.Text.Length;

// Select line
int line = editor.RichTextBox.GetLineFromCharIndex(editor.SelectionStart);
int start = editor.RichTextBox.GetFirstCharIndexFromLine(line);
int length = editor.RichTextBox.Lines[line].Length;
editor.Select(start, length);
```

### Read-Only Viewer

```csharp
editor.RichTextBox.ReadOnly = true;
editor.ShowLineNumbers = true;
editor.EnableCodeFolding = true;
```

## Events

```csharp
editor.TextChanged += (sender, e) => { /* Handle text change */ };
```

## Keyboard Shortcuts

- **Tab**: Indent
- **Shift+Tab**: Unindent
- **Enter**: Auto-indent
- **Ctrl+A**: Select All
- **Ctrl+C/V/X**: Copy/Paste/Cut

## File Extensions Map

| Extension | Language |
|-----------|----------|
| `.cs` | CSharp |
| `.cpp`, `.h`, `.hpp` | Cpp |
| `.vb` | VbNet |
| `.js` | JavaScript |
| `.ts` | TypeScript |
| `.py` | Python |
| `.rs` | Rust |
| `.go` | Go |
| `.java` | Java |
| `.php` | PHP |
| `.rb` | Ruby |
| `.swift` | Swift |
| `.kt` | Kotlin |
| `.sql` | Sql |
| `.json` | Json |
| `.yaml`, `.yml` | Yaml |
| `.toml` | Toml |
| `.md` | Markdown |
| `.xml` | Xml |
| `.html`, `.htm` | Html |
| `.css` | Css |
| `.bat`, `.cmd` | Batch |
| `.ps1` | PowerShell |

