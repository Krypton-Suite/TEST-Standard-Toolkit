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
/// Provide a PDF viewer control with Krypton styling applied using native Windows PDF rendering.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(WebBrowser), "ToolboxBitmaps.WebBrowser.bmp")]
[Designer(typeof(KryptonPdfViewerDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to view PDF documents with Krypton theming support.")]
public class KryptonPdfViewer : WebBrowser
{
    #region Instance Fields
    private PaletteBase? _palette;
    private PaletteMode _paletteMode = PaletteMode.Global;
    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
    private KryptonContextMenu? _kryptonContextMenu;
    private IRenderer _renderer;
    private string? _pdfFilePath;
    #endregion Instance Fields

    #region Events
    /// <summary>
    /// Occurs when a PDF document is loaded.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a PDF document is loaded.")]
    public event EventHandler? PdfLoaded;

    /// <summary>
    /// Occurs when a PDF document fails to load.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a PDF document fails to load.")]
    public event EventHandler<PdfLoadErrorEventArgs>? PdfLoadError;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPdfViewer class.
    /// </summary>
    public KryptonPdfViewer()
    {
        // We use double buffering to reduce drawing flicker
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        // We need to repaint entire control whenever resized
        SetStyle(ControlStyles.ResizeRedraw, true);

        // Yes, we want to be drawn double buffered by default
        DoubleBuffered = true;

        // Disable script errors
        ScriptErrorsSuppressed = true;

        // Set initial palette
        _palette = KryptonManager.CurrentGlobalPalette;
        _renderer = _palette.GetRenderer();

        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(_palette);

        // Create the palette storage
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Hook into document completed event
        DocumentCompleted += OnDocumentCompleted;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            DocumentCompleted -= OnDocumentCompleted;

            // Unhook from palette events
            if (_palette != null)
            {
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
            }

            // Clean up palette objects
            _stateNormal = null;
            _stateDisabled = null;
            _stateCommon = null;
            _redirector = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the path to the PDF file to display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The path to the PDF file to display.")]
    [DefaultValue(null)]
    [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public string? PdfFilePath
    {
        get => _pdfFilePath;
        set
        {
            if (_pdfFilePath != value)
            {
                _pdfFilePath = value;
                if (!string.IsNullOrEmpty(value))
                {
                    LoadPdf(value);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateRedirector();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                    _palette = value;
                }

                UpdateRedirector();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets access to the common PDF viewer appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common PDF viewer appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon?.Back ?? throw new ObjectDisposedException(nameof(KryptonPdfViewer));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled PDF viewer appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled PDF viewer appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled?.Back ?? throw new ObjectDisposedException(nameof(KryptonPdfViewer));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal PDF viewer appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal PDF viewer appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal?.Back ?? throw new ObjectDisposedException(nameof(KryptonPdfViewer));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.Back.IsDefault;
    #endregion

    #region Public Methods
    /// <summary>
    /// Loads a PDF file from the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the PDF file.</param>
    public void LoadPdf(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            OnPdfLoadError(new PdfLoadErrorEventArgs($"PDF file not found: {filePath}"));
            return;
        }

        try
        {
            _pdfFilePath = filePath;
            // Convert file path to file:// URL format
            var uri = new Uri(filePath);
            Navigate(uri.AbsoluteUri);
        }
        catch (Exception ex)
        {
            OnPdfLoadError(new PdfLoadErrorEventArgs($"Error loading PDF: {ex.Message}", ex));
        }
    }

    /// <summary>
    /// Loads a PDF from a byte array.
    /// </summary>
    /// <param name="pdfData">The PDF file data as a byte array.</param>
    public void LoadPdf(byte[] pdfData)
    {
        if (pdfData == null || pdfData.Length == 0)
        {
            throw new ArgumentException("PDF data cannot be null or empty.", nameof(pdfData));
        }

        try
        {
            // Create a temporary file to load the PDF
            var tempFile = Path.Combine(Path.GetTempPath(), $"KryptonPdfViewer_{Guid.NewGuid()}.pdf");
            File.WriteAllBytes(tempFile, pdfData);
            LoadPdf(tempFile);
        }
        catch (Exception ex)
        {
            OnPdfLoadError(new PdfLoadErrorEventArgs($"Error loading PDF from byte array: {ex.Message}", ex));
        }
    }

    /// <summary>
    /// Clears the currently displayed PDF.
    /// </summary>
    public void ClearPdf()
    {
        _pdfFilePath = null;
        Navigate("about:blank");
    }
    #endregion

    #region MenuStrip Overrides

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
    /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
    [Category(@"Behavior")]
    [Description(@"Consider using KryptonContextMenu within the behaviors section.\nThe Winforms shortcut menu to show when the user right-clicks the PDF.\nNote: The ContextMenu will be rendered.")]
    [DefaultValue(null)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip;

        set
        {
            // Unhook from any current menu strip
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
            }

            // Let parent handle actual storage
            base.ContextMenuStrip = value;

            // Hook into the strip being shown (so we can set the correct renderer)
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening += OnContextMenuStripOpening;
            }
        }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut menu to show when the user right-clicks the PDF.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;

        set
        {
            if (_kryptonContextMenu != value)
            {
                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                }

                _kryptonContextMenu = value;

                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                }
            }
        }
    }

    private void OnContextMenuStripOpening(object? sender, CancelEventArgs e)
    {
        // Get the actual strip instance
        ContextMenuStrip? cms = base.ContextMenuStrip;

        // Make sure it has the correct renderer
        cms!.Renderer = CreateToolStripRenderer();
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        if ((m.Msg == PI.WM_.CONTEXTMENU)  // For some reason this is not being fired here, therefore the Menu KeyDown is also being lost.
             || (m.Msg == PI.WM_.PARENTNOTIFY && PI.LOWORD(m.WParam) == PI.WM_.RBUTTONDOWN)    // Hack to intercept the mouse button due to the above
           )
        {
            // Only interested in overriding the behavior when we have a krypton context menu...
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                if (((int)(long)m.LParam) == -1)
                {
                    mousePt = new Point(Width / 2, Height / 2);
                }
                else
                {
                    if (m.Msg == PI.WM_.CONTEXTMENU)
                    {
                        mousePt = PointToClient(mousePt);
                    }

                    // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                    // of the showing context menu just like it happens for a ContextMenuStrip.
                    mousePt.X -= 1;
                    mousePt.Y -= 1;
                }

                // If the mouse position is within our client area
                if (ClientRectangle.Contains(mousePt))
                {
                    // Show the context menu
                    KryptonContextMenu.Show(this, PointToScreen(mousePt));

                    // We eat the message!
                    return;
                }
            }
        }

        base.WndProc(ref m);
    }

    private void OnKryptonContextMenuDisposed(object? sender, EventArgs e) =>
        // When the current krypton context menu is disposed, we should remove
        // it to prevent it being used again, as that would just throw an exception
        // because it has been disposed.
        KryptonContextMenu = null;

    #endregion MenuStrip Overrides

    #region Palette Controls

    private void UpdateRedirector()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_redirector != null)
        {
            _redirector.Target = currentPalette;
        }
        else
        {
            _redirector = new PaletteRedirect(currentPalette);
            if (_stateCommon != null)
            {
                _stateCommon.SetRedirector(_redirector);
            }
        }

        // Get the renderer associated with the palette
        _renderer = currentPalette.GetRenderer();

        // Hook to palette events
        if (currentPalette != null)
        {
            currentPalette.BasePaletteChanged -= OnBaseChanged;
            currentPalette.BaseRendererChanged -= OnBaseChanged;
            currentPalette.BasePaletteChanged += OnBaseChanged;
            currentPalette.BaseRendererChanged += OnBaseChanged;
        }
    }

    /// <summary>Called when there is a change in base renderer or base palette.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void OnBaseChanged(object? sender, EventArgs e)
    {
        // Change in base renderer or base palette require we fetch the latest renderer
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        _renderer = currentPalette.GetRenderer();
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        Invalidate();
    }

    /// <summary>
    /// Occurs when the global palette has been changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnGlobalPaletteChanged(object sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            // Update self with the new global palette
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
        }
    }

    /// <summary>
    /// Create a tool strip renderer appropriate for the current renderer/palette pair.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer? CreateToolStripRenderer()
    {
        var palette = GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette;
        return _renderer?.RenderToolStrip(palette);
    }

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase GetResolvedPalette() => _palette;

    #endregion Palette Controls

    #region Implementation
    private void OnDocumentCompleted(object? sender, WebBrowserDocumentCompletedEventArgs e)
    {
        // Check if the document loaded successfully
        if (e.Url != null && !string.IsNullOrEmpty(_pdfFilePath))
        {
            OnPdfLoaded(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Raises the PdfLoaded event.
    /// </summary>
    protected virtual void OnPdfLoaded(EventArgs e) => PdfLoaded?.Invoke(this, e);

    /// <summary>
    /// Raises the PdfLoadError event.
    /// </summary>
    protected virtual void OnPdfLoadError(PdfLoadErrorEventArgs e) => PdfLoadError?.Invoke(this, e);
    #endregion
}

/// <summary>
/// Provides event data for PDF load error events.
/// </summary>
public class PdfLoadErrorEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the PdfLoadErrorEventArgs class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public PdfLoadErrorEventArgs(string message)
        : this(message, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the PdfLoadErrorEventArgs class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exception">The exception that caused the error.</param>
    public PdfLoadErrorEventArgs(string message, Exception? exception)
    {
        Message = message;
        Exception = exception;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the exception that caused the error, if any.
    /// </summary>
    public Exception? Exception { get; }
}

