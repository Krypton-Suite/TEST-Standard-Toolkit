#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RibbonTest : KryptonForm
{
    public RibbonTest()
    {
        InitializeComponent();
        CreateBackstageDemo();
    }

    private void CreateBackstageDemo()
    {
        // Demo: designer-friendly backstage view with pages
        var backstage = new KryptonBackstageView
        {
            Dock = DockStyle.Fill
        };

        var page1 = new KryptonBackstagePage { Text = @"Info" };
        var page2 = new KryptonBackstagePage { Text = @"Actions" };

        var info = new KryptonLabel
        {
            Text = @"Backstage demo page (Info). Add pages via designer on KryptonBackstageView.Pages.",
            Dock = DockStyle.Top
        };
        page1.Controls.Add(info);

        var closeButton = new KryptonButton
        {
            Text = @"Close Backstage",
            Dock = DockStyle.Top
        };
        closeButton.Click += (_, _) => kryptonRibbon.CloseBackstageView();
        page2.Controls.Add(closeButton);

        backstage.Pages.Add(page1);
        backstage.Pages.Add(page2);

        kryptonRibbon.RibbonFileAppTab.UseBackstageView = true;
        kryptonRibbon.RibbonFileAppTab.BackstageView = backstage;
    }

    private void krgbtnTest1715_Click(object sender, EventArgs e)
    {
        kryptonRibbon.SelectedTab!.ContextName = @"Testing";
    }
}