#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Navigator.Utilities;

namespace TestForm;

public partial class KryptonNavigatorTabbedEditorDemo : KryptonForm
{
    private int _tabCounter = 1;

    public KryptonNavigatorTabbedEditorDemo()
    {
        InitializeComponent();
        InitializeExample();
    }

    private void InitializeExample()
    {
        // Add initial tabs with sample content
        _ = navigatorTabbedEditor.AddTab("Welcome", "Welcome to KryptonNavigatorTabbedEditor!\n\nThis control uses KryptonNavigator with a KryptonRichTextBox on each tab page.\n\nFeatures:\n- Tab management (add, close, clear)\n- Close button on each tab\n- Rich text editing with full Krypton styling\n- Navigator modes (Bar Tab Group, Bar Tab Only, etc.)\n- Editor operations (undo, redo, copy, cut, paste)\n- Text operations (find, get/set text)");

        _ = navigatorTabbedEditor.AddTab("Features", "Key Features:\n\n✓ KryptonNavigator-based tabs (BarTabGroup mode)\n✓ One KryptonRichTextBox per page\n✓ Per-tab close ButtonSpec\n✓ Pages collection (KryptonPageCollection)\n✓ SelectedPage, SelectedEditor, GetEditor(index)\n✓ AddTab(text), AddTab(text, initialText)\n✓ RemoveTab(index), ClearTabs()\n✓ SelectedIndexChanged event");

        _ = navigatorTabbedEditor.AddTab("Sample Code", "// Krypton.Navigator.Utilities\nvar editor = new KryptonNavigatorTabbedEditor();\neditor.Dock = DockStyle.Fill;\n\n// Add tabs\nvar rtb1 = editor.AddTab(\"Document 1\");\nvar rtb2 = editor.AddTab(\"Document 2\", \"Initial text\");\n\n// Access selected editor\nvar current = editor.SelectedEditor;\nvar page = editor.SelectedPage;\n\n// Navigator (for advanced settings)\neditor.Navigator.NavigatorMode = NavigatorMode.BarTabGroup;");

        navigatorTabbedEditor.SelectedIndex = 0;
        InitializeNavigatorModeCombo();
        UpdateStatus();
    }

    private void InitializeNavigatorModeCombo()
    {
        kcbNavigatorMode.Items.Clear();
        kcbNavigatorMode.Items.Add("BarTabGroup");
        kcbNavigatorMode.Items.Add("BarTabOnly");
        kcbNavigatorMode.Items.Add("BarRibbonTabGroup");
        kcbNavigatorMode.Items.Add("BarRibbonTabOnly");
        kcbNavigatorMode.Items.Add("BarCheckButtonGroupOnly");
        kcbNavigatorMode.Items.Add("BarCheckButtonOnly");
        kcbNavigatorMode.Items.Add("HeaderBarCheckButtonOnly");
        kcbNavigatorMode.Items.Add("OutlookFull");
        kcbNavigatorMode.Items.Add("OutlookMini");
        kcbNavigatorMode.Items.Add("Panel");
        kcbNavigatorMode.Items.Add("Group");
        kcbNavigatorMode.Items.Add("HeaderGroup");
        kcbNavigatorMode.SelectedIndex = 0;
    }

    private void UpdateStatus()
    {
        var count = navigatorTabbedEditor.Pages.Count;
        var selected = navigatorTabbedEditor.SelectedIndex;
        var selectedPage = navigatorTabbedEditor.SelectedPage;

        kryptonLabelStatus.Text = $"Tabs: {count} | Selected: {(selected >= 0 ? selectedPage?.Text : "None")} (Index: {selected})";

        kbtnCloseTab.Enabled = count > 0 && selected >= 0;
        kbtnClearTabs.Enabled = count > 0;
    }

    private void KbtnAddTab_Click(object? sender, EventArgs e)
    {
        var tabName = $"Document {_tabCounter++}";
        var initialText = ktxtInitialText.Text;

        var editor = string.IsNullOrWhiteSpace(initialText)
            ? navigatorTabbedEditor.AddTab(tabName)
            : navigatorTabbedEditor.AddTab(tabName, initialText);

        navigatorTabbedEditor.SelectedIndex = navigatorTabbedEditor.Pages.Count - 1;
        editor?.Focus();
        UpdateStatus();
    }

