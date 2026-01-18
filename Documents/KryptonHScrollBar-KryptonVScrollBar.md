# KryptonHScrollBar & KryptonVScrollBar Developer Documentation

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [API Reference](#api-reference)
4. [Properties](#properties)
5. [Methods](#methods)
6. [Events](#events)
7. [State Properties & Theming](#state-properties--theming)
8. [Context Menu](#context-menu)
9. [Usage Examples](#usage-examples)
10. [Keyboard Navigation](#keyboard-navigation)
11. [Best Practices](#best-practices)
12. [Comparison with Standard WinForms ScrollBars](#comparison-with-standard-winforms-scrollbars)
13. [Troubleshooting](#troubleshooting)

---

## Overview

`KryptonHScrollBar` and `KryptonVScrollBar` are fully-featured horizontal and vertical scrollbar controls with complete Krypton theming support. These controls provide a modern, themeable alternative to the standard WinForms `HScrollBar` and `VScrollBar` controls, with enhanced visual states, palette integration, and a Krypton-styled context menu.

### Key Characteristics

- **Full Krypton Theming**: Integrates seamlessly with the Krypton palette system
- **State-Based Appearance**: Supports Normal, Active, Disabled, and Hot states
- **Custom Context Menu**: Built-in KryptonContextMenu with scrollbar-specific actions
- **Keyboard Support**: Full keyboard navigation (Arrow keys, Page Up/Down, Home/End)
- **Mouse Interaction**: Thumb dragging, arrow button clicking, track clicking
- **Performance Optimized**: Double-buffered rendering for smooth scrolling

### Class Hierarchy

```
System.Windows.Forms.Control
└── Krypton.Toolkit.KryptonHScrollBar
└── Krypton.Toolkit.KryptonVScrollBar
```

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Features

### Core Functionality

✅ **Range Management**
- Configurable `Minimum` and `Maximum` values
- Automatic value validation and clamping
- Dynamic thumb size calculation based on `LargeChange`

✅ **Scrolling Modes**
- Small increments via `SmallChange` (arrow buttons)
- Large increments via `LargeChange` (track clicking)
- Direct value setting via `Value` property
- Thumb dragging for precise positioning

✅ **Visual States**
- **Normal**: Default appearance
- **Active**: Mouse hover state
- **Hot**: Mouse over specific elements (thumb, arrows)
- **Pressed**: Mouse button down state
- **Disabled**: Control disabled state

✅ **Context Menu**
- Scroll Here
- Jump to Left/Top or Right/Bottom
- Page Left/Right (or Up/Down)
- Scroll Left/Right (or Up/Down)

✅ **Performance**
- `BeginUpdate()` / `EndUpdate()` for batch operations
- Optimized double-buffered rendering
- Efficient repaint on value changes only

---

## API Reference

### KryptonHScrollBar

```csharp
[Designer(typeof(KryptonScrollBarDesigner))]
[DefaultEvent(nameof(Scroll))]
[DefaultProperty(nameof(Value))]
[ToolboxBitmap(typeof(HScrollBar), "ToolboxBitmaps.KryptonHScrollBar.bmp")]
[DesignerCategory(@"code")]
[Description(@"A horizontal scrollbar control with Krypton theming.")]
public class KryptonHScrollBar : Control
```

### KryptonVScrollBar

```csharp
[Designer(typeof(KryptonScrollBarDesigner))]
[DefaultEvent(nameof(Scroll))]
[DefaultProperty(nameof(Value))]
[ToolboxBitmap(typeof(VScrollBar), "ToolboxBitmaps.KryptonVScrollBar.bmp")]
[DesignerCategory(@"code")]
[Description(@"A vertical scrollbar control with Krypton theming.")]
public class KryptonVScrollBar : Control
```

---

## Properties

### Behavior Properties

#### Minimum

**Type**: `int`  
**Default**: `0`  
**Category**: `Behavior`  
**Description**: Gets or sets the minimum value of the scrollbar.

```csharp
scrollBar.Minimum = 0;
```

**Validation Rules**:
- Must be `>= 0`
- Must be `< Maximum`
- If `Value < Minimum` after setting, `Value` is automatically adjusted to `Minimum`

**Example**:
```csharp
kryptonHScrollBar.Minimum = 10;
kryptonHScrollBar.Maximum = 100;
// Value will be clamped to range [10, 100]
```

---

#### Maximum

**Type**: `int`  
**Default**: `100`  
**Category**: `Behavior`  
**Description**: Gets or sets the maximum value of the scrollbar.

```csharp
scrollBar.Maximum = 1000;
```

**Validation Rules**:
- Must be `>= 1`
- Must be `> Minimum`
- If `Value > Maximum` after setting, `Value` is automatically adjusted to `Maximum`
- Automatically adjusts `LargeChange` if it exceeds the valid range

**Example**:
```csharp
kryptonHScrollBar.Minimum = 0;
kryptonHScrollBar.Maximum = 500;
kryptonHScrollBar.Value = 250; // Valid
```

---

#### Value

**Type**: `int`  
**Default**: `0`  
**Category**: `Behavior`  
**Description**: Gets or sets the current value of the scrollbar.

```csharp
scrollBar.Value = 50;
int currentValue = scrollBar.Value;
```

**Validation Rules**:
- Must be `>= Minimum`
- Must be `<= Maximum`
- Setting an invalid value is silently ignored (no exception thrown)
- Automatically raises `Scroll` event with `ScrollEventType.ThumbPosition`

**Example**:
```csharp
// Set value programmatically
kryptonHScrollBar.Value = 75;

// Read current value
int position = kryptonHScrollBar.Value;
Console.WriteLine($"Scroll position: {position}");
```

---

#### SmallChange

**Type**: `int`  
**Default**: `1`  
**Category**: `Behavior`  
**Description**: Gets or sets the amount by which the `Value` property changes when the user clicks the arrow buttons or uses arrow keys.

```csharp
scrollBar.SmallChange = 5;
```

**Validation Rules**:
- Must be `>= 1`
- Must be `< LargeChange`
- Represents the increment/decrement when clicking arrow buttons

**Example**:
```csharp
kryptonHScrollBar.SmallChange = 10;
// Clicking left arrow decreases Value by 10
// Clicking right arrow increases Value by 10
```

---

#### LargeChange

**Type**: `int`  
**Default**: `10`  
**Category**: `Behavior`  
**Description**: Gets or sets the amount by which the `Value` property changes when the user clicks in the scrollbar track or uses Page Up/Page Down keys.

```csharp
scrollBar.LargeChange = 25;
```

**Validation Rules**:
- Must be `>= 2`
- Must be `> SmallChange`
- Must be `<= (Maximum - Minimum)`
- If set to a value greater than the range, it's automatically adjusted
- Affects the visual size of the thumb (larger `LargeChange` = larger thumb)

**Example**:
```csharp
kryptonHScrollBar.Minimum = 0;
kryptonHScrollBar.Maximum = 100;
kryptonHScrollBar.LargeChange = 20;
// Clicking track or Page Up/Down changes Value by 20
// Thumb size represents 20% of the scrollable range
```

**Thumb Size Calculation**:
The thumb size is calculated as: `(LargeChange / Maximum) * TrackLength`

---

### Appearance Properties

#### BorderColor

**Type**: `Color`  
**Default**: `Color.FromArgb(93, 140, 201)`  
**Category**: `Appearance`  
**Description**: Gets or sets the border color of the scrollbar when enabled.

```csharp
scrollBar.BorderColor = Color.Blue;
```

**Example**:
```csharp
kryptonHScrollBar.BorderColor = Color.FromArgb(0, 120, 215); // Modern blue
```

---

#### DisabledBorderColor

**Type**: `Color`  
**Default**: `Color.Gray`  
**Category**: `Appearance`  
**Description**: Gets or sets the border color of the scrollbar when disabled.

```csharp
scrollBar.DisabledBorderColor = Color.LightGray;
```

**Example**:
```csharp
kryptonHScrollBar.DisabledBorderColor = Color.FromArgb(200, 200, 200);
```

---

### Context Menu Property

#### KryptonContextMenu

**Type**: `KryptonContextMenu?`  
**Default**: `null`  
**Category**: `Behavior`  
**Description**: Gets and sets the KryptonContextMenu to show when the user right-clicks the control.

```csharp
var contextMenu = new KryptonContextMenu();
// Add custom items...
scrollBar.KryptonContextMenu = contextMenu;
```

**Behavior**:
- If set to `null`, the built-in default context menu is used
- If set to a custom menu, that menu is shown instead
- Automatically handles disposal events

**Example - Custom Context Menu**:
```csharp
var customMenu = new KryptonContextMenu();
customMenu.Items.Add(new KryptonContextMenuItem("Custom Action", (s, e) => {
    MessageBox.Show("Custom action executed!");
}));

kryptonHScrollBar.KryptonContextMenu = customMenu;
```

**Example - Use Default Menu**:
```csharp
kryptonHScrollBar.KryptonContextMenu = null; // Uses built-in menu
```

---

### State Properties (Theming)

#### StateCommon

**Type**: `PaletteInputControlTripleRedirect`  
**Category**: `Visuals`  
**Description**: Gets access to the common scrollbar appearance entries that other states can override.

```csharp
scrollBar.StateCommon.Back.Color1 = Color.LightBlue;
scrollBar.StateCommon.Border.Color1 = Color.DarkBlue;
```

**Usage**: Define common appearance that applies to all states unless overridden.

**Example**:
```csharp
// Set common background
kryptonHScrollBar.StateCommon.Back.Color1 = Color.FromArgb(240, 240, 240);
kryptonHScrollBar.StateCommon.Back.Color2 = Color.FromArgb(250, 250, 250);
kryptonHScrollBar.StateCommon.Back.ColorStyle = PaletteColorStyle.Linear;

// Set common border
kryptonHScrollBar.StateCommon.Border.Color1 = Color.FromArgb(180, 180, 180);
kryptonHScrollBar.StateCommon.Border.Width = 1;
```

---

#### StateNormal

**Type**: `PaletteInputControlTripleStates`  
**Category**: `Visuals`  
**Description**: Gets access to the normal scrollbar appearance entries.

```csharp
scrollBar.StateNormal.Back.Color1 = Color.White;
```

**Usage**: Override appearance for the normal (default) state.

**Example**:
```csharp
kryptonHScrollBar.StateNormal.Back.Color1 = Color.White;
kryptonHScrollBar.StateNormal.Border.Color1 = Color.Gray;
```

---

#### StateActive

**Type**: `PaletteInputControlTripleStates`  
**Category**: `Visuals`  
**Description**: Gets access to the active scrollbar appearance entries (mouse hover).

```csharp
scrollBar.StateActive.Back.Color1 = Color.LightBlue;
```

**Usage**: Override appearance when the mouse is over the control.

**Example**:
```csharp
kryptonHScrollBar.StateActive.Back.Color1 = Color.FromArgb(230, 240, 255);
kryptonHScrollBar.StateActive.Border.Color1 = Color.FromArgb(0, 120, 215);
```

---

#### StateDisabled

**Type**: `PaletteInputControlTripleStates`  
**Category**: `Visuals`  
**Description**: Gets access to the disabled scrollbar appearance entries.

```csharp
scrollBar.StateDisabled.Back.Color1 = Color.LightGray;
```

**Usage**: Override appearance when the control is disabled.

**Example**:
```csharp
kryptonHScrollBar.StateDisabled.Back.Color1 = Color.FromArgb(240, 240, 240);
kryptonHScrollBar.StateDisabled.Border.Color1 = Color.FromArgb(200, 200, 200);
```

---

### Inherited Properties

#### Text

**Type**: `string?`  
**Browsable**: `false`  
**Description**: The `Text` property is hidden and not used by scrollbar controls. Use `Value` instead.

---

## Methods

### Public Methods

#### BeginUpdate()

**Signature**: `public void BeginUpdate()`

**Description**: Prevents the drawing of the control until `EndUpdate()` is called. Useful for making multiple property changes without intermediate repaints.

**Usage**:
```csharp
scrollBar.BeginUpdate();
try
{
    scrollBar.Minimum = 0;
    scrollBar.Maximum = 1000;
    scrollBar.LargeChange = 100;
    scrollBar.Value = 500;
}
finally
{
    scrollBar.EndUpdate();
}
```

**Performance**: Improves performance when making multiple changes by suppressing repaints.

---

#### EndUpdate()

**Signature**: `public void EndUpdate()`

**Description**: Ends the updating process and allows the control to draw itself again. Must be called after `BeginUpdate()`.

**Usage**: See `BeginUpdate()` example above.

**Note**: Always use `BeginUpdate()` and `EndUpdate()` in a try-finally block to ensure `EndUpdate()` is called even if an exception occurs.

---

### Protected Methods

#### OnScroll(ScrollEventArgs)

**Signature**: `protected virtual void OnScroll(ScrollEventArgs e)`

**Description**: Raises the `Scroll` event. Can be overridden in derived classes to provide custom scroll handling.

**Parameters**:
- `e`: `ScrollEventArgs` containing event data

**Example - Override in Derived Class**:
```csharp
public class CustomScrollBar : KryptonHScrollBar
{
    protected override void OnScroll(ScrollEventArgs e)
    {
        // Custom logic before event
        Console.WriteLine($"Scrolling: {e.NewValue}");
        
        // Call base implementation
        base.OnScroll(e);
        
        // Custom logic after event
    }
}
```

---

## Events

### Scroll

**Type**: `event ScrollEventHandler? Scroll`  
**Category**: `Behavior`  
**Description**: Occurs when the scrollbar value changes through user interaction or programmatic changes.

**Event Arguments**: `ScrollEventArgs`

**ScrollEventArgs Properties**:
- `Type`: `ScrollEventType` - Type of scroll operation
- `OldValue`: `int` - Previous scroll value (-1 for some event types)
- `NewValue`: `int` - New scroll value
- `ScrollOrientation`: `ScrollOrientation` - Horizontal or Vertical

**ScrollEventType Values**:
- `First` - Scrolled to minimum
- `Last` - Scrolled to maximum
- `SmallDecrement` - Small step backward (arrow button)
- `SmallIncrement` - Small step forward (arrow button)
- `LargeDecrement` - Large step backward (track click or Page Up)
- `LargeIncrement` - Large step forward (track click or Page Down)
- `ThumbPosition` - Thumb position changed programmatically
- `ThumbTrack` - Thumb is being dragged
- `EndScroll` - Thumb drag operation ended

**Example**:
```csharp
kryptonHScrollBar.Scroll += (sender, e) =>
{
    Console.WriteLine($"Scroll Type: {e.Type}");
    Console.WriteLine($"Old Value: {e.OldValue}");
    Console.WriteLine($"New Value: {e.NewValue}");
    
    switch (e.Type)
    {
        case ScrollEventType.ThumbTrack:
            // User is dragging the thumb
            UpdatePreview(e.NewValue);
            break;
            
        case ScrollEventType.EndScroll:
            // User finished dragging
            CommitValue(e.NewValue);
            break;
            
        case ScrollEventType.SmallIncrement:
        case ScrollEventType.SmallDecrement:
            // Arrow button clicked
            PlayClickSound();
            break;
    }
};
```

**Example - Update Dependent Control**:
```csharp
private void SetupScrollBar()
{
    kryptonHScrollBar.Minimum = 0;
    kryptonHScrollBar.Maximum = 1000;
    kryptonHScrollBar.LargeChange = 100;
    
    kryptonHScrollBar.Scroll += (sender, e) =>
    {
        // Update a panel's position based on scroll value
        contentPanel.Left = -e.NewValue;
    };
}
```

---

## State Properties & Theming

### Overview

The scrollbar controls support full Krypton theming through state properties. Each state property provides access to background, border, and content styling that can be customized independently.

### State Hierarchy

```
StateCommon (base appearance)
├── StateNormal (default state)
├── StateActive (mouse hover)
└── StateDisabled (disabled state)
```

States inherit from `StateCommon` unless explicitly overridden.

### Palette Structure

Each state property (`StateCommon`, `StateNormal`, `StateActive`, `StateDisabled`) provides:

- **Back**: Background appearance (`PaletteBack`)
- **Border**: Border appearance (`PaletteBorder`)
- **Content**: Content appearance (`PaletteContent`)

### Common Theming Tasks

#### 1. Custom Background Colors

```csharp
// Set common background gradient
kryptonHScrollBar.StateCommon.Back.Color1 = Color.FromArgb(245, 245, 245);
kryptonHScrollBar.StateCommon.Back.Color2 = Color.FromArgb(255, 255, 255);
kryptonHScrollBar.StateCommon.Back.ColorStyle = PaletteColorStyle.Linear;
kryptonHScrollBar.StateCommon.Back.ColorAlign = PaletteRectangleAlign.Local;

// Override active state
kryptonHScrollBar.StateActive.Back.Color1 = Color.FromArgb(230, 240, 255);
kryptonHScrollBar.StateActive.Back.Color2 = Color.FromArgb(200, 220, 255);
```

#### 2. Custom Border Styling

```csharp
// Common border
kryptonHScrollBar.StateCommon.Border.Color1 = Color.FromArgb(180, 180, 180);
kryptonHScrollBar.StateCommon.Border.Width = 1;
kryptonHScrollBar.StateCommon.Border.Rounding = 0;

// Active border (highlighted)
kryptonHScrollBar.StateActive.Border.Color1 = Color.FromArgb(0, 120, 215);
kryptonHScrollBar.StateActive.Border.Width = 2;
```

#### 3. Integration with Global Palette

The controls automatically integrate with `KryptonManager.CurrentGlobalPalette`. When the global palette changes, the controls automatically update their appearance.

```csharp
// Change global theme
KryptonManager.GlobalPalette = new PaletteOffice2013Blue();

// Scrollbars automatically update to match the new theme
```

#### 4. Custom Palette per Control

You can override the global palette for individual controls by setting state properties directly:

```csharp
// Create custom appearance for this specific scrollbar
kryptonHScrollBar.StateCommon.Back.Color1 = Color.Navy;
kryptonHScrollBar.StateCommon.Back.Color2 = Color.Blue;
kryptonHScrollBar.StateCommon.Border.Color1 = Color.White;
```

### Available Palette Styles

The controls use `PaletteBackStyle.InputControlStandalone`, `PaletteBorderStyle.HeaderCalendar`, and `PaletteContentStyle.LabelNormalPanel` by default. These can be customized through the state properties.

---

## Context Menu

### Built-in Context Menu

Both controls include a built-in `KryptonContextMenu` that appears when the user right-clicks the control. The menu provides quick access to common scrollbar operations.

### Menu Items (KryptonHScrollBar)

1. **Scroll Here** - Scrolls to the position where the user right-clicked
2. **Left** - Scrolls to `Minimum` value
3. **Right** - Scrolls to `Maximum` value
4. **Page Left** - Scrolls by `-LargeChange`
5. **Page Right** - Scrolls by `+LargeChange`
6. **Scroll Left** - Scrolls by `-SmallChange`
7. **Scroll Right** - Scrolls by `+SmallChange`

### Menu Items (KryptonVScrollBar)

1. **Scroll Here** - Scrolls to the position where the user right-clicked
2. **Top** - Scrolls to `Minimum` value
3. **Bottom** - Scrolls to `Maximum` value
4. **Page Up** - Scrolls by `-LargeChange`
5. **Page Down** - Scrolls by `+LargeChange`
6. **Scroll Up** - Scrolls by `-SmallChange`
7. **Scroll Down** - Scrolls by `+SmallChange`

### Customizing the Context Menu

#### Replace with Custom Menu

```csharp
var customMenu = new KryptonContextMenu();
customMenu.Items.Add(new KryptonContextMenuItem("Custom Action 1", OnAction1));
customMenu.Items.Add(new KryptonContextMenuSeparator());
customMenu.Items.Add(new KryptonContextMenuItem("Custom Action 2", OnAction2));

kryptonHScrollBar.KryptonContextMenu = customMenu;
```

#### Modify Built-in Menu Items

The built-in menu items are private, but you can access them by creating a custom menu with similar items:

```csharp
var menu = new KryptonContextMenu();
var scrollHere = new KryptonContextMenuItem("Scroll Here", (s, e) => {
    // Custom scroll here logic
    Point mousePos = PointToClient(MousePosition);
    // Calculate and set value based on mouse position
});
menu.Items.Add(scrollHere);
// Add more items...

kryptonHScrollBar.KryptonContextMenu = menu;
```

### Context Menu Theming

The context menu automatically uses the current Krypton palette for theming. You can customize it through the `KryptonContextMenu` state properties:

```csharp
var menu = new KryptonContextMenu();
menu.StateCommon.Back.Color1 = Color.White;
menu.StateNormal.ItemHighlight.Back.Color1 = Color.LightBlue;
kryptonHScrollBar.KryptonContextMenu = menu;
```

---

## Usage Examples

### Example 1: Basic Setup

```csharp
// Create and configure a horizontal scrollbar
var hScrollBar = new KryptonHScrollBar
{
    Location = new Point(10, 10),
    Size = new Size(200, 19),
    Minimum = 0,
    Maximum = 100,
    SmallChange = 1,
    LargeChange = 10,
    Value = 0
};

// Handle scroll events
hScrollBar.Scroll += (sender, e) =>
{
    Console.WriteLine($"Scrolled to: {e.NewValue}");
};

// Add to form
this.Controls.Add(hScrollBar);
```

### Example 2: Scrolling Content Panel

```csharp
private KryptonHScrollBar _hScrollBar;
private KryptonPanel _contentPanel;

private void InitializeScrolling()
{
    // Create scrollbar
    _hScrollBar = new KryptonHScrollBar
    {
        Dock = DockStyle.Bottom,
        Minimum = 0,
        Maximum = 500, // Content width - visible width
        LargeChange = 100, // Visible width
        SmallChange = 10
    };
    
    // Create content panel
    _contentPanel = new KryptonPanel
    {
        Dock = DockStyle.Fill,
        Width = 500 // Total content width
    };
    
    // Handle scrolling
    _hScrollBar.Scroll += (sender, e) =>
    {
        _contentPanel.Left = -e.NewValue;
    };
    
    // Add to form
    this.Controls.Add(_contentPanel);
    this.Controls.Add(_hScrollBar);
}
```

### Example 3: Synchronized Horizontal and Vertical Scrollbars

```csharp
private KryptonHScrollBar _hScrollBar;
private KryptonVScrollBar _vScrollBar;
private KryptonPanel _contentPanel;

private void SetupScrolling()
{
    int contentWidth = 1000;
    int contentHeight = 800;
    int visibleWidth = 400;
    int visibleHeight = 300;
    
    // Horizontal scrollbar
    _hScrollBar = new KryptonHScrollBar
    {
        Dock = DockStyle.Bottom,
        Minimum = 0,
        Maximum = contentWidth - visibleWidth,
        LargeChange = visibleWidth,
        SmallChange = 20
    };
    
    // Vertical scrollbar
    _vScrollBar = new KryptonVScrollBar
    {
        Dock = DockStyle.Right,
        Minimum = 0,
        Maximum = contentHeight - visibleHeight,
        LargeChange = visibleHeight,
        SmallChange = 20
    };
    
    // Content panel
    _contentPanel = new KryptonPanel
    {
        Dock = DockStyle.Fill,
        Size = new Size(contentWidth, contentHeight)
    };
    
    // Handle horizontal scrolling
    _hScrollBar.Scroll += (sender, e) =>
    {
        _contentPanel.Left = -e.NewValue;
    };
    
    // Handle vertical scrolling
    _vScrollBar.Scroll += (sender, e) =>
    {
        _contentPanel.Top = -e.NewValue;
    };
    
    // Add to form
    this.Controls.Add(_contentPanel);
    this.Controls.Add(_hScrollBar);
    this.Controls.Add(_vScrollBar);
}
```

### Example 4: Custom Theming

```csharp
private void ApplyCustomTheme(KryptonHScrollBar scrollBar)
{
    // Common state - applies to all states
    scrollBar.StateCommon.Back.Color1 = Color.FromArgb(240, 240, 240);
    scrollBar.StateCommon.Back.Color2 = Color.FromArgb(255, 255, 255);
    scrollBar.StateCommon.Back.ColorStyle = PaletteColorStyle.Linear;
    
    scrollBar.StateCommon.Border.Color1 = Color.FromArgb(180, 180, 180);
    scrollBar.StateCommon.Border.Width = 1;
    
    // Normal state
    scrollBar.StateNormal.Back.Color1 = Color.White;
    
    // Active state (mouse hover)
    scrollBar.StateActive.Back.Color1 = Color.FromArgb(230, 240, 255);
    scrollBar.StateActive.Back.Color2 = Color.FromArgb(200, 220, 255);
    scrollBar.StateActive.Border.Color1 = Color.FromArgb(0, 120, 215);
    
    // Disabled state
    scrollBar.StateDisabled.Back.Color1 = Color.FromArgb(245, 245, 245);
    scrollBar.StateDisabled.Border.Color1 = Color.FromArgb(200, 200, 200);
    
    // Border colors
    scrollBar.BorderColor = Color.FromArgb(0, 120, 215);
    scrollBar.DisabledBorderColor = Color.Gray;
}
```

### Example 5: Programmatic Scrolling with Animation

```csharp
private async Task AnimateScroll(KryptonHScrollBar scrollBar, int targetValue, int durationMs)
{
    int startValue = scrollBar.Value;
    int steps = 30;
    int delay = durationMs / steps;
    int delta = targetValue - startValue;
    
    scrollBar.BeginUpdate();
    try
    {
        for (int i = 0; i <= steps; i++)
        {
            double progress = (double)i / steps;
            // Ease-out animation
            progress = 1 - Math.Pow(1 - progress, 3);
            
            int newValue = startValue + (int)(delta * progress);
            scrollBar.Value = newValue;
            
            await Task.Delay(delay);
        }
    }
    finally
    {
        scrollBar.EndUpdate();
    }
}

// Usage
await AnimateScroll(kryptonHScrollBar, 500, 1000); // Scroll to 500 over 1 second
```

### Example 6: Data Binding Simulation

```csharp
private void BindToData(KryptonHScrollBar scrollBar, int dataItemCount, int itemsPerPage)
{
    scrollBar.Minimum = 0;
    scrollBar.Maximum = Math.Max(0, dataItemCount - itemsPerPage);
    scrollBar.LargeChange = itemsPerPage;
    scrollBar.SmallChange = 1;
    scrollBar.Value = 0;
    
    scrollBar.Scroll += (sender, e) =>
    {
        int startIndex = e.NewValue;
        int endIndex = Math.Min(startIndex + itemsPerPage, dataItemCount);
        
        // Load and display data items from startIndex to endIndex
        LoadDataItems(startIndex, endIndex);
    };
}
```

### Example 7: Custom Context Menu

```csharp
private void SetupCustomContextMenu(KryptonHScrollBar scrollBar)
{
    var menu = new KryptonContextMenu();
    
    // Scroll Here
    menu.Items.Add(new KryptonContextMenuItem("Scroll Here", (s, e) =>
    {
        Point mousePos = scrollBar.PointToClient(MousePosition);
        int trackLength = scrollBar.Width - (2 * 17); // Account for arrow buttons
        double percent = (double)mousePos.X / trackLength;
        int newValue = (int)(percent * (scrollBar.Maximum - scrollBar.Minimum)) + scrollBar.Minimum;
        scrollBar.Value = Math.Max(scrollBar.Minimum, Math.Min(scrollBar.Maximum, newValue));
    }));
    
    menu.Items.Add(new KryptonContextMenuSeparator());
    
    // Jump to positions
    menu.Items.Add(new KryptonContextMenuItem("25%", (s, e) =>
    {
        scrollBar.Value = scrollBar.Minimum + (scrollBar.Maximum - scrollBar.Minimum) / 4;
    }));
    
    menu.Items.Add(new KryptonContextMenuItem("50%", (s, e) =>
    {
        scrollBar.Value = scrollBar.Minimum + (scrollBar.Maximum - scrollBar.Minimum) / 2;
    }));
    
    menu.Items.Add(new KryptonContextMenuItem("75%", (s, e) =>
    {
        scrollBar.Value = scrollBar.Minimum + 3 * (scrollBar.Maximum - scrollBar.Minimum) / 4;
    }));
    
    scrollBar.KryptonContextMenu = menu;
}
```

---

## Keyboard Navigation

The scrollbar controls support full keyboard navigation for accessibility and power users.

### Supported Keys

#### KryptonHScrollBar

| Key | Action | Event Type |
|-----|--------|------------|
| `Left Arrow` | Decrease by `SmallChange` | `SmallDecrement` |
| `Right Arrow` | Increase by `SmallChange` | `SmallIncrement` |
| `Page Up` | Decrease by `LargeChange` | `LargeDecrement` |
| `Page Down` | Increase by `LargeChange` | `LargeIncrement` |
| `Home` | Set to `Minimum` | `First` |
| `End` | Set to `Maximum` | `Last` |

#### KryptonVScrollBar

| Key | Action | Event Type |
|-----|--------|------------|
| `Up Arrow` | Decrease by `SmallChange` | `SmallDecrement` |
| `Down Arrow` | Increase by `SmallChange` | `SmallIncrement` |
| `Page Up` | Decrease by `LargeChange` | `LargeDecrement` |
| `Page Down` | Increase by `LargeChange` | `LargeIncrement` |
| `Home` | Set to `Minimum` | `First` |
| `End` | Set to `Maximum` | `Last` |

### Keyboard Focus

The control must have focus to receive keyboard input. Use `Focus()` method or `TabIndex` property to enable keyboard navigation.

```csharp
// Enable keyboard navigation
kryptonHScrollBar.TabIndex = 0;
kryptonHScrollBar.Focus();
```

### Example: Keyboard-Only Navigation

```csharp
private void SetupKeyboardNavigation()
{
    kryptonHScrollBar.TabIndex = 0;
    kryptonHScrollBar.GotFocus += (s, e) =>
    {
        // Visual indicator that control has focus
        kryptonHScrollBar.StateActive.Border.Color1 = Color.Red;
        kryptonHScrollBar.StateActive.Border.Width = 2;
    };
    
    kryptonHScrollBar.LostFocus += (s, e) =>
    {
        // Remove focus indicator
        kryptonHScrollBar.StateActive.Border.Width = 1;
    };
}
```

---

## Best Practices

### 1. Range Configuration

**✅ DO**:
```csharp
// Set Minimum and Maximum before Value
scrollBar.Minimum = 0;
scrollBar.Maximum = 1000;
scrollBar.Value = 500;
```

**❌ DON'T**:
```csharp
// Setting Value before Maximum may cause issues
scrollBar.Value = 500;
scrollBar.Maximum = 1000; // Value might be invalid during this assignment
```

### 2. LargeChange Configuration

**✅ DO**:
```csharp
// LargeChange should represent visible portion
scrollBar.Maximum = 1000;
scrollBar.LargeChange = 100; // Represents 10% of range
```

**❌ DON'T**:
```csharp
// LargeChange greater than range
scrollBar.Maximum = 100;
scrollBar.LargeChange = 150; // Will be automatically clamped, but confusing
```

### 3. Batch Updates

**✅ DO**:
```csharp
scrollBar.BeginUpdate();
try
{
    scrollBar.Minimum = 0;
    scrollBar.Maximum = 1000;
    scrollBar.LargeChange = 100;
    scrollBar.Value = 500;
}
finally
{
    scrollBar.EndUpdate();
}
```

**❌ DON'T**:
```csharp
// Multiple updates without BeginUpdate/EndUpdate
scrollBar.Minimum = 0; // Repaint
scrollBar.Maximum = 1000; // Repaint
scrollBar.LargeChange = 100; // Repaint
scrollBar.Value = 500; // Repaint
// 4 repaints instead of 1
```

### 4. Event Handling

**✅ DO**:
```csharp
// Handle ThumbTrack for real-time updates
scrollBar.Scroll += (sender, e) =>
{
    if (e.Type == ScrollEventType.ThumbTrack)
    {
        UpdatePreview(e.NewValue); // Lightweight preview update
    }
    else if (e.Type == ScrollEventType.EndScroll)
    {
        CommitValue(e.NewValue); // Expensive commit operation
    }
};
```

**❌ DON'T**:
```csharp
// Expensive operations on every scroll event
scrollBar.Scroll += (sender, e) =>
{
    SaveToDatabase(e.NewValue); // Too expensive for ThumbTrack
    RefreshAllControls(); // Unnecessary work
};
```

### 5. Theming

**✅ DO**:
```csharp
// Set common properties first, then override specific states
scrollBar.StateCommon.Back.Color1 = Color.White;
scrollBar.StateActive.Back.Color1 = Color.LightBlue; // Override for active
```

**❌ DON'T**:
```csharp
// Setting individual state properties without common base
scrollBar.StateNormal.Back.Color1 = Color.White;
scrollBar.StateActive.Back.Color1 = Color.LightBlue;
scrollBar.StateDisabled.Back.Color1 = Color.Gray;
// Better to set StateCommon and override only what's different
```

### 6. Memory Management

**✅ DO**:
```csharp
// Unsubscribe from events when control is disposed
scrollBar.Scroll -= OnScroll;
scrollBar.Dispose();
```

**❌ DON'T**:
```csharp
// Leaving event handlers attached can cause memory leaks
// Always unsubscribe in Dispose or FormClosing
```

### 7. Thread Safety

**⚠️ IMPORTANT**: Scrollbar controls are not thread-safe. All property changes and event handling must occur on the UI thread.

**✅ DO**:
```csharp
if (InvokeRequired)
{
    Invoke(new Action(() => scrollBar.Value = newValue));
}
else
{
    scrollBar.Value = newValue;
}
```

---

## Comparison with Standard WinForms ScrollBars

### Feature Comparison

| Feature | HScrollBar/VScrollBar | KryptonHScrollBar/KryptonVScrollBar |
|---------|----------------------|-------------------------------------|
| **Theming** | System theme only | Full Krypton palette support |
| **Visual States** | Limited | Normal, Active, Disabled, Hot, Pressed |
| **Context Menu** | None | Built-in KryptonContextMenu |
| **Customization** | Limited | Extensive via state properties |
| **Border Colors** | System default | Customizable `BorderColor` and `DisabledBorderColor` |
| **Performance** | Standard | Optimized with `BeginUpdate`/`EndUpdate` |
| **Keyboard Support** | ✅ | ✅ |
| **Mouse Interaction** | ✅ | ✅ Enhanced with visual feedback |
| **Thumb Size** | Automatic | Automatic (based on `LargeChange`) |
| **Event Types** | Basic | Enhanced with detailed `ScrollEventType` |

### Migration Guide

#### From HScrollBar to KryptonHScrollBar

```csharp
// Old code
HScrollBar hScroll = new HScrollBar
{
    Minimum = 0,
    Maximum = 100,
    Value = 50
};

// New code (drop-in replacement)
KryptonHScrollBar hScroll = new KryptonHScrollBar
{
    Minimum = 0,
    Maximum = 100,
    Value = 50
};

// Additional benefits
hScroll.StateCommon.Back.Color1 = Color.White; // Custom theming
hScroll.KryptonContextMenu = customMenu; // Custom context menu
```

#### Event Handler Compatibility

```csharp
// Standard Scroll event handler works the same
void OnScroll(object sender, ScrollEventArgs e)
{
    // Works with both HScrollBar and KryptonHScrollBar
    int value = ((ScrollBar)sender).Value;
}

// Attach to either control
standardScrollBar.Scroll += OnScroll;
kryptonScrollBar.Scroll += OnScroll;
```

### When to Use Krypton ScrollBars

**✅ Use Krypton ScrollBars when**:
- You need consistent theming with other Krypton controls
- You want custom visual appearance
- You need a themed context menu
- You're building a Krypton-themed application

**✅ Use Standard ScrollBars when**:
- You need maximum performance (minimal overhead)
- You want system-native appearance
- You're not using Krypton elsewhere in the application
- You need maximum compatibility with legacy code

---

## Troubleshooting

### Common Issues

#### Issue: Scrollbar doesn't respond to mouse clicks

**Symptoms**: Clicking arrow buttons or track doesn't change the value.

**Possible Causes**:
1. Control is disabled (`Enabled = false`)
2. `Minimum == Maximum` (no scrollable range)
3. Control doesn't have focus
4. Control is behind another control

**Solutions**:
```csharp
// Check enabled state
if (!scrollBar.Enabled)
{
    scrollBar.Enabled = true;
}

// Check range
if (scrollBar.Minimum >= scrollBar.Maximum)
{
    scrollBar.Maximum = scrollBar.Minimum + 1;
}

// Ensure control is visible and has focus
scrollBar.Visible = true;
scrollBar.BringToFront();
scrollBar.Focus();
```

---

#### Issue: Thumb size is incorrect

**Symptoms**: Thumb appears too large or too small.

**Possible Causes**:
1. `LargeChange` is not properly configured
2. `Maximum` value is incorrect
3. Control size is too small

**Solutions**:
```csharp
// Thumb size is calculated as: (LargeChange / Maximum) * TrackLength
// Ensure LargeChange represents visible portion
scrollBar.Maximum = 1000;
scrollBar.LargeChange = 100; // 10% of range = 10% thumb size

// If thumb should represent visible area
int visibleWidth = 400;
int totalWidth = 1000;
scrollBar.Maximum = totalWidth;
scrollBar.LargeChange = visibleWidth; // Thumb represents visible portion
```

---

#### Issue: Scroll event fires too frequently

**Symptoms**: Performance issues during thumb dragging.

**Possible Causes**:
1. Expensive operations in `Scroll` event handler
2. Not checking `ScrollEventType`

**Solutions**:
```csharp
// Only do expensive operations on EndScroll
scrollBar.Scroll += (sender, e) =>
{
    if (e.Type == ScrollEventType.ThumbTrack)
    {
        // Lightweight preview update only
        UpdatePreview(e.NewValue);
    }
    else if (e.Type == ScrollEventType.EndScroll)
    {
        // Expensive commit operation
        CommitChanges(e.NewValue);
    }
};
```

---

#### Issue: Context menu doesn't appear

**Symptoms**: Right-click doesn't show context menu.

**Possible Causes**:
1. `KryptonContextMenu` is `null`
2. Control is disabled
3. Another control is handling the right-click

**Solutions**:
```csharp
// Ensure context menu is set
if (scrollBar.KryptonContextMenu == null)
{
    // Built-in menu should be created automatically
    // If not, create one
    scrollBar.KryptonContextMenu = new KryptonContextMenu();
}

// Check if control is enabled
scrollBar.Enabled = true;

// Ensure no other control is intercepting mouse events
```

---

#### Issue: Value doesn't change when set programmatically

**Symptoms**: Setting `Value` property has no effect.

**Possible Causes**:
1. Value is outside `Minimum`/`Maximum` range
2. Value is same as current value
3. Control is in `BeginUpdate()` mode

**Solutions**:
```csharp
// Check range
int newValue = 150;
if (newValue >= scrollBar.Minimum && newValue <= scrollBar.Maximum)
{
    scrollBar.Value = newValue;
}
else
{
    // Adjust range first
    scrollBar.Maximum = Math.Max(scrollBar.Maximum, newValue);
    scrollBar.Value = newValue;
}

// Ensure not in update mode
scrollBar.EndUpdate(); // If BeginUpdate was called
scrollBar.Value = newValue;
```

---

#### Issue: Theming doesn't apply

**Symptoms**: State properties don't change appearance.

**Possible Causes**:
1. Global palette override
2. State properties not set correctly
3. Control needs refresh

**Solutions**:
```csharp
// Force refresh after theme changes
scrollBar.StateCommon.Back.Color1 = Color.Red;
scrollBar.Invalidate();
scrollBar.Refresh();

// Check if global palette is overriding
// State properties should override global palette
scrollBar.StateNormal.Back.Color1 = Color.Blue; // Should override global
```

---

#### Issue: Keyboard navigation doesn't work

**Symptoms**: Arrow keys don't scroll.

**Possible Causes**:
1. Control doesn't have focus
2. `TabIndex` not set
3. Another control is consuming keyboard events

**Solutions**:
```csharp
// Set TabIndex and focus
scrollBar.TabIndex = 0;
scrollBar.Focus();

// Ensure control can receive focus
scrollBar.Enabled = true;
scrollBar.Visible = true;

// Check for key event handlers that might be consuming keys
// Remove any KeyDown/KeyUp handlers that return true without calling base
```

---

### Performance Optimization

#### Reduce Repaints

```csharp
// Use BeginUpdate/EndUpdate for multiple changes
scrollBar.BeginUpdate();
try
{
    scrollBar.Minimum = 0;
    scrollBar.Maximum = 1000;
    scrollBar.LargeChange = 100;
    scrollBar.SmallChange = 5;
    scrollBar.Value = 500;
}
finally
{
    scrollBar.EndUpdate(); // Single repaint
}
```

#### Optimize Event Handlers

```csharp
// Cache frequently accessed values
private int _cachedMaximum;

scrollBar.Scroll += (sender, e) =>
{
    // Avoid property access in tight loop
    if (e.NewValue > _cachedMaximum / 2)
    {
        // Do something
    }
};
```

---

## Additional Resources

### Related Controls

- **KryptonScrollBar** - Unified scrollbar with orientation property
- **KryptonTrackBar** - Similar range control with different interaction model
- **KryptonProgressBar** - Visual progress indicator

### Related Documentation

- [Krypton Palette System](palette-mechanics-intro.md)
- [KryptonContextMenu Documentation](KryptonContextMenu.md)
- [Krypton Control Theming Guide](theming-guide.md)

### Code Examples

See the `TestForm` project in `Source/Krypton Components/TestForm/` for working examples of scrollbar usage.

---

## Changelog

### Version 1.0 (2026-01)

- ✅ Initial implementation of `KryptonHScrollBar` and `KryptonVScrollBar`
- ✅ Full Krypton theming support with state properties
- ✅ Built-in KryptonContextMenu integration
- ✅ Complete keyboard navigation support
- ✅ Mouse interaction with visual feedback
- ✅ `BeginUpdate`/`EndUpdate` for performance
- ✅ Customizable border colors
- ✅ Comprehensive scroll event types

---

## Support

For issues or questions:

1. **Check this documentation** - Most common scenarios are covered
2. **Review code examples** - See `TestForm` for working implementations
3. **Check Krypton forums** - Community support available
4. **Create GitHub Issue** - Include code samples and error messages

---

**Last Updated**: January 2026  
**Maintainer**: Krypton Toolkit Team  
**Status**: Production Ready ✅
