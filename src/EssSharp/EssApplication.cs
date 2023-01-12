using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly EssServer   _server;
        private readonly Application _application;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplication( Application application, EssServer server = null ) : base(server?.Configuration, server?.Client)
        {
            _application = application ?? 
                throw new ArgumentNullException(nameof(application), $"An API model {nameof(application)} is required to create an {nameof(EssApplication)}.");

            _server = server;
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
        public IEssServer Server => _server;

        /// <inheritdoc />
        public Stream DownloadLatestLogFile() => DownloadLatestLogFileAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<Stream> DownloadLatestLogFileAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationLogsApi>();
                var fileStream = await api.ApplicationLogsDownloadLatestLogFileStreamAsync(_application?.Name, 0, cancellationToken).ConfigureAwait(false);

                return fileStream;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public string DownloadLatestLogFileString() => DownloadLatestLogFileStringAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<string> DownloadLatestLogFileStringAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<ApplicationLogsApi>();
                var fileContent = await api.ApplicationLogsDownloadLatestLogFileContentAsync(_application?.Name, 0, cancellationToken).ConfigureAwait(false);

                return fileContent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public List<IEssCube> GetCubes() => GetCubesAsync()?.GetAwaiter().GetResult() ?? new List<IEssCube>();

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
        public List<IEssApplicationVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssApplicationVariable>();

        /// <inheritdoc />
        public async Task<List<IEssApplicationVariable>> GetVariablesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<VariablesApi>();
                var variableList = await api.VariablesListAppVariablesAsync(_application?.Name, 0, cancellationToken).ConfigureAwait(false);
                return variableList.Items.Select(variable => new EssApplicationVariable(this, variable)).Cast<IEssApplicationVariable>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Copy(String copyName ) => CopyAsync(copyName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task CopyAsync( String copyName, CancellationToken cancellationToken = default )
        {
            CopyRenameBean copy = new (_application?.Name, copyName);
            try
            {
                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsCopyApplicationAsync(copy, 0, cancellationToken).ConfigureAwait(false); ;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsDeleteApplicationAsync(_application?.Name, 0, cancellationToken).ConfigureAwait(false); ;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Start() => StartAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task StartAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<ApplicationsApi>();
            // in practice, it seems that 'start' also works or the input parameter is simply not case-sensitive
            await api.ApplicationsPerformOperationAsync(_application?.Name, "Start", 0, cancellationToken).ConfigureAwait(false);            
        }

        /// <inheritdoc />
        public void Stop() => StopAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task StopAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<ApplicationsApi>();
            // in practice, it seems that 'stop' also works or the input parameter is simply not case-sensitive
            await api.ApplicationsPerformOperationAsync(_application?.Name, "Stop", 0, cancellationToken).ConfigureAwait(false);
        }
        #endregion
    }
}
