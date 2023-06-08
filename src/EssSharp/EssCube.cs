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
        public void ClearDataFromCube( EssJobClearDataOptions options = null ) => ClearDataFromCubeAsync(options).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task ClearDataFromCubeAsync( EssJobClearDataOptions options = null, CancellationToken cancellationToken = default )
        {
            try
            {
                options ??= new EssJobClearDataOptions();

                options.ApplicationName = Application.Name;
                options.CubeName = Name;

                var job = (await Application.Server.CreateJob(options).ExecuteAsync(cancellationToken).ConfigureAwait(false)).ThrowIfFailed();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to clear data from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>an <see cref="IEssCubeVariable"/></returns>
        public IEssCubeVariable CreateCubeVariable( string name, string value ) => CreateCubeVariableAsync(name, value).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>an <see cref="IEssCubeVariable"/></returns>
        public async Task<IEssCubeVariable> CreateCubeVariableAsync( string name, string value, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(name) )
                throw new ArgumentException($"A variable name is required to create an {nameof(EssCubeVariable)}.", nameof(name));

            if ( string.IsNullOrWhiteSpace(value) )
                throw new ArgumentException($"A value is required to create an {nameof(EssCubeVariable)}.", nameof(value));
            try
            {
                var variableInfo = new Variable(name: name, value: value);
                var api = GetApi<VariablesApi>();
                if ( await api.VariablesCreateVariableAsync(applicationName: _application.Name, databaseName: Name, body: variableInfo, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } variable )
                    throw new Exception("Could not create cube variable.");

                return new EssCubeVariable(variable, this);
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create cube variable ""{name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns><see cref="Stream"/></returns>
        public Stream ExportCubeToWorkbook( EssJobExportExcelOptions options = null ) => ExportCubeToWorkbookAsync( options ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="Stream"/></returns>
        public Task<Stream> ExportCubeToWorkbookAsync( EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default ) => 
            Application.ExportCubeToWorkbookAsync(Name, options, cancellationToken);

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
        /// <returns>An <see cref="EssDrillthroughReport"/> object.</returns>
        public IEssDrillthroughReport GetDrillthroughReport( string reportName, bool getDetails = false ) => GetDrillthroughReportAsync(reportName, getDetails)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssDrillthroughReport"/> object.</returns>
        public async Task<IEssDrillthroughReport> GetDrillthroughReportAsync( string reportName, bool getDetails = false, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(reportName) )
                throw new ArgumentException($"A report name is required to get an {nameof(EssDrillthroughReport)}.", nameof(reportName));

            try
            {
                var api = GetApi<DrillThroughReportsApi>();

                if ( (await GetDrillthroughReportsAsync(false, cancellationToken).ConfigureAwait(false)).FirstOrDefault(dtr => string.Equals(dtr?.Name, reportName, StringComparison.OrdinalIgnoreCase)) is not { } report )
                    throw new Exception($@"The given {nameof(reportName)} could not be found.");

                if ( getDetails )
                    await report.GetDetailsAsync(cancellationToken).ConfigureAwait(false);

                return report;
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the report ""{reportName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDrillthroughReport"/> objects.</returns>
        public List<IEssDrillthroughReport> GetDrillthroughReports( bool getDetails = false ) => GetDrillthroughReportsAsync(getDetails)?.GetAwaiter().GetResult() ?? new List<IEssDrillthroughReport>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssDrillthroughReport"/> objects.</returns>
        public async Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( bool getDetails = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DrillThroughReportsApi>();
                var reports = (await api.DrillThroughReportsGetReportsAsync(_application?.Name, _cube?.Name, 0, cancellationToken).ConfigureAwait(false))?
                    .ToEssSharpList(this) ?? new List<IEssDrillthroughReport>();

                if ( getDetails )
                    reports.ForEach(async dtr => await dtr.GetDetailsAsync(cancellationToken));

                return reports;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssLock"/> objects.</returns>
        public List<IEssLock> GetLockedObjects() => GetLockedObjectsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <param name="cancellationToken"></param>
        /// <returns>A list of <see cref="IEssLock"/> objects.</returns>
        public async Task<List<IEssLock>> GetLockedObjectsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();
                var lockedObjects = await api.LocksGetLockedObjectsAsync(Application.Name, Name, null, null, 0, cancellationToken).ConfigureAwait(false);

                return lockedObjects?.ToEssSharpList(this) ?? new List<IEssLock>();
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

                foreach (var script in await GetScriptsAsync(cancellationToken).ConfigureAwait(false))
                {
                    if ( string.Equals(script?.Name, scriptName, StringComparison.OrdinalIgnoreCase) )
                        return script;
                }

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

        /// <inheritdoc />
        public void LoadDataToCube( EssJobLoadDataOptions options ) => LoadDataToCubeAsync(options).GetAwaiter().GetResult();


        /// <inheritdoc />
        public async Task LoadDataToCubeAsync( EssJobLoadDataOptions options, CancellationToken cancellationToken = default )
        {
            try
            {
                // Check that EssJobLoadDataOptions is not null
                if ( options is null )
                    throw new ArgumentException($"{nameof(EssJobLoadDataOptions)} is required to load data to a {nameof(EssCube)}.");

                // Add Application and Cube name to options
                options.ApplicationName = Application.Name;
                options.CubeName = Name;
                IEssFolder folder = null;

                if ( options.File?.Any() != true )
                {
                    if ( !string.IsNullOrEmpty(options.DataFileLocalPath) && options.DataFileStream is null ) 
                                throw new FileNotFoundException("Unable to find the data file at the given local path.", options.DataFileLocalPath);

                    folder = await Application.Server.GetFolderAsync($@"catalog/application/{Application.Name}/{Name}").ConfigureAwait(false);
                    var file = !string.IsNullOrEmpty(options.DataFileLocalPath) ?
                        await folder.UploadFileAsync(path: options.DataFileLocalPath, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false) :
                        await folder.UploadFileAsync(stream: options.DataFileStream, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false);

                    options.File = new list<string>() { file.FullPath };
                }

                if ( options.Rule is null )
                {
                    if ( !string.IsNullOrEmpty(options.RuleFileLocalPath) && options.RuleFileStream is null )
                        throw new FileNotFoundException("Unable to find the data file at the given local path.", options.DataFileLocalPath);

                    folder = await Application.Server.GetFolderAsync($@"/application/{Application.Name}/{Name}").ConfigureAwait(false);
                    var file = !string.IsNullOrEmpty(options.RuleFileLocalPath) ?
                        await folder.UploadFileAsync(path: options.RuleFileLocalPath, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false) :
                        await folder.UploadFileAsync(stream: options.RuleFileStream, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false);

                    options.File = new list<string>() { file.FullPath };
                }

                // Check that options.File is not null - this field holds the file name of the data being loaded and is required
                if ( options.File?.Any() != true)
                  throw new Exception($"A server file is required to load data to {Name}.");

                // create and execute the Load Data job
                (await Application.Server.CreateJob(options).ExecuteAsync(cancellationToken).ConfigureAwait(false)).ThrowIfFailed();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to load data to cube ""{Name}"". {e.Message}", e);
            }
        }
        #endregion
    }
}
