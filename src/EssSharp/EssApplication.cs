using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplication : EssObject, IEssApplication
    {
        private readonly EssServer   server;
        private readonly Application application;

        /// <summary />
        public EssApplication( EssServer server, Application application ) : base(server.Configuration, server.Client)
        {
            this.server      = server;
            this.application = application;
        }

        /// <inheritdoc />
        public override string Name => application?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Application;

        /// <summary>
        /// Gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <returns>A list of <see cref="EssCube" /> objects under this application.</returns>
        public async Task<List<EssCube>> GetCubesAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api = ApiClientFactory.GetApi<ApplicationsApi>(Configuration, Client);
                var cubes = (await api.ApplicationsGetCubesAsync(application?.Name, null, null, 0, cancellationToken))?.Items?
                    .Select(x => new EssCube(this, x))?.ToList() ?? new List<EssCube>();

                return cubes;
            }
            catch ( Exception )
            {
                throw;
            }
        }
    }
}
