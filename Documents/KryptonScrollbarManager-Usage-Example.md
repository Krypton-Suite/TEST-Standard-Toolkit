# KryptonScrollbarManager Usage Examples

## Basic Usage with KryptonPanel

The `KryptonScrollbarManager` provides a simple way to add Krypton-themed scrollbars to any scrollable control.

### Example 1: Container Mode (KryptonPanel)

```csharp
public partial class ScrollablePanelForm : KryptonForm
{
    private KryptonScrollbarManager? _scrollbarManager;
    private KryptonPanel _contentPanel;

    public ScrollablePanelForm()
    {
        InitializeComponent();

        // Create a panel with content that exceeds its bounds
        _contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = false // Disable native AutoScroll
        };

        // Add some controls that extend beyond the panel size
        for (int i = 0; i < 20; i++)
        {
            var label = new KryptonLabel
            {
                Text = $"Label {i + 1}",
                Location = new Point(10, i * 30 + 10),
                Size = new Size(200, 25)
            };
            _contentPanel.Controls.Add(label);
        }

        Controls.Add(_contentPanel);

        // Create and attach the scrollbar manager
        _scrollbarManager = new KryptonScrollbarManager(_contentPanel, ScrollbarManagerMode.Container)
        {
            Enabled = true
        };
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scrollbarManager?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

### Example 2: Programmatic Control

```csharp
public class MyScrollableControl : KryptonPanel
{
    private KryptonScrollbarManager? _scrollbarManager;

    public MyScrollableControl()
    {
        // Initialize scrollbar manager
        _scrollbarManager = new KryptonScrollbarManager(this, ScrollbarManagerMode.Container)
        {
            Enabled = true
        };

        _scrollbarManager.ScrollbarsChanged += OnScrollbarsChanged;
    }

    private void OnScrollbarsChanged(object? sender, EventArgs e)
    {
        // React to scrollbar visibility changes
        Invalidate();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scrollbarManager?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

### Example 3: Dynamic Content

```csharp
public class DynamicScrollablePanel : KryptonPanel
{
    private KryptonScrollbarManager? _scrollbarManager;

    public DynamicScrollablePanel()
    {
        AutoScroll = false;
        _scrollbarManager = new KryptonScrollbarManager(this, ScrollbarManagerMode.Container);
    }

    public void AddControl(Control control)
    {
        Controls.Add(control);
        // Scrollbar manager will automatically update when layout changes
        _scrollbarManager?.UpdateScrollbars();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scrollbarManager?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

## Integration with Existing Controls

### Adding to KryptonPanel

The easiest way to add Krypton scrollbars to a `KryptonPanel` is to create a derived class:

```csharp
public class KryptonScrollablePanel : KryptonPanel
{
    private KryptonScrollbarManager? _scrollbarManager;

    public KryptonScrollablePanel()
    {
        AutoScroll = false; // Disable native scrolling
        _scrollbarManager = new KryptonScrollbarManager(this, ScrollbarManagerMode.Container)
        {
            Enabled = true
        };
    }

    [Category(@"Behavior")]
    [Description(@"Gets or sets whether Krypton scrollbars are enabled.")]
    [DefaultValue(true)]
    public bool KryptonScrollbarsEnabled
    {
        get => _scrollbarManager?.Enabled ?? false;
        set
        {
            if (_scrollbarManager != null)
            {
                _scrollbarManager.Enabled = value;
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scrollbarManager?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

## Best Practices

1. **Always Dispose**: Make sure to dispose the manager when the control is disposed
2. **Handle Created**: The manager automatically waits for the control handle to be created
3. **Layout Events**: The manager listens to layout events and updates automatically
4. **Performance**: The manager uses efficient update logic to minimize overhead

## Notes

- Container mode works best with `Panel`-derived controls
- Native wrapper mode (for TextBox, RichTextBox) will be available in a future update
- The manager automatically handles scrollbar visibility based on content size
- Scrollbars are positioned correctly, accounting for each other's presence
