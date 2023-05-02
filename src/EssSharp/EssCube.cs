using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssCube : EssObject, IEssCube
    {
        #region Private Data

        private readonly EssApplication _application;
        private readonly Cube           _cube;

        #endregion

        #region Constructors

        /// <summary />
        internal EssCube( Cube cube, EssApplication application ) : base(application?.Configuration, application?.Client)
        {
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An API model {nameof(cube)} is required to create an {nameof(EssCube)}.");

            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssApplication)} {nameof(application)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _cube?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Cube;

        #endregion

        #region IEssCube Members

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDimension"/> objects.</returns>
        public List<IEssDimension> GetDimensions() => GetDimensionsAsync()?.GetAwaiter().GetResult() ?? new List<IEssDimension>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDimension"/> objects.</returns>
        public async Task<List<IEssDimension>> GetDimensionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DimensionsApi>();
                var dimensions = await api.DimensionsListDimensionsAsync(_application?.Name, _cube?.Name, 0, cancellationToken).ConfigureAwait(false);

                return dimensions?.ToEssSharpList(this) ?? new List<IEssDimension>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssDrillThroughReport"/> object.</returns>
        public IEssDrillThroughReport GetDrillThroughReport( string reportName ) => GetDrillThroughReportAsync(reportName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssDrillThroughReport"/> object.</returns>
        public async Task<IEssDrillThroughReport> GetDrillThroughReportAsync( string reportName, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(reportName) )
                throw new ArgumentException($"A report name is required to get an {nameof(EssDrillThroughReport)}.", nameof(reportName));

            try
            {
                var api = GetApi<DrillThroughReportsApi>();

                if ( await api.DrillThroughReportsGetReportAsync(_application?.Name, _cube?.Name, reportName, 0, cancellationToken).ConfigureAwait(false) is { } report )
                    return new EssDrillThroughReport(report, this);

                throw new Exception("Received an empty or invalid response.");
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the report ""{reportName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDrillThroughReport"/> objects.</returns>
        public List<IEssDrillThroughReport> GetDrillThroughReports() => GetDrillThroughReportsAsync()?.GetAwaiter().GetResult() ?? new List<IEssDrillThroughReport>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDrillThroughReport"/> objects.</returns>
        public async Task<List<IEssDrillThroughReport>> GetDrillThroughReportsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DrillThroughReportsApi>();
                var reports = await api.DrillThroughReportsGetReportsAsync(_application?.Name, _cube?.Name, 0, cancellationToken).ConfigureAwait(false);

                return reports?.ToEssSharpList(this) ?? new List<IEssDrillThroughReport>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns> A list of <see cref="IEssScript"/> objects. </returns>
        public IEssScript GetScript( string scriptName ) => GetScriptAsync( scriptName ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns> An <see cref="IEssScript"/> objects. </returns>
        public async Task<IEssScript> GetScriptAsync( string scriptName, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();
                if (await api.ScriptsListScriptsAsync(Application?.Name, _cube.Name, scriptName, 0, cancellationToken).ConfigureAwait(false) is { } script)
                    return new EssScript(script.Items[0], this) as IEssScript;

                throw new Exception($"Cannot find script {scriptName}");
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the script ""{scriptName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns> A list of <see cref="EssScript"/> objects. </returns>
        public List<IEssScript> GetScripts() => GetScriptsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns> A list of <see cref="EssScript"/> objects. </returns>
        public async Task<List<IEssScript>> GetScriptsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();
                var scripts = await api.ScriptsListScriptsAsync(Application?.Name, _cube.Name, null, 0, cancellationToken).ConfigureAwait(false);

                return scripts?.ToEssSharpList(this) ?? new List<IEssScript>();
            }
            catch ( Exception ) 
            {
                throw; 
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssCubeVariable"/> objects.</returns>
        public List<IEssCubeVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssCubeVariable>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssCubeVariable"/> objects.</returns>
        public async Task<List<IEssCubeVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<VariablesApi>();
                var variables = await api.VariablesListVariablesAsync(_application?.Name, _cube?.Name, 0, cancellationToken).ConfigureAwait(false);

                return variables?.ToEssSharpList<IEssCubeVariable>(this) ?? new List<IEssCubeVariable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>true if scenarios are enabled, else false</returns>
        public bool isScenariosEnabled() => isScenariosEnabledAsync().GetAwaiter().GetResult();


        /// <inheritdoc />
        /// <returns>true if scenarios are enabled, else false</returns>
        public async Task<bool> isScenariosEnabledAsync()
        {
            try
            {
                var api = GetApi<ScenariosApi>();
                var scenarioCubeList = await api.ScenariosGetRegisteredCubesAsync().ConfigureAwait(false);

                foreach (var scenarioCube in scenarioCubeList.Items)
                {
                    if (scenarioCube.Application.Equals(this.Application.Name))
                    {
                        var databases = scenarioCube.Databases;
                        return databases.Contains(this.Name);
                    }
                }
                return false;
            }
            catch (Exception) 
            {
                throw;
            }
        }
        #endregion
    }
}
