# ToolStripStatusLabel Flashing Alert Feature

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Classes](#classes)
5. [Extension Methods](#extension-methods)
6. [Usage Examples](#usage-examples)
7. [Designer Support](#designer-support)
8. [Implementation Details](#implementation-details)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)
11. [Performance Considerations](#performance-considerations)
12. [Related Issues](#related-issues)

---

## Overview

The Flashing Alert feature provides extension methods and a settings class for adding animated flashing functionality to `ToolStripStatusLabel` controls. This feature is particularly useful for drawing user attention to important status messages, alerts, warnings, or notifications displayed in the status bar.

### Key Features

- **Easy to Use**: Simple extension methods that work with any `ToolStripStatusLabel`
- **Flexible Configuration**: Support for custom foreground and background colors
- **Configurable Intervals**: Adjustable flash timing (milliseconds)
- **Settings Class**: `FlashingAlertSettings` class with `ExpandableObjectConverter` for designer support
- **Automatic Cleanup**: Handles disposal and cleanup automatically
- **Thread-Safe**: Safe for UI thread operations
- **State Management**: Tracks flashing state per label independently
- **Dynamic Control**: Start, stop, toggle, and change intervals at runtime

### Use Cases

- **Alert Notifications**: Draw attention to critical errors or warnings
- **Status Updates**: Highlight important status changes
- **Progress Indicators**: Visual feedback for long-running operations
- **User Attention**: Alert users to important messages or events
- **Warning Messages**: Emphasize warnings or cautions

### Requirements

- **Namespace**: `Krypton.Utilities`
- **Target Framework**: .NET Framework 4.7.2+ or .NET 8.0+ (Windows)
- **Dependencies**: `Krypton.Toolkit` project reference
- **Controls**: Works with standard `System.Windows.Forms.ToolStripStatusLabel` controls

---

## Quick Start

### Basic Usage

```csharp
using Krypton.Utilities;

// Start flashing with default settings (500ms interval)
toolStripStatusLabel1.StartFlashing();

// Start flashing with custom colors
toolStripStatusLabel1.StartFlashing(
    flashForeColor: Color.Red,
    flashBackColor: Color.Yellow,
    interval: 300
);

// Stop flashing
toolStripStatusLabel1.StopFlashing();

// Check if flashing
if (toolStripStatusLabel1.IsFlashing())
{
    // Change interval while flashing
    toolStripStatusLabel1.SetFlashInterval(1000);
}
```

### Using FlashingAlertSettings

```csharp
using Krypton.Utilities;

// Create and configure settings
var settings = new FlashingAlertSettings
{
    FlashForeColor = Color.White,
    FlashBackColor = Color.Red,
    Interval = 500
};

// Start flashing with settings
toolStripStatusLabel1.StartFlashing(settings);
```

### Designer Usage

1. Add a `ToolStripStatusLabel` to your form's `StatusStrip`
2. In code, create a `FlashingAlertSettings` instance or use direct method calls
3. Configure the settings properties (they appear as expandable objects in the property grid)
4. Call `StartFlashing()` with your settings

---

## API Reference

### Namespace

```csharp
using Krypton.Utilities;
```

---

## Classes

### FlashingAlertSettings

Storage class for flashing alert configuration. This class uses `ExpandableObjectConverter` for designer support, allowing properties to be expanded in the Visual Studio property grid.

#### Class Declaration

```csharp
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FlashingAlertSettings
```

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `FlashForeColor` | `Color` | `Color.Empty` | The foreground color to use when flashing. If `Color.Empty`, uses the original foreground color. |
| `FlashBackColor` | `Color` | `Color.Empty` | The background color to use when flashing. If `Color.Empty`, uses the original background color. |
| `Interval` | `int` | `500` | The flash interval in milliseconds. Must be greater than zero. |

#### Methods

##### `ToString()`

Returns a string representation of the settings for display in the property grid.

**Returns**: `string` - A formatted string showing the current settings (e.g., "Fore: Red, Back: Yellow, Interval: 500ms")

**Example**:
```csharp
var settings = new FlashingAlertSettings
{
    FlashForeColor = Color.Red,
    FlashBackColor = Color.Yellow,
    Interval = 500
};

Console.WriteLine(settings.ToString());
// Output: "Fore: Red, Back: Yellow, Interval: 500ms"
```

#### Usage Example

```csharp
// Create settings object
var alertSettings = new FlashingAlertSettings
{
    FlashForeColor = Color.White,
    FlashBackColor = Color.DarkRed,
    Interval = 400
};

// Use with extension method
statusLabel.StartFlashing(alertSettings);
```

---

## Extension Methods

All extension methods are defined in the `ToolStripStatusLabelExtensions` static class.

### StartFlashing Methods

#### `StartFlashing(FlashingAlertSettings settings)`

Starts flashing the `ToolStripStatusLabel` using the specified settings object.

**Parameters**:
- `settings` (`FlashingAlertSettings`): The flashing alert settings to use. Cannot be `null`.

**Exceptions**:
- `ArgumentNullException`: Thrown when `label` or `settings` is `null`.

**Example**:
```csharp
var settings = new FlashingAlertSettings
{
    FlashForeColor = Color.Red,
    FlashBackColor = Color.Yellow,
    Interval = 300
};

statusLabel.StartFlashing(settings);
```

#### `StartFlashing(Color? flashForeColor = null, Color? flashBackColor = null, int interval = 500)`

Starts flashing the `ToolStripStatusLabel` with the specified colors and interval.

**Parameters**:
- `flashForeColor` (`Color?`): Optional. The foreground color to use when flashing. If `null`, uses the original foreground color.
- `flashBackColor` (`Color?`): Optional. The background color to use when flashing. If `null`, uses the original background color.
- `interval` (`int`): The flash interval in milliseconds. Default is 500ms. Must be greater than zero.

**Exceptions**:
- `ArgumentNullException`: Thrown when `label` is `null`.
- `ArgumentOutOfRangeException`: Thrown when `interval` is less than or equal to zero.

**Example**:
```csharp
// Start with custom colors and interval
statusLabel.StartFlashing(Color.White, Color.Red, 400);

// Start with only foreground color (background unchanged)
statusLabel.StartFlashing(flashForeColor: Color.Red);

// Start with default settings (500ms, original colors)
statusLabel.StartFlashing();
```

**Behavior**:
- If the label is already flashing, it will be stopped first and then restarted with the new settings.
- Original colors are automatically stored and restored when flashing stops.
- A timer is created and started to handle the flashing animation.
- The label's `Disposed` event is hooked to ensure proper cleanup.

### StopFlashing

#### `StopFlashing()`

Stops flashing the `ToolStripStatusLabel` and restores original colors.

**Parameters**: None

**Returns**: `void`

**Example**:
```csharp
statusLabel.StopFlashing();
```

**Behavior**:
- Stops the flashing timer
- Disposes the timer
- Restores the original foreground and background colors
- Removes the label from the internal tracking dictionary
- Unhooks the `Disposed` event handler
- Safe to call even if the label is not currently flashing (no exception thrown)

### IsFlashing

#### `IsFlashing()`

Gets a value indicating whether the `ToolStripStatusLabel` is currently flashing.

**Parameters**: None

**Returns**: `bool` - `true` if the label is flashing; otherwise, `false`.

**Example**:
```csharp
if (statusLabel.IsFlashing())
{
    // Label is currently flashing
    statusLabel.StopFlashing();
}
else
{
    // Label is not flashing
    statusLabel.StartFlashing(Color.Red, Color.Yellow);
}
```

**Behavior**:
- Returns `false` if the label is `null`
- Returns `false` if the label is not in the flashing states dictionary
- Returns the current flashing state (may be `true` or `false` depending on the current flash cycle)

### SetFlashInterval

#### `SetFlashInterval(int interval)`

Sets the flash interval for a currently flashing `ToolStripStatusLabel`.

**Parameters**:
- `interval` (`int`): The new flash interval in milliseconds. Must be greater than zero.

**Exceptions**:
- `ArgumentNullException`: Thrown when `label` is `null`.
- `ArgumentOutOfRangeException`: Thrown when `interval` is less than or equal to zero.
- `InvalidOperationException`: Thrown when the label is not currently flashing.

**Example**:
```csharp
if (statusLabel.IsFlashing())
{
    // Change from fast to slow
    statusLabel.SetFlashInterval(1000);
}
```

**Behavior**:
- Only works if the label is currently flashing
- Updates the timer interval immediately
- The change takes effect on the next flash cycle

---

## Usage Examples

### Example 1: Basic Alert Notification

```csharp
using Krypton.Utilities;

public partial class MainForm : KryptonForm
{
    private void ShowErrorAlert(string message)
    {
        errorStatusLabel.Text = $"Error: {message}";
        errorStatusLabel.StartFlashing(
            flashForeColor: Color.White,
            flashBackColor: Color.Red,
            interval: 500
        );
    }

    private void ClearErrorAlert()
    {
        errorStatusLabel.StopFlashing();
        errorStatusLabel.Text = "Ready";
    }
}
```

### Example 2: Using Settings Object

```csharp
using Krypton.Utilities;

public partial class MainForm : KryptonForm
{
    private readonly FlashingAlertSettings _warningSettings = new()
    {
        FlashForeColor = Color.Black,
        FlashBackColor = Color.Yellow,
        Interval = 600
    };

    private readonly FlashingAlertSettings _errorSettings = new()
    {
        FlashForeColor = Color.White,
        FlashBackColor = Color.DarkRed,
        Interval = 400
    };

    private void ShowWarning(string message)
    {
        warningStatusLabel.Text = $"Warning: {message}";
        warningStatusLabel.StartFlashing(_warningSettings);
    }

    private void ShowError(string message)
    {
        errorStatusLabel.Text = $"Error: {message}";
        errorStatusLabel.StartFlashing(_errorSettings);
    }
}
```

### Example 3: Toggle Flashing

```csharp
using Krypton.Utilities;

private void ToggleAlertButton_Click(object sender, EventArgs e)
{
    if (statusLabel.IsFlashing())
    {
        statusLabel.StopFlashing();
        toggleButton.Text = "Start Alert";
    }
    else
    {
        statusLabel.StartFlashing(Color.Red, Color.Yellow, 500);
        toggleButton.Text = "Stop Alert";
    }
}
```

### Example 4: Dynamic Interval Changes

```csharp
using Krypton.Utilities;

private void SpeedUpAlert()
{
    if (statusLabel.IsFlashing())
    {
        // Get current interval and make it faster
        var currentInterval = GetCurrentInterval(statusLabel);
        var newInterval = Math.Max(100, currentInterval - 100);
        statusLabel.SetFlashInterval(newInterval);
    }
}

private int GetCurrentInterval(ToolStripStatusLabel label)
{
    // Note: There's no direct method to get current interval
    // You'll need to track it yourself if needed
    return 500; // Example
}
```

### Example 5: Multiple Labels with Different Settings

```csharp
using Krypton.Utilities;

public partial class MainForm : KryptonForm
{
    private readonly FlashingAlertSettings _fastAlert = new()
    {
        FlashForeColor = Color.White,
        FlashBackColor = Color.Red,
        Interval = 300
    };

    private readonly FlashingAlertSettings _slowAlert = new()
    {
        FlashForeColor = Color.Black,
        FlashBackColor = Color.Orange,
        Interval = 800
    };

    private void StartAllAlerts()
    {
        statusLabel1.StartFlashing(_fastAlert);
        statusLabel2.StartFlashing(_slowAlert);
        statusLabel3.StartFlashing(Color.Blue, Color.White, 500);
    }

    private void StopAllAlerts()
    {
        statusLabel1.StopFlashing();
        statusLabel2.StopFlashing();
        statusLabel3.StopFlashing();
    }
}
```

### Example 6: Auto-Stop After Duration

```csharp
using Krypton.Utilities;
using System.Windows.Forms;

public partial class MainForm : KryptonForm
{
    private Timer _autoStopTimer;

    private void ShowTemporaryAlert(string message, int durationMs)
    {
        statusLabel.Text = message;
        statusLabel.StartFlashing(Color.Red, Color.Yellow, 500);

        // Auto-stop after duration
        _autoStopTimer?.Stop();
        _autoStopTimer = new Timer { Interval = durationMs };
        _autoStopTimer.Tick += (s, e) =>
        {
            statusLabel.StopFlashing();
            _autoStopTimer.Stop();
            _autoStopTimer.Dispose();
            _autoStopTimer = null;
        };
        _autoStopTimer.Start();
    }
}
```

### Example 7: Conditional Flashing Based on State

```csharp
using Krypton.Utilities;

public enum ConnectionStatus
{
    Connected,
    Disconnected,
    Connecting,
    Error
}

private void UpdateConnectionStatus(ConnectionStatus status)
{
    switch (status)
    {
        case ConnectionStatus.Connected:
            statusLabel.Text = "Connected";
            statusLabel.StopFlashing();
            statusLabel.ForeColor = Color.Green;
            break;

        case ConnectionStatus.Disconnected:
            statusLabel.Text = "Disconnected";
            statusLabel.StopFlashing();
            statusLabel.ForeColor = Color.Gray;
            break;

        case ConnectionStatus.Connecting:
            statusLabel.Text = "Connecting...";
            statusLabel.StartFlashing(Color.Blue, Color.LightBlue, 600);
            break;

        case ConnectionStatus.Error:
            statusLabel.Text = "Connection Error";
            statusLabel.StartFlashing(Color.White, Color.Red, 400);
            break;
    }
}
```

---

## Designer Support

The `FlashingAlertSettings` class is decorated with `[TypeConverter(typeof(ExpandableObjectConverter))]`, which allows it to appear as an expandable object in the Visual Studio property grid.

### Using in Designer

While you cannot directly set flashing properties on a `ToolStripStatusLabel` in the designer (since these are extension methods), you can:

1. **Create Settings Objects**: Create `FlashingAlertSettings` instances as fields or properties in your form/control class
2. **Configure in Property Grid**: Select the settings object in the property grid and expand it to configure properties
3. **Use in Code**: Call the extension methods in your code-behind

### Example: Designer-Friendly Settings

```csharp
public partial class MainForm : KryptonForm
{
    // These will appear in the property grid as expandable objects
    [Category("Alerts")]
    [Description("Settings for warning alerts")]
    public FlashingAlertSettings WarningAlertSettings { get; set; } = new()
    {
        FlashForeColor = Color.Black,
        FlashBackColor = Color.Yellow,
        Interval = 600
    };

    [Category("Alerts")]
    [Description("Settings for error alerts")]
    public FlashingAlertSettings ErrorAlertSettings { get; set; } = new()
    {
        FlashForeColor = Color.White,
        FlashBackColor = Color.Red,
        Interval = 400
    };

    public MainForm()
    {
        InitializeComponent();
    }

    private void ShowWarning()
    {
        statusLabel.StartFlashing(WarningAlertSettings);
    }

    private void ShowError()
    {
        statusLabel.StartFlashing(ErrorAlertSettings);
    }
}
```

---

## Implementation Details

### Internal State Management

The extension methods use a static `Dictionary<ToolStripStatusLabel, FlashingState>` to track the flashing state of each label. This allows:

- Multiple labels to flash independently
- Proper cleanup when labels are disposed
- State tracking per label

### Timer Usage

Each flashing label uses a `System.Windows.Forms.Timer` instance:

- **Thread Safety**: `System.Windows.Forms.Timer` raises events on the UI thread, ensuring thread safety
- **Interval**: Configurable via the `Interval` parameter or `FlashingAlertSettings.Interval` property
- **Lifecycle**: Timer is created when flashing starts and disposed when flashing stops

### Color Management

Original colors are stored when flashing starts:

- **Original Colors**: `ForeColor` and `BackColor` are captured before flashing begins
- **Restoration**: Original colors are restored when flashing stops
- **Flash Colors**: If `Color.Empty` or `null` is provided, original colors are used (no visual change)

### Disposal Handling

The implementation automatically handles disposal:

- **Event Hook**: The label's `Disposed` event is hooked when flashing starts
- **Cleanup**: When the label is disposed, flashing is automatically stopped
- **Resource Cleanup**: Timers are properly disposed to prevent memory leaks

### Thread Safety

- **UI Thread Only**: All operations must be performed on the UI thread
- **Timer Events**: Timer tick events are raised on the UI thread automatically
- **No Locking**: No explicit locking is used as all operations are expected on the UI thread

---

## Best Practices

### 1. Use Appropriate Colors

Choose colors that provide good contrast and are easily visible:

```csharp
// Good: High contrast
statusLabel.StartFlashing(Color.White, Color.Red, 500);

// Good: Warning colors
statusLabel.StartFlashing(Color.Black, Color.Yellow, 600);

// Avoid: Low contrast
statusLabel.StartFlashing(Color.LightGray, Color.White, 500);
```

### 2. Set Reasonable Intervals

- **Fast Alerts**: 200-400ms for urgent/critical alerts
- **Normal Alerts**: 500-700ms for general notifications
- **Slow Alerts**: 800-1000ms for less urgent information
- **Avoid**: Intervals below 100ms (too fast, may cause eye strain)

### 3. Stop Flashing When Appropriate

Always stop flashing when the condition that triggered it is resolved:

```csharp
// Good: Stop when condition changes
if (connectionStatus == ConnectionStatus.Connected)
{
    statusLabel.StopFlashing();
    statusLabel.Text = "Connected";
}

// Avoid: Leaving alerts flashing indefinitely
```

### 4. Use Settings Objects for Reusability

Create settings objects for common alert types:

```csharp
private static readonly FlashingAlertSettings ErrorAlert = new()
{
    FlashForeColor = Color.White,
    FlashBackColor = Color.Red,
    Interval = 400
};

private static readonly FlashingAlertSettings WarningAlert = new()
{
    FlashForeColor = Color.Black,
    FlashBackColor = Color.Yellow,
    Interval = 600
};

private static readonly FlashingAlertSettings InfoAlert = new()
{
    FlashForeColor = Color.Blue,
    FlashBackColor = Color.LightBlue,
    Interval = 800
};
```

### 5. Check State Before Operations

Check if flashing before performing operations:

```csharp
// Good: Check before changing interval
if (statusLabel.IsFlashing())
{
    statusLabel.SetFlashInterval(1000);
}

// Good: Check before starting (optional, but safe)
if (!statusLabel.IsFlashing())
{
    statusLabel.StartFlashing(Color.Red, Color.Yellow);
}
```

### 6. Handle Multiple Labels

When managing multiple labels, consider using helper methods:

```csharp
private void StartAllAlerts()
{
    statusLabel1.StartFlashing(_errorSettings);
    statusLabel2.StartFlashing(_warningSettings);
    statusLabel3.StartFlashing(_infoSettings);
}

private void StopAllAlerts()
{
    statusLabel1.StopFlashing();
    statusLabel2.StopFlashing();
    statusLabel3.StopFlashing();
}
```

### 7. Clean Up on Form Close

Ensure all flashing stops when the form closes:

```csharp
protected override void OnFormClosing(FormClosingEventArgs e)
{
    // Stop all flashing alerts
    statusLabel1.StopFlashing();
    statusLabel2.StopFlashing();
    statusLabel3.StopFlashing();

    base.OnFormClosing(e);
}
```

---

## Troubleshooting

### Issue: Label Doesn't Flash

**Symptoms**: Calling `StartFlashing()` but the label doesn't flash.

**Possible Causes**:
1. Colors are the same as original colors
2. Label is disposed
3. Form is not visible

**Solutions**:
```csharp
// Ensure colors are different
statusLabel.StartFlashing(Color.Red, Color.Yellow, 500);

// Check if label is disposed
if (!statusLabel.IsDisposed)
{
    statusLabel.StartFlashing(Color.Red, Color.Yellow, 500);
}

// Ensure form is visible
form.Show();
statusLabel.StartFlashing(Color.Red, Color.Yellow, 500);
```

### Issue: Colors Not Restored After Stop

**Symptoms**: After calling `StopFlashing()`, colors don't return to original.

**Possible Causes**:
1. Label was disposed during flashing
2. Colors were changed externally

**Solutions**:
```csharp
// Stop flashing before disposing
statusLabel.StopFlashing();
statusLabel.Dispose();

// Or check before stopping
if (!statusLabel.IsDisposed && statusLabel.IsFlashing())
{
    statusLabel.StopFlashing();
}
```

### Issue: InvalidOperationException When Changing Interval

**Symptoms**: `SetFlashInterval()` throws `InvalidOperationException`.

**Possible Causes**:
1. Label is not currently flashing

**Solutions**:
```csharp
// Check before changing interval
if (statusLabel.IsFlashing())
{
    statusLabel.SetFlashInterval(1000);
}
```

### Issue: Memory Leaks

**Symptoms**: Memory usage increases over time.

**Possible Causes**:
1. Labels are not properly disposed
2. Flashing is not stopped before disposal

**Solutions**:
```csharp
// The implementation automatically handles disposal, but ensure:
// 1. Stop flashing before disposing labels
statusLabel.StopFlashing();

// 2. Or let the automatic disposal handler work (it's hooked automatically)
```

### Issue: Flashing Too Fast/Slow

**Symptoms**: Flashing speed is not appropriate.

**Solutions**:
```csharp
// Adjust interval
statusLabel.SetFlashInterval(800); // Slower

// Or restart with new interval
statusLabel.StopFlashing();
statusLabel.StartFlashing(Color.Red, Color.Yellow, 200); // Faster
```

### Issue: Multiple Labels Flashing Out of Sync

**Symptoms**: Multiple labels flash at different rates or times.

**Explanation**: This is expected behavior. Each label has its own timer and flashes independently.

**Solutions**:
```csharp
// If you need synchronized flashing, use the same interval
var settings = new FlashingAlertSettings { Interval = 500 };
label1.StartFlashing(settings);
label2.StartFlashing(settings);
label3.StartFlashing(settings);

// Note: They may still be slightly out of sync due to timer precision
```

---

## Performance Considerations

### Timer Overhead

- Each flashing label uses one `System.Windows.Forms.Timer` instance
- Timer overhead is minimal for typical use cases
- For many labels (50+), consider if flashing is necessary for all simultaneously

### Memory Usage

- Each flashing label stores a `FlashingState` object in a static dictionary
- Memory is automatically cleaned up when labels are disposed
- No memory leaks if labels are properly disposed

### UI Thread Impact

- Timer events are raised on the UI thread
- Color changes are lightweight operations
- Multiple flashing labels should not significantly impact UI responsiveness

### Recommendations

- **Limit Simultaneous Flashing**: Avoid flashing more than 5-10 labels simultaneously
- **Use Appropriate Intervals**: Don't use very short intervals (< 100ms) as they may impact performance
- **Stop When Not Needed**: Always stop flashing when the alert condition is resolved

---

## Related Issues

### Known Limitations

1. **No Synchronization**: Labels flash independently; there's no built-in synchronization mechanism
2. **No Fade Effects**: Only supports instant color changes, not smooth transitions
3. **No Sound Support**: Visual flashing only; no audio feedback
4. **Windows Forms Only**: Works only with `System.Windows.Forms.ToolStripStatusLabel`, not WPF or other UI frameworks

### Future Enhancements

Potential future improvements:

- Smooth color transitions (fade effects)
- Synchronized flashing for multiple labels
- Audio feedback support
- Flash count limits (auto-stop after N flashes)
- Custom flash patterns (e.g., flash 3 times then stop)

---

## Code Examples Summary

### Quick Reference

```csharp
using Krypton.Utilities;

// Start flashing (default: 500ms, original colors)
label.StartFlashing();

// Start with colors
label.StartFlashing(Color.Red, Color.Yellow, 500);

// Start with settings
var settings = new FlashingAlertSettings
{
    FlashForeColor = Color.White,
    FlashBackColor = Color.Red,
    Interval = 400
};
label.StartFlashing(settings);

// Stop flashing
label.StopFlashing();

// Check status
if (label.IsFlashing())
{
    // Change interval
    label.SetFlashInterval(1000);
}
```

---

## Version History

- **Initial Release**: 2026
  - Added `FlashingAlertSettings` class
  - Added `StartFlashing()` extension methods (overloads)
  - Added `StopFlashing()` extension method
  - Added `IsFlashing()` extension method
  - Added `SetFlashInterval()` extension method
  - Full designer support with `ExpandableObjectConverter`

---

## See Also

- [Krypton Toolkit Documentation](https://github.com/Krypton-Suite/Standard-Toolkit)
- [ToolStripStatusLabel MSDN Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripstatuslabel)
- [System.Windows.Forms.Timer Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.timer)

---

## License

This feature is part of the Krypton Standard Toolkit and is licensed under the [New BSD 3-Clause License](https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE).

---

*Documentation last updated: 2026*
