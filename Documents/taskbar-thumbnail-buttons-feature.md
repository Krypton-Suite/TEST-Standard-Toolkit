# Taskbar Thumbnail Buttons Feature

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

The Taskbar Thumbnail Buttons feature allows you to add interactive buttons directly to the Windows taskbar thumbnail preview. These buttons appear in the thumbnail flyout when users hover over your application's taskbar button, providing quick access to essential commands without requiring the user to restore or activate the window.

### Key Features

- **Windows 7+ Support**: Uses the native Windows ITaskbarList3 API
- **Interactive Buttons**: Up to 7 buttons per thumbnail toolbar
- **Image List Support**: Uses Windows ImageList for button icons with automatic hover/click states
- **Button States**: Enable, disable, hide, or show buttons dynamically
- **Click Events**: Full event support for button clicks
- **Developer Controlled**: Fully programmable via properties and collections
- **Designer Support**: Full Visual Studio designer integration with expandable properties
- **Automatic Updates**: Button states update automatically when properties change
- **Error Handling**: Gracefully handles unsupported platforms and errors

### Use Cases

- **Media Players**: Play, pause, stop, next, previous, mute controls
- **Hypervisors**: Start, stop, pause, resume virtual machines
- **Communication Apps**: Answer, decline, mute, hold call buttons
- **File Transfer Apps**: Pause, resume, cancel transfer operations
- **Development Tools**: Build, run, stop, debug operations
- **System Utilities**: Quick actions for system operations
- **Any Application**: Quick access to frequently used commands

### Requirements

- **Windows Version**: Windows 7 or later (ITaskbarList3 API requirement)
- **Form Handle**: Form must have a valid window handle
- **Taskbar Visibility**: Form must be visible and shown in taskbar (`ShowInTaskbar = true`)
- **Thumbnail Display**: Buttons only appear when thumbnails are displayed (not in legacy menu mode)
- **Image List**: Button images must be provided via an ImageList (32-bit icons recommended)

---

## Quick Start

### Basic Usage

```csharp
// Create a form with thumbnail buttons
var form = new KryptonForm();
form.Text = "Media Player";

// Create an image list with button icons
var imageList = new ImageList();
imageList.Images.Add(Properties.Resources.PlayIcon);
imageList.Images.Add(Properties.Resources.PauseIcon);
imageList.Images.Add(Properties.Resources.StopIcon);

// Set the image list
form.TaskbarThumbnailButtons.ImageList = imageList;

// Add buttons
var playButton = new TaskbarThumbnailButton
{
    Id = 1,
    ImageIndex = 0,
    Tooltip = "Play",
    Flags = ThumbnailButtonFlags.Enabled
};
form.TaskbarThumbnailButtons.Buttons.Add(playButton);

var pauseButton = new TaskbarThumbnailButton
{
    Id = 2,
    ImageIndex = 1,
    Tooltip = "Pause",
    Flags = ThumbnailButtonFlags.Enabled
};
form.TaskbarThumbnailButtons.Buttons.Add(pauseButton);

// Handle button clicks
form.TaskbarThumbnailButtonClick += (sender, e) =>
{
    switch (e.ButtonId)
    {
        case 1:
            // Handle play
            break;
        case 2:
            // Handle pause
            break;
    }
};
```

### Designer Usage

1. Select a `KryptonForm` in the designer
2. In the Properties window, find the `TaskbarThumbnailButtons` property
3. Expand the `TaskbarThumbnailButtons` property (it appears as an expandable object)
4. Set the `ImageList` property to an ImageList component
5. Use the `Buttons` collection editor to add and configure buttons

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Classes

### ThumbnailButtonFlags Enumeration

Enumeration defining the flags that control button behavior and state. This enum is defined in `General/Definitions.cs`.

```csharp
[Flags]
public enum ThumbnailButtonFlags
{
    Enabled = 0x00000000,           // Button is enabled
    Disabled = 0x00000001,          // Button is disabled
    DismissOnClick = 0x00000002,    // Dismiss thumbnail on click
    NoBackground = 0x00000004,      // No background border
    Hidden = 0x00000008,            // Button is hidden
    NonInteractive = 0x00000010    // Button is non-interactive
}
```

**Location**: `Krypton.Toolkit.General/Definitions.cs`

#### Enum Values

| Value | Description |
|-------|-------------|
| `Enabled` | Button is enabled and can be clicked. This is the default state. |
| `Disabled` | Button is disabled and appears grayed out. Users cannot click disabled buttons. |
| `DismissOnClick` | When clicked, the thumbnail preview is automatically dismissed. Useful for actions that complete an operation. |
| `NoBackground` | Button has no background border. The button appears as just the icon without a border. |
| `Hidden` | Button is hidden and not displayed. The button still exists but is not visible. |
| `NonInteractive` | Button is non-interactive. The button is displayed but cannot be clicked. Useful for status indicators. |

**Note**: Flags can be combined using bitwise OR (`|`). For example: `ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick`

---

### ThumbnailButtonMask Enumeration

Enumeration defining which fields in a thumbnail button structure are valid. Used internally for Windows API marshaling.

