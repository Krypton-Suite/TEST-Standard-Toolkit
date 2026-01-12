# Fix RTL support for KryptonForm title bar elements (#2103)

## Description

This PR fixes Right-to-Left (RTL) layout support for `KryptonForm`, ensuring that control box buttons (minimize, maximize/restore, and close), the form icon, and title text are positioned correctly when `RightToLeft` is set to `Yes` and `RightToLeftLayout` is enabled.

## Problem

Previously, `KryptonForm` did not properly support RTL layouts. When RTL mode was enabled:
- Control box buttons remained on the right side instead of moving to the left
- The form icon was not repositioned for RTL
- The title text was not positioned correctly relative to the icon

Additionally, the `RightToLeft` property was not visible in the designer or properties window, and was not serialized to the designer source.

## Solution

### 1. Fixed Button Positioning (`ButtonSpecManagerBase.cs`)
- Updated `GetDockStyle()` method to account for RTL when `RightToLeftLayout` is enabled
- When in RTL mode, buttons on the "Far" edge are now docked to the left instead of the right
- This ensures control box buttons appear on the left side in RTL layouts

### 2. Made RightToLeft Property Browsable (`KryptonForm.cs`)
- Overrode the `RightToLeft` property to make it browsable, visible, and serialized
- Added proper attributes: `[Browsable(true)]`, `[DefaultValue(RightToLeft.No)]`, `[EditorBrowsable(EditorBrowsableState.Always)]`, and `[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]`
- This allows developers to set RTL properties in the designer

### 3. Added RTL Change Handlers (`KryptonForm.cs`)
- Implemented `OnRightToLeftChanged()` and `OnRightToLeftLayoutChanged()` methods
- These handlers recreate buttons when RTL settings change to ensure proper positioning
- Buttons are automatically repositioned when RTL mode is toggled at runtime

### 4. Fixed Icon Positioning (`FormPaletteRedirect` in `KryptonForm.cs`)
- Overrode `GetContentImageH()` to position the icon on the right (Far alignment) in RTL mode
- The icon now appears on the far right side of the title bar in RTL layouts

### 5. Fixed Title Text Positioning (`FormPaletteRedirect` in `KryptonForm.cs`)
- Updated `GetContentShortTextH()` to position the title text on the right (Far alignment) in RTL mode
- The title text appears before the icon, maintaining the correct order: [Buttons] [Title] [Icon]

## Behavior

### LTR Mode (Default)
- Control box buttons: Right side
- Title text: Left side (or as configured by `FormTitleAlign`)
- Icon: Left side (before title)

### RTL Mode (`RightToLeft = Yes` and `RightToLeftLayout = true`)
- Control box buttons: Left side
- Title text: Right side (before icon)
- Icon: Far right side

This matches standard Windows RTL behavior where the title bar and buttons are mirrored.

## Testing

Manual testing steps:
1. Create a `KryptonForm` instance
2. Set `RightToLeft = RightToLeft.Yes`
3. Set `RightToLeftLayout = true`
4. Verify:
   - Control box buttons appear on the left side
   - Title text appears on the right side (before the icon)
   - Icon appears on the far right side
   - Changing RTL properties at runtime correctly repositions elements

## Files Changed

- `Source/Krypton Components/Krypton.Toolkit/ButtonSpec/ButtonSpecManagerBase.cs`
  - Updated `GetDockStyle()` to handle RTL layout

- `Source/Krypton Components/Krypton.Toolkit/Controls Toolkit/KryptonForm.cs`
  - Made `RightToLeft` property browsable and visible
  - Added `OnRightToLeftChanged()` and `OnRightToLeftLayoutChanged()` handlers
  - Updated `FormPaletteRedirect.GetContentImageH()` for RTL icon positioning
  - Updated `FormPaletteRedirect.GetContentShortTextH()` for RTL title positioning

## Breaking Changes

None. This is a bug fix that adds proper RTL support without changing existing LTR behavior.

## Related Issues

Fixes #2103
