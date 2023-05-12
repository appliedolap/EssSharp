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
        /// Returns the Connection Type
        /// </summary>
        public EssDatasourceConnectionType ConnectionType { get; }

        /// <summary>
        /// Returns the Connection Subtype
        /// </summary>
        public EssDatasourceConnectionSubtype ConnectionSubtype { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public string Path {get; }

        /// <inheritdoc />
        public bool Catalog { get; }

        /// <inheritdoc />
        public string Host { get; }

        /// <inheritdoc />
        public int Port { get; }

        /// <inheritdoc />
        public string User { get; }

        /// <inheritdoc />
        //public string Password { get; }

        /// <inheritdoc />
        public bool Encrypted { get; }

        /// <inheritdoc />
        public string Token { get; }

        /// <inheritdoc />
        public string Sid { get; }

        /// <inheritdoc />
        public string Service { get; }

        /// <inheritdoc />
        public string Schema { get; }

        /// <inheritdoc />
        public string DbURL { get; }

        /// <inheritdoc />
        public string DbDriver { get; }

        /// <inheritdoc />
        public string DatasourceName { get; }

        /// <inheritdoc />
        public string WalletPath { get; }

        /// <inheritdoc />
        public bool RepoWallet { get; }

        /// <inheritdoc />
        public int MinPoolSize { get; }

        /// <inheritdoc />
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
