# KryptonScrollbarManager Design Document

## Overview

The `KryptonScrollbarManager` is a helper class that manages Krypton-themed scrollbars for any scrollable control. It provides a unified, robust solution for replacing native Windows scrollbars with Krypton scrollbars across all Krypton controls.

## Architecture

### Core Components

1. **KryptonScrollbarManager** - Main manager class
2. **IScrollableControl** - Interface for controls that can use the manager
3. **ScrollbarManagerMode** - Enum for different integration modes

### Integration Modes

1. **Container Mode** - For controls like `KryptonPanel`, `KryptonGroupBox`
   - Disables `AutoScroll`
   - Adds `KryptonHScrollBar` and `KryptonVScrollBar` as child controls
   - Manages content scrolling manually

2. **Native Wrapper Mode** - For controls like `KryptonTextBox`, `KryptonRichTextBox`
   - Hides native scrollbars using Windows API
   - Adds Krypton scrollbars as overlays
   - Synchronizes scroll positions bidirectionally

3. **Custom Mode** - For controls with custom scrolling logic
   - Provides scrollbar controls
   - Control manages its own scrolling

## Key Features

### Robustness Features

1. **Automatic Detection**
   - Detects when scrollbars are needed
   - Handles dynamic content changes
   - Manages visibility automatically

2. **Synchronization**
   - Bidirectional sync between scrollbars and content
   - Handles scroll events from both sources
   - Prevents infinite loops

3. **Performance**
   - Lazy initialization
   - Efficient event handling
   - Minimal overhead when not scrolling

4. **DPI Awareness**
   - Respects DPI scaling
   - Proper sizing at all DPI levels

5. **State Management**
   - Handles control lifecycle (Create, Dispose, Resize)
   - Manages scrollbar visibility
   - Preserves scroll position when possible

## API Design

```csharp
public class KryptonScrollbarManager : IDisposable
{
    // Properties
    public bool Enabled { get; set; }
    public KryptonHScrollBar? HorizontalScrollBar { get; }
    public KryptonVScrollBar? VerticalScrollBar { get; }
    public ScrollbarManagerMode Mode { get; set; }
    
    // Methods
    public void Attach(Control targetControl);
    public void Detach();
    public void UpdateScrollbars();
    public void SyncFromNative();
    public void SyncToNative();
    
    // Events
    public event EventHandler? ScrollbarsChanged;
}

public enum ScrollbarManagerMode
{
    Container,      // For Panel, GroupBox, etc.
    NativeWrapper, // For TextBox, RichTextBox, etc.
    Custom         // For custom implementations
}
```

## Implementation Strategy

### Phase 1: Core Manager
- Create `KryptonScrollbarManager` class
- Implement container mode
- Basic synchronization

### Phase 2: Native Wrapper Support
- Add Windows API integration
- Implement native scrollbar hiding
- Bidirectional synchronization

### Phase 3: Integration
- Integrate with `KryptonPanel`
- Integrate with `KryptonRichTextBox`
- Add to other controls incrementally

### Phase 4: Polish
- Performance optimization
- Edge case handling
- Comprehensive testing

## Benefits

1. **Maintainability**: Single codebase for all scrollbar logic
2. **Consistency**: Same behavior across all controls
3. **Flexibility**: Works with different control types
4. **Testability**: Can be tested independently
5. **Extensibility**: Easy to add new features or modes

## Usage Example

```csharp
public class KryptonScrollablePanel : KryptonPanel
{
    private KryptonScrollbarManager? _scrollbarManager;
    
    public KryptonScrollablePanel()
    {
        _scrollbarManager = new KryptonScrollbarManager
        {
            Mode = ScrollbarManagerMode.Container,
            Enabled = true
        };
        _scrollbarManager.Attach(this);
    }
    
    protected override void Dispose(bool disposing)
    {
        _scrollbarManager?.Dispose();
        base.Dispose(disposing);
    }
}
```
