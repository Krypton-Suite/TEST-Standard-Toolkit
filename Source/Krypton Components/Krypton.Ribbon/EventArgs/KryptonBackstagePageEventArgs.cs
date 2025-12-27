namespace Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection event args with specific type.
    /// </summary>
    public class KryptonBackstagePageEventArgs : TypedCollectionEventArgs<KryptonBackstagePage>
    {
        /// <summary>
        /// Initialize a new instance of the KryptonBackstagePageEventArgs class.
        /// </summary>
        /// <param name="item">Page affected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public KryptonBackstagePageEventArgs(KryptonBackstagePage? item, int index)
            : base(item, index)
        {
        }
    }
}