    private void KbtnCloseTab_Click(object? sender, EventArgs e)
    {
        var selectedIndex = navigatorTabbedEditor.SelectedIndex;
        if (selectedIndex >= 0 && selectedIndex < navigatorTabbedEditor.Pages.Count)
        {
            navigatorTabbedEditor.RemoveTab(selectedIndex);
        }
        UpdateStatus();
    }

    private void KbtnClearTabs_Click(object? sender, EventArgs e)
    {
        var result = KryptonMessageBox.Show(
            "Are you sure you want to close all tabs?",
            "Clear All Tabs",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            navigatorTabbedEditor.ClearTabs();
            _tabCounter = 1;
            UpdateStatus();
        }
    }

    private void NavigatorTabbedEditor_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateStatus();
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null)
        {
            kryptonLabelEditorInfo.Text = $"Text Length: {editor.TextLength} | Lines: {editor.Lines.Length} | Modified: {editor.Modified}";
        }
        else
        {
            kryptonLabelEditorInfo.Text = "No editor selected";
        }
    }

    private void KcbNavigatorMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (Enum.TryParse<NavigatorMode>(kcbNavigatorMode.SelectedItem?.ToString(), out var mode))
        {
            navigatorTabbedEditor.Navigator.NavigatorMode = mode;
        }
    }

    private void KbtnGetSelectedText_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null)
        {
            var selectedText = editor.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                KryptonMessageBox.Show($"Selected Text:\n\n{selectedText}", "Selected Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
            else
            {
                KryptonMessageBox.Show("No text is currently selected.", "Selected Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
        }
        else
        {
            KryptonMessageBox.Show("No tab is currently selected.", "Selected Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void KbtnGetAllText_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null)
        {
            var text = editor.Text;
            var preview = text.Length > 200 ? text.Substring(0, 200) + "..." : text;
            KryptonMessageBox.Show($"Editor Text ({text.Length} characters):\n\n{preview}", "Editor Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
        else
        {
            KryptonMessageBox.Show("No tab is currently selected.", "Editor Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void KbtnSetText_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null)
        {
            editor.Text = ktxtSetText.Text;
            UpdateStatus();
        }
        else
        {
            KryptonMessageBox.Show("No tab is currently selected.", "Set Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void KbtnFindText_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null)
        {
            var searchText = ktxtFindText.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                KryptonMessageBox.Show("Please enter text to search for.", "Find Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            var position = editor.Find(searchText);
            if (position >= 0)
            {
                editor.SelectionStart = position;
                editor.SelectionLength = searchText.Length;
                editor.ScrollToCaret();
                KryptonMessageBox.Show($"Found '{searchText}' at position {position}.", "Find Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
            else
            {
                KryptonMessageBox.Show($"Text '{searchText}' not found.", "Find Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
        }
        else
        {
            KryptonMessageBox.Show("No tab is currently selected.", "Find Text", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
        }
    }

    private void KbtnUndo_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null && editor.CanUndo)
        {
            editor.Undo();
        }
    }

    private void KbtnRedo_Click(object? sender, EventArgs e)
    {
        var editor = navigatorTabbedEditor.SelectedEditor;
        if (editor != null && editor.CanRedo)
        {
            editor.Redo();
        }
    }

    private void KbtnCopy_Click(object? sender, EventArgs e)
    {
        navigatorTabbedEditor.SelectedEditor?.Copy();
    }

    private void KbtnCut_Click(object? sender, EventArgs e)
    {
        navigatorTabbedEditor.SelectedEditor?.Cut();
    }

    private void KbtnPaste_Click(object? sender, EventArgs e)
    {
        navigatorTabbedEditor.SelectedEditor?.Paste();
    }

    private void KbtnSelectAll_Click(object? sender, EventArgs e)
    {
        navigatorTabbedEditor.SelectedEditor?.SelectAll();
    }
}
