namespace EssSharp
{
    /// <summary />
    public interface IEssObject
    {
        /// <summary>
        /// The name of the thing.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the thing.
        /// </summary>
        public EssType Type { get; }
    }
}
