#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonRatingValues
{
    #region Instance Fields

    private Color _emptyStarColor;
    private Color _filledStarColor;
    private int _maximumRating;
    private int _minimumRating;
    private int _starSpacing;

    #endregion

    #region Identity

    public KryptonRatingValues()
    {
        _minimumRating = 0;
        _maximumRating = 5;
        _starSpacing = 4;
        _filledStarColor = Color.Gold;
        _emptyStarColor = Color.LightGray;
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the maximum rating value.</summary>
    [DefaultValue(5)]
    [Description("The maximum rating value.")]
    public int MaximumRating { get => _maximumRating; set => _maximumRating = value; }

    /// <summary>Gets or sets the minimum rating value.</summary>
    [DefaultValue(0)]
    [Description("The minimum rating value.")]
    public int MinimumRating { get => _minimumRating; set => _minimumRating = value; }

    /// <summary>Gets or sets the spacing between stars.</summary>
    [DefaultValue(4)]
    [Description("The spacing between stars.")]
    public int StarSpacing { get => _starSpacing; set => _starSpacing = value; }

    /// <summary>Gets or sets the color of the filled star.</summary>
    [DefaultValue(typeof(Color), "Gold")]
    [Description("The color of the filled star.")]
    public Color FilledStarColor { get => _filledStarColor; set => _filledStarColor = value; }

    /// <summary>Gets or sets the color of the empty star.</summary>
    [DefaultValue(typeof(Color), "LightGray")]
    [Description("The color of the empty star.")]
    public Color EmptyStarColor { get => _emptyStarColor; set => _emptyStarColor = value; }

    #endregion
}