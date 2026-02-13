# File Operation Dialogs

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [KryptonFileCopyDialog](#kryptonfilecopydialog)
5. [KryptonFileCompressionDialog](#kryptonfilecompressiondialog)
6. [Usage Examples](#usage-examples)
7. [Features](#features)
8. [Implementation Details](#implementation-details)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)
11. [Related Components](#related-components)

---

## Overview

The File Operation Dialogs provide fully-themed replacements for Windows file copy and compression operations. These dialogs offer comprehensive progress indication, speed visualization, and user control features while maintaining full integration with the Krypton Toolkit's theming system.

### Components

- **KryptonFileCopyDialog**: A Krypton-styled dialog for copying files and directories with progress indication
- **KryptonFileCompressionDialog**: A Krypton-styled dialog for compressing files and directories into ZIP archives with progress indication

### Key Features

- **Full Krypton Theming**: All controls use Krypton styling and respect the current global palette
- **Progress Tracking**: Real-time progress indication with percentage, speed, time remaining, and items remaining
- **Speed Visualization**: Visual graph showing transfer/compression speed over time
- **Pause/Resume**: Ability to pause and resume long-running operations
- **File and Directory Support**: Handles both single files and entire directory trees
- **Async Operations**: Non-blocking operations using async/await patterns
- **Cancellation Support**: Full support for operation cancellation
- **Overwrite Handling**: Configurable prompts before overwriting existing files
- **Component-Based**: Inherit from `Component` and implement `IDisposable` for proper resource management
- **Modal Dialogs**: Standard modal dialog behavior with owner window support

### Supported Platforms

- .NET Framework 4.7.2 and later
- .NET 8.0 Windows and later
- All target frameworks supported by Krypton Toolkit

### Requirements

- **Krypton.Utilities**: Both components are located in the `Krypton.Utilities` project
- **System.IO.Compression**: Required for `KryptonFileCompressionDialog` (included in .NET Framework and .NET)

---

## Quick Start

### Basic File Copy

```csharp
using Krypton.Utilities;

var copyDialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\Source\Folder",
    DestinationPath = @"D:\Destination\Folder"
};

var result = copyDialog.ShowDialog(this);
if (result == DialogResult.OK)
{
    MessageBox.Show("Copy completed successfully!");
}
```

### Basic File Compression

```csharp
using System.IO.Compression;
using Krypton.Utilities;

var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\FolderToCompress",
    DestinationPath = @"D:\Archive.zip",
    CompressionLevel = CompressionLevel.Optimal
};

var result = compressionDialog.ShowDialog(this);
if (result == DialogResult.OK)
{
    MessageBox.Show("Compression completed successfully!");
}
```

### Without UI (Silent Operation)

```csharp
// Copy without showing dialog
var copyDialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\Source\File.txt",
    DestinationPath = @"D:\Destination\File.txt",
    ShowUI = false,
    OverwritePrompt = false
};

var result = copyDialog.ShowDialog();
```

---

## API Reference

### Namespace

```csharp
using Krypton.Utilities;
```

### Class Declarations

```csharp
public class KryptonFileCopyDialog : Component, IDisposable
public class KryptonFileCompressionDialog : Component, IDisposable
```

---

## KryptonFileCopyDialog

### Overview

The `KryptonFileCopyDialog` provides a Krypton-styled interface for copying files and directories with comprehensive progress indication. It supports both single file and directory tree operations.

### Properties

#### SourcePath

```csharp
[Category(@"Behavior")]
[DefaultValue(null)]
[Description(@"The source path (file or directory) to copy from.")]
public string? SourcePath { get; set; }
```

**Description**: Gets or sets the source path (file or directory) to copy from.

**Remarks**: 
- Must be set before calling `ShowDialog()`
- Can be a file path or directory path
- Throws `InvalidOperationException` if null or empty when showing dialog

**Example**:
```csharp
copyDialog.SourcePath = @"C:\MyFolder";
copyDialog.SourcePath = @"C:\MyFile.txt";
```

#### DestinationPath

```csharp
[Category(@"Behavior")]
[DefaultValue(null)]
[Description(@"The destination path (file or directory) to copy to.")]
public string? DestinationPath { get; set; }
```

**Description**: Gets or sets the destination path (file or directory) to copy to.

**Remarks**:
- Must be set before calling `ShowDialog()`
- For file copies, specifies the destination file path
- For directory copies, specifies the destination directory path
- Throws `InvalidOperationException` if null or empty when showing dialog

**Example**:
```csharp
copyDialog.DestinationPath = @"D:\Backup\MyFolder";
copyDialog.DestinationPath = @"D:\Backup\MyFile.txt";
```

#### ShowUI

```csharp
[Category(@"Behavior")]
[DefaultValue(true)]
[Description(@"Indicates whether to show the progress dialog UI.")]
public bool ShowUI { get; set; }
```

**Description**: Gets or sets a value indicating whether to show the progress dialog UI.

**Remarks**:
- When `true` (default), shows the progress dialog with all visual feedback
- When `false`, performs the copy operation silently without UI
- Silent operations return `DialogResult.OK` on success, `DialogResult.Cancel` on failure

**Example**:
```csharp
copyDialog.ShowUI = false; // Perform silent copy
```

#### OverwritePrompt

```csharp
[Category(@"Behavior")]
[DefaultValue(true)]
[Description(@"Indicates whether to prompt before overwriting existing files.")]
public bool OverwritePrompt { get; set; }
```

**Description**: Gets or sets a value indicating whether to prompt before overwriting existing files.

**Remarks**:
- When `true` (default), prompts user before overwriting existing files
- When `false`, automatically overwrites existing files without prompting
- Only applies when `ShowUI` is `true`

**Example**:
```csharp
copyDialog.OverwritePrompt = false; // Auto-overwrite without prompting
```

### Methods

#### ShowDialog

```csharp
public DialogResult ShowDialog(IWin32Window? owner = null)
```

**Description**: Shows the file copy dialog and performs the copy operation.

**Parameters**:
- `owner` (optional): The owner window for the dialog. If `null`, the dialog has no owner.

**Returns**: 
- `DialogResult.OK`: Copy operation completed successfully
- `DialogResult.Cancel`: Operation was cancelled or failed

**Exceptions**:
- `InvalidOperationException`: Thrown if `SourcePath` or `DestinationPath` is null or empty

**Remarks**:
- This method is blocking and returns when the operation completes or is cancelled
- The dialog is modal and blocks user interaction with other windows
- Progress is updated in real-time during the operation

**Example**:
```csharp
var result = copyDialog.ShowDialog(this);
if (result == DialogResult.OK)
{
    // Handle success
}
```

### Usage Examples

#### Example 1: Copy Single File

```csharp
var copyDialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\Documents\Report.pdf",
    DestinationPath = @"D:\Backup\Report.pdf",
    OverwritePrompt = true
};

if (copyDialog.ShowDialog(this) == DialogResult.OK)
{
    MessageBox.Show("File copied successfully!");
}
```

#### Example 2: Copy Directory Tree

```csharp
var copyDialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\MyProject",
    DestinationPath = @"D:\Backups\MyProject_2026",
    OverwritePrompt = false
};

copyDialog.ShowDialog(this);
```

#### Example 3: Silent Copy Operation

```csharp
var copyDialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\Data\file.dat",
    DestinationPath = @"D:\Backup\file.dat",
    ShowUI = false,
    OverwritePrompt = false
};

try
{
    var result = copyDialog.ShowDialog();
    if (result == DialogResult.OK)
    {
        // Copy completed
    }
}
catch (Exception ex)
{
    // Handle error
}
```

#### Example 4: Copy with Error Handling

```csharp
try
{
    var copyDialog = new KryptonFileCopyDialog
    {
        SourcePath = sourcePath,
        DestinationPath = destPath
    };

    var result = copyDialog.ShowDialog(this);
    
    if (result == DialogResult.OK)
    {
        Log.Info("Copy operation completed successfully");
    }
    else
    {
        Log.Warning("Copy operation was cancelled");
    }
}
catch (InvalidOperationException ex)
{
    MessageBox.Show($"Invalid configuration: {ex.Message}", "Error", 
        MessageBoxButtons.OK, MessageBoxIcon.Error);
}
catch (Exception ex)
{
    MessageBox.Show($"Copy failed: {ex.Message}", "Error", 
        MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

---

## KryptonFileCompressionDialog

### Overview

The `KryptonFileCompressionDialog` provides a Krypton-styled interface for compressing files and directories into ZIP archives with comprehensive progress indication. It supports both single file and directory tree compression.

### Properties

#### SourcePath

```csharp
[Category(@"Behavior")]
[DefaultValue(null)]
[Description(@"The source path (file or directory) to compress.")]
public string? SourcePath { get; set; }
```

**Description**: Gets or sets the source path (file or directory) to compress.

**Remarks**:
- Must be set before calling `ShowDialog()`
- Can be a file path or directory path
- Throws `InvalidOperationException` if null or empty when showing dialog

**Example**:
```csharp
compressionDialog.SourcePath = @"C:\MyFolder";
compressionDialog.SourcePath = @"C:\MyFile.txt";
```

#### DestinationPath

```csharp
[Category(@"Behavior")]
[DefaultValue(null)]
[Description(@"The destination ZIP file path.")]
public string? DestinationPath { get; set; }
```

**Description**: Gets or sets the destination ZIP file path.

**Remarks**:
- Must be set before calling `ShowDialog()`
- Automatically appends `.zip` extension if not present
- Throws `InvalidOperationException` if null or empty when showing dialog
- If the file already exists, user is prompted to replace it (when `ShowUI` is `true`)

**Example**:
```csharp
compressionDialog.DestinationPath = @"D:\Archive.zip";
compressionDialog.DestinationPath = @"D:\Archive"; // Will become "Archive.zip"
```

#### ShowUI

```csharp
[Category(@"Behavior")]
[DefaultValue(true)]
[Description(@"Indicates whether to show the progress dialog UI.")]
public bool ShowUI { get; set; }
```

**Description**: Gets or sets a value indicating whether to show the progress dialog UI.

**Remarks**:
- When `true` (default), shows the progress dialog with all visual feedback
- When `false`, performs the compression operation silently without UI
- Silent operations return `DialogResult.OK` on success, `DialogResult.Cancel` on failure

**Example**:
```csharp
compressionDialog.ShowUI = false; // Perform silent compression
```

#### CompressionLevel

```csharp
[Category(@"Behavior")]
[DefaultValue(CompressionLevel.Optimal)]
[Description(@"The compression level to use (Fastest, Optimal, or NoCompression).")]
public CompressionLevel CompressionLevel { get; set; }
```

**Description**: Gets or sets the compression level to use.

**Remarks**:
- `CompressionLevel.Fastest`: Fastest compression, larger file size
- `CompressionLevel.Optimal`: Balanced compression (default)
- `CompressionLevel.NoCompression`: No compression, fastest but largest file size
- Uses `System.IO.Compression.CompressionLevel` enum

**Example**:
```csharp
compressionDialog.CompressionLevel = CompressionLevel.Fastest; // Speed over size
compressionDialog.CompressionLevel = CompressionLevel.Optimal; // Balanced (default)
compressionDialog.CompressionLevel = CompressionLevel.NoCompression; // No compression
```

#### IncludeBaseDirectory

```csharp
[Category(@"Behavior")]
[DefaultValue(false)]
[Description(@"Indicates whether to include the base directory in the archive.")]
public bool IncludeBaseDirectory { get; set; }
```

**Description**: Gets or sets a value indicating whether to include the base directory in the archive.

**Remarks**:
- When `false` (default), only the contents of the directory are included
- When `true`, the directory itself is included as the root folder in the archive
- Only applies when compressing directories

**Example**:
```csharp
// Source: C:\MyProject
// IncludeBaseDirectory = false: Archive contains files directly
// IncludeBaseDirectory = true: Archive contains "MyProject\" folder with files inside
compressionDialog.IncludeBaseDirectory = true;
```

### Methods

#### ShowDialog

```csharp
public DialogResult ShowDialog(IWin32Window? owner = null)
```

**Description**: Shows the file compression dialog and performs the compression operation.

**Parameters**:
- `owner` (optional): The owner window for the dialog. If `null`, the dialog has no owner.

**Returns**:
- `DialogResult.OK`: Compression operation completed successfully
- `DialogResult.Cancel`: Operation was cancelled or failed

**Exceptions**:
- `InvalidOperationException`: Thrown if `SourcePath` or `DestinationPath` is null or empty

**Remarks**:
- This method is blocking and returns when the operation completes or is cancelled
- The dialog is modal and blocks user interaction with other windows
- Progress is updated in real-time during the operation
- Automatically appends `.zip` extension to destination path if not present

**Example**:
```csharp
var result = compressionDialog.ShowDialog(this);
if (result == DialogResult.OK)
{
    // Handle success
}
```

### Usage Examples

#### Example 1: Compress Single File

```csharp
var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\Documents\Report.pdf",
    DestinationPath = @"D:\Backup\Report.zip",
    CompressionLevel = CompressionLevel.Optimal
};

if (compressionDialog.ShowDialog(this) == DialogResult.OK)
{
    MessageBox.Show("File compressed successfully!");
}
```

#### Example 2: Compress Directory Tree

```csharp
var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\MyProject",
    DestinationPath = @"D:\Backups\MyProject.zip",
    CompressionLevel = CompressionLevel.Optimal,
    IncludeBaseDirectory = false
};

compressionDialog.ShowDialog(this);
```

#### Example 3: Fast Compression (Speed Over Size)

```csharp
var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\LargeFolder",
    DestinationPath = @"D:\QuickBackup.zip",
    CompressionLevel = CompressionLevel.Fastest, // Faster compression
    ShowUI = true
};

compressionDialog.ShowDialog(this);
```

#### Example 4: Silent Compression

```csharp
var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\Data",
    DestinationPath = @"D:\Backup.zip",
    CompressionLevel = CompressionLevel.Optimal,
    ShowUI = false
};

try
{
    var result = compressionDialog.ShowDialog();
    if (result == DialogResult.OK)
    {
        // Compression completed
    }
}
catch (Exception ex)
{
    // Handle error
}
```

#### Example 5: Compress with Base Directory

```csharp
// This will create an archive where the root folder is "MyProject"
var compressionDialog = new KryptonFileCompressionDialog
{
    SourcePath = @"C:\MyProject",
    DestinationPath = @"D:\Archive.zip",
    IncludeBaseDirectory = true // Include "MyProject" folder in archive
};

compressionDialog.ShowDialog(this);
```

#### Example 6: Batch Compression

```csharp
var foldersToCompress = new[]
{
    @"C:\Project1",
    @"C:\Project2",
    @"C:\Project3"
};

foreach (var folder in foldersToCompress)
{
    var compressionDialog = new KryptonFileCompressionDialog
    {
        SourcePath = folder,
        DestinationPath = Path.Combine(@"D:\Backups", Path.GetFileName(folder) + ".zip"),
        CompressionLevel = CompressionLevel.Optimal
    };

    if (compressionDialog.ShowDialog(this) != DialogResult.OK)
    {
        MessageBox.Show($"Failed to compress {folder}");
        break; // Stop on first failure
    }
}
```

---

## Features

### Progress Indication

Both dialogs provide comprehensive progress indication:

- **Percentage Complete**: Shows overall completion percentage
- **Progress Bar**: Visual progress bar with Krypton theming
- **Current File**: Displays the name of the file currently being processed
- **Items Remaining**: Shows count and total size of remaining items
- **Time Remaining**: Estimated time to completion based on current speed
- **Transfer Speed**: Current processing speed in bytes/second
- **Speed Graph**: Visual graph showing speed over time

### Speed Visualization

Both dialogs include a speed graph that visualizes transfer/compression speed over time:

- **Real-time Updates**: Graph updates continuously during operation
- **Historical Data**: Maintains a rolling history of speed measurements
- **Visual Feedback**: Color-coded graph with filled area under the line
- **Automatic Scaling**: Graph automatically scales to show speed variations

### Pause/Resume Functionality

Both dialogs support pausing and resuming operations:

- **Pause Button**: Click to pause the current operation
- **Resume Button**: Click to resume a paused operation
- **State Preservation**: Operation state is preserved when paused
- **Cancellation**: Can still cancel while paused

### Cancellation Support

Both dialogs support operation cancellation:

- **Cancel Button**: Always available to cancel the operation
- **Clean Shutdown**: Operations cleanly shut down when cancelled
- **Resource Cleanup**: All resources are properly disposed on cancellation
- **Dialog Result**: Returns `DialogResult.Cancel` when cancelled

### Details Panel

Both dialogs include an expandable details panel:

- **Toggle Button**: "More details" / "Fewer details" button
- **File Information**: Current file name being processed
- **Time Information**: Estimated time remaining
- **Items Information**: Remaining items count and size
- **Speed Information**: Current processing speed

### Overwrite Handling

**KryptonFileCopyDialog**:
- Prompts user before overwriting existing files (when `OverwritePrompt` is `true`)
- User can choose to replace, skip, or cancel
- When `OverwritePrompt` is `false`, automatically overwrites

**KryptonFileCompressionDialog**:
- Prompts user before overwriting existing ZIP files
- User can choose to replace or cancel
- Only applies when `ShowUI` is `true`

### File and Directory Support

Both dialogs support:

- **Single Files**: Copy or compress individual files
- **Directory Trees**: Recursively process entire directory structures
- **Mixed Operations**: Automatically detects file vs. directory
- **Path Validation**: Validates paths before starting operations

---

## Implementation Details

### Architecture

Both components follow a component-based architecture:

1. **Public Component Class**: `KryptonFileCopyDialog` / `KryptonFileCompressionDialog` (inherits from `Component`)
   - Provides the public API
   - Manages dialog lifecycle
   - Exposes properties and methods

2. **Internal Form Class**: `VisualFileCopyDialogForm` / `VisualFileCompressionDialogForm` (inherits from `KryptonForm`)
   - Contains the actual UI
   - Manages progress tracking
   - Handles user interactions
   - Located in `Controls Visuals` directory

### Progress Tracking

Progress is tracked using:

- **File List Building**: All files are enumerated before starting the operation
- **Total Size Calculation**: Total bytes are calculated upfront
- **Incremental Updates**: Progress is updated as each file is processed
- **Thread Safety**: All UI updates are marshalled to the UI thread

### Async Operations

Both dialogs use async/await patterns:

- **Non-blocking**: Operations don't block the UI thread
- **Cancellation Tokens**: Support for operation cancellation
- **Progress Updates**: Real-time progress updates during operations
- **Error Handling**: Comprehensive error handling and reporting

### Resource Management

Both components properly implement `IDisposable`:

- **Form Disposal**: Internal form instances are disposed when component is disposed
- **Stream Cleanup**: File streams are properly closed and disposed
- **Memory Management**: Resources are cleaned up properly

### Error Handling

Both dialogs include comprehensive error handling:

- **Path Validation**: Validates source and destination paths
- **File System Errors**: Handles file system errors gracefully
- **User Feedback**: Shows error messages to users
- **Exception Handling**: Catches and handles exceptions appropriately

---

## Best Practices

### 1. Always Set Required Properties Before Showing Dialog

```csharp
// ✅ Good
var dialog = new KryptonFileCopyDialog
{
    SourcePath = sourcePath,
    DestinationPath = destPath
};
dialog.ShowDialog();

// ❌ Bad - Will throw InvalidOperationException
var dialog = new KryptonFileCopyDialog();
dialog.ShowDialog(); // SourcePath and DestinationPath not set!
```

### 2. Handle Dialog Results Appropriately

```csharp
var result = copyDialog.ShowDialog(this);
if (result == DialogResult.OK)
{
    // Operation completed successfully
    Log.Info("Copy operation completed");
}
else if (result == DialogResult.Cancel)
{
    // Operation was cancelled or failed
    Log.Warning("Copy operation was cancelled");
}
```

### 3. Use Try-Catch for Error Handling

```csharp
try
{
    var dialog = new KryptonFileCopyDialog
    {
        SourcePath = sourcePath,
        DestinationPath = destPath
    };
    
    var result = dialog.ShowDialog(this);
    // Handle result
}
catch (InvalidOperationException ex)
{
    // Handle configuration errors
    MessageBox.Show($"Configuration error: {ex.Message}");
}
catch (Exception ex)
{
    // Handle unexpected errors
    MessageBox.Show($"Error: {ex.Message}");
}
```

### 4. Choose Appropriate Compression Levels

```csharp
// For speed-critical operations
compressionDialog.CompressionLevel = CompressionLevel.Fastest;

// For size-critical operations (default)
compressionDialog.CompressionLevel = CompressionLevel.Optimal;

// For no compression (fastest, largest)
compressionDialog.CompressionLevel = CompressionLevel.NoCompression;
```

### 5. Use Silent Operations for Background Tasks

```csharp
// For background operations where UI is not needed
var dialog = new KryptonFileCopyDialog
{
    SourcePath = sourcePath,
    DestinationPath = destPath,
    ShowUI = false // No UI, faster execution
};

var result = dialog.ShowDialog();
```

### 6. Validate Paths Before Operations

```csharp
if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
{
    MessageBox.Show("Source path does not exist!");
    return;
}

var dialog = new KryptonFileCopyDialog
{
    SourcePath = sourcePath,
    DestinationPath = destPath
};
```

### 7. Dispose Components Properly

```csharp
using (var dialog = new KryptonFileCopyDialog
{
    SourcePath = sourcePath,
    DestinationPath = destPath
})
{
    dialog.ShowDialog(this);
} // Automatically disposed
```

### 8. Consider User Experience

```csharp
// Show UI for user-initiated operations
copyDialog.ShowUI = true;
copyDialog.OverwritePrompt = true; // Let user decide

// Silent for automated/background operations
copyDialog.ShowUI = false;
copyDialog.OverwritePrompt = false; // Auto-overwrite
```

---

## Troubleshooting

### Common Issues

#### Issue: InvalidOperationException when showing dialog

**Symptoms**: Exception thrown: "SourcePath must be set before showing the dialog."

**Cause**: `SourcePath` or `DestinationPath` property is null or empty.

**Solution**: Always set both `SourcePath` and `DestinationPath` before calling `ShowDialog()`.

```csharp
// Fix
var dialog = new KryptonFileCopyDialog
{
    SourcePath = @"C:\Source", // Must be set
    DestinationPath = @"D:\Dest" // Must be set
};
dialog.ShowDialog();
```

#### Issue: Dialog shows but progress doesn't update

**Symptoms**: Dialog appears but progress bar stays at 0%.

**Cause**: Source path doesn't exist or is inaccessible.

**Solution**: Validate paths before showing dialog.

```csharp
if (!Directory.Exists(sourcePath))
{
    MessageBox.Show("Source path does not exist!");
    return;
}

var dialog = new KryptonFileCopyDialog
{
    SourcePath = sourcePath,
    DestinationPath = destPath
};
dialog.ShowDialog();
```

#### Issue: Compression fails silently

**Symptoms**: Dialog returns `DialogResult.Cancel` but no error message.

**Cause**: Destination path is invalid or inaccessible.

**Solution**: Check destination path permissions and validity.

```csharp
var destDir = Path.GetDirectoryName(destinationPath);
if (!Directory.Exists(destDir))
{
    Directory.CreateDirectory(destDir);
}

var dialog = new KryptonFileCompressionDialog
{
    SourcePath = sourcePath,
    DestinationPath = destinationPath
};
dialog.ShowDialog();
```

#### Issue: Operation is very slow

**Symptoms**: Copy or compression takes much longer than expected.

**Possible Causes**:
1. Large number of small files
2. Network drive as destination
3. Antivirus scanning files
4. Disk fragmentation

**Solutions**:
- Use `CompressionLevel.Fastest` for compression operations
- Consider using `ShowUI = false` for better performance
- Check disk/network performance
- Verify antivirus exclusions

#### Issue: "Access Denied" errors

**Symptoms**: Operation fails with access denied errors.

**Cause**: Insufficient permissions or file is locked.

**Solution**: 
- Run application with appropriate permissions
- Ensure files aren't locked by other processes
- Check file/folder permissions

#### Issue: Memory issues with large operations

**Symptoms**: Out of memory exceptions or high memory usage.

**Cause**: Building file list for very large directory trees.

**Solution**: 
- Consider processing in batches
- Use silent mode (`ShowUI = false`) to reduce memory overhead
- Ensure sufficient available memory

### Performance Considerations

1. **Large Directory Trees**: Building the file list for very large directories can take time. Consider showing a "Preparing..." message.

2. **Network Drives**: Operations on network drives may be slower. Consider showing appropriate messaging.

3. **Compression Level**: `CompressionLevel.Fastest` is significantly faster than `CompressionLevel.Optimal` but produces larger files.

4. **UI Updates**: Frequent UI updates can impact performance. The dialogs throttle updates appropriately.

### Debugging Tips

1. **Enable Logging**: Add logging to track operation progress.

2. **Check Paths**: Verify source and destination paths are correct and accessible.

3. **Test with Small Sets**: Test with small file sets first before large operations.

4. **Monitor Resources**: Monitor memory and CPU usage during operations.

---

## Related Components

### Krypton Components

- **KryptonProgressBar**: The progress bar control used in both dialogs
- **KryptonForm**: Base form class for both dialogs
- **KryptonButton**: Buttons used in both dialogs
- **KryptonWrapLabel**: Labels used for displaying information

### .NET Framework Components

- **System.IO.File**: Used for file operations
- **System.IO.Directory**: Used for directory operations
- **System.IO.Compression.ZipFile**: Used for compression operations
- **System.IO.Compression.CompressionLevel**: Enumeration for compression levels

### Similar Components

- **KryptonPrintPreviewDialog**: Another Krypton dialog with progress indication
- **KryptonTaskDialog**: Task dialog component with progress bar support

---

## Additional Resources

### Code Examples

See the `TestForm` project for example usage of both dialogs.

### API Documentation

Full API documentation is available in the XML comments of both component classes.

### Source Code Location

- **KryptonFileCopyDialog**: `Source/Krypton Components/Krypton.Utilities/Components/KryptonFileCopyDialog/`
- **KryptonFileCompressionDialog**: `Source/Krypton Components/Krypton.Utilities/Components/KryptonFileCompressionDialog/`

---

## Version History

### Version 1.0 (Initial Release)

- Initial implementation of `KryptonFileCopyDialog`
- Initial implementation of `KryptonFileCompressionDialog`
- Full progress indication support
- Speed visualization
- Pause/Resume functionality
- Comprehensive error handling

---

## License

These components are part of the Krypton Toolkit and are subject to the same license terms as the rest of the toolkit.

---

## Support

For issues, questions, or contributions, please refer to the main Krypton Toolkit repository and issue tracker.
