# KryptonPrintDocument

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Properties](#properties)
5. [Methods](#methods)
6. [Events](#events)
7. [Theming and Palette Integration](#theming-and-palette-integration)
8. [Usage Examples](#usage-examples)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)
11. [Related Components](#related-components)

---

## Overview

The `KryptonPrintDocument` extends the standard .NET `PrintDocument` class with full Krypton theming support. It provides helper methods and properties that allow you to easily print documents using colors, fonts, and styles from the current Krypton palette, ensuring your printed output matches your application's visual theme.

### Key Features

- **Full Palette Integration**: Automatically uses colors from the current Krypton palette
- **Themed Printing Helpers**: Convenient methods for drawing with palette colors
- **Global Palette Support**: Automatically updates when the global palette changes
- **Custom Palette Support**: Can use a specific custom palette
- **Style Customization**: Configurable text and background styles
- **Backward Compatible**: Inherits from `PrintDocument`, works with all existing printing code
- **Toolbox Item**: Available in Visual Studio toolbox

### Supported Platforms

- .NET Framework 4.7.2 and later
- .NET 8.0 Windows and later
- All target frameworks supported by Krypton Toolkit

---

## Quick Start

### Basic Usage

```csharp
using System.Drawing;
using System.Drawing.Printing;
using Krypton.Toolkit;

// Create a KryptonPrintDocument
var printDoc = new KryptonPrintDocument
{
    DocumentName = "My Document"
};

// Handle PrintPage event
printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Get themed colors
    var textColor = printDoc.GetTextColor();
    var backColor = printDoc.GetBackgroundColor();
    var font = printDoc.GetFont();

    // Draw with themed colors
    using (var brush = new SolidBrush(backColor))
    {
        g.FillRectangle(brush, marginBounds);
    }

    using (var brush = new SolidBrush(textColor))
    {
        g.DrawString("Hello, Themed Printing!", font, brush, 100, 100);
    }

    e.HasMorePages = false;
};

// Use with KryptonPrintPreviewDialog
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDoc
};
previewDialog.ShowDialog();
```

### Using Helper Methods

```csharp
var printDoc = new KryptonPrintDocument();

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Draw themed rectangle
    var rect = new Rectangle(50, 50, 200, 100);
    printDoc.DrawThemedRectangle(g, rect);

    // Draw themed text
    printDoc.DrawThemedText(g, "Themed Text", null, 
        new Rectangle(60, 60, 180, 80));

    e.HasMorePages = false;
};
```

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

### Class Declaration

```csharp
[ToolboxItem(true)]
[ToolboxBitmap(typeof(PrintDocument), "ToolboxBitmaps.KryptonPrintDocument.bmp")]
[DefaultProperty(nameof(DocumentName))]
[DesignerCategory(@"code")]
[Description(@"Represents a document to be printed with Krypton theming support.")]
public class KryptonPrintDocument : PrintDocument
```

### Inheritance Hierarchy

```
System.Object
  └─ System.MarshalByRefObject
      └─ System.ComponentModel.Component
          └─ System.Drawing.Printing.PrintDocument
              └─ Krypton.Toolkit.KryptonPrintDocument
```

---

## Properties

### PaletteMode

Gets or sets the palette mode to use for printing.

```csharp
[Category(@"Visuals")]
[Description(@"Palette applied to printing.")]
public PaletteMode PaletteMode { get; set; }
```

**Default**: `PaletteMode.Global`

**Values**:
- `PaletteMode.Global`: Uses the current global palette (default)
- `PaletteMode.Custom`: Uses a custom palette (requires `Palette` property to be set)
- `PaletteMode.Office2013Blue`, `PaletteMode.Microsoft365Blue`, etc.: Use specific built-in palettes

**Example**:
```csharp
// Use global palette (default)
printDoc.PaletteMode = PaletteMode.Global;

// Use specific theme
printDoc.PaletteMode = PaletteMode.Office2013Blue;

// Use custom palette
printDoc.PaletteMode = PaletteMode.Custom;
printDoc.Palette = myCustomPalette;
```

### Palette

Gets or sets a custom palette implementation.

```csharp
[Category(@"Visuals")]
[Description(@"Custom palette applied to printing.")]
[DefaultValue(null)]
public PaletteBase? Palette { get; set; }
```

**Use Case**: Apply a specific custom palette to printing.

**Example**:
```csharp
var customPalette = new KryptonCustomPaletteBase();
// Configure custom palette...

printDoc.Palette = customPalette;
printDoc.PaletteMode = PaletteMode.Custom;
```

### UsePaletteColors

Gets or sets a value indicating whether to use palette colors when printing.

```csharp
[Category(@"Behavior")]
[Description(@"Indicates whether to use palette colors when printing.")]
[DefaultValue(true)]
public bool UsePaletteColors { get; set; }
```

**Default**: `true`

**Behavior**:
- When `true`: All helper methods return palette colors
- When `false`: Helper methods return default colors (Black for text, White for background)

**Example**:
```csharp
// Disable theming (use standard colors)
printDoc.UsePaletteColors = false;
```

### TextStyle

Gets or sets the text style used for retrieving text colors from the palette.

```csharp
[Category(@"Visuals")]
[Description(@"Text style used for retrieving text colors from the palette.")]
[DefaultValue(PaletteContentStyle.LabelNormalPanel)]
public PaletteContentStyle TextStyle { get; set; }
```

**Default**: `PaletteContentStyle.LabelNormalPanel`

**Common Values**:
- `PaletteContentStyle.LabelNormalPanel`: Standard label text
- `PaletteContentStyle.LabelBoldPanel`: Bold label text
- `PaletteContentStyle.HeaderPrimaryPanel`: Header text
- `PaletteContentStyle.ButtonStandalone`: Button text style

**Example**:
```csharp
// Use header style for titles
printDoc.TextStyle = PaletteContentStyle.HeaderPrimaryPanel;
```

### BackgroundStyle

Gets or sets the background style used for retrieving background colors from the palette.

```csharp
[Category(@"Visuals")]
[Description(@"Background style used for retrieving background colors from the palette.")]
[DefaultValue(PaletteBackStyle.PanelClient)]
public PaletteBackStyle BackgroundStyle { get; set; }
```

**Default**: `PaletteBackStyle.PanelClient`

**Common Values**:
- `PaletteBackStyle.PanelClient`: Standard panel background
- `PaletteBackStyle.PanelAlternate`: Alternate panel background
- `PaletteBackStyle.ButtonStandalone`: Button background style

**Example**:
```csharp
// Use alternate panel style
printDoc.BackgroundStyle = PaletteBackStyle.PanelAlternate;
```

### Standard PrintDocument Properties

`KryptonPrintDocument` inherits all standard `PrintDocument` properties:

- `DocumentName`: Name of the document
- `PrinterSettings`: Printer settings
- `DefaultPageSettings`: Default page settings
- `OriginAtMargins`: Whether to use margins as origin
- `PrintController`: Print controller

---

## Methods

### Color Retrieval Methods

#### GetTextColor

Gets the text color from the current palette.

```csharp
public Color GetTextColor(PaletteState state = PaletteState.Normal)
```

**Parameters**:
- `state`: The palette state to use (Normal, Disabled, etc.)

**Returns**: The text color from the palette, or `Color.Black` if palette is not available.

**Example**:
```csharp
var textColor = printDoc.GetTextColor();
using var brush = new SolidBrush(textColor);
g.DrawString("Text", font, brush, 100, 100);
```

#### GetBackgroundColor

Gets the background color from the current palette.

```csharp
public Color GetBackgroundColor(PaletteState state = PaletteState.Normal)
```

**Returns**: The background color from the palette, or `Color.White` if palette is not available.

**Example**:
```csharp
var backColor = printDoc.GetBackgroundColor();
using var brush = new SolidBrush(backColor);
g.FillRectangle(brush, bounds);
```

#### GetBorderColor

Gets the border color from the current palette.

```csharp
public Color GetBorderColor(PaletteState state = PaletteState.Normal)
```

**Returns**: The border color from the palette, or `Color.Black` if palette is not available.

**Example**:
```csharp
var borderColor = printDoc.GetBorderColor();
using var pen = new Pen(borderColor);
g.DrawRectangle(pen, bounds);
```

#### GetFont

Gets the font from the current palette.

```csharp
public Font GetFont(PaletteState state = PaletteState.Normal)
```

**Returns**: The font from the palette, or a default Arial 12pt font if palette is not available.

**Example**:
```csharp
var font = printDoc.GetFont();
g.DrawString("Text", font, brush, 100, 100);
```

### Drawing Helper Methods

#### DrawThemedText

Draws text using palette colors.

```csharp
public void DrawThemedText(Graphics graphics, string text, Font? font, Rectangle bounds, 
    StringFormat? format = null, PaletteState state = PaletteState.Normal)
```

**Parameters**:
- `graphics`: The Graphics object to draw on
- `text`: The text to draw
- `font`: The font to use (if null, uses palette font)
- `bounds`: The bounding rectangle for the text
- `format`: The StringFormat to use (optional)
- `state`: The palette state to use (optional)

**Example**:
```csharp
printDoc.DrawThemedText(g, "Hello, World!", null, 
    new Rectangle(100, 100, 200, 50));
```

#### DrawThemedRectangle

Draws a rectangle with themed background and border.

```csharp
public void DrawThemedRectangle(Graphics graphics, Rectangle bounds, 
    PaletteState state = PaletteState.Normal)
```

**Parameters**:
- `graphics`: The Graphics object to draw on
- `bounds`: The bounding rectangle
- `state`: The palette state to use (optional)

**Example**:
```csharp
var rect = new Rectangle(50, 50, 200, 100);
printDoc.DrawThemedRectangle(g, rect);
```

#### DrawThemedLine

Draws a line using the border color from the palette.

```csharp
public void DrawThemedLine(Graphics graphics, int x1, int y1, int x2, int y2, 
    PaletteState state = PaletteState.Normal)
```

**Example**:
```csharp
printDoc.DrawThemedLine(g, 0, 100, 500, 100);
```

### Brush and Pen Helpers

#### GetTextBrush

Gets a `SolidBrush` with the text color from the palette.

```csharp
public Brush GetTextBrush(PaletteState state = PaletteState.Normal)
```

**Returns**: A `SolidBrush` with the palette text color.

**Note**: You are responsible for disposing the brush.

**Example**:
```csharp
using var brush = printDoc.GetTextBrush();
g.DrawString("Text", font, brush, 100, 100);
```

#### GetBackgroundBrush

Gets a `SolidBrush` with the background color from the palette.

```csharp
public Brush GetBackgroundBrush(PaletteState state = PaletteState.Normal)
```

**Example**:
```csharp
using var brush = printDoc.GetBackgroundBrush();
g.FillRectangle(brush, bounds);
```

#### GetBorderPen

Gets a `Pen` with the border color from the palette.

```csharp
public Pen GetBorderPen(PaletteState state = PaletteState.Normal, float width = 1.0f)
```

**Parameters**:
- `state`: The palette state to use
- `width`: The pen width (default: 1.0f)

**Example**:
```csharp
using var pen = printDoc.GetBorderPen(width: 2.0f);
g.DrawRectangle(pen, bounds);
```

---

## Events

### PaletteChanged

Occurs when the palette changes.

```csharp
[Category(@"Property Changed")]
[Description(@"Occurs when the value of the Palette property is changed.")]
public event EventHandler? PaletteChanged;
```

**Example**:
```csharp
printDoc.PaletteChanged += (sender, e) =>
{
    Console.WriteLine("Palette changed - colors updated");
};
```

### Standard PrintDocument Events

`KryptonPrintDocument` inherits all standard `PrintDocument` events:

- `PrintPage`: Fired for each page to print
- `BeginPrint`: Fired when printing begins
- `EndPrint`: Fired when printing ends
- `QueryPageSettings`: Fired to query page settings

---

## Theming and Palette Integration

### Global Palette

The control automatically uses the current global palette:

```csharp
// Change global theme
KryptonManager.GlobalPaletteMode = PaletteMode.Office2013Blue;

// All KryptonPrintDocument instances automatically update
```

### Custom Palette

Apply a custom palette to a specific document:

```csharp
var customPalette = new KryptonCustomPaletteBase();
// Configure custom palette...

printDoc.Palette = customPalette;
printDoc.PaletteMode = PaletteMode.Custom;
```

### Palette Modes

```csharp
// Use global palette (default)
printDoc.PaletteMode = PaletteMode.Global;

// Use specific theme
printDoc.PaletteMode = PaletteMode.Microsoft365Blue;
printDoc.PaletteMode = PaletteMode.VisualStudio2019Dark;
printDoc.PaletteMode = PaletteMode.Office2013Blue;
// ... and many more
```

### Style Customization

Customize which palette styles are used:

```csharp
// Use header style for text
printDoc.TextStyle = PaletteContentStyle.HeaderPrimaryPanel;

// Use alternate panel style for backgrounds
printDoc.BackgroundStyle = PaletteBackStyle.PanelAlternate;
```

---

## Usage Examples

### Example 1: Basic Themed Printing

```csharp
using System.Drawing;
using System.Drawing.Printing;
using Krypton.Toolkit;

var printDoc = new KryptonPrintDocument
{
    DocumentName = "Themed Document",
    PaletteMode = PaletteMode.Global
};

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Fill background with themed color
    var backColor = printDoc.GetBackgroundColor();
    using (var brush = new SolidBrush(backColor))
    {
        g.FillRectangle(brush, marginBounds);
    }

    // Draw text with themed color
    var textColor = printDoc.GetTextColor();
    var font = printDoc.GetFont();
    using (var brush = new SolidBrush(textColor))
    {
        g.DrawString("Themed Document", font, brush, 
            marginBounds.Left + 100, marginBounds.Top + 100);
    }

    e.HasMorePages = false;
};

// Preview
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDoc
};
previewDialog.ShowDialog();
```

### Example 2: Using Helper Methods

```csharp
var printDoc = new KryptonPrintDocument();

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Draw themed header rectangle
    var headerRect = new Rectangle(marginBounds.Left, marginBounds.Top, 
        marginBounds.Width, 80);
    printDoc.DrawThemedRectangle(g, headerRect);

    // Draw header text
    printDoc.DrawThemedText(g, "Document Title", null, headerRect,
        new StringFormat { Alignment = StringAlignment.Center, 
            LineAlignment = StringAlignment.Center });

    // Draw content
    var contentRect = new Rectangle(marginBounds.Left, marginBounds.Top + 100,
        marginBounds.Width, marginBounds.Height - 100);
    printDoc.DrawThemedText(g, "Document content goes here...", null, contentRect);

    e.HasMorePages = false;
};
```

### Example 3: Multi-Page Document

```csharp
private int _currentPage = 0;
private readonly List<string> _pages = new() { "Page 1", "Page 2", "Page 3" };

var printDoc = new KryptonPrintDocument
{
    DocumentName = "Multi-Page Document"
};

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Draw page background
    var backColor = printDoc.GetBackgroundColor();
    using (var brush = new SolidBrush(backColor))
    {
        g.FillRectangle(brush, marginBounds);
    }

    // Draw page content
    if (_currentPage < _pages.Count)
    {
        var textColor = printDoc.GetTextColor();
        var font = printDoc.GetFont();
        using (var brush = new SolidBrush(textColor))
        {
            g.DrawString(_pages[_currentPage], font, brush, 
                marginBounds.Left + 100, marginBounds.Top + 100);
        }

        _currentPage++;
        e.HasMorePages = _currentPage < _pages.Count;
    }
    else
    {
        e.HasMorePages = false;
    }
};

// Reset for preview
_currentPage = 0;
var previewDialog = new KryptonPrintPreviewDialog { Document = printDoc };
previewDialog.ShowDialog();
```

### Example 4: Custom Style Configuration

```csharp
var printDoc = new KryptonPrintDocument
{
    PaletteMode = PaletteMode.Office2013Blue,
    TextStyle = PaletteContentStyle.HeaderPrimaryPanel,  // Use header style
    BackgroundStyle = PaletteBackStyle.PanelAlternate     // Use alternate background
};

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Background uses PanelAlternate style
    var backColor = printDoc.GetBackgroundColor();
    using (var brush = new SolidBrush(backColor))
    {
        g.FillRectangle(brush, marginBounds);
    }

    // Text uses HeaderPrimaryPanel style
    var textColor = printDoc.GetTextColor();
    var font = printDoc.GetFont(); // Font from header style
    using (var brush = new SolidBrush(textColor))
    {
        g.DrawString("Header Style Text", font, brush, 100, 100);
    }

    e.HasMorePages = false;
};
```

### Example 5: Themed Tables and Grids

```csharp
var printDoc = new KryptonPrintDocument();

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    var marginBounds = e.MarginBounds;

    // Draw table header
    var headerRect = new Rectangle(marginBounds.Left, marginBounds.Top, 
        marginBounds.Width, 40);
    printDoc.DrawThemedRectangle(g, headerRect);
    printDoc.DrawThemedText(g, "Column 1\tColumn 2\tColumn 3", null, headerRect);

    // Draw table rows
    var rowHeight = 30;
    for (int i = 0; i < 5; i++)
    {
        var rowRect = new Rectangle(marginBounds.Left, 
            marginBounds.Top + 40 + (i * rowHeight), 
            marginBounds.Width, rowHeight);
        
        // Alternate row backgrounds
        var backColor = (i % 2 == 0) 
            ? printDoc.GetBackgroundColor() 
            : printDoc.GetBackgroundColor(PaletteState.Normal);
        
        using (var brush = new SolidBrush(backColor))
        {
            g.FillRectangle(brush, rowRect);
        }

        // Draw row border
        printDoc.DrawThemedLine(g, rowRect.Left, rowRect.Bottom, 
            rowRect.Right, rowRect.Bottom);

        // Draw row text
        printDoc.DrawThemedText(g, $"Row {i + 1} Col1\tCol2\tCol3", null, rowRect);
    }

    e.HasMorePages = false;
};
```

### Example 6: Disabling Theming

```csharp
var printDoc = new KryptonPrintDocument
{
    UsePaletteColors = false  // Use standard colors
};

printDoc.PrintPage += (sender, e) =>
{
    var g = e.Graphics!;
    
    // These will return standard colors (Black, White)
    var textColor = printDoc.GetTextColor();      // Returns Color.Black
    var backColor = printDoc.GetBackgroundColor(); // Returns Color.White
    
    // Standard printing code...
};
```

---

## Best Practices

### 1. Palette Mode Selection

Use `PaletteMode.Global` for consistency across your application:

```csharp
var printDoc = new KryptonPrintDocument
{
    PaletteMode = PaletteMode.Global  // Matches application theme
};
```

### 2. Style Selection

Choose appropriate styles for different content types:

```csharp
// For headers
printDoc.TextStyle = PaletteContentStyle.HeaderPrimaryPanel;

// For body text
printDoc.TextStyle = PaletteContentStyle.LabelNormalPanel;

// For backgrounds
printDoc.BackgroundStyle = PaletteBackStyle.PanelClient;
```

### 3. Resource Management

Always dispose of brushes and pens:

```csharp
// Good: Using statement
using var brush = printDoc.GetTextBrush();
g.DrawString("Text", font, brush, 100, 100);

// Also good: Manual disposal
var brush = printDoc.GetTextBrush();
g.DrawString("Text", font, brush, 100, 100);
brush.Dispose();
```

### 4. Error Handling

Handle potential palette errors gracefully:

```csharp
printDoc.PrintPage += (sender, e) =>
{
    try
    {
        var textColor = printDoc.GetTextColor();
        // Use color...
    }
    catch
    {
        // Fallback to standard color
        using var brush = new SolidBrush(Color.Black);
        // Draw with fallback...
    }
};
```

### 5. Performance

For large documents, cache colors:

```csharp
private Color? _cachedTextColor;
private Color? _cachedBackColor;

printDoc.PrintPage += (sender, e) =>
{
    // Cache colors on first page
    _cachedTextColor ??= printDoc.GetTextColor();
    _cachedBackColor ??= printDoc.GetBackgroundColor();

    // Use cached colors for all pages
    using var textBrush = new SolidBrush(_cachedTextColor.Value);
    // ...
};
```

### 6. Integration with KryptonPrintPreviewControl

Use `KryptonPrintDocument` with `KryptonPrintPreviewControl` for a fully themed experience:

```csharp
var printDoc = new KryptonPrintDocument
{
    PaletteMode = PaletteMode.Global
};

var previewControl = new KryptonPrintPreviewControl
{
    Document = printDoc,
    PanelBackStyle = PaletteBackStyle.PanelAlternate
};
```

---

## Troubleshooting

### Issue: Colors Not Themed

**Symptoms**: Helper methods return default colors instead of palette colors.

**Solutions**:
1. Verify `UsePaletteColors` is `true`
2. Check that `PaletteMode` is set correctly
3. Ensure global palette is set if using `PaletteMode.Global`

```csharp
// Debug: Check settings
Console.WriteLine($"UsePaletteColors: {printDoc.UsePaletteColors}");
Console.WriteLine($"PaletteMode: {printDoc.PaletteMode}");
Console.WriteLine($"Global Palette: {KryptonManager.CurrentGlobalPalette != null}");
```

### Issue: Wrong Colors

**Symptoms**: Colors don't match expected theme.

**Solutions**:
1. Check `TextStyle` and `BackgroundStyle` properties
2. Verify palette mode matches application theme
3. Test with different style values

```csharp
// Try different styles
printDoc.TextStyle = PaletteContentStyle.LabelNormalPanel;
printDoc.BackgroundStyle = PaletteBackStyle.PanelClient;
```

### Issue: Font Too Large/Small

**Symptoms**: Font from palette is inappropriate for printing.

**Solutions**:
1. Use custom font instead of palette font
2. Scale the palette font
3. Use `TextStyle` that provides appropriate font size

```csharp
// Use custom font
var customFont = new Font("Arial", 10);
printDoc.DrawThemedText(g, "Text", customFont, bounds);

// Or scale palette font
var paletteFont = printDoc.GetFont();
var scaledFont = new Font(paletteFont.FontFamily, paletteFont.Size * 0.8f);
```

### Issue: Palette Not Updating

**Symptoms**: Colors don't change when global palette changes.

**Solutions**:
1. Ensure `PaletteMode` is `Global`
2. Check that `PaletteChanged` event is handled if needed
3. Verify `KryptonManager.GlobalPaletteChanged` is firing

```csharp
// Subscribe to palette changes
printDoc.PaletteChanged += (sender, e) =>
{
    // Invalidate or regenerate document if needed
};
```

---

## Related Components

### KryptonPrintPreviewControl

Preview documents with full theming support.

**See**: [KryptonPrintPreviewControl.md](./KryptonPrintPreviewControl.md)

### KryptonPrintPreviewDialog

Complete print preview dialog with integrated toolbar.

**See**: [print-preview-dialog-feature.md](./print-preview-dialog-feature.md)

### KryptonPrintDialog

Themed print dialog for printer selection and settings.

**Namespace**: `Krypton.Toolkit`

---

## Summary

The `KryptonPrintDocument` provides a seamless way to print documents using Krypton's theming system. It extends the standard `PrintDocument` with palette integration, making it easy to create printed output that matches your application's visual theme.

Key benefits:
- **Consistent Theming**: Printed documents match your application's appearance
- **Easy to Use**: Simple helper methods for common printing tasks
- **Flexible**: Supports global, custom, and specific palette modes
- **Backward Compatible**: Works with all existing printing code
