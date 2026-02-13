# Krypton Navigator Tabbed Editor Feature

## Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [API Reference](#api-reference)
4. [Properties](#properties)
5. [Methods](#methods)
6. [Events](#events)
7. [Integration with KryptonNavigator and KryptonRichTextBox](#integration-with-kryptonnavigator-and-kryptonrichtextbox)
8. [Usage Examples](#usage-examples)
9. [Designer Support](#designer-support)
10. [Implementation Details](#implementation-details)
11. [Best Practices](#best-practices)
12. [Troubleshooting](#troubleshooting)
13. [Platform Compatibility](#platform-compatibility)
14. [Related Documentation](#related-documentation)
15. [Related Issues](#related-issues)

---

## Overview

The **KryptonNavigatorTabbedEditor** is a composite control that provides a tabbed document interface where each tab is a **KryptonPage** hosting a **KryptonRichTextBox**. It is built on **KryptonNavigator** from Krypton.Navigator and **KryptonRichTextBox** from Krypton.Toolkit, and is delivered in the **Krypton.Navigator.Utilities** assembly.

Use this control when you need a multi-document editor with Krypton-styled tabs, per-tab close buttons, and full access to both the navigator (for mode, palette, and layout) and each rich text editor (for content and formatting).

### Key Features

- **KryptonNavigator-based tabs**: Uses `KryptonNavigator` in **BarTabGroup** mode by default; supports all Navigator modes (BarTabOnly, OutlookFull, Panel, etc.).
- **One KryptonRichTextBox per tab**: Each tab is a `KryptonPage` with a single `KryptonRichTextBox` docked to fill the page.
- **Per-tab close button**: Each page gets a close `ButtonSpec`; close action is configurable via the underlying Navigator (default: remove and dispose page).
- **Unified API**: Add/remove tabs, get/set selected index or page, get editor by index or page, clear all tabs.
- **Editor collection**: Read-only collection of editors in tab order for iteration and indexing.
- **SelectedIndexChanged**: Single event when the selected tab changes.
- **Full Krypton theming**: Inherits palette from parent; Navigator and RichTextBox both respect Krypton styling.
- **Toolbox support**: Control is toolbox-enabled with a toolbox bitmap.

### Use Cases

- **Multi-document text/RTF editors**: Notepad-style or rich-text apps with multiple open documents.
- **Log or output viewers**: Multiple log/output tabs with copy and search.
- **Template or snippet editors**: Several editor tabs with different content.
- **Script/code panels**: Multiple script tabs with rich text (syntax highlighting can be added externally).
- **Form builders or config editors**: Tabbed panels where each tab is an editable text area.
- **Any MDI-like editor UI**: When you want tabbed documents with Krypton look and Navigator flexibility.

### Requirements

- **Assembly**: `Krypton.Navigator.Utilities` (references `Krypton.Navigator` and `Krypton.Toolkit`).
- **Namespace**: `Krypton.Navigator.Utilities`.
- **Base class**: `VisualPanel` (Krypton.Toolkit); requires Windows Forms and Krypton theming.
- **.NET**: Same target frameworks as the rest of the suite (e.g. .NET Framework 4.7.2–4.8.1 and .NET 8/9/10 Windows).

---

## Quick Start

### Basic Usage

```csharp
using Krypton.Navigator.Utilities;
using Krypton.Toolkit;

// Create the control (e.g. on a form or panel)
var editor = new KryptonNavigatorTabbedEditor
{
    Dock = DockStyle.Fill
};

// Add tabs
var rtb1 = editor.AddTab("Document 1");
var rtb2 = editor.AddTab("Document 2", "Initial content here.");

// Select first tab and focus its editor
editor.SelectedIndex = 0;
editor.SelectedEditor?.Focus();

// React to tab change
editor.SelectedIndexChanged += (s, e) =>
{
    var current = editor.SelectedEditor;
    if (current != null)
        System.Diagnostics.Debug.WriteLine($"Switched to editor: {current.TextLength} chars");
};

// Optional: change Navigator mode (e.g. Outlook-style)
editor.Navigator.NavigatorMode = Krypton.Navigator.NavigatorMode.OutlookFull;
```

### Add, Remove, and Clear Tabs

```csharp
// Add tab with optional initial text
KryptonRichTextBox rtb = editor.AddTab("New Tab", "Hello, World!");

// Remove by index
editor.RemoveTab(0);

// Remove by page reference
editor.RemoveTab(editor.SelectedPage!);

// Remove all tabs
editor.ClearTabs();
```

### Get Editor by Index or Page

```csharp
KryptonRichTextBox? byIndex = editor.GetEditor(0);
KryptonRichTextBox? byPage = editor.GetEditor(editor.SelectedPage!);

// Or use the read-only collection (same order as tabs)
foreach (var rtb in editor.EditorControls)
{
    rtb.Clear(); // example
}
```

---

## API Reference

### Namespace

```csharp
using Krypton.Navigator.Utilities;
```

The control also uses types from:

```csharp
using Krypton.Navigator;  // KryptonPage, KryptonPageCollection, NavigatorMode, CloseActionEventArgs, etc.
using Krypton.Toolkit;   // KryptonRichTextBox, VisualPanel, ButtonSpecAny, PaletteButtonSpecStyle, etc.
```

### Assembly

- **Project**: `Krypton.Navigator.Utilities`
- **Assembly name**: `Krypton.Navigator.Utilities.dll`

---

## Properties

| Property | Type | Access | Description |
|----------|------|--------|-------------|
| **Navigator** | `KryptonNavigator` | get | The underlying KryptonNavigator. Use for NavigatorMode, Button settings, palette, and other Navigator-specific configuration. |
| **Pages** | `KryptonPageCollection` | get | Collection of tab pages. Same as `Navigator.Pages`. Add/remove pages through the control’s methods to keep internal editor list in sync. |
| **EditorControls** | `ReadOnlyCollection<KryptonRichTextBox>` | get | Read-only list of editors in tab order. One-to-one with `Pages` by index. |
| **SelectedIndex** | `int` | get/set | Zero-based index of the selected tab. -1 if none. Setting selects the tab at that index. |
| **SelectedPage** | `KryptonPage?` | get/set | The currently selected tab page. Null if none. |
| **SelectedEditor** | `KryptonRichTextBox?` | get | The KryptonRichTextBox of the currently selected tab. Null if no selection or index out of range. |

All of the above are effectively forwarded to or derived from the internal `KryptonNavigator` and the internal list of editors. The control does not expose a separate “tab count” property; use `Pages.Count` or `EditorControls.Count`.

---

## Methods

### Tab management

| Method | Signature | Description |
|--------|-----------|-------------|
| **AddTab** | `KryptonRichTextBox AddTab(string tabText)` | Adds a new tab with the given header text and an empty KryptonRichTextBox. Returns the new editor. |
| **AddTab** | `KryptonRichTextBox AddTab(string tabText, string? initialText)` | Same as above but sets the initial text of the editor. |
| **RemoveTab** | `void RemoveTab(int index)` | Removes the tab at `index` and disposes the page. Keeps `EditorControls` in sync. No-op if index out of range. |
| **RemoveTab** | `void RemoveTab(KryptonPage page)` | Removes the tab that is the given page (by index). No-op if page not in `Pages`. |
| **ClearTabs** | `void ClearTabs()` | Removes and disposes all pages and clears the internal editor list. |
| **GetEditor** | `KryptonRichTextBox? GetEditor(int index)` | Returns the editor at `index`, or null if out of range. |
| **GetEditor** | `KryptonRichTextBox? GetEditor(KryptonPage page)` | Returns the editor for the given page, or null if page not found. |
| **SetFixedState** | `void SetFixedState(PaletteState state)` | Reserved for palette state; no-op in this control. |

### AddTab behavior (summary)

- Creates a `KryptonPage` with `Text` / `TextTitle` set to `tabText`, and a unique `UniqueName` (GUID).
- Creates a `KryptonRichTextBox` with `Dock = Fill`, `Multiline = true`, `AcceptsTab = true`, `WordWrap = true`, `ScrollBars = Both`.
- If `initialText` is non-null/non-empty, sets `editor.Text = initialText`.
- Adds a close `ButtonSpecAny` (PaletteButtonSpecStyle.Close) to the page’s `ButtonSpecs`.
- Adds the page to `Navigator.Pages` and appends the editor to the internal list, then returns the editor.

### RemoveTab / ClearTabs behavior

- **RemoveTab(index)**: Removes the editor at that index from the internal list, then removes the page from the Navigator and disposes the page.
- **ClearTabs**: Clears the internal editor list, disposes each page, then clears `Navigator.Pages`.

Closing a tab via the **page’s close button** is handled by the Navigator’s close action (default: remove and dispose). The control subscribes to `Navigator.CloseAction` and removes the corresponding editor from its list when the close action is `RemovePage` or `RemovePageAndDispose`.

---

## Events

| Event | Type | Description |
|-------|------|-------------|
| **SelectedIndexChanged** | `EventHandler` | Raised when the selected tab index changes (including when tabs are removed and selection moves). Fired after the Navigator’s selected page has changed. |

There are no other public events on the control. For close handling, use the Navigator’s **CloseAction** (on `Navigator` property) or handle the close button’s action via Navigator configuration.

---

## Integration with KryptonNavigator and KryptonRichTextBox

### KryptonNavigator

- The control creates one **KryptonNavigator** and keeps it in the **Navigator** property.
- **NavigatorMode**: Default is `NavigatorMode.BarTabGroup`. You can set `editor.Navigator.NavigatorMode` to any supported mode (e.g. `BarTabOnly`, `BarRibbonTabGroup`, `OutlookFull`, `Panel`, `Group`, `HeaderGroup`).
- **Close button**: `Navigator.Button.CloseButtonDisplay` is set to `ButtonDisplay.ShowEnabled` and `CloseButtonAction` to `RemovePageAndDispose` so that each tab’s close button removes and disposes the page. You can change these on `Navigator.Button` if you need different behavior.
- **Palette**: Navigator uses the same palette as its parent; no extra steps required for theming.
- **Pages**: Do not add or remove pages directly via `Navigator.Pages` if you want the control’s **EditorControls** and **GetEditor** to stay correct. Use **AddTab**, **RemoveTab**, and **ClearTabs** instead.

### KryptonPage

- Each tab is a **KryptonPage** with:
  - **Text** / **TextTitle**: Tab caption.
  - **UniqueName**: Set to a new GUID string so pages are uniquely identified.
  - **ButtonSpecs**: One close **ButtonSpecAny** (type Close) with tooltip.
  - **Controls**: One child, the **KryptonRichTextBox**, Dock = Fill.

You can read or change **SelectedPage** and use **Pages**[index] to access page properties (e.g. text, visibility) without breaking the control, as long as you do not remove pages directly from **Pages**.

### KryptonRichTextBox

- Each tab’s content is a **KryptonRichTextBox** with default settings suitable for multi-line editing:
  - **Dock = Fill**, **Multiline = true**, **AcceptsTab = true**, **WordWrap = true**, **ScrollBars = Both**.
- You can use the returned instance from **AddTab** or **GetEditor** / **SelectedEditor** to:
  - Set **Text**, **Rtf**, **SelectedText**, **Font**, **ForeColor**, etc.
  - Use **Cut**, **Copy**, **Paste**, **Undo**, **Redo**, **SelectAll**, **Find**, **Clear**.
  - Subscribe to **TextChanged**, **SelectionChanged**, **Modified**, and other RichTextBox events.
- The control does not replace or subclass the RichTextBox; it only creates and hosts it, so the full Krypton.Toolkit KryptonRichTextBox API applies.

---

## Usage Examples

### Minimal form with tabbed editor

```csharp
var form = new KryptonForm { Text = "Tabbed Editor", Size = new Size(800, 600) };
var editor = new KryptonNavigatorTabbedEditor { Dock = DockStyle.Fill };
form.Controls.Add(editor);

editor.AddTab("Untitled 1");
editor.AddTab("Readme", "Welcome to the editor.");
editor.SelectedIndex = 0;
form.Show();
```

### Add tab and focus

```csharp
var rtb = editor.AddTab($"Document {editor.Pages.Count + 1}", initialText);
editor.SelectedIndex = editor.Pages.Count - 1;
rtb.Focus();
```

### Iterate all editors

```csharp
for (int i = 0; i < editor.EditorControls.Count; i++)
{
    var page = editor.Pages[i];
    var rtb = editor.EditorControls[i];
    Console.WriteLine($"Tab {i}: {page.Text}, Length={rtb.TextLength}, Modified={rtb.Modified}");
}
```

### Find text in current editor

```csharp
var rtb = editor.SelectedEditor;
if (rtb != null)
{
    int pos = rtb.Find(searchText);
    if (pos >= 0)
    {
        rtb.SelectionStart = pos;
        rtb.SelectionLength = searchText.Length;
        rtb.ScrollToCaret();
    }
}
```

### Change Navigator mode at runtime

```csharp
editor.Navigator.NavigatorMode = NavigatorMode.BarTabOnly;   // Tabs only, no content area
editor.Navigator.NavigatorMode = NavigatorMode.BarTabGroup;   // Tabs + content (default)
editor.Navigator.NavigatorMode = NavigatorMode.OutlookFull;   // Outlook-style list + content
```

### Confirm before closing last tab (via CloseAction)

```csharp
editor.Navigator.CloseAction += (s, e) =>
{
    if (editor.Pages.Count <= 1)
    {
        e.Cancel = true;  // If Cancel exists; otherwise prevent removal in your logic
        // Or set e.Action to CloseButtonAction.None if supported
    }
};
```

(Exact event args depend on Krypton.Navigator’s CloseActionEventArgs; use the type to see if cancellation or changing Action is supported.)

---

## Designer Support

- **Toolbox**: The control is in the toolbox when the project references **Krypton.Navigator.Utilities**. Toolbox bitmap is the same as **KryptonNavigator**.
- **Design time**: You can drop **KryptonNavigatorTabbedEditor** onto a form or panel. Tabs are created at runtime via code (AddTab); there is no design-time tab collection editor for this control.
- **Properties**: **Navigator** is the main design-time entry point for Navigator-specific settings (mode, button visibility, palette). **SelectedIndex**, **SelectedPage**, **SelectedEditor**, **Pages**, and **EditorControls** are typically used in code.
- **Events**: Only **SelectedIndexChanged** appears in the Events list; wire it in the designer or in code.

---

## Implementation Details

- **Base class**: `VisualPanel` (Krypton.Toolkit). The control has one child: the **KryptonNavigator**, which is docked Fill.
- **Editor list**: An internal `Collection<KryptonRichTextBox>` is kept in the same order as **Pages**. It is updated on **AddTab**, **RemoveTab**, **ClearTabs**, and in the **CloseAction** handler when a page is closed by the close button.
- **CloseAction handling**: When the user clicks a tab’s close button, the Navigator raises **CloseAction**. If the action is **RemovePage** or **RemovePageAndDispose**, the control removes the corresponding editor from its list by **e.Index** so **EditorControls** and **GetEditor** remain consistent.
- **New page identity**: Each new page gets `UniqueName = Guid.NewGuid().ToString("N")` so it is unique in the Navigator’s collection.
- **Dispose**: On dispose, the control unsubscribes from Navigator events and clears the editor list; the Navigator and its pages are disposed by the framework as child controls.

---

## Best Practices

1. **Use the control’s API for tabs**: Prefer **AddTab**, **RemoveTab**, and **ClearTabs** instead of adding/removing pages directly on **Navigator.Pages** so **EditorControls** and **GetEditor** stay correct.
2. **Check SelectedEditor for null**: When using **SelectedEditor** or **GetEditor**, always handle null (no tabs or invalid index).
3. **Use SelectedIndexChanged for UI updates**: Update status bars, toolbar state, or title in **SelectedIndexChanged** so they stay in sync with the active tab.
4. **Configure Navigator once**: Set **NavigatorMode** and close-button behavior (e.g. on **Navigator.Button**) after creation if you need non-default behavior.
5. **Avoid removing pages in CloseAction**: In a **CloseAction** handler, do not remove other pages or change the collection; the control already syncs its list from the event.
6. **Dispose**: The control disposes its Navigator and children; ensure you do not hold references to disposed pages or editors after the control is disposed.

---

## Troubleshooting

| Issue | Cause | Solution |
|-------|--------|----------|
| **EditorControls count doesn’t match tabs** | Pages were added/removed directly on **Navigator.Pages** or via Navigator UI without going through the control. | Use only **AddTab**, **RemoveTab**, and **ClearTabs** for tab lifecycle. |
| **GetEditor returns null** | Index out of range or page not in **Pages**. | Check `index >= 0 && index < EditorControls.Count` and that the page belongs to this control. |
| **SelectedEditor is null** | No tabs or SelectedIndex is -1. | Check **Pages.Count** and **SelectedIndex** before using **SelectedEditor**. |
| **Close button doesn’t remove tab** | Navigator’s **CloseButtonAction** or **CloseButtonDisplay** was changed. | Ensure **Navigator.Button.CloseButtonDisplay** shows the button and **CloseButtonAction** is **RemovePage** or **RemovePageAndDispose** if you want automatic removal. |
| **Tabs look different (e.g. Outlook)** | **NavigatorMode** was changed. | Set **Navigator.NavigatorMode** back to **BarTabGroup** for “tabs + content” style. |
| **Control not in toolbox** | Project doesn’t reference **Krypton.Navigator.Utilities** or toolbox wasn’t refreshed. | Add reference to **Krypton.Navigator.Utilities** and rebuild; right-click toolbox and refresh. |

---

## Platform Compatibility

- **Target frameworks**: Same as Krypton.Navigator and Krypton.Toolkit (e.g. .NET Framework 4.7.2, 4.8, 4.8.1, .NET 8/9/10 Windows). See the **Krypton.Navigator.Utilities** project file for the exact list.
- **Dependencies**: Requires **Krypton.Navigator** and **Krypton.Toolkit**. No extra OS or native APIs; behavior is the same on all supported Windows versions where the Krypton suite runs.
- **Designer**: Requires a Visual Studio or design host that can load the control’s assembly and Krypton dependencies.

---

## Related Documentation

- **KryptonNavigator**: See Krypton.Navigator documentation for **NavigatorMode**, **KryptonPage**, **ButtonSpecs**, and **CloseAction**.
- **KryptonRichTextBox**: See Krypton.Toolkit documentation for text, selection, formatting, and events.
- **Krypton.Navigator.Utilities**: This control is the main public API of the Navigator.Utilities assembly; other utilities may be added in the same assembly in the future.
- **Demo**: A full demo form is available in the TestForm project: **KryptonNavigatorTabbedEditorDemo** (launched from Start Screen as "Krypton Navigator Tabbed Editor"). It demonstrates tab management, editor operations, text operations (find, get/set), and Navigator mode switching.

## Related Issues

- None at time of writing. For bugs or feature requests, open an issue in the [Krypton Standard Toolkit repository](https://github.com/Krypton-Suite/Standard-Toolkit).