```csharp
[Flags]
internal enum ThumbnailButtonMask
{
    Bitmap = 0x00000001,    // iBitmap field is valid
    Icon = 0x00000002,      // hIcon field is valid
    Tooltip = 0x00000004,   // pszTip field is valid
    Flags = 0x00000008      // dwFlags field is valid
}
```

**Location**: `Krypton.Toolkit.General/PlatformInvoke.cs`

---

### TaskbarThumbnailButton Class

Represents a single button in the thumbnail toolbar.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Id` | `uint` | `0` | Unique identifier for the button. This ID is sent in the click event when the button is clicked. Must be unique within the button collection. |
| `ImageIndex` | `int` | `-1` | Index of the image in the ImageList to use for this button. Must be a valid index in the associated ImageList. |
| `Tooltip` | `string` | `""` | Tooltip text displayed when hovering over the button. Maximum length is 260 characters (Windows limitation). |
| `Flags` | `ThumbnailButtonFlags` | `Enabled` | Flags controlling button behavior and state. Can be combined using bitwise OR. |

#### Methods

##### `CopyFrom(TaskbarThumbnailButton source)`

Copies all properties from another button instance.

```csharp
var source = new TaskbarThumbnailButton
{
    Id = 1,
    ImageIndex = 0,
    Tooltip = "Play",
    Flags = ThumbnailButtonFlags.Enabled
};

var target = new TaskbarThumbnailButton();
target.CopyFrom(source); // Copies all properties
```

##### `Reset()`

Resets all properties to their default values.

```csharp
button.Reset(); // Sets Id=0, ImageIndex=-1, Tooltip="", Flags=Enabled
```

#### Serialization Methods

- `ShouldSerializeId()` - Returns `true` if Id is not 0
- `ShouldSerializeImageIndex()` - Returns `true` if ImageIndex is not -1
- `ShouldSerializeTooltip()` - Returns `true` if Tooltip is not empty
- `ShouldSerializeFlags()` - Returns `true` if Flags is not Enabled

#### Reset Methods

- `ResetId()` - Sets Id to 0
- `ResetImageIndex()` - Sets ImageIndex to -1
- `ResetTooltip()` - Sets Tooltip to empty string
- `ResetFlags()` - Sets Flags to Enabled

---

### TaskbarThumbnailButtonCollection Class

Collection class for managing thumbnail buttons. Inherits from `List<TaskbarThumbnailButton>` with additional change notification.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Count` | `int` | Gets the number of buttons in the collection. Maximum is 7 (Windows limitation). |

#### Methods

##### `Add(TaskbarThumbnailButton button)`

Adds a button to the collection. Throws an exception if the collection already has 7 buttons.

```csharp
var button = new TaskbarThumbnailButton { Id = 1, ImageIndex = 0 };
collection.Add(button);
```

##### `Remove(TaskbarThumbnailButton button)`

Removes a button from the collection.

```csharp
collection.Remove(button);
```

##### `Clear()`

Removes all buttons from the collection.

```csharp
collection.Clear();
```

##### `FindById(uint id)`

Finds a button by its ID.

```csharp
var button = collection.FindById(1);
```

#### Events

##### `CollectionChanged`

Raised when the collection is modified (items added, removed, or cleared). Used internally to update the thumbnail toolbar.

---

### TaskbarThumbnailButtonValues Class

Storage class for taskbar thumbnail button value information. This class uses `ExpandableObjectConverter` for designer support.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ImageList` | `ImageList?` | `null` | The ImageList containing button icons. Must be set before adding buttons. Icons should be 32-bit with dimensions matching `GetSystemMetrics(SM_CXICON) x GetSystemMetrics(SM_CYICON)` (typically 32x32 pixels). |
| `Buttons` | `TaskbarThumbnailButtonCollection` | Empty collection | Collection of thumbnail buttons. Maximum 7 buttons (Windows limitation). Buttons are displayed in the order they appear in the collection. |

#### Methods

##### `CopyFrom(TaskbarThumbnailButtonValues source)`

Copies all thumbnail button values from another instance.

```csharp
var source = new TaskbarThumbnailButtonValues(needPaint);
source.ImageList = myImageList;
source.Buttons.Add(new TaskbarThumbnailButton { Id = 1, ImageIndex = 0 });

