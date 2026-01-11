# KryptonPrintPreviewDialog Feature

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Properties](#properties)
5. [Methods](#methods)
6. [Usage Examples](#usage-examples)
7. [Integration with Other Components](#integration-with-other-components)
8. [Toolbar Features](#toolbar-features)
9. [PrintPreviewControl Access](#printpreviewcontrol-access)
10. [Designer Support](#designer-support)
11. [Implementation Details](#implementation-details)
12. [Best Practices](#best-practices)
13. [Troubleshooting](#troubleshooting)
14. [Related Components](#related-components)

---

## Overview

The `KryptonPrintPreviewDialog` is a fully-themed replacement for the standard Windows Forms `PrintPreviewDialog`. It provides a comprehensive print preview interface with a Krypton-styled toolbar and integrates seamlessly with the Krypton Toolkit's theming system.

### Key Features

- **Full Krypton Theming**: All controls use Krypton styling and respect the current global palette
- **Integrated Toolbar**: Built-in toolbar with print, zoom, and page layout controls using Krypton buttons
- **Print Integration**: Seamlessly integrates with `KryptonPrintDialog` for printing
- **PrintPreviewControl Access**: Direct access to the underlying `PrintPreviewControl` for advanced customization
- **Anti-aliasing Support**: Configurable anti-aliasing for smoother preview rendering
- **Modal Dialog**: Standard modal dialog behavior with owner window support
- **Window State Control**: Configurable initial window state (Normal, Maximized, Minimized)
- **Custom Icon/Title**: Support for custom form icons and titles
- **Component-Based**: Inherits from `Component` and implements `IDisposable` for proper resource management

### Supported Platforms

- .NET Framework 4.7.2 and later
- .NET 8.0 Windows and later
- All target frameworks supported by Krypton Toolkit

---

## Quick Start

### Basic Usage

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

// Create and show the preview dialog
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    Text = "My Document Preview"
};

previewDialog.ShowDialog();
```

### With Owner Window

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument
};

previewDialog.ShowDialog(this); // 'this' is the parent form
```

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

### Class Declaration

```csharp
public class KryptonPrintPreviewDialog : Component, IDisposable
```

---

## Properties

### Document

Gets or sets the `PrintDocument` to preview.

**Type:** `PrintDocument?`

**Default Value:** `null`

**Category:** Behavior

**Description:** The PrintDocument that will be previewed in the dialog. This property must be set before calling `ShowDialog()`.

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog();
previewDialog.Document = myPrintDocument;
previewDialog.ShowDialog();
```

**Remarks:**
- This property must be set to a non-null value before showing the dialog, otherwise an `ArgumentNullException` will be thrown
- Changing this property after the dialog is shown will update the preview control

---

### PrintPreviewControl

Gets the `PrintPreviewControl` contained in this dialog.

**Type:** `PrintPreviewControl?`

**Browsable:** `false`

**Description:** Provides direct access to the underlying `PrintPreviewControl` for advanced customization. This property is `null` until the dialog has been shown at least once.

**Example:**

```csharp
previewDialog.ShowDialog();

// Access the underlying control for advanced features
var control = previewDialog.PrintPreviewControl;
if (control != null)
{
    control.Zoom = 1.5; // 150% zoom
    control.Columns = 2; // 2 columns
    control.Rows = 2;    // 2 rows
}
```

**Remarks:**
- The control is only available after the dialog has been displayed
- You can modify properties of the control even while the dialog is shown
- Changes to the control properties are immediately reflected in the preview

---

### UseAntiAlias

Gets or sets a value indicating whether printing uses anti-aliasing.

**Type:** `bool`

**Default Value:** `true`

**Category:** Behavior

**Description:** When set to `true`, the preview uses anti-aliasing for smoother text and graphics rendering. This may impact performance on slower systems.

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    UseAntiAlias = true  // Enable anti-aliasing for smoother preview
};
```

**Remarks:**
- Anti-aliasing improves visual quality but may reduce performance
- The default value is `true` for optimal visual quality
- This setting only affects the preview, not the actual printed output

---

### Icon

Gets or sets the icon for the form.

**Type:** `Icon?`

**Default Value:** `null`

**Category:** Appearance

**Description:** The icon displayed in the title bar and taskbar for the preview dialog. If `null`, the default form icon is used.

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    Icon = myCustomIcon  // Use a custom icon
};
```

**Remarks:**
- Set this property before calling `ShowDialog()` for it to take effect
- The icon is shown in the dialog's title bar and taskbar

---

### Text

Gets or sets the text associated with this control (the dialog title).

**Type:** `string`

**Default Value:** `"Print Preview"`

**Category:** Appearance

**Localizable:** `true`

**Description:** The text displayed in the title bar of the preview dialog. This property is localizable.

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    Text = "Document Preview - My Application"
};
```

**Remarks:**
- This property is localizable using standard .NET localization mechanisms
- The default value is "Print Preview"
- Changing this property before showing the dialog updates the form title

---

### WindowState

Gets or sets the form's window state.

**Type:** `FormWindowState`

**Default Value:** `FormWindowState.Normal`

**Category:** Window Style

**Description:** Specifies how the dialog window should appear initially (Normal, Maximized, or Minimized).

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    WindowState = FormWindowState.Maximized  // Start maximized
};
```

**Values:**
- `FormWindowState.Normal`: Default size and position
- `FormWindowState.Maximized`: Window starts maximized
- `FormWindowState.Minimized`: Window starts minimized (rarely used for dialogs)

---

## Methods

### ShowDialog()

Runs a print preview dialog box.

**Signature:**

```csharp
public DialogResult ShowDialog()
```

**Returns:** `DialogResult` - One of the `DialogResult` values (typically `DialogResult.OK` or `DialogResult.Cancel`)

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument
};

DialogResult result = previewDialog.ShowDialog();
if (result == DialogResult.OK)
{
    // User closed the dialog (typically by clicking Close button)
}
```

**Remarks:**
- This method displays the dialog as a modal dialog box
- The dialog is centered on the screen
- Execution of the calling code is blocked until the dialog is closed

---

### ShowDialog(IWin32Window? owner)

Runs a print preview dialog box with the specified owner.

**Signature:**

```csharp
public DialogResult ShowDialog(IWin32Window? owner)
```

**Parameters:**
- `owner`: Any object that implements `IWin32Window` that represents the top-level window that will own the modal dialog box. Can be `null`.

**Returns:** `DialogResult` - One of the `DialogResult` values

**Example:**

```csharp
var previewDialog = new KryptonPrintPreviewDialog
{
    Document = printDocument
};

// Show as child of the current form
previewDialog.ShowDialog(this);

// Or show without a parent
previewDialog.ShowDialog(null);
```

**Remarks:**
- Specifying an owner window centers the dialog relative to the owner
- The owner window is disabled while the dialog is shown (standard modal behavior)
- If `null` is passed, the dialog is centered on the screen

**Exceptions:**
- `ArgumentNullException`: Thrown if the `Document` property is `null` before calling this method

---

### Dispose(bool disposing)

Disposes of the resources used by the `KryptonPrintPreviewDialog`.

**Signature:**

```csharp
protected override void Dispose(bool disposing)
```

**Parameters:**
- `disposing`: `true` to release both managed and unmanaged resources; `false` to release only unmanaged resources

**Remarks:**
- This method is called automatically when the component is disposed
- Implement `IDisposable` pattern correctly when storing references to instances

---

## Usage Examples

### Example 1: Simple Text Document

```csharp
using System.Drawing;
using System.Drawing.Printing;
using Krypton.Toolkit;

private void PreviewSimpleDocument()
{
    var printDoc = new PrintDocument();
    printDoc.PrintPage += (sender, e) =>
    {
        var font = new Font("Arial", 12);
        var brush = Brushes.Black;
        
        e.Graphics!.DrawString("Sample Document", new Font("Arial", 16, FontStyle.Bold), 
            brush, 100, 100);
        e.Graphics.DrawString("This is a simple text document.", font, brush, 100, 140);
        e.Graphics.DrawString("It demonstrates basic printing.", font, brush, 100, 160);
    };

    var preview = new KryptonPrintPreviewDialog
    {
        Document = printDoc,
        Text = "Simple Document Preview"
    };

    preview.ShowDialog();
}
```

### Example 2: Multi-Page Document

```csharp
private void PreviewMultiPageDocument()
{
    var printDoc = new PrintDocument();
    int currentPage = 0;
    int totalPages = 5;

    printDoc.PrintPage += (sender, e) =>
    {
        currentPage++;
        var font = new Font("Arial", 12);
        var pageText = $"Page {currentPage} of {totalPages}";
        
        e.Graphics!.DrawString(pageText, font, Brushes.Black, 100, 100);
        e.Graphics.DrawString($"Content for page {currentPage} goes here.", 
            font, Brushes.Black, 100, 130);

        // Continue to next page if not the last page
        e.HasMorePages = currentPage < totalPages;
    };

    var preview = new KryptonPrintPreviewDialog
    {
        Document = printDoc,
        UseAntiAlias = true,
        WindowState = FormWindowState.Maximized
    };

    preview.ShowDialog();
}
```

### Example 3: Custom Icon and Maximized

```csharp
private void PreviewWithCustomSettings()
{
    var printDoc = new PrintDocument();
    printDoc.PrintPage += (sender, e) =>
    {
        // Print content here
    };

    var preview = new KryptonPrintPreviewDialog
    {
        Document = printDoc,
        Icon = Properties.Resources.ApplicationIcon,  // Custom icon
        Text = "My Application - Print Preview",
        UseAntiAlias = true,
        WindowState = FormWindowState.Maximized
    };

    preview.ShowDialog(this);  // Owner is current form
}
```

### Example 4: Advanced Control Customization

```csharp
private void PreviewWithAdvancedControl()
{
    var printDoc = new PrintDocument();
    printDoc.PrintPage += (sender, e) =>
    {
        // Print content
    };

    var preview = new KryptonPrintPreviewDialog
    {
        Document = printDoc
    };

    // Show dialog first
    preview.ShowDialog();

    // Then customize the underlying control
    var control = preview.PrintPreviewControl;
    if (control != null)
    {
        control.Zoom = 0.75;  // 75% zoom
        control.Columns = 2;   // 2 columns
        control.Rows = 1;      // 1 row
        control.UseAntiAlias = true;
    }
}
```

### Example 5: Using with KryptonPrintDialog

```csharp
private void PrintWithPreview()
{
    var printDoc = new PrintDocument();
    printDoc.PrintPage += (sender, e) =>
    {
        // Print content
    };

    // First show preview
    var preview = new KryptonPrintPreviewDialog
    {
        Document = printDoc
    };

    if (preview.ShowDialog() == DialogResult.OK)
    {
        // User wants to print, show print dialog
        var printDialog = new KryptonPrintDialog
        {
            Document = printDoc
        };

        if (printDialog.ShowDialog() == DialogResult.OK)
        {
            printDoc.Print();
        }
    }
}
```

---

## Integration with Other Components

### KryptonPrintDialog

The `KryptonPrintPreviewDialog` integrates seamlessly with `KryptonPrintDialog`. When the user clicks the "Print..." button in the preview dialog, it automatically opens a `KryptonPrintDialog` with the same document.

**Example:**

```csharp
// The Print button in the preview dialog automatically uses KryptonPrintDialog
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog();  // Print button uses KryptonPrintDialog internally
```

### KryptonForm

The preview dialog uses a `KryptonForm` internally, ensuring full theming consistency with your application.

### KryptonManager Global Palette

The preview dialog automatically respects the current global palette set via `KryptonManager`. All Krypton controls in the toolbar will use the active theme.

**Example:**

```csharp
// Set global palette
KryptonManager.GlobalPaletteMode = PaletteMode.Office2010Blue;

// Preview dialog will use Office 2010 Blue theme
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog();
```

---

## Toolbar Features

The `KryptonPrintPreviewDialog` includes a comprehensive toolbar with the following features:

### Print Button

- **Text:** "Print..."
- **Function:** Opens `KryptonPrintDialog` to configure and execute printing
- **Behavior:** Only enabled when a document is assigned

### Zoom Controls

#### Zoom In
- **Text:** "Zoom In"
- **Function:** Increases zoom level by 25% (0.25)
- **Behavior:** Continues to zoom in with each click

#### Zoom Out
- **Text:** "Zoom Out"
- **Function:** Decreases zoom level by 25% (0.25)
- **Behavior:** Stops at minimum zoom of 25% (0.25)

### Page Layout Controls

#### One Page
- **Text:** "One Page"
- **Function:** Displays 1 page at a time (1 row × 1 column)

#### Two Pages
- **Text:** "Two Pages"
- **Function:** Displays 2 pages horizontally (1 row × 2 columns)

#### Three Pages
- **Text:** "Three Pages"
- **Function:** Displays 3 pages horizontally (1 row × 3 columns)

#### Four Pages
- **Text:** "Four Pages"
- **Function:** Displays 4 pages in a 2×2 grid (2 rows × 2 columns)

#### Six Pages
- **Text:** "Six Pages"
- **Function:** Displays 6 pages in a 2×3 grid (2 rows × 3 columns)

### Close Button

- **Text:** "Close"
- **Function:** Closes the preview dialog
- **DialogResult:** `DialogResult.Cancel`
- **Position:** Right-aligned in the toolbar

---

## PrintPreviewControl Access

While the dialog provides a user-friendly interface, you can access the underlying `PrintPreviewControl` for advanced customization:

### Available Properties

- **Zoom** (`double`): Zoom factor (1.0 = 100%)
- **Columns** (`int`): Number of pages displayed horizontally
- **Rows** (`int`): Number of pages displayed vertically
- **UseAntiAlias** (`bool`): Anti-aliasing setting
- **Document** (`PrintDocument?`): The document being previewed
- **StartPage** (`int`): The first page to display

### Example: Programmatic Control

```csharp
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};

preview.ShowDialog();

// Access control for customization
var control = preview.PrintPreviewControl;
if (control != null)
{
    // Set custom zoom
    control.Zoom = 1.5;  // 150%

    // Set custom layout
    control.Columns = 2;
    control.Rows = 2;

    // Enable anti-aliasing
    control.UseAntiAlias = true;

    // Set start page
    control.StartPage = 0;
}
```

**Remarks:**
- The `PrintPreviewControl` is only available after `ShowDialog()` has been called at least once
- Changes to the control properties are immediately reflected
- You can modify properties while the dialog is shown for dynamic updates

---

## Designer Support

The `KryptonPrintPreviewDialog` can be added to the Visual Studio Toolbox and used in the designer, though it's most commonly used programmatically.

### Toolbox Icon

The dialog uses the same toolbox bitmap as `KryptonPrintDialog` (`ToolboxBitmaps.KryptonPrintDialog.png`).

### Designer Properties

The following properties are available in the Visual Studio Properties window:

- **Document** - Select or create a PrintDocument
- **UseAntiAlias** - Enable/disable anti-aliasing
- **Icon** - Set a custom icon
- **Text** - Set the dialog title
- **WindowState** - Set initial window state

### Example: Designer Usage

1. Drag `KryptonPrintPreviewDialog` from the Toolbox onto your form
2. Set the `Document` property to a PrintDocument component
3. Configure other properties as needed
4. Call `ShowDialog()` in code-behind

```csharp
// In form code
private void btnPreview_Click(object sender, EventArgs e)
{
    kryptonPrintPreviewDialog1.ShowDialog();
}
```

---

## Implementation Details

### Architecture

The `KryptonPrintPreviewDialog` uses a component-based architecture:

1. **Public Component Class**: `KryptonPrintPreviewDialog` (inherits from `Component`)
   - Provides the public API
   - Manages dialog lifecycle
   - Exposes properties and methods

2. **Internal Form Class**: `KryptonPrintPreviewForm` (inherits from `KryptonForm`)
   - Contains the actual UI
   - Manages toolbar and preview control
   - Handles user interactions

### Key Design Decisions

1. **Component vs Form**: The dialog inherits from `Component` rather than `Form` to allow it to be used as a component in the designer and provide better lifecycle management.

2. **Form Creation**: A new form instance is created each time `ShowDialog()` is called, ensuring clean state for each preview session.

3. **Toolbar Integration**: The toolbar uses native Krypton controls (`KryptonButton`, `KryptonLabel`, `KryptonPanel`) for full theming support.

4. **Print Integration**: The Print button directly uses `KryptonPrintDialog` for consistency.

### Resource Management

- The dialog properly implements `IDisposable`
- Internal form instances are disposed when the component is disposed
- `PrintPreviewControl` resources are cleaned up properly

---

## Best Practices

### 1. Always Set Document Before Showing

```csharp
// ✅ Good
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog();

// ❌ Bad - will throw ArgumentNullException
var preview = new KryptonPrintPreviewDialog();
preview.ShowDialog();  // Exception!
```

### 2. Use Using Statement for Disposal

```csharp
// ✅ Good - automatic disposal
using (var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
})
{
    preview.ShowDialog();
}

// ✅ Also good - manual disposal
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog();
preview.Dispose();
```

### 3. Reuse PrintDocument Instances

```csharp
// ✅ Good - reuse document
private PrintDocument _printDocument;

private void InitializePrintDocument()
{
    _printDocument = new PrintDocument();
    _printDocument.PrintPage += OnPrintPage;
}

private void ShowPreview()
{
    var preview = new KryptonPrintPreviewDialog
    {
        Document = _printDocument
    };
    preview.ShowDialog();
}
```

### 4. Set Window State for Better UX

```csharp
// ✅ Good - maximized for better viewing
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    WindowState = FormWindowState.Maximized
};
```

### 5. Use Owner Window for Modal Behavior

```csharp
// ✅ Good - proper parent-child relationship
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog(this);  // 'this' is the parent form
```

### 6. Configure Anti-Aliasing Based on Performance

```csharp
// For high-performance systems
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    UseAntiAlias = true  // Better quality
};

// For slower systems
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    UseAntiAlias = false  // Better performance
};
```

---

## Troubleshooting

### Issue: ArgumentNullException when showing dialog

**Problem:** `ArgumentNullException` is thrown with message "Document must be set before showing the dialog."

**Solution:** Ensure the `Document` property is set before calling `ShowDialog()`.

```csharp
// Fix
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument  // Set this first!
};
preview.ShowDialog();
```

---

### Issue: PrintPreviewControl is null

**Problem:** Accessing `PrintPreviewControl` property returns `null`.

**Solution:** The control is only available after the dialog has been shown at least once.

```csharp
// Fix
preview.ShowDialog();  // Show dialog first
var control = preview.PrintPreviewControl;  // Now it's available
```

---

### Issue: Preview not updating when document changes

**Problem:** Changes to PrintDocument don't reflect in the preview.

**Solution:** The preview control needs to be invalidated or the document needs to be reassigned.

```csharp
// Option 1: Reassign document
preview.Document = updatedPrintDocument;

// Option 2: Access control and invalidate
var control = preview.PrintPreviewControl;
if (control != null)
{
    control.InvalidatePreview();
}
```

---

### Issue: Dialog doesn't respect theme

**Problem:** Dialog controls don't match the application theme.

**Solution:** Ensure `KryptonManager.GlobalPaletteMode` is set before showing the dialog.

```csharp
// Set global palette
KryptonManager.GlobalPaletteMode = PaletteMode.Office2010Blue;

// Dialog will use the theme
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument
};
preview.ShowDialog();
```

---

### Issue: Poor preview performance

**Problem:** Preview rendering is slow or choppy.

**Solutions:**
1. Disable anti-aliasing: `UseAntiAlias = false`
2. Reduce zoom level
3. Use fewer pages in layout (e.g., 1 page instead of 6)

```csharp
var preview = new KryptonPrintPreviewDialog
{
    Document = printDocument,
    UseAntiAlias = false  // Disable for better performance
};
```

---

### Issue: Print button doesn't work

**Problem:** Print button in toolbar doesn't open print dialog.

**Solution:** Ensure a `PrintDocument` is assigned. The Print button requires a valid document.

---

## Related Components

### KryptonPrintDialog

The `KryptonPrintDialog` is automatically used by the Print button in the preview dialog. For standalone printing without preview, use `KryptonPrintDialog` directly.

**Related Documentation:** See `KryptonPrintDialog` class documentation.

### PrintDocument

The standard .NET `PrintDocument` class is used to define what will be printed/previewed.

**MSDN Documentation:** [PrintDocument Class](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.printing.printdocument)

### PrintPreviewControl

The underlying `PrintPreviewControl` provides the actual preview rendering.

**MSDN Documentation:** [PrintPreviewControl Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.printpreviewcontrol)

### KryptonForm

The dialog uses `KryptonForm` internally for consistent theming.

**Related Documentation:** See `KryptonForm` class documentation.

### KryptonManager

The dialog respects the global palette mode set via `KryptonManager`.

**Related Documentation:** See `KryptonManager` class documentation.

---

## Version History

- **Initial Release**: Part of Krypton Toolkit Suite
- **Feature Status**: ✅ Fully Implemented
- **Target Frameworks**: .NET Framework 4.7.2+, .NET 8.0 Windows+, .NET 9.0 Windows+, .NET 10.0 Windows+

---

## Additional Resources

- [Krypton Toolkit Documentation](https://github.com/Krypton-Suite/Standard-Toolkit)
- [PrintPreviewDialog API Reference](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.printpreviewdialog)
- [PrintDocument API Reference](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.printing.printdocument)
- [KryptonPrintDialog Documentation](KryptonPrintDialog.md) (if available)

---

## Summary

The `KryptonPrintPreviewDialog` provides a fully-themed, feature-rich print preview experience that integrates seamlessly with the Krypton Toolkit. It offers:

- ✅ Full Krypton theming support
- ✅ Comprehensive toolbar with print, zoom, and layout controls
- ✅ Integration with `KryptonPrintDialog`
- ✅ Access to underlying `PrintPreviewControl` for advanced scenarios
- ✅ Configurable appearance and behavior
- ✅ Proper resource management
- ✅ Designer support

Use this component whenever you need a print preview dialog that matches your Krypton-themed application's appearance and user experience.
