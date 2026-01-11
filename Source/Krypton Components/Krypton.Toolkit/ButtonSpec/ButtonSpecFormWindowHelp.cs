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
/// Implementation for the fixed help button for krypton form.
/// </summary>
public class ButtonSpecFormWindowHelp : ButtonSpecFormFixed
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecFormWindowHelp class.
    /// </summary>
    /// <param name="form">Reference to owning krypton form instance.</param>
    public ButtonSpecFormWindowHelp(KryptonForm form)
        : base(form, PaletteButtonSpecStyle.FormHelp)
    {
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette) =>
        // Help button only shows when HelpButton is true and both MinimizeBox and MaximizeBox are false
        KryptonForm.HelpButton && !KryptonForm.MaximizeBox && !KryptonForm.MinimizeBox;

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) =>
        // Help button is enabled when visible
        GetVisible(palette) ? ButtonEnabled.True : ButtonEnabled.False;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) =>
        // Help button is never shown as checked
        ButtonCheckState.NotCheckButton;

    #endregion    

    #region Protected Overrides
    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Only if associated view is enabled do we perform an action
        if (GetViewEnabled())
        {
            // If we do not provide an inert form
            if (!KryptonForm.InertForm)
            {
                // Only if the mouse is still within the button bounds do we perform action
                var mea = (MouseEventArgs)e;
                if (GetView().ClientRectangle.Contains(mea.Location))
                {
                    // Get the mouse position in screen coordinates
                    Point screenPos = Control.MousePosition;
                    Point clientPos = KryptonForm.PointToClient(screenPos);

                    // Find the control at the mouse position (or use the form itself)
                    Control? targetControl = KryptonForm.GetChildAtPoint(clientPos, 
                        GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled) ?? KryptonForm;

                    // Ensure the control has a handle before using it
                    if (!targetControl.IsHandleCreated && targetControl != KryptonForm)
                    {
                        targetControl = KryptonForm;
                    }

                    // Create HELPINFO structure
                    var helpInfo = new PI.HELPINFO
                    {
                        cbSize = Marshal.SizeOf(typeof(PI.HELPINFO)),
                        iContextType = 1, // HELPINFO_WINDOW
                        iCtrlId = 0,
                        hItemHandle = targetControl.Handle,
                        dwContextId = IntPtr.Zero,
                        MousePos = new PI.WINDOWLOCATION { X = screenPos.X, Y = screenPos.Y }
                    };

                    // Allocate memory for HELPINFO structure
                    IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PI.HELPINFO)));
                    try
                    {
                        // Copy structure to unmanaged memory
                        Marshal.StructureToPtr(helpInfo, lParam, false);

                        // Send WM_HELP message to the form - this will be handled by KryptonForm.WndProc
                        const int WM_HELP = 0x0053;
                        PI.SendMessage(KryptonForm.Handle, WM_HELP, IntPtr.Zero, lParam);
                    }
                    finally
                    {
                        // Free the allocated memory
                        Marshal.FreeHGlobal(lParam);
                    }

                    // Let base class fire any other attached events
                    base.OnClick(e);
                }
            }
        }
    }
    #endregion
}
