# Jump List Feature

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

The Jump List feature allows you to create and manage custom jump lists for your application's taskbar button. Jump lists provide quick access to frequently used files, recent documents, and common tasks directly from the Windows taskbar.

### Key Features

- **Windows 7+ Support**: Uses the native Windows ICustomDestinationList API
- **User Tasks**: Add custom tasks that appear at the bottom of the jump list
- **Custom Categories**: Create custom categories with your own items
- **Known Categories**: Support for Frequent and Recent categories managed by Windows
- **Shell Links**: Full support for shell links with icons, arguments, and descriptions
- **Developer Controlled**: Fully programmable via properties
- **Designer Support**: Full Visual Studio designer integration with expandable properties
- **Automatic Updates**: Jump list updates automatically when properties change
- **Error Handling**: Gracefully handles unsupported platforms and errors

### Use Cases

- **Recent Files**: Display recently opened files for quick access
- **Frequent Files**: Show frequently accessed files
- **Quick Tasks**: Provide shortcuts to common application tasks
- **Document Management**: Organize documents by category
- **Application Shortcuts**: Quick access to application features
- **File Associations**: Quick access to files associated with your application

### Requirements

- **Windows Version**: Windows 7 or later (ICustomDestinationList API requirement)
- **Application ID**: Must set a unique application ID (`AppId` property)
- **Form Handle**: Form must have a valid window handle
- **Taskbar Visibility**: Form must be visible and shown in taskbar (`ShowInTaskbar = true`)

---

## Quick Start

### Basic Usage

```csharp
// Create a form with jump list
var form = new KryptonForm();
form.Text = "My Application";

// Set application ID (required)
form.JumpList.AppId = "MyCompany.MyApplication";

// Add a user task
var task = new JumpListItem
{
    Title = "Open Settings",
    Path = Application.ExecutablePath,
    Arguments = "/settings",
    Description = "Open application settings"
};
form.JumpList.UserTasks.Add(task);
```

### Designer Usage

1. Select a `KryptonForm` in the designer
2. In the Properties window, find the `JumpList` property
3. Expand the `JumpList` property (it appears as an expandable object)
4. Set the `AppId` property (required)
5. Add items to `UserTasks` collection
6. Configure categories and known categories as needed

---

## API Reference

### Namespace

```csharp
using Krypton.Toolkit;
```

---

## Classes

### JumpListItem

Represents a single item in a jump list (task or destination). This is a simple data class used to define jump list entries.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Title` | `string` | `""` | The display title of the jump list item. This is the text shown in the jump list. |
| `Path` | `string` | `""` | The path to the executable or file. For tasks, this is typically the application executable path. For destinations, this is the file path. |
| `Arguments` | `string` | `""` | Command line arguments to pass when the item is clicked. Use this to pass parameters to your application or to open files with specific options. |
| `WorkingDirectory` | `string` | `""` | The working directory for the executable. If empty, the directory containing the executable is used. |
| `IconPath` | `string` | `""` | The path to the icon file. Can be an executable, DLL, or ICO file. If empty, the default icon is used. |
| `IconIndex` | `int` | `0` | The index of the icon within the icon file. Use 0 for single-icon files or to specify which icon to use from a multi-icon resource. |
| `Description` | `string` | `""` | Description/tooltip text shown when hovering over the item. Provides additional context about what the item does. |

#### Usage Notes

- **Path is Required**: The `Path` property must be set for the item to function
- **Title Display**: The `Title` property is what users see in the jump list
- **Icon Support**: Icons can be loaded from executables, DLLs, or ICO files
- **Arguments**: Use arguments to pass command-line parameters or file paths

---

### JumpListValues

Storage class for jump list value information. This class uses `ExpandableObjectConverter` for designer support.

#### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `AppId` | `string` | `""` | Application ID used to identify the application's jump list. **Required** - must be set before jump list will work. Should be a unique identifier like "CompanyName.ApplicationName". |
| `UserTasks` | `List<JumpListItem>` | Empty | List of user tasks displayed in the jump list. Tasks appear at the bottom of the jump list and are always visible. |
| `Categories` | `Dictionary<string, List<JumpListItem>>` | Empty | Dictionary of custom categories and their items. Keys are category names, values are lists of items in that category. |
| `ShowFrequentCategory` | `bool` | `false` | Whether to show the Frequent category in the jump list. This category is managed by Windows and shows frequently accessed files. |
| `ShowRecentCategory` | `bool` | `false` | Whether to show the Recent category in the jump list. This category is managed by Windows and shows recently accessed files. |

#### Methods

##### `AddCategory(string categoryName, List<JumpListItem> items)`

Adds a custom category with items to the jump list.

```csharp
var recentFiles = new List<JumpListItem>
{
    new JumpListItem { Title = "Document1.txt", Path = @"C:\Documents\Document1.txt" },
    new JumpListItem { Title = "Document2.txt", Path = @"C:\Documents\Document2.txt" }
};
jumpList.AddCategory("Recent Files", recentFiles);
```

**Parameters**:
- `categoryName`: Name of the category (displayed as section header)
- `items`: List of items to include in the category

**Notes**:
- Category names must be unique
- Adding a category with an existing name replaces the previous category
- Empty category names are ignored

##### `RemoveCategory(string categoryName)`

Removes a custom category from the jump list.

```csharp
jumpList.RemoveCategory("Recent Files");
```

**Parameters**:
- `categoryName`: Name of the category to remove

**Returns**: `true` if category was removed, `false` if category didn't exist

##### `ClearCategories()`

Clears all custom categories from the jump list.

```csharp
jumpList.ClearCategories();
```

##### `CopyFrom(JumpListValues source)`

Copies all jump list values from another instance.

```csharp
var source = new JumpListValues(needPaint);
source.AppId = "MyApp";
source.UserTasks.Add(new JumpListItem { Title = "Task", Path = "app.exe" });

