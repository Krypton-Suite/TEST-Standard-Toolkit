# Implement global RTL support for VisualSimpleBase controls

## Description

This PR implements comprehensive Right-to-Left (RTL) layout support for all controls that inherit from `VisualSimpleBase`, with a detailed implementation for `KryptonMonthCalendar`. RTL support is now available globally through the base class, making it easy to enable RTL layouts for all VisualSimpleBase controls.

## Problem

Previously, RTL support was not consistently implemented across VisualSimpleBase controls. Controls that inherit from `VisualSimpleBase` (such as `KryptonMonthCalendar`, `KryptonLabel`, `KryptonCheckBox`, `KryptonRadioButton`, `KryptonTrackBar`, `KryptonHeader`, `KryptonColorButton`, `KryptonCommandLinkButton`, `KryptonDropButton`, and `KryptonBreadCrumb`) did not have standardized RTL support.

Additionally, `KryptonMonthCalendar` specifically did not properly mirror its layout when RTL mode was enabled, causing day names, dates, and navigation elements to remain in LTR order.

## Solution

### 1. Global RTL Support in VisualSimpleBase

- Added `RightToLeftLayout` property to `VisualSimpleBase` class
- Implemented private `_rightToLeftLayout` field to store RTL layout state
- Overrode `RightToLeft` property to trigger repaint on changes
- Added virtual `OnRightToLeftLayoutChanged()` method for derived classes to override
- Added `OnRightToLeftChanged()` override to trigger layout updates

This provides a consistent foundation for RTL support across all VisualSimpleBase controls:
- `KryptonBreadCrumb`
- `KryptonLabel`
- `KryptonColorButton`
- `KryptonCommandLinkButton`
- `KryptonMonthCalendar`
- `KryptonRadioButton`
- `KryptonCheckBox`
- `KryptonDropButton`
- `KryptonHeader`
- `KryptonTrackBar`

### 2. RTL Implementation for KryptonMonthCalendar

Implemented comprehensive RTL support for `KryptonMonthCalendar`:

#### ViewDrawMonthDays.cs
- Modified `Layout()` and `RenderBefore()` methods to reverse day ordering in RTL mode
- Updated day positioning calculations to start from the right in RTL
- Adjusted `DayFromPoint()` and `DayNearPoint()` methods for correct mouse interaction in RTL
- Used `CommonHelper.GetRtlAwareStep()` for proper movement calculations

#### ViewDrawMonthDayNames.cs
- Modified `Layout()` and `RenderBefore()` methods to reverse day name ordering
- Updated positioning to start from the right in RTL mode
- Day names now display in reverse order (Sun, Sat, Fri, ... Mon) in RTL

#### ViewLayoutContext.cs
- Added `IsRightToLeftLayout` property for easy RTL detection from layout context
- Added `RightToLeft` property to access control's RTL setting

### 3. Global RTL Helper Methods

#### CommonHelper.cs
Added reusable RTL utility methods:
- `IsRightToLeftLayout(Control)` - Checks if a control has RTL layout enabled
- `GetRtlAwareXPosition(int, int, int, bool)` - Calculates X position for RTL layouts
- `GetRtlAwareStep(int, bool)` - Returns step direction for RTL-aware movement
- `GetRtlAwareIndex(int, int, bool)` - Maps indices for RTL-aware iteration
- Updated `GetRightToLeftLayout(Control)` - Enhanced to support VisualSimpleBase and use reflection for other controls

### 4. Integration with Existing RTL-Aware Components

#### ButtonSpecManagerBase.cs
- Updated `GetDockStyle()` to use `CommonHelper.IsRightToLeftLayout()` for consistent RTL detection
- Button docking now correctly reverses in RTL mode

#### ViewLayoutDocker.cs
- Updated `CalculateDock()` to use `CommonHelper.IsRightToLeftLayout()` for consistent RTL detection
- Dock styles now correctly invert left/right in RTL mode

### 5. Demo Application

Created comprehensive demo (`RTLControlsTest`) showcasing:
- Toggle RTL layout dynamically
- Multiple calendar configurations (LTR, RTL, multi-month, with features)
- Property grid integration for testing RTL properties
- Visual examples of all VisualSimpleBase controls with RTL support

## Behavior

### VisualSimpleBase Controls
All controls inheriting from `VisualSimpleBase` now have:
- `RightToLeft` property (inherited from Control, overridden for proper event handling)
- `RightToLeftLayout` property (new, provided by VisualSimpleBase)
- Automatic layout updates when RTL properties change
- Virtual `OnRightToLeftLayoutChanged()` method for custom handling

