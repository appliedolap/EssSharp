namespace EssSharp
{
    /// <summary />
    public interface IEssAbout
    {
        /// <summary>
        /// Returns the server build revision.
        /// </summary>
        public string Build { get; }

        /// <summary>
        /// Returns the server description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the server version.
        /// </summary>
        public string Version { get; }
    }
}