var target = new TaskbarThumbnailButtonValues(needPaint);
target.CopyFrom(source); // Copies ImageList and all buttons
```

##### `Reset()`

Resets all values to their defaults.

```csharp
thumbnailButtons.Reset(); // Clears ImageList and Buttons collection
```

#### Serialization Methods

- `ShouldSerializeImageList()` - Returns `true` if ImageList is not null
- `ShouldSerializeButtons()` - Returns `true` if Buttons collection is not empty

#### Reset Methods

- `ResetImageList()` - Sets ImageList to null
- `ResetButtons()` - Clears the Buttons collection

---

### KryptonForm Properties

#### `TaskbarThumbnailButtons`

Gets access to the taskbar thumbnail button values.

```csharp
[Category(@"Visuals")]
[Description(@"Taskbar thumbnail buttons to display in the thumbnail preview.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public TaskbarThumbnailButtonValues TaskbarThumbnailButtons { get; }
```

**Usage**:
```csharp
form.TaskbarThumbnailButtons.ImageList = myImageList;
form.TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 1, ImageIndex = 0 });
```

#### `ResetTaskbarThumbnailButtons()`

Resets the TaskbarThumbnailButtons property to its default value.

```csharp
public void ResetTaskbarThumbnailButtons() => TaskbarThumbnailButtons.Reset();
```

#### `ShouldSerializeTaskbarThumbnailButtons()`

Indicates whether the TaskbarThumbnailButtons property should be serialized.

```csharp
public bool ShouldSerializeTaskbarThumbnailButtons() => !TaskbarThumbnailButtons.IsDefault;
```

---

### Events

#### `TaskbarThumbnailButtonClick`

Raised when a thumbnail button is clicked.

**Event Signature**:
```csharp
public event EventHandler<TaskbarThumbnailButtonClickEventArgs>? TaskbarThumbnailButtonClick;
```

**Event Args**:
```csharp
public class TaskbarThumbnailButtonClickEventArgs : EventArgs
{
    public uint ButtonId { get; }  // ID of the clicked button
}
```

**Usage**:
```csharp
form.TaskbarThumbnailButtonClick += (sender, e) =>
{
    switch (e.ButtonId)
    {
        case 1:
            // Handle button 1 click
            break;
        case 2:
            // Handle button 2 click
            break;
    }
};
```

---

## Usage Examples

### Example 1: Basic Media Player Controls

```csharp
public partial class MediaPlayerForm : KryptonForm
{
    private readonly ImageList _buttonImages;

    public MediaPlayerForm()
    {
        InitializeComponent();
        
        // Create image list with button icons
        _buttonImages = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        _buttonImages.Images.Add(Properties.Resources.PlayIcon);
        _buttonImages.Images.Add(Properties.Resources.PauseIcon);
        _buttonImages.Images.Add(Properties.Resources.StopIcon);
        _buttonImages.Images.Add(Properties.Resources.NextIcon);
        _buttonImages.Images.Add(Properties.Resources.PreviousIcon);

        // Set image list
        TaskbarThumbnailButtons.ImageList = _buttonImages;

        // Add buttons
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Play",
            Flags = ThumbnailButtonFlags.Enabled
        });

        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 2,
            ImageIndex = 1,
            Tooltip = "Pause",
            Flags = ThumbnailButtonFlags.Enabled
        });

        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 3,
            ImageIndex = 2,
            Tooltip = "Stop",
            Flags = ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick
        });

        // Handle button clicks
        TaskbarThumbnailButtonClick += OnThumbnailButtonClick;
    }

    private void OnThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        switch (e.ButtonId)
        {
            case 1:
                Play();
                break;
            case 2:
                Pause();
                break;
            case 3:
                Stop();
                break;
        }
    }

    private void Play() { /* Implementation */ }
    private void Pause() { /* Implementation */ }
    private void Stop() { /* Implementation */ }
}
```

### Example 2: Virtual Machine Controls

```csharp
public partial class VirtualMachineForm : KryptonForm
{
    private bool _isRunning;

    public VirtualMachineForm()
    {
        InitializeComponent();
        InitializeThumbnailButtons();
    }

    private void InitializeThumbnailButtons()
    {
        var imageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(Properties.Resources.StartIcon);
        imageList.Images.Add(Properties.Resources.StopIcon);
        imageList.Images.Add(Properties.Resources.PauseIcon);
        imageList.Images.Add(Properties.Resources.ResumeIcon);

        TaskbarThumbnailButtons.ImageList = imageList;

        // Start button
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Start Virtual Machine",
            Flags = ThumbnailButtonFlags.Enabled
        });

        // Stop button
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 2,
            ImageIndex = 1,
            Tooltip = "Stop Virtual Machine",
            Flags = ThumbnailButtonFlags.Disabled
        });

        // Pause button
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 3,
            ImageIndex = 2,
            Tooltip = "Pause Virtual Machine",
            Flags = ThumbnailButtonFlags.Disabled
        });

        // Resume button
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 4,
            ImageIndex = 3,
            Tooltip = "Resume Virtual Machine",
            Flags = ThumbnailButtonFlags.Hidden
        });

        TaskbarThumbnailButtonClick += OnThumbnailButtonClick;
    }

    private void OnThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        switch (e.ButtonId)
        {
            case 1:
                StartVirtualMachine();
                break;
            case 2:
                StopVirtualMachine();
                break;
            case 3:
                PauseVirtualMachine();
                break;
            case 4:
                ResumeVirtualMachine();
                break;
        }
    }

    private void StartVirtualMachine()
    {
        _isRunning = true;
        UpdateButtonStates();
    }

    private void StopVirtualMachine()
    {
        _isRunning = false;
        UpdateButtonStates();
    }

    private void PauseVirtualMachine()
    {
        UpdateButtonStates();
    }

    private void ResumeVirtualMachine()
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        // Update button states based on VM state
        var startButton = TaskbarThumbnailButtons.Buttons.FindById(1);
        var stopButton = TaskbarThumbnailButtons.Buttons.FindById(2);
        var pauseButton = TaskbarThumbnailButtons.Buttons.FindById(3);
        var resumeButton = TaskbarThumbnailButtons.Buttons.FindById(4);

        if (_isRunning)
        {
            startButton.Flags = ThumbnailButtonFlags.Disabled;
            stopButton.Flags = ThumbnailButtonFlags.Enabled;
            pauseButton.Flags = ThumbnailButtonFlags.Enabled;
            resumeButton.Flags = ThumbnailButtonFlags.Hidden;
        }
        else
        {
            startButton.Flags = ThumbnailButtonFlags.Enabled;
            stopButton.Flags = ThumbnailButtonFlags.Disabled;
            pauseButton.Flags = ThumbnailButtonFlags.Disabled;
            resumeButton.Flags = ThumbnailButtonFlags.Hidden;
        }
    }
}
```

### Example 3: File Transfer Controls

```csharp
public partial class FileTransferForm : KryptonForm
{
    private bool _isPaused;