var target = new JumpListValues(needPaint);
target.CopyFrom(source); // Copies all properties
```

##### `Reset()`

Resets all values to their default state.

```csharp
jumpList.Reset(); // Clears AppId, UserTasks, Categories, and resets flags
```

#### Serialization Methods

- `ShouldSerializeAppId()` - Returns `true` if AppId is not empty
- `ShouldSerializeUserTasks()` - Returns `true` if UserTasks has items
- `ShouldSerializeShowFrequentCategory()` - Returns `true` if ShowFrequentCategory is true
- `ShouldSerializeShowRecentCategory()` - Returns `true` if ShowRecentCategory is true

#### Reset Methods

- `ResetAppId()` - Sets AppId to empty string
- `ResetUserTasks()` - Clears UserTasks collection
- `ResetShowFrequentCategory()` - Sets ShowFrequentCategory to false
- `ResetShowRecentCategory()` - Sets ShowRecentCategory to false

---

### KryptonForm Properties

#### `JumpList`

Gets access to the jump list values.

```csharp
[Category(@"Visuals")]
[Description(@"Jump list configuration for the taskbar button.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
public JumpListValues JumpList { get; }
```

**Usage**:
```csharp
form.JumpList.AppId = "MyCompany.MyApplication";
form.JumpList.UserTasks.Add(new JumpListItem { /* ... */ });
```

#### `ResetJumpList()`

Resets the JumpList property to its default value.

```csharp
public void ResetJumpList() => JumpList.Reset();
```

#### `ShouldSerializeJumpList()`

Indicates whether the JumpList property should be serialized.

```csharp
public bool ShouldSerializeJumpList() => !JumpList.IsDefault;
```

---

## Usage Examples

### Example 1: Basic User Tasks

```csharp
var form = new KryptonForm();
form.Text = "My Application";

// Set application ID (required)
form.JumpList.AppId = "MyCompany.MyApplication";

// Add user tasks
form.JumpList.UserTasks.Add(new JumpListItem
{
    Title = "New Document",
    Path = Application.ExecutablePath,
    Arguments = "/new",
    Description = "Create a new document"
});

form.JumpList.UserTasks.Add(new JumpListItem
{
    Title = "Open Settings",
    Path = Application.ExecutablePath,
    Arguments = "/settings",
    Description = "Open application settings"
});
```

### Example 2: Recent Files Category

```csharp
public class RecentFilesManager
{
    private readonly KryptonForm _form;
    private readonly List<string> _recentFiles = new();

    public RecentFilesManager(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
    }

    public void AddRecentFile(string filePath)
    {
        // Add to internal list (keep last 10)
        _recentFiles.Remove(filePath);
        _recentFiles.Insert(0, filePath);
        if (_recentFiles.Count > 10)
        {
            _recentFiles.RemoveAt(10);
        }

        UpdateJumpList();
    }

    private void UpdateJumpList()
    {
        var items = _recentFiles.Select(filePath => new JumpListItem
        {
            Title = Path.GetFileName(filePath),
            Path = filePath,
            Description = filePath
        }).ToList();

        _form.JumpList.AddCategory("Recent Files", items);
    }
}
```

### Example 3: Custom Categories

```csharp
public class DocumentManager
{
    private readonly KryptonForm _form;

    public DocumentManager(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
        InitializeJumpList();
    }

    private void InitializeJumpList()
    {
        // Add user tasks
        _form.JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "New Document",
            Path = Application.ExecutablePath,
            Arguments = "/new",
            IconPath = Application.ExecutablePath,
            Description = "Create a new document"
        });

        // Add custom categories
        var templates = new List<JumpListItem>
        {
            new JumpListItem
            {
                Title = "Invoice Template",
                Path = @"C:\Templates\Invoice.dotx",
                Description = "Create new invoice"
            },
            new JumpListItem
            {
                Title = "Letter Template",
                Path = @"C:\Templates\Letter.dotx",
                Description = "Create new letter"
            }
        };
        _form.JumpList.AddCategory("Templates", templates);

        var quickAccess = new List<JumpListItem>
        {
            new JumpListItem
            {
                Title = "My Documents",
                Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Description = "Open My Documents folder"
            },
            new JumpListItem
            {
                Title = "Desktop",
                Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Description = "Open Desktop folder"
            }
        };
        _form.JumpList.AddCategory("Quick Access", quickAccess);
    }
}
```

### Example 4: Known Categories (Frequent/Recent)

```csharp
public class JumpListConfigurator
{
    private readonly KryptonForm _form;

