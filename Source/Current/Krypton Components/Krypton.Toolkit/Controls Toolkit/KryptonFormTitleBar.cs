#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Identifies which standard item was clicked in <see cref="KryptonFormTitleBar"/>.
/// </summary>
public enum FormTitleBarStandardItem
{
    /// <summary>New</summary>
    New,

    /// <summary>Open</summary>
    Open,

    /// <summary>Save</summary>
    Save,

    /// <summary>Save As</summary>
    SaveAs,

    /// <summary>Save All</summary>
    SaveAll,

    /// <summary>Cut</summary>
    Cut,

    /// <summary>Copy</summary>
    Copy,

    /// <summary>Paste</summary>
    Paste,

    /// <summary>Undo</summary>
    Undo,

    /// <summary>Redo</summary>
    Redo,

    /// <summary>Page Setup</summary>
    PageSetup,

    /// <summary>Print Preview</summary>
    PrintPreview,

    /// <summary>Print</summary>
    Print,

    /// <summary>Quick Print</summary>
    QuickPrint,

    /// <summary>Exit</summary>
    Exit,

    /// <summary>Select All</summary>
    SelectAll,

    /// <summary>Customize</summary>
    Customize,

    /// <summary>Options</summary>
    Options,

    /// <summary>Contents</summary>
    Contents,

    /// <summary>Index</summary>
    Index,

    /// <summary>About</summary>
    About
}

/// <summary>
/// Provides event data for <see cref="KryptonFormTitleBar.StandardItemClick"/>.
/// </summary>
public class FormTitleBarStandardItemClickEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormTitleBarStandardItemClickEventArgs"/> class.
    /// </summary>
    /// <param name="item">The standard item that was clicked.</param>
    public FormTitleBarStandardItemClickEventArgs(FormTitleBarStandardItem item)
    {
        Item = item;
    }

    /// <summary>Gets the standard item that was clicked.</summary>
    public FormTitleBarStandardItem Item { get; }
}

/// <summary>
/// Provides a toolbar-style area inside the <see cref="KryptonForm"/> title bar (caption area).
/// </summary>
/// <remarks>
/// <para>
/// Add <see cref="ButtonSpecAny"/> items to <see cref="ButtonSpecs"/> to display icon buttons on
/// the <em>left</em> side of the title bar, after the form icon and before the title text.
/// </para>
/// <para>
/// Assign an instance of this component to <see cref="KryptonForm.TitleBar"/> to activate the
/// integration.  The mechanism mirrors the approach used by <c>KryptonRibbon</c> when it injects
/// its Quick Access Toolbar into the custom chrome caption area.
/// </para>
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonFormTitleBar), "ToolboxBitmaps.KryptonFormTitleBar.bmp")]
[DefaultEvent(nameof(ButtonSpecs))]
[DefaultProperty(nameof(ButtonSpecs))]
[Designer(typeof(KryptonFormTitleBarDesigner))]
[DesignerCategory(@"code")]
[Description(@"Hosts button-spec items inside the KryptonForm title bar.")]
public class KryptonFormTitleBar : Component
{
    #region Instance Fields

    private KryptonForm? _ownerForm;

    #endregion

    #region Events

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecInserted;

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecRemoved;

