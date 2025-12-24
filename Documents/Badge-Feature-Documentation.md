# Badge Feature

## Overview

The Badge feature adds small circular indicators to `KryptonButton` and `KryptonCheckButton` controls, enabling developers to display notifications, counters, status indicators, and icons. Badges are positioned at the corners of buttons and can display either text (numbers, letters, symbols) or images.

This feature was implemented to address [GitHub Issue #2783](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2783) and provides a comprehensive solution for adding visual indicators to buttons in the Krypton Toolkit.

---

## Table of Contents

1. [Architecture](#architecture)
2. [API Reference](#api-reference)
3. [Usage Examples](#usage-examples)
4. [Design-Time Support](#design-time-support)
5. [Implementation Details](#implementation-details)
6. [Best Practices](#best-practices)
7. [Known Limitations](#known-limitations)

---

## Architecture

### Component Hierarchy

The badge feature is integrated into the Krypton Toolkit's rendering system through the following components:

```
KryptonDropButton (base class for KryptonButton/KryptonCheckButton)
    ├── BadgeValues (property storage)
    └── ViewDrawButton
        └── ViewDrawBadge (rendering element)
```

### Key Components

#### 1. `BadgeValues` Class
Located in `Source/Krypton Components/Krypton.Toolkit/Values/BadgeValues.cs`

This class stores all badge-related properties and is exposed as an expandable property in the Visual Studio Property Grid. It inherits from `Storage` and implements the standard Krypton property storage pattern.

#### 2. `ViewDrawBadge` Class
Located in `Source/Krypton Components/Krypton.Toolkit/View Draw/ViewDrawBadge.cs`

This `ViewLeaf` class is responsible for rendering the badge. It handles layout calculations, positioning, and drawing of both text and image badges.

#### 3. `BadgePosition` Enum
Located in `Source/Krypton Components/Krypton.Toolkit/General/Definitions.cs`

Defines the four possible positions where a badge can be displayed on a button.

#### 4. Integration Points

- **KryptonDropButton**: Exposes the `BadgeValues` property and initializes badge rendering
- **ViewDrawButton**: Contains the `ViewDrawBadge` instance and manages its lifecycle
- **Layout System**: Badge positioning is calculated during the layout phase

---

## API Reference

### Enum: `BadgePosition`

Specifies the position of a badge relative to its parent button.

```csharp
public enum BadgePosition
{
    /// <summary>
    /// Specifies the badge is positioned at the top-right corner.
    /// </summary>
    TopRight,

    /// <summary>
    /// Specifies the badge is positioned at the top-left corner.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Specifies the badge is positioned at the bottom-right corner.
    /// </summary>
    BottomRight,

    /// <summary>
    /// Specifies the badge is positioned at the bottom-left corner.
    /// </summary>
    BottomLeft
}
```

**Default Value**: `TopRight`

---

### Enum: `BadgeShape`

Specifies the shape of a badge.

```csharp
public enum BadgeShape
{
    /// <summary>
    /// Specifies a circular badge.
    /// </summary>
    Circle,

    /// <summary>
    /// Specifies a square badge.
    /// </summary>
    Square,

    /// <summary>
    /// Specifies a rounded rectangle badge.
    /// </summary>
    RoundedRectangle
}
```

**Default Value**: `Circle`

---

### Enum: `BadgeAnimation`

Specifies the animation type for a badge.

```csharp
public enum BadgeAnimation
{
    /// <summary>
    /// No animation.
    /// </summary>
    None,

    /// <summary>
    /// Fade in and out animation.
    /// </summary>
    FadeInOut,

    /// <summary>
    /// Pulsing animation (scale and opacity).
    /// </summary>
    Pulse
}
```

**Default Value**: `None`

---

### Class: `BadgeValues`

Storage class for badge value information. This class is exposed via the `BadgeValues` property on `KryptonDropButton` and its derived classes.

#### Properties

##### `Text` (string)

Gets and sets the text displayed on the badge.

- **Type**: `string`
- **Default**: `""` (empty string)
- **Localizable**: Yes
- **Category**: Visuals
- **Description**: The text to display on the badge.

```csharp
button.BadgeValues.Text = "5";
button.BadgeValues.Text = "99+";
button.BadgeValues.Text = "!";
```

**Notes**:
- If both `Text` and `Image` are set, the image takes priority
- Text is rendered in bold Segoe UI font at 7.5pt
- Text is centered both horizontally and vertically within the badge
- Text length should be kept short (1-3 characters recommended for optimal appearance)

##### `Image` (Image?)

Gets and sets the image displayed on the badge.

- **Type**: `Image?` (nullable)
- **Default**: `null`
- **Localizable**: Yes
- **Category**: Visuals
- **Description**: The image to display on the badge. If set, the image will be displayed instead of text.

```csharp
button.BadgeValues.Image = SystemIcons.Error.ToBitmap();
button.BadgeValues.Image = SystemIcons.Warning.ToBitmap();
button.BadgeValues.Image = SystemIcons.Information.ToBitmap();
```

**Notes**:
- If an image is provided, it takes priority over text
- Images are automatically scaled to fit within the badge while maintaining aspect ratio
- Images are centered within the badge
- High-quality bicubic interpolation is used for image rendering
- Recommended image size: 16x16 to 32x32 pixels for best results

##### `BadgeColor` (Color)

Gets and sets the background color of the badge.

- **Type**: `Color`
- **Default**: `Color.Red`
- **Category**: Visuals
- **Description**: The background color of the badge.

```csharp
button.BadgeValues.BadgeColor = Color.Red;
button.BadgeValues.BadgeColor = Color.Blue;
button.BadgeValues.BadgeColor = Color.FromArgb(255, 128, 0);
```

##### `TextColor` (Color)

Gets and sets the color of the text on the badge.

- **Type**: `Color`
- **Default**: `Color.White`
- **Category**: Visuals
- **Description**: The text color of the badge.

```csharp
button.BadgeValues.TextColor = Color.White;
button.BadgeValues.TextColor = Color.Yellow;
```

**Note**: This property only affects text badges. Image badges ignore this property.

##### `Font` (Font?)

Gets and sets the font used to display badge text.

- **Type**: `Font?` (nullable)
- **Default**: `null` (uses default font: Segoe UI 7.5pt Bold)
- **Localizable**: Yes
- **Category**: Visuals
- **Description**: The font used to display badge text. If null, uses default font (Segoe UI 7.5pt Bold).

```csharp
button.BadgeValues.Font = new Font("Arial", 8f, FontStyle.Bold);
button.BadgeValues.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
button.BadgeValues.Font = null; // Use default font
```

**Note**: This property only affects text badges. Image badges ignore this property.

##### `Shape` (BadgeShape)

Gets and sets the shape of the badge.

- **Type**: `BadgeShape`
- **Default**: `BadgeShape.Circle`
- **Category**: Visuals
- **Description**: The shape of the badge.

```csharp
button.BadgeValues.Shape = BadgeShape.Circle;
button.BadgeValues.Shape = BadgeShape.Square;
button.BadgeValues.Shape = BadgeShape.RoundedRectangle;
```

##### `Animation` (BadgeAnimation)

Gets and sets the animation type for the badge.

- **Type**: `BadgeAnimation`
- **Default**: `BadgeAnimation.None`
- **Category**: Visuals
- **Description**: The animation type for the badge.

```csharp
button.BadgeValues.Animation = BadgeAnimation.None;
button.BadgeValues.Animation = BadgeAnimation.FadeInOut;
button.BadgeValues.Animation = BadgeAnimation.Pulse;
```

**Notes**:
- Animations only run when the badge is visible (`Visible = true`)
- `FadeInOut`: Continuously fades the badge opacity between 30% and 100%
- `Pulse`: Continuously pulses the badge size (85% to 100%) and opacity (60% to 100%)
- Animations use a 50ms timer interval for smooth updates
- Setting animation to `None` stops any running animation immediately

##### `Position` (BadgePosition)

Gets and sets the position of the badge on the button.

- **Type**: `BadgePosition`
- **Default**: `BadgePosition.TopRight`
- **Category**: Visuals
- **Description**: The position of the badge on the button.

```csharp
button.BadgeValues.Position = BadgePosition.TopRight;
button.BadgeValues.Position = BadgePosition.TopLeft;
button.BadgeValues.Position = BadgePosition.BottomRight;
button.BadgeValues.Position = BadgePosition.BottomLeft;
```

##### `Visible` (bool)

Gets and sets whether the badge is visible.

- **Type**: `bool`
- **Default**: `false`
- **Category**: Visuals
- **Description**: Whether the badge is visible.

```csharp
button.BadgeValues.Visible = true;  // Show badge
button.BadgeValues.Visible = false; // Hide badge
```

**Note**: The badge is only rendered if `Visible` is `true` AND either `Text` has content or `Image` is not null.

#### Methods

##### `Reset()`

Resets all badge properties to their default values.

```csharp
button.BadgeValues.Reset();
```

This method sets:
- `Text` to empty string
- `Image` to null
- `BadgeColor` to `Color.Red`
- `TextColor` to `Color.White`
- `Font` to null (default font)
- `Shape` to `BadgeShape.Circle`
- `Animation` to `BadgeAnimation.None`
- `Position` to `BadgePosition.TopRight`
- `Visible` to `false`

##### Property-Specific Reset Methods

Each property has a corresponding reset method:

```csharp
button.BadgeValues.ResetText();
button.BadgeValues.ResetImage();
button.BadgeValues.ResetBadgeColor();
button.BadgeValues.ResetTextColor();
button.BadgeValues.ResetFont();
button.BadgeValues.ResetShape();
button.BadgeValues.ResetAnimation();
button.BadgeValues.ResetPosition();
button.BadgeValues.ResetVisible();
```

#### Property: `IsDefault` (bool, read-only)

Indicates whether all badge values are at their default settings.

- **Type**: `bool` (read-only)
- **Browsable**: No (hidden from Property Grid)
- **Usage**: Used internally by the serialization system

---

### Class: `KryptonDropButton` (and derived classes)

The base class for `KryptonButton` and `KryptonCheckButton` that exposes badge functionality.

#### Property: `BadgeValues`

Gets access to the badge values.

```csharp
public BadgeValues BadgeValues { get; }
```

**Usage**:

```csharp
KryptonButton button = new KryptonButton();
button.BadgeValues.Text = "5";
button.BadgeValues.Visible = true;
button.BadgeValues.Position = BadgePosition.TopRight;
```

**Design-Time Support**:
- Appears as an expandable property in the Property Grid
- All sub-properties are editable at design-time
- Changes are persisted in the designer-generated code

---

### Class: `ViewDrawBadge`

Internal rendering class responsible for drawing badges. This class is not intended for direct use by developers.

#### Internal Constants

- `DEFAULT_BADGE_SIZE`: 18 pixels (default diameter for empty badge)
- `BADGE_MIN_SIZE`: 16 pixels (minimum badge diameter)
- `BADGE_OFFSET`: 3 pixels (offset from button edge)

---

## Usage Examples

### Basic Text Badge

Display a simple numeric badge in the top-right corner:

```csharp
KryptonButton button = new KryptonButton
{
    Text = "Notifications",
    Location = new Point(10, 10),
    Size = new Size(120, 30)
};

button.BadgeValues.Text = "5";
button.BadgeValues.Visible = true;
button.BadgeValues.BadgeColor = Color.Red;
button.BadgeValues.TextColor = Color.White;
button.BadgeValues.Position = BadgePosition.TopRight;

this.Controls.Add(button);
```

### Image Badge

Display an icon badge using a system icon:

```csharp
KryptonButton button = new KryptonButton
{
    Text = "Alerts",
    Location = new Point(10, 50),
    Size = new Size(120, 30)
};

button.BadgeValues.Image = SystemIcons.Warning.ToBitmap();
button.BadgeValues.Visible = true;
button.BadgeValues.BadgeColor = Color.Orange;
button.BadgeValues.Position = BadgePosition.TopRight;

this.Controls.Add(button);
```

### Dynamic Notification Counter

Update badge text based on notification count:

```csharp
private int _notificationCount = 0;
private KryptonButton _notificationButton;

private void InitializeNotificationButton()
{
    _notificationButton = new KryptonButton
    {
        Text = "Messages",
        Location = new Point(10, 10),
        Size = new Size(120, 30)
    };

    _notificationButton.BadgeValues.Visible = false; // Start hidden
    _notificationButton.BadgeValues.BadgeColor = Color.Red;
    _notificationButton.BadgeValues.TextColor = Color.White;
    
    this.Controls.Add(_notificationButton);
    UpdateNotificationBadge();
}

private void UpdateNotificationBadge()
{
    if (_notificationCount > 0)
    {
        _notificationButton.BadgeValues.Text = _notificationCount > 99 ? "99+" : _notificationCount.ToString();
        _notificationButton.BadgeValues.Visible = true;
    }
    else
    {
        _notificationButton.BadgeValues.Visible = false;
    }
}

private void OnNewNotification()
{
    _notificationCount++;
    UpdateNotificationBadge();
}

private void OnNotificationRead()
{
    if (_notificationCount > 0)
    {
        _notificationCount--;
        UpdateNotificationBadge();
    }
}
```

### Different Badge Shapes

Demonstrate all three badge shapes:

```csharp
// Circle (default)
KryptonButton btnCircle = new KryptonButton { Text = "Circle", Location = new Point(10, 10) };
btnCircle.BadgeValues.Text = "5";
btnCircle.BadgeValues.Visible = true;
btnCircle.BadgeValues.Shape = BadgeShape.Circle;
btnCircle.BadgeValues.BadgeColor = Color.Red;

// Square
KryptonButton btnSquare = new KryptonButton { Text = "Square", Location = new Point(150, 10) };
btnSquare.BadgeValues.Text = "12";
btnSquare.BadgeValues.Visible = true;
btnSquare.BadgeValues.Shape = BadgeShape.Square;
btnSquare.BadgeValues.BadgeColor = Color.Blue;

// Rounded Rectangle
KryptonButton btnRounded = new KryptonButton { Text = "Rounded", Location = new Point(290, 10) };
btnRounded.BadgeValues.Text = "99+";
btnRounded.BadgeValues.Visible = true;
btnRounded.BadgeValues.Shape = BadgeShape.RoundedRectangle;
btnRounded.BadgeValues.BadgeColor = Color.Green;
```

### Custom Font

Use a custom font for badge text:

```csharp
KryptonButton button = new KryptonButton
{
    Text = "Custom Font",
    Location = new Point(10, 10),
    Size = new Size(120, 30)
};

button.BadgeValues.Text = "NEW";
button.BadgeValues.Visible = true;
button.BadgeValues.Font = new Font("Arial", 9f, FontStyle.Bold);
button.BadgeValues.BadgeColor = Color.Purple;
button.BadgeValues.TextColor = Color.White;
```

### Animated Badges

Use fade in/out animation:

```csharp
KryptonButton button = new KryptonButton
{
    Text = "Animated",
    Location = new Point(10, 10),
    Size = new Size(120, 30)
};

button.BadgeValues.Text = "!";
button.BadgeValues.Visible = true;
button.BadgeValues.BadgeColor = Color.Red;
button.BadgeValues.Animation = BadgeAnimation.FadeInOut;
```

Use pulsing animation:

```csharp
KryptonButton button = new KryptonButton
{
    Text = "Pulsing",
    Location = new Point(10, 10),
    Size = new Size(120, 30)
};

button.BadgeValues.Text = "5";
button.BadgeValues.Visible = true;
button.BadgeValues.BadgeColor = Color.Orange;
button.BadgeValues.Animation = BadgeAnimation.Pulse;
```

### Different Badge Positions

Demonstrate all four badge positions:

```csharp
// Top Right (default)
KryptonButton btnTopRight = new KryptonButton { Text = "Top Right", Location = new Point(10, 10) };
btnTopRight.BadgeValues.Text = "1";
btnTopRight.BadgeValues.Visible = true;
btnTopRight.BadgeValues.Position = BadgePosition.TopRight;

// Top Left
KryptonButton btnTopLeft = new KryptonButton { Text = "Top Left", Location = new Point(150, 10) };
btnTopLeft.BadgeValues.Text = "2";
btnTopLeft.BadgeValues.Visible = true;
btnTopLeft.BadgeValues.Position = BadgePosition.TopLeft;
btnTopLeft.BadgeValues.BadgeColor = Color.Blue;

// Bottom Right
KryptonButton btnBottomRight = new KryptonButton { Text = "Bottom Right", Location = new Point(10, 50) };
btnBottomRight.BadgeValues.Text = "3";
btnBottomRight.BadgeValues.Visible = true;
btnBottomRight.BadgeValues.Position = BadgePosition.BottomRight;
btnBottomRight.BadgeValues.BadgeColor = Color.Green;

// Bottom Left
KryptonButton btnBottomLeft = new KryptonButton { Text = "Bottom Left", Location = new Point(150, 50) };
btnBottomLeft.BadgeValues.Text = "4";
btnBottomLeft.BadgeValues.Visible = true;
btnBottomLeft.BadgeValues.Position = BadgePosition.BottomLeft;
btnBottomLeft.BadgeValues.BadgeColor = Color.Orange;
```

### Custom Image Badge

Load and display a custom image:

```csharp
// Load image from file
Image customIcon = Image.FromFile(@"path\to\icon.png");

KryptonButton button = new KryptonButton
{
    Text = "Custom Badge",
    Location = new Point(10, 10),
    Size = new Size(120, 30)
};

button.BadgeValues.Image = customIcon;
button.BadgeValues.Visible = true;
button.BadgeValues.BadgeColor = Color.Purple;
button.BadgeValues.Position = BadgePosition.TopRight;
```

### Color-Coded Status Badges

Use different colors to indicate status:

```csharp
private void UpdateStatusBadge(KryptonButton button, Status status)
{
    button.BadgeValues.Visible = true;
    button.BadgeValues.Text = "";
    
    switch (status)
    {
        case Status.Success:
            button.BadgeValues.Image = SystemIcons.Shield.ToBitmap();
            button.BadgeValues.BadgeColor = Color.Green;
            break;
            
        case Status.Warning:
            button.BadgeValues.Image = SystemIcons.Warning.ToBitmap();
            button.BadgeValues.BadgeColor = Color.Orange;
            break;
            
        case Status.Error:
            button.BadgeValues.Image = SystemIcons.Error.ToBitmap();
            button.BadgeValues.BadgeColor = Color.Red;
            break;
            
        case Status.Info:
            button.BadgeValues.Image = SystemIcons.Information.ToBitmap();
            button.BadgeValues.BadgeColor = Color.Blue;
            break;
    }
}
```

### KryptonCheckButton with Badge

Use badges on check buttons:

```csharp
KryptonCheckButton checkButton = new KryptonCheckButton
{
    Text = "Enable Feature",
    Location = new Point(10, 10),
    Size = new Size(150, 30)
};

checkButton.BadgeValues.Image = SystemIcons.Shield.ToBitmap();
checkButton.BadgeValues.Visible = true;
checkButton.BadgeValues.BadgeColor = Color.Green;
checkButton.BadgeValues.Position = BadgePosition.TopRight;
```

---

## Design-Time Support

### Property Grid Integration

The `BadgeValues` property is fully integrated into the Visual Studio Property Grid:

1. **Expandable Property**: The `BadgeValues` property appears as an expandable node in the Property Grid
2. **Sub-Properties**: All badge properties (`Text`, `Image`, `BadgeColor`, etc.) are accessible as sub-properties
3. **Type Conversion**: The property uses `ExpandableObjectConverter` for proper Property Grid display
4. **Serialization**: Changes are automatically serialized to the designer code

### Designer Code Generation

When you set badge properties in the Property Grid, the designer generates code like:

```csharp
// 
// button1
// 
this.button1.BadgeValues.BadgeColor = System.Drawing.Color.Red;
this.button1.BadgeValues.Text = "5";
this.button1.BadgeValues.TextColor = System.Drawing.Color.White;
this.button1.BadgeValues.Position = Krypton.Toolkit.BadgePosition.TopRight;
this.button1.BadgeValues.Visible = true;
```

### Default Value Handling

Properties with default values are not serialized unless explicitly changed. For example, if `Position` remains `TopRight`, it won't appear in the designer code unless changed.

---

## Implementation Details

### Rendering Pipeline

The badge rendering follows this flow:

1. **Layout Phase** (`ViewDrawBadge.Layout`):
   - Calculates badge size based on content (text or image)
   - Determines position relative to button's client rectangle
   - Sets `ClientRectangle` for the badge

2. **Paint Phase** (`ViewDrawBadge.Render`):
   - Applies animation effects (opacity, scale) if enabled
   - Draws badge background using `BadgeColor` and `Shape` (circle, square, or rounded rectangle)
   - Renders content (image or text) centered within the badge
   - Uses custom `Font` if specified, otherwise default font
   - Applies animation opacity to colors for fade effects
   - Uses anti-aliasing for smooth rendering

### Size Calculation

#### Text Badges

The badge size for text is calculated as follows:

```csharp
// Measure text using Segoe UI 7.5pt Bold font
SizeF textSize = graphics.MeasureString(text, font);

// Calculate diameter: larger dimension + padding (8px)
int diameter = Math.Max(16, (int)Math.Max(textSize.Width, textSize.Height) + 8);
```

#### Image Badges

The badge size for images is calculated as follows:

```csharp
// Use image dimensions with padding, capped at reasonable maximum
int imageMax = Math.Max(image.Width, image.Height);
int size = Math.Max(16, Math.Min(imageMax + 4, 32));
```

### Position Calculation

Badges are positioned relative to the button's client rectangle with a 3-pixel offset:

- **TopRight**: `(right - badgeWidth - 3, top + 3)`
- **TopLeft**: `(left + 3, top + 3)`
- **BottomRight**: `(right - badgeWidth - 3, bottom - badgeHeight - 3)`
- **BottomLeft**: `(left + 3, bottom - badgeHeight - 3)`

### Image Scaling

When rendering images:

1. Calculate maximum size: `min(badgeWidth, badgeHeight) - 4px padding`
2. Calculate scale factor: `min(maxSize / imageWidth, maxSize / imageHeight)`
3. Apply scale while maintaining aspect ratio
4. Center the scaled image within the badge
5. Use high-quality bicubic interpolation for smooth scaling

### Paint Notification System

All `BadgeValues` properties automatically trigger repaints when changed:

```csharp
public string Text
{
    get => _text ?? "";
    set
    {
        if (_text != value)
        {
            _text = value;
            PerformNeedPaint(true); // Triggers repaint
        }
    }
}
```

This ensures the UI updates immediately when badge properties change.

### Animation System

Badges support two animation types controlled by a `System.Windows.Forms.Timer`:

1. **FadeInOut Animation**:
   - Opacity oscillates between 30% and 100%
   - Uses sine-wave-like interpolation for smooth transitions
   - Updates every 50ms

2. **Pulse Animation**:
   - Scale oscillates between 85% and 100%
   - Opacity oscillates between 60% and 100%
   - Both properties animate in sync for a pulsing effect
   - Updates every 50ms

The animation timer:
- Starts automatically when `Animation != None` and `Visible == true`
- Stops automatically when `Animation == None` or `Visible == false`
- Is managed internally and cleaned up when no longer needed

---

## Best Practices

### 1. Text Length

Keep badge text short for optimal appearance:
- ✅ Recommended: 1-3 characters (`"5"`, `"99+"`, `"!"`)
- ⚠️ Acceptable: Up to 5 characters (`"NEW"`)
- ❌ Not recommended: Long text (`"Hello World"`)

### 2. Color Contrast

Ensure good contrast between badge background and text:

```csharp
// Good contrast
button.BadgeValues.BadgeColor = Color.Red;
button.BadgeValues.TextColor = Color.White;

// Poor contrast (avoid)
button.BadgeValues.BadgeColor = Color.LightGray;
button.BadgeValues.TextColor = Color.White; // Hard to read
```

### 3. Font Selection

When using custom fonts:
- Keep font sizes small (7-9pt) to fit within the badge
- Bold fonts work better for readability at small sizes
- Test fonts at different DPI settings for consistency
- Consider system fonts (Segoe UI, Arial) for better cross-platform compatibility

### 4. Shape Selection

Choose badge shapes based on content:
- **Circle**: Best for single-digit numbers and icons
- **Square**: Good for letters and short text (1-2 characters)
- **RoundedRectangle**: Works well for longer text (2-4 characters) or when you want a softer appearance

### 5. Image Size

Use appropriately sized images:
- Recommended: 16x16 to 32x32 pixels
- Images are automatically scaled, but smaller images scale better
- Use vector-style icons for best results at different sizes

### 6. Visibility Management

Show/hide badges based on content:

```csharp
// Good: Hide when no content
if (count > 0)
{
    button.BadgeValues.Text = count.ToString();
    button.BadgeValues.Visible = true;
}
else
{
    button.BadgeValues.Visible = false;
}

// Avoid: Leaving badge visible with empty text
button.BadgeValues.Text = "";
button.BadgeValues.Visible = true; // Badge appears but is empty
```

### 7. Resource Management

Dispose of custom images when done:

```csharp
// If loading custom images
Image customIcon = Image.FromFile(@"icon.png");
button.BadgeValues.Image = customIcon;

// When button is disposed, the image should be disposed too
// Consider using image resources or a shared image cache
```

### 8. Performance

Badges are lightweight and render efficiently:
- Size calculations are cached during layout
- Repaints only occur when properties change
- No performance impact when badges are hidden (`Visible = false`)

### 9. Accessibility

Consider accessibility when using badges:
- Provide tooltips or status text for badge meaning
- Don't rely solely on color to convey information
- Ensure badges don't obscure important button text

---

## Known Limitations

### 1. Text vs Image Priority

If both `Text` and `Image` are set, the image takes priority. To display text, set `Image` to `null`:

```csharp
// To show text, ensure image is null
button.BadgeValues.Image = null;
button.BadgeValues.Text = "5";
```

### 2. Badge Size Limits

- Minimum size: 16x16 pixels
- Maximum recommended text badge: ~40-50 pixels diameter
- Image badges are capped at 32 pixels diameter (with padding)

### 3. Position Constraints

Badges are positioned relative to the button's client rectangle. Very small buttons may cause badges to overlap button content.

### 4. Animation Performance

Animations run continuously while the badge is visible. For better performance, consider:
- Using animations only when necessary (e.g., for important notifications)
- Stopping animations (`Animation = BadgeAnimation.None`) when badges are not actively being monitored
- Avoiding too many animated badges on the same form simultaneously

### 5. Animation Limitations

Animation timing and parameters (speed, opacity ranges, scale ranges) are fixed and cannot be customized. The animation interval is 50ms for smooth updates.

### 6. Multiple Badges

Only one badge per button is supported. Multiple badges are not supported.