# Taskbar Progress Feature

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

The Taskbar Progress feature allows you to display a progress indicator directly on the Windows taskbar button for your application. This provides visual feedback for long-running operations without requiring the user to switch to your application window.

### Key Features

- **Windows 7+ Support**: Uses the native Windows ITaskbarList3 API
- **Multiple Progress States**: Normal, Indeterminate, Error, Paused, and No Progress
- **Developer Controlled**: Fully programmable via properties
- **Designer Support**: Full Visual Studio designer integration with expandable properties
- **Automatic Updates**: Progress indicator updates automatically when properties change
- **Value Range Control**: Configurable minimum, maximum, and current value
- **Error Handling**: Gracefully handles unsupported platforms and errors

### Use Cases

- **File Operations**: Show progress for file downloads, uploads, or transfers
- **Installation Progress**: Display installation or update progress
- **Background Processing**: Indicate progress of background tasks
- **Data Synchronization**: Show sync progress for data operations
- **Error States**: Display error or paused states for failed operations
- **Indeterminate Operations**: Show indeterminate progress for operations without known duration

### Requirements

- **Windows Version**: Windows 7 or later (ITaskbarList3 API requirement)
- **Form Handle**: Form must have a valid window handle
- **Taskbar Visibility**: Form must be visible and shown in taskbar (`ShowInTaskbar = true`)

---

## Quick Start

### Basic Usage

```csharp
// Create a form with taskbar progress
var form = new KryptonForm();
form.Text = "My Application";

// Set normal progress (50%)
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Maximum = 100;
form.TaskbarProgress.Value = 50;
```

### Designer Usage

1. Select a `KryptonForm` in the designer
2. In the Properties window, find the `TaskbarProgress` property
3. Expand the `TaskbarProgress` property (it appears as an expandable object)
4. Set the `State`, `Value`, and `Maximum` properties as needed

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Classes

### TaskbarProgressState Enumeration

Enumeration defining the different progress states available for the taskbar progress indicator. This enum is defined in `General/Definitions.cs`.

```csharp
public enum TaskbarProgressState
{
    NoProgress = 0,        // No progress indicator is displayed
    Indeterminate = 1,     // An indeterminate progress indicator is displayed
    Normal = 2,            // A normal progress indicator is displayed
    Error = 4,             // An error progress indicator is displayed
    Paused = 8             // A paused progress indicator is displayed
}
```

**Location**: `Krypton.Toolkit.General/Definitions.cs`

#### Enum Values

| Value | Description |
|-------|-------------|
| `NoProgress` | No progress indicator is displayed. This clears any existing progress indicator. |
| `Indeterminate` | An indeterminate (marquee-style) progress indicator is displayed. The progress bar animates continuously without showing a specific value. Use this for operations with unknown duration. |
| `Normal` | A normal progress indicator is displayed showing the current progress value. The progress bar fills from left to right based on the `Value` and `Maximum` properties. |
| `Error` | An error progress indicator is displayed. The progress bar appears in red, indicating an error state. The current `Value` is still displayed. |
| `Paused` | A paused progress indicator is displayed. The progress bar appears in yellow, indicating a paused state. The current `Value` is still displayed. |

---

### TaskbarProgressValues

Storage class for taskbar progress value information. This class uses `ExpandableObjectConverter` for designer support.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `State` | `TaskbarProgressState` | `NoProgress` | The progress state to display on the taskbar button. Determines the visual appearance and behavior of the progress indicator. |
| `Value` | `ulong` | `0` | Current progress value. Must be between 0 and `Maximum`. Automatically clamped to valid range when set. |
| `Maximum` | `ulong` | `100` | Maximum progress value. Defines the upper bound for the `Value` property. When changed, the current `Value` is automatically clamped if it exceeds the new maximum. |

#### Methods

##### `CopyFrom(TaskbarProgressValues source)`

Copies all taskbar progress values from another instance.

```csharp
var source = new TaskbarProgressValues(needPaint);
source.State = TaskbarProgressState.Normal;
source.Maximum = 100;
source.Value = 50;

var target = new TaskbarProgressValues(needPaint);
target.CopyFrom(source); // Copies all properties
```

##### `Reset()`

Resets all values to their default state.

```csharp
taskbarProgress.Reset(); // Sets State=NoProgress, Value=0, Maximum=100
```

#### Serialization Methods

