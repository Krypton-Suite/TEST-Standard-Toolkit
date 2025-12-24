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
    private readonly KryptonListBox _navigationList;
    private readonly KryptonPanel _pageContainer;

    private int _navigationWidth;
    private bool _suspendSync;
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

        // Left navigation area
        _navigationPanel = new KryptonPanel
        {
            Dock = DockStyle.Left,
            Width = _navigationWidth,
            PanelBackStyle = PaletteBackStyle.ControlRibbonAppMenu
        };

        _navigationList = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _navigationList.SelectedIndexChanged += OnNavigationSelectedIndexChanged;
        _navigationList.DisplayMember = nameof(Text);

        _navigationPanel.Controls.Add(_navigationList);

        // Page container area
        _pageContainer = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.ControlRibbonAppMenu
        };

        Controls.Add(_pageContainer);
        Controls.Add(_navigationPanel);
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
    #endregion
}


