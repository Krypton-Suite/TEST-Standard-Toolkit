#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace Krypton.Utilities;

/// <summary>
/// Provides a markdown preview control with Krypton styling support.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultProperty(nameof(MarkdownText))]
[DesignerCategory(@"code")]
[Description(@"Displays markdown content with custom Krypton rendering or HTML preview with CSS theming.")]
public class KryptonMarkdownPreview : VisualPanel
{
    #region Type Definitions
    private enum MarkdownElementType
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

    private class MarkdownElement
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

    private class MarkdownParser
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
    #endregion

    #region Instance Fields
    private string _markdownText = string.Empty;
    private PreviewMode _previewMode = PreviewMode.Custom;
    private KryptonPanel _customPreviewPanel;
    private KryptonWebBrowser _htmlPreviewBrowser;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonMarkdownPreview class.
    /// </summary>
    public KryptonMarkdownPreview()
    {
        // Create custom preview panel
        _customPreviewPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Create HTML preview browser
        _htmlPreviewBrowser = new KryptonWebBrowser
        {
            Dock = DockStyle.Fill
        };

        // Add both, but only show one
        Controls.Add(_customPreviewPanel);
        Controls.Add(_htmlPreviewBrowser);
        
        _htmlPreviewBrowser.Visible = false;
        
        // Set default size
        Size = new Size(300, 200);
        
        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the markdown text to preview.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue("")]
    [Description(@"The markdown text content to preview.")]
    [Localizable(true)]
    public string MarkdownText
    {
        get => _markdownText;
        set
        {
            if (_markdownText != value)
            {
                _markdownText = value ?? string.Empty;
                UpdatePreview();
            }
        }
    }

    /// <summary>
    /// Gets and sets the preview mode.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(PreviewMode.Custom)]
    [Description(@"Determines whether to use custom Krypton rendering or HTML preview.")]
    public PreviewMode PreviewMode
    {
        get => _previewMode;
        set
        {
            if (_previewMode != value)
            {
                _previewMode = value;
                UpdatePreviewMode();
            }
        }
    }