- `ShouldSerializeState()` - Returns `true` if State is not NoProgress
- `ShouldSerializeValue()` - Returns `true` if Value is not 0
- `ShouldSerializeMaximum()` - Returns `true` if Maximum is not 100

#### Reset Methods

- `ResetState()` - Sets State to NoProgress
- `ResetValue()` - Sets Value to 0
- `ResetMaximum()` - Sets Maximum to 100

#### Property Behavior

**Value Clamping**: When setting `Value`, it is automatically clamped to the valid range [0, Maximum]. If you set a value greater than Maximum, it will be set to Maximum.

**Maximum Changes**: When `Maximum` is changed, if the current `Value` exceeds the new maximum, `Value` is automatically clamped to the new maximum.

**State-Dependent Behavior**:
- `NoProgress`: Ignores `Value` and `Maximum` - clears the progress indicator
- `Indeterminate`: Ignores `Value` and `Maximum` - shows animated progress bar
- `Normal`, `Error`, `Paused`: Uses `Value` and `Maximum` to calculate progress percentage

---

### KryptonForm Properties

#### `TaskbarProgress`

Gets access to the taskbar progress values.

```csharp
[Category(@"Visuals")]
[Description(@"Taskbar progress indicator to display on the taskbar button.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public TaskbarProgressValues TaskbarProgress { get; }
```

**Usage**:
```csharp
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Maximum = 100;
form.TaskbarProgress.Value = 75;
```

#### `ResetTaskbarProgress()`

Resets the TaskbarProgress property to its default value.

```csharp
public void ResetTaskbarProgress() => TaskbarProgress.Reset();
```

#### `ShouldSerializeTaskbarProgress()`

Indicates whether the TaskbarProgress property should be serialized.

```csharp
public bool ShouldSerializeTaskbarProgress() => !TaskbarProgress.IsDefault;
```

---

### KryptonProgressBar Integration

The `KryptonProgressBar` control can optionally synchronize its progress with the parent form's taskbar progress indicator.

#### `SyncWithTaskbar` Property

Gets or sets whether the progress bar should automatically sync with the parent form's taskbar progress indicator.

```csharp
[Category(@"Behavior")]
[Description(@"Whether to automatically sync progress bar value with the parent form's taskbar progress indicator.")]
[DefaultValue(false)]
public bool SyncWithTaskbar { get; set; }
```

**Usage**:
```csharp
var progressBar = new KryptonProgressBar();
progressBar.SyncWithTaskbar = true; // Enable automatic synchronization
progressBar.Maximum = 100;
progressBar.Value = 50; // Automatically updates taskbar progress!
```

