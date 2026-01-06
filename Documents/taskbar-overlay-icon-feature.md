# Taskbar Overlay Icon Feature

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Classes](#classes)
5. [Usage Examples](#usage-examples)
6. [Designer Support](#designer-support)
7. [Implementation Details](#implementation-details)
8. [Best Practices](#best-practices)
9. [Troubleshooting](#troubleshooting)
10. [Platform Compatibility](#platform-compatibility)
11. [Related Issues](#related-issues)

---

## Overview

The Taskbar Overlay Icon feature allows you to display a small overlay icon on the Windows taskbar button for your application. This is useful for showing status indicators, notification badges, or other visual cues directly on the taskbar icon without modifying the main application icon.

### Key Features

- **Windows 7+ Support**: Uses the native Windows ITaskbarList3 API
- **Developer Controlled**: Fully programmable via properties
- **Designer Support**: Full Visual Studio designer integration with expandable properties
- **Automatic Updates**: Overlay icon updates automatically when properties change
- **Tooltip Support**: Optional description text shown in taskbar tooltip
- **Error Handling**: Gracefully handles unsupported platforms and errors
- **Fixed Position**: Overlay icon is always displayed at the lower-right corner of the taskbar button (controlled by Windows, cannot be changed)

### Use Cases

- **Notification Badges**: Display unread message counts or notification indicators
- **Status Indicators**: Show application status (online, offline, processing, error)
- **Progress Indicators**: Visual feedback for background operations
- **Alert Badges**: Highlight important events or warnings

### Requirements

- **Windows Version**: Windows 7 or later (ITaskbarList3 API requirement)
- **Form Handle**: Form must have a valid window handle
- **Taskbar Visibility**: Form must be visible and shown in taskbar (`ShowInTaskbar = true`)

---

## Quick Start

### Basic Usage

```csharp
// Create a form with taskbar overlay icon
var form = new KryptonForm();
form.Text = "My Application";

// Set notification badge overlay
var notificationIcon = new Icon("notification.ico", 16, 16);
form.TaskbarOverlayIcon.Icon = notificationIcon;
form.TaskbarOverlayIcon.Description = "You have new messages";
```

### Designer Usage

1. Select a `KryptonForm` in the designer
2. In the Properties window, find the `TaskbarOverlayIcon` property
3. Expand the `TaskbarOverlayIcon` property (it appears as an expandable object)
4. Set the `Icon` and `Description` properties as needed

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Classes

### TaskbarOverlayIconValues

Storage class for taskbar overlay icon value information. This class uses `ExpandableObjectConverter` for designer support.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Icon` | `Icon?` | `null` | The overlay icon to display on the taskbar button. Typically a small 16x16 icon. The icon is always positioned at the lower-right corner of the taskbar button by Windows (position cannot be changed). |
| `Description` | `string` | `""` | Description text for the overlay icon, shown in the taskbar tooltip. |

#### Methods

##### `CopyFrom(TaskbarOverlayIconValues source)`

Copies all taskbar overlay icon values from another instance.

```csharp
var source = new TaskbarOverlayIconValues(needPaint);
source.Icon = myOverlayIcon;
source.Description = "Status indicator";

var target = new TaskbarOverlayIconValues(needPaint);
target.CopyFrom(source); // Copies all properties
```

#### Serialization Methods

- `ShouldSerializeIcon()` - Returns `true` if Icon is not null
- `ShouldSerializeDescription()` - Returns `true` if Description is not empty

#### Reset Methods

- `ResetIcon()` - Sets Icon to null
- `ResetDescription()` - Sets Description to empty string

---

### KryptonForm Properties

#### `TaskbarOverlayIcon`

Gets access to the taskbar overlay icon values.

```csharp
[Category(@"Visuals")]
[Description(@"Taskbar overlay icon to display on the taskbar button.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public TaskbarOverlayIconValues TaskbarOverlayIcon { get; }
```

**Usage**:
```csharp
form.TaskbarOverlayIcon.Icon = myIcon;
form.TaskbarOverlayIcon.Description = "Status: Online";
```

#### `ResetTaskbarOverlayIcon()`

Resets the TaskbarOverlayIcon property to its default value.

```csharp
public void ResetTaskbarOverlayIcon() => TaskbarOverlayIcon.Reset();
```

#### `ShouldSerializeTaskbarOverlayIcon()`

Indicates whether the TaskbarOverlayIcon property should be serialized.

```csharp
public bool ShouldSerializeTaskbarOverlayIcon() => !TaskbarOverlayIcon.IsDefault;
```

---

## Usage Examples

### Example 1: Basic Overlay Icon

```csharp
var form = new KryptonForm();
form.Text = "My Application";

// Set notification badge overlay
var notificationIcon = new Icon("notification.ico", 16, 16);
form.TaskbarOverlayIcon.Icon = notificationIcon;
form.TaskbarOverlayIcon.Description = "You have new messages";
```

### Example 2: Dynamic Overlay Updates

```csharp
private void UpdateNotificationBadge(int count)
{
    if (count > 0)
    {
        // Create badge icon with count
        var badge = CreateBadgeIcon(count, Color.Red);
        this.TaskbarOverlayIcon.Icon = badge;
        this.TaskbarOverlayIcon.Description = $"{count} new notifications";
    }
    else
    {
        // Clear overlay when no notifications
        this.TaskbarOverlayIcon.Icon = null;
        this.TaskbarOverlayIcon.Description = string.Empty;
    }
}

private Icon CreateBadgeIcon(int count, Color color)
{
    var bitmap = new Bitmap(16, 16);
    using (var g = Graphics.FromImage(bitmap))
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        
        // Red circular background
        using (var brush = new SolidBrush(color))
        {
            g.FillEllipse(brush, 0, 0, 16, 16);
        }
        
        // White border
        using (var pen = new Pen(Color.White, 1))
        {
            g.DrawEllipse(pen, 0, 0, 15, 15);
        }
        
        // Count text (centered)
        var text = count > 99 ? "99+" : count.ToString();
        using (var font = new Font("Arial", 7, FontStyle.Bold))
        using (var brush = new SolidBrush(Color.White))
        using (var sf = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        })
        {
            var rect = new RectangleF(0, 0, 16, 16);
            g.DrawString(text, font, brush, rect, sf);
        }
    }
    return Icon.FromHandle(bitmap.GetHicon());
}
```

### Example 3: Status Indicators

```csharp
public enum ApplicationStatus
{
    Normal,
    Warning,
    Error,
    Processing
}

private void SetStatus(ApplicationStatus status)
{
    Icon? overlayIcon = status switch
    {
        ApplicationStatus.Warning => Properties.Resources.WarningIcon,
        ApplicationStatus.Error => Properties.Resources.ErrorIcon,
        ApplicationStatus.Processing => Properties.Resources.ProcessingIcon,
        _ => null
    };

    this.TaskbarOverlayIcon.Icon = overlayIcon;
    this.TaskbarOverlayIcon.Description = status switch
    {
        ApplicationStatus.Warning => "Warning: Check application status",
        ApplicationStatus.Error => "Error: Action required",
        ApplicationStatus.Processing => "Processing...",
        _ => string.Empty
    };
}
```

### Example 4: Notification System Integration

```csharp
public class NotificationManager
{
    private readonly KryptonForm _form;
    private int _unreadCount;

    public NotificationManager(KryptonForm form)
    {
        _form = form;
        _unreadCount = 0;
    }

    public void AddNotification()
    {
        _unreadCount++;
        UpdateTaskbarBadge();
    }

    public void ClearNotifications()
    {
        _unreadCount = 0;
        UpdateTaskbarBadge();
    }

    private void UpdateTaskbarBadge()
    {
        if (_unreadCount > 0)
        {
            var badge = CreateNotificationBadge(_unreadCount);
            _form.TaskbarOverlayIcon.Icon = badge;
            _form.TaskbarOverlayIcon.Description = 
                _unreadCount == 1 
                    ? "1 unread notification" 
                    : $"{_unreadCount} unread notifications";
        }
        else
        {
            _form.TaskbarOverlayIcon.Icon = null;
            _form.TaskbarOverlayIcon.Description = string.Empty;
        }
    }

    private Icon CreateNotificationBadge(int count)
    {
        // Implementation similar to Example 2
        // ...
    }
}
```

### Example 5: Progress Indicator

```csharp
public class ProgressIndicator
{
    private readonly KryptonForm _form;
    private Icon? _processingIcon;

    public ProgressIndicator(KryptonForm form)
    {
        _form = form;
        _processingIcon = Properties.Resources.ProcessingIcon;
    }

    public void ShowProgress()
    {
        _form.TaskbarOverlayIcon.Icon = _processingIcon;
        _form.TaskbarOverlayIcon.Description = "Processing...";
    }

    public void HideProgress()
    {
        _form.TaskbarOverlayIcon.Icon = null;
        _form.TaskbarOverlayIcon.Description = string.Empty;
    }
}
```

### Example 6: Multi-Form Application

```csharp
public static class TaskbarOverlayManager
{
    private static readonly Dictionary<Form, Icon?> _overlayIcons = new();

    public static void SetOverlayIcon(Form form, Icon? icon, string description)
    {
        if (form is KryptonForm kryptonForm)
        {
            kryptonForm.TaskbarOverlayIcon.Icon = icon;
            kryptonForm.TaskbarOverlayIcon.Description = description;
            _overlayIcons[form] = icon;
        }
    }

    public static void ClearOverlayIcon(Form form)
    {
        if (form is KryptonForm kryptonForm)
        {
            kryptonForm.TaskbarOverlayIcon.Icon = null;
            kryptonForm.TaskbarOverlayIcon.Description = string.Empty;
            _overlayIcons.Remove(form);
        }
    }

    public static void ClearAllOverlayIcons()
    {
        foreach (var form in _overlayIcons.Keys.ToList())
        {
            ClearOverlayIcon(form);
        }
    }
}
```

---

## Designer Support

### Property Grid Integration

The `TaskbarOverlayIconValues` class uses `ExpandableObjectConverter`, which means in the Visual Studio designer:

1. The `TaskbarOverlayIcon` property appears as an expandable node in the Properties window
2. All overlay icon properties are grouped under this node
3. Properties can be edited directly in the designer
4. Changes are serialized to the `.Designer.cs` file

### Designer Code Generation

When you configure taskbar overlay icons in the designer, code similar to this is generated:

```csharp
// 
// kryptonForm1
// 
this.kryptonForm1.TaskbarOverlayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("kryptonForm1.TaskbarOverlayIcon.Icon")));
this.kryptonForm1.TaskbarOverlayIcon.Description = "New notifications";
```

### Designer Best Practices

1. **Use Resource Files**: Store overlay icons in resource files for better designer support
2. **Set Defaults**: Use sensible defaults so forms look good even before customization
3. **Test at Runtime**: Verify overlay icons display correctly at runtime (designer doesn't show taskbar)
4. **Document Custom Values**: If creating custom implementations, ensure proper designer attributes

---

## Implementation Details

### Windows API Integration

The feature uses the Windows `ITaskbarList3` COM interface, which is available on Windows 7 and later:

#### Interface Definition

```csharp
[ComImport]
[Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface ITaskbarList3
{
    void SetOverlayIcon(IntPtr hwnd, IntPtr hIcon, string pszDescription);
    // ... other methods
}
```

#### COM Object Creation

```csharp
[ComImport]
[Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
[ClassInterface(ClassInterfaceType.None)]
internal class TaskbarList
{
}
```

#### Method Call

```csharp
var taskbarList = (ITaskbarList3)new TaskbarList();
taskbarList.HrInit();
taskbarList.SetOverlayIcon(Handle, hIcon, description);
```

**Important Note on Position**: The `SetOverlayIcon` method does not accept a position parameter. Windows automatically positions the overlay icon at the **lower-right corner** of the taskbar button. This position is fixed by the Windows shell and cannot be changed programmatically. If you need control over overlay position, consider using the [Overlay Image Feature](./overlay-image-feature.md) for control-level overlays instead.

### Update Mechanism

The overlay icon is automatically updated:

1. **On Handle Creation**: When the form handle is created (`OnHandleCreated`)
2. **On Property Change**: When the `Icon` or `Description` properties change
3. **Event-Driven**: Uses internal event notification system

### Error Handling

The implementation includes comprehensive error handling:

- **Platform Check**: Verifies Windows 7+ before attempting to use API
- **Handle Validation**: Ensures form handle is created before setting overlay
- **COM Exception Handling**: Catches and logs COM-related errors
- **Graceful Degradation**: Silently fails on unsupported platforms

### Icon Size Recommendations

- **Recommended Size**: 16x16 pixels
- **Maximum Size**: Windows will scale larger icons, but 16x16 is optimal
- **Format**: Standard Windows icon format (.ico)
- **Transparency**: Icons should use transparency for best visual effect
- **DPI Awareness**: Icons are automatically scaled by Windows based on DPI

### Position Limitations

**Important**: The overlay icon position is **fixed by Windows** and cannot be changed:

- **Fixed Position**: The overlay icon is always displayed at the **lower-right corner** of the taskbar button
- **No Position Control**: The Windows `ITaskbarList3::SetOverlayIcon` API does not provide any position parameters
- **System-Controlled**: This is a limitation of the Windows shell, not the Krypton Toolkit
- **Alternative**: If you need position control, use the [Overlay Image Feature](./overlay-image-feature.md) for control-level overlays which supports TopLeft, TopRight, BottomLeft, and BottomRight positions

### Performance Considerations

- **Icon Caching**: Cache icon instances instead of creating new ones repeatedly
- **Update Frequency**: Avoid updating overlay icon too frequently (throttle if needed)
- **Resource Management**: Dispose icons properly when no longer needed

---

## Best Practices

### 1. Icon Design

- **Size**: Use 16x16 pixel icons for optimal display
- **Transparency**: Ensure icons have proper transparency for clean overlay effect
- **Contrast**: Use high-contrast colors for visibility on various backgrounds
- **Simplicity**: Keep icons simple and recognizable at small sizes
- **Position Awareness**: Design icons knowing they will always appear in the lower-right corner of the taskbar button (Windows limitation)

### 2. Icon Management

- **Resource Files**: Store icons in resource files for easy management
- **Caching**: Cache frequently used icons
- **Disposal**: Dispose icons when no longer needed to prevent memory leaks

```csharp
private Icon? _cachedIcon;

private Icon GetNotificationIcon()
{
    if (_cachedIcon == null)
    {
        _cachedIcon = Properties.Resources.NotificationIcon;
    }
    return _cachedIcon;
}

protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _cachedIcon?.Dispose();
    }
    base.Dispose(disposing);
}
```

### 3. Update Patterns

- **Clear When Done**: Set `Icon` to `null` when the overlay is no longer needed
- **Batch Updates**: Update both Icon and Description together for consistency
- **State Management**: Use enums or constants to manage overlay states

```csharp
public enum OverlayState
{
    None,
    Notification,
    Warning,
    Error
}

private void SetOverlayState(OverlayState state)
{
    switch (state)
    {
        case OverlayState.None:
            TaskbarOverlayIcon.Icon = null;
            TaskbarOverlayIcon.Description = string.Empty;
            break;
        case OverlayState.Notification:
            TaskbarOverlayIcon.Icon = _notificationIcon;
            TaskbarOverlayIcon.Description = "New notifications";
            break;
        // ... other states
    }
}
```

### 4. Thread Safety

- **UI Thread**: Always update overlay icon from the UI thread
- **Invoke Required**: Use `Invoke` or `BeginInvoke` when updating from background threads

```csharp
private void UpdateOverlayFromBackgroundThread(Icon icon, string description)
{
    if (InvokeRequired)
    {
        BeginInvoke(new Action(() => UpdateOverlayFromBackgroundThread(icon, description)));
        return;
    }

    TaskbarOverlayIcon.Icon = icon;
    TaskbarOverlayIcon.Description = description;
}
```

### 5. Error Handling

- **Platform Check**: Verify Windows version before using feature
- **Handle Check**: Ensure form handle is created
- **Exception Handling**: Wrap API calls in try-catch blocks

```csharp
private void SafeUpdateOverlayIcon(Icon? icon, string description)
{
    if (Environment.OSVersion.Version < new Version(6, 1))
    {
        return; // Not supported on Windows Vista or earlier
    }

    if (!IsHandleCreated)
    {
        HandleCreated += (s, e) => SafeUpdateOverlayIcon(icon, description);
        return;
    }

    try
    {
        TaskbarOverlayIcon.Icon = icon;
        TaskbarOverlayIcon.Description = description;
    }
    catch (Exception ex)
    {
        // Log error but don't crash
        Debug.WriteLine($"Failed to update taskbar overlay: {ex.Message}");
    }
}
```

---

## Troubleshooting

### Overlay Icon Not Appearing

**Problem**: Overlay icon is not visible on taskbar.

**Solutions**:
1. **Verify Windows Version**: Ensure Windows 7 or later
   ```csharp
   if (Environment.OSVersion.Version < new Version(6, 1))
   {
       // Not supported
   }
   ```

2. **Check Form Handle**: Ensure form has a valid window handle
   ```csharp
   if (!IsHandleCreated)
   {
       HandleCreated += (s, e) => UpdateOverlayIcon();
       return;
   }
   ```

3. **Verify Icon**: Ensure icon is not null and is a valid icon
   ```csharp
   if (icon == null || icon.Handle == IntPtr.Zero)
   {
       // Invalid icon
   }
   ```

4. **Check Taskbar Visibility**: Ensure form is visible and shown in taskbar
   ```csharp
   if (!ShowInTaskbar)
   {
       ShowInTaskbar = true;
   }
   ```

5. **Icon Size**: Verify icon size is appropriate (16x16 recommended)

### Icon Not Updating

**Problem**: Overlay icon doesn't update when properties change.

**Solutions**:
1. **Handle Creation**: Ensure form handle is created before setting overlay icon
2. **Property Assignment**: Verify you're setting properties on the correct form instance
3. **Icon Handle**: Check that the icon handle is valid
4. **Thread Safety**: Ensure updates are on the UI thread

### COM Exception

**Problem**: COM exception when setting overlay icon.

**Solutions**:
1. **Windows Version**: Ensure Windows 7 or later
2. **COM Registration**: Check that COM registration is correct (should be automatic)
3. **Form Handle**: Verify form handle is valid
4. **Error Logging**: Check Windows event logs for details

### Icon Display Issues

**Problem**: Icon appears distorted or incorrectly sized.

**Solutions**:
1. **Icon Size**: Use 16x16 pixel icons
2. **Icon Format**: Ensure proper .ico format with transparency
3. **DPI Scaling**: Windows handles DPI scaling automatically
4. **Icon Quality**: Use high-quality icons designed for small sizes

### Icon Position Cannot Be Changed

**Problem**: Need to position overlay icon in a different location (e.g., top-left, top-right).

**Explanation**: The Windows `ITaskbarList3::SetOverlayIcon` API does not support position parameters. The overlay icon is **always** displayed at the lower-right corner of the taskbar button by the Windows shell. This is a system limitation and cannot be changed programmatically.

**Alternatives**:
1. **Control-Level Overlays**: Use the [Overlay Image Feature](./overlay-image-feature.md) for `KryptonButton`, `KryptonLabel`, and other controls, which supports full position control (TopLeft, TopRight, BottomLeft, BottomRight)
2. **Custom Taskbar Implementation**: Not recommended - would require extensive Windows shell integration
3. **Accept Limitation**: Design your overlay icons to work well in the lower-right corner position

### Performance Issues

**Problem**: Slow performance when updating overlay icon frequently.

**Solutions**:
1. **Throttle Updates**: Limit update frequency
2. **Cache Icons**: Reuse icon instances
3. **Batch Updates**: Update multiple properties together
4. **Lazy Loading**: Only create icons when needed

---

## Platform Compatibility

### Supported Platforms

| Platform | Version | Support Level |
|----------|---------|---------------|
| Windows 7 | All versions | ✅ Fully Supported |
| Windows 8 | All versions | ✅ Fully Supported |
| Windows 8.1 | All versions | ✅ Fully Supported |
| Windows 10 | All versions | ✅ Fully Supported |
| Windows 11 | All versions | ✅ Fully Supported |
| Windows Server 2008 R2 | All versions | ✅ Fully Supported |
| Windows Server 2012+ | All versions | ✅ Fully Supported |
| Windows Vista | All versions | ❌ Not Supported |
| Windows XP | All versions | ❌ Not Supported |

### Feature Detection

```csharp
public static bool IsTaskbarOverlaySupported()
{
    // ITaskbarList3 requires Windows 7+
    return Environment.OSVersion.Version >= new Version(6, 1);
}
```

### Graceful Degradation

The implementation gracefully handles unsupported platforms:

- **Automatic Detection**: Checks Windows version before using API
- **Silent Failure**: Does not throw exceptions on unsupported platforms
- **Error Logging**: Logs errors for debugging without crashing

---

## Related Issues

- **GitHub Issue**: [#1214](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1214) - Implement "Overlay Icons" on Form Task bar images

---

## See Also

- [Windows Taskbar Extensions Documentation](https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist3-setoverlayicon)
- [Overlay Image Feature](./overlay-image-feature.md) - For control-level overlay images
- [Krypton Toolkit Documentation](https://github.com/Krypton-Suite/Standard-Toolkit)

---

## Version Information

- **Introduced**: Version 110 (Issue #1214)
- **Namespace**: `Krypton.Toolkit`
- **Assembly**: `Krypton.Toolkit.dll`
- **Windows API**: ITaskbarList3 (Windows 7+)

---

## Summary

The Taskbar Overlay Icon feature provides a powerful way to display status indicators, notification badges, and other visual cues directly on the Windows taskbar button. With full designer support, automatic updates, and comprehensive error handling, it enables rich user experiences while maintaining simplicity for common use cases.

The feature is developer-controlled, meaning you have full programmatic control over when and how overlay icons are displayed, making it suitable for a wide range of application scenarios.

**Important Limitation**: The overlay icon position is fixed by Windows at the lower-right corner of the taskbar button and cannot be changed. This is a limitation of the Windows `ITaskbarList3` API, not the Krypton Toolkit. If you need position control, consider using the [Overlay Image Feature](./overlay-image-feature.md) for control-level overlays instead.

For questions or issues, please refer to [GitHub Issue #1214](https://github.com/Krypton-Suite/Standard-Toolkit/issues/1214).
