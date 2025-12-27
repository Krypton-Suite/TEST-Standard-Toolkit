namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for badge overflow value information.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BadgeOverflowValues : Storage
    {
        #region Static Fields
        private const string DEFAULT_OVERFLOW_TEXT = "99+";
        private const int DEFAULT_OVERFLOW_NUMBER = 99;
        #endregion

        #region Instance Fields
        private readonly NeedPaintHandler? _needPaint;
        private string _overflowText;
        private int _overflowNumber;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the BadgeOverflowValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public BadgeOverflowValues(NeedPaintHandler? needPaint)
        {
            _needPaint = needPaint;
            _overflowText = DEFAULT_OVERFLOW_TEXT;
            _overflowNumber = DEFAULT_OVERFLOW_NUMBER;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsDefault => (OverflowText == DEFAULT_OVERFLOW_TEXT) &&
                                          (OverflowNumber == DEFAULT_OVERFLOW_NUMBER);
        #endregion

        #region OverflowText
        /// <summary>
        /// Gets and sets the text to display when the badge value exceeds OverflowNumber.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The text to display when the badge numeric value exceeds OverflowNumber (e.g., '99+').")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("99+")]
        public string OverflowText
        {
            get => _overflowText ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
            set
            {
                if (_overflowText != value)
                {
                    _overflowText = value;
                    _needPaint?.Invoke(true);
                }
            }
        }

        private bool ShouldSerializeOverflowText() => OverflowText != DEFAULT_OVERFLOW_TEXT;

        /// <summary>
        /// Resets the OverflowText property to its default value.
        /// </summary>
        public void ResetOverflowText() => OverflowText = DEFAULT_OVERFLOW_TEXT;
        #endregion

        #region OverflowNumber
        /// <summary>
        /// Gets and sets the threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The threshold number. If the badge text value (as a number) exceeds this value, OverflowText is displayed instead. Set to 0 to disable overflow checking.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(99)]
        public int OverflowNumber
        {
            get => _overflowNumber;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (_overflowNumber != value)
                {
                    _overflowNumber = value;
                    _needPaint?.Invoke(true);
                }
            }
        }

        private bool ShouldSerializeOverflowNumber() => OverflowNumber != DEFAULT_OVERFLOW_NUMBER;

        /// <summary>
        /// Resets the OverflowNumber property to its default value.
        /// </summary>
        public void ResetOverflowNumber() => OverflowNumber = DEFAULT_OVERFLOW_NUMBER;
        #endregion

        #region Reset
        /// <summary>
        /// Resets all overflow values to their defaults.
        /// </summary>
        public void Reset()
        {
            ResetOverflowText();
            ResetOverflowNumber();
        }
        #endregion
    }
}