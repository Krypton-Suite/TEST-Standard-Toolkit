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
/// Storage for taskbar-related values (overlay icon, progress, and jump list).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarValues : Storage
{
    #region Instance Fields
    private readonly TaskbarOverlayIconValues _overlayIcon;
    private readonly TaskbarProgressValues _progress;
    private readonly JumpListValues _jumpList;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TaskbarValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Initialize nested expandable objects
        _overlayIcon = new TaskbarOverlayIconValues(needPaint);
        _progress = new TaskbarProgressValues(needPaint);
        _jumpList = new JumpListValues(needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => OverlayIcon.IsDefault && Progress.IsDefault && JumpList.IsDefault;
    #endregion

    #region OverlayIcon
    /// <summary>
    /// Gets access to the taskbar overlay icon values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Taskbar overlay icon to display on the taskbar button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TaskbarOverlayIconValues OverlayIcon => _overlayIcon;

    private bool ShouldSerializeOverlayIcon() => !OverlayIcon.IsDefault;

    /// <summary>
    /// Resets the OverlayIcon property to its default value.
    /// </summary>
    public void ResetOverlayIcon() => OverlayIcon.Reset();
    #endregion

    #region Progress
    /// <summary>
    /// Gets access to the taskbar progress values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Taskbar progress indicator to display on the taskbar button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TaskbarProgressValues Progress => _progress;

    private bool ShouldSerializeProgress() => !Progress.IsDefault;

    /// <summary>
    /// Resets the Progress property to its default value.
    /// </summary>
    public void ResetProgress() => Progress.Reset();
    #endregion

    #region JumpList
    /// <summary>
    /// Gets access to the jump list values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Jump list configuration for the taskbar button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public JumpListValues JumpList => _jumpList;

    private bool ShouldSerializeJumpList() => !JumpList.IsDefault;

    /// <summary>
    /// Resets the JumpList property to its default value.
    /// </summary>
    public void ResetJumpList() => JumpList.Reset();
    #endregion

    #region Implementation
    /// <summary>
    /// Resets all taskbar values to their defaults.
    /// </summary>
    public void Reset()
    {
        OverlayIcon.Reset();
        Progress.Reset();
        JumpList.Reset();
    }

    /// <summary>
    /// Copies values from another TaskbarValues instance.
    /// </summary>
    /// <param name="source">Source instance to copy from.</param>
    public void CopyFrom(TaskbarValues source)
    {
        OverlayIcon.CopyFrom(source.OverlayIcon);
        Progress.CopyFrom(source.Progress);
        JumpList.CopyFrom(source.JumpList);
    }
    #endregion
}
