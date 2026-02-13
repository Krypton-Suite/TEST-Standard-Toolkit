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
/// Storage for taskbar thumbnail button value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarThumbnailButtonValues : Storage
{
    #region Instance Fields
    private ImageList? _imageList;
    private readonly TaskbarThumbnailButtonCollection _buttons;
    internal event Action? OnTaskbarThumbnailButtonsChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarThumbnailButtonValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TaskbarThumbnailButtonValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Initialize buttons collection
        _buttons = new TaskbarThumbnailButtonCollection();
        _buttons.CollectionChanged += OnButtonsCollectionChanged;

        Reset();
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (ImageList == null) && (Buttons.Count == 0);
    #endregion

    #region ImageList
    /// <summary>
    /// Gets and sets the ImageList containing button icons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The ImageList containing button icons. Must be set before adding buttons.")]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _imageList;

        set
        {
            if (_imageList != value)
            {
                _imageList = value;
                PerformNeedPaint(true);
                OnTaskbarThumbnailButtonsChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeImageList() => ImageList != null;

    /// <summary>
    /// Resets the ImageList property to its default value.
    /// </summary>
    public void ResetImageList() => ImageList = null;
    #endregion

    #region Buttons
    /// <summary>
    /// Gets access to the collection of thumbnail buttons.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Collection of thumbnail buttons. Maximum 7 buttons (Windows limitation).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TaskbarThumbnailButtonCollection Buttons => _buttons;
    #endregion

    #region Implementation
    /// <summary>
    /// Handles changes to the buttons collection.
    /// </summary>
    private void OnButtonsCollectionChanged()
    {
        PerformNeedPaint(true);
        OnTaskbarThumbnailButtonsChanged?.Invoke();
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        ResetImageList();
        Buttons.Clear();
    }

    /// <summary>
    /// Copies values from another TaskbarThumbnailButtonValues instance.
    /// </summary>
    /// <param name="source">Source instance to copy from.</param>
    public void CopyFrom(TaskbarThumbnailButtonValues source)
    {
        ImageList = source.ImageList;
        Buttons.Clear();
        foreach (var button in source.Buttons)
        {
            var newButton = new TaskbarThumbnailButton();
            newButton.CopyFrom(button);
            Buttons.Add(newButton);
        }
    }
    #endregion
}
