using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;

namespace EssSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class EssServer : EssObject, IEssServer
    {
        private const string defaultRestApiPath = "/rest/v1";
        private const int    maxApplications    = 100;

        private readonly string server;

        /// <summary />
        internal EssServer( Configuration configuration, ApiClient client ) : base(configuration, client) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EssServer( string server, string username, string password )
        {
            var basePath = $@"{server?.TrimEnd()}{defaultRestApiPath}";

            if ( !Uri.TryCreate(basePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server URL is required.", nameof(server));

            this.server = server;

            if ( string.IsNullOrEmpty(username) )
                throw new ArgumentException("A username is required.", nameof(username));

            Client = new ApiClient(basePath);
            Configuration = new Configuration()
            {
                BasePath  = basePath,
                Username  = username,
                Password  = password,
                Timeout   = TimeSpan.FromMilliseconds(int.MaxValue).Milliseconds,
                UserAgent = "EssSharp.Client/1.0.0.0"
            };
        }

        /// <inheritdoc />
        public override string Name => server;

        /// <inheritdoc />
        public override EssType Type => EssType.Server;

        /// <summary>
        /// Gets the list of applications for this server available to the currently connected user.
        /// </summary>
        /// <returns> A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="maxApplications"/>.</remarks>
        public async Task<List<EssApplication>> GetApplicationsAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api = ApiClientFactory.GetApi<ApplicationsApi>(Configuration, Client);
                var applications = (await api.ApplicationsGetApplicationsAsync(null, null, maxApplications, null, null, null, 0, cancellationToken))?.Items?
                    .Select(application => new EssApplication(this, application))?.ToList() ?? new List<EssApplication>();

                return applications;
            }
            catch ( Exception )
            {
                throw;
            }
        }
    }
}