    /// <summary>
    /// Raised when a standard item (menu or toolbar button) is clicked.
    /// Subscribers can switch on <see cref="FormTitleBarStandardItemClickEventArgs.Item"/> to handle each action.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Raised when a standard menu or toolbar item is clicked.")]
    public event EventHandler<FormTitleBarStandardItemClickEventArgs>? StandardItemClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormTitleBar"/> class.
    /// </summary>
    public KryptonFormTitleBar()
    {
        ButtonSpecs = new FormTitleBarButtonSpecCollection(this);
        ButtonSpecs.Inserted += (s, e) => ButtonSpecInserted?.Invoke(s, e);
        ButtonSpecs.Removed += (s, e) => ButtonSpecRemoved?.Invoke(s, e);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the collection of button specifications displayed in the title bar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications shown in the title bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarButtonSpecCollection ButtonSpecs { get; }

    /// <summary>
    /// Gets the <see cref="KryptonForm"/> this component is currently attached to, or <c>null</c>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonForm? OwnerForm => _ownerForm;

    /// <summary>
    /// Inserts a standard set of button specifications into the title bar, similar to the
    /// WinForms MenuStrip "Insert Standard Items" option.
    /// </summary>
    /// <remarks>
    /// Adds top-level menu dropdowns (File, Edit, Tools, Help) each with sub-items, followed
    /// by flat icon buttons for quick access: New, Open, Save, Save As, Save All, Cut, Copy,
    /// Paste, Undo, Redo, Page Setup, Print Preview, Print, and Quick Print. Handle
    /// <see cref="StandardItemClick"/> to respond to all items, or wire
    /// <see cref="ButtonSpecAny.Click"/> / <see cref="KryptonContextMenuItem.Click"/> individually.
    /// </remarks>
    public void InsertStandardItems()
    {
        void OnItem(FormTitleBarStandardItem item) =>
            StandardItemClick?.Invoke(this, new FormTitleBarStandardItemClickEventArgs(item));

        ButtonSpecs.AddRange(CreateStandardMenuButtonSpecs(OnItem));
        ButtonSpecs.AddRange(CreateStandardToolbarButtonSpecs(OnItem));
    }

    /// <summary>
    /// Creates the top-level menu button specifications (File, Edit, Tools, Help) with dropdowns.
    /// Uses <see cref="KryptonManager.Strings"/> for localizable text.
    /// </summary>
    /// <param name="onItemClick">Optional. When provided, wires each menu item's Click to raise the callback.</param>
    internal static ButtonSpecAny[] CreateStandardMenuButtonSpecs(Action<FormTitleBarStandardItem>? onItemClick = null)
    {
        var tb = KryptonManager.Strings.ToolBarStrings;
        var fb = KryptonManager.Strings.TitleBarStrings;

        var fileNew = new KryptonContextMenuItem(tb.New);
        var fileOpen = new KryptonContextMenuItem(tb.Open);
        var fileSave = new KryptonContextMenuItem(tb.Save);
        var fileSaveAs = new KryptonContextMenuItem(tb.SaveAs);
        var fileSaveAll = new KryptonContextMenuItem(tb.SaveAll);
        var filePrint = new KryptonContextMenuItem(tb.Print);
        var filePrintPreview = new KryptonContextMenuItem(tb.PrintPreview);
        var fileExit = new KryptonContextMenuItem(fb.Exit);
        WireClick(fileNew, FormTitleBarStandardItem.New, onItemClick);
        WireClick(fileOpen, FormTitleBarStandardItem.Open, onItemClick);
        WireClick(fileSave, FormTitleBarStandardItem.Save, onItemClick);
        WireClick(fileSaveAs, FormTitleBarStandardItem.SaveAs, onItemClick);
        WireClick(fileSaveAll, FormTitleBarStandardItem.SaveAll, onItemClick);
        WireClick(filePrint, FormTitleBarStandardItem.Print, onItemClick);
        WireClick(filePrintPreview, FormTitleBarStandardItem.PrintPreview, onItemClick);
        WireClick(fileExit, FormTitleBarStandardItem.Exit, onItemClick);

        var fileItems = new KryptonContextMenuItems();
        fileItems.Items.Add(fileNew);
        fileItems.Items.Add(fileOpen);
        fileItems.Items.Add(fileSave);
        fileItems.Items.Add(fileSaveAs);
        fileItems.Items.Add(fileSaveAll);
        fileItems.Items.Add(new KryptonContextMenuSeparator());
        fileItems.Items.Add(filePrint);
        fileItems.Items.Add(filePrintPreview);
        fileItems.Items.Add(new KryptonContextMenuSeparator());
        fileItems.Items.Add(fileExit);
        var fileMenu = new KryptonContextMenu();
        fileMenu.Items.Add(fileItems);

        var editUndo = new KryptonContextMenuItem(tb.Undo);
        var editRedo = new KryptonContextMenuItem(tb.Redo);
        var editCut = new KryptonContextMenuItem(tb.Cut);
        var editCopy = new KryptonContextMenuItem(tb.Copy);
        var editPaste = new KryptonContextMenuItem(tb.Paste);
        var editSelectAll = new KryptonContextMenuItem(fb.SelectAll);
        WireClick(editUndo, FormTitleBarStandardItem.Undo, onItemClick);
        WireClick(editRedo, FormTitleBarStandardItem.Redo, onItemClick);
        WireClick(editCut, FormTitleBarStandardItem.Cut, onItemClick);
        WireClick(editCopy, FormTitleBarStandardItem.Copy, onItemClick);
        WireClick(editPaste, FormTitleBarStandardItem.Paste, onItemClick);
        WireClick(editSelectAll, FormTitleBarStandardItem.SelectAll, onItemClick);

        var editItems = new KryptonContextMenuItems();
        editItems.Items.Add(editUndo);
        editItems.Items.Add(editRedo);
        editItems.Items.Add(new KryptonContextMenuSeparator());
        editItems.Items.Add(editCut);
        editItems.Items.Add(editCopy);
        editItems.Items.Add(editPaste);
        editItems.Items.Add(new KryptonContextMenuSeparator());
        editItems.Items.Add(editSelectAll);
        var editMenu = new KryptonContextMenu();
        editMenu.Items.Add(editItems);

        var toolsCustomize = new KryptonContextMenuItem(fb.Customize);
        var toolsOptions = new KryptonContextMenuItem(fb.Options);
        WireClick(toolsCustomize, FormTitleBarStandardItem.Customize, onItemClick);
        WireClick(toolsOptions, FormTitleBarStandardItem.Options, onItemClick);

        var toolsItems = new KryptonContextMenuItems();
        toolsItems.Items.Add(toolsCustomize);
        toolsItems.Items.Add(toolsOptions);
        var toolsMenu = new KryptonContextMenu();
        toolsMenu.Items.Add(toolsItems);

        var helpContents = new KryptonContextMenuItem(fb.Contents);
        var helpIndex = new KryptonContextMenuItem(fb.Index);
        var helpAbout = new KryptonContextMenuItem(fb.About);
        WireClick(helpContents, FormTitleBarStandardItem.Contents, onItemClick);
        WireClick(helpIndex, FormTitleBarStandardItem.Index, onItemClick);
        WireClick(helpAbout, FormTitleBarStandardItem.About, onItemClick);

        var helpItems = new KryptonContextMenuItems();
        helpItems.Items.Add(helpContents);
        helpItems.Items.Add(helpIndex);
        helpItems.Items.Add(new KryptonContextMenuSeparator());
        helpItems.Items.Add(helpAbout);
        var helpMenu = new KryptonContextMenu();
        helpMenu.Items.Add(helpItems);

        var fileBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.File,
            AllowInheritText = false,
            ShowDrop = true,
            KryptonContextMenu = fileMenu,
            ToolTipTitle = fb.File
        };
        var editBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Edit,
            AllowInheritText = false,
            ShowDrop = true,
            KryptonContextMenu = editMenu,
            ToolTipTitle = fb.Edit
        };
        var toolsBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Tools,
            AllowInheritText = false,
            ShowDrop = true,
            KryptonContextMenu = toolsMenu,
            ToolTipTitle = fb.Tools
        };
        var helpBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Help,
            AllowInheritText = false,
            ShowDrop = true,
            KryptonContextMenu = helpMenu,
            ToolTipTitle = fb.Help
        };

        return new[] { fileBtn, editBtn, toolsBtn, helpBtn };
    }

    /// <summary>
    /// Creates the flat toolbar button specifications (New, Open, Save, etc.).
    /// Uses <see cref="KryptonManager.Strings"/> for localizable text.
    /// </summary>
    /// <param name="onItemClick">Optional. When provided, wires each button's Click to raise the callback.</param>
    internal static ButtonSpecAny[] CreateStandardToolbarButtonSpecs(Action<FormTitleBarStandardItem>? onItemClick = null)
    {
        var tb = KryptonManager.Strings.ToolBarStrings;

        var newBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.New, ToolTipTitle = tb.New };
        var openBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Open, ToolTipTitle = tb.Open };
        var saveBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Save, ToolTipTitle = tb.Save };
        var saveAsBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.SaveAs, ToolTipTitle = tb.SaveAs };
        var saveAllBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.SaveAll, ToolTipTitle = tb.SaveAll };
        var cutBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Cut, ToolTipTitle = tb.Cut };
        var copyBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Copy, ToolTipTitle = tb.Copy };
        var pasteBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Paste, ToolTipTitle = tb.Paste };
        var undoBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Undo, ToolTipTitle = tb.Undo };
        var redoBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Redo, ToolTipTitle = tb.Redo };
        var pageSetupBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.PageSetup, ToolTipTitle = tb.PageSetup };
        var printPreviewBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.PrintPreview, ToolTipTitle = tb.PrintPreview };
        var printBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Print, ToolTipTitle = tb.Print };
        var quickPrintBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.QuickPrint, ToolTipTitle = tb.QuickPrint };

        WireClick(newBtn, FormTitleBarStandardItem.New, onItemClick);
        WireClick(openBtn, FormTitleBarStandardItem.Open, onItemClick);
        WireClick(saveBtn, FormTitleBarStandardItem.Save, onItemClick);
        WireClick(saveAsBtn, FormTitleBarStandardItem.SaveAs, onItemClick);
        WireClick(saveAllBtn, FormTitleBarStandardItem.SaveAll, onItemClick);
        WireClick(cutBtn, FormTitleBarStandardItem.Cut, onItemClick);
        WireClick(copyBtn, FormTitleBarStandardItem.Copy, onItemClick);
        WireClick(pasteBtn, FormTitleBarStandardItem.Paste, onItemClick);
        WireClick(undoBtn, FormTitleBarStandardItem.Undo, onItemClick);
        WireClick(redoBtn, FormTitleBarStandardItem.Redo, onItemClick);
        WireClick(pageSetupBtn, FormTitleBarStandardItem.PageSetup, onItemClick);
        WireClick(printPreviewBtn, FormTitleBarStandardItem.PrintPreview, onItemClick);
        WireClick(printBtn, FormTitleBarStandardItem.Print, onItemClick);
        WireClick(quickPrintBtn, FormTitleBarStandardItem.QuickPrint, onItemClick);

        return new[]
        {
            newBtn,
            openBtn,
            saveBtn,
            saveAsBtn,
            saveAllBtn,
            cutBtn,
            copyBtn,
            pasteBtn,
            undoBtn,
            redoBtn,
            pageSetupBtn,
            printPreviewBtn,
            printBtn,
            quickPrintBtn
        };
    }

    private static void WireClick(KryptonContextMenuItem item, FormTitleBarStandardItem which, Action<FormTitleBarStandardItem>? onItemClick)
    {
        if (onItemClick != null)
        {
            item.Click += (_, _) => onItemClick(which);
        }
    }

    private static void WireClick(ButtonSpecAny spec, FormTitleBarStandardItem which, Action<FormTitleBarStandardItem>? onItemClick)
    {
        if (onItemClick != null)
        {
            spec.Click += (_, _) => onItemClick(which);
        }
    }

    /// <summary>
    /// Creates the complete standard set of button specifications (menus + toolbar).
    /// Used by the designer when inserting via the "Insert Standard Items" verb.
    /// </summary>
    internal static ButtonSpecAny[] CreateStandardButtonSpecs()
    {
        var list = new List<ButtonSpecAny>();
        list.AddRange(CreateStandardMenuButtonSpecs());
        list.AddRange(CreateStandardToolbarButtonSpecs());
        return list.ToArray();
    }

    #endregion

    #region Internal

    internal void SetOwnerForm(KryptonForm? form) => _ownerForm = form;

    #endregion

    #region Protected

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing && _ownerForm != null)
        {
            _ownerForm.TitleBar = null;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Typed collection of <see cref="ButtonSpecAny"/> items for the title bar.
    /// </summary>
    public class FormTitleBarButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormTitleBarButtonSpecCollection"/> class.
        /// </summary>
        public FormTitleBarButtonSpecCollection(KryptonFormTitleBar owner)
            : base(owner)
        {
        }
    }

    #endregion
}
