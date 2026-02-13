#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Floating window form for hosting a KryptonRadialMenu control.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class VisualRadialMenuFloatingForm : KryptonForm
{
    #region Instance Fields
    private KryptonRadialMenu? _radialMenu;
    private readonly KryptonPanel? _contentPanel;
    private readonly Form? _originalParent;
    private readonly Control? _originalContainer;
    private readonly Point _originalLocation;
    private readonly DockStyle _originalDock;
    private readonly AnchorStyles _originalAnchor;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the window is being closed and the menu should be returned to its original location.
    /// </summary>
    public event EventHandler? WindowReturning;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualRadialMenuFloatingForm class.
    /// </summary>
    /// <param name="radialMenu">The radial menu to host in this floating window.</param>
    /// <param name="owner">Owner form for this floating window.</param>
    public VisualRadialMenuFloatingForm(KryptonRadialMenu radialMenu, Form? owner = null)
    {
        _radialMenu = radialMenu ?? throw new ArgumentNullException(nameof(radialMenu));
        
        // Store original parent information
        _originalParent = radialMenu.FindForm();
        _originalContainer = radialMenu.Parent;
        _originalLocation = radialMenu.Location;
        _originalDock = radialMenu.Dock;
        _originalAnchor = radialMenu.Anchor;

        // Set form properties
        Owner = owner ?? _originalParent;
        Text = string.Empty;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        TopMost = false;
        MinimizeBox = false;
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.None;

        // Check if owner is an MDI container
        if (owner is { IsMdiContainer: true })
        {
            // Set as MDI child
            MdiParent = owner;
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }

        // Make form invisible using transparency key
        // Set background to transparency key color so form itself is invisible
        BackColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        TransparencyKey = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        
        // Remove border drawing
        StateCommon!.Border.DrawBorders = PaletteDrawBorders.None;
        StateCommon.Border.Width = 0;

        // Create content panel - transparent to let radial menu show through
        _contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0)
        };
        
        // Set panel to transparent (use transparency key color)
        _contentPanel.BackColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        _contentPanel.StateCommon.Color1 = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        _contentPanel.StateCommon.ColorStyle = PaletteColorStyle.Solid;

        // Move the radial menu to this window
        CommonHelper.RemoveControlFromParent(radialMenu);
        _contentPanel.Controls.Add(radialMenu);
        radialMenu.Dock = DockStyle.Fill;
        radialMenu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

        Controls.Add(_contentPanel);

        // Set initial size based on menu size
        Size = new Size(radialMenu.Width + 10, radialMenu.Height + 30);

        // Position window near the original menu location
        if (_originalParent != null)
        {
            Point screenLocation = _originalParent.PointToScreen(_originalLocation);
            Location = screenLocation;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the radial menu hosted in this window.
    /// </summary>
    public KryptonRadialMenu? RadialMenu => _radialMenu;

    /// <summary>
    /// Returns the radial menu to its original location.
    /// </summary>
    public void ReturnToOriginalLocation()
    {
        if (_radialMenu == null || _originalContainer == null)
        {
            return;
        }

        OnWindowReturning(EventArgs.Empty);

        // Remove from floating window
        _contentPanel?.Controls.Remove(_radialMenu);

        // Restore original properties
        _radialMenu.Dock = _originalDock;
        _radialMenu.Anchor = _originalAnchor;
        _radialMenu.Location = _originalLocation;

        // Return to original container
        CommonHelper.AddControlToParent(_originalContainer, _radialMenu);

        _radialMenu = null;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the FormClosing event.
    /// </summary>
    /// <param name="e">A FormClosingEventArgs that contains the event data.</param>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            // Return menu to original location instead of closing
            e.Cancel = true;
            ReturnToOriginalLocation();
            Hide();
        }

        base.OnFormClosing(e);
    }

    /// <summary>
    /// Raises the WindowReturning event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnWindowReturning(EventArgs e) => WindowReturning?.Invoke(this, e);
    #endregion
}

