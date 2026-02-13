namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for badge border value information.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BadgeBorderValues : Storage
    {
        #region Static Fields
        private static readonly Color _defaultBorderColor = Color.Empty;
        private const int DEFAULT_BORDER_SIZE = 0;
        private const BadgeBevelType DEFAULT_BORDER_BEVEL = BadgeBevelType.None;
        #endregion

        #region Instance Fields
        private readonly NeedPaintHandler? _needPaint;
        private Color _borderColor;
        private int _borderSize;
        private BadgeBevelType _borderBevel;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the BadgeBorderValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public BadgeBorderValues(NeedPaintHandler? needPaint)
        {
            _needPaint = needPaint;
            _borderColor = _defaultBorderColor;
            _borderSize = DEFAULT_BORDER_SIZE;
            _borderBevel = DEFAULT_BORDER_BEVEL;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsDefault => (BorderColor == _defaultBorderColor) &&
                                          (BorderSize == DEFAULT_BORDER_SIZE) &&
                                          (BorderBevel == DEFAULT_BORDER_BEVEL);
        #endregion

        #region BorderColor
        /// <summary>
        /// Gets and sets the badge border color.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The border color of the badge. Empty means no border.")]
        [RefreshProperties(RefreshProperties.All)]
        [KryptonDefaultColor]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    _needPaint?.Invoke(this, new NeedLayoutEventArgs(true));
                }
            }
        }

        private bool ShouldSerializeBorderColor() => BorderColor != _defaultBorderColor;

        /// <summary>
        /// Resets the BorderColor property to its default value.
        /// </summary>
        public void ResetBorderColor() => BorderColor = _defaultBorderColor;
        #endregion

        #region BorderSize
        /// <summary>
        /// Gets and sets the badge border size (thickness in pixels).
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The border size (thickness in pixels) of the badge. 0 means no border.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        public int BorderSize
        {
            get => _borderSize;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (_borderSize != value)
                {
                    _borderSize = value;
                    _needPaint?.Invoke(this, new NeedLayoutEventArgs(true));
                }
            }
        }

        private bool ShouldSerializeBorderSize() => BorderSize != DEFAULT_BORDER_SIZE;

        /// <summary>
        /// Resets the BorderSize property to its default value.
        /// </summary>
        public void ResetBorderSize() => BorderSize = DEFAULT_BORDER_SIZE;
        #endregion

        #region BorderBevel
        /// <summary>
        /// Gets and sets the type of bevel effect for the badge border.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The type of bevel effect for the badge border (None, Raised, or Inset).")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(BadgeBevelType.None)]
        public BadgeBevelType BorderBevel
        {
            get => _borderBevel;
            set
            {
                if (_borderBevel != value)
                {
                    _borderBevel = value;
                    _needPaint?.Invoke(this, new NeedLayoutEventArgs(true));
                }
            }
        }

        private bool ShouldSerializeBorderBevel() => BorderBevel != DEFAULT_BORDER_BEVEL;

        /// <summary>
        /// Resets the BorderBevel property to its default value.
        /// </summary>
        public void ResetBorderBevel() => BorderBevel = DEFAULT_BORDER_BEVEL;
        #endregion

        #region Reset
        /// <summary>
        /// Resets all border values to their defaults.
        /// </summary>
        public void Reset()
        {
            ResetBorderColor();
            ResetBorderSize();
            ResetBorderBevel();
        }
        #endregion
    }
}