    public FileTransferForm()
    {
        InitializeComponent();
        InitializeThumbnailButtons();
    }

    private void InitializeThumbnailButtons()
    {
        var imageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(Properties.Resources.PauseIcon);
        imageList.Images.Add(Properties.Resources.ResumeIcon);
        imageList.Images.Add(Properties.Resources.CancelIcon);

        TaskbarThumbnailButtons.ImageList = imageList;

        // Pause/Resume button (toggles based on state)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Pause Transfer",
            Flags = ThumbnailButtonFlags.Enabled
        });

        // Cancel button
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 2,
            ImageIndex = 2,
            Tooltip = "Cancel Transfer",
            Flags = ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick
        });

        TaskbarThumbnailButtonClick += OnThumbnailButtonClick;
    }

    private void OnThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        switch (e.ButtonId)
        {
            case 1:
                TogglePause();
                break;
            case 2:
                CancelTransfer();
                break;
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        var pauseButton = TaskbarThumbnailButtons.Buttons.FindById(1);
        
        if (_isPaused)
        {
            pauseButton.ImageIndex = 1; // Resume icon
            pauseButton.Tooltip = "Resume Transfer";
            ResumeTransfer();
        }
        else
        {
            pauseButton.ImageIndex = 0; // Pause icon
            pauseButton.Tooltip = "Pause Transfer";
            PauseTransfer();
        }
    }

    private void PauseTransfer() { /* Implementation */ }
    private void ResumeTransfer() { /* Implementation */ }
    private void CancelTransfer() { /* Implementation */ }
}
```

### Example 4: Dynamic Button Updates

```csharp
public partial class DynamicButtonsForm : KryptonForm
{
    public DynamicButtonsForm()
    {
        InitializeComponent();
        InitializeThumbnailButtons();
    }

    private void InitializeThumbnailButtons()
    {
        var imageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(Properties.Resources.Action1Icon);
        imageList.Images.Add(Properties.Resources.Action2Icon);
        imageList.Images.Add(Properties.Resources.Action3Icon);

        TaskbarThumbnailButtons.ImageList = imageList;

        // Add initial buttons
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Action 1",
            Flags = ThumbnailButtonFlags.Enabled
        });

        TaskbarThumbnailButtonClick += OnThumbnailButtonClick;
    }

    private void OnThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        var button = TaskbarThumbnailButtons.Buttons.FindById(e.ButtonId);
        
        // Disable button temporarily
        button.Flags = ThumbnailButtonFlags.Disabled;
        
        // Perform action
        PerformAction(e.ButtonId);
        
        // Re-enable after delay
        Task.Delay(1000).ContinueWith(_ =>
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => button.Flags = ThumbnailButtonFlags.Enabled));
            }
            else
            {
                button.Flags = ThumbnailButtonFlags.Enabled;
            }
        });
    }

    private void PerformAction(uint buttonId) { /* Implementation */ }

    // Example: Show/hide buttons based on context
    public void ShowActionButtons()
    {
        foreach (var button in TaskbarThumbnailButtons.Buttons)
        {
            if (button.Flags.HasFlag(ThumbnailButtonFlags.Hidden))
            {
                button.Flags &= ~ThumbnailButtonFlags.Hidden;
            }
        }
    }

    public void HideActionButtons()
    {
        foreach (var button in TaskbarThumbnailButtons.Buttons)
        {
            button.Flags |= ThumbnailButtonFlags.Hidden;
        }
    }
}
```

### Example 5: Communication App Controls

```csharp
public partial class CommunicationForm : KryptonForm
{
    private bool _inCall;

    public CommunicationForm()
    {
        InitializeComponent();
        InitializeThumbnailButtons();
    }

