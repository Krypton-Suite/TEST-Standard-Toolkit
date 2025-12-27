#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TestForm;

public partial class MarkdownEditorViewer : KryptonForm
{
    private string _currentFile;

    public MarkdownEditorViewer()
    {
        InitializeComponent();
        InitializeEditor();
        UpdateUIState();
    }

    private void InitializeEditor()
    {
        // Wire up editor and preview
        kmdEditor.TextChanged += (s, e) => kmdPreview.MarkdownText = kmdEditor.Text;
        
        // Initialize preview mode combo
        kcmbPreviewMode.Items.Clear();
        kcmbPreviewMode.Items.Add("Custom Krypton");
        kcmbPreviewMode.Items.Add("HTML Preview");
        kcmbPreviewMode.SelectedIndex = 0;
        
        // Initialize palette mode combo
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0;
        
        // Set up initial content
        var sampleMarkdown = @"# Markdown Editor/Viewer

This is a **markdown editor** with full Krypton theming support.

## Features

- Custom Krypton rendering
- HTML preview with CSS theming
- Live preview updates
- Full palette integration

### Code Block

```csharp
public class Example
{
    public void DoSomething() { }
}
```

> This is a blockquote example.

**Bold text** and *italic text* and ~~strikethrough~~.

[Link example](https://example.com)

---

## Lists

1. First item
2. Second item
3. Third item

- Unordered item
- Another item
";
        kmdEditor.Text = sampleMarkdown;
        kmdPreview.MarkdownText = sampleMarkdown;
    }


    private string ConvertMarkdownToHtml(string markdown)
    {
        var html = new StringBuilder();
        var parser = new MarkdownParser();
        var elements = parser.Parse(markdown);

        foreach (var element in elements)
        {
            html.AppendLine(element.Type switch
            {
                MarkdownElementType.Heading1 => $"<h1>{EscapeHtml(element.Text)}</h1>",
                MarkdownElementType.Heading2 => $"<h2>{EscapeHtml(element.Text)}</h2>",
                MarkdownElementType.Heading3 => $"<h3>{EscapeHtml(element.Text)}</h3>",
                MarkdownElementType.Paragraph => $"<p>{ProcessInlineHtml(element.Text)}</p>",
                MarkdownElementType.CodeBlock => $"<pre><code>{EscapeHtml(element.Text)}</code></pre>",
                MarkdownElementType.Blockquote => $"<blockquote>{EscapeHtml(element.Text)}</blockquote>",
                MarkdownElementType.UnorderedList => CreateHtmlList(element.Items ?? new List<string>(), false),
                MarkdownElementType.OrderedList => CreateHtmlList(element.Items ?? new List<string>(), true),
                MarkdownElementType.HorizontalRule => "<hr>",
                _ => ""
            });
        }

        return html.ToString();
    }

    private string CreateHtmlList(List<string> items, bool ordered)
    {
        var tag = ordered ? "ol" : "ul";
        var html = new StringBuilder($"<{tag}>");
        foreach (var item in items)
        {
            html.AppendLine($"<li>{ProcessInlineHtml(item)}</li>");
        }
        html.AppendLine($"</{tag}>");
        return html.ToString();
    }

    private string ProcessInlineHtml(string text)
    {
        text = Regex.Replace(text, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
        text = Regex.Replace(text, @"\*(.*?)\*", "<em>$1</em>");
        text = Regex.Replace(text, @"~~(.*?)~~", "<del>$1</del>");
        text = Regex.Replace(text, @"`(.*?)`", "<code>$1</code>");
        text = Regex.Replace(text, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");
        return text;
    }

    private string EscapeHtml(string text)
    {
        return text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }

    private string GenerateKryptonCss()
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        
        // Get colors from current palette
        var backColor = palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        var foreColor = palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);
        var headingColor = palette.GetContentShortTextColor1(PaletteContentStyle.LabelTitlePanel, PaletteState.Normal);
        var borderColor = palette.GetBorderColor1(PaletteBorderStyle.HeaderPrimary, PaletteState.Normal);
        var linkColor = palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkNotVisitedOverride);
        var codeBackColor = palette.GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal);
        
