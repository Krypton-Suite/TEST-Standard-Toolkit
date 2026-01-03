# Overlay Image Feature

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Enumerations](#enumerations)
5. [Classes](#classes)
6. [Interfaces](#interfaces)
7. [Usage Examples](#usage-examples)
8. [Designer Support](#designer-support)
9. [Implementation Details](#implementation-details)
10. [Best Practices](#best-practices)
11. [Troubleshooting](#troubleshooting)
12. [Related Issues](#related-issues)

---

## Overview

The Overlay Image feature allows you to display a secondary image (overlay) on top of the main image in Krypton controls. This is particularly useful for displaying badges, indicators, notification counts, or status icons on buttons and labels.

### Key Features

- **Position Control**: Place overlay images at any of the four corners (TopLeft, TopRight, BottomLeft, BottomRight)
- **Flexible Scaling**: Multiple scaling modes to control overlay size relative to the main image
- **Designer Support**: Full Visual Studio designer integration with expandable properties
- **DPI Awareness**: Automatic DPI scaling support
- **Orientation Support**: Works correctly with all visual orientations
- **Transparent Color**: Support for color-key transparency

### Supported Controls

- `KryptonButton`
- `KryptonLabel`
- Any control that implements `IContentValues` interface

---

## Quick Start

### Basic Usage

```csharp
// Create a button with an overlay image
var button = new KryptonButton();
button.Values.Image = mainImage; // Your main button image

// Configure overlay image
button.Values.OverlayImage.Image = overlayImage; // Your overlay/badge image
button.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
button.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
button.Values.OverlayImage.ScaleFactor = 0.3f; // 30% of main image size
```

### Designer Usage

1. Select a `KryptonButton` or `KryptonLabel` in the designer
2. In the Properties window, expand the `Values` property
3. Expand the `OverlayImage` property (it appears as an expandable object)
4. Set the `Image`, `Position`, `ScaleMode`, and other properties as needed

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Enumerations

### OverlayImagePosition

Specifies the position of an overlay image relative to the main image.

```csharp
public enum OverlayImagePosition
{
    /// <summary>
    /// Specifies the overlay image is positioned at the top-left corner.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Specifies the overlay image is positioned at the top-right corner.
    /// </summary>
    TopRight,

    /// <summary>
    /// Specifies the overlay image is positioned at the bottom-left corner.
    /// </summary>
    BottomLeft,

    /// <summary>
    /// Specifies the overlay image is positioned at the bottom-right corner.
    /// </summary>
    BottomRight
}
```

**Default Value**: `TopRight`

**Usage Example**:
```csharp
button.Values.OverlayImage.Position = OverlayImagePosition.BottomRight;
```

---

### OverlayImageScaleMode

Specifies how an overlay image should be scaled relative to the main image.

```csharp
public enum OverlayImageScaleMode
{
    /// <summary>
    /// Use the actual size of the overlay image without scaling.
    /// </summary>
    None,

    /// <summary>
    /// Scale the overlay image as a percentage of the main image size.
    /// Maintains aspect ratio using the smaller dimension of the main image.
    /// </summary>
    Percentage,

    /// <summary>
    /// Scale the overlay image to a fixed size.
    /// </summary>
    FixedSize,

    /// <summary>
    /// Scale the overlay image proportionally to maintain aspect ratio,
    /// using the smaller dimension of the main image as reference.
    /// </summary>
    ProportionalToMain
}
```

**Default Value**: `None`

**Usage Example**:
```csharp
button.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
button.Values.OverlayImage.ScaleFactor = 0.4f; // 40% of main image
```

---

## Classes

### OverlayImageValues

Storage class for overlay image value information. This class uses `ExpandableObjectConverter` for designer support.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Image` | `Image?` | `null` | The overlay image to display on top of the main image |
| `ImageTransparentColor` | `Color` | `Color.Empty` | Color that should be treated as transparent in the overlay image |
| `Position` | `OverlayImagePosition` | `TopRight` | Position of the overlay image relative to the main image |
| `ScaleMode` | `OverlayImageScaleMode` | `None` | How the overlay image should be scaled |
| `ScaleFactor` | `float` | `0.5f` | Scale factor for percentage/proportional modes (0.1 to 2.0) |
| `FixedSize` | `Size` | `16, 16` | Fixed size when ScaleMode is FixedSize |

#### Methods

##### `CopyFrom(OverlayImageValues source)`

Copies all overlay image values from another instance.

```csharp
var source = new OverlayImageValues(needPaint);
source.Image = myOverlayImage;
source.Position = OverlayImagePosition.TopLeft;

var target = new OverlayImageValues(needPaint);
target.CopyFrom(source); // Copies all properties
```

#### Serialization Methods

- `ShouldSerializeImage()` - Returns `true` if Image is not null
- `ShouldSerializeImageTransparentColor()` - Returns `true` if color is not empty
- `ShouldSerializePosition()` - Returns `true` if position is not TopRight
- `ShouldSerializeScaleMode()` - Returns `true` if scale mode is not None
- `ShouldSerializeScaleFactor()` - Returns `true` if scale factor is not 0.5
- `ShouldSerializeFixedSize()` - Returns `true` if fixed size is not 16x16

#### Reset Methods

- `ResetImage()` - Sets Image to null
- `ResetImageTransparentColor()` - Sets color to Color.Empty
- `ResetPosition()` - Sets position to TopRight
- `ResetScaleMode()` - Sets scale mode to None
- `ResetScaleFactor()` - Sets scale factor to 0.5f
- `ResetFixedSize()` - Sets fixed size to 16x16

---

## Interfaces

### IContentValues Extensions

The `IContentValues` interface has been extended with the following methods:

#### `GetOverlayImage(PaletteState state)`

Gets the overlay image for the specified state.

```csharp
Image? GetOverlayImage(PaletteState state);
```

**Parameters**:
- `state`: The palette state for which the overlay image is needed

**Returns**: The overlay image, or `null` if no overlay image is set

**Example**:
```csharp
public class CustomContentValues : IContentValues
{
    public Image? GetOverlayImage(PaletteState state)
    {
        // Return different overlay based on state
        return state == PaletteState.Disabled ? null : _overlayImage;
    }
}
```

#### `GetOverlayImageTransparentColor(PaletteState state)`

Gets the transparent color for the overlay image.

```csharp
Color GetOverlayImageTransparentColor(PaletteState state);
```

**Returns**: The color to treat as transparent, or `Color.Empty` if none

#### `GetOverlayImagePosition(PaletteState state)`

Gets the position of the overlay image.

```csharp
OverlayImagePosition GetOverlayImagePosition(PaletteState state);
```

**Returns**: The overlay image position

#### `GetOverlayImageScaleMode(PaletteState state)`

Gets the scaling mode for the overlay image.

```csharp
OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state);
```

**Returns**: The overlay image scale mode

#### `GetOverlayImageScaleFactor(PaletteState state)`

Gets the scale factor for the overlay image.

```csharp
float GetOverlayImageScaleFactor(PaletteState state);
```

**Returns**: Scale factor (0.1 to 2.0)

#### `GetOverlayImageFixedSize(PaletteState state)`

Gets the fixed size for the overlay image.

```csharp
Size GetOverlayImageFixedSize(PaletteState state);
```

**Returns**: Fixed size when ScaleMode is FixedSize

---

## Usage Examples

### Example 1: Basic Overlay Image

```csharp
var button = new KryptonButton
{
    Text = "Messages",
    Values = { Image = Properties.Resources.MailIcon }
};

// Add notification badge overlay
button.Values.OverlayImage.Image = CreateNotificationBadge(5);
button.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
button.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.FixedSize;
button.Values.OverlayImage.FixedSize = new Size(24, 24);
```

### Example 2: Percentage Scaling

```csharp
var button = new KryptonButton
{
    Values = { Image = mainIcon }
};

// Overlay scales to 30% of main image size
button.Values.OverlayImage.Image = statusIcon;
button.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
button.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
button.Values.OverlayImage.ScaleFactor = 0.3f;
```

### Example 3: Dynamic Overlay Updates

```csharp
private void UpdateNotificationCount(int count)
{
    if (count > 0)
    {
        // Create badge with count
        var badge = CreateBadgeImage(count, Color.Red);
        button.Values.OverlayImage.Image = badge;
        button.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
    }
    else
    {
        // Hide overlay when no notifications
        button.Values.OverlayImage.Image = null;
    }
    button.Invalidate();
}

private Image CreateBadgeImage(int count, Color color)
{
    var bitmap = new Bitmap(24, 24);
    using (var g = Graphics.FromImage(bitmap))
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(color))
        {
            g.FillEllipse(brush, 0, 0, 24, 24);
        }
        using (var font = new Font("Arial", 10, FontStyle.Bold))
        using (var brush = new SolidBrush(Color.White))
        using (var sf = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        })
        {
            var text = count > 99 ? "99+" : count.ToString();
            var rect = new RectangleF(0, 0, 24, 24);
            g.DrawString(text, font, brush, rect, sf);
        }
    }
    return bitmap;
}
```

### Example 4: Label with Overlay

```csharp
var label = new KryptonLabel
{
    Text = "Status: Online",
    Values = { Image = statusIcon }
};

// Add online indicator overlay
label.Values.OverlayImage.Image = CreateIndicator(Color.Green);
label.Values.OverlayImage.Position = OverlayImagePosition.BottomRight;
label.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.ProportionalToMain;
label.Values.OverlayImage.ScaleFactor = 0.4f;
```

### Example 5: State-Based Overlays

```csharp
public class StatefulOverlayValues : ButtonValues
{
    public override Image? GetOverlayImage(PaletteState state)
    {
        return state switch
        {
            PaletteState.Disabled => null,
            PaletteState.Pressed => _pressedOverlay,
            PaletteState.Tracking => _hoverOverlay,
            _ => _normalOverlay
        };
    }

    public override OverlayImagePosition GetOverlayImagePosition(PaletteState state)
    {
        // Could vary position based on state if needed
        return OverlayImagePosition.TopRight;
    }
}
```

### Example 6: Custom IContentValues Implementation

```csharp
public class CustomListItem : IContentValues
{
    private Image? _mainImage;
    private Image? _overlayImage;
    private OverlayImageValues _overlayValues;

    public CustomListItem()
    {
        _overlayValues = new OverlayImageValues(() => { });
    }

    public Image? GetImage(PaletteState state) => _mainImage;

    public Image? GetOverlayImage(PaletteState state) => _overlayImage;

    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) 
        => _overlayValues.Position;

    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) 
        => _overlayValues.ScaleMode;

    public float GetOverlayImageScaleFactor(PaletteState state) 
        => _overlayValues.ScaleFactor;

    public Size GetOverlayImageFixedSize(PaletteState state) 
        => _overlayValues.FixedSize;

    public Color GetOverlayImageTransparentColor(PaletteState state) 
        => _overlayValues.ImageTransparentColor;

    // ... other IContentValues members
}
```

---

## Designer Support

### Property Grid Integration

The `OverlayImageValues` class uses `ExpandableObjectConverter`, which means in the Visual Studio designer:

1. The `OverlayImage` property appears as an expandable node in the Properties window
2. All overlay image properties are grouped under this node
3. Properties can be edited directly in the designer
4. Changes are serialized to the `.Designer.cs` file

### Designer Code Generation

When you configure overlay images in the designer, code similar to this is generated:

```csharp
// 
// button1
// 
this.button1.Values.OverlayImage.Image = ((System.Drawing.Image)(resources.GetObject("button1.Values.OverlayImage.Image")));
this.button1.Values.OverlayImage.ImageTransparentColor = System.Drawing.Color.Empty;
this.button1.Values.OverlayImage.Position = Krypton.Toolkit.OverlayImagePosition.TopRight;
this.button1.Values.OverlayImage.ScaleMode = Krypton.Toolkit.OverlayImageScaleMode.Percentage;
this.button1.Values.OverlayImage.ScaleFactor = 0.3F;
```

### Designer Best Practices

1. **Use Resource Files**: Store overlay images in resource files for better designer support
2. **Set Defaults**: Use sensible defaults so controls look good even before customization
3. **Test in Designer**: Verify overlay images display correctly in design mode
4. **Document Custom Values**: If creating custom `IContentValues`, ensure proper designer attributes

---

## Implementation Details

### Control Implementation

#### KryptonButton and KryptonDropButton

`KryptonButton` inherits from `KryptonDropButton`, which implements the `IContentValues` interface. The overlay image methods in `KryptonDropButton` delegate to the `Values` property (which is a `ButtonValues` instance):

```csharp
public Image? GetOverlayImage(PaletteState state) => Values.GetOverlayImage(state);
public Color GetOverlayImageTransparentColor(PaletteState state) => Values.GetOverlayImageTransparentColor(state);
public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => Values.GetOverlayImagePosition(state);
// ... other overlay methods
```

This means all button types (`KryptonButton`, `KryptonDropButton`, etc.) automatically support overlay images through their `Values.OverlayImage` property.

#### KryptonLabel

`KryptonLabel` also implements `IContentValues` and delegates overlay image methods to its `Values` property (which is a `LabelValues` instance).

### Rendering Pipeline

The overlay image rendering follows this flow:

1. **Layout Phase** (`LayoutContent`):
   - Main image is laid out first
   - Overlay image position is calculated relative to main image rectangle
   - Scaling is applied based on `ScaleMode`
   - Overlay rectangle is cached in `StandardContentMemento`

2. **Drawing Phase** (`DrawContent`):
   - Main image is drawn first
   - Overlay image is drawn on top using cached rectangle
   - Both images respect orientation transformations

3. **Orientation Handling** (`AdjustForOrientation`):
   - When control orientation changes, overlay position is recalculated
   - Maintains correct corner position relative to main image

### Scaling Algorithms

#### Percentage Mode

```csharp
// Uses smaller dimension of main image as reference
float mainImageMinDim = Math.Min(mainImageRect.Width, mainImageRect.Height);
float targetSize = mainImageMinDim * OverlayImageScaleFactor;

// Calculate scale to fit target size while maintaining aspect ratio
float scale = Math.Min(
    targetSize / originalOverlaySize.Width,
    targetSize / originalOverlaySize.Height);

overlaySize = new Size(
    (int)(originalOverlaySize.Width * scale),
    (int)(originalOverlaySize.Height * scale));
```

#### ProportionalToMain Mode

Same algorithm as Percentage, but explicitly uses the smaller dimension of the main image as the reference point.

#### FixedSize Mode

Uses the exact size specified in `FixedSize` property, regardless of main image size.

### Position Calculation

```csharp
switch (OverlayImagePosition)
{
    case OverlayImagePosition.TopLeft:
        overlayX = mainImageRect.Left;
        overlayY = mainImageRect.Top;
        break;
    case OverlayImagePosition.TopRight:
        overlayX = mainImageRect.Right - overlaySize.Width;
        overlayY = mainImageRect.Top;
        break;
    case OverlayImagePosition.BottomLeft:
        overlayX = mainImageRect.Left;
        overlayY = mainImageRect.Bottom - overlaySize.Height;
        break;
    case OverlayImagePosition.BottomRight:
        overlayX = mainImageRect.Right - overlaySize.Width;
        overlayY = mainImageRect.Bottom - overlaySize.Height;
        break;
}
```

### DPI Scaling

Overlay images automatically respect DPI scaling:
- Images are scaled during layout phase
- Final rendering uses DPI-aware coordinates
- Works correctly on high-DPI displays

### Memory Management

- Overlay images are not automatically disposed by the framework
- You are responsible for disposing images when no longer needed
- Consider using `using` statements or implementing `IDisposable` in custom implementations

---

## Best Practices

### 1. Image Sizing

- **Recommended**: Create overlay images at 2x resolution for high-DPI support
- **Aspect Ratio**: Maintain square aspect ratio for badge-style overlays
- **Size Guidelines**:
  - Small badges: 16x16 to 24x24 pixels
  - Medium badges: 24x24 to 32x32 pixels
  - Large badges: 32x32 to 48x48 pixels

### 2. Scaling Mode Selection

- **None**: Use when overlay image is already the correct size
- **Percentage**: Use for responsive overlays that should scale with main image
- **FixedSize**: Use for consistent badge sizes regardless of main image
- **ProportionalToMain**: Use when you want overlay to maintain relative size to main image

### 3. Performance Considerations

- **Cache Images**: Reuse overlay images instead of creating new ones repeatedly
- **Lazy Loading**: Only create overlay images when needed
- **Dispose Properly**: Dispose images when controls are disposed

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _overlayImage?.Dispose();
        _mainImage?.Dispose();
    }
    base.Dispose(disposing);
}
```

### 4. Transparent Color Usage

```csharp
// Set transparent color if your overlay image has a specific background color
button.Values.OverlayImage.ImageTransparentColor = Color.Magenta;
```

### 5. State Management

For state-specific overlays, implement custom `IContentValues`:

```csharp
public Image? GetOverlayImage(PaletteState state)
{
    return state switch
    {
        PaletteState.Disabled => _disabledOverlay,
        PaletteState.Pressed => _pressedOverlay,
        PaletteState.Tracking => _hoverOverlay,
        _ => _normalOverlay
    };
}
```

### 6. Notification Badges

Common pattern for notification counters:

```csharp
private void UpdateNotificationBadge(int count)
{
    if (count <= 0)
    {
        button.Values.OverlayImage.Image = null;
        return;
    }

    var badge = CreateNotificationBadge(count);
    button.Values.OverlayImage.Image = badge;
    button.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
    button.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.FixedSize;
    button.Values.OverlayImage.FixedSize = new Size(24, 24);
    button.Invalidate();
}
```

---

## Troubleshooting

### Overlay Image Not Appearing

**Problem**: Overlay image is not visible.

**Solutions**:
1. **For KryptonButton**: Verify `Values.Image` property is set (not `Image` - use `button.Values.Image = mainImage`)
2. **For KryptonLabel**: Verify `Values.Image` property is set (not `Image` - use `label.Values.Image = mainImage`)
3. Check that main image exists (overlay only shows when main image is present)
4. Ensure overlay image is not transparent or same color as background
5. Check `ScaleMode` and `ScaleFactor` settings
6. Verify control is not disabled (overlay may be hidden in disabled state)
7. Ensure overlay image is set via `Values.OverlayImage.Image` property

### Overlay Position Incorrect

**Problem**: Overlay appears in wrong position.

**Solutions**:
1. Check `Position` property value
2. Verify main image rectangle is correct
3. Check if orientation transformations are affecting position
4. Ensure overlay size calculation is correct for chosen `ScaleMode`

### Overlay Too Large/Small

**Problem**: Overlay size is not as expected.

**Solutions**:
1. Adjust `ScaleFactor` value (0.1 to 2.0 range)
2. Try different `ScaleMode`:
   - Use `FixedSize` for exact pixel control
   - Use `Percentage` for relative sizing
3. Check original overlay image dimensions
4. Verify main image size

### Performance Issues

**Problem**: Slow rendering with overlay images.

**Solutions**:
1. Cache overlay images instead of recreating
2. Use appropriate image formats (PNG with transparency)
3. Avoid very large overlay images
4. Consider using `FixedSize` mode to avoid scaling calculations

### Designer Issues

**Problem**: Overlay properties not showing in designer.

**Solutions**:
1. Ensure `OverlayImageValues` uses `ExpandableObjectConverter`
2. Check that control implements `IContentValues` correctly
3. Rebuild solution to refresh designer metadata
4. Clear Visual Studio cache if needed

---

## Related Issues

- **GitHub Issue**: [#1205](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1205) - Overlay Images within Krypton Toolkit controls

---

## Code Examples Reference

### Complete Button Example

```csharp
public class NotificationButton : KryptonButton
{
    private int _notificationCount;

    public int NotificationCount
    {
        get => _notificationCount;
        set
        {
            if (_notificationCount != value)
            {
                _notificationCount = value;
                UpdateOverlay();
            }
        }
    }

    public NotificationButton()
    {
        // Set main image
        Values.Image = Properties.Resources.MailIcon;
        Text = "Messages";
        
        // Configure overlay defaults
        Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        Values.OverlayImage.ScaleMode = OverlayImageScaleMode.FixedSize;
        Values.OverlayImage.FixedSize = new Size(24, 24);
        
        UpdateOverlay();
    }

    private void UpdateOverlay()
    {
        if (_notificationCount > 0)
        {
            Values.OverlayImage.Image = CreateBadge(_notificationCount);
        }
        else
        {
            Values.OverlayImage.Image = null;
        }
        Invalidate();
    }

    private Image CreateBadge(int count)
    {
        var bitmap = new Bitmap(24, 24);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Red circular background
            using (var brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, 0, 0, 24, 24);
            }
            
            // White border
            using (var pen = new Pen(Color.White, 2))
            {
                g.DrawEllipse(pen, 1, 1, 22, 22);
            }
            
            // Count text (centered)
            var text = count > 99 ? "99+" : count.ToString();
            using (var font = new Font("Arial", 9, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var rect = new RectangleF(0, 0, 24, 24);
                g.DrawString(text, font, brush, rect, sf);
            }
        }
        return bitmap;
    }
}
```

### Custom Content Values with Overlay

```csharp
public class StatusContentValues : IContentValues
{
    private readonly Image _mainImage;
    private readonly Image? _statusOverlay;
    private readonly OverlayImageValues _overlayValues;

    public StatusContentValues(Image mainImage, Image? statusOverlay)
    {
        _mainImage = mainImage;
        _statusOverlay = statusOverlay;
        _overlayValues = new OverlayImageValues(() => { })
        {
            Image = statusOverlay,
            Position = OverlayImagePosition.BottomRight,
            ScaleMode = OverlayImageScaleMode.Percentage,
            ScaleFactor = 0.3f
        };
    }

    public Image? GetImage(PaletteState state) => _mainImage;
    
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;
    
    public string GetShortText() => "Status";
    
    public string GetLongText() => string.Empty;

    // Overlay image methods
    public Image? GetOverlayImage(PaletteState state) => _overlayValues.Image;
    
    public Color GetOverlayImageTransparentColor(PaletteState state) 
        => _overlayValues.ImageTransparentColor;
    
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) 
        => _overlayValues.Position;
    
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) 
        => _overlayValues.ScaleMode;
    
    public float GetOverlayImageScaleFactor(PaletteState state) 
        => _overlayValues.ScaleFactor;
    
    public Size GetOverlayImageFixedSize(PaletteState state) 
        => _overlayValues.FixedSize;
}
```

---

## Version Information

- **Introduced**: Version 110 (Issue #1205)
- **Namespace**: `Krypton.Toolkit`
- **Assembly**: `Krypton.Toolkit.dll`

---

## See Also

- [Krypton Toolkit Documentation](https://github.com/Krypton-Suite/Standard-Toolkit)
- [TestForm Examples](../Source/Krypton%20Components/TestForm/OverlayImageTest.cs)
- [IContentValues Interface](../Source/Krypton%20Components/Krypton.Toolkit/General/Definitions.cs)

---

## Summary

The Overlay Image feature provides a powerful and flexible way to add secondary images, badges, and indicators to Krypton controls. With support for multiple positions, scaling modes, and full designer integration, it enables rich UI experiences while maintaining simplicity for common use cases.

For questions or issues, please refer to [GitHub Issue #1205](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1205).

---

## Related Features

- [Taskbar Overlay Icon Feature](./taskbar-overlay-icon-feature.md) - For taskbar-level overlay icons on KryptonForm