    public JumpListConfigurator(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
    }

    public void EnableKnownCategories()
    {
        // Show Windows-managed Frequent category
        _form.JumpList.ShowFrequentCategory = true;

        // Show Windows-managed Recent category
        _form.JumpList.ShowRecentCategory = true;
    }

    public void DisableKnownCategories()
    {
        _form.JumpList.ShowFrequentCategory = false;
        _form.JumpList.ShowRecentCategory = false;
    }
}
```

### Example 5: Dynamic Jump List Updates

```csharp
public class DynamicJumpListManager
{
    private readonly KryptonForm _form;

    public DynamicJumpListManager(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
    }

    public void UpdateRecentProjects(List<string> projectPaths)
    {
        var items = projectPaths.Select(path => new JumpListItem
        {
            Title = Path.GetFileNameWithoutExtension(path),
            Path = Application.ExecutablePath,
            Arguments = $"\"{path}\"",
            IconPath = Application.ExecutablePath,
            Description = path
        }).ToList();

        _form.JumpList.AddCategory("Recent Projects", items);
    }

    public void UpdateFavoriteFiles(List<string> filePaths)
    {
        var items = filePaths.Select(path => new JumpListItem
        {
            Title = Path.GetFileName(path),
            Path = path,
            Description = $"Open {Path.GetFileName(path)}"
        }).ToList();

        _form.JumpList.AddCategory("Favorites", items);
    }

    public void ClearAllCategories()
    {
        _form.JumpList.ClearCategories();
    }
}
```

### Example 6: Jump List with Icons

```csharp
public class IconJumpListBuilder
{
    private readonly KryptonForm _form;

    public IconJumpListBuilder(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
        BuildJumpList();
    }

    private void BuildJumpList()
    {
        // Task with custom icon
        _form.JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open Calculator",
            Path = "calc.exe",
            IconPath = "shell32.dll",
            IconIndex = 137, // Calculator icon index in shell32.dll
            Description = "Open Windows Calculator"
        });

        // Task with application icon
        _form.JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open Application",
            Path = Application.ExecutablePath,
            IconPath = Application.ExecutablePath,
            IconIndex = 0,
            Description = "Launch application"
        });

        // File with associated icon
        _form.JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "ReadMe.txt",
            Path = @"C:\ReadMe.txt",
            Description = "Open ReadMe file"
            // Icon will be determined by file association
        });
    }
}
```

### Example 7: Command-Line Argument Handling

```csharp
public partial class MainForm : KryptonForm
{
    public MainForm()
    {
        InitializeComponent();
        InitializeJumpList();
        HandleCommandLineArguments();
    }

