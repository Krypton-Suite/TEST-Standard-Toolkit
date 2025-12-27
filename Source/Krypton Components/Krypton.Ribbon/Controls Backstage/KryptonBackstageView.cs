#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provides an Office 2010-style backstage navigation surface.
/// </summary>
[ToolboxItem(true)]
[DefaultEvent(nameof(SelectedPageChanged))]
[DefaultProperty(nameof(Pages))]
[Designer(typeof(KryptonBackstageViewDesigner))]
[DesignerCategory(@"code")]
[Description(@"Office 2010-style Backstage view surface for use with KryptonRibbon File tab.")]
public class KryptonBackstageView : KryptonPanel
{
    #region Instance Fields
    private readonly KryptonBackstagePageCollection _pages;
    private KryptonBackstagePage? _selectedPage;

    private readonly KryptonPanel _navigationPanel;
    private readonly BackstageNavigationList _navigationList;
    private readonly KryptonPanel _pageContainer;

    private int _navigationWidth;
    private bool _suspendSync;
    private readonly BackstageColors _colors;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the selected page changes.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when the selected page changes.")]
    public event EventHandler? SelectedPageChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBackstageView"/> class.
    /// </summary>
    public KryptonBackstageView()
    {
        _navigationWidth = 200;

        _pages = new KryptonBackstagePageCollection();
        _pages.Inserted += OnPagesInserted;
        _pages.Removed += OnPagesRemoved;
        _pages.Cleared += OnPagesCleared;

        // Initialize colors object
        _colors = new BackstageColors(OnColorsNeedPaint);

        // Left navigation area - defaults to PanelAlternate style
        _navigationPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = _navigationWidth,
            PanelBackStyle = PaletteBackStyle.PanelAlternate
        };

        _navigationList = new BackstageNavigationList(this)
        {
            Dock = DockStyle.Fill
        };
        _navigationList.SelectedIndexChanged += OnNavigationSelectedIndexChanged;

        _navigationPanel.Controls.Add(_navigationList);

        // Page container area - defaults to PanelClient style
        _pageContainer = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        Controls.Add(_pageContainer);
        Controls.Add(_navigationPanel);

