#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internal form class for the print preview dialog.
/// </summary>
internal partial class VisualPrintPreviewForm : KryptonForm
{
    #region Instance Fields

    private PrintDocument? _document;
    private bool _useAntiAlias = true;

    #endregion

    #region Identity

    public VisualPrintPreviewForm()
    {
        InitializeComponent();
        SetupToolbar();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _previewControl?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the PrintDocument to preview.
    /// </summary>
    public PrintDocument? Document
    {
        get => _document;
        set
        {
            _document = value;
            _previewControl.Document = value;
            UpdatePageInfo();
        }
    }

    /// <summary>
    /// Gets the PrintPreviewControl contained in this form.
    /// </summary>
    public PrintPreviewControl PrintPreviewControl => _previewControl;

    /// <summary>
    /// Gets or sets whether to use anti-aliasing.
    /// </summary>
    public bool UseAntiAlias
    {
        get => _useAntiAlias;
        set
        {
            _useAntiAlias = value;
            _previewControl.UseAntiAlias = value;
        }
    }

    #endregion

    #region Private Methods

    private void SetupToolbar()
    {
        // Ensure proper spacing and layout
        _toolbarPanel.Padding = new Padding(4);
        
        // Initialize zoom scrollbar with current zoom value
        UpdateZoomScrollBar();
    }

    private void UpdateZoomScrollBar()
    {
        // Map zoom (double, e.g., 0.25-5.0) to scrollbar value (int, e.g., 25-500)
        int scrollValue = (int)(_previewControl.Zoom * 100);
        scrollValue = Math.Max(_zoomScrollBar.Minimum, Math.Min(_zoomScrollBar.Maximum, scrollValue));
        _zoomScrollBar.Value = scrollValue;
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        if (_document != null)
        {
            using var printDialog = new KryptonPrintDialog
            {
                Document = _document
            };
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                _document.Print();
            }
        }
    }

    private void BtnZoomIn_Click(object? sender, EventArgs e)
    {
        _previewControl.Zoom += 0.25;
        UpdateZoomScrollBar();
    }

    private void BtnZoomOut_Click(object? sender, EventArgs e)
    {
        if (_previewControl.Zoom > 0.25)
        {
            _previewControl.Zoom -= 0.25;
            UpdateZoomScrollBar();
        }
    }

    private void ZoomScrollBar_Scroll(object? sender, ScrollEventArgs e)
    {
        // Map scrollbar value (int, e.g., 25-500) to zoom (double, e.g., 0.25-5.0)
        double zoom = e.NewValue / 100.0;
        _previewControl.Zoom = zoom;
    }

    private void BtnOnePage_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 1;
    }

    private void BtnTwoPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 2;
    }

    private void BtnThreePages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 3;
    }

    private void BtnFourPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 2;
        _previewControl.Columns = 2;
    }

    private void BtnSixPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 2;
        _previewControl.Columns = 3;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void UpdatePageInfo()
    {
        if (_document != null && _previewControl.Document != null)
        {
            // Note: PrintPreviewControl doesn't expose page count directly
            // This is a simplified implementation
            _lblPageInfo.Text = @"Preview";
        }
    }

    #endregion
}