    private void InitializeJumpList()
    {
        JumpList.AppId = "MyCompany.MyApplication";

        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "New Document",
            Path = Application.ExecutablePath,
            Arguments = "/new",
            Description = "Create a new document"
        });

        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open File",
            Path = Application.ExecutablePath,
            Arguments = "/open",
            Description = "Open an existing file"
        });
    }

    private void HandleCommandLineArguments()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            switch (args[1])
            {
                case "/new":
                    CreateNewDocument();
                    break;
                case "/open":
                    OpenFile();
                    break;
                default:
                    // Assume it's a file path
                    if (File.Exists(args[1]))
                    {
                        OpenFile(args[1]);
                    }
                    break;
            }
        }
    }

    private void CreateNewDocument() { /* ... */ }
    private void OpenFile() { /* ... */ }
    private void OpenFile(string path) { /* ... */ }
}
```

### Example 8: Jump List Persistence

```csharp
public class JumpListPersistence
{
    private readonly KryptonForm _form;
    private readonly string _configPath;

    public JumpListPersistence(KryptonForm form)
    {
        _form = form;
        _form.JumpList.AppId = "MyCompany.MyApplication";
        _configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MyApplication",
            "jumplist.json"
        );
    }

    public void SaveJumpList()
    {
        var config = new
        {
            AppId = _form.JumpList.AppId,
            UserTasks = _form.JumpList.UserTasks.Select(t => new
            {
                t.Title,
                t.Path,
                t.Arguments,
                t.Description,
                t.IconPath,
                t.IconIndex
            }).ToList(),
            ShowFrequentCategory = _form.JumpList.ShowFrequentCategory,
            ShowRecentCategory = _form.JumpList.ShowRecentCategory
        };

        Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);
        File.WriteAllText(_configPath, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
    }

    public void LoadJumpList()
    {
        if (!File.Exists(_configPath))
            return;

        var json = File.ReadAllText(_configPath);
        var config = JsonSerializer.Deserialize<dynamic>(json);

        _form.JumpList.AppId = config.AppId;
        _form.JumpList.ShowFrequentCategory = config.ShowFrequentCategory;
        _form.JumpList.ShowRecentCategory = config.ShowRecentCategory;

        _form.JumpList.UserTasks.Clear();
        foreach (var task in config.UserTasks)
        {
            _form.JumpList.UserTasks.Add(new JumpListItem
            {
                Title = task.Title,
                Path = task.Path,
                Arguments = task.Arguments,
                Description = task.Description,
                IconPath = task.IconPath,
                IconIndex = task.IconIndex
            });
        }
    }
}
```

---

## Designer Support

### Property Grid Integration

The `JumpListValues` class uses `ExpandableObjectConverter`, which means in the Visual Studio designer:

1. The `JumpList` property appears as an expandable node in the Properties window
2. All jump list properties are grouped under this node
3. Properties can be edited directly in the designer
4. Changes are serialized to the `.Designer.cs` file

### Designer Code Generation

When you configure jump lists in the designer, code similar to this is generated:

```csharp
// 
// kryptonForm1
// 
this.kryptonForm1.JumpList.AppId = "MyCompany.MyApplication";
this.kryptonForm1.JumpList.ShowFrequentCategory = true;
this.kryptonForm1.JumpList.ShowRecentCategory = true;
// UserTasks would be added programmatically or via collection editor
```

### Designer Limitations

- Jump list updates are only applied at runtime, not in the designer
- The jump list is not visible in the designer preview
- Changes to jump list properties require the form to have a valid handle at runtime
- `UserTasks` collection can be edited, but complex scenarios may require code

---

## Implementation Details

### Windows API Integration

The feature uses the Windows `ICustomDestinationList` COM interface, which is available on Windows 7 and later:

#### Interface Definition

```csharp
[ComImport]
[Guid("6332debf-87b5-4670-90c0-5e57b408a49e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface ICustomDestinationList
{
    void SetAppID(string pszAppID);
    int BeginList(out uint pcMaxSlots, ref Guid riid, out IntPtr ppv);
    int AppendCategory(string pszCategory, IObjectArray poa);
    int AppendKnownCategory(KNOWNDESTCATEGORY category);
    int AddUserTasks(IObjectArray poa);
    int CommitList();
    // ... other methods
}
```

#### Known Destination Categories

```csharp
internal enum KNOWNDESTCATEGORY
{
    KDC_FREQUENT = 1,
    KDC_RECENT = 2
}
```

#### Shell Link Creation

Jump list items are converted to Windows shell links (`IShellLinkW`) which are then added to object collections:

```csharp
var shellLink = (IShellLinkW)new ShellLink();
shellLink.SetPath(item.Path);
shellLink.SetArguments(item.Arguments);
shellLink.SetWorkingDirectory(item.WorkingDirectory);
shellLink.SetDescription(item.Description);
shellLink.SetIconLocation(item.IconPath, item.IconIndex);
```

#### COM Object Creation

```csharp
[ComImport]
[Guid("77f10cf0-3db5-496c-bb43-78e2f5a1d207")]
[ClassInterface(ClassInterfaceType.None)]
internal class CustomDestinationList
{
}
```

#### Method Call Sequence

```csharp
var destinationList = (ICustomDestinationList)new CustomDestinationList();
destinationList.SetAppID(appId);
destinationList.BeginList(out uint maxSlots, ref iidObjectArray, out IntPtr removedItems);

// Add known categories
if (showFrequent)
    destinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_FREQUENT);
if (showRecent)
    destinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_RECENT);