        // Hook into palette changes to update colors
        PaletteChanged += OnPaletteChanged;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the backstage pages collection.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Collection of pages hosted by the backstage view.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(BackstagePageCollectionEditor), typeof(UITypeEditor))]
    public KryptonBackstagePageCollection Pages => _pages;

    /// <summary>
    /// Gets and sets the selected page.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Selected backstage page.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonBackstagePage? SelectedPage
    {
        get => _selectedPage;
        set
        {
            if (!ReferenceEquals(_selectedPage, value))
            {
                SelectPage(value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the width of the left navigation panel.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Width of the left navigation panel.")]
    [DefaultValue(200)]
    public int NavigationWidth
    {
        get => _navigationWidth;
        set
        {
            if (_navigationWidth != value)
            {
                _navigationWidth = value;
                _navigationPanel.Width = value;
                PerformLayout();
            }
        }
    }

    /// <summary>
    /// Gets access to the internal page container (used by the designer).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonPanel PageContainer => _pageContainer;

    /// <summary>
    /// Gets access to the backstage colors.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Groups backstage view color properties.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BackstageColors Colors => _colors;

    private bool ShouldSerializeColors() => !_colors.IsDefault;
    #endregion

    #region Implementation
    private void SelectPage(KryptonBackstagePage? page)
    {
        _suspendSync = true;
        try
        {
            if (_selectedPage != null)
            {
                _selectedPage.Visible = false;
            }

            _selectedPage = page;

            if (_selectedPage != null)
            {
                _selectedPage.Dock = DockStyle.Fill;
                _selectedPage.Visible = true;
                _selectedPage.BringToFront();

                if (!ReferenceEquals(_navigationList.SelectedItem, _selectedPage))
                {
                    _navigationList.SelectedItem = _selectedPage;
                }
            }
            else
            {
                _navigationList.ClearSelected();
            }
        }
        finally
        {
            _suspendSync = false;
        }

        SelectedPageChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnNavigationSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suspendSync)
        {
            return;
        }

        var page = _navigationList.SelectedItem as KryptonBackstagePage;
        if (!ReferenceEquals(_selectedPage, page))
        {
            SelectPage(page);
        }
    }

    private void OnPagesInserted(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e)
    {
        KryptonBackstagePage? page = e.Item;
        if (page == null)
        {
            return;
        }

        // Ensure page is hosted inside our internal page container
        if (page.Parent != _pageContainer)
        {
            _pageContainer.Controls.Add(page);
        }

        page.Dock = DockStyle.Fill;
        page.Visible = false;
        page.NavigationPropertyChanged += OnPageNavigationPropertyChanged;

        RebuildNavigationList();

        // Auto-select first page
        if (_selectedPage == null)
        {
            SelectPage(page);
        }
    }

    private void OnPagesRemoved(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e)
    {
        KryptonBackstagePage? page = e.Item;
        if (page == null)
        {
            return;
        }

        page.NavigationPropertyChanged -= OnPageNavigationPropertyChanged;

        if (page.Parent == _pageContainer)
        {
            _pageContainer.Controls.Remove(page);
        }

        RebuildNavigationList();

        // If we removed the selected page, select another
        if (ReferenceEquals(_selectedPage, page))
        {
            SelectPage(_pages.Count > 0 ? _pages[0] : null);
        }
    }

    private void OnPagesCleared(object? sender, EventArgs e)
    {
        foreach (KryptonBackstagePage page in _pages.ToArray())
        {
            page.NavigationPropertyChanged -= OnPageNavigationPropertyChanged;
        }

        _pageContainer.Controls.Clear();
        _navigationList.Items.Clear();
        SelectPage(null);
    }

    private void OnPageNavigationPropertyChanged(object? sender, EventArgs e) => RebuildNavigationList();

    private void UpdateNavigationColors()
    {
        if (_colors.NavigationBackgroundColor.HasValue)
        {
            _navigationPanel.StateCommon.Back.Color1 = _colors.NavigationBackgroundColor.Value;
            _navigationPanel.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
        }
        else
        {
            // Reset to use PanelAlternate palette
            _navigationPanel.StateCommon.Back.ColorStyle = PaletteColorStyle.Inherit;
            _navigationPanel.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        }
        _navigationPanel.Invalidate();
    }

    private void UpdateContentColors()
    {
        if (_colors.ContentBackgroundColor.HasValue)
        {
            _pageContainer.StateCommon.Back.Color1 = _colors.ContentBackgroundColor.Value;
            _pageContainer.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
        }
        else
        {
            // Reset to use PanelClient palette
            _pageContainer.StateCommon.Back.ColorStyle = PaletteColorStyle.Inherit;
            _pageContainer.PanelBackStyle = PaletteBackStyle.PanelClient;
        }
        _pageContainer.Invalidate();
    }

    private void OnColorsNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateNavigationColors();
        UpdateContentColors();
        _navigationList.Invalidate();
        PerformNeedPaint(e.NeedLayout);
    }

    internal Color GetNavigationBackgroundColor()
    {
        if (_colors.NavigationBackgroundColor.HasValue)
        {
            return _colors.NavigationBackgroundColor.Value;
        }

        // Theme-aware defaults
        var palette = GetPalette();
        if (IsOffice2013Theme(palette))
        {
            // Office 2013: Dark blue navigation panel
            return Color.FromArgb(31, 78, 121);
        }

        // Office 2010 and others: Use PanelAlternate palette color
        return palette?.GetBackColor1(PaletteBackStyle.PanelAlternate, PaletteState.Normal) ?? Color.FromArgb(240, 240, 240);
    }

    internal Color GetContentBackgroundColor()
    {
        if (_colors.ContentBackgroundColor.HasValue)
        {
            return _colors.ContentBackgroundColor.Value;
        }

        // All themes: Use PanelClient palette color
        var palette = GetPalette();
        return palette?.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal) ?? Color.White;
    }

    internal Color GetSelectedItemHighlightColor()
    {
        if (_colors.SelectedItemHighlightColor.HasValue)
        {
            return _colors.SelectedItemHighlightColor.Value;
        }

        // Theme-aware defaults
        var palette = GetPalette();
        if (IsOffice2013Theme(palette))
        {
            // Office 2013: Lighter blue highlight for selected items
            return Color.FromArgb(68, 114, 196);
        }

        // Office 2010: Orange highlight
        return Color.FromArgb(242, 155, 57);
    }

    private static bool IsOffice2013Theme(PaletteBase? palette)
    {
        if (palette == null)
        {
            return false;
        }

        // Check if the palette is an Office 2013 palette
        var paletteType = palette.GetType();
        return paletteType.Name.Contains("Office2013", StringComparison.OrdinalIgnoreCase) ||
               paletteType.Namespace?.Contains("Office 2013", StringComparison.OrdinalIgnoreCase) == true;
    }

    private PaletteBase? GetPalette()
    {
        // Use our own resolved palette (inherits from VisualPanel)
        return GetResolvedPalette();
    }

    private void RebuildNavigationList()
    {
        if (_suspendSync)
        {
            return;
        }

        _suspendSync = true;
        try
        {
            _navigationList.BeginUpdate();
            _navigationList.Items.Clear();

            foreach (KryptonBackstagePage page in _pages)
            {
                if (page.VisibleInNavigation)
                {
                    _navigationList.Items.Add(page);
                }
            }

            if (_selectedPage != null && _selectedPage.VisibleInNavigation)
            {
                _navigationList.SelectedItem = _selectedPage;
            }
        }
        finally
        {
            _navigationList.EndUpdate();
            _suspendSync = false;
        }
    }

    private void OnPaletteChanged(object? sender, EventArgs e)
    {
        UpdateNavigationColors();
        UpdateContentColors();
        _navigationList.Invalidate();
    }
    #endregion
}