    private void InitializeThumbnailButtons()
    {
        var imageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(Properties.Resources.AnswerIcon);
        imageList.Images.Add(Properties.Resources.DeclineIcon);
        imageList.Images.Add(Properties.Resources.MuteIcon);
        imageList.Images.Add(Properties.Resources.HoldIcon);

        TaskbarThumbnailButtons.ImageList = imageList;

        // Answer button (shown when incoming call)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Answer Call",
            Flags = ThumbnailButtonFlags.Hidden
        });

        // Decline button (shown when incoming call)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 2,
            ImageIndex = 1,
            Tooltip = "Decline Call",
            Flags = ThumbnailButtonFlags.Hidden | ThumbnailButtonFlags.DismissOnClick
        });

        // Mute button (shown during call)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 3,
            ImageIndex = 2,
            Tooltip = "Mute",
            Flags = ThumbnailButtonFlags.Hidden
        });

        // Hold button (shown during call)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 4,
            ImageIndex = 3,
            Tooltip = "Hold",
            Flags = ThumbnailButtonFlags.Hidden
        });

        TaskbarThumbnailButtonClick += OnThumbnailButtonClick;
    }

    public void OnIncomingCall()
    {
        // Show answer/decline buttons
        TaskbarThumbnailButtons.Buttons.FindById(1).Flags = ThumbnailButtonFlags.Enabled;
        TaskbarThumbnailButtons.Buttons.FindById(2).Flags = ThumbnailButtonFlags.Enabled;
    }

    public void OnCallStarted()
    {
        _inCall = true;
        
        // Hide answer/decline, show mute/hold
        TaskbarThumbnailButtons.Buttons.FindById(1).Flags = ThumbnailButtonFlags.Hidden;
        TaskbarThumbnailButtons.Buttons.FindById(2).Flags = ThumbnailButtonFlags.Hidden;
        TaskbarThumbnailButtons.Buttons.FindById(3).Flags = ThumbnailButtonFlags.Enabled;
        TaskbarThumbnailButtons.Buttons.FindById(4).Flags = ThumbnailButtonFlags.Enabled;
    }

    public void OnCallEnded()
    {
        _inCall = false;
        
        // Hide all buttons
        foreach (var button in TaskbarThumbnailButtons.Buttons)
        {
            button.Flags = ThumbnailButtonFlags.Hidden;
        }
    }

    private void OnThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        switch (e.ButtonId)
        {
            case 1:
                AnswerCall();
                break;
            case 2:
                DeclineCall();
                break;
            case 3:
                ToggleMute();
                break;
            case 4:
                ToggleHold();
                break;
        }
    }

    private void AnswerCall() { /* Implementation */ }
    private void DeclineCall() { /* Implementation */ }
    private void ToggleMute() { /* Implementation */ }
    private void ToggleHold() { /* Implementation */ }
}
```

### Example 6: Status Indicator Buttons

```csharp
public partial class StatusForm : KryptonForm
{
    public StatusForm()
    {
        InitializeComponent();
        InitializeThumbnailButtons();
    }

    private void InitializeThumbnailButtons()
    {
        var imageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(Properties.Resources.OnlineIcon);
        imageList.Images.Add(Properties.Resources.OfflineIcon);
        imageList.Images.Add(Properties.Resources.SyncingIcon);

        TaskbarThumbnailButtons.ImageList = imageList;

        // Status indicator (non-interactive)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            ImageIndex = 0,
            Tooltip = "Status: Online",
            Flags = ThumbnailButtonFlags.NonInteractive
        });

        // Sync indicator (non-interactive, hidden initially)
        TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 2,
            ImageIndex = 2,
            Tooltip = "Synchronizing...",
            Flags = ThumbnailButtonFlags.NonInteractive | ThumbnailButtonFlags.Hidden
        });
    }

    public void SetStatus(ConnectionStatus status)
    {
        var statusButton = TaskbarThumbnailButtons.Buttons.FindById(1);
        
        switch (status)
        {
            case ConnectionStatus.Online:
                statusButton.ImageIndex = 0;
                statusButton.Tooltip = "Status: Online";
                statusButton.Flags = ThumbnailButtonFlags.NonInteractive;
                break;
            case ConnectionStatus.Offline:
                statusButton.ImageIndex = 1;
                statusButton.Tooltip = "Status: Offline";
                statusButton.Flags = ThumbnailButtonFlags.NonInteractive;
                break;
        }
    }

    public void ShowSyncing(bool syncing)
    {
        var syncButton = TaskbarThumbnailButtons.Buttons.FindById(2);
        
        if (syncing)
        {
            syncButton.Flags = ThumbnailButtonFlags.NonInteractive;
        }
        else
        {
            syncButton.Flags = ThumbnailButtonFlags.NonInteractive | ThumbnailButtonFlags.Hidden;
        }
    }
}
```

---

## Designer Support

### Property Grid Integration

The `TaskbarThumbnailButtonValues` class uses `ExpandableObjectConverter`, which means in the Visual Studio designer:

1. The `TaskbarThumbnailButtons` property appears as an expandable node in the Properties window
2. All thumbnail button properties are grouped under this node
3. Properties can be edited directly in the designer
4. The `Buttons` collection can be edited using the collection editor
5. Changes are serialized to the `.Designer.cs` file

### Collection Editor

The `Buttons` collection supports the standard Windows Forms collection editor:

1. Click the ellipsis (...) button next to the `Buttons` property
2. Use the collection editor to add, remove, and configure buttons
3. Set properties for each button in the property grid
4. Buttons are displayed in the order they appear in the collection

### Designer Code Generation

When you configure thumbnail buttons in the designer, code similar to this is generated:

```csharp
// 
// kryptonForm1
// 
this.imageList1.Images.SetKeyName(0, "PlayIcon");
this.imageList1.Images.SetKeyName(1, "PauseIcon");
this.kryptonForm1.TaskbarThumbnailButtons.ImageList = this.imageList1;
this.kryptonForm1.TaskbarThumbnailButtons.Buttons.AddRange(new Krypton.Toolkit.TaskbarThumbnailButton[] {
    new Krypton.Toolkit.TaskbarThumbnailButton {
        Id = 1U,
        ImageIndex = 0,
        Tooltip = "Play",
        Flags = Krypton.Toolkit.ThumbnailButtonFlags.Enabled
    },
    new Krypton.Toolkit.TaskbarThumbnailButton {
        Id = 2U,
        ImageIndex = 1,
        Tooltip = "Pause",
        Flags = Krypton.Toolkit.ThumbnailButtonFlags.Enabled
    }
});
```

### Designer Limitations

- Thumbnail buttons are only applied at runtime, not in the designer
- The thumbnail toolbar is not visible in the designer preview
- Changes to button properties require the form to have a valid handle at runtime
- ImageList must be created and populated before buttons can be added

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
    void ThumbBarAddButtons(IntPtr hwnd, uint cButtons, IntPtr[] pButtons);
    void ThumbBarUpdateButtons(IntPtr hwnd, uint cButtons, IntPtr[] pButtons);
    void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
    // ... other methods
}
```