// Add custom categories
foreach (var category in categories)
{
    var items = CreateObjectArray(category.Value);
    destinationList.AppendCategory(category.Key, items);
}

// Add user tasks
if (userTasks.Count > 0)
{
    var tasks = CreateObjectArray(userTasks);
    destinationList.AddUserTasks(tasks);
}

// Commit the jump list
destinationList.CommitList();
```

### Update Mechanism

The jump list is automatically updated:

1. **On Property Change**: When jump list properties change, the jump list is rebuilt
2. **Event-Driven**: Uses internal event notification system
3. **Handle Validation**: Ensures form handle is created before updating jump list

### Error Handling

The implementation includes comprehensive error handling:

- **Platform Check**: Verifies Windows 7+ before attempting to use API
- **Handle Validation**: Ensures form handle is created before updating jump list
- **AppId Validation**: Checks that AppId is set before creating jump list
- **COM Exception Handling**: Catches and logs COM-related errors
- **Graceful Degradation**: Silently fails on unsupported platforms

### Application ID Requirements

The `AppId` property is **required** for jump lists to function. It should be:

- **Unique**: Use a format like "CompanyName.ApplicationName" to ensure uniqueness
- **Persistent**: Use the same AppId across application sessions
- **Valid**: Should not contain special characters that might cause issues

**Recommended Format**: `"CompanyName.ApplicationName"` or `"CompanyName.ApplicationName.Version"`

---

## Best Practices

### 1. Set Application ID Early

Always set the `AppId` property before adding items to the jump list:

```csharp
// Good
form.JumpList.AppId = "MyCompany.MyApplication";
form.JumpList.UserTasks.Add(new JumpListItem { /* ... */ });

// Bad - AppId not set
form.JumpList.UserTasks.Add(new JumpListItem { /* ... */ });
```

### 2. Use Meaningful Titles

Provide clear, descriptive titles for jump list items:

```csharp
// Good
Title = "Create New Document"

// Bad
Title = "New"
```

### 3. Provide Descriptions

Always provide descriptions for better user experience:

```csharp
new JumpListItem
{
    Title = "Open Settings",
    Path = Application.ExecutablePath,
    Arguments = "/settings",
    Description = "Open application settings and preferences"
}
```

### 4. Use Appropriate Icons

Use icons that clearly represent the action or file:

```csharp
// Use application icon for tasks
IconPath = Application.ExecutablePath,
IconIndex = 0

// Use file icon for documents (let Windows determine)
// IconPath left empty - Windows will use file association icon
```

### 5. Limit Category Items

Keep the number of items in categories reasonable (Windows recommends 10-15 items):

```csharp
// Good - limit to 10 recent items
var recentItems = recentFiles.Take(10).Select(/* ... */).ToList();
jumpList.AddCategory("Recent Files", recentItems);

// Bad - too many items
var allItems = allFiles.Select(/* ... */).ToList(); // Could be hundreds
jumpList.AddCategory("All Files", allItems);
```

### 6. Handle Command-Line Arguments

Always handle command-line arguments from jump list items:

```csharp
private void HandleCommandLineArguments()
{
    var args = Environment.GetCommandLineArgs();
    if (args.Length > 1)
    {
        // Handle arguments passed from jump list
        ProcessArguments(args.Skip(1).ToArray());
    }
}
```

### 7. Update Jump Lists Dynamically

Update jump lists when relevant data changes:

```csharp
public void OnFileOpened(string filePath)
{
    AddToRecentFiles(filePath);
    UpdateJumpList(); // Refresh jump list with new recent file
}
```

### 8. Use Known Categories Appropriately

Use Windows-managed categories when appropriate:

```csharp
// Good - let Windows manage recent files
jumpList.ShowRecentCategory = true;

