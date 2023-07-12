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

        #region IEssCube Properties

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        public EssCubeType CubeType => _cube.Type.HasValue && Enum.IsDefined(typeof(EssCubeType), (int)_cube.Type) ? (EssCubeType)_cube.Type : EssCubeType.Unknown;

        #endregion

        #region IEssCube Methods

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
        /// <returns><see cref="IEssLockObject"/></returns>
        public IEssLockObject CreateLockObject( EssLockOptions lockOptions ) => CreateLockObjectAsync(lockOptions).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="IEssLockObject"/></returns>
        public async Task<IEssLockObject> CreateLockObjectAsync( EssLockOptions lockOptions, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();

                if ( await api.LocksLockObjectAsync(applicationName: Application.Name, databaseName: Name, lockOptions.ToLockObject(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } lockObject )
                    throw new Exception($@"Unable to lock object {lockOptions.LockObjectName}.");

                return new EssLockObject(lockObject, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to lock object {lockOptions.LockObjectName} on cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssScript"/> object of type <typeparamref name="T"/>.</returns>
        public T CreateScript<T>( string name, string content = null ) where T : class, IEssScript => CreateScriptAsync<T>(name, content).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssScript"/> object of type <typeparamref name="T"/>.</returns>
        public async Task<T> CreateScriptAsync<T>( string name, string content = null, CancellationToken cancellationToken = default ) where T : class, IEssScript 
        {
            // Throw if a specific type of IEssScript is not given.
            if ( typeof(T) == typeof(IEssScript) )
                throw new ArgumentException($"A specific type of {nameof(IEssScript)} must be given.", "T");

            // Get the script type associated with the given IEssScript interface.
            var scriptType = Extensions.GetScriptType<T>();

            if ( string.IsNullOrWhiteSpace(name) )
                throw new ArgumentException($"A script name is required to create a new {scriptType} script.", nameof(name));

            // If a script name with an extension was given...
            if ( Path.HasExtension(name) )
            {
                // ...and the extension is appropriate for the given script type
                bool hasAppropriateExtension = Path.GetExtension(name)?.ToLowerInvariant() switch
                {
                    ".csc" => scriptType is EssScriptType.Calc,
                    ".rep" => scriptType is EssScriptType.Report,
                    ".mdx" => scriptType is EssScriptType.MDX,
                    ".msh" => scriptType is EssScriptType.MaxL,
                    _      => false
                };

                // Strip an appropriate extension for the given script type.
                if ( hasAppropriateExtension )
                    name = Path.GetFileNameWithoutExtension(name);
            }

            try
            {
                // Create a new specific IEssScript of the given type with the given name and content.
                var script = Extensions.CreateScript<T>(new Script() { Name = name, Content = content }, this);
                
                // Save the script to the server.
                return await script.SaveAsync<T>(cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create {scriptType} script ""{name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void ExecuteScript( EssJobScriptOptions options ) => ExecuteScriptAsync( options ).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task ExecuteScriptAsync( EssJobScriptOptions options, CancellationToken cancellationToken = default )
        {
            try
            {
                if (options is null ) 
                    throw new ArgumentNullException($@"{nameof(EssJobScriptOptions)} initialized with a script name or {nameof(IEssScript)} is a required parameter.");

                options.ApplicationName = Application.Name;
                options.CubeName = Name;

                (await Application.Server.CreateJob(options).ExecuteAsync().ConfigureAwait(false)).ThrowIfFailed();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to load data to cube ""{Name}"". {e.Message}", e);
            }

        }

        /// <inheritdoc />
        /// <returns><see cref="string"/></returns>
        public string ExecuteReportQuery(string name, string query) => ExecuteReportQueryAsync(name, query).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="string"/></returns>
        public async Task<string> ExecuteReportQueryAsync( string name, string query, CancellationToken cancellationToken = default ) =>
            await new EssReportScript(new Script() {Content = query, Name = name}, this).QueryAsync().ConfigureAwait(false);

        /// <inheritdoc />
        /// <returns><see cref="EssQueryReport"/></returns>
        public EssQueryReport ExecuteMDXQuery(string query, EssQueryPreferences preferences = null) => ExecuteMDXQueryAsync(query, preferences).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="EssQueryReport"/></returns>
        public async Task<EssQueryReport> ExecuteMDXQueryAsync(string query, EssQueryPreferences preferences = null, CancellationToken cancellationToken = default ) => 
            await new EssMdxScript(new Script() { Content = query }, this).QueryAsync(preferences, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        /// <returns><see cref="Stream"/></returns>
        public Stream ExportCubeToWorkbook( EssJobExportExcelOptions options = null ) => ExportCubeToWorkbookAsync( options ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="Stream"/></returns>
        public Task<Stream> ExportCubeToWorkbookAsync( EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default ) => 
            Application.ExportCubeToWorkbookAsync(Name, options, cancellationToken);

        /// <inheritdoc />
        /// <returns><see cref="string"/></returns>
        public string GetActiveAlias() => GetActiveAliasAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="string"/></returns>
        public async Task<string> GetActiveAliasAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();

                if ( await api.ApplicationsGetActiveAliasAsync(applicationName: Application.Name, databaseName: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } activeAlias )
                    throw new Exception("Could not get active alias.");

                return activeAlias;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get Active Alias for cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="string"/></returns>
        public List<string> GetAliases() => GetAliasesAsync().GetAwaiter().GetResult();

        /// <inherit />
        /// <returns>A list of <see cref="string"/></returns>
        public async Task<List<string>> GetAliasesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();

                if ( await api.ApplicationsGetAliasesAsync(applicationName: Application.Name, databaseName: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } aliasList )
                    throw new Exception("Could not get list of aliases.");

                return aliasList?.Items;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get Active Alias for cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns><see cref="IEssGrid"/></returns>
        public IEssGrid GetDefaultGrid( bool reset = false ) => GetDefaultGridAsync(reset).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="IEssGrid"/></returns>
        public async Task<IEssGrid> GetDefaultGridAsync( bool reset = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GridApi>();

                if ( await api.GridGetDefaultAsync(applicationName: Application.Name, databaseName: Name, reset: reset, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception("Cannot get default grid.");

                return new EssGrid(grid, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get default grid of cube ""{Name}"". {e.Message}", e);
            }
        }

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
                {
                    foreach ( var report in reports )
                        await report.GetDetailsAsync(cancellationToken).ConfigureAwait(false);
                }

                return reports;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFile"/> object.</returns>
        public IEssFile GetFile( string fileName ) => GetFileAsync( fileName ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFile"/> object.</returns>
        public async Task<IEssFile> GetFileAsync( string fileName, CancellationToken cancellationToken = default )
        {
            try
            {
                foreach ( var file in await GetFilesAsync().ConfigureAwait(false) )
                {
                    if ( string.Equals(file.Name, fileName, StringComparison.OrdinalIgnoreCase) )
                        return file;
                }

                throw new Exception($@"Unable to get file {fileName}.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get file {fileName} from ""application/{Application.Name}/{Name}"" from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssFile"/> objects.</returns>
        public List<IEssFile> GetFiles() => GetFilesAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssFile"/> objects.</returns>
        public async Task<List<IEssFile>> GetFilesAsync( CancellationToken cancellationToken = default)
        {
            try
            {
                return await (await GetFolderAsync(cancellationToken).ConfigureAwait(false)).GetFilesAsync(cancellationToken: cancellationToken) ??
                    throw new Exception($@"Unable to get files.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get files from ""application/{Application.Name}/{Name}"" from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public IEssFolder GetFolder() => GetFolderAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public async Task<IEssFolder> GetFolderAsync( CancellationToken cancellationToken = default)
        {
            try
            {
                return await Application.Server.GetFolderAsync($@"applications/{Application.Name}/{Name}", cancellationToken).ConfigureAwait(false) ??
                    throw new Exception($@"Unable to retrieve folder ""application/{Application.Name}/{Name}"".");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get folder from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssLockObject"/> object.</returns>
        public IEssLockObject GetLockedObject( string name ) => GetLockedObjectAsync( name ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssLockObject"/> object.</returns>
        public async Task<IEssLockObject> GetLockedObjectAsync( string name, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();

                foreach ( var lockObject in await GetLockedObjectsAsync( cancellationToken ).ConfigureAwait(false) )
                {
                    if ( String.Equals(name, lockObject.Name, StringComparison.OrdinalIgnoreCase ) )
                        return lockObject; 
                }
                
                throw new Exception($@"Could not get locked object {name}.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get locked objects from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssLockObject"/> objects.</returns>
        public List<IEssLockObject> GetLockedObjects( ) => GetLockedObjectsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <param name="cancellationToken"></param>
        /// <returns>A list of <see cref="IEssLockObject"/> objects.</returns>
        public async Task<List<IEssLockObject>> GetLockedObjectsAsync(CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();

                if ( await api.LocksGetLockedObjectsAsync(applicationName: Application.Name, databaseName: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } lockObjects )
                    throw new Exception("Cannot get locked objects.");

                return lockObjects.ToEssSharpList(this) ?? new List<IEssLockObject>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get locked objects from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void Unlock<T>( List<T> lockedList ) where T : class, IEssLock  => UnlockAsync<T>(lockedList).GetAwaiter().GetResult(); 

        /// <inheritdoc />
        public async Task UnlockAsync<T>(List<T> lockedList, CancellationToken cancellationToken = default ) where T : class, IEssLock
        {
            try
            {
                foreach ( var locked in lockedList )
                    await locked.UnlockAsync( cancellationToken ).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get locked objects from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssLockBlock"/> objects.</returns>
        public List<IEssLockBlock> GetLockedBlocks() => GetLockedBlocksAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssLockBlock"/> objects.</returns>
        public async Task<List<IEssLockBlock>> GetLockedBlocksAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();

                if ( await api.LocksGetLockedBlocksAsync(applicationName: Application.Name, databaseName: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } lockBlocks )
                    throw new Exception("Cannot get locked blocks.");

                return lockBlocks?.ToEssSharpList(this) ?? new List<IEssLockBlock>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to load get locked blocks from cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns> An <see cref="IEssScript"/> object of type <typeparamref name="T"/>. </returns>
        public T GetScript<T>( string scriptName, bool getContent = false ) where T : class, IEssScript => GetScriptAsync<T>(scriptName, getContent).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssScript"/> object of type <typeparamref name="T"/>.</returns>
        public async Task<T> GetScriptAsync<T>( string scriptName, bool getContent = false, CancellationToken cancellationToken = default ) where T : class, IEssScript
        {
            // Throw if a specific type of IEssScript is not given.
            if ( typeof(T) == typeof(IEssScript) )
                throw new ArgumentException($"A specific type of {nameof(IEssScript)} must be requested.", "T");

            // Get the script type associated with the given IEssScript interface.
            var scriptType = Extensions.GetScriptType<T>();

            if ( string.IsNullOrWhiteSpace(scriptName) )
                throw new ArgumentException($"A script name is required to get {(scriptType is EssScriptType.MDX ? "an" : "a")} {scriptType} script.", nameof(scriptName));

            // If a script name with an extension was given...
            if ( Path.HasExtension(scriptName) )
            {
                // ...and the extension is appropriate for the given script type
                bool hasAppropriateExtension = Path.GetExtension(scriptName)?.ToLowerInvariant() switch
                {
                    ".csc" => scriptType is EssScriptType.Calc,
                    ".rep" => scriptType is EssScriptType.Report,
                    ".mdx" => scriptType is EssScriptType.MDX,
                    ".msh" => scriptType is EssScriptType.MaxL,
                    _      => false
                };

                // Strip an appropriate extension for the given script type.
                if ( hasAppropriateExtension )
                    scriptName = Path.GetFileNameWithoutExtension(scriptName);
            }

            try
            {
                foreach ( var script in await GetScriptsAsync<T>(false, cancellationToken).ConfigureAwait(false) )
                {
                    if ( string.Equals(script.Name, scriptName, StringComparison.OrdinalIgnoreCase) )
                    {
                        if ( getContent )
                            await script.GetContentAsync(cancellationToken).ConfigureAwait(false);

                        return script;
                    }
                }

                throw new Exception($"Script not found.");
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the {scriptType} script named ""{scriptName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssScript"/> objects of <typeparamref name="T"/>.</returns>
        public List<T> GetScripts<T>( bool getContent = false ) where T : class, IEssScript => GetScriptsAsync<T>(getContent).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssScript"/> objects of <typeparamref name="T"/>.</returns>
       public async Task<List<T>> GetScriptsAsync<T>( bool getContent = false, CancellationToken cancellationToken = default ) where T : class, IEssScript
        {
            // Throw if a specific type of IEssScript is not given.
            if ( typeof(T) == typeof(IEssScript) )
                throw new ArgumentException($"A specific type of {nameof(IEssScript)} must be requested.", "T");

            var scriptType = Extensions.GetScriptType<T>();

            try
            {
                var api = GetApi<ScriptsApi>();
                var scripts = (await api.ScriptsListScriptsAsync(applicationName: Application.Name, databaseName: _cube.Name, file: scriptType.ToString()?.ToLowerInvariant(), cancellationToken: cancellationToken).ConfigureAwait(false))?
                    .ToEssSharpList<T>(this) ?? new List<T>();

                if ( getContent )
                {
                    foreach ( var script in scripts )
                        await script.GetContentAsync(cancellationToken).ConfigureAwait(false);
                }

                return scripts;
            }
            catch ( Exception e ) 
            {
                throw new Exception($@"Unable to get the list of {scriptType} scripts. {e.Message}", e); 
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
        public bool IsScenariosEnabled() => IsScenariosEnabledAsync().GetAwaiter().GetResult();


        /// <inheritdoc />
        /// <returns>true if scenarios are enabled, else false</returns>
        public async Task<bool> IsScenariosEnabledAsync()
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

                // If the data list is null or empty...
                if ( options.File?.Any() != true )
                {
                    if ( !File.Exists(options.LocalDataFilePath) && options.LocalDataFileStream is null ) 
                        throw new FileNotFoundException("Unable to find the data file at the given local path.", options.LocalDataFilePath);

                    folder = await Application.Server.GetFolderAsync($@"/applications/{Application.Name}/{Name}").ConfigureAwait(false);
                    var dataFile = File.Exists(options.LocalDataFilePath) ?
                        await folder.UploadFileAsync(path: options.LocalDataFilePath, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false) :
                        await folder.UploadFileAsync(stream: options.LocalDataFileStream, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false);

                    options.File = new list<string>() { $@"catalog{dataFile.FullPath}" };
                }

                // If the rule list is null...
                if ( options.Rule is null )
                {
                    if ( !File.Exists(options.LocalRuleFilePath) && options.LocalRuleFileStream is null )
                    {
                        options.Rule = new List<string>() { "" };
                    }
                    else
                    {
                        folder = await Application.Server.GetFolderAsync($@"/applications/{Application.Name}/{Name}").ConfigureAwait(false);
                        var ruleFile = File.Exists(options.LocalRuleFilePath) ?
                            await folder.UploadFileAsync(path: options.LocalRuleFilePath, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false) :
                            await folder.UploadFileAsync(stream: options.LocalRuleFileStream, overwrite: true, cancellationToken: cancellationToken).ConfigureAwait(false);

                        options.Rule = new list<string>() { $@"catalog{ruleFile.FullPath}" };
                    }
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