### KryptonMonthCalendar RTL Behavior
When `RightToLeft = Yes` and `RightToLeftLayout = true`:
- Day names are reversed (Sun, Sat, Fri, ... Mon)
- Days are positioned from right to left
- Multi-month calendars maintain RTL layout
- Week numbers, today indicators, and bolded dates work correctly in RTL
- Mouse interaction correctly maps to days in RTL mode
- Navigation buttons are positioned correctly (handled by ButtonSpecManagerBase)

## Testing

### Manual Testing Steps

1. **KryptonMonthCalendar RTL Testing:**
   - Open `RTLControlsTest` demo
   - Use "Toggle RTL" button to enable/disable RTL on the calendar
   - Verify day names are reversed in RTL mode
   - Verify days are positioned from right to left
   - Test date selection in RTL mode
   - Test multi-month calendars in RTL mode
   - Use PropertyGrid to change RTL properties and verify updates

2. **VisualSimpleBase Controls Testing:**
   - Create instances of any VisualSimpleBase control
   - Set `RightToLeft = RightToLeft.Yes`
   - Set `RightToLeftLayout = true`
   - Verify properties are available and can be set
   - Verify layout updates occur when properties change

3. **Integration Testing:**
   - Verify ButtonSpecManagerBase correctly handles RTL for buttons
   - Verify ViewLayoutDocker correctly inverts dock styles in RTL
   - Test with multiple VisualSimpleBase controls in the same form

## Files Changed

### Core Implementation
- `Source/Krypton Components/Krypton.Toolkit/Controls Visuals/VisualSimpleBase.cs`
  - Added `_rightToLeftLayout` field
  - Added `RightToLeftLayout` property
  - Overrode `RightToLeft` property
  - Added `OnRightToLeftChanged()` override
  - Added virtual `OnRightToLeftLayoutChanged()` method

- `Source/Krypton Components/Krypton.Toolkit/General/CommonHelper.cs`
  - Added `IsRightToLeftLayout()` method
  - Added `GetRtlAwareXPosition()` method
  - Added `GetRtlAwareStep()` method
  - Added `GetRtlAwareIndex()` method
  - Updated `GetRightToLeftLayout()` to support VisualSimpleBase

- `Source/Krypton Components/Krypton.Toolkit/View Layout/ViewLayoutContext.cs`
  - Added `IsRightToLeftLayout` property
  - Added `RightToLeft` property

- `Source/Krypton Components/Krypton.Toolkit/View Draw/ViewDrawMonthDays.cs`
  - Modified `Layout()` method for RTL day positioning
  - Modified `RenderBefore()` method for RTL day rendering
  - Updated `DayFromPoint()` and `DayNearPoint()` for RTL mouse interaction

- `Source/Krypton Components/Krypton.Toolkit/View Draw/ViewDrawMonthDayNames.cs`
  - Modified `Layout()` method for RTL day name positioning
  - Modified `RenderBefore()` method for RTL day name rendering

- `Source/Krypton Components/Krypton.Toolkit/ButtonSpec/ButtonSpecManagerBase.cs`
  - Updated `GetDockStyle()` to use `CommonHelper.IsRightToLeftLayout()`

- `Source/Krypton Components/Krypton.Toolkit/View Layout/ViewLayoutDocker.cs`
  - Updated `CalculateDock()` to use `CommonHelper.IsRightToLeftLayout()`

### Demo Application
- `Source/Krypton Components/TestForm/RTLControlsTest.cs` (NEW)
  - Comprehensive demo showcasing RTL support
  - Multiple calendar examples with RTL toggle
  - Property grid integration
  - Visual examples of all VisualSimpleBase controls with RTL support

- `Source/Krypton Components/TestForm/RTLControlsTest.Designer.cs` (NEW)
  - Designer code for RTL demo form

## Breaking Changes

None. This is a new feature that adds RTL support without changing existing LTR behavior. All changes are additive and backward-compatible.

## Benefits

1. **Global RTL Support:** All VisualSimpleBase controls now inherit consistent RTL support from the base class
2. **Easy to Extend:** New VisualSimpleBase controls automatically get RTL support
3. **Reusable Helpers:** CommonHelper methods provide utilities for RTL calculations
4. **Comprehensive Demo:** RTLControlsTest demonstrates RTL functionality
5. **Consistent API:** Standardized RTL properties and event handling across all VisualSimpleBase controls

## Related Issues

This implementation provides the foundation for RTL support across all VisualSimpleBase controls and includes a complete implementation for KryptonMonthCalendar.
