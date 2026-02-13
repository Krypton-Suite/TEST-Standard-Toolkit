# KryptonQRCode Developer Documentation

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [KryptonQRCode Control](#kryptonqrcode-control)
5. [QRErrorCorrectionLevel Enum](#qrerrorcorrectionlevel-enum)
6. [Static Methods](#static-methods)
7. [Features & Capabilities](#features--capabilities)
8. [Data Capacity](#data-capacity)
9. [Usage Examples](#usage-examples)
10. [Configuration & Customization](#configuration--customization)
11. [Error Handling](#error-handling)
12. [Technical Implementation](#technical-implementation)
13. [Limitations](#limitations)
14. [Best Practices](#best-practices)
15. [Related Components](#related-components)

---

## Overview

The **KryptonQRCode** component provides native QR code generation and display capabilities within the Krypton Standard Toolkit. It generates QR codes entirely from managed .NET code without any external NuGet packages, web services, or native dependencies. The implementation follows the ISO/IEC 18004 QR code specification.

### Components

| Component | Type | Description |
|-----------|------|-------------|
| `KryptonQRCode` | Control | A `KryptonPanel`-derived control that displays a QR code with live updates when content changes |
| `QRErrorCorrectionLevel` | Enum | Error correction levels (L, M, Q, H) for QR code durability |

### Key Features

- **100% Native**: No external dependencies—uses only `System.Drawing` and .NET BCL
- **UTF-8 Support**: Full Unicode support via byte mode encoding
- **Versions 1–10**: Automatic version selection (21×21 to 57×57 modules)
- **Configurable Error Correction**: Four levels (L, M, Q, H) for durability vs. capacity trade-off
- **Customizable Rendering**: Colors, module size, quiet zone (border)
- **Static Generation**: `GenerateBitmap()` for programmatic use without a control instance
- **Krypton Integration**: Inherits from `KryptonPanel` for full theming support
- **Design-Time Support**: Toolbox-enabled, property grid support

### Supported Platforms

- .NET Framework 4.8 and later
- .NET 8.0 Windows and later
- All target frameworks supported by Krypton.Utilities

### Requirements

- **Krypton.Utilities**: Component is located in the `Krypton.Utilities` project
- **Krypton.Toolkit**: Required dependency (inherits `KryptonPanel`)
- **System.Drawing**: For bitmap generation and control rendering

---

## Quick Start

### As a Control (Designer or Code)

```csharp
using Krypton.Utilities;

// Create and configure
var qrCode = new KryptonQRCode
{
    Content = "https://example.com",
    ErrorCorrectionLevel = QRErrorCorrectionLevel.M,
    ModuleSize = 4,
    Size = new Size(120, 120)
};

// Add to form
this.Controls.Add(qrCode);
```

### Programmatic Bitmap Generation (No UI)

```csharp
using System.Drawing;
using System.Drawing.Imaging;
using Krypton.Utilities;

// Generate bitmap directly
Bitmap bmp = KryptonQRCode.GenerateBitmap("Hello, World!", moduleSize: 6);
bmp.Save("qrcode.png", ImageFormat.Png);
bmp.Dispose();
```

### Save from Control

```csharp
qrCode.Content = "User-specific data";
qrCode.SaveToFile(@"C:\Output\qr.png", ImageFormat.Png);
```

---

## API Reference

### Namespace

```csharp
using Krypton.Utilities;
```

### Assembly

- **Krypton.Utilities.dll**

---

## KryptonQRCode Control

### Class Declaration

```csharp
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultProperty("Content")]
[DefaultEvent("ContentChanged")]
[DesignerCategory("code")]
[Description("Displays a QR code generated from the specified content. Uses native generation without external packages.")]
public class KryptonQRCode : KryptonPanel
```

### Inheritance Hierarchy

```
System.Object
  └─ System.MarshalByRefObject
      └─ System.ComponentModel.Component
          └─ System.Windows.Forms.Control
              └─ Krypton.Toolkit.KryptonPanel
                  └─ Krypton.Utilities.KryptonQRCode
```

---

### Properties

| Property | Type | Default | Category | Description |
|----------|------|---------|----------|-------------|
| `Content` | `string` | `""` | Behavior | The text or data to encode in the QR code. Encoded as UTF-8. Setting triggers regeneration. |
| `ErrorCorrectionLevel` | `QRErrorCorrectionLevel` | `M` | Appearance | Error correction level. Higher = more durable, less capacity. |
| `ModuleSize` | `int` | `4` | Appearance | Pixels per QR module (1–20). Affects rendered size. |
| `DarkColor` | `Color` | `Color.Black` | Appearance | Color for dark (filled) modules. |
| `LightColor` | `Color` | `Color.White` | Appearance | Color for light (empty) modules. |
| `ShowBorder` | `bool` | `true` | Appearance | Whether to show quiet zone (4-module border) around QR code. |

#### Property Details

**Content**
- **Setter behavior**: Regenerates the QR code and raises `ContentChanged`
- **Null handling**: `null` is stored as `string.Empty`
- **Empty string**: Displays nothing (blank control)

**ModuleSize**
- **Valid range**: 1–20 pixels
- **Effect**: Total pixel size = `(matrixSize × ModuleSize) + (border if ShowBorder)`
- **Example**: Version 1 (21×21) with ModuleSize=4 and border → ~116×116 pixels

---

### Events

| Event | Type | Description |
|-------|------|-------------|
| `ContentChanged` | `EventHandler` | Raised when the `Content` property changes and the QR code is regenerated. |

```csharp
qrCode.ContentChanged += (sender, e) =>
{
    // QR code has been regenerated
    statusLabel.Text = $"Encoded: {qrCode.Content.Length} characters";
};
```

---

### Instance Methods

#### GetBitmap()

```csharp
public Bitmap? GetBitmap()
```

**Returns**: A `Bitmap` of the current QR code, or `null` if `Content` is empty.

**Usage**:
```csharp
using (Bitmap? bmp = qrCode.GetBitmap())
{
    if (bmp != null)
    {
        Clipboard.SetImage(bmp);
    }
}
```

**Note**: Caller is responsible for disposing the returned `Bitmap`.

---

#### SaveToFile(string path, ImageFormat format)

```csharp
public void SaveToFile(string path, System.Drawing.Imaging.ImageFormat format)
```

**Parameters**:
| Parameter | Type | Description |
|-----------|------|-------------|
| `path` | `string` | File path to save to |
| `format` | `ImageFormat` | Image format (e.g., `ImageFormat.Png`, `ImageFormat.Jpeg`) |

**Behavior**: Saves the current QR code to file. No-op if `Content` is empty (no file created).

**Usage**:
```csharp
qrCode.SaveToFile(@"C:\exports\qr.png", ImageFormat.Png);
```

---

### Static Methods

#### GenerateBitmap(string content, int moduleSize, QRErrorCorrectionLevel eccLevel, Color? darkColor, Color? lightColor)

```csharp
public static Bitmap GenerateBitmap(
    string content,
    int moduleSize = 4,
    QRErrorCorrectionLevel eccLevel = QRErrorCorrectionLevel.M,
    Color? darkColor = null,
    Color? lightColor = null)
```

**Parameters**:
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `content` | `string` | — | The text to encode (required) |
| `moduleSize` | `int` | `4` | Pixels per module |
| `eccLevel` | `QRErrorCorrectionLevel` | `M` | Error correction level |
| `darkColor` | `Color?` | `null` | Dark module color (default: Black) |
| `lightColor` | `Color?` | `null` | Light module color (default: White) |

**Returns**: A new `Bitmap` containing the QR code. Always includes quiet zone.

**Throws**:
- `ArgumentException` – if `content` is null or empty
- `ArgumentException` – if data exceeds maximum capacity

**Usage**:
```csharp
// Minimal
Bitmap bmp = KryptonQRCode.GenerateBitmap("https://example.com");

// Full customization
Bitmap bmp = KryptonQRCode.GenerateBitmap(
    content: "vCard data",
    moduleSize: 8,
    eccLevel: QRErrorCorrectionLevel.H,
    darkColor: Color.Navy,
    lightColor: Color.AliceBlue
);
```

---

## QRErrorCorrectionLevel Enum

```csharp
public enum QRErrorCorrectionLevel
{
    L = 0,  // ~7% recovery. Maximum data capacity.
    M = 1,  // ~15% recovery. Good balance.
    Q = 2,  // ~25% recovery. Better durability.
    H = 3   // ~30% recovery. Highest durability, least capacity.
}
```

### Choosing a Level

| Level | Use Case | Capacity Impact |
|-------|----------|-----------------|
| **L** | Maximum data, clean environment (e.g., digital display) | Highest capacity |
| **M** | General purpose, recommended default | ~15% less than L |
| **Q** | Printed materials, partial obstruction expected | ~25% less than L |
| **H** | Harsh environments (labels, outdoor, damaged) | ~40% less than L |

---

## Static Methods

The control exposes one static method for generating QR codes without instantiating a control:

- **`KryptonQRCode.GenerateBitmap(...)`** – See [KryptonQRCode Control](#kryptonqrcode-control) above.

The internal `QRCodeGeneratorCore` and `QRCodeBitmapRenderer` classes are `internal` and not part of the public API.

---

## Features & Capabilities

### Encoding Mode

- **Byte Mode Only**: All content is encoded as UTF-8 bytes
- **Unicode Support**: Full Unicode via UTF-8 (e.g., emoji, CJK characters)
- **Automatic Version Selection**: Smallest version that fits the data is chosen

### QR Code Structure

- **Finder Patterns**: Three 7×7 finder patterns in corners (per spec)
- **Timing Patterns**: Horizontal and vertical timing strips
- **Alignment Patterns**: For versions 2–10 (position correction)
- **Format Information**: 15-bit format string (ECC level + mask)
- **Data Masking**: Mask pattern 0 `(row + col) % 2 == 0` applied to data area

### Rendering

- **Module-Based**: Each QR module maps to `ModuleSize`×`ModuleSize` pixels
- **Quiet Zone**: Optional 4-module border (recommended for reliable scanning)
- **Centered**: Control centers the QR code within its bounds
- **Nearest-Neighbor**: Uses `InterpolationMode.NearestNeighbor` for crisp edges

---

## Data Capacity

### Byte Mode Capacity by Version and ECC Level

Maximum bytes (characters in UTF-8) per version:

| Version | Size | L | M | Q | H |
|---------|------|---|---|---|---|
| 1 | 21×21 | 17 | 14 | 11 | 7 |
| 2 | 25×25 | 32 | 26 | 20 | 14 |
| 3 | 29×29 | 53 | 42 | 32 | 24 |
| 4 | 33×33 | 78 | 62 | 46 | 34 |
| 5 | 37×37 | 106 | 84 | 60 | 44 |
| 6 | 41×41 | 134 | 106 | 74 | 58 |
| 7 | 45×45 | 154 | 122 | 86 | 64 |
| 8 | 49×49 | 192 | 152 | 108 | 84 |
| 9 | 53×53 | 230 | 180 | 130 | 98 |
| 10 | 57×57 | 271 | 213 | 151 | 119 |

**Notes**:
- Multi-byte UTF-8 characters (e.g., emoji) consume more than one byte
- ASCII text: 1 byte per character
- Version is chosen automatically; maximum ~271 bytes at L, ~119 at H for version 10

---

## Usage Examples

### Example 1: URL QR Code

```csharp
var qrCode = new KryptonQRCode
{
    Content = "https://kryptonsuite.com",
    ErrorCorrectionLevel = QRErrorCorrectionLevel.M,
    ModuleSize = 5,
    Size = new Size(150, 150)
};
```

### Example 2: vCard / Contact

```csharp
string vCard = @"BEGIN:VCARD
VERSION:3.0
FN:John Doe
TEL:+1234567890
EMAIL:john@example.com
END:VCARD";

var qrCode = new KryptonQRCode
{
    Content = vCard,
    ErrorCorrectionLevel = QRErrorCorrectionLevel.L,
    ModuleSize = 3  // Smaller modules for larger data
};
```

### Example 3: Dynamic Content with Event

```csharp
var qrCode = new KryptonQRCode { Content = "" };
var textBox = new KryptonTextBox();

textBox.TextChanged += (s, e) =>
{
    qrCode.Content = textBox.Text;
};

qrCode.ContentChanged += (s, e) =>
{
    statusLabel.Text = qrCode.GetBitmap() != null ? "Ready" : "Empty";
};
```

### Example 4: Batch Export to Files

```csharp
var items = new[] { "Item1", "Item2", "Item3" };
for (int i = 0; i < items.Length; i++)
{
    var qr = KryptonQRCode.GenerateBitmap(items[i], moduleSize: 10);
    qr.Save($"qr_{i}.png", ImageFormat.Png);
    qr.Dispose();
}
```

### Example 5: Custom Colors (Dark Theme)

```csharp
var qrCode = new KryptonQRCode
{
    Content = "Dark theme QR",
    DarkColor = Color.White,
    LightColor = Color.FromArgb(30, 30, 30),
    ModuleSize = 4
};
```

### Example 6: Clipboard Copy

```csharp
void CopyQRToClipboard()
{
    using Bitmap? bmp = qrCode.GetBitmap();
    if (bmp != null)
    {
        Clipboard.SetImage(bmp);
    }
}
```

---

## Configuration & Customization

### Property Configuration Matrix

| Goal | Property | Value |
|------|----------|-------|
| Maximum data | `ErrorCorrectionLevel` | `L` |
| Maximum durability | `ErrorCorrectionLevel` | `H` |
| Smaller display | `ModuleSize` | 1–3 |
| Larger display | `ModuleSize` | 6–20 |
| No border | `ShowBorder` | `false` |
| Inverted (light on dark) | `DarkColor` | White, `LightColor` | Dark |
| Branded colors | `DarkColor`, `LightColor` | Custom `Color` |

### Control Size Recommendations

- **Version 1 (21×21)**: ~100–120 px with ModuleSize=4–5
- **Version 5 (37×37)**: ~160–200 px with ModuleSize=4–5
- **Version 10 (57×57)**: ~250–300 px with ModuleSize=4–5

Formula: `ControlSize ≈ (17 + version×4) × ModuleSize + (ShowBorder ? 8×ModuleSize : 0)`

---

## Error Handling

### Exceptions

| Scenario | Exception | Message |
|----------|-----------|---------|
| Empty or null content to `GenerateBitmap` | `ArgumentException` | "Content cannot be null or empty." |
| Data exceeds capacity | `ArgumentException` | "Data too long for QR code. Maximum ~X bytes for ECC Y." |
| SaveToFile with empty content | None | No file created; method returns without error |

### Control Behavior

When `Content` is set and generation throws (e.g., data too long):
- The control clears the displayed QR code (`_moduleMatrix = null`)
- No exception propagates to the caller
- `GetBitmap()` returns `null`
- `SaveToFile` creates no file

### Defensive Coding

```csharp
try
{
    Bitmap bmp = KryptonQRCode.GenerateBitmap(userInput);
    // Use bmp
}
catch (ArgumentException ex)
{
    // Handle: empty content or data too long
}
```

---

## Technical Implementation

### Architecture

```
KryptonQRCode (Control)
    │
    ├─► QRCodeGeneratorCore.Generate()  → bool[,] module matrix
    │       ├─ Data encoding (Byte mode, UTF-8)
    │       ├─ Reed-Solomon error correction (GF(256))
    │       ├─ Module matrix construction
    │       └─ Format/mask application
    │
    └─► QRCodeBitmapRenderer.Render()   → Bitmap (internal, used by GetBitmap/GenerateBitmap)
            └─ Renders matrix to pixels
```

### Reed-Solomon Error Correction

- **Galois Field**: GF(256) with primitive polynomial `0x11D`
- **Algorithm**: Polynomial division for ECC codeword generation
- **Blocks**: Supports both single-block and two-group block structures per version/ECC

### File Structure

```
Krypton.Utilities/Components/KryptonQRCode/
├── Controls Toolkit/
│   └── KryptonQRCode.cs          # Public control
└── General/
    ├── QRCodeEnums.cs            # QRErrorCorrectionLevel
    ├── QRCodeGeneratorCore.cs    # Internal generator
    └── QRCodeBitmapRenderer.cs   # Internal bitmap renderer
```

---

## Limitations

1. **Encoding Mode**: Byte mode only (no numeric or alphanumeric optimization)
2. **Versions**: 1–10 only (not 11–40)
3. **Mask Pattern**: Fixed to pattern 0 (no mask optimization)
4. **Version Info**: Version 7+ version info blocks not placed (may affect some strict readers)
5. **Decoding**: Generation only; no QR code reading/decoding
6. **ModuleSize**: Clamped to 1–20 for control rendering

---

## Best Practices

1. **Use M or L for URLs**: URLs are usually short; L or M is sufficient.
2. **Use H for printed labels**: Improves scan success when printed or worn.
3. **Keep ShowBorder = true**: Quiet zone improves scanner reliability.
4. **Dispose bitmaps**: Always dispose `Bitmap` instances from `GetBitmap()` or `GenerateBitmap()`.
5. **Validate length**: For user input, check byte count: `Encoding.UTF8.GetByteCount(text) <= 271` (at L).
6. **ModuleSize for printing**: Use 3–5 for screen, 5–10 for print to ensure scanner compatibility.

---

## Related Components

- **KryptonPanel**: Base class; provides theming and layout
- **Krypton.Toolkit**: Core Krypton controls and theming
- **Krypton.Utilities**: Parent project containing KryptonQRCode

---

## Revision History

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 2026 | Initial release; native QR code generation, KryptonQRCode control |