**Behavior**:
- When enabled, changes to `Value`, `Maximum`, `Minimum`, or `Style` automatically update the parent form's taskbar progress
- `ProgressBarStyle.Marquee` maps to `TaskbarProgressState.Indeterminate`
- `ProgressBarStyle.Continuous` and `ProgressBarStyle.Blocks` map to `TaskbarProgressState.Normal`
- Automatically handles minimum offset (taskbar progress doesn't support minimum, so it uses `Value - Minimum` and `Maximum - Minimum`)
- Clears taskbar progress when sync is disabled or control is disposed
- Updates taskbar progress when parent form changes

**Notes**:
- Only works when parent form is a `VisualForm` (e.g., `KryptonForm`)
- Only syncs at runtime, not in designer
- If multiple progress bars have `SyncWithTaskbar = true`, the last one to update will control the taskbar progress

---

## Usage Examples

### Example 1: Basic Progress Indicator

```csharp
var form = new KryptonForm();
form.Text = "File Download";

// Initialize progress
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Maximum = 100;
form.TaskbarProgress.Value = 0;

// Update progress during operation
for (int i = 0; i <= 100; i++)
{
    form.TaskbarProgress.Value = (ulong)i;
    System.Threading.Thread.Sleep(50); // Simulate work
}

// Clear progress when done
form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
```

### Example 2: File Download Progress

```csharp
public class FileDownloader
{
    private readonly KryptonForm _form;

    public FileDownloader(KryptonForm form)
    {
        _form = form;
        InitializeProgress();
    }

    private void InitializeProgress()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Normal;
        _form.TaskbarProgress.Maximum = 100;
        _form.TaskbarProgress.Value = 0;
    }

    public async Task DownloadFileAsync(string url, string destination)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var totalBytes = response.Content.Headers.ContentLength ?? 0;
            
            _form.TaskbarProgress.Maximum = (ulong)totalBytes;
            _form.TaskbarProgress.Value = 0;

            using var fileStream = new FileStream(destination, FileMode.Create);
            using var stream = await response.Content.ReadAsStreamAsync();
            
            var buffer = new byte[8192];
            long totalRead = 0;
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead);
                totalRead += bytesRead;
                _form.TaskbarProgress.Value = (ulong)totalRead;
            }

            // Success - clear progress
            _form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
        }
        catch (Exception ex)
        {
            // Show error state
            _form.TaskbarProgress.State = TaskbarProgressState.Error;
            MessageBox.Show($"Download failed: {ex.Message}");
        }
    }
}
```

### Example 3: Indeterminate Progress

```csharp
public class BackgroundProcessor
{
    private readonly KryptonForm _form;

    public BackgroundProcessor(KryptonForm form)
    {
        _form = form;
    }

    public async Task ProcessDataAsync()
    {
        // Show indeterminate progress for unknown duration operation
        _form.TaskbarProgress.State = TaskbarProgressState.Indeterminate;

        try
        {
            await PerformLongRunningOperation();
        }
        finally
        {
            // Clear progress when done
            _form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
        }
    }

    private async Task PerformLongRunningOperation()
    {
        // Simulate work with unknown duration
        await Task.Delay(5000);
    }
}
```

### Example 4: Error and Paused States

```csharp
public class OperationManager
{
    private readonly KryptonForm _form;
    private bool _isPaused;

    public OperationManager(KryptonForm form)
    {
        _form = form;
        InitializeProgress();
    }

    private void InitializeProgress()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Normal;
        _form.TaskbarProgress.Maximum = 100;
        _form.TaskbarProgress.Value = 0;
    }

    public void PauseOperation()
    {
        _isPaused = true;
        _form.TaskbarProgress.State = TaskbarProgressState.Paused;
    }

    public void ResumeOperation()
    {
        _isPaused = false;
        _form.TaskbarProgress.State = TaskbarProgressState.Normal;
    }

    public void HandleError()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Error;
        // Optionally keep the current progress value to show where it failed
    }

    public void ClearProgress()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
    }
}
```

### Example 5: Animated Progress with Timer

```csharp
public partial class ProgressForm : KryptonForm
{
    private readonly Timer _progressTimer;
    private int _currentProgress;

    public ProgressForm()
    {
        InitializeComponent();
        
        _progressTimer = new Timer { Interval = 100 };
        _progressTimer.Tick += ProgressTimer_Tick;
        
        InitializeProgress();
    }

    private void InitializeProgress()
    {
        TaskbarProgress.State = TaskbarProgressState.Normal;
        TaskbarProgress.Maximum = 100;
        TaskbarProgress.Value = 0;
        _currentProgress = 0;
    }

    public void StartProgress()
    {
        _currentProgress = 0;
        TaskbarProgress.Value = 0;
        _progressTimer.Start();
    }

    public void StopProgress()
    {
        _progressTimer.Stop();
        TaskbarProgress.State = TaskbarProgressState.NoProgress;
    }

    private void ProgressTimer_Tick(object? sender, EventArgs e)
    {
        _currentProgress++;
        if (_currentProgress > 100)
        {
            _currentProgress = 0;
        }
        TaskbarProgress.Value = (ulong)_currentProgress;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _progressTimer?.Stop();
            _progressTimer?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

### Example 6: Multi-Step Operation Progress

```csharp
public class MultiStepOperation
{
    private readonly KryptonForm _form;
    private readonly string[] _steps = { "Initializing", "Processing", "Validating", "Finalizing" };

    public MultiStepOperation(KryptonForm form)
    {
        _form = form;
    }

    public async Task ExecuteAsync()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Normal;
        _form.TaskbarProgress.Maximum = (ulong)_steps.Length;
        _form.TaskbarProgress.Value = 0;

        for (int i = 0; i < _steps.Length; i++)
        {
            await ExecuteStepAsync(_steps[i]);
            _form.TaskbarProgress.Value = (ulong)(i + 1);
        }

        _form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
    }

    private async Task ExecuteStepAsync(string stepName)
    {
        // Update UI with step name
        _form.Text = $"Processing: {stepName}";
        
        // Simulate step execution
        await Task.Delay(1000);
    }
}
```

### Example 7: Progress with Percentage Display

```csharp
public class ProgressReporter
{
    private readonly KryptonForm _form;

