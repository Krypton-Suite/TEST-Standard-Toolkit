#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

using Krypton.Navigator;
using Krypton.Toolkit;
using Krypton.Navigator.Utilities.Internal;

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Provides a tabbed code editor control using KryptonNavigator with a KryptonCodeEditor on each tab page.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonNavigator))]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(Pages))]
[DesignerCategory(@"code")]
[Description(@"Provides a tabbed code editor using KryptonNavigator with a KryptonCodeEditor on each page.")]
public class KryptonNavigatorTabbedCodeEditor : VisualPanel
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private readonly Collection<KryptonCodeEditor> _editorControls;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonNavigatorTabbedCodeEditor class.
    /// </summary>
    public KryptonNavigatorTabbedCodeEditor()
    {
        _navigator = new KryptonNavigator
        {
            Dock = DockStyle.Fill,
            NavigatorMode = NavigatorMode.BarTabGroup
        };

        _navigator.Button.CloseButtonDisplay = ButtonDisplay.ShowEnabled;
        _navigator.Button.CloseButtonAction = CloseButtonAction.RemovePageAndDispose;

        _navigator.SelectedPageChanged += OnNavigatorSelectedPageChanged;
        _navigator.CloseAction += OnNavigatorCloseAction;

        _editorControls = new Collection<KryptonCodeEditor>();

        Controls.Add(_navigator);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _navigator.SelectedPageChanged -= OnNavigatorSelectedPageChanged;
            _navigator.CloseAction -= OnNavigatorCloseAction;
            _editorControls.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the underlying KryptonNavigator.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonNavigator Navigator => _navigator;

    /// <summary>
    /// Gets the collection of pages (tabs) in this tabbed code editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPageCollection Pages => _navigator.Pages;

    /// <summary>
    /// Gets the collection of code editor controls (KryptonCodeEditor) in this tabbed editor.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReadOnlyCollection<KryptonCodeEditor> EditorControls => new ReadOnlyCollection<KryptonCodeEditor>(_editorControls);

    /// <summary>
    /// Gets or sets the index of the currently selected tab page.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int SelectedIndex
    {
        get => _navigator.SelectedIndex;
        set => _navigator.SelectedIndex = value;
    }

    /// <summary>
    /// Gets or sets the currently selected tab page.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public KryptonPage? SelectedPage
    {
        get => _navigator.SelectedPage;
        set => _navigator.SelectedPage = value;
    }

    /// <summary>
    /// Gets the currently selected code editor control (KryptonCodeEditor).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonCodeEditor? SelectedEditor
    {
        get
        {
            if (SelectedIndex >= 0 && SelectedIndex < _editorControls.Count)
            {
                return _editorControls[SelectedIndex];
            }
            return null;
        }
    }

    /// <summary>
    /// Adds a new tab page with a KryptonCodeEditor.
    /// </summary>
    /// <param name="tabText">The text to display on the tab.</param>
    /// <returns>The newly created KryptonCodeEditor control.</returns>
    public KryptonCodeEditor AddTab(string tabText)
    {
        return AddTab(tabText, null, Language.None);
    }

    /// <summary>
    /// Adds a new tab page with a KryptonCodeEditor.
    /// </summary>
    /// <param name="tabText">The text to display on the tab.</param>
    /// <param name="initialText">The initial text content for the editor.</param>
    /// <returns>The newly created KryptonCodeEditor control.</returns>
    public KryptonCodeEditor AddTab(string tabText, string? initialText)
    {
        return AddTab(tabText, initialText, Language.None);
    }

    /// <summary>
    /// Adds a new tab page with a KryptonCodeEditor and optional syntax language.
    /// </summary>
    /// <param name="tabText">The text to display on the tab.</param>
    /// <param name="initialText">The initial text content for the editor.</param>
    /// <param name="language">The programming language for syntax highlighting.</param>
    /// <returns>The newly created KryptonCodeEditor control.</returns>
    public KryptonCodeEditor AddTab(string tabText, string? initialText, Language language)
    {
        var page = new KryptonPage
        {
            Text = tabText,
            TextTitle = tabText,
            TextDescription = string.Empty,
            UniqueName = Guid.NewGuid().ToString("N")
        };

        var editor = new KryptonCodeEditor
        {
            Dock = DockStyle.Fill
        };

        if (language != Language.None)
        {
            editor.Language = language;
        }

        if (!string.IsNullOrEmpty(initialText))
        {
            editor.Text = initialText;
        }

        page.Controls.Add(editor);

        var closeButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipTitle = "Close",
            ToolTipBody = $"Close {tabText}"
        };
        page.ButtonSpecs!.Add(closeButton);

        _navigator.Pages.Add(page);
        _editorControls.Add(editor);

        return editor;
    }

    /// <summary>
    /// Removes a tab page and its associated code editor control.
    /// </summary>
    /// <param name="index">The index of the tab page to remove.</param>
    public void RemoveTab(int index)
    {
        if (index >= 0 && index < _navigator.Pages.Count)
        {
            if (index < _editorControls.Count)
            {
                _editorControls.RemoveAt(index);
            }
            var page = _navigator.Pages[index];
            _navigator.Pages.Remove(page);
            page.Dispose();
        }
    }

    /// <summary>
    /// Removes a tab page and its associated code editor control.
    /// </summary>
    /// <param name="page">The tab page to remove.</param>
    public void RemoveTab(KryptonPage page)
    {
        var index = _navigator.Pages.IndexOf(page);
        if (index >= 0)
        {
            RemoveTab(index);
        }
    }

    /// <summary>
    /// Removes all tab pages and their associated code editor controls.
    /// </summary>
    public void ClearTabs()
    {
        _editorControls.Clear();
        foreach (KryptonPage page in _navigator.Pages)
        {
            page.Dispose();
        }
        _navigator.Pages.Clear();
    }

    /// <summary>
    /// Gets the code editor control (KryptonCodeEditor) for a specific tab page.
    /// </summary>
    /// <param name="index">The index of the tab page.</param>
    /// <returns>The KryptonCodeEditor control, or null if not found.</returns>
    public KryptonCodeEditor? GetEditor(int index)
    {
        if (index >= 0 && index < _editorControls.Count)
        {
            return _editorControls[index];
        }
        return null;
    }

    /// <summary>
    /// Gets the code editor control (KryptonCodeEditor) for a specific tab page.
    /// </summary>
    /// <param name="page">The tab page.</param>
    /// <returns>The KryptonCodeEditor control, or null if not found.</returns>
    public KryptonCodeEditor? GetEditor(KryptonPage page)
    {
        var index = _navigator.Pages.IndexOf(page);
        return index >= 0 ? GetEditor(index) : null;
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for NavigatorTabbedCodeEditor
    }
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the SelectedIndex property has changed.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the SelectedIndex property has changed.")]
    public event EventHandler? SelectedIndexChanged;
    #endregion

    #region Implementation
    private void OnNavigatorSelectedPageChanged(object? sender, EventArgs e)
    {
        SelectedIndexChanged?.Invoke(this, e);
    }

    private void OnNavigatorCloseAction(object? sender, CloseActionEventArgs e)
    {
        if (e.Action == CloseButtonAction.RemovePageAndDispose || e.Action == CloseButtonAction.RemovePage)
        {
            var index = e.Index;
            if (index >= 0 && index < _editorControls.Count)
            {
                _editorControls.RemoveAt(index);
            }
        }
    }
    #endregion
}
