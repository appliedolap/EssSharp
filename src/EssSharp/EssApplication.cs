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
        #region Private Data

        private readonly EssServer   server;
        private readonly Application application;

        #endregion

        #region Constructors

        /// <summary />
        public EssApplication( EssServer server, Application application ) : base(server?.Configuration, server?.Client)
        {
            this.server      = server;
            this.application = application;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => application?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Application;

        #endregion

        #region IEssApplication Members

        /// <inheritdoc />
        public async Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var cubes = (await api.ApplicationsGetCubesAsync(application?.Name, null, null, 0, cancellationToken))?.Items?
                    .Select(cube => new EssCube(this, cube) as IEssCube)?.ToList() ?? new List<IEssCube>();

                return cubes;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task StartAsync( CancellationToken cancellationToken)
        {
            var   api = GetApi<ApplicationsApi>();
            await api.ApplicationsPerformOperationAsync(application?.Name, "Start", 0, cancellationToken);
            // in practice, it seems that 'start' also works or the input parameter is simply not case-sensitive
        }

        /// <inheritdoc />
        public async Task StopAsync( CancellationToken cancellationToken )
        {
            var   api = GetApi<ApplicationsApi>();
            await api.ApplicationsPerformOperationAsync(application?.Name, "Stop", 0, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api = GetApi<VariablesApi>();
                //var variables = (await api.VariablesListAppVariablesAsync(application?.Name, 0, cancellationToken))?.Items?
                    //.Select(variable => new EssApplicationVariable(this, variable) as IEssVariable)?.ToList() ?? new List<IEssVariable>();

                return null;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