    public ProgressReporter(KryptonForm form)
    {
        _form = form;
    }

    public void ReportProgress(ulong current, ulong total)
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Normal;
        _form.TaskbarProgress.Maximum = total;
        _form.TaskbarProgress.Value = current;

        // Calculate percentage for UI display
        double percentage = total > 0 ? (double)current / total * 100 : 0;
        _form.Text = $"Progress: {percentage:F1}%";
    }

    public void ReportIndeterminate()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Indeterminate;
        _form.Text = "Processing...";
    }

    public void ReportComplete()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
        _form.Text = "Complete";
    }

    public void ReportError()
    {
        _form.TaskbarProgress.State = TaskbarProgressState.Error;
        _form.Text = "Error occurred";
    }
}
```

### Example 8: Automatic Sync with KryptonProgressBar

```csharp
public partial class DownloadForm : KryptonForm
{
    private readonly KryptonProgressBar _progressBar;

    public DownloadForm()
    {
        InitializeComponent();
        
        // Create progress bar with automatic taskbar sync
        _progressBar = new KryptonProgressBar
        {
            Dock = DockStyle.Top,
            Height = 30,
            Maximum = 100,
            Value = 0,
            SyncWithTaskbar = true // Enable automatic synchronization
        };
        
        Controls.Add(_progressBar);
    }

    public async Task DownloadFileAsync(string url)
    {
        // Progress bar automatically syncs with taskbar
        _progressBar.Value = 0;
        
        for (int i = 0; i <= 100; i++)
        {
            _progressBar.Value = i; // Both UI and taskbar update automatically!
            await Task.Delay(50);
        }
        
        // When progress bar is disposed or sync disabled, taskbar progress is cleared
    }
}
```

### Example 9: Multiple Progress Bars with Sync

```csharp
public partial class MultiTaskForm : KryptonForm
{
    private readonly KryptonProgressBar _downloadProgress;
    private readonly KryptonProgressBar _processProgress;

    public MultiTaskForm()
    {
        InitializeComponent();
        
        // Primary progress bar (syncs with taskbar)
        _downloadProgress = new KryptonProgressBar
        {
            Dock = DockStyle.Top,
            Height = 30,
            Maximum = 100,
            SyncWithTaskbar = true // This one controls taskbar
        };
        
        // Secondary progress bar (doesn't sync)
        _processProgress = new KryptonProgressBar
        {
            Dock = DockStyle.Top,
            Height = 30,
            Maximum = 100,
            SyncWithTaskbar = false // UI only, doesn't affect taskbar
        };
        
        Controls.Add(_processProgress);
        Controls.Add(_downloadProgress);
    }

    public void UpdateDownloadProgress(int value)
    {
        // Updates both UI and taskbar
        _downloadProgress.Value = value;
    }

    public void UpdateProcessProgress(int value)
    {
        // Updates UI only
        _processProgress.Value = value;
    }
}
```

---

## Designer Support

### Property Grid Integration

The `TaskbarProgressValues` class uses `ExpandableObjectConverter`, which means in the Visual Studio designer:

1. The `TaskbarProgress` property appears as an expandable node in the Properties window
2. All progress properties are grouped under this node
3. Properties can be edited directly in the designer
4. Changes are serialized to the `.Designer.cs` file

### Designer Code Generation

When you configure taskbar progress in the designer, code similar to this is generated:

```csharp
// 
// kryptonForm1
// 
this.kryptonForm1.TaskbarProgress.Maximum = 100UL;
this.kryptonForm1.TaskbarProgress.State = Krypton.Toolkit.TaskbarProgressState.Normal;
this.kryptonForm1.TaskbarProgress.Value = 50UL;
```

### Designer Limitations

- Progress updates are only applied at runtime, not in the designer
- The taskbar progress indicator is not visible in the designer preview
- Changes to progress properties require the form to have a valid handle at runtime

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
    void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
    void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
    // ... other methods
}
```

#### Progress Flag Values