    /// <summary>
    /// Refreshes the preview display.
    /// </summary>
    public void RefreshPreview()
    {
        UpdatePreview();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _customPreviewPanel?.Dispose();
            _htmlPreviewBrowser?.Dispose();
        }
        base.Dispose(disposing);
    }
    #endregion

    #region Private
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_previewMode == PreviewMode.Html)
        {
            UpdatePreview();
        }
    }

    private void UpdatePreviewMode()
    {
        _customPreviewPanel.Visible = _previewMode == PreviewMode.Custom;
        _htmlPreviewBrowser.Visible = _previewMode == PreviewMode.Html;
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (_previewMode == PreviewMode.Custom)
        {
            RenderCustomPreview(_markdownText);
        }
        else
        {
            RenderHtmlPreview(_markdownText);
        }
    }

    private void RenderCustomPreview(string markdown)
    {
        _customPreviewPanel.Controls.Clear();
        
        var parser = new MarkdownParser();
        var elements = parser.Parse(markdown);
        
        var yPos = 10;
        foreach (var element in elements)
        {
            var control = CreateKryptonControl(element);
            if (control != null)
            {
                control.Location = new Point(10, yPos);
                control.Width = _customPreviewPanel.Width - 20;
                control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                _customPreviewPanel.Controls.Add(control);
                yPos += control.Height + 5;
            }
        }
    }

    private string ColorToHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
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

    private string ProcessInlineHtml(string text)
    {
        text = Regex.Replace(text, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
        text = Regex.Replace(text, @"\*(.*?)\*", "<em>$1</em>");
        text = Regex.Replace(text, @"~~(.*?)~~", "<del>$1</del>");
        text = Regex.Replace(text, @"`(.*?)`", "<code>$1</code>");
        text = Regex.Replace(text, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");
        return text;
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

    private void RenderHtmlPreview(string markdown)
    {
        var html = ConvertMarkdownToHtml(markdown);
        var css = GenerateKryptonCss();
        var fullHtml = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
{css}
    </style>
</head>
<body>
{html}
</body>
</html>";

        _htmlPreviewBrowser.DocumentText = fullHtml;
    }

    private Control? CreateKryptonControl(MarkdownElement element)
    {
        switch (element.Type)
        {
            case MarkdownElementType.Heading1:
            {
                var label = new KryptonLabel
                {
                    Text = element.Text,
                    LabelStyle = LabelStyle.TitlePanel,
                    AutoSize = false,
                    Height = 40
                };
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
                return label;
            }
            case MarkdownElementType.Heading2:
            {
                var label = new KryptonLabel
                {
                    Text = element.Text,
                    LabelStyle = LabelStyle.TitlePanel,
                    AutoSize = false,
                    Height = 35
                };
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
                return label;
            }
            case MarkdownElementType.Heading3:
            {
                var label = new KryptonLabel
                {
                    Text = element.Text,
                    LabelStyle = LabelStyle.TitlePanel,
                    AutoSize = false,
                    Height = 30
                };
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                return label;
            }
            case MarkdownElementType.Paragraph:
                return new KryptonLabel
                {
                    Text = ProcessInlineFormatting(element.Text),
                    LabelStyle = LabelStyle.NormalControl,
                    AutoSize = true,
                    MaximumSize = new Size(_customPreviewPanel.Width - 20, 0)
                };
            case MarkdownElementType.CodeBlock:
            {
                var panel = new KryptonPanel
                {
                    PanelBackStyle = PaletteBackStyle.PanelAlternate,
                    Padding = new Padding(10),
                    Height = 100
                };
                panel.Controls.Add(new KryptonLabel
                {
                    Text = element.Text,
                    Font = new Font("Consolas", 9F),
                    Dock = DockStyle.Fill,
                    LabelStyle = LabelStyle.NormalControl
                });
                return panel;
            }
            case MarkdownElementType.Blockquote:
            {
                var panel = new KryptonPanel
                {
                    PanelBackStyle = PaletteBackStyle.PanelAlternate,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10, 5, 10, 5),
                    Height = 50
                };
                var label = new KryptonLabel
                {
                    Text = element.Text,
                    Dock = DockStyle.Fill,
                    LabelStyle = LabelStyle.NormalControl
                };
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                panel.Controls.Add(label);
                return panel;
            }
            case MarkdownElementType.UnorderedList:
                return CreateListControl(element, false);
            case MarkdownElementType.OrderedList:
                return CreateListControl(element, true);
            case MarkdownElementType.HorizontalRule:
                return new KryptonBorderEdge
                {
                    BorderStyle = PaletteBorderStyle.HeaderPrimary,
                    Height = 2,
                    Dock = DockStyle.Top,
                    Width = _customPreviewPanel.Width - 20
                };
            default:
                return null;
        }
    }

    private Control CreateListControl(MarkdownElement element, bool ordered)
    {
        var panel = new KryptonPanel
        {
            PanelBackStyle = PaletteBackStyle.PanelClient,
            AutoSize = true,
            Padding = new Padding(20, 5, 5, 5)
        };

        var items = element.Items ?? new List<string>();
        for (int i = 0; i < items.Count; i++)
        {
            var prefix = ordered ? $"{i + 1}. " : "• ";
            var label = new KryptonLabel
            {
                Text = prefix + items[i],
                Location = new Point(0, i * 25),
                AutoSize = true,
                LabelStyle = LabelStyle.NormalControl
            };
            panel.Controls.Add(label);
        }

        panel.Height = items.Count * 25 + 10;
        return panel;
    }

    private string ProcessInlineFormatting(string text)
    {
        // Simple inline formatting processing
        text = Regex.Replace(text, @"\*\*(.*?)\*\*", "$1"); // Bold
        text = Regex.Replace(text, @"\*(.*?)\*", "$1"); // Italic
        text = Regex.Replace(text, @"~~(.*?)~~", "$1"); // Strikethrough
        text = Regex.Replace(text, @"\[(.*?)\]\(.*?\)", "$1"); // Links
        return text;
    }
    #endregion
}

/// <summary>
/// Specifies the preview mode for markdown rendering.
/// </summary>
public enum PreviewMode
{
    /// <summary>
    /// Use custom Krypton controls for rendering.
    /// </summary>
    Custom,
    
    /// <summary>
    /// Use HTML preview with CSS theming matching Krypton palettes.
    /// </summary>
    Html
}

