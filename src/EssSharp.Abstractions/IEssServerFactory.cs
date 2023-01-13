namespace EssSharp
{
    public interface IEssServerFactory
    {
        /// <summary />
        /// <param name="server" />
        /// <param name="username" />
        /// <param name="password" />
        /// <returns>A new <see cref="IEssServer"/> instance.</returns>
        public IEssServer CreateEssServer( string server, string username, string password );
    }
}
