# Fix blurry text rendering in Stimulsoft preview windows (#2913)

## Description

Fixes an issue where Krypton Toolkit controls caused low-quality/blurry text rendering in Stimulsoft report preview windows. The problem occurred even when just referencing the Krypton library or adding a single Krypton control (e.g., `KryptonLabel`) to any form in the application.

## Root Cause

Several Krypton controls were modifying `Graphics.TextRenderingHint` directly without properly saving and restoring the original value. This caused the text rendering hint to "leak" to other controls that shared the same graphics context, including Stimulsoft preview windows.

## Solution

All instances where `TextRenderingHint` was set directly have been updated to use the existing `GraphicsTextHint` helper class, which properly saves the original value, applies the new hint, and restores it when disposed (via `using` statement). This ensures that Krypton's text rendering settings are properly scoped and don't affect other controls.

## Changes Made

### Fixed Controls
1. **KryptonWrapLabel.cs** — Wrapped `TextRenderingHint` change with `GraphicsTextHint` before calling `base.OnPaint(e)`
2. **KryptonComboBox.cs** — Wrapped `TextRenderingHint` change in the `DrawItem` event handler
3. **KryptonTextBox.cs** — Wrapped `TextRenderingHint` change when drawing disabled text
4. **KryptonMaskedTextBox.cs** — Same fix as `KryptonTextBox`

### Code Pattern

**Before:**
```csharp
e.Graphics.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(hint);
base.OnPaint(e);
```

**After:**
```csharp
using (new GraphicsTextHint(e.Graphics, CommonHelper.PaletteTextHintToRenderingHint(hint)))
{
    base.OnPaint(e);
}
```

## Files Changed

- `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonWrapLabel.cs`
- `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonComboBox.cs`
- `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonTextBox.cs`
- `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonMaskedTextBox.cs`

## Testing

### Manual Testing Steps
1. Create a WinForms application that references both Krypton Toolkit and Stimulsoft
2. Add any Krypton control (e.g., `KryptonLabel`) to a form
3. Open a Stimulsoft report preview window
4. Verify that text in the preview window is no longer blurry and renders with proper quality

### Verification
- All modified files compile without errors
- No linter errors introduced
- Changes follow existing codebase patterns (similar to `GraphicsHint` for `SmoothingMode`)
- Backward compatible — no breaking changes

## Impact

- **Breaking Changes:** None
- **TFM Impact:** None — works across all supported TFMs (`net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`)
- **Performance:** Negligible — `GraphicsTextHint` is a lightweight wrapper with minimal overhead

## Related Issues

Closes #2913

## Notes

This fix follows the same pattern already established in the codebase for managing graphics state (e.g., `GraphicsHint` for `SmoothingMode`). The `GraphicsTextHint` helper class was already available but wasn't being used consistently across all controls.
