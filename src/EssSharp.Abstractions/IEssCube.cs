using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssCube : IEssObject
    {
        /// <summary>
        /// Returns the parent application of the cube.
        /// </summary>
        public IEssApplication Application { get; }

        /// <summary>
        /// Returns the type of the cube.
        /// </summary>
        public EssCubeType CubeType { get; }

        /// <summary>
        /// Clears data from a cube.
        /// </summary>
        /// <param name="options"></param>
        public void ClearDataFromCube( EssJobClearDataOptions options = null );

        /// <summary>
        /// Asynchronously clears data from a cube.
        /// </summary>
        /// <param name="options">Options for clearingn data from a cube.</param>
        /// <param name="cancellationToken"></param>
        public Task ClearDataFromCubeAsync( EssJobClearDataOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Create a cube variable.
        /// </summary>
        /// <param name="name">Name of cube variable.</param>
        /// <param name="value">Value to store in the cube variable.</param>
        public IEssCubeVariable CreateCubeVariable( string name, string value );

        /// <summary>
        /// Asynchronously create a cube variable.
        /// </summary>
        /// <param name="name">Name of cube variable.</param>
        /// <param name="value">Value to store in the cube variable.</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssCubeVariable> CreateCubeVariableAsync( string name, string value, CancellationToken cancellationToken = default );

        /// <summary>
        /// Lock an object on the server.
        /// </summary>
        /// <param name="lockOptions">Includes file name, and file type</param>
        public IEssLockObject CreateLockObject( EssLockOptions lockOptions );

        /// <summary>
        /// Asynchronously lock an object on the server.
        /// </summary>
        /// <param name="lockOptions">Includes file name, and file type</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssLockObject> CreateLockObjectAsync( EssLockOptions lockOptions, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a script with the given name (and type <typeparamref name="T"/>) on the cube.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        /// <param name="content">(optional) The content of the new script.</param>
        /// <remarks>Creates an <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public T CreateScript<T>( string name, string content = null ) where T : class, IEssScript;

        /// <summary>
        /// Asynchronously creates a script with the given name (and type <typeparamref name="T"/>) on the cube.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        /// <param name="content">(optional) The content of the new script.</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>Creates an <see cref="IEssScript"/> of the specific type <typeparamref name="T"/>.</remarks>
        public Task<T> CreateScriptAsync<T>( string name, string content = null, CancellationToken cancellationToken = default ) where T : class, IEssScript;

        /// <summary>
        /// Executes a report query.
        /// </summary>
        /// <param name="name">Name of report query.</param>
        /// <param name="query">Report query to execute.</param>
        /// <returns></returns>
        public string ExecuteReportQuery( string name, string query );

        /// <summary>
        /// Asynchronously executes a report query.
        /// </summary>
        /// <param name="name">Name of report query.</param>
        /// <param name="query">Report query to execute.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> ExecuteReportQueryAsync( string name, string query, CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes an MDX query.
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="preferences">Execution preferences</param>
        public EssQueryReport ExecuteMDXQuery( string query, EssQueryPreferences preferences = null );

        /// <summary>
        /// Asynchronously executes an MDX query.
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="preferences">Execution preferences</param>
        /// <param name="cancellationToken"></param>
        public Task<EssQueryReport> ExecuteMDXQueryAsync( string query, EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes a script (Calc or MDX) on a cube.
        /// </summary>
        /// <param name="options"></param>
        public void ExecuteScript( EssJobScriptOptions options );

        /// <summary>
        /// Asynchronously executes a script (Calc or MDX) on a cube.
        /// </summary>
        /// <param name="options">Options incude script name or report script name, and lock for update</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ExecuteScriptAsync( EssJobScriptOptions options, CancellationToken cancellationToken = default );

        /// <summary>
        /// Exports a cube to an excel workbook.
        /// </summary>
        /// <param name="options">Export options including Build method, calc, data, and include member IDs.</param>
        /// <returns></returns>
        public Stream ExportCubeToWorkbook( EssJobExportExcelOptions options = null );

        /// <summary>
        /// Asynchronously exports a cube to an excel workbook.
        /// </summary>
        /// <param name="options">Export options including Build method, calc, data, and include member IDs.</param>
        /// <param name="cancellationToken"></param>
        public Task<Stream> ExportCubeToWorkbookAsync( EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the active alias of the cube.
        /// </summary>
        public string GetActiveAlias();

        /// <summary>
        /// Asynchronously returns the active alias of the cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<string> GetActiveAliasAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of aliases on a cube.
        /// </summary>
        public List<string> GetAliases();

        /// <summary>
        /// Asynchronously returns a list of aliases on a cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<string>> GetAliasesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the default grid from a cube.
        /// </summary>
        /// <param name="reset">Reset flag to avoid saved grid layout.</param>
        public IEssGrid GetDefaultGrid( bool reset = false );

        /// <summary>
        /// Asynchronously returns the default grid from a cube
        /// </summary>
        /// <param name="reset">Reset flag to avoid saved grid layout.</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> GetDefaultGridAsync( bool reset = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of dimensions.
        /// </summary>
        public List<IEssDimension> GetDimensions();

        /// <summary>
        /// Asynchronously gets the list of dimensions.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssDimension>> GetDimensionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="reportName">The name of the report</param>
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        public IEssDrillthroughReport GetDrillthroughReport( string reportName, bool getDetails = false );

        /// <summary>
        /// Asynchronously gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="reportName">The name of the report.</param> />
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public Task<IEssDrillthroughReport> GetDrillthroughReportAsync( string reportName, bool getDetails = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of drillthrough reports for this cube.
        /// </summary>
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        public List<IEssDrillthroughReport> GetDrillthroughReports( bool getDetails = false );

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports for this cube.
        /// </summary>
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( bool getDetails = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a file with the given file name.
        /// </summary>
        /// <param name="fileName">Name of file on the server.</param>
        /// <returns></returns>
        public IEssFile GetFile( string fileName );

        /// <summary>
        /// Asynchronously gets a file with the given file name.
        /// </summary>
        /// <param name="fileName">Name of file on the server.</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssFile> GetFileAsync( string fileName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of all files on a cube.
        /// </summary>
        public List<IEssFile> GetFiles();

        /// <summary>
        /// Asynchronously gets a list of all files on a cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssFile>> GetFilesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of all folders on a cube.
        /// </summary>
        public IEssFolder GetFolder();

        /// <summary>
        /// Asynchronously gets a list of all folders on a cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssFolder> GetFolderAsync( CancellationToken cancellationToken = default );


        /// <summary>
        /// Gets a locked object with the given name.
        /// </summary>
        /// <param name="name" />
        public IEssLockObject GetLockedObject( string name );

        /// <summary>
        /// Asynchronously gets a locked object with the given name.
        /// </summary>
        /// <param name="name" />
        /// <param name="cancellationToken" />
        public Task<IEssLockObject> GetLockedObjectAsync( string name, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of locked objects.
        /// </summary>
        public List<IEssLockObject> GetLockedObjects();

        /// <summary>
        /// Asynchronously gets a list of locked objects.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssLockObject>> GetLockedObjectsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of locked blocks on a cube.
        /// </summary>
        public List<IEssLockBlock> GetLockedBlocks();

        /// <summary>
        /// Asynchronously gets a list of locked blocks on a cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssLockBlock>> GetLockedBlocksAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Unlocks an object or block of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="lockedList">List of locked objects or blocks to unlock.</param>
        public void Unlock<T>( List<T> lockedList ) where T : class, IEssLock;

        /// <summary>
        /// Asynchronously unlocks an object or block of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="lockedList">List of locked objects or blocks to unlock.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UnlockAsync<T>( List<T> lockedList, CancellationToken cancellationToken = default ) where T : class, IEssLock;

        /// <summary>
        /// Gets the script with the given name (and type <typeparamref name="T"/>) from the cube.
        /// </summary>
        /// <param name="scriptName">The name of the script.</param>
        /// <param name="getContent">Whether to get the full script, including content, or only summary details.</param>
        /// <remarks>Returns an <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public T GetScript<T>( string scriptName, bool getContent = false ) where T : class, IEssScript;

        /// <summary>
        /// Asynchronously gets the script with the given name (and type <typeparamref name="T"/>) from the cube.
        /// </summary>
        /// <param name="scriptName">The name of the script.</param>
        /// <param name="getContent">Whether to get the full script, including content, or only summary details.</param>
        /// <param name="cancellationToken" />
        /// <remarks>Returns an <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public Task<T> GetScriptAsync<T>( string scriptName, bool getContent = false, CancellationToken cancellationToken = default ) where T : class, IEssScript;

        /// <summary>
        /// Gets the list of scripts with the given type <typeparamref name="T"/> from the cube.
        /// </summary>
        /// <param name="getContent">Whether to get the full scripts, including content, or only summary details.</param>
        /// <remarks>Returns a list of <see cref="IEssScript"/> objects of the specific given type <typeparamref name="T"/>.</remarks>
        public List<T> GetScripts<T>( bool getContent = false ) where T : class, IEssScript;

        /// <summary>
        /// Asynchronously gets the list of scripts with the given type <typeparamref name="T"/> from the cube.
        /// </summary>
        /// <param name="getContent">Whether to get the full scripts, including content, or only summary details.</param>
        /// <param name="cancellationToken" />
        /// <remarks>Returns a list of <see cref="IEssScript"/> objects of the specific given type <typeparamref name="T"/>.</remarks>
        public Task<List<T>> GetScriptsAsync<T>( bool getContent = false, CancellationToken cancellationToken = default ) where T : class, IEssScript;

        /// <summary>
        /// Gets the list of cube-scoped variables available to the connected user.
        /// </summary>
        public List<IEssCubeVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of cube-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssCubeVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns true if this cube has scenarios are enabled, else false
        /// </summary>
        public bool IsScenariosEnabled();

        /// <summary>
        /// Asynchronously returns true if this cube has scenarios are enabled, else false
        /// </summary>
        public Task<bool> IsScenariosEnabledAsync();

        /// <summary>
        /// Loads data to a cube from a file on the Server.
        /// </summary>
        /// <param name="options">Options for loading data, includes data file, rule file, and whether to abort on error</param>
        public void LoadDataToCube( EssJobLoadDataOptions options );

        /// <summary>
        /// Asynchronously loads data to a cube from a file on the Server.
        /// </summary>
        /// <param name="options">Options for loading data, includes data file, rule file, and whether to abort on error</param>
        /// <param name="cancellationToken"></param>
        public Task LoadDataToCubeAsync( EssJobLoadDataOptions options, CancellationToken cancellationToken = default );

    }

    /// <summary>
    /// Fluent extensions for <see cref="IEssCube"/>.
    /// </summary>
    public static class FluentIEssCubeExtensions
    {
        /// <summary>
        /// Asynchronously creates a script with the given name (and type <typeparamref name="T"/>) on the cube.
        /// </summary>
        /// <param name="cubeTask" />
        /// <param name="name">The name of the script.</param>
        /// <param name="content">(optional) The content of the new script.</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>Creates an <see cref="IEssScript"/> of the specific type <typeparamref name="T"/>.</remarks>
        public static async Task<T> CreateScriptAsync<T>( this Task<IEssCube> cubeTask, string name, string content = null, CancellationToken cancellationToken = default ) where T : class, IEssScript =>
            await (await cubeTask.ConfigureAwait(false)).CreateScriptAsync<T>(name, content, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="cubeTask" />
        /// <param name="reportName">The name of the report.</param>
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public static async Task<IEssDrillthroughReport> GetDrillthroughReportAsync( this Task<IEssCube> cubeTask, string reportName, bool getDetails = false, CancellationToken cancellationToken = default ) =>
            await (await cubeTask.ConfigureAwait(false)).GetDrillthroughReportAsync(reportName, getDetails, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports.
        /// </summary>
        /// <param name="cubeTask" />
        /// <param name="getDetails">(optional) Whether to get the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public static async Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( this Task<IEssCube> cubeTask, bool getDetails = false, CancellationToken cancellationToken = default ) =>
            await (await cubeTask.ConfigureAwait(false)).GetDrillthroughReportsAsync(getDetails, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the locked object with the given name.
        /// </summary>
        /// <param name="cubeTask" />
        /// <param name="name" />
        /// <param name="cancellationToken" />
        public static async Task<IEssLockObject> GetLockedObjectAsync( this Task<IEssCube> cubeTask, string name, CancellationToken cancellationToken = default ) =>
            await (await cubeTask.ConfigureAwait(false)).GetLockedObjectAsync(name, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the script with the given name (and type <typeparamref name="T"/>) from the cube.
        /// </summary>
        /// <param name="cubeTask" />
        /// <param name="scriptName">The name of the script.</param>
        /// <param name="getContent">Whether to get the full script, including content, or only summary details.</param>
        /// <param name="cancellationToken" />
        /// <remarks>Returns an <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public static async Task<T> GetScriptAsync<T>( this Task<IEssCube> cubeTask, string scriptName, bool getContent = false, CancellationToken cancellationToken = default ) where T : class, IEssScript =>
            await (await cubeTask.ConfigureAwait(false)).GetScriptAsync<T>(scriptName, getContent, cancellationToken).ConfigureAwait(false);
    }
}
