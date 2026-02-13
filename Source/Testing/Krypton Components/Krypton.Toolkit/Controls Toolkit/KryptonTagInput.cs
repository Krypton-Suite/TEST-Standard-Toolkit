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

namespace Krypton.Toolkit;

/// <summary>
/// Represents a single tag in a KryptonTagInput control.
/// </summary>
public class TagItem
{
    /// <summary>
    /// Initializes a new instance of the TagItem class.
    /// </summary>
    /// <param name="text">The text of the tag.</param>
    public TagItem(string text)
    {
        Text = text ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the text of the tag.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets user-defined data associated with the tag.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// Returns a string representation of the tag.
    /// </summary>
    public override string ToString() => Text;
}

/// <summary>
/// Collection of tags for KryptonTagInput control.
/// </summary>
public class TagCollection : Collection<TagItem>
{
    private readonly KryptonTagInput _owner;

    /// <summary>
    /// Initializes a new instance of the TagCollection class.
    /// </summary>
    /// <param name="owner">The owner control.</param>
    internal TagCollection(KryptonTagInput owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Adds a tag with the specified text.
    /// </summary>
    /// <param name="text">The text of the tag to add.</param>
    public void Add(string text)
    {
        Add(new TagItem(text));
    }

    /// <summary>
    /// Removes all tags with the specified text.
    /// </summary>
    /// <param name="text">The text of the tags to remove.</param>
    public void Remove(string text)
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            if (this[i].Text == text)
            {
                RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Clears all tags from the collection.
    /// </summary>
    protected override void ClearItems()
    {
        base.ClearItems();
        _owner?.OnTagsChanged();
    }

    /// <summary>
    /// Inserts a tag at the specified index.
    /// </summary>
    protected override void InsertItem(int index, TagItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        // Don't allow duplicate tags
        if (this.Any(t => t.Text.Equals(item.Text, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        base.InsertItem(index, item);
        _owner?.OnTagsChanged();
    }

    /// <summary>
    /// Removes a tag at the specified index.
    /// </summary>
    protected override void RemoveItem(int index)
    {
        base.RemoveItem(index);
        _owner?.OnTagsChanged();
    }

    /// <summary>
    /// Sets a tag at the specified index.
    /// </summary>
    protected override void SetItem(int index, TagItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        base.SetItem(index, item);
        _owner?.OnTagsChanged();
    }
}

/// <summary>
/// Provides a control for entering and managing tags (chips/tokens).
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTextBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
[DefaultEvent(nameof(TagAdded))]
[DefaultProperty(nameof(Tags))]
[DesignerCategory(@"code")]
[Description(@"Provides a control for entering and managing tags (chips/tokens).")]
public class KryptonTagInput : UserControl
{
    #region Instance Fields
    private KryptonTextBox? _textBox;
    private readonly TagCollection _tags;
    private readonly List<Rectangle> _tagRectangles;
    private int _hoveredTagIndex;
    private int _hoveredRemoveIndex;
    private const int TagPadding = 4;
    private const int TagSpacing = 4;
    private const int RemoveButtonSize = 16;
    private const int TagHeight = 24;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a tag is added.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a tag is added.")]
    public event EventHandler<TagEventArgs>? TagAdded;

    /// <summary>
    /// Occurs when a tag is removed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a tag is removed.")]
    public event EventHandler<TagEventArgs>? TagRemoved;

    /// <summary>
    /// Raises the TagAdded event.
    /// </summary>
    protected virtual void OnTagAdded(TagEventArgs e) => TagAdded?.Invoke(this, e);

    /// <summary>
    /// Raises the TagRemoved event.
    /// </summary>
    protected virtual void OnTagRemoved(TagEventArgs e) => TagRemoved?.Invoke(this, e);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTagInput class.
    /// </summary>
    public KryptonTagInput()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor, true);

        _tags = new TagCollection(this);
        _tagRectangles = new List<Rectangle>();
        _hoveredTagIndex = -1;
        _hoveredRemoveIndex = -1;

        // Create the internal text box
        _textBox = new KryptonTextBox
        {
            Dock = DockStyle.None
        };

        _textBox.TextChanged += OnTextBoxTextChanged;
        _textBox.KeyDown += OnTextBoxKeyDown;
        _textBox.KeyPress += OnTextBoxKeyPress;
        _textBox.GotFocus += OnTextBoxGotFocus;
        _textBox.LostFocus += OnTextBoxLostFocus;

        Controls.Add(_textBox);

        // Set default size
        Size = new Size(200, TagHeight + 8);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_textBox != null)
            {
                _textBox.TextChanged -= OnTextBoxTextChanged;
                _textBox.KeyDown -= OnTextBoxKeyDown;
                _textBox.KeyPress -= OnTextBoxKeyPress;
                _textBox.GotFocus -= OnTextBoxGotFocus;
                _textBox.LostFocus -= OnTextBoxLostFocus;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the collection of tags.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The collection of tags.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TagCollection Tags => _tags;

    /// <summary>
    /// Gets or sets the placeholder text displayed when the text box is empty.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The placeholder text displayed when the text box is empty.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string PlaceholderText
    {
        get => _textBox?.CueHint.CueHintText ?? string.Empty;
        set
        {
            if (_textBox != null)
            {
                _textBox.CueHint.CueHintText = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _textBox?.PaletteMode ?? PaletteMode.Global;
        set
        {
            if (_textBox != null)
            {
                _textBox.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _textBox?.LocalCustomPalette;
        set
        {
            if (_textBox != null)
            {
                _textBox.LocalCustomPalette = value as KryptonCustomPaletteBase;
            }
        }
    }

    /// <summary>
    /// Gets access to the underlying KryptonTextBox control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTextBox? TextBox => _textBox;

    /// <summary>
    /// Clears all tags.
    /// </summary>
    public void ClearTags()
    {
        _tags.Clear();
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_textBox == null)
        {
            return;
        }

        // Calculate tag positions
        CalculateTagPositions();

        // Draw tags
        for (int i = 0; i < _tags.Count && i < _tagRectangles.Count; i++)
        {
            DrawTag(e.Graphics, i, _tagRectangles[i]);
        }
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        int oldHoveredTag = _hoveredTagIndex;
        int oldHoveredRemove = _hoveredRemoveIndex;

        _hoveredTagIndex = -1;
        _hoveredRemoveIndex = -1;

        // Check if mouse is over a tag or remove button
        for (int i = 0; i < _tagRectangles.Count; i++)
        {
            Rectangle tagRect = _tagRectangles[i];
            Rectangle removeRect = GetRemoveButtonRectangle(tagRect);

            if (tagRect.Contains(e.Location))
            {
                _hoveredTagIndex = i;
            }

            if (removeRect.Contains(e.Location))
            {
                _hoveredRemoveIndex = i;
                break;
            }
        }

        if (_hoveredTagIndex != oldHoveredTag || _hoveredRemoveIndex != oldHoveredRemove)
        {
            Invalidate();
            Cursor = _hoveredRemoveIndex >= 0 ? Cursors.Hand : Cursors.Default;
        }
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_hoveredTagIndex >= 0 || _hoveredRemoveIndex >= 0)
        {
            _hoveredTagIndex = -1;
            _hoveredRemoveIndex = -1;
            Invalidate();
            Cursor = Cursors.Default;
        }
    }

    /// <summary>
    /// Raises the MouseClick event.
    /// </summary>
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (e.Button == MouseButtons.Left)
        {
            // Check if clicked on remove button
            for (int i = 0; i < _tagRectangles.Count; i++)
            {
                Rectangle removeRect = GetRemoveButtonRectangle(_tagRectangles[i]);
                if (removeRect.Contains(e.Location))
                {
                    RemoveTag(i);
                    return;
                }
            }

            // If clicked on tag area but not on remove button, focus textbox
            if (_hoveredTagIndex >= 0)
            {
                _textBox?.Focus();
            }
        }
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        UpdateTextBoxPosition();
    }

    /// <summary>
    /// Raises the FontChanged event.
    /// </summary>
    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        if (_textBox != null)
        {
            _textBox.Font = Font;
        }
        Invalidate();
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        if (_textBox != null)
        {
            _textBox.Enabled = Enabled;
        }
        Invalidate();
    }
    #endregion

    #region Implementation
    private void OnTextBoxTextChanged(object? sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    private void OnTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back && _textBox != null)
        {
            // If textbox is empty and there are tags, remove the last tag
            if (string.IsNullOrEmpty(_textBox.Text) && _tags.Count > 0)
            {
                RemoveTag(_tags.Count - 1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        OnKeyDown(e);
    }

    private void OnTextBoxKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
        {
            // Add tag from textbox text
            if (_textBox != null && !string.IsNullOrWhiteSpace(_textBox.Text))
            {
                AddTag(_textBox.Text.Trim());
                _textBox.Text = string.Empty;
            }

            e.Handled = true;
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            // Clear textbox
            if (_textBox != null)
            {
                _textBox.Text = string.Empty;
            }

            e.Handled = true;
        }

        if (!e.Handled)
        {
            OnKeyPress(e);
        }
    }

    private void OnTextBoxGotFocus(object? sender, EventArgs e)
    {
        OnGotFocus(e);
    }

    private void OnTextBoxLostFocus(object? sender, EventArgs e)
    {
        // Add tag from textbox text if not empty
        if (_textBox != null && !string.IsNullOrWhiteSpace(_textBox.Text))
        {
            AddTag(_textBox.Text.Trim());
            _textBox.Text = string.Empty;
        }

        OnLostFocus(e);
    }

    private void AddTag(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        // Check for duplicates
        if (_tags.Any(t => t.Text.Equals(text, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        var tag = new TagItem(text);
        _tags.Add(tag);
        OnTagAdded(new TagEventArgs(tag));
    }

    private void RemoveTag(int index)
    {
        if (index >= 0 && index < _tags.Count)
        {
            var tag = _tags[index];
            _tags.RemoveAt(index);
            OnTagRemoved(new TagEventArgs(tag));
            _textBox?.Focus();
        }
    }

    internal void OnTagsChanged()
    {
        UpdateTextBoxPosition();
        Invalidate();
    }

    private void CalculateTagPositions()
    {
        _tagRectangles.Clear();

        if (_textBox == null)
        {
            return;
        }

        int x = Padding.Left;
        int y = Padding.Top + (Height - TagHeight - Padding.Top - Padding.Bottom) / 2;

        using (Graphics g = CreateGraphics())
        {
            for (int i = 0; i < _tags.Count; i++)
            {
                string tagText = _tags[i].Text;
                SizeF textSize = g.MeasureString(tagText, Font);
                int tagWidth = (int)textSize.Width + TagPadding * 2 + RemoveButtonSize + TagPadding;

                // Check if tag fits on current line
                if (x + tagWidth > ClientSize.Width - Padding.Right)
                {
                    // Move to next line (if multiline support is added)
                    x = Padding.Left;
                    y += TagHeight + TagSpacing;
                }

                _tagRectangles.Add(new Rectangle(x, y, tagWidth, TagHeight));
                x += tagWidth + TagSpacing;
            }
        }
    }

    private void UpdateTextBoxPosition()
    {
        if (_textBox == null)
        {
            return;
        }

        CalculateTagPositions();

        int textBoxX = Padding.Left;
        int textBoxY = Padding.Top + (Height - _textBox.Height - Padding.Top - Padding.Bottom) / 2;
        int textBoxWidth = ClientSize.Width - Padding.Left - Padding.Right;
        int textBoxHeight = _textBox.Height;

        // If there are tags, position textbox after the last tag
        if (_tagRectangles.Count > 0)
        {
            Rectangle lastTag = _tagRectangles[_tagRectangles.Count - 1];
            textBoxX = lastTag.Right + TagSpacing;

            // Adjust width to fit remaining space
            textBoxWidth = ClientSize.Width - textBoxX - Padding.Right;
            if (textBoxWidth < 50)
            {
                // Not enough space, move to new line
                textBoxX = Padding.Left;
                textBoxY = lastTag.Bottom + TagSpacing;
                textBoxWidth = ClientSize.Width - Padding.Left - Padding.Right;
            }
        }

        _textBox.Location = new Point(textBoxX, textBoxY);
        _textBox.Width = Math.Max(50, textBoxWidth);
    }

    private void DrawTag(Graphics g, int index, Rectangle rect)
    {
        if (index < 0 || index >= _tags.Count)
        {
            return;
        }

        TagItem tag = _tags[index];
        bool isHovered = _hoveredTagIndex == index;
        bool isRemoveHovered = _hoveredRemoveIndex == index;

        // Get colors from palette if available
        Color backColor = isHovered ? SystemColors.ControlDark : SystemColors.Control;
        Color textColor = ForeColor;
        Color borderColor = SystemColors.ControlDark;

        if (_textBox != null)
        {
            var palette = _textBox.GetResolvedPalette();
            if (palette != null)
            {
                var state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
                backColor = palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, state);
                textColor = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, state);
                borderColor = palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, state);
            }
        }

        // Draw tag background
        using (var brush = new SolidBrush(backColor))
        {
            g.FillRectangle(brush, rect);
        }

        // Draw tag border
        using (var pen = new Pen(borderColor))
        {
            g.DrawRectangle(pen, rect);
        }

        // Draw tag text
        Rectangle textRect = new Rectangle(rect.X + TagPadding, rect.Y, rect.Width - TagPadding * 2 - RemoveButtonSize, rect.Height);
        TextRenderer.DrawText(g, tag.Text, Font, textRect, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix);

        // Draw remove button
        Rectangle removeRect = GetRemoveButtonRectangle(rect);
        Color removeColor = isRemoveHovered ? Color.Red : textColor;
        using (var pen = new Pen(removeColor, 2))
        {
            int centerX = removeRect.X + removeRect.Width / 2;
            int centerY = removeRect.Y + removeRect.Height / 2;
            int size = 8;
            g.DrawLine(pen, centerX - size / 2, centerY - size / 2, centerX + size / 2, centerY + size / 2);
            g.DrawLine(pen, centerX - size / 2, centerY + size / 2, centerX + size / 2, centerY - size / 2);
        }
    }

    private Rectangle GetRemoveButtonRectangle(Rectangle tagRect)
    {
        return new Rectangle(
            tagRect.Right - RemoveButtonSize - TagPadding,
            tagRect.Y + (tagRect.Height - RemoveButtonSize) / 2,
            RemoveButtonSize,
            RemoveButtonSize);
    }
    #endregion
}

/// <summary>
/// Provides data for tag events.
/// </summary>
public class TagEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the TagEventArgs class.
    /// </summary>
    /// <param name="tag">The tag associated with the event.</param>
    public TagEventArgs(TagItem tag)
    {
        Tag = tag;
    }

    /// <summary>
    /// Gets the tag associated with the event.
    /// </summary>
    public TagItem Tag { get; }
}