// Also good - custom recent files with more control
var customRecent = GetRecentFiles();
jumpList.AddCategory("Recent Files", customRecent);
```

### 9. Validate File Paths

Validate file paths before adding to jump list:

```csharp
public void AddFileToJumpList(string filePath)
{
    if (File.Exists(filePath))
    {
        jumpList.AddCategory("Files", new List<JumpListItem>
        {
            new JumpListItem { Title = Path.GetFileName(filePath), Path = filePath }
        });
    }
}
```

### 10. Clear Jump Lists When Appropriate

Clear jump lists when they're no longer relevant:

```csharp
public void ClearJumpList()
{
    jumpList.ClearCategories();
    jumpList.UserTasks.Clear();
}
```

---

## Troubleshooting

### Jump List Not Appearing

**Problem**: Jump list doesn't appear when right-clicking taskbar button.

**Possible Causes**:
1. Application ID not set
2. Windows version is earlier than Windows 7
3. Form handle hasn't been created yet
4. No items added to jump list
5. Jump list commit failed

**Solutions**:
- Verify `AppId` is set: `form.JumpList.AppId != string.Empty`
- Verify Windows version: `Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1`
- Ensure form handle is created: Check `IsHandleCreated` property
- Add at least one item (user task or category)
- Check for COM exceptions in error logs

### Items Not Appearing in Jump List

**Problem**: Items are added but don't appear in jump list.

**Possible Causes**:
1. Items added before AppId is set
2. Path is invalid or empty
3. Jump list not committed
4. Too many items (Windows limit)

**Solutions**:
- Set `AppId` before adding items
- Verify `Path` property is set and valid
- Ensure jump list is committed (happens automatically on property change)
- Limit items to reasonable number (10-15 per category)

### Icons Not Displaying

**Problem**: Icons don't appear for jump list items.

**Possible Causes**:
1. Icon path is invalid
2. Icon index is incorrect
3. Icon file doesn't contain icon at specified index

**Solutions**:
- Verify `IconPath` points to valid file (EXE, DLL, or ICO)
- Use `IconIndex = 0` for single-icon files
- For multi-icon resources, verify correct index
- Leave `IconPath` empty to use default/associated icon

### Command-Line Arguments Not Working

**Problem**: Clicking jump list item doesn't pass arguments correctly.

**Possible Causes**:
1. Arguments not handled in application
2. Arguments format is incorrect
3. File paths with spaces not quoted

**Solutions**:
- Handle command-line arguments in `Main` method or form constructor
- Use quoted paths for arguments with spaces: `Arguments = $"\"{filePath}\""`
- Parse arguments correctly: `Environment.GetCommandLineArgs()`

### Jump List Updates Not Reflecting

**Problem**: Changes to jump list don't appear.

**Possible Causes**:
1. Jump list cached by Windows
2. Changes made before handle creation
3. Jump list not committed

**Solutions**:
- Windows may cache jump lists - wait a moment or restart application
- Ensure form handle is created before making changes
- Changes are automatically committed, but you can force update by clearing and re-adding

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
// Check if Windows 7+ (ICustomDestinationList requires Windows 7+)
if (Environment.OSVersion.Version.Major < 6 ||
    (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 1))
{
    return; // Not supported on Windows Vista or earlier
}
```

### Graceful Degradation

On unsupported platforms, the feature silently fails without throwing exceptions. Your application will continue to function normally, but the jump list will not be displayed.

---

## Related Issues

- **Issue #1214**: Implement "overlay icons" on form task bar images (related feature)
- **Issue #2886**: Taskbar progress bar support (related feature)

---

## See Also

- [Taskbar Overlay Icon Feature](./taskbar-overlay-icon-feature.md) - Related feature for overlay icons
- [Taskbar Progress Feature](./taskbar-progress-feature.md) - Related feature for progress indicators
- [Windows Jump Lists Documentation](https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icustomdestinationlist) - Microsoft documentation for ICustomDestinationList
- [Windows Shell Links Documentation](https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ishelllinkw) - Microsoft documentation for IShellLinkW

---

## Version History

- **2026-01-14**: Initial implementation of jump list feature
  - Added `JumpListValues` class
  - Added `JumpListItem` class
  - Integrated with `VisualForm` base class
  - Full designer support
  - Support for user tasks, custom categories, and known categories
  - Comprehensive error handling

---

## License

This feature is part of the Krypton Standard Toolkit and is licensed under the BSD 3-Clause License. See the [LICENSE](../LICENSE) file for details.
