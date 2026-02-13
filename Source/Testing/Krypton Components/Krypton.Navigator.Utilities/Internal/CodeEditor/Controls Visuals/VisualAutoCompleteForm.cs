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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace Krypton.Navigator.Utilities.Internal;

/// <summary>
/// Auto-complete popup form.
/// </summary>
internal class VisualAutoCompleteForm : Form
{
    private readonly KryptonListBox _listBox;
    private readonly KryptonCodeEditor _editor;
    private List<string> _items;
    private string _filter = "";

    public VisualAutoCompleteForm(KryptonCodeEditor editor)
    {
        _editor = editor;
        _items = new List<string>();

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        TopMost = true;

        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        _listBox.SelectedIndexChanged += (s, e) => { };

        _listBox.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsertSelectedItem();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                _currentWordPrefix = "";
                Hide();
                _editor.RichTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                Hide();
                _editor.RichTextBox.Focus();
                e.Handled = true;
            }
        };

        Controls.Add(_listBox);
    }

    public void SetItems(List<string> items)
    {
        _items = items;
    }

    public void FilterItems(string filter)
    {
        _filter = filter;
        if (_items == null)
        {
            _listBox.Items.Clear();
            return;
        }

        _listBox.BeginUpdate();
        SuspendLayout();
        try
        {
            _listBox.Items.Clear();

            var filtered = string.IsNullOrEmpty(filter)
                ? _items
                : _items.Where(i => i.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var item in filtered.Take(20))
            {
                _listBox.Items.Add(item);
            }

            if (_listBox.Items.Count > 0)
            {
                _listBox.SelectedIndex = 0;
            }
        }
        finally
        {
            ResumeLayout();
            _listBox.EndUpdate();
        }

        if (_listBox.Items.Count > 0)
        {
            var itemHeight = _listBox.GetItemHeight(0);
            Height = Math.Min(itemHeight * Math.Min(_listBox.Items.Count, 10) + 4, 300);

            using (var g = _listBox.CreateGraphics())
            {
                var maxWidth = 0;
                foreach (var item in _listBox.Items)
                {
                    var itemText = item?.ToString() ?? "";
                    var textSize = TextRenderer.MeasureText(g, itemText, _listBox.Font);
                    maxWidth = Math.Max(maxWidth, textSize.Width);
                }
                Width = Math.Max(200, Math.Min(maxWidth + 30, 400));
            }
        }
    }

    private string _currentWordPrefix = "";

    public void SetCurrentWordPrefix(string prefix)
    {
        _currentWordPrefix = prefix;
    }

    public void InsertSelectedItem()
    {
        if (_listBox.SelectedIndex >= 0)
        {
            var selected = _listBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selected))
            {
                var rtb = _editor.RichTextBox;
                var startPos = rtb.SelectionStart - _currentWordPrefix.Length;
                rtb.Select(startPos, _currentWordPrefix.Length);
                rtb.SelectedText = selected;
                rtb.SelectionStart = startPos + selected.Length;
                Hide();
            }
        }
    }

    public int ItemCount => _listBox.Items.Count;

    public void UpdateFilter(char keyChar)
    {
        if (keyChar == '.')
        {
            _currentWordPrefix = "";
            FilterItems("");
            UpdatePosition();
            if (_listBox.Items.Count > 0 && !Visible)
            {
                Show();
            }
        }
        else if (char.IsLetterOrDigit(keyChar) || keyChar == '_')
        {
            _currentWordPrefix += keyChar;
            FilterItems(_currentWordPrefix);

            if (_listBox.Items.Count > 0)
            {
                UpdatePosition();
                if (!Visible)
                {
                    Show();
                }
            }
            else
            {
                Hide();
            }
        }
        else if (keyChar == '\b' && _currentWordPrefix.Length > 0)
        {
            _currentWordPrefix = _currentWordPrefix.Substring(0, _currentWordPrefix.Length - 1);
            FilterItems(_currentWordPrefix);

            if (_listBox.Items.Count > 0)
            {
                UpdatePosition();
                if (!Visible)
                {
                    Show();
                }
            }
            else
            {
                Hide();
            }
        }
        else if (keyChar == '(')
        {
            Hide();
        }
    }

    private void UpdatePosition()
    {
        var pos = _editor.RichTextBox.GetPositionFromCharIndex(_editor.RichTextBox.SelectionStart);
        var screenPos = _editor.PointToScreen(pos);
        Location = new Point(screenPos.X, screenPos.Y + _editor.RichTextBox.Font.Height);
    }

    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x00000008;
            cp.ExStyle |= unchecked((int)0x08000000);
            return cp;
        }
    }
}
