#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace Krypton.Utilities;

/// <summary>
/// Provides a markdown editor control with Krypton styling.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRichTextBox), "ToolboxBitmaps.KryptonRichTextBox.bmp")]
[DefaultEvent(nameof(TextChanged))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Provides a markdown editor control with Krypton styling and formatting capabilities.")]
public class KryptonMarkdownEditor : VisualControlBase,
    IContainedInputControl
{
    #region Instance Fields
    private KryptonRichTextBox _richTextBox;
    private bool _wordWrap;
    private Font _editorFont;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonMarkdownEditor class.
    /// </summary>
    public KryptonMarkdownEditor()
    {
        // Default properties
        _wordWrap = false;
        _editorFont = new Font("Consolas", 10F);
        
        // Create the internal rich text box
        _richTextBox = new KryptonRichTextBox
        {
            Dock = DockStyle.Fill,
            WordWrap = _wordWrap,
            Font = _editorFont
        };
        
        _richTextBox.TextChanged += OnRichTextBoxTextChanged;
        
        // Add to controls
        Controls.Add(_richTextBox);
        
        // Set default size
        Size = new Size(300, 200);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the markdown text content.
    /// </summary>
    [Bindable(true)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue("")]
    [Localizable(true)]
    public override string Text
    {
        get => _richTextBox.Text;
        set => _richTextBox.Text = value;
    }

    /// <summary>
    /// Gets and sets whether text wraps to multiple lines.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Indicates whether text wraps to multiple lines.")]
    public bool WordWrap
    {
        get => _wordWrap;
        set
        {
            if (_wordWrap != value)
            {
                _wordWrap = value;
                _richTextBox.WordWrap = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the font used for editing.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Font), "Consolas, 10pt")]
    [Description(@"The font used for editing markdown text.")]
    public Font EditorFont
    {
        get => _editorFont;
        set
        {
            if (_editorFont != value)
            {
                _editorFont?.Dispose();
                _editorFont = value;
                if (_richTextBox != null)
                {
                    _richTextBox.Font = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets the underlying KryptonRichTextBox control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRichTextBox RichTextBox => _richTextBox;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control ContainedControl => RichTextBox;

    /// <summary>
    /// Gets or sets the selected text.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
        get => _richTextBox.SelectedText;
        set => _richTextBox.SelectedText = value;
    }

    /// <summary>
    /// Gets or sets the starting point of text selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
        get => _richTextBox.SelectionStart;
        set => _richTextBox.SelectionStart = value;
    }

    /// <summary>
    /// Gets or sets the number of characters selected in the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
        get => _richTextBox.SelectionLength;
        set => _richTextBox.SelectionLength = value;
    }

    /// <summary>
    /// Inserts bold markdown formatting around the selected text.
    /// </summary>
    public void InsertBold()
    {
        InsertMarkdown("**", "**");
    }

    /// <summary>
    /// Inserts italic markdown formatting around the selected text.
    /// </summary>
    public void InsertItalic()
    {
        InsertMarkdown("*", "*");
    }

    /// <summary>
    /// Inserts a heading 1 markdown at the start of the current line.
    /// </summary>
    public void InsertHeading1()
    {
        InsertMarkdownAtLineStart("# ");
    }

    /// <summary>
    /// Inserts a heading 2 markdown at the start of the current line.
    /// </summary>
    public void InsertHeading2()
    {
        InsertMarkdownAtLineStart("## ");
    }

    /// <summary>
    /// Inserts a heading 3 markdown at the start of the current line.
    /// </summary>
    public void InsertHeading3()
    {
        InsertMarkdownAtLineStart("### ");
    }

    /// <summary>
    /// Inserts a code block markdown around the selected text.
    /// </summary>
    public void InsertCodeBlock()
    {
        var selection = _richTextBox.SelectedText;
        if (!string.IsNullOrEmpty(selection))
        {
            _richTextBox.SelectedText = $"```\n{selection}\n```";
        }
        else
        {
            InsertMarkdown("```\n", "\n```");
        }
    }

    /// <summary>
    /// Inserts a link markdown template.
    /// </summary>
    public void InsertLink()
    {
        InsertMarkdown("[", "]()");
        _richTextBox.SelectionStart -= 2;
        _richTextBox.SelectionLength = 0;
    }

    /// <summary>
    /// Inserts an unordered list markdown at the start of the current line.
    /// </summary>
    public void InsertUnorderedList()
    {
        InsertMarkdownAtLineStart("- ");
    }

    /// <summary>
    /// Inserts an ordered list markdown at the start of the current line.
    /// </summary>
    public void InsertOrderedList()
    {
        InsertMarkdownAtLineStart("1. ");
    }

    /// <summary>
    /// Inserts a blockquote markdown at the start of the current line.
    /// </summary>
    public void InsertBlockquote()
    {
        InsertMarkdownAtLineStart("> ");
    }

    /// <summary>
    /// Inserts a horizontal rule markdown.
    /// </summary>
    public void InsertHorizontalRule()
    {
        var pos = _richTextBox.SelectionStart;
        var text = _richTextBox.Text;
        var lineStart = text.LastIndexOf('\n', pos - 1) + 1;
        var lineEnd = text.IndexOf('\n', pos);
        if (lineEnd == -1) lineEnd = text.Length;
        
        var line = text.Substring(lineStart, lineEnd - lineStart);
        if (string.IsNullOrWhiteSpace(line))
        {
            _richTextBox.SelectedText = "---\n";
        }
        else
        {
            _richTextBox.SelectedText = "\n---\n";
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnTextChanged(EventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _editorFont?.Dispose();
        }
        base.Dispose(disposing);
    }
    #endregion

    #region Private
    private void OnRichTextBoxTextChanged(object? sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    private void InsertMarkdown(string prefix, string suffix)
    {
        var selection = _richTextBox.SelectedText;
        if (!string.IsNullOrEmpty(selection))
        {
            _richTextBox.SelectedText = prefix + selection + suffix;
            _richTextBox.SelectionStart += prefix.Length + selection.Length + suffix.Length;
        }
        else
        {
            _richTextBox.SelectedText = prefix + suffix;
            _richTextBox.SelectionStart -= suffix.Length;
        }
        _richTextBox.Focus();
    }

    private void InsertMarkdownAtLineStart(string prefix)
    {
        var pos = _richTextBox.SelectionStart;
        var text = _richTextBox.Text;
        var lineStart = text.LastIndexOf('\n', pos - 1) + 1;
        
        _richTextBox.SelectionStart = lineStart;
        _richTextBox.SelectionLength = 0;
        _richTextBox.SelectedText = prefix;
        _richTextBox.SelectionStart = pos + prefix.Length;
        _richTextBox.Focus();
    }
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the Text property value changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Text property value changes.")]
    public new event EventHandler? TextChanged;
    #endregion
}