        var font = palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);
        var headingFont = palette.GetContentShortTextFont(PaletteContentStyle.LabelTitlePanel, PaletteState.Normal);

        return $@"
        body {{
            font-family: '{font?.FontFamily.Name ?? "Segoe UI"}', sans-serif;
            font-size: {font?.Size ?? 9}pt;
            color: {ColorToHex(foreColor)};
            background-color: {ColorToHex(backColor)};
            padding: 20px;
            line-height: 1.6;
        }}
        h1 {{
            font-family: '{headingFont?.FontFamily.Name ?? "Segoe UI"}', sans-serif;
            font-size: 24pt;
            font-weight: bold;
            color: {ColorToHex(headingColor)};
            margin-top: 20px;
            margin-bottom: 10px;
        }}
        h2 {{
            font-family: '{headingFont?.FontFamily.Name ?? "Segoe UI"}', sans-serif;
            font-size: 20pt;
            font-weight: bold;
            color: {ColorToHex(headingColor)};
            margin-top: 18px;
            margin-bottom: 8px;
        }}
        h3 {{
            font-family: '{headingFont?.FontFamily.Name ?? "Segoe UI"}', sans-serif;
            font-size: 16pt;
            font-weight: bold;
            color: {ColorToHex(headingColor)};
            margin-top: 16px;
            margin-bottom: 6px;
        }}
        p {{
            margin: 10px 0;
        }}
        code {{
            font-family: 'Consolas', monospace;
            background-color: {ColorToHex(codeBackColor)};
            padding: 2px 4px;
            border-radius: 3px;
        }}
        pre {{
            background-color: {ColorToHex(codeBackColor)};
            padding: 10px;
            border-radius: 5px;
            overflow-x: auto;
        }}
        pre code {{
            background-color: transparent;
            padding: 0;
        }}
        blockquote {{
            border-left: 3px solid {ColorToHex(borderColor)};
            padding-left: 15px;
            margin: 15px 0;
            font-style: italic;
            color: {ColorToHex(foreColor)};
        }}
        ul, ol {{
            margin: 10px 0;
            padding-left: 30px;
        }}
        li {{
            margin: 5px 0;
        }}
        a {{
            color: {ColorToHex(linkColor)};
            text-decoration: none;
        }}
        a:hover {{
            text-decoration: underline;
        }}
        hr {{
            border: none;
            border-top: 1px solid {ColorToHex(borderColor)};
            margin: 20px 0;
        }}
        strong {{
            font-weight: bold;
        }}
        em {{
            font-style: italic;
        }}
        del {{
            text-decoration: line-through;
        }}