#### THUMBBUTTON Structure

The Windows `THUMBBUTTON` structure is marshaled as:

```csharp
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct THUMBBUTTON
{
    public ThumbnailButtonMask dwMask;
    public uint iId;
    public uint iBitmap;
    public IntPtr hIcon;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string pszTip;
    public ThumbnailButtonFlags dwFlags;
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

// Set image list (must be done before adding buttons)
taskbarList.ThumbBarSetImageList(Handle, imageList.Handle);

// Add buttons (can only be done once per window)
IntPtr[] buttonPtrs = MarshalButtons(buttons);
taskbarList.ThumbBarAddButtons(Handle, (uint)buttons.Count, buttonPtrs);

// Update buttons (can be called multiple times)
taskbarList.ThumbBarUpdateButtons(Handle, (uint)buttons.Count, buttonPtrs);
```

### Message Handling

When a thumbnail button is clicked, Windows sends a `WM_COMMAND` message with:

- **HIWORD(wParam)**: `THBN_CLICKED` (0x1800)
- **LOWORD(wParam)**: Button ID

The implementation handles this in `WndProc`:

```csharp
protected override void WndProc(ref Message m)
{
    const int WM_COMMAND = 0x0111;
    const int THBN_CLICKED = 0x1800;
    
    if (m.Msg == WM_COMMAND)
    {
        uint hiWord = (uint)((int)m.WParam >> 16) & 0xFFFF;
        uint loWord = (uint)(int)m.WParam & 0xFFFF;
        
        if (hiWord == THBN_CLICKED)
        {
            // Raise TaskbarThumbnailButtonClick event
            OnTaskbarThumbnailButtonClick(new TaskbarThumbnailButtonClickEventArgs(loWord));
            m.Result = IntPtr.Zero;
            return;
        }
    }
    
    base.WndProc(ref m);
}
```

### Update Mechanism

The thumbnail toolbar is automatically updated:

1. **On Handle Creation**: When the form handle is created (`OnHandleCreated`)
2. **On ImageList Change**: When the `ImageList` property changes
3. **On Button Collection Change**: When buttons are added, removed, or modified
4. **On Button Property Change**: When individual button properties change
5. **Event-Driven**: Uses internal event notification system

### Image List Requirements

- **Size**: Icons should match `GetSystemMetrics(SM_CXICON) x GetSystemMetrics(SM_CYICON)` (typically 32x32 pixels)
- **Format**: 32-bit icons recommended for best visual quality
- **Color Depth**: Use `ColorDepth.Depth32Bit` for ImageList
- **Automatic States**: Windows automatically provides hover, click, and disabled states for buttons
- **Transparency**: Icons should use transparency for clean appearance

### Button Limitations

- **Maximum Buttons**: 7 buttons per thumbnail toolbar (Windows limitation)
- **One-Time Addition**: Buttons can only be added once per window. After `ThumbBarAddButtons` is called, buttons cannot be added or removed, only updated.
- **Fixed Order**: Button order is determined by the order in the collection when buttons are first added and cannot be changed.
- **No Reordering**: Buttons cannot be reordered after being added.

### Error Handling

The implementation includes comprehensive error handling:

- **Platform Check**: Verifies Windows 7+ before attempting to use API
- **Handle Validation**: Ensures form handle is created before setting buttons
- **ImageList Validation**: Verifies ImageList is set before adding buttons
- **Button Count Validation**: Prevents adding more than 7 buttons
- **COM Exception Handling**: Catches and logs COM-related errors
- **Graceful Degradation**: Silently fails on unsupported platforms

---

## Best Practices

### 1. Image List Setup

Always set up the ImageList before adding buttons:

```csharp
// Good
var imageList = new ImageList
{
    ImageSize = new Size(32, 32),
    ColorDepth = ColorDepth.Depth32Bit
};
imageList.Images.Add(Properties.Resources.PlayIcon);
form.TaskbarThumbnailButtons.ImageList = imageList;
form.TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { ImageIndex = 0 });

// Bad - ImageList not set
form.TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { ImageIndex = 0 });
form.TaskbarThumbnailButtons.ImageList = imageList; // Too late!
```

### 2. Button ID Management

Use unique, meaningful IDs for buttons:

