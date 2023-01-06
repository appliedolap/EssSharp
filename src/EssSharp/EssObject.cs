using System;
using System.Runtime.CompilerServices;

namespace EssSharp
{
    /// <summary />
    public abstract class EssObject : IEssObject
    {
        #region Constructors

        /// <summary />
        internal EssObject() { }

        /// <summary />
        /// <param name="configuration" />
        /// <param name="client" />
        internal EssObject( EssSharp.Client.Configuration configuration, EssSharp.Client.ApiClient client = null )
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Client        = client ?? new EssSharp.Client.ApiClient(configuration.BasePath);
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public virtual string Name => throw new NotImplementedException();

        /// <inheritdoc />
        public virtual EssType Type => throw new NotImplementedException();

        #endregion

        #region Internal Properties and Methods

        /// <summary>
        /// The <see cref="EssSharp.Client.ApiClient"/> assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual EssSharp.Client.ApiClient Client { get; set; }

        /// <summary>
        /// The <see cref="EssSharp.Client.Configuration"/> assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual EssSharp.Client.Configuration Configuration { get; set; }

        /// <summary>
        /// Gets the requested API using the <see cref="EssSharp.Client.Configuration"/> and <see cref="EssSharp.Client.ApiClient"/> 
        /// assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        /// <typeparam name="T">An <see cref="EssSharp.Client.IApiAccessor"/> (API) from the <see cref="EssSharp.Api"/> namespace.</typeparam>
        /// <param name="callerPath" />
        /// <param name="callerName" />
        /// <returns>An <see cref="EssSharp.Client.IApiAccessor"/> (API) from the <see cref="EssSharp.Api"/> namespace.</returns>
        internal T GetApi<T>( [CallerFilePath] string callerPath = null, [CallerMemberName] string callerName = null ) where T : EssSharp.Client.IApiAccessor, new() => 
            ApiFactory.GetApi<T>(Configuration, Client, callerPath, callerName);

        #endregion
    }
}