";
    }

    private string ColorToHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    private void UpdateUIState()
    {
        klblStatus.Text = _currentFile != null ? $"File: {Path.GetFileName(_currentFile)}" : "New Document";
        kbtnSave.Enabled = _currentFile != null;
    }

    private void kbtnBold_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertBold();
    }

    private void kbtnItalic_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertItalic();
    }

    private void kbtnHeading1_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertHeading1();
    }

    private void kbtnHeading2_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertHeading2();
    }

    private void kbtnHeading3_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertHeading3();
    }

    private void kbtnCodeBlock_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertCodeBlock();
    }

    private void kbtnLink_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertLink();
    }

    private void kbtnList_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertUnorderedList();
    }

    private void kbtnNumberedList_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertOrderedList();
    }

    private void kbtnBlockquote_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertBlockquote();
    }

    private void kbtnHorizontalRule_Click(object sender, EventArgs e)
    {
        kmdEditor.InsertHorizontalRule();
    }

    private void kbtnOpen_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonOpenFileDialog
        {
            Filter = "Markdown Files (*.md)|*.md|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Open Markdown File"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                kmdEditor.Text = File.ReadAllText(dialog.FileName);
                kmdPreview.MarkdownText = kmdEditor.Text;
                _currentFile = dialog.FileName;
                UpdateUIState();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error opening file:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kbtnSave_Click(object sender, EventArgs e)
    {
        if (_currentFile != null)
        {
            try
            {
                File.WriteAllText(_currentFile, kmdEditor.Text);
                KryptonMessageBox.Show(this,
                    "File saved successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error saving file:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
        else
        {
            kbtnSaveAs_Click(sender, e);
        }
    }

    private void kbtnSaveAs_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonSaveFileDialog
        {
            Filter = "Markdown Files (*.md)|*.md|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Save Markdown File",
            FileName = _currentFile ?? "untitled.md"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                File.WriteAllText(dialog.FileName, kmdEditor.Text);
                _currentFile = dialog.FileName;
                UpdateUIState();
                KryptonMessageBox.Show(this,
                    "File saved successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error saving file:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kbtnExportHtml_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonSaveFileDialog
        {
            Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*",
            Title = "Export to HTML",
            FileName = Path.ChangeExtension(_currentFile ?? "export", ".html")
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var html = ConvertMarkdownToHtml(kmdEditor.Text);
                var css = GenerateKryptonCss();
                var fullHtml = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>{Path.GetFileNameWithoutExtension(dialog.FileName)}</title>
    <style>
{css}
    </style>
</head>
<body>
{html}
</body>
</html>";

                File.WriteAllText(dialog.FileName, fullHtml);
                KryptonMessageBox.Show(this,
                    "HTML exported successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error exporting HTML:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kcmbPreviewMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = kcmbPreviewMode.SelectedItem?.ToString();
        kmdPreview.PreviewMode = selected switch
        {
            "Custom Krypton" => Krypton.Utilities.PreviewMode.Custom,
            "HTML Preview" => Krypton.Utilities.PreviewMode.Html,
            _ => Krypton.Utilities.PreviewMode.Custom
        };
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };

        // Update preview to reflect new palette
        kmdPreview.RefreshPreview();
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}

// Simple Markdown Parser (for HTML export)
internal class MarkdownParser
{
    public List<MarkdownElement> Parse(string markdown)
    {
        var elements = new List<MarkdownElement>();
        var lines = markdown.Split('\n');
        var i = 0;

        while (i < lines.Length)
        {
            var line = lines[i].TrimEnd();

            if (string.IsNullOrWhiteSpace(line))
            {
                i++;
                continue;
            }

            if (line.StartsWith("# "))
            {
                elements.Add(new MarkdownElement(MarkdownElementType.Heading1, line.Substring(2)));
                i++;
            }
            else if (line.StartsWith("## "))
            {
                elements.Add(new MarkdownElement(MarkdownElementType.Heading2, line.Substring(3)));
                i++;
            }
            else if (line.StartsWith("### "))
            {
                elements.Add(new MarkdownElement(MarkdownElementType.Heading3, line.Substring(4)));
                i++;
            }
            else if (line.StartsWith("```"))
            {
                var code = new StringBuilder();
                i++; // Skip opening ```
                while (i < lines.Length && !lines[i].Trim().StartsWith("```"))
                {
                    code.AppendLine(lines[i]);
                    i++;
                }
                if (i < lines.Length) i++; // Skip closing ```
                elements.Add(new MarkdownElement(MarkdownElementType.CodeBlock, code.ToString().TrimEnd()));
            }
            else if (line.StartsWith("> "))
            {
                elements.Add(new MarkdownElement(MarkdownElementType.Blockquote, line.Substring(2)));
                i++;
            }
            else if (line.StartsWith("- ") || line.StartsWith("* "))
            {
                var items = new List<string>();
                while (i < lines.Length && (lines[i].TrimStart().StartsWith("- ") || lines[i].TrimStart().StartsWith("* ")))
                {
                    items.Add(lines[i].TrimStart().Substring(2).Trim());
                    i++;
                }
                elements.Add(new MarkdownElement(MarkdownElementType.UnorderedList, null, items));
            }
            else if (Regex.IsMatch(line, @"^\d+\.\s"))
            {
                var items = new List<string>();
                while (i < lines.Length && Regex.IsMatch(lines[i].TrimStart(), @"^\d+\.\s"))
                {
                    var match = Regex.Match(lines[i].TrimStart(), @"^\d+\.\s(.*)");
                    if (match.Success)
                    {
                        items.Add(match.Groups[1].Value);
                    }
                    i++;
                }
                elements.Add(new MarkdownElement(MarkdownElementType.OrderedList, null, items));
            }
            else if (line.StartsWith("---") || line.StartsWith("***"))
            {
                elements.Add(new MarkdownElement(MarkdownElementType.HorizontalRule, ""));
                i++;
            }
            else
            {
                // Regular paragraph
                var para = new StringBuilder(line);
                i++;
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]) && 
                       !lines[i].TrimStart().StartsWith("#") &&
                       !lines[i].TrimStart().StartsWith("-") &&
                       !lines[i].TrimStart().StartsWith("*") &&
                       !lines[i].TrimStart().StartsWith(">") &&
                       !lines[i].TrimStart().StartsWith("```") &&
                       !Regex.IsMatch(lines[i].TrimStart(), @"^\d+\.\s") &&
                       !lines[i].TrimStart().StartsWith("---"))
                {
                    para.Append(" ").Append(lines[i].TrimEnd());
                    i++;
                }
                elements.Add(new MarkdownElement(MarkdownElementType.Paragraph, para.ToString()));
            }
        }

        return elements;
    }
}

internal enum MarkdownElementType
{
    Heading1,
    Heading2,
    Heading3,
    Paragraph,
    CodeBlock,
    Blockquote,
    UnorderedList,
    OrderedList,
    HorizontalRule
}

internal class MarkdownElement
{
    public MarkdownElementType Type { get; }
    public string Text { get; }
    public List<string>? Items { get; }

    public MarkdownElement(MarkdownElementType type, string? text, List<string>? items = null)
    {
        Type = type;
        Text = text ?? "";
        Items = items;
    }
}

