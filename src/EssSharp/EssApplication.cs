﻿using System;
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

        private readonly EssServer _server;
        
        private readonly Application _application;

        #endregion

        #region Constructors

        /// <summary />
        public EssApplication( EssServer server, Application application ) : base(server?.Configuration, server?.Client)
        {
            _server = server;
            _application = application;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => _application?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Application;

        #endregion

        #region IEssApplication Members

        /// <inheritdoc />
        public List<IEssCube> GetCubes() => GetCubesAsync()?.GetAwaiter().GetResult() ?? new List<IEssCube>();

        public IEssServer Server => _server;

        /// <inheritdoc />
        public async Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var cubes = await api.ApplicationsGetCubesAsync(_application?.Name, null, null, 0, cancellationToken).ConfigureAwait(false);

                return cubes?.ToEssSharpList(this) ?? new List<IEssCube>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<List<IEssApplicationVariable>> GetVariablesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<VariablesApi>();
                var variableList = await api.VariablesListAppVariablesAsync(_application?.Name, 0, cancellationToken);
                return variableList.Items.Select(variable => new EssApplicationVariable(this, variable)).Cast<IEssApplicationVariable>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task StartAsync( CancellationToken cancellationToken = default )
        {
            var   api = GetApi<ApplicationsApi>();
            await api.ApplicationsPerformOperationAsync(_application?.Name, "Start", 0, cancellationToken);
            // in practice, it seems that 'start' also works or the input parameter is simply not case-sensitive
        }

        /// <inheritdoc />
        public async Task StopAsync( CancellationToken cancellationToken = default )
        {
            var   api = GetApi<ApplicationsApi>();
            await api.ApplicationsPerformOperationAsync(_application?.Name, "Stop", 0, cancellationToken);
        }

        #endregion

    }
}
