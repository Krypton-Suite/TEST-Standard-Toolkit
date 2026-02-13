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
/// Represents a single button in the thumbnail toolbar.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarThumbnailButton
{
    #region Instance Fields
    private uint _id;
    private int _imageIndex;
    private string _tooltip;
    private ThumbnailButtonFlags _flags;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarThumbnailButton class.
    /// </summary>
    public TaskbarThumbnailButton()
    {
        Reset();
    }
    #endregion

    #region Id
    /// <summary>
    /// Gets and sets the unique identifier for the button.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Unique identifier for the button. This ID is sent in the click event when the button is clicked.")]
    [DefaultValue(0U)]
    public uint Id
    {
        get => _id;
        set => _id = value;
    }

    private bool ShouldSerializeId() => Id != 0;

    /// <summary>
    /// Resets the Id property to its default value.
    /// </summary>
    public void ResetId() => Id = 0;
    #endregion

    #region ImageIndex
    /// <summary>
    /// Gets and sets the index of the image in the ImageList to use for this button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Index of the image in the ImageList to use for this button.")]
    [DefaultValue(-1)]
    public int ImageIndex
    {
        get => _imageIndex;
        set => _imageIndex = value;
    }

    private bool ShouldSerializeImageIndex() => ImageIndex != -1;

    /// <summary>
    /// Resets the ImageIndex property to its default value.
    /// </summary>
    public void ResetImageIndex() => ImageIndex = -1;
    #endregion

    #region Tooltip
    /// <summary>
    /// Gets and sets the tooltip text displayed when hovering over the button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Tooltip text displayed when hovering over the button.")]
    [Localizable(true)]
    [DefaultValue("")]
    public string Tooltip
    {
        get => _tooltip;
        set => _tooltip = value ?? string.Empty;
    }

    private bool ShouldSerializeTooltip() => Tooltip != string.Empty;

    /// <summary>
    /// Resets the Tooltip property to its default value.
    /// </summary>
    public void ResetTooltip() => Tooltip = string.Empty;
    #endregion

    #region Flags
    /// <summary>
    /// Gets and sets the flags controlling button behavior and state.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Flags controlling button behavior and state.")]
    [DefaultValue(ThumbnailButtonFlags.Enabled)]
    public ThumbnailButtonFlags Flags
    {
        get => _flags;
        set => _flags = value;
    }

    private bool ShouldSerializeFlags() => Flags != ThumbnailButtonFlags.Enabled;

    /// <summary>
    /// Resets the Flags property to its default value.
    /// </summary>
    public void ResetFlags() => Flags = ThumbnailButtonFlags.Enabled;
    #endregion

    #region Implementation
    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetId();
        ResetImageIndex();
        ResetTooltip();
        ResetFlags();
    }

    /// <summary>
    /// Copies properties from another button instance.
    /// </summary>
    /// <param name="source">Source instance to copy from.</param>
    public void CopyFrom(TaskbarThumbnailButton source)
    {
        Id = source.Id;
        ImageIndex = source.ImageIndex;
        Tooltip = source.Tooltip;
        Flags = source.Flags;
    }
    #endregion
}
