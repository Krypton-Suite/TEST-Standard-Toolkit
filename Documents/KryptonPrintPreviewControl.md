# KryptonPrintPreviewControl

## Table of Contents

1. [Overview](#overview)
2. [Two Implementations](#two-implementations)
3. [Quick Start](#quick-start)
4. [API Reference](#api-reference)
5. [Properties](#properties)
6. [Methods](#methods)
7. [Events](#events)
8. [Theming and Appearance](#theming-and-appearance)
9. [Usage Examples](#usage-examples)
10. [Advanced Features](#advanced-features)
11. [Performance Considerations](#performance-considerations)
12. [Best Practices](#best-practices)
13. [Troubleshooting](#troubleshooting)
14. [Related Components](#related-components)

---

## Overview

The `KryptonPrintPreviewControl` provides a print preview control with full Krypton theming support. It displays a `PrintDocument` in a preview format, allowing users to see how their document will look when printed before actually printing it.

### Key Features

- **Full Krypton Theming**: Complete integration with Krypton's palette system
- **Multiple Page Layouts**: Display 1, 2, 3, 4, or 6 pages simultaneously
- **Zoom Control**: Adjustable zoom levels from 10% to 500%
- **Auto-Zoom**: Automatic zoom calculation to fit pages in the viewport
- **Anti-Aliasing**: Configurable anti-aliasing for smoother page rendering
- **Page Navigation**: Navigate through multi-page documents
- **Themed Backgrounds**: Control and page backgrounds use palette colors
- **Designer Support**: Full Visual Studio designer integration
- **Toolbox Item**: Available in the Visual Studio toolbox

### Supported Platforms

- .NET Framework 4.7.2 and later
- .NET 8.0 Windows and later
- All target frameworks supported by Krypton Toolkit

---

## Two Implementations

The Krypton Toolkit provides **two implementations** of `KryptonPrintPreviewControl`, each with different capabilities:

### 1. Krypton.Toolkit.KryptonPrintPreviewControl

**Location**: `Krypton.Toolkit` namespace  
**Base Class**: `VisualControlBase`  
**Implementation**: Wrapper around standard `PrintPreviewControl`

**Characteristics**:
- Wraps the standard WinForms `PrintPreviewControl`
- Provides themed container/background
- Full API compatibility with standard `PrintPreviewControl`
- Page backgrounds remain white (standard control limitation)
- Better performance (uses native rendering)
- Recommended for: Standard use cases, maximum compatibility

**Namespace**:
```csharp
using Krypton.Toolkit;
```

### 2. Krypton.Utilities.KryptonPrintPreviewControl

**Location**: `Krypton.Utilities` namespace  
**Base Class**: `VisualPanel`  
**Implementation**: Full custom rendering

**Characteristics**:
- Complete custom implementation from scratch
- Full control over page rendering
- Themed page backgrounds (uses `PanelAlternate` style by default)
- Custom page shadows and borders
- Mouse wheel zoom (Ctrl+Wheel)
- Panning support (Middle mouse button or Shift+Left mouse)
- More customization options
- Recommended for: Advanced theming needs, custom page backgrounds

**Namespace**:
```csharp
using Krypton.Utilities;
```

---

## Quick Start

### Basic Usage (Krypton.Toolkit)

```csharp
using System.Drawing.Printing;
using Krypton.Toolkit;

// Create a PrintDocument
var printDocument = new PrintDocument();
printDocument.PrintPage += (sender, e) =>
{
    var font = new Font("Arial", 12);
    e.Graphics!.DrawString("Hello, Krypton Print Preview!", font, Brushes.Black, 100, 100);
};

// Create the preview control
var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument,
    Dock = DockStyle.Fill,
    Zoom = 0.5,
    Columns = 1,
    Rows = 1
};

// Add to form
form.Controls.Add(previewControl);
```

### Basic Usage (Krypton.Utilities)

```csharp
using System.Drawing.Printing;
using Krypton.Utilities;

// Create a PrintDocument
var printDocument = new PrintDocument();
printDocument.PrintPage += (sender, e) =>
{
    var font = new Font("Arial", 12);
    e.Graphics!.DrawString("Hello, Krypton Print Preview!", font, Brushes.Black, 100, 100);
};

// Create the preview control with full theming
var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument,
    Dock = DockStyle.Fill,
    Zoom = 0.5,
    Columns = 1,
    Rows = 1,
    PageBackStyle = PaletteBackStyle.PanelAlternate  // Themed page backgrounds
};

// Add to form
form.Controls.Add(previewControl);
```

---

## API Reference

### Krypton.Toolkit.KryptonPrintPreviewControl

#### Class Declaration

```csharp
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPrintPreviewControl), "ToolboxBitmaps.KryptonPrintPreviewControl.bmp")]
[DefaultEvent(nameof(StartPageChanged))]
[DefaultProperty(nameof(Document))]
[DesignerCategory(@"code")]
[Description(@"Displays a PrintDocument in a preview format with Krypton theming.")]
public class KryptonPrintPreviewControl : VisualControlBase
```

#### Inheritance Hierarchy

```
System.Object
  └─ System.MarshalByRefObject
      └─ System.ComponentModel.Component
          └─ System.Windows.Forms.Control
              └─ Krypton.Toolkit.VisualControlBase
                  └─ Krypton.Toolkit.KryptonPrintPreviewControl
```

### Krypton.Utilities.KryptonPrintPreviewControl

#### Class Declaration

```csharp
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPrintPreviewControl), "ToolboxBitmaps.KryptonPrintPreviewControl.bmp")]
[DefaultEvent(nameof(StartPageChanged))]
[DefaultProperty(nameof(Document))]
[DesignerCategory(@"code")]
[Description(@"Displays a PrintDocument in a preview format with full Krypton theming support.")]
public class KryptonPrintPreviewControl : VisualPanel
```

#### Inheritance Hierarchy

```
System.Object
  └─ System.MarshalByRefObject
      └─ System.ComponentModel.Component
          └─ System.Windows.Forms.Control
              └─ System.Windows.Forms.Panel
                  └─ Krypton.Toolkit.VisualPanel
                      └─ Krypton.Utilities.KryptonPrintPreviewControl
```

---

## Properties

### Common Properties (Both Implementations)

#### Document

Gets or sets the `PrintDocument` to preview.

```csharp
[Category(@"Behavior")]
[Description(@"The PrintDocument to preview.")]
[DefaultValue(null)]
public PrintDocument? Document { get; set; }
```

**Example**:
```csharp
var previewControl = new KryptonPrintPreviewControl();
previewControl.Document = myPrintDocument;
```

#### Columns

Gets or sets the number of pages displayed horizontally across the page.

```csharp
[Category(@"Behavior")]
[Description(@"The number of pages displayed horizontally across the page.")]
[DefaultValue(1)]
public int Columns { get; set; }
```

**Valid Range**: 1 or greater  
**Example**:
```csharp
previewControl.Columns = 2; // Display 2 pages side-by-side
```

#### Rows

Gets or sets the number of pages displayed vertically down the page.

```csharp
[Category(@"Behavior")]
[Description(@"The number of pages displayed vertically down the page.")]
[DefaultValue(1)]
public int Rows { get; set; }
```

**Valid Range**: 1 or greater  
**Example**:
```csharp
previewControl.Rows = 2; // Display 2 rows of pages
```

**Common Layouts**:
- 1 page: `Columns = 1, Rows = 1`
- 2 pages: `Columns = 2, Rows = 1`
- 3 pages: `Columns = 3, Rows = 1`
- 4 pages: `Columns = 2, Rows = 2`
- 6 pages: `Columns = 3, Rows = 2`

#### Zoom

Gets or sets the zoom level of the pages.

```csharp
[Category(@"Behavior")]
[Description(@"The zoom level of the pages.")]
[DefaultValue(0.3)]
public double Zoom { get; set; }
```

**Valid Range**: 
- **Krypton.Toolkit**: Standard `PrintPreviewControl` range (typically 0.25 to 5.0)
- **Krypton.Utilities**: 0.1 to 5.0 (automatically clamped)

**Example**:
```csharp
previewControl.Zoom = 0.5;  // 50% zoom
previewControl.Zoom = 1.0;  // 100% zoom (actual size)
previewControl.Zoom = 2.0;  // 200% zoom
```

#### AutoZoom

Gets or sets a value indicating whether the control automatically resizes to fit its contents.

```csharp
[Category(@"Behavior")]
[Description(@"Indicates whether the control automatically resizes to fit its contents.")]
[DefaultValue(false)]
public bool AutoZoom { get; set; }
```

**Behavior**:
- When `true`, the zoom level is automatically calculated to fit all visible pages in the viewport
- When `false`, the `Zoom` property value is used directly
- Auto-zoom recalculates when the control is resized

**Example**:
```csharp
previewControl.AutoZoom = true; // Automatically fit pages to viewport
```

#### StartPage

Gets or sets the starting page number (zero-based index).

```csharp
[Category(@"Behavior")]
[Description(@"The starting page number.")]
[DefaultValue(0)]
public int StartPage { get; set; }
```

**Example**:
```csharp
previewControl.StartPage = 0; // Start at first page
previewControl.StartPage = 4; // Start at page 5 (zero-based)
```

#### UseAntiAlias

Gets or sets a value indicating whether anti-aliasing is used when rendering the page.

```csharp
[Category(@"Behavior")]
[Description(@"Indicates whether anti-aliasing is used when rendering the page.")]
[DefaultValue(true)]
public bool UseAntiAlias { get; set; }
```

**Note**: 
- **Krypton.Toolkit**: Delegates to standard `PrintPreviewControl.UseAntiAlias`
- **Krypton.Utilities**: Controls custom rendering anti-aliasing

**Example**:
```csharp
previewControl.UseAntiAlias = true; // Smoother rendering (default)
previewControl.UseAntiAlias = false; // Faster rendering, may appear pixelated
```

#### TabStop

Gets or sets if the control is in the tab chain.

```csharp
[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
public new bool TabStop { get; set; }
```

### Krypton.Toolkit Specific Properties

#### PanelBackStyle

Gets or sets the panel style for the control background.

```csharp
[Category(@"Visuals")]
[Description(@"Panel style.")]
[DefaultValue(PaletteBackStyle.PanelAlternate)]
public PaletteBackStyle PanelBackStyle { get; set; }
```

**Default**: `PaletteBackStyle.PanelAlternate`  
**Note**: This affects the control background, not the page backgrounds (which remain white).

**Example**:
```csharp
previewControl.PanelBackStyle = PaletteBackStyle.PanelClient;
previewControl.PanelBackStyle = PaletteBackStyle.PanelAlternate;
```

#### PrintPreviewControl

Gets access to the underlying standard `PrintPreviewControl` instance.

```csharp
[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
[EditorBrowsable(EditorBrowsableState.Always)]
[Browsable(false)]
public System.Windows.Forms.PrintPreviewControl PrintPreviewControl { get; }
```

**Use Case**: Access standard `PrintPreviewControl` properties/methods not exposed by the wrapper.

**Example**:
```csharp
var standardControl = previewControl.PrintPreviewControl;
// Access any standard PrintPreviewControl members
```

### Krypton.Utilities Specific Properties

#### PageCount

Gets the total number of pages in the document.

```csharp
[Browsable(false)]
public int PageCount { get; }
```

**Example**:
```csharp
var totalPages = previewControl.PageCount;
Console.WriteLine($"Document has {totalPages} pages");
```

#### PageBackStyle

Gets or sets the panel style for page backgrounds.

```csharp
[Category(@"Visuals")]
[Description(@"Panel style for page backgrounds.")]
[DefaultValue(PaletteBackStyle.PanelAlternate)]
public PaletteBackStyle PageBackStyle { get; set; }
```

**Default**: `PaletteBackStyle.PanelAlternate`  
**Note**: This controls the background color of the actual preview pages (the white rectangles).

**Example**:
```csharp
previewControl.PageBackStyle = PaletteBackStyle.PanelAlternate; // Themed page backgrounds
previewControl.PageBackStyle = PaletteBackStyle.PanelClient;     // Different theme style
```

### Appearance Properties

Both implementations provide access to palette appearance properties:

#### StateCommon

Gets access to the common appearance that other states can override.

```csharp
[Category(@"Visuals")]
[Description(@"Overrides for defining common print preview control appearance that other states can override.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public PaletteBack StateCommon { get; }
```

#### StateNormal

Gets access to the normal appearance.

```csharp
[Category(@"Visuals")]
[Description(@"Overrides for defining normal print preview control appearance.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public PaletteBack StateNormal { get; }
```

#### StateDisabled

Gets access to the disabled appearance.

```csharp
[Category(@"Visuals")]
[Description(@"Overrides for defining disabled print preview control appearance.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public PaletteBack StateDisabled { get; }
```

**Example - Customizing Appearance**:
```csharp
// Customize normal state background
previewControl.StateNormal.Back.Color1 = Color.LightBlue;

// Customize disabled state
previewControl.StateDisabled.Back.Color1 = Color.LightGray;
```

---

## Methods

### Public Methods

Both implementations inherit standard `Control` methods. No additional public methods are exposed beyond standard WinForms control methods.

### Protected Methods

#### OnLayout (Krypton.Toolkit)

Handles layout calculations for positioning the internal `PrintPreviewControl`.

```csharp
protected override void OnLayout(LayoutEventArgs levent)
```

#### OnEnabledChanged

Updates the control appearance when the enabled state changes.

```csharp
protected override void OnEnabledChanged(EventArgs e)
```

#### OnPaletteChanged

Updates the control when the palette changes.

```csharp
protected override void OnPaletteChanged(EventArgs e)
```

#### OnStartPageChanged (Krypton.Utilities)

Raises the `StartPageChanged` event.

```csharp
protected virtual void OnStartPageChanged(EventArgs e)
```

#### OnPaint (Krypton.Utilities)

Handles custom rendering of preview pages.

```csharp
protected override void OnPaint(PaintEventArgs e)
```

#### OnResize (Krypton.Utilities)

Recalculates auto-zoom when the control is resized.

```csharp
protected override void OnResize(EventArgs e)
```

---

## Events

### StartPageChanged

Occurs when the starting page changes.

```csharp
[Category(@"Property Changed")]
[Description(@"Occurs when the starting page changes.")]
public event EventHandler? StartPageChanged;
```

**Example**:
```csharp
previewControl.StartPageChanged += (sender, e) =>
{
    Console.WriteLine($"Now viewing page {previewControl.StartPage + 1}");
};
```

### Standard Control Events

Both implementations support all standard `Control` events:
- `Click`, `DoubleClick`
- `MouseEnter`, `MouseLeave`, `MouseMove`, `MouseDown`, `MouseUp`
- `Paint`, `Resize`, `Layout`
- `EnabledChanged`, etc.

---

## Theming and Appearance

### Palette Integration

Both implementations fully integrate with Krypton's theming system:

#### Global Palette

The control automatically uses the current global palette:

```csharp
// Change global theme
KryptonManager.GlobalPaletteMode = PaletteMode.Office2013Blue;
// All KryptonPrintPreviewControl instances update automatically
```

#### Custom Palette

Apply a custom palette to a specific control:

```csharp
var customPalette = new KryptonCustomPaletteBase();
// Configure custom palette...

previewControl.Palette = customPalette;
previewControl.PaletteMode = PaletteMode.Custom;
```

#### Palette Modes

```csharp
previewControl.PaletteMode = PaletteMode.Office2013Blue;
previewControl.PaletteMode = PaletteMode.Microsoft365Blue;
previewControl.PaletteMode = PaletteMode.VisualStudio2019Dark;
// ... and many more
```

### Appearance Customization

#### Control Background (Both Implementations)

```csharp
// Use PanelAlternate style (default for Toolkit version)
previewControl.PanelBackStyle = PaletteBackStyle.PanelAlternate;

// Use PanelClient style
previewControl.PanelBackStyle = PaletteBackStyle.PanelClient;

// Customize colors directly
previewControl.StateNormal.Back.Color1 = Color.FromArgb(240, 240, 240);
previewControl.StateNormal.Back.Color2 = Color.FromArgb(220, 220, 220);
```

#### Page Backgrounds (Krypton.Utilities Only)

```csharp
// Use PanelAlternate for page backgrounds (default)
previewControl.PageBackStyle = PaletteBackStyle.PanelAlternate;

// Use PanelClient for page backgrounds
previewControl.PageBackStyle = PaletteBackStyle.PanelClient;

// Customize page background colors
// (Requires accessing internal palette - advanced usage)
```

### State Management

The control supports multiple visual states:

- **Normal**: Default enabled state
- **Disabled**: When `Enabled = false`
- **Common**: Base appearance that other states inherit from

**Example**:
```csharp
// Customize normal state
previewControl.StateNormal.Back.Color1 = Color.White;

// Customize disabled state
previewControl.StateDisabled.Back.Color1 = Color.LightGray;
previewControl.StateDisabled.Back.Color2 = Color.Gray;

// Disable the control to see disabled state
previewControl.Enabled = false;
```

---

## Usage Examples

### Example 1: Basic Print Preview

```csharp
using System.Drawing;
using System.Drawing.Printing;
using Krypton.Toolkit;

public partial class MainForm : KryptonForm
{
    private void ShowPrintPreview()
    {
        var printDoc = new PrintDocument();
        printDoc.PrintPage += PrintDocument_PrintPage;

        var previewControl = new KryptonPrintPreviewControl
        {
            Document = printDoc,
            Dock = DockStyle.Fill,
            Zoom = 0.75,
            UseAntiAlias = true
        };

        var previewForm = new KryptonForm
        {
            Text = "Print Preview",
            WindowState = FormWindowState.Maximized
        };
        previewForm.Controls.Add(previewControl);
        previewForm.ShowDialog(this);
    }

    private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    {
        var font = new Font("Arial", 12, FontStyle.Bold);
        e.Graphics!.DrawString("Sample Document", font, Brushes.Black, 100, 100);
        e.Graphics.DrawString("This is a test print preview.", new Font("Arial", 10), 
            Brushes.Black, 100, 150);
    }
}
```

### Example 2: Multi-Page Document Preview

```csharp
using Krypton.Toolkit;

var previewControl = new KryptonPrintPreviewControl
{
    Document = multiPageDocument,
    Columns = 2,  // Show 2 pages side-by-side
    Rows = 1,
    Zoom = 0.5,
    StartPage = 0
};

// Navigate to different pages
previewControl.StartPage = 2; // Jump to page 3 (zero-based)
```

### Example 3: Custom Theming

```csharp
using Krypton.Toolkit;

var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument,
    PanelBackStyle = PaletteBackStyle.PanelClient,
    StateNormal = 
    {
        Back = 
        {
            Color1 = Color.FromArgb(245, 245, 250),
            Color2 = Color.FromArgb(235, 235, 240)
        }
    }
};
```

### Example 4: Using Krypton.Utilities Version

```csharp
using Krypton.Utilities;

var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument,
    Dock = DockStyle.Fill,
    Zoom = 0.5,
    Columns = 1,
    Rows = 1,
    PageBackStyle = PaletteBackStyle.PanelAlternate, // Themed page backgrounds
    UseAntiAlias = true,
    AutoZoom = false
};

// Mouse wheel zoom (Ctrl+Wheel)
// Panning (Middle mouse button or Shift+Left mouse)
```

### Example 5: Dynamic Zoom Control

```csharp
using Krypton.Toolkit;

var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument,
    Dock = DockStyle.Fill
};

// Zoom in
previewControl.Zoom += 0.25;

// Zoom out
if (previewControl.Zoom > 0.25)
{
    previewControl.Zoom -= 0.25;
}

// Set specific zoom level
previewControl.Zoom = 1.0; // 100%

// Enable auto-zoom
previewControl.AutoZoom = true;
```

### Example 6: Page Layout Selection

```csharp
using Krypton.Toolkit;

var previewControl = new KryptonPrintPreviewControl
{
    Document = printDocument
};

// One page view
previewControl.Columns = 1;
previewControl.Rows = 1;

// Two pages side-by-side
previewControl.Columns = 2;
previewControl.Rows = 1;

// Four pages (2x2 grid)
previewControl.Columns = 2;
previewControl.Rows = 2;

// Six pages (3x2 grid)
previewControl.Columns = 3;
previewControl.Rows = 2;
```

### Example 7: Integration with KryptonPrintPreviewDialog

```csharp
using Krypton.Toolkit;

var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    UseAntiAlias = true
};

// Access the underlying control
var previewControl = previewDialog.PrintPreviewControl;
if (previewControl != null)
{
    previewControl.Zoom = 0.75;
    previewControl.Columns = 2;
    previewControl.Rows = 1;
}

previewDialog.ShowDialog(this);
```

---

## Advanced Features

### Krypton.Utilities Specific Features

#### Mouse Wheel Zoom

Zoom in/out using Ctrl+Mouse Wheel:

```csharp
// Automatically supported - no code needed
// User holds Ctrl and scrolls mouse wheel to zoom
```

#### Panning Support

Pan the preview using:
- **Middle mouse button**: Click and drag
- **Shift+Left mouse button**: Click and drag

```csharp
// Automatically supported - no code needed
// User can pan to navigate large documents
```

#### Custom Page Rendering

The Utilities version provides full control over page rendering, including:
- Custom page shadows
- Themed page borders
- Customizable page spacing
- Full palette integration for page backgrounds

### Performance Optimization

#### Anti-Aliasing

Disable anti-aliasing for better performance on large documents:

```csharp
previewControl.UseAntiAlias = false; // Faster rendering
```

#### Page Caching (Krypton.Utilities)

The Utilities version caches rendered pages to improve performance when navigating:

```csharp
// Pages are automatically cached after first generation
// Changing zoom or layout recalculates but doesn't regenerate pages
```

---

## Performance Considerations

### Krypton.Toolkit Version

- **Pros**: 
  - Uses native `PrintPreviewControl` rendering (optimized)
  - Lower memory footprint
  - Faster initial rendering
  
- **Cons**:
  - Limited customization of page rendering
  - Page backgrounds remain white

### Krypton.Utilities Version

- **Pros**:
  - Full control over rendering
  - Themed page backgrounds
  - Custom visual effects (shadows, borders)
  
- **Cons**:
  - Higher memory usage (caches page images)
  - Slightly slower initial rendering
  - More CPU usage for custom rendering

### Recommendations

- **Use Krypton.Toolkit version** for:
  - Standard print preview needs
  - Maximum performance
  - Large documents
  - Simple theming requirements

- **Use Krypton.Utilities version** for:
  - Advanced theming needs
  - Custom page backgrounds
  - Enhanced visual effects
  - Full rendering control

---

## Best Practices

### 1. Document Setup

Always set up the `PrintDocument` before assigning it:

```csharp
var printDoc = new PrintDocument();
printDoc.PrintPage += PrintDocument_PrintPage;
printDoc.DocumentName = "My Document";

// Set document after setup
previewControl.Document = printDoc;
```

### 2. Resource Management

Dispose of controls properly:

```csharp
using (var previewControl = new KryptonPrintPreviewControl())
{
    previewControl.Document = printDocument;
    // Use control...
} // Automatically disposed
```

### 3. Zoom Levels

Use appropriate zoom levels for different scenarios:

```csharp
// Overview: Show multiple pages
previewControl.Zoom = 0.25;
previewControl.Columns = 2;
previewControl.Rows = 2;

// Detail: Single page, larger zoom
previewControl.Zoom = 1.0;
previewControl.Columns = 1;
previewControl.Rows = 1;
```

### 4. Theming Consistency

Maintain theme consistency across your application:

```csharp
// Use global palette for consistency
KryptonManager.GlobalPaletteMode = PaletteMode.Office2013Blue;

// All KryptonPrintPreviewControl instances will use this theme
```

### 5. Error Handling

Handle document errors gracefully:

```csharp
try
{
    previewControl.Document = printDocument;
}
catch (Exception ex)
{
    MessageBox.Show($"Error setting document: {ex.Message}");
}
```

### 6. Large Documents

For documents with many pages:

```csharp
// Use lower zoom for overview
previewControl.Zoom = 0.3;
previewControl.Columns = 2;
previewControl.Rows = 2;

// Disable anti-aliasing for performance
previewControl.UseAntiAlias = false;
```

---

## Troubleshooting

### Issue: Pages Not Visible

**Symptoms**: Control shows background but no pages.

**Solutions**:
1. Ensure `Document` property is set
2. Verify `PrintDocument` has a `PrintPage` event handler
3. Check that the document actually has content
4. Verify `Zoom` is not too small (try `Zoom = 0.5`)

```csharp
// Debug: Check if document is set
if (previewControl.Document == null)
{
    MessageBox.Show("Document is null!");
}

// Debug: Verify PrintPage handler
if (previewControl.Document != null)
{
    var handlers = previewControl.Document.GetType()
        .GetEvent("PrintPage")
        ?.GetInvocationList();
    Console.WriteLine($"PrintPage handlers: {handlers?.Length ?? 0}");
}
```

### Issue: Background Not Themed

**Symptoms**: Control background appears gray/white instead of themed.

**Solutions**:
1. Verify global palette is set
2. Check `PanelBackStyle` property
3. Ensure control is enabled
4. Verify palette mode is not `Custom` without a custom palette

```csharp
// Check current palette mode
Console.WriteLine($"Palette Mode: {previewControl.PaletteMode}");

// Force palette update
previewControl.PaletteMode = PaletteMode.Global;
```

### Issue: Page Backgrounds Not Themed (Krypton.Toolkit)

**Symptoms**: Page backgrounds remain white.

**Explanation**: This is a limitation of the Toolkit wrapper version. The standard `PrintPreviewControl` renders pages with white backgrounds internally.

**Solution**: Use `Krypton.Utilities.KryptonPrintPreviewControl` for themed page backgrounds.

### Issue: Performance Problems

**Symptoms**: Slow rendering, laggy scrolling.

**Solutions**:
1. Disable anti-aliasing: `UseAntiAlias = false`
2. Reduce zoom level
3. Use fewer columns/rows
4. For Utilities version: Consider page caching optimizations

```csharp
// Optimize for performance
previewControl.UseAntiAlias = false;
previewControl.Zoom = 0.5;
previewControl.Columns = 1;
previewControl.Rows = 1;
```

### Issue: Auto-Zoom Not Working

**Symptoms**: `AutoZoom = true` but zoom doesn't change.

**Solutions**:
1. Ensure control has a valid size
2. Verify document has pages
3. Check that control is visible

```csharp
// Force auto-zoom recalculation
previewControl.AutoZoom = false;
previewControl.AutoZoom = true;
previewControl.Invalidate();
```

### Issue: StartPage Out of Range

**Symptoms**: `StartPage` doesn't change or throws exception.

**Solutions**:
1. Check `PageCount` (Utilities version) before setting `StartPage`
2. Ensure document has been loaded
3. Use zero-based indexing

```csharp
// Safe StartPage setting (Utilities version)
if (previewControl.PageCount > 0)
{
    var maxPage = previewControl.PageCount - 1;
    previewControl.StartPage = Math.Min(requestedPage, maxPage);
}
```

---

## Related Components

### KryptonPrintPreviewDialog

A complete dialog wrapper around `KryptonPrintPreviewControl` with integrated toolbar.

**See**: [print-preview-dialog-feature.md](./print-preview-dialog-feature.md)

### KryptonPrintDialog

Themed print dialog for actual printing.

**Namespace**: `Krypton.Toolkit`

### PrintDocument

Standard .NET `PrintDocument` class used for document definition.

**Namespace**: `System.Drawing.Printing`

---

## Implementation Details

### Krypton.Toolkit Implementation

#### Architecture

```
KryptonPrintPreviewControl (VisualControlBase)
  └─ ViewManager
      └─ ViewDrawDocker (themed border/background)
          └─ ViewLayoutDocker
              └─ ViewLayoutFill
                  └─ InternalPrintPreviewControl (PrintPreviewControl)
```

#### Key Components

- **ViewDrawDocker**: Provides themed border and background
- **ViewLayoutFill**: Manages layout of internal control
- **InternalPrintPreviewControl**: Wraps standard `PrintPreviewControl`
- **PaletteDoubleRedirect**: Manages palette state (Normal/Disabled)

#### Rendering Flow

1. `VisualControlBase.OnPaint` → ViewManager paints themed background
2. Internal `PrintPreviewControl` renders pages on top
3. Background color synchronized via `UpdatePreviewControlBackColor()`

### Krypton.Utilities Implementation

#### Architecture

```
KryptonPrintPreviewControl (VisualPanel)
  └─ PrintPreviewPageCache (page image cache)
  └─ Custom OnPaint rendering
      └─ RenderPages() method
          ├─ Page shadows
          ├─ Page backgrounds (themed)
          ├─ Page borders (themed)
          └─ Page content (from cache)
```

#### Key Components

- **PrintPreviewPageCache**: Caches rendered page images
- **Custom OnPaint**: Full control over rendering
- **Separate Palettes**: Control background vs page backgrounds
- **Mouse Interaction**: Zoom and pan support

#### Rendering Flow

1. `OnPaint` → Fill control background (themed)
2. Generate pages if needed → `GeneratePreview()`
3. Calculate layout → `CalculateTotalSize()`
4. Render pages → `RenderPages()` with themed backgrounds

---

## Code Examples

### Complete Example: Print Preview Form

```csharp
using System.Drawing;
using System.Drawing.Printing;
using Krypton.Toolkit;

public partial class PrintPreviewForm : KryptonForm
{
    private KryptonPrintPreviewControl _previewControl;
    private PrintDocument _document;

    public PrintPreviewForm(PrintDocument document)
    {
        InitializeComponent();
        _document = document;
        SetupPreviewControl();
    }

    private void SetupPreviewControl()
    {
        _previewControl = new KryptonPrintPreviewControl
        {
            Document = _document,
            Dock = DockStyle.Fill,
            Zoom = 0.75,
            Columns = 1,
            Rows = 1,
            UseAntiAlias = true,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        _previewControl.StartPageChanged += PreviewControl_StartPageChanged;
        Controls.Add(_previewControl);
    }

    private void PreviewControl_StartPageChanged(object? sender, EventArgs e)
    {
        Text = $"Print Preview - Page {_previewControl.StartPage + 1}";
    }
}
```

### Example: Custom Toolbar Integration

```csharp
using Krypton.Toolkit;

public partial class CustomPreviewForm : KryptonForm
{
    private KryptonPrintPreviewControl _previewControl;
    private KryptonPanel _toolbar;
    private KryptonButton _btnZoomIn, _btnZoomOut;
    private KryptonTrackBar _zoomTrackBar;

    public CustomPreviewForm()
    {
        InitializeComponent();
        SetupToolbar();
        SetupPreviewControl();
    }

    private void SetupToolbar()
    {
        _toolbar = new KryptonPanel { Dock = DockStyle.Top, Height = 40 };

        _btnZoomIn = new KryptonButton { Text = "Zoom In", Location = new Point(8, 8) };
        _btnZoomIn.Click += (s, e) => _previewControl.Zoom += 0.25;

        _btnZoomOut = new KryptonButton { Text = "Zoom Out", Location = new Point(89, 8) };
        _btnZoomOut.Click += (s, e) => 
        {
            if (_previewControl.Zoom > 0.25)
                _previewControl.Zoom -= 0.25;
        };

        _zoomTrackBar = new KryptonTrackBar 
        { 
            Location = new Point(170, 8),
            Minimum = 25,
            Maximum = 500,
            Value = 75
        };
        _zoomTrackBar.ValueChanged += (s, e) => 
            _previewControl.Zoom = _zoomTrackBar.Value / 100.0;

        _toolbar.Controls.AddRange(new Control[] 
        { 
            _btnZoomIn, 
            _btnZoomOut, 
            _zoomTrackBar 
        });
        Controls.Add(_toolbar);
    }

    private void SetupPreviewControl()
    {
        _previewControl = new KryptonPrintPreviewControl
        {
            Dock = DockStyle.Fill,
            Document = myPrintDocument
        };
        Controls.Add(_previewControl);
    }
}
```

---

## Summary

The `KryptonPrintPreviewControl` provides a fully-themed print preview experience with two implementation options:

- **Krypton.Toolkit**: Wrapper version with standard compatibility and performance
- **Krypton.Utilities**: Full custom implementation with advanced theming

Both versions integrate seamlessly with Krypton's theming system and provide a consistent, professional appearance across all supported themes.
