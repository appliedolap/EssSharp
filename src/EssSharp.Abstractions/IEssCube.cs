using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System;
using System.Xml.Linq;

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
        /// Clears data from a cube
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        public void ClearDataFromCube( EssJobClearDataOptions options = null );

        /// <summary>
        /// Clears data from a cube
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        public Task ClearDataFromCubeAsync( EssJobClearDataOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Create a cube variable
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="value"></param>
        public IEssCubeVariable CreateCubeVariable( string name, string value );

        /// <summary>
        /// Create a cube variable
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssCubeVariable> CreateCubeVariableAsync( string name, string value, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="preferences"></param>
        public Object ExecuteMDXQuery( string query, EssQueryPreferences preferences = null );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="preferences"></param>
        /// <param name="cancellationToken"></param>
        public Task<Object> ExecuteMDXQueryAsync( string query, EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes a script (Calc or MDX) on a cube
        /// </summary>
        /// <param name="options"></param>
        public void ExecuteScript( EssJobScriptOptions options );

        /// <summary>
        /// Executes a script (Calc or MDX) on a cube
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ExecuteScriptAsync( EssJobScriptOptions options, CancellationToken cancellationToken = default );

        /// <summary>
        /// Exports a cube to an excel workbook
        /// </summary>
        /// <param name="options"></param>
        public Stream ExportCubeToWorkbook( EssJobExportExcelOptions options = null );

        /// <summary>
        /// Exports a cube to an excel workbook
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        public Task<Stream> ExportCubeToWorkbookAsync( EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the active alias of the cube.
        /// </summary>
        /// <returns></returns>
        public string GetActiveAlias();

        /// <summary>
        /// Returns the active alias of the cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetActiveAliasAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of aliases on a cube.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAliases();

        /// <summary>
        /// Returns a list of aliases on a cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<string>> GetAliasesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the default grid of a cube
        /// </summary>
        /// <param name="reset"></param>
        public IEssGrid GetDefaultGrid( bool reset = false );

        /// <summary>
        /// Returns the default grid of a cube
        /// </summary>
        /// <param name="reset"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> GetDefaultGridAsync( bool reset = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of dimensions.
        /// </summary>
        public List<IEssDimension> GetDimensions();

        /// <summary>
        /// Gets the list of dimensions.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssDimension>> GetDimensionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        /// <param name="reportName" />
        public IEssDrillthroughReport GetDrillthroughReport( string reportName, bool getDetails = false );

        /// <summary>
        /// Asynchronously gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="reportName" />
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public Task<IEssDrillthroughReport> GetDrillthroughReportAsync( string reportName, bool getDetails = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of drillthrough reports for this cube.
        /// </summary>
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        public List<IEssDrillthroughReport> GetDrillthroughReports( bool getDetails = false );

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports for this cube.
        /// </summary>
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( bool getDetails = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of locked objects
        /// </summary>
        public List<IEssLock> GetLockedObjects();

        /// <summary>
        /// Asynchronously gets a list of locked objects
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssLock>> GetLockedObjectsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a  specific scripts in a cube
        /// </summary>
        /// <param name="scriptName"></param>
        public IEssScript GetScript( string scriptName, EssScriptType scriptType );

        /// <summary>
        /// Returns a specific script in a cube
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssScript> GetScriptAsync( string scriptName, EssScriptType scriptType, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of scripts in a cube
        /// </summary>
        public List<IEssScript> GetScripts( EssScriptType scriptType = EssScriptType.Calc );

        /// <summary>
        /// Returns a list of scripts in a cube
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssScript>> GetScriptsAsync( EssScriptType scriptType = EssScriptType.Calc, CancellationToken cancellationToken = default );

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
        public bool isScenariosEnabled();

        /// <summary>
        /// Returns true if this cube has scenarios are enabled, else false
        /// </summary>
        public Task<bool> isScenariosEnabledAsync();

        /// <summary>
        /// Loads data to a cube from a file on the Server
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        public void LoadDataToCube( EssJobLoadDataOptions options );

        /// <summary>
        /// Loads data to a cube from a file on the Server
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        public Task LoadDataToCubeAsync( EssJobLoadDataOptions options, CancellationToken cancellationToken = default );

    }

    /// <summary>
    /// Fluent extensions for <see cref="IEssCube"/>.
    /// </summary>
    public static class IEssCubeExtensions
    {
        /// <summary>
        /// Asynchronously gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="reportName" />
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public static async Task<IEssDrillthroughReport> GetDrillthroughReportAsync( this Task<IEssCube> cubeTask, string reportName, bool getDetails = false, CancellationToken cancellationToken = default ) =>
            await (await cubeTask.ConfigureAwait(false)).GetDrillthroughReportAsync(reportName, getDetails, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports.
        /// </summary>
        /// <param name="getDetails">Whether to the full report specification (or only summary details).</param>
        /// <param name="cancellationToken" />
        public static async Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( this Task<IEssCube> cubeTask, bool getDetails = false, CancellationToken cancellationToken = default ) =>
            await (await cubeTask.ConfigureAwait(false)).GetDrillthroughReportsAsync(getDetails, cancellationToken).ConfigureAwait(false);
    }
}
