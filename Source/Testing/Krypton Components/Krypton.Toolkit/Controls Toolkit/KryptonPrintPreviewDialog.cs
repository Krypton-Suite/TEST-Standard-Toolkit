#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a dialog box form that contains a <see cref="PrintPreviewControl"/> for printing from a Windows Forms application.
/// </summary>
[DefaultProperty(nameof(Document))]
[ToolboxBitmap(typeof(PrintPreviewDialog), "ToolboxBitmaps.KryptonPrintDialog.png")]
[Description("Displays a Kryptonised version of the standard PrintPreview dialog window.")]
[DesignerCategory(@"code")]
public class KryptonPrintPreviewDialog : Component, IDisposable
{
    #region Instance Fields

    private VisualPrintPreviewForm? _previewForm;
    private PrintDocument? _document;
    private bool _useAntiAlias = true;
    private Icon? _icon;
    private string _text = @"Print Preview";
    private bool _disposed;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref='KryptonPrintPreviewDialog'/> class.
    /// </summary>
    public KryptonPrintPreviewDialog()
    {
    }

    /// <summary>
    /// Disposes of the resources (other than memory) used by the <see cref='KryptonPrintPreviewDialog'/>.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _previewForm?.Dispose();
            _disposed = true;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the <see cref='PrintDocument'/> to preview.
    /// </summary>
    [DefaultValue(null)]
    [Description("The PrintDocument to preview.")]
    [Category(@"Behavior")]
    public PrintDocument? Document
    {
        get => _document;
        set
        {
            _document = value;
            if (_previewForm != null)
            {
                _previewForm.Document = value;
            }
        }
    }

    /// <summary>
    /// Gets the <see cref='KryptonPrintPreviewControl'/> contained in this form.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPrintPreviewControl? PrintPreviewControl => _previewForm?.PrintPreviewControl;

    /// <summary>
    /// Gets the underlying <see cref='PrintPreviewControl'/> for compatibility.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PrintPreviewControl? PrintPreviewControlBase => _previewForm?.PrintPreviewControlBase;

    /// <summary>
    /// Gets or sets a value indicating whether printing uses anti-aliasing.
    /// </summary>
    [DefaultValue(true)]
    [Description("Indicates whether printing uses anti-aliasing.")]
    [Category(@"Behavior")]
    public bool UseAntiAlias
    {
        get => _useAntiAlias;
        set
        {
            _useAntiAlias = value;
            if (_previewForm != null)
            {
                _previewForm.UseAntiAlias = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the icon for the form.
    /// </summary>
    [DefaultValue(null)]
    [Description("The icon for the form.")]
    [Category(@"Appearance")]
    public Icon? Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            if (_previewForm != null)
            {
                _previewForm.Icon = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [DefaultValue(@"Print Preview")]
    [Description("The text associated with this control.")]
    [Category(@"Appearance")]
    [Localizable(true)]
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            if (_previewForm != null)
            {
                _previewForm.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the form's window state.
    /// </summary>
    [DefaultValue(FormWindowState.Normal)]
    [Description("The form's window state.")]
    [Category(@"Window Style")]
    public FormWindowState WindowState { get; set; } = FormWindowState.Normal;

    #endregion

    #region Public Methods

    /// <summary>
    /// Runs a print preview dialog box.
    /// </summary>
    /// <returns>One of the <see cref="DialogResult"/> values.</returns>
    public DialogResult ShowDialog()
    {
        return ShowDialog(null);
    }

    /// <summary>
    /// Runs a print preview dialog box with the specified owner.
    /// </summary>
    /// <param name="owner">Any object that implements <see cref="IWin32Window"/> that represents the top-level window that will own the modal dialog box.</param>
    /// <returns>One of the <see cref="DialogResult"/> values.</returns>
    public DialogResult ShowDialog(IWin32Window? owner)
    {
        if (_document == null)
        {
            throw new ArgumentNullException(nameof(Document), @"Document must be set before showing the dialog.");
        }

        _previewForm?.Dispose();
        _previewForm = new VisualPrintPreviewForm
        {
            Document = _document,
            UseAntiAlias = _useAntiAlias,
            Icon = _icon,
            Text = _text,
            WindowState = WindowState
        };

        return _previewForm.ShowDialog(owner);
    }

    #endregion

}
