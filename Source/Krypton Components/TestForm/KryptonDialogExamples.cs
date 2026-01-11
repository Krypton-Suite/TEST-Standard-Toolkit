#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonDialogExamples: KryptonForm
{
    public KryptonDialogExamples()
    {
        InitializeComponent();
    }

    private void kbtnColorDialog_Click(object sender, EventArgs e)
    {
        var kcd = new KryptonColorDialog();

        kcd.ShowDialog();
    }

    private void kbtnFontDialog_Click(object sender, EventArgs e)
    {
        var kfd = new KryptonFontDialog();

        kfd.ShowDialog();
    }

    private void kbtnPrintDialog_Click(object sender, EventArgs e)
    {
        var kpd = new KryptonPrintDialog();

        kpd.ShowDialog();
    }

    private void kbtnPrintPreviewDialog_Click(object sender, EventArgs e)
    {
        // Create a simple PrintDocument for testing
        var printDoc = new PrintDocument();
        printDoc.PrintPage += (s, args) =>
        {
            var font = new Font("Arial", 12);
            args.Graphics!.DrawString("This is a test print preview.", font, Brushes.Black, 100, 100);
        };

        var kppd = new KryptonPrintPreviewDialog
        {
            Document = printDoc,
            Text = "Print Preview Test"
        };

        kppd.ShowDialog();
    }
}