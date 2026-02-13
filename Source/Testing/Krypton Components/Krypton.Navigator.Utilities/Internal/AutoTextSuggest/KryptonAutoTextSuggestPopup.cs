#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace Krypton.Navigator.Utilities.Internal;

/// <summary>
/// Popup control for displaying text suggestions.
/// </summary>
internal class KryptonAutoTextSuggestPopup : VisualPopup
{
    private static class Win32
    {
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;
        public const int WM_DEADCHAR = 0x0103;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;
        public const int WM_SYSDEADCHAR = 0x0107;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }

    #region Instance Fields
    private readonly KryptonListBox _listBox;
    private readonly KryptonAutoTextSuggestProvider _provider;
    #endregion

    #region Identity
    public KryptonAutoTextSuggestPopup(KryptonAutoTextSuggestProvider provider, IRenderer? renderer)
        : base(new ViewManager(), renderer, true)
    {
        _provider = provider;
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        _listBox.DoubleClick += ListBox_DoubleClick;

        var layoutFill = new ViewLayoutFill(_listBox);
        var layoutDocker = new ViewLayoutDocker
        {
            { layoutFill, ViewDockStyle.Fill }
        };

        ViewManager.Control = this;
        ViewManager.AlignControl = this;
        ViewManager.Root = layoutDocker;

        Controls.Add(_listBox);
        Size = new Size(200, 150);
    }
    #endregion

    #region Public
    public KryptonListBox ListBox => _listBox;

    public void UpdateSuggestions(List<KryptonAutoTextSuggestItem> suggestions)
    {
        _listBox.Items.Clear();
        foreach (var item in suggestions)
        {
            _listBox.Items.Add(item);
        }

        if (_listBox.Items.Count > 0)
        {
            _listBox.SelectedIndex = 0;
        }

        AdjustSize();
    }

    public void SelectNext()
    {
        if (_listBox.Items.Count == 0)
            return;

        int nextIndex = _listBox.SelectedIndex + 1;
        if (nextIndex >= _listBox.Items.Count)
            nextIndex = 0;

        _listBox.SelectedIndex = nextIndex;
        EnsureVisible();
    }

    public void SelectPrevious()
    {
        if (_listBox.Items.Count == 0)
            return;

        int prevIndex = _listBox.SelectedIndex - 1;
        if (prevIndex < 0)
            prevIndex = _listBox.Items.Count - 1;

        _listBox.SelectedIndex = prevIndex;
        EnsureVisible();
    }

    public KryptonAutoTextSuggestItem? GetSelectedItem()
    {
        if (_listBox.SelectedIndex >= 0 && _listBox.SelectedIndex < _listBox.Items.Count)
        {
            return _listBox.Items[_listBox.SelectedIndex] as KryptonAutoTextSuggestItem;
        }
        return null;
    }

    public void ShowPopup(Control parentControl)
    {
        if (parentControl == null || !parentControl.IsHandleCreated)
            return;

        Point location = parentControl.PointToScreen(new Point(0, parentControl.Height));
        Rectangle screenRect = new Rectangle(location, Size);

        Screen screen = Screen.FromControl(parentControl);
        if (screenRect.Right > screen.WorkingArea.Right)
            screenRect.X = screen.WorkingArea.Right - screenRect.Width;

        if (screenRect.Bottom > screen.WorkingArea.Bottom)
            screenRect.Y = parentControl.PointToScreen(Point.Empty).Y - screenRect.Height;

        if (screenRect.X < screen.WorkingArea.Left)
            screenRect.X = screen.WorkingArea.Left;

        if (screenRect.Y < screen.WorkingArea.Top)
            screenRect.Y = screen.WorkingArea.Top;

        Show(screenRect);
    }

    public new void Close()
    {
        if (IsHandleCreated && !IsDisposed)
        {
            VisualPopupManager.Singleton.EndPopupTracking(this);
        }
    }
    #endregion

    #region Protected Override
    protected override void WndProc(ref Message m)
    {
        Control? attachedControl = _provider.AttachedControl;
        if (attachedControl != null && attachedControl.IsHandleCreated && attachedControl.Focused)
        {
            if (m.Msg == Win32.WM_KEYDOWN ||
                m.Msg == Win32.WM_KEYUP ||
                m.Msg == Win32.WM_CHAR ||
                m.Msg == Win32.WM_SYSKEYDOWN ||
                m.Msg == Win32.WM_SYSKEYUP ||
                m.Msg == Win32.WM_SYSCHAR ||
                m.Msg == Win32.WM_DEADCHAR ||
                m.Msg == Win32.WM_SYSDEADCHAR)
            {
                Win32.SendMessage(attachedControl.Handle, m.Msg, m.WParam, m.LParam);
                return;
            }
        }

        base.WndProc(ref m);
    }
    #endregion

    #region Implementation
    private void AdjustSize()
    {
        int itemCount = _listBox.Items.Count;
        int maxVisibleItems = _provider.MaxVisibleItems;

        int itemHeight = itemCount > 0
            ? _listBox.GetItemHeight(0)
            : Font.Height + 4;

        int height = Math.Min(itemCount * itemHeight + 4, maxVisibleItems * itemHeight + 4);
        height = Math.Max(height, itemHeight + 4);

        Size = new Size(_provider.PopupWidth, height);
    }

    private void EnsureVisible()
    {
        if (_listBox.SelectedIndex >= 0)
        {
            _listBox.TopIndex = Math.Max(0, _listBox.SelectedIndex - _provider.MaxVisibleItems + 1);
        }
    }

    private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
    }

    private void ListBox_DoubleClick(object? sender, EventArgs e)
    {
        var item = GetSelectedItem();
        if (item != null)
        {
            _provider.ApplySuggestion(item);
        }
    }
    #endregion
}
