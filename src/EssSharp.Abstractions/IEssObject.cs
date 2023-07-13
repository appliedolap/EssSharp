namespace EssSharp
{
    /// <summary>
    /// An Essbase object.
    /// </summary>
    public interface IEssObject
    {
        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public EssType Type { get; }
    }
}