```csharp
internal enum TBPFLAG
{
    TBPF_NOPROGRESS = 0,
    TBPF_INDETERMINATE = 0x1,
    TBPF_NORMAL = 0x2,
    TBPF_ERROR = 0x4,
    TBPF_PAUSED = 0x8
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

#### Method Call Sequence

```csharp
var taskbarList = (ITaskbarList3)new TaskbarList();
taskbarList.HrInit();

// Set progress state
taskbarList.SetProgressState(Handle, tbpFlag);

// If state is Normal, Error, or Paused, set the progress value
if (tbpFlag != TBPF_NOPROGRESS && tbpFlag != TBPF_INDETERMINATE)
{
    taskbarList.SetProgressValue(Handle, value, maximum);
}
```

### Update Mechanism

The progress indicator is automatically updated:

1. **On Handle Creation**: When the form handle is created (`OnHandleCreated`)
2. **On Property Change**: When the `State`, `Value`, or `Maximum` properties change
3. **Event-Driven**: Uses internal event notification system

### Error Handling

The implementation includes comprehensive error handling:

- **Platform Check**: Verifies Windows 7+ before attempting to use API
- **Handle Validation**: Ensures form handle is created before setting progress
- **COM Exception Handling**: Catches and logs COM-related errors
- **Graceful Degradation**: Silently fails on unsupported platforms

### Value Clamping

The implementation automatically clamps values to valid ranges:

- `Value` is clamped to [0, Maximum] when set
- When `Maximum` changes, `Value` is clamped if it exceeds the new maximum
- This prevents invalid states and ensures consistent behavior

---

## Best Practices

### 1. Initialize Progress Before Operations

Always initialize the progress state before starting a long-running operation:

```csharp
// Good
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Maximum = 100;
form.TaskbarProgress.Value = 0;
await PerformOperationAsync();

// Bad - progress might show stale values
await PerformOperationAsync();
form.TaskbarProgress.Value = 50;
```

### 2. Clear Progress When Complete

Always clear the progress indicator when an operation completes:

```csharp
try
{
    form.TaskbarProgress.State = TaskbarProgressState.Normal;
    await PerformOperationAsync();
}
finally
{
    form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
}
```

### 3. Use Appropriate States

Choose the appropriate progress state for your use case:

- **Normal**: Use for operations with known duration and progress
- **Indeterminate**: Use for operations with unknown duration
- **Error**: Use when an operation fails
- **Paused**: Use when an operation is temporarily paused
- **NoProgress**: Use to clear the progress indicator

### 4. Handle Errors Appropriately

Show error state when operations fail:

```csharp
try
{
    await PerformOperationAsync();
    form.TaskbarProgress.State = TaskbarProgressState.NoProgress;
}
catch (Exception ex)
{
    form.TaskbarProgress.State = TaskbarProgressState.Error;
    // Log error, show message, etc.
}
```

### 5. Update Progress Regularly

Update progress frequently enough to provide smooth visual feedback, but not so frequently that it impacts performance:

```csharp
// Good - update every 1% or significant milestone
for (int i = 0; i <= 100; i++)
{
    form.TaskbarProgress.Value = (ulong)i;
    await ProcessItemAsync(i);
}

// Bad - update too frequently (every iteration of large loop)
for (int i = 0; i < 1000000; i++)
{
    form.TaskbarProgress.Value = (ulong)(i / 10000); // Updates 100,000 times!
    ProcessItem(i);
}
```

### 6. Use Indeterminate for Unknown Duration

Use indeterminate progress for operations where you cannot calculate progress:

```csharp
// Good - use indeterminate for unknown duration
form.TaskbarProgress.State = TaskbarProgressState.Indeterminate;
await WaitForExternalEventAsync();

// Bad - don't fake progress
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Value = 50; // Fake progress value
await WaitForExternalEventAsync();
```

### 7. Thread Safety

If updating progress from background threads, use proper synchronization:

```csharp
private void UpdateProgress(ulong value)
{
    if (InvokeRequired)
    {
        Invoke(new Action<ulong>(UpdateProgress), value);
        return;
    }
    
    TaskbarProgress.Value = value;
}
```

### 8. Using SyncWithTaskbar

When using `SyncWithTaskbar` on `KryptonProgressBar`:

- **One Progress Bar**: Enable sync on the primary progress bar that should control taskbar progress
- **Multiple Progress Bars**: Only enable sync on one progress bar to avoid conflicts
- **Form-Level Operations**: Use form-level `TaskbarProgress` for operations without a UI progress bar
- **Automatic Cleanup**: Taskbar progress is automatically cleared when sync is disabled or control is disposed

```csharp
// Good - single progress bar with sync
var progressBar = new KryptonProgressBar();
progressBar.SyncWithTaskbar = true;
progressBar.Value = 50; // Updates both UI and taskbar