```csharp
// Good - use constants for button IDs
private const uint BUTTON_PLAY = 1;
private const uint BUTTON_PAUSE = 2;
private const uint BUTTON_STOP = 3;

TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
{
    Id = BUTTON_PLAY,
    ImageIndex = 0,
    Tooltip = "Play"
});

// Bad - magic numbers
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
{
    Id = 1, // What does 1 mean?
    ImageIndex = 0
});
```

### 3. Button State Management

Update button states to reflect application state:

```csharp
private void UpdateButtonStates(bool isPlaying)
{
    var playButton = TaskbarThumbnailButtons.Buttons.FindById(BUTTON_PLAY);
    var pauseButton = TaskbarThumbnailButtons.Buttons.FindById(BUTTON_PAUSE);
    
    if (isPlaying)
    {
        playButton.Flags = ThumbnailButtonFlags.Disabled;
        pauseButton.Flags = ThumbnailButtonFlags.Enabled;
    }
    else
    {
        playButton.Flags = ThumbnailButtonFlags.Enabled;
        pauseButton.Flags = ThumbnailButtonFlags.Disabled;
    }
}
```

### 4. Use DismissOnClick Appropriately

Use `DismissOnClick` for actions that complete an operation:

```csharp
// Good - stop action dismisses thumbnail
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
{
    Id = BUTTON_STOP,
    ImageIndex = 2,
    Tooltip = "Stop",
    Flags = ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick
});

// Avoid - play action shouldn't dismiss
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
{
    Id = BUTTON_PLAY,
    Flags = ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick // User might want to pause next
});
```

### 5. Thread Safety

Always update buttons from the UI thread:

```csharp
private void UpdateButtonFromBackgroundThread(uint buttonId, ThumbnailButtonFlags flags)
{
    if (InvokeRequired)
    {
        Invoke(new Action(() => UpdateButtonFromBackgroundThread(buttonId, flags)));
        return;
    }
    
    var button = TaskbarThumbnailButtons.Buttons.FindById(buttonId);
    if (button != null)
    {
        button.Flags = flags;
    }
}
```

### 6. Button Ordering

Place most important buttons first (leftmost), as buttons may be truncated from right to left if space is limited:

```csharp
// Good - most important buttons first
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_PLAY });    // Most used
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_PAUSE });  // Frequently used
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_STOP });    // Less frequently used
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_NEXT }); // Occasionally used

// Bad - important button last (might be truncated)
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_NEXT });
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = BUTTON_PLAY }); // Might be hidden!
```

### 7. Icon Design

Design icons that work well at small sizes:

- **Size**: Use 32x32 pixel icons (Windows will scale as needed)
- **Simplicity**: Keep icons simple and recognizable
- **Contrast**: Use high-contrast colors for visibility
- **Transparency**: Use transparency for clean appearance
- **Consistency**: Use consistent style across all button icons

### 8. Error Handling

Handle errors gracefully:

```csharp
private void SafeUpdateButtons()
{
    try
    {
        if (!IsHandleCreated)
        {
            HandleCreated += (s, e) => SafeUpdateButtons();
            return;
        }
        
        if (Environment.OSVersion.Version < new Version(6, 1))
        {
            return; // Not supported
        }
        
        // Update buttons
        UpdateButtonStates();
    }
    catch (Exception ex)
    {
        // Log error but don't crash
        Debug.WriteLine($"Failed to update thumbnail buttons: {ex.Message}");
    }
}
```

### 9. Resource Management

Properly dispose of ImageList when form is disposed:

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _buttonImageList?.Dispose();
    }
    base.Dispose(disposing);
}
```

### 10. Limit Button Count

Respect the 7-button limit and prioritize functionality:

```csharp
// Good - 5 buttons, well within limit
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 1 }); // Play
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 2 }); // Pause
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 3 }); // Stop
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 4 }); // Next
TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = 5 }); // Previous

