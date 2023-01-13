namespace EssSharp
{
    /// <summary>
    /// A factory for creating new <see cref="EssServer"/> instances.
    /// </summary>
    public class EssServerFactory : IEssServerFactory
    {
        /// <summary>
        /// Creates a new <see cref="EssServer"/> from the given configuration.
        /// </summary>
        /// <inheritdoc />
        public IEssServer CreateEssServer( string server, string username, string password ) => new EssServer(server, username, password);
    }
}
