#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class TabbedEditorTest : KryptonForm
{
    private int _tabCounter = 1;

    public TabbedEditorTest()
    {
        InitializeComponent();
        InitializeExample();
    }

    private void InitializeExample()
    {
        // Add some initial tabs with sample content
        var tab1 = kryptonTabbedEditor1.AddTab("Welcome", "Welcome to KryptonTabbedEditor!\n\nThis is a comprehensive example demonstrating:\n- Tab management\n- Close buttons\n- Rich text editing\n- Event handling");
        
        var tab2 = kryptonTabbedEditor1.AddTab("Features", "Key Features:\n\n✓ Multiple tabs with RichTextBox editors\n✓ Close buttons on each tab\n✓ Krypton theming support\n✓ Programmatic tab management\n✓ Event handling\n✓ Customizable appearance");
        
        var tab3 = kryptonTabbedEditor1.AddTab("Sample Code", "// Example usage:\nvar editor = new KryptonTabbedEditor();\neditor.Dock = DockStyle.Fill;\n\n// Add tabs\nvar tab1 = editor.AddTab(\"Document 1\");\nvar tab2 = editor.AddTab(\"Document 2\", \"Initial text\");\n\n// Access selected editor\nvar current = editor.SelectedEditor;\nif (current != null)\n{\n    current.Text = \"Modified content\";\n}");

        // Set initial selection
        kryptonTabbedEditor1.SelectedIndex = 0;

        // Update status
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        var count = kryptonTabbedEditor1.TabPages.Count;
        var selected = kryptonTabbedEditor1.SelectedIndex;
        var selectedTab = kryptonTabbedEditor1.SelectedTab;
        
        kryptonLabelStatus.Text = $"Tabs: {count} | Selected: {(selected >= 0 ? selectedTab?.Text : "None")} (Index: {selected})";
        
        // Update button states
        kbtnCloseTab.Enabled = count > 0 && selected >= 0;
        kbtnClearTabs.Enabled = count > 0;
    }

    private void KbtnAddTab_Click(object sender, EventArgs e)
    {
        var tabName = $"Document {_tabCounter++}";
        var initialText = ktxtInitialText.Text;
        
        var editor = string.IsNullOrWhiteSpace(initialText)
            ? kryptonTabbedEditor1.AddTab(tabName)
            : kryptonTabbedEditor1.AddTab(tabName, initialText);
        
        // Select the newly added tab
        kryptonTabbedEditor1.SelectedIndex = kryptonTabbedEditor1.TabPages.Count - 1;
        
        // Focus the editor
        editor?.Focus();
        
        UpdateStatus();
    }

    private void KbtnCloseTab_Click(object sender, EventArgs e)
    {
        var selectedIndex = kryptonTabbedEditor1.SelectedIndex;
        if (selectedIndex >= 0 && selectedIndex < kryptonTabbedEditor1.TabPages.Count)
        {
            kryptonTabbedEditor1.RemoveTab(selectedIndex);
            // Status will be updated by SelectedIndexChanged event
        }
    }

    private void KbtnClearTabs_Click(object sender, EventArgs e)
    {
        var result = KryptonMessageBox.Show(
            "Are you sure you want to close all tabs?",
            "Clear All Tabs",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);
        
        if (result == DialogResult.Yes)
        {
            kryptonTabbedEditor1.ClearTabs();
            _tabCounter = 1; // Reset counter
            // Status will be updated by SelectedIndexChanged event
            UpdateStatus(); // But update immediately since selection might not change
        }
    }

    private void KryptonTabbedEditor1_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateStatus();
        
        // Update editor info
        var editor = kryptonTabbedEditor1.SelectedEditor;
        if (editor != null)
        {
            kryptonLabelEditorInfo.Text = $"Text Length: {editor.TextLength} | Lines: {editor.Lines.Length} | Modified: {editor.Modified}";
        }
        else
        {
            kryptonLabelEditorInfo.Text = "No editor selected";
        }
    }

    private void KcbTabStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Enum.TryParse<TabStyle>(kcbTabStyle.SelectedItem?.ToString(), out var style))
        {
            kryptonTabbedEditor1.TabStyle = style;
        }
    }

    private void KcbTabAlignment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Enum.TryParse<TabAlignment>(kcbTabAlignment.SelectedItem?.ToString(), out var alignment))
        {
            kryptonTabbedEditor1.Alignment = alignment;
        }
    }

    private void KbtnGetSelectedText_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
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

    private void KbtnGetAllText_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
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

    private void KbtnSetText_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
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

    private void KbtnFindText_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
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

    private void KbtnUndo_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        if (editor != null && editor.CanUndo)
        {
            editor.Undo();
        }
    }

    private void KbtnRedo_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        if (editor != null && editor.CanRedo)
        {
            editor.Redo();
        }
    }

    private void KbtnCopy_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        editor?.Copy();
    }

    private void KbtnCut_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        editor?.Cut();
    }

    private void KbtnPaste_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        editor?.Paste();
    }

    private void KbtnSelectAll_Click(object sender, EventArgs e)
    {
        var editor = kryptonTabbedEditor1.SelectedEditor;
        editor?.SelectAll();
    }

}
