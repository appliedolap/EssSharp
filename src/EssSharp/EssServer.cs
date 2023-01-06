using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;

namespace EssSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class EssServer : EssObject, IEssServer
    {
        #region Private Data

        private const string defaultRestApiPath = "/rest/v1";
        private const int    maxApplications    = 100;

        private readonly string server;

        #endregion

        #region Constructors

        /// <summary />
        internal EssServer( Client.Configuration configuration, Client.ApiClient client ) : base(configuration, client) 
        {
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server REST endpoint must be set on the configuration.", nameof(configuration));

            int defaultRestApiPathIndex = configuration.BasePath.ToLowerInvariant().LastIndexOf(defaultRestApiPath);

            if ( defaultRestApiPathIndex >= 0 )
                this.server = configuration.BasePath.Substring(0, defaultRestApiPathIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EssServer( string server, string username, string password )
        {
            this.server = server?.TrimEnd('/');

            var basePath = $@"{this.server}{defaultRestApiPath}";

            if ( !Uri.TryCreate(basePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server URL is required.", nameof(server));

            if ( string.IsNullOrEmpty(username) )
                throw new ArgumentException("A username is required.", nameof(username));

            Client        = new Client.ApiClient(basePath);
            Configuration = new Client.Configuration()
            {
                BasePath  = basePath,
                Username  = username,
                Password  = password,
                Timeout   = TimeSpan.FromMilliseconds(int.MaxValue).Milliseconds,
                UserAgent = $"{nameof(EssSharp)}/{typeof(EssServer).Assembly.GetName().Version}"
            };

            var test = new EssServer(Configuration, Client);
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => server;

        /// <inheritdoc />
        public override EssType Type => EssType.Server;

        #endregion

        #region IEssServer Members

        /// <inheritdoc />
        /// <remarks>The number of returned applications is limited to the value of <see cref="maxApplications"/>.</remarks>
        public async Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var applications = (await api.ApplicationsGetApplicationsAsync(null, null, maxApplications, null, null, null, 0, cancellationToken))?.Items?
                    .Select(application => new EssApplication(this, application) as IEssApplication)?.ToList() ?? new List<IEssApplication>();

                return applications;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
