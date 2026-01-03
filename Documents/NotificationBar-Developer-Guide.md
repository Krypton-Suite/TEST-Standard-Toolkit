# Krypton Ribbon Notification Bar - Developer Guide

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
3. [API Reference](#api-reference)
4. [Usage Examples](#usage-examples)
5. [Customization Guide](#customization-guide)
6. [Event Handling](#event-handling)
7. [Best Practices](#best-practices)
8. [Advanced Scenarios](#advanced-scenarios)
9. [Troubleshooting](#troubleshooting)

---

## Overview

The Krypton Ribbon Notification Bar is a highly customizable notification system that displays informational messages, warnings, errors, and success notifications directly below the ribbon groups area. It provides an Office-style notification experience similar to Microsoft Office applications.

### Key Features

- **Multiple Notification Types**: Information, Warning, Error, Success, and Custom
- **Rich Content Support**: Icons, titles, text messages, and action buttons
- **Auto-Dismiss Timer**: Configurable automatic dismissal after a specified time
- **Fully Customizable**: Colors, padding, height, and visibility controls
- **Event-Driven**: Comprehensive event system for user interactions
- **Theme Integration**: Works seamlessly with Krypton theming system

### Visual Placement

The notification bar appears below the ribbon groups area and above the quick access toolbar (when positioned below the ribbon). It spans the full width of the ribbon control and automatically adjusts its height based on content or a fixed height setting.

---

## Architecture

### Component Structure

```
KryptonRibbon
├── NotificationBar (KryptonRibbonNotificationBarData)
│   ├── Type (RibbonNotificationBarType)
│   ├── Content (Text, Title, Icon)
│   ├── Buttons (Action buttons, Close button)
│   └── Styling (Colors, Padding, Height)
└── ViewDrawRibbonNotificationBar (View Element)
    ├── Layout Management
    ├── Rendering
    └── Mouse Interaction
```

### Key Classes

1. **KryptonRibbonNotificationBarData**: Data class containing all customization properties
2. **RibbonNotificationBarType**: Enumeration defining notification types
3. **RibbonNotificationBarEventArgs**: Event arguments for button click events
4. **ViewDrawRibbonNotificationBar**: Internal view element responsible for rendering

---

## API Reference

### KryptonRibbon.NotificationBar Property

Gets access to the notification bar customization properties.

**Type**: `KryptonRibbonNotificationBarData`  
**Category**: Appearance  
**Description**: Provides access to notification bar customization properties.

```csharp
public KryptonRibbonNotificationBarData NotificationBar { get; }
```

**Example**:
```csharp
ribbon.NotificationBar.Visible = true;
ribbon.NotificationBar.Text = "Updates available";
```

---

### KryptonRibbonNotificationBarData Class

The main data class for customizing the notification bar appearance and behavior.

#### Properties

##### Visible

Gets or sets whether the notification bar is visible.

**Type**: `bool`  
**Default**: `false`  
**Category**: Appearance

```csharp
public bool Visible { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Visible = true;  // Show notification bar
ribbon.NotificationBar.Visible = false; // Hide notification bar
```

---

##### Type

Gets or sets the type of notification bar, which determines default colors.

**Type**: `RibbonNotificationBarType`  
**Default**: `RibbonNotificationBarType.Information`  
**Category**: Appearance

```csharp
public RibbonNotificationBarType Type { get; set; }
```

**Available Types**:
- `Information` - Blue color scheme for informational messages
- `Warning` - Yellow/orange color scheme for warnings
- `Error` - Red color scheme for errors
- `Success` - Green color scheme for success messages
- `Custom` - Uses custom colors specified in `CustomBackColor`, `CustomForeColor`, and `CustomBorderColor`

**Example**:
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
```

---

##### Text

Gets or sets the main text message displayed in the notification bar.

**Type**: `string`  
**Default**: `""`  
**Category**: Appearance  
**Localizable**: Yes

```csharp
public string Text { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Text = "Updates for Office are ready to be applied, but are blocked by one or more apps.";
```

---

##### Title

Gets or sets the title text displayed before the main text (e.g., "UPDATES AVAILABLE").

**Type**: `string`  
**Default**: `""`  
**Category**: Appearance  
**Localizable**: Yes

```csharp
public string Title { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Title = "UPDATES AVAILABLE";
```

**Note**: The title is displayed in bold or with emphasis before the main text message.

---

##### Icon

Gets or sets the icon image displayed in the notification bar.

**Type**: `Image?`  
**Default**: `null`  
**Category**: Appearance

```csharp
public Image? Icon { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Icon = SystemIcons.Information.ToBitmap();
ribbon.NotificationBar.Icon = Properties.Resources.WarningIcon;
```

**Note**: The icon is automatically sized to fit the notification bar height. Recommended size is 24x24 pixels at 96 DPI.

---

##### ShowIcon

Gets or sets whether to show the icon.

**Type**: `bool`  
**Default**: `true`  
**Category**: Appearance

```csharp
public bool ShowIcon { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.ShowIcon = true;  // Display icon
ribbon.NotificationBar.ShowIcon = false; // Hide icon
```

---

##### ShowCloseButton

Gets or sets whether to show the close button (X) in the top-right corner.

**Type**: `bool`  
**Default**: `true`  
**Category**: Appearance

```csharp
public bool ShowCloseButton { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.ShowCloseButton = true;  // Show close button
ribbon.NotificationBar.ShowCloseButton = false; // Hide close button
```

**Note**: When the close button is clicked, the `NotificationBarButtonClick` event is raised with `ActionButtonIndex = -1`, and the notification bar is automatically hidden.

---

##### ShowActionButtons

Gets or sets whether to show action buttons.

**Type**: `bool`  
**Default**: `true`  
**Category**: Appearance

```csharp
public bool ShowActionButtons { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.ShowActionButtons = true;  // Show action buttons
ribbon.NotificationBar.ShowActionButtons = false; // Hide action buttons
```

---

##### ActionButtonTexts

Gets or sets the array of action button texts.

**Type**: `string[]`  
**Default**: `new[] { "Update now" }`  
**Category**: Appearance  
**Localizable**: Yes

```csharp
public string[] ActionButtonTexts { get; set; }
```

**Example**:
```csharp
// Single action button
ribbon.NotificationBar.ActionButtonTexts = new[] { "Update now" };

// Multiple action buttons
ribbon.NotificationBar.ActionButtonTexts = new[] { "Update now", "Later", "Learn more" };
```

**Note**: 
- Buttons are displayed from right to left (rightmost button first)
- Button width is automatically calculated based on text content
- Maximum recommended number of buttons is 3-4 for optimal UX

---

##### ActionButtonImages

Gets or sets the array of action button images (optional).

**Type**: `Image[]?`  
**Default**: `null`  
**Category**: Appearance

```csharp
public Image[]? ActionButtonImages { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.ActionButtonImages = new Image[]
{
    Properties.Resources.UpdateIcon,
    Properties.Resources.LaterIcon
};
```

**Note**: 
- Array length should match `ActionButtonTexts` array length
- If `null` or shorter than `ActionButtonTexts`, buttons will display text only
- Recommended image size is 16x16 pixels at 96 DPI

---

##### CustomBackColor

Gets or sets the custom background color (used when `Type` is `Custom`).

**Type**: `Color`  
**Default**: `Color.FromArgb(255, 242, 204)` (light yellow)  
**Category**: Appearance

```csharp
public Color CustomBackColor { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBackColor = Color.FromArgb(240, 240, 240);
```

---

##### CustomForeColor

Gets or sets the custom foreground (text) color (used when `Type` is `Custom`).

**Type**: `Color`  
**Default**: `Color.Black`  
**Category**: Appearance

```csharp
public Color CustomForeColor { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomForeColor = Color.DarkBlue;
```

---

##### CustomBorderColor

Gets or sets the custom border color (used when `Type` is `Custom`).

**Type**: `Color`  
**Default**: `Color.FromArgb(255, 192, 0)` (orange)  
**Category**: Appearance

```csharp
public Color CustomBorderColor { get; set; }
```

**Example**:
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBorderColor = Color.FromArgb(100, 100, 100);
```

---

##### AutoDismissSeconds

Gets or sets the number of seconds before the notification bar automatically dismisses (0 = never).

**Type**: `int`  
**Default**: `0`  
**Category**: Behavior

```csharp
public int AutoDismissSeconds { get; set; }
```

**Example**:
```csharp
// Auto-dismiss after 5 seconds
ribbon.NotificationBar.AutoDismissSeconds = 5;

// Never auto-dismiss (user must close manually)
ribbon.NotificationBar.AutoDismissSeconds = 0;
```

**Note**: 
- Timer starts when `Visible` is set to `true`
- Timer is automatically stopped if any button is clicked
- Timer is reset if `Visible` is toggled
- Recommended values: 0 (never), 5-10 seconds for informational messages, 15-30 seconds for warnings

---

##### Padding

Gets or sets the padding around the notification bar content.

**Type**: `Padding`  
**Default**: `new Padding(12, 8, 12, 8)`  
**Category**: Layout

```csharp
public Padding Padding { get; set; }
```

**Example**:
```csharp
// Increase padding for more spacious layout
ribbon.NotificationBar.Padding = new Padding(16, 10, 16, 10);

// Minimal padding
ribbon.NotificationBar.Padding = new Padding(8, 4, 8, 4);
```

**Note**: Padding values are automatically scaled for DPI.

---

##### Height

Gets or sets the height of the notification bar (0 = auto-calculate based on content).

**Type**: `int`  
**Default**: `0`  
**Category**: Layout

```csharp
public int Height { get; set; }
```

**Example**:
```csharp
// Auto-calculate height based on content
ribbon.NotificationBar.Height = 0;

// Fixed height of 50 pixels
ribbon.NotificationBar.Height = 50;
```

**Note**: 
- When set to 0, height is calculated based on content, icon size, and padding
- Minimum calculated height is approximately 40 pixels at 96 DPI
- Fixed height should account for padding and content

---

### RibbonNotificationBarType Enumeration

Defines the available notification bar types.

```csharp
public enum RibbonNotificationBarType
{
    Information,  // Blue color scheme
    Warning,      // Yellow/orange color scheme
    Error,        // Red color scheme
    Success,      // Green color scheme
    Custom        // Uses custom colors
}
```

**Color Schemes**:

| Type | Background Color | Border Color | Use Case |
|------|-----------------|--------------|----------|
| Information | Light Blue (RGB: 217, 236, 255) | Blue (RGB: 91, 155, 213) | General information, tips |
| Warning | Light Yellow (RGB: 255, 242, 204) | Orange (RGB: 255, 192, 0) | Warnings, cautions |
| Error | Light Red (RGB: 255, 204, 204) | Red (RGB: 192, 0, 0) | Errors, critical issues |
| Success | Light Green (RGB: 204, 255, 204) | Green (RGB: 0, 192, 0) | Success messages, confirmations |
| Custom | User-defined | User-defined | Brand-specific or special cases |

---

### RibbonNotificationBarEventArgs Class

Provides data for the `NotificationBarButtonClick` event.

#### Properties

##### ActionButtonIndex

Gets the index of the action button that was clicked, or -1 if the close button was clicked.

**Type**: `int`

```csharp
public int ActionButtonIndex { get; }
```

**Values**:
- `-1`: Close button was clicked
- `0`: First action button (rightmost) was clicked
- `1`: Second action button was clicked
- `2`: Third action button was clicked
- And so on...

---

### KryptonRibbon.NotificationBarButtonClick Event

Occurs when a notification bar action button or close button is clicked.

**Type**: `EventHandler<RibbonNotificationBarEventArgs>`  
**Category**: Action

```csharp
public event EventHandler<RibbonNotificationBarEventArgs>? NotificationBarButtonClick;
```

**Example**:
```csharp
ribbon.NotificationBarButtonClick += OnNotificationBarButtonClick;

private void OnNotificationBarButtonClick(object? sender, RibbonNotificationBarEventArgs e)
{
    if (e.ActionButtonIndex == -1)
    {
        // Close button was clicked
        MessageBox.Show("Notification dismissed by user");
    }
    else
    {
        // Action button was clicked
        string buttonText = ribbon.NotificationBar.ActionButtonTexts[e.ActionButtonIndex];
        MessageBox.Show($"Button '{buttonText}' was clicked");
    }
}
```

---

## Usage Examples

### Basic Information Notification

```csharp
// Simple informational message
ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
ribbon.NotificationBar.Text = "Your document has been saved successfully.";
ribbon.NotificationBar.ShowActionButtons = false; // No action buttons needed
ribbon.NotificationBar.Visible = true;
```

### Warning with Action Button

```csharp
// Warning with action button (like Office update notification)
ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
ribbon.NotificationBar.Title = "UPDATES AVAILABLE";
ribbon.NotificationBar.Text = "Updates for Office are ready to be applied, but are blocked by one or more apps.";
ribbon.NotificationBar.ActionButtonTexts = new[] { "Update now" };
ribbon.NotificationBar.Visible = true;

// Handle the action button click
ribbon.NotificationBarButtonClick += (sender, e) =>
{
    if (e.ActionButtonIndex == 0) // "Update now" button
    {
        // Perform update action
        PerformUpdate();
        ribbon.NotificationBar.Visible = false;
    }
};
```

### Error Notification with Multiple Actions

```csharp
// Error with multiple action options
ribbon.NotificationBar.Type = RibbonNotificationBarType.Error;
ribbon.NotificationBar.Text = "Failed to save document. What would you like to do?";
ribbon.NotificationBar.ActionButtonTexts = new[] { "Retry", "Save As", "Discard" };
ribbon.NotificationBar.Visible = true;

ribbon.NotificationBarButtonClick += (sender, e) =>
{
    switch (e.ActionButtonIndex)
    {
        case 0: // Retry
            RetrySave();
            break;
        case 1: // Save As
            ShowSaveAsDialog();
            break;
        case 2: // Discard
            DiscardChanges();
            break;
    }
    ribbon.NotificationBar.Visible = false;
};
```

### Success Notification with Auto-Dismiss

```csharp
// Success message that auto-dismisses after 5 seconds
ribbon.NotificationBar.Type = RibbonNotificationBarType.Success;
ribbon.NotificationBar.Text = "Changes have been saved successfully.";
ribbon.NotificationBar.ShowActionButtons = false;
ribbon.NotificationBar.AutoDismissSeconds = 5;
ribbon.NotificationBar.Visible = true;
```

### Custom Styled Notification

```csharp
// Custom branded notification
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBackColor = Color.FromArgb(240, 248, 255); // Alice Blue
ribbon.NotificationBar.CustomForeColor = Color.FromArgb(25, 25, 112);   // Midnight Blue
ribbon.NotificationBar.CustomBorderColor = Color.FromArgb(70, 130, 180); // Steel Blue
ribbon.NotificationBar.Text = "Welcome to our application!";
ribbon.NotificationBar.Icon = Properties.Resources.CompanyLogo;
ribbon.NotificationBar.Visible = true;
```

### Notification with Icon

```csharp
// Notification with custom icon
ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
ribbon.NotificationBar.Text = "New features are available in this update.";
ribbon.NotificationBar.Icon = SystemIcons.Information.ToBitmap();
ribbon.NotificationBar.ShowIcon = true;
ribbon.NotificationBar.ActionButtonTexts = new[] { "Learn more", "Dismiss" };
ribbon.NotificationBar.Visible = true;
```

### Dynamic Notification Updates

```csharp
// Update notification content dynamically
ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
ribbon.NotificationBar.Text = "Processing...";
ribbon.NotificationBar.Visible = true;

// Simulate progress updates
Task.Run(async () =>
{
    for (int i = 1; i <= 10; i++)
    {
        await Task.Delay(500);
        
        // Update on UI thread
        ribbon.Invoke((MethodInvoker)delegate
        {
            ribbon.NotificationBar.Text = $"Processing... {i * 10}%";
        });
    }
    
    // Complete
    ribbon.Invoke((MethodInvoker)delegate
    {
        ribbon.NotificationBar.Type = RibbonNotificationBarType.Success;
        ribbon.NotificationBar.Text = "Processing complete!";
        ribbon.NotificationBar.AutoDismissSeconds = 3;
    });
});
```

---

## Customization Guide

### Color Customization

#### Using Predefined Types

The easiest way to customize colors is to use the predefined types:

```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning; // Uses warning color scheme
```

#### Using Custom Type

For complete control over colors:

```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBackColor = Color.LightGray;
ribbon.NotificationBar.CustomForeColor = Color.DarkBlue;
ribbon.NotificationBar.CustomBorderColor = Color.Gray;
```

**Color Selection Tips**:
- Ensure sufficient contrast between background and text colors for readability
- Use subtle background colors to avoid overwhelming the UI
- Border colors should complement the background while providing clear definition
- Consider accessibility: test with high contrast themes

### Layout Customization

#### Adjusting Padding

```csharp
// More spacious layout
ribbon.NotificationBar.Padding = new Padding(20, 12, 20, 12);

// Compact layout
ribbon.NotificationBar.Padding = new Padding(8, 4, 8, 4);
```

#### Fixed Height

```csharp
// Set fixed height for consistent layout
ribbon.NotificationBar.Height = 60; // 60 pixels
```

#### Auto-Height (Recommended)

```csharp
// Let the system calculate height based on content
ribbon.NotificationBar.Height = 0;
```

### Content Customization

#### Text Formatting

The notification bar supports plain text. For rich formatting, consider:

```csharp
// Use title for emphasis
ribbon.NotificationBar.Title = "IMPORTANT";
ribbon.NotificationBar.Text = "Please review the following information carefully.";
```

#### Icon Selection

```csharp
// System icons
ribbon.NotificationBar.Icon = SystemIcons.Information.ToBitmap();
ribbon.NotificationBar.Icon = SystemIcons.Warning.ToBitmap();
ribbon.NotificationBar.Icon = SystemIcons.Error.ToBitmap();

// Custom icons from resources
ribbon.NotificationBar.Icon = Properties.Resources.CustomIcon;

// Load from file
ribbon.NotificationBar.Icon = Image.FromFile("icon.png");
```

**Icon Best Practices**:
- Use 24x24 pixel icons at 96 DPI
- Icons are automatically scaled for high DPI displays
- Use transparent backgrounds (PNG format recommended)
- Match icon style to notification type

### Button Customization

#### Single Action Button

```csharp
ribbon.NotificationBar.ActionButtonTexts = new[] { "OK" };
```

#### Multiple Action Buttons

```csharp
ribbon.NotificationBar.ActionButtonTexts = new[] { "Yes", "No", "Cancel" };
```

**Button Order**: Buttons are displayed from right to left. The first element in the array appears as the rightmost button.

#### Buttons with Images

```csharp
ribbon.NotificationBar.ActionButtonTexts = new[] { "Save", "Cancel" };
ribbon.NotificationBar.ActionButtonImages = new Image[]
{
    Properties.Resources.SaveIcon,
    Properties.Resources.CancelIcon
};
```

---

## Event Handling

### Basic Event Handler

```csharp
ribbon.NotificationBarButtonClick += OnNotificationBarButtonClick;

private void OnNotificationBarButtonClick(object? sender, RibbonNotificationBarEventArgs e)
{
    if (e.ActionButtonIndex == -1)
    {
        // Close button clicked
        HandleClose();
    }
    else
    {
        // Action button clicked
        HandleAction(e.ActionButtonIndex);
    }
}
```

### Lambda Expression Handler

```csharp
ribbon.NotificationBarButtonClick += (sender, e) =>
{
    if (e.ActionButtonIndex == -1)
    {
        ribbon.NotificationBar.Visible = false;
    }
    else
    {
        ProcessAction(e.ActionButtonIndex);
    }
};
```

### Handler with Button Text Lookup

```csharp
ribbon.NotificationBarButtonClick += (sender, e) =>
{
    if (e.ActionButtonIndex >= 0 && 
        e.ActionButtonIndex < ribbon.NotificationBar.ActionButtonTexts.Length)
    {
        string buttonText = ribbon.NotificationBar.ActionButtonTexts[e.ActionButtonIndex];
        MessageBox.Show($"You clicked: {buttonText}");
    }
};
```

### Async Event Handling

```csharp
ribbon.NotificationBarButtonClick += async (sender, e) =>
{
    if (e.ActionButtonIndex == 0)
    {
        ribbon.NotificationBar.Text = "Processing...";
        ribbon.NotificationBar.ShowActionButtons = false;
        
        await PerformLongRunningOperation();
        
        ribbon.NotificationBar.Type = RibbonNotificationBarType.Success;
        ribbon.NotificationBar.Text = "Operation completed successfully!";
        ribbon.NotificationBar.AutoDismissSeconds = 3;
    }
};
```

---

## Best Practices

### 1. Notification Type Selection

- **Information**: Use for general information, tips, or non-critical updates
- **Warning**: Use for important notices that require user attention but aren't critical
- **Error**: Use for errors, failures, or critical issues that need immediate attention
- **Success**: Use for confirmations, successful operations, or positive feedback
- **Custom**: Use when you need brand-specific colors or special styling

### 2. Message Content

- **Keep it concise**: Notification bars work best with short, clear messages
- **Use titles sparingly**: Titles should be used for emphasis (e.g., "UPDATES AVAILABLE")
- **Be actionable**: If you're showing a notification, provide clear actions when possible
- **Avoid redundancy**: Don't repeat information already visible elsewhere

### 3. Auto-Dismiss Timing

- **Never dismiss errors**: Errors should require user acknowledgment
- **Short timers for success**: 3-5 seconds is usually sufficient for success messages
- **Medium timers for information**: 5-10 seconds for informational messages
- **Long timers for warnings**: 15-30 seconds for warnings, or don't auto-dismiss

### 4. Button Design

- **Limit button count**: 1-3 buttons is optimal; more than 4 becomes cluttered
- **Clear button labels**: Use action verbs (e.g., "Update now", "Retry", "Learn more")
- **Primary action first**: Place the most important action as the rightmost button
- **Provide escape**: Always allow users to dismiss (close button or "Dismiss" action)

### 5. Visibility Management

```csharp
// Always hide previous notification before showing new one
ribbon.NotificationBar.Visible = false;
ribbon.NotificationBar.Text = "New message";
ribbon.NotificationBar.Visible = true;
```

### 6. Thread Safety

```csharp
// Always update notification bar on UI thread
if (ribbon.InvokeRequired)
{
    ribbon.Invoke((MethodInvoker)delegate
    {
        ribbon.NotificationBar.Text = "Updated from background thread";
        ribbon.NotificationBar.Visible = true;
    });
}
else
{
    ribbon.NotificationBar.Text = "Updated from UI thread";
    ribbon.NotificationBar.Visible = true;
}
```

### 7. Resource Management

```csharp
// Dispose custom icons when done
if (ribbon.NotificationBar.Icon != null)
{
    ribbon.NotificationBar.Icon.Dispose();
    ribbon.NotificationBar.Icon = null;
}
```

---

## Advanced Scenarios

### Notification Queue System

```csharp
private Queue<NotificationInfo> _notificationQueue = new Queue<NotificationInfo>();

public void ShowNotification(NotificationInfo info)
{
    if (ribbon.NotificationBar.Visible)
    {
        // Queue notification if one is already showing
        _notificationQueue.Enqueue(info);
    }
    else
    {
        // Show immediately
        DisplayNotification(info);
    }
}

private void DisplayNotification(NotificationInfo info)
{
    ribbon.NotificationBar.Type = info.Type;
    ribbon.NotificationBar.Text = info.Text;
    ribbon.NotificationBar.ActionButtonTexts = info.ButtonTexts;
    ribbon.NotificationBar.Visible = true;
}

private void OnNotificationBarButtonClick(object? sender, RibbonNotificationBarEventArgs e)
{
    ribbon.NotificationBar.Visible = false;
    
    // Show next queued notification
    if (_notificationQueue.Count > 0)
    {
        var next = _notificationQueue.Dequeue();
        DisplayNotification(next);
    }
}
```

### Progress Notification

```csharp
public void ShowProgressNotification(string message, int progressPercent)
{
    ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
    ribbon.NotificationBar.Text = $"{message} ({progressPercent}%)";
    ribbon.NotificationBar.ShowActionButtons = false;
    ribbon.NotificationBar.AutoDismissSeconds = 0; // Don't auto-dismiss progress
    ribbon.NotificationBar.Visible = true;
}

// Update progress
ShowProgressNotification("Uploading file", 45);
```

### Conditional Notifications

```csharp
public void ShowUpdateNotification()
{
    if (HasPendingUpdates())
    {
        ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
        ribbon.NotificationBar.Title = "UPDATES AVAILABLE";
        ribbon.NotificationBar.Text = $"Version {GetLatestVersion()} is available.";
        ribbon.NotificationBar.ActionButtonTexts = new[] { "Update now", "Later" };
        ribbon.NotificationBar.Visible = true;
    }
}
```

### Localized Notifications

```csharp
// Use resource files for localization
ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
ribbon.NotificationBar.Text = Resources.NotificationBar_SaveSuccess;
ribbon.NotificationBar.ActionButtonTexts = new[]
{
    Resources.NotificationBar_ButtonOK,
    Resources.NotificationBar_ButtonCancel
};
ribbon.NotificationBar.Visible = true;
```

### Notification with Validation

```csharp
public bool ShowConfirmationNotification(string message, out bool confirmed)
{
    confirmed = false;
    bool userResponded = false;
    
    ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
    ribbon.NotificationBar.Text = message;
    ribbon.NotificationBar.ActionButtonTexts = new[] { "Yes", "No" };
    ribbon.NotificationBar.Visible = true;
    
    EventHandler<RibbonNotificationBarEventArgs> handler = null;
    handler = (sender, e) =>
    {
        if (e.ActionButtonIndex == 0) // Yes
        {
            confirmed = true;
        }
        userResponded = true;
        ribbon.NotificationBarButtonClick -= handler;
    };
    
    ribbon.NotificationBarButtonClick += handler;
    
    // Wait for user response (in real scenario, use async/await)
    return userResponded;
}
```

---

## Troubleshooting

### Notification Bar Not Visible

**Problem**: Notification bar doesn't appear even when `Visible = true`.

**Solutions**:
1. Check that the ribbon control itself is visible
2. Verify the ribbon is not minimized
3. Ensure the notification bar is not hidden by other UI elements
4. Check that `Height` is not set to 0 with no content

```csharp
// Debug visibility
Debug.WriteLine($"Ribbon Visible: {ribbon.Visible}");
Debug.WriteLine($"Notification Bar Visible: {ribbon.NotificationBar.Visible}");
Debug.WriteLine($"Notification Bar Text: {ribbon.NotificationBar.Text}");
```

### Buttons Not Clickable

**Problem**: Action buttons don't respond to clicks.

**Solutions**:
1. Verify `ShowActionButtons` is `true`
2. Check that `ActionButtonTexts` array is not empty
3. Ensure event handler is properly attached
4. Check for overlapping UI elements

```csharp
// Verify button configuration
Debug.WriteLine($"ShowActionButtons: {ribbon.NotificationBar.ShowActionButtons}");
Debug.WriteLine($"Button Count: {ribbon.NotificationBar.ActionButtonTexts?.Length ?? 0}");
```

### Auto-Dismiss Not Working

**Problem**: Notification bar doesn't auto-dismiss after specified time.

**Solutions**:
1. Verify `AutoDismissSeconds` is greater than 0
2. Check that timer isn't being stopped by button clicks
3. Ensure notification bar remains visible (not hidden by other code)

```csharp
// Verify auto-dismiss settings
Debug.WriteLine($"AutoDismissSeconds: {ribbon.NotificationBar.AutoDismissSeconds}");
```

### Colors Not Applied

**Problem**: Custom colors don't appear when using `Custom` type.

**Solutions**:
1. Ensure `Type` is set to `RibbonNotificationBarType.Custom`
2. Verify colors are set after type is set
3. Check that colors are not transparent

```csharp
// Correct order
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBackColor = Color.LightBlue;
ribbon.NotificationBar.CustomForeColor = Color.DarkBlue;
ribbon.NotificationBar.CustomBorderColor = Color.Blue;
```

### Performance Issues

**Problem**: UI becomes sluggish when showing notifications.

**Solutions**:
1. Avoid frequent updates to notification bar
2. Use `AutoDismissSeconds` to automatically hide notifications
3. Don't update notification bar in tight loops
4. Consider debouncing rapid updates

```csharp
// Bad: Rapid updates
for (int i = 0; i < 1000; i++)
{
    ribbon.NotificationBar.Text = $"Update {i}";
}

// Good: Throttled updates
private DateTime _lastUpdate = DateTime.MinValue;
public void UpdateNotification(string text)
{
    if ((DateTime.Now - _lastUpdate).TotalMilliseconds > 100)
    {
        ribbon.NotificationBar.Text = text;
        _lastUpdate = DateTime.Now;
    }
}
```

---

## Integration with Other Krypton Features

### Working with Krypton Themes

The notification bar automatically adapts to Krypton themes. Colors are defined per notification type and work with all theme variants.

```csharp
// Notification bar works with all themes
kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
// Notification bar will use appropriate colors for the theme
```

### Integration with KryptonForm

The notification bar integrates seamlessly with `KryptonForm` and respects the form's theme settings.

### Accessibility

The notification bar supports standard Windows accessibility features:
- Keyboard navigation (when implemented)
- High contrast themes
- Screen reader compatibility (text content is accessible)

---

## Version History

- **Version 1.0** (Initial Release)
  - Basic notification bar functionality
  - Support for Information, Warning, Error, Success, and Custom types
  - Action buttons and close button
  - Auto-dismiss timer
  - Full customization support

---

## Additional Resources

- [Krypton Toolkit Documentation](https://github.com/Krypton-Suite/Standard-Toolkit)
- [Krypton Ribbon Documentation](../Documents/)
- [Krypton Community Forums](https://github.com/Krypton-Suite/Standard-Toolkit/discussions)

---

## Support

For issues, questions, or feature requests related to the Notification Bar feature, please:

1. Check this documentation first
2. Search existing issues on GitHub
3. Create a new issue with detailed information including:
   - Krypton version
   - .NET version
   - Operating system
   - Steps to reproduce
   - Expected vs. actual behavior

---

*Last Updated: 2026*