// Good - form-level for operations without UI progress bar
form.TaskbarProgress.State = TaskbarProgressState.Normal;
form.TaskbarProgress.Value = 50;

// Avoid - multiple progress bars with sync (last one wins)
progressBar1.SyncWithTaskbar = true;
progressBar2.SyncWithTaskbar = true; // This will override progressBar1's taskbar updates
```

---

## Troubleshooting

### Progress Indicator Not Showing

**Problem**: Progress indicator doesn't appear on the taskbar.

**Possible Causes**:
1. Windows version is earlier than Windows 7
2. Form handle hasn't been created yet
3. Form is not visible or `ShowInTaskbar` is false
4. Progress state is set to `NoProgress`

**Solutions**:
- Verify Windows version: `Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1`
- Ensure form handle is created: Check `IsHandleCreated` property
- Verify form visibility: `form.Visible == true && form.ShowInTaskbar == true`
- Check progress state: Ensure state is not `NoProgress`

### Progress Value Not Updating

**Problem**: Progress value changes but indicator doesn't update.

**Possible Causes**:
1. Progress state is `NoProgress` or `Indeterminate`
2. Form handle was recreated
3. COM object initialization failed

**Solutions**:
- Set state to `Normal`, `Error`, or `Paused` before setting value
- Re-initialize progress after handle recreation
- Check for COM exceptions in error logs

### Progress Shows Incorrect Percentage

**Problem**: Progress bar shows wrong percentage.

**Possible Causes**:
1. Maximum value is incorrect
2. Value exceeds maximum (should be clamped automatically)
3. Value is negative (should be prevented)

**Solutions**:
- Verify `Maximum` is set correctly before setting `Value`
- Check that `Value` is within valid range [0, Maximum]
- Ensure `Maximum` is greater than 0

### Indeterminate Progress Not Animating

**Problem**: Indeterminate progress shows but doesn't animate.

**Possible Causes**:
1. Windows version doesn't support indeterminate progress
2. Taskbar is in a non-standard configuration
3. Visual effects are disabled in Windows

**Solutions**:
- Verify Windows 7+ support
- Check Windows visual effects settings
- Test on standard Windows configuration

---

## Platform Compatibility

### Supported Platforms

- **Windows 7**: Full support
- **Windows 8/8.1**: Full support
- **Windows 10**: Full support
- **Windows 11**: Full support
- **Windows Vista and earlier**: Not supported (API not available)
- **Non-Windows platforms**: Not supported

### Feature Detection

The implementation automatically detects platform support:

```csharp
// Check if Windows 7+ (ITaskbarList3 requires Windows 7+)
if (Environment.OSVersion.Version.Major < 6 ||
    (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 1))
{
    return; // Not supported on Windows Vista or earlier
}
```

### Graceful Degradation

On unsupported platforms, the feature silently fails without throwing exceptions. Your application will continue to function normally, but the progress indicator will not be displayed.

---

## Related Issues

- **Issue #1214**: Implement "overlay icons" on form task bar images (related feature)
- **Issue #2886**: Taskbar progress bar support (this feature)

---

## See Also

- [Taskbar Overlay Icon Feature](./taskbar-overlay-icon-feature.md) - Related feature for overlay icons
- [Jump List Feature](./jump-list-feature.md) - Related feature for jump lists
- [Windows Taskbar Extensions Documentation](https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-itaskbarlist3) - Microsoft documentation for ITaskbarList3

---

## Version History

- **2026-01-14**: Initial implementation of taskbar progress feature
  - Added `TaskbarProgressValues` class
  - Added `TaskbarProgressState` enumeration (defined in `General/Definitions.cs`)
  - Integrated with `VisualForm` base class
  - Added `SyncWithTaskbar` property to `KryptonProgressBar` for automatic synchronization
  - Full designer support
  - Comprehensive error handling

---

## License

This feature is part of the Krypton Standard Toolkit and is licensed under the BSD 3-Clause License. See the [LICENSE](../LICENSE) file for details.
