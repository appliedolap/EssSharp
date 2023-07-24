using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssDatasourceConnection : IEssObject
    {
        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        public IEssServer Server { get; }

        /// <summary>
        /// Returns the Connection Type.
        /// </summary>
        public EssDatasourceConnectionType ConnectionType { get; }

        /// <summary>
        /// Returns the Connection Subtype.
        /// </summary>
        public EssDatasourceConnectionSubtype ConnectionSubtype { get; }

        /// <summary>
        /// Returns Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the Path.
        /// </summary>
        public string Path {get; }

        /// <summary>
        /// Returns the folder path.
        /// </summary>
        public bool Catalog { get; }

        /// <summary>
        /// Returns the host.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Returns the port.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Returns the user.
        /// </summary>
        public string User { get; }

        /// <summary>
        /// Returns 
        /// </summary>
        //public string Password { get; }

        /// <summary>
        /// Returns true if password is encrypted.
        /// </summary>
        public bool Encrypted { get; }

        /// <summary>
        /// Returns the token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        ///
        /// </summary>
        public string Sid { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Service { get; }

        /// <summary>
        /// Returns the schema.
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Returns the cube URL.
        /// </summary>
        public string DbURL { get; }

        /// <summary>
        /// Returns the cube driver.
        /// </summary>
        public string DbDriver { get; }

        /// <summary>
        /// Returns the Datasource name.
        /// </summary>
        public string DatasourceName { get; }

        /// <summary>
        /// Returns the wallet path.
        /// </summary>
        public string WalletPath { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool RepoWallet { get; }

        /// <summary>
        /// Returns the minimum pool size.
        /// </summary>
        public int MinPoolSize { get; }

        /// <summary>
        /// Returns the maximum pool size.
        /// </summary>
        public int MaxPoolSize { get; }

        /// <summary>
        /// Tests the connection
        /// </summary>
        public void TestConnection();

        /// <summary>
        /// Tests the connection
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task TestConnectionAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Delete a connection
        /// </summary>
        public void DeleteConnection();

        /// <summary>
        /// Delete a connection
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task DeleteConnectionAsync( CancellationToken cancellationToken = default );

    }
}
