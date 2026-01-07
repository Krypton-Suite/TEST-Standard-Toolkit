# Fix dock cell dragging lag (#1809)

## Description

Fixes a lag issue when dragging dock cells where the floating window position would not update smoothly during mouse movement. The window would only update when mouse movement slowed down, creating a noticeable delay and poor user experience.

## Problem

The `DockingDragManager` was using a 10ms timer to throttle position updates of the floating window during drag operations. This throttling caused lag when the mouse moved faster than the timer interval, as the window position would only update when the timer fired, not immediately on mouse movement.

## Solution

Modified `DragMove` to update the floating window position immediately instead of relying on a timer-based approach. The position update logic was extracted into a separate `UpdateFloatingWindowPosition()` method for better code organization.

### Changes

- **`DockingDragManager.DragMove()`**: Now calls `UpdateFloatingWindowPosition()` immediately instead of starting a timer
- **`DockingDragManager.UpdateFloatingWindowPosition()`**: New method that contains the position update logic (extracted from the timer handler)
- **`DockingDragManager.OnFloatingWindowMove()`**: Timer handler is now unused but kept for backward compatibility

## Testing

Manual testing should verify:
- Dragging dock cells feels smooth and responsive
- No lag or delay when moving the mouse quickly
- Window position updates immediately with mouse movement
- All existing docking functionality continues to work correctly

## Impact

- **Breaking Changes**: None
- **TFM Impact**: None - works across all supported target frameworks
- **Performance**: Improved responsiveness during drag operations
- **Backward Compatibility**: Maintained - timer infrastructure remains but is unused

## Related Issues

Closes #1809

## Notes

The timer was originally added to prevent display tearing, but modern Windows Forms handles frequent position updates smoothly. The immediate update approach provides better responsiveness without visual artifacts.