// Avoid - trying to add 10 buttons (only 7 allowed)
for (int i = 1; i <= 10; i++)
{
    TaskbarThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton { Id = (uint)i });
}
```

---

## Troubleshooting

### Buttons Not Appearing

**Problem**: Thumbnail buttons don't appear in the taskbar preview.

**Possible Causes**:
1. Windows version is earlier than Windows 7
2. Form handle hasn't been created yet
3. ImageList is not set
4. Buttons collection is empty
5. Thumbnails are not being displayed (legacy menu mode)
6. Form is not visible or `ShowInTaskbar` is false

**Solutions**:
- Verify Windows version: `Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1`
- Ensure form handle is created: Check `IsHandleCreated` property
- Verify ImageList is set: `form.TaskbarThumbnailButtons.ImageList != null`
- Check buttons collection: `form.TaskbarThumbnailButtons.Buttons.Count > 0`
- Verify form visibility: `form.Visible == true && form.ShowInTaskbar == true`
- Check if thumbnails are displayed: Buttons only appear when thumbnails are shown (not in legacy menu)

### Button Clicks Not Working

**Problem**: Buttons appear but clicks don't trigger events.

**Possible Causes**:
1. Event handler not attached
2. Button is disabled or hidden
3. Button has `NonInteractive` flag
4. Message handling issue

**Solutions**:
- Verify event handler is attached: `form.TaskbarThumbnailButtonClick += Handler`
- Check button flags: Ensure button doesn't have `Disabled`, `Hidden`, or `NonInteractive` flags
- Verify button ID matches: Use the correct button ID in event handler
- Check Windows message handling: Ensure `WndProc` is properly overriding base implementation

### ImageList Issues

**Problem**: Button icons don't display correctly.

**Possible Causes**:
1. ImageList not set before adding buttons
2. ImageIndex is invalid
3. ImageList size is incorrect
4. ImageList color depth is too low

**Solutions**:
- Set ImageList before adding buttons
- Verify ImageIndex is valid: `0 <= ImageIndex < ImageList.Images.Count`
- Use appropriate size: `ImageSize = new Size(32, 32)` (or match system icon size)
- Use 32-bit color depth: `ColorDepth = ColorDepth.Depth32Bit`

### Buttons Not Updating

**Problem**: Button states don't update when properties change.

**Possible Causes**:
1. Buttons haven't been added yet (ThumbBarAddButtons not called)
2. Form handle was recreated
3. Update mechanism not triggered

**Solutions**:
- Ensure buttons are added first: `ThumbBarAddButtons` must be called before updates
- Re-initialize after handle recreation: Update buttons in `OnHandleCreated`
- Manually trigger update: Changes to button properties should automatically trigger updates

### Maximum Button Limit

**Problem**: Cannot add more than 7 buttons.

**Explanation**: This is a Windows limitation. The `ITaskbarList3::ThumbBarAddButtons` API supports a maximum of 7 buttons per thumbnail toolbar.

**Solutions**:
- Prioritize buttons: Only include the most important actions
- Use button states: Show/hide buttons based on context instead of adding more
- Combine functionality: Use a single button that toggles between states

### Buttons Truncated

**Problem**: Some buttons are not visible in the thumbnail preview.

**Explanation**: Windows may truncate buttons from right to left if there's insufficient space in the thumbnail preview.

**Solutions**:
- Prioritize button order: Place most important buttons first (leftmost)
- Reduce button count: Use fewer buttons if possible
- Accept limitation: This is a Windows behavior and cannot be changed

### COM Exception

**Problem**: COM exception when adding or updating buttons.

**Possible Causes**:
1. Windows version doesn't support ITaskbarList3
2. COM registration issue
3. Form handle is invalid
4. Invalid button structure

**Solutions**:
- Verify Windows 7+: Check OS version before using feature
- Check form handle: Ensure handle is valid
- Verify button structure: Ensure all required fields are set
- Check error logs: Review Windows event logs for details

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
public static bool IsTaskbarThumbnailButtonsSupported()
{
    // ITaskbarList3 requires Windows 7+
    return Environment.OSVersion.Version.Major >= 6 &&
           (Environment.OSVersion.Version.Major > 6 || Environment.OSVersion.Version.Minor >= 1);
}
```

### Graceful Degradation

On unsupported platforms, the feature silently fails without throwing exceptions. Your application will continue to function normally, but thumbnail buttons will not be displayed.

### Thumbnail Display Requirements

Thumbnail buttons only appear when:
- Thumbnails are being displayed (not in legacy menu mode)
- There is sufficient space in the thumbnail preview
- The taskbar is in a standard configuration

If Windows reverts to legacy menu mode (e.g., too many windows open), buttons will not be visible.

---

## Related Issues

- **GitHub Issue**: [#2916](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2916) - Taskbar Thumbnail Button support

---

## See Also

- [Taskbar Overlay Icon Feature](./taskbar-overlay-icon-feature.md) - Related feature for overlay icons
- [Taskbar Progress Feature](./taskbar-progress-feature.md) - Related feature for progress indicators
- [Jump List Feature](./jump-list-feature.md) - Related feature for jump lists
- [Windows Taskbar Extensions Documentation](https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist3-thumbbaraddbuttons) - Microsoft documentation for ThumbBarAddButtons
- [Windows Taskbar Extensions Overview](https://learn.microsoft.com/en-us/windows/win32/shell/taskbar-extensions) - Microsoft overview of taskbar extension features

---

## Version Information

- **Introduced**: Version TBD (Issue #2916)
- **Namespace**: `Krypton.Toolkit`
- **Assembly**: `Krypton.Toolkit.dll`
- **Windows API**: ITaskbarList3 (Windows 7+)
- **Dependencies**: Windows Forms ImageList

---

## Summary

The Taskbar Thumbnail Buttons feature provides a powerful way to add interactive controls directly to the Windows taskbar thumbnail preview. With support for up to 7 buttons, dynamic state management, and full event handling, it enables rich user experiences for media players, hypervisors, communication apps, and any application that benefits from quick access to essential commands.

The feature is fully integrated with the Krypton Toolkit, providing designer support, automatic updates, and comprehensive error handling. Buttons can be enabled, disabled, shown, or hidden dynamically based on application state, making it suitable for a wide range of use cases.

**Key Limitations**:
- Maximum 7 buttons per thumbnail toolbar (Windows limitation)
- Buttons can only be added once per window (cannot add/remove after initial creation)
- Button order is fixed after initial creation
- Buttons only appear when thumbnails are displayed (not in legacy menu mode)
- Buttons may be truncated from right to left if space is limited

For questions or issues, please refer to [GitHub Issue #2916](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2916).
