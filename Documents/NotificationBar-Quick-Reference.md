# Krypton Ribbon Notification Bar - Quick Reference

## Quick Start

```csharp
// Show a simple notification
ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
ribbon.NotificationBar.Text = "Your message here";
ribbon.NotificationBar.Visible = true;
```

## Property Reference

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Visible` | `bool` | `false` | Show/hide notification bar |
| `Type` | `RibbonNotificationBarType` | `Information` | Notification type (affects colors) |
| `Text` | `string` | `""` | Main message text |
| `Title` | `string` | `""` | Title text (displayed before main text) |
| `Icon` | `Image?` | `null` | Icon image |
| `ShowIcon` | `bool` | `true` | Show/hide icon |
| `ShowCloseButton` | `bool` | `true` | Show/hide close button |
| `ShowActionButtons` | `bool` | `true` | Show/hide action buttons |
| `ActionButtonTexts` | `string[]` | `["Update now"]` | Array of button texts |
| `ActionButtonImages` | `Image[]?` | `null` | Optional button images |
| `CustomBackColor` | `Color` | Light Yellow | Custom background color |
| `CustomForeColor` | `Color` | Black | Custom text color |
| `CustomBorderColor` | `Color` | Orange | Custom border color |
| `AutoDismissSeconds` | `int` | `0` | Auto-dismiss timer (0 = never) |
| `Padding` | `Padding` | `(12, 8, 12, 8)` | Content padding |
| `Height` | `int` | `0` | Fixed height (0 = auto) |

## Notification Types

| Type | Background | Border | Use Case |
|------|-----------|--------|----------|
| `Information` | Light Blue | Blue | General info, tips |
| `Warning` | Light Yellow | Orange | Warnings, cautions |
| `Error` | Light Red | Red | Errors, critical issues |
| `Success` | Light Green | Green | Success messages |
| `Custom` | User-defined | User-defined | Brand-specific |

## Common Patterns

### Information Message
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
ribbon.NotificationBar.Text = "Document saved successfully.";
ribbon.NotificationBar.ShowActionButtons = false;
ribbon.NotificationBar.Visible = true;
```

### Warning with Action
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
ribbon.NotificationBar.Title = "UPDATES AVAILABLE";
ribbon.NotificationBar.Text = "Updates are ready to be applied.";
ribbon.NotificationBar.ActionButtonTexts = new[] { "Update now" };
ribbon.NotificationBar.Visible = true;
```

### Error with Multiple Actions
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Error;
ribbon.NotificationBar.Text = "Save failed. What would you like to do?";
ribbon.NotificationBar.ActionButtonTexts = new[] { "Retry", "Save As", "Cancel" };
ribbon.NotificationBar.Visible = true;
```

### Auto-Dismiss Success
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Success;
ribbon.NotificationBar.Text = "Operation completed!";
ribbon.NotificationBar.ShowActionButtons = false;
ribbon.NotificationBar.AutoDismissSeconds = 5;
ribbon.NotificationBar.Visible = true;
```

### Custom Styled
```csharp
ribbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
ribbon.NotificationBar.CustomBackColor = Color.LightGray;
ribbon.NotificationBar.CustomForeColor = Color.DarkBlue;
ribbon.NotificationBar.CustomBorderColor = Color.Gray;
ribbon.NotificationBar.Text = "Custom notification";
ribbon.NotificationBar.Visible = true;
```

## Event Handling

```csharp
ribbon.NotificationBarButtonClick += (sender, e) =>
{
    if (e.ActionButtonIndex == -1)
    {
        // Close button clicked
    }
    else
    {
        // Action button clicked (index: 0, 1, 2, ...)
        string buttonText = ribbon.NotificationBar.ActionButtonTexts[e.ActionButtonIndex];
    }
};
```

## Best Practices

✅ **Do:**
- Keep messages concise
- Use appropriate notification types
- Provide clear action buttons
- Use auto-dismiss for non-critical messages
- Hide previous notification before showing new one

❌ **Don't:**
- Use more than 3-4 action buttons
- Auto-dismiss error notifications
- Update notification bar in tight loops
- Forget to handle button click events
- Use custom colors without sufficient contrast

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Not visible | Check `Visible = true`, ribbon visibility, and content |
| Buttons not working | Verify `ShowActionButtons = true` and event handler attached |
| Auto-dismiss not working | Ensure `AutoDismissSeconds > 0` |
| Colors not applied | Set `Type = Custom` before setting custom colors |
| Performance issues | Avoid rapid updates, use throttling |

## Thread Safety

```csharp
// Always update on UI thread
if (ribbon.InvokeRequired)
{
    ribbon.Invoke((MethodInvoker)delegate
    {
        ribbon.NotificationBar.Text = "Updated";
        ribbon.NotificationBar.Visible = true;
    });
}
```

## See Also

- [Full Developer Guide](./NotificationBar-Developer-Guide.md) - Comprehensive documentation
- [Krypton Ribbon Documentation](../Documents/) - General ribbon documentation

