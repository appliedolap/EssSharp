﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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
        /// <param name="reportName" />
        public IEssDrillthroughReport GetDrillthroughReport( string reportName );

        /// <summary>
        /// Asynchronously gets the drillthrough report with the given name.
        /// </summary>
        /// <param name="reportName" />
        /// <param name="cancellationToken" />
        public Task<IEssDrillthroughReport> GetDrillthroughReportAsync( string reportName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of drillthrough reports for this cube.
        /// </summary>
        public List<IEssDrillthroughReport> GetDrillthroughReports();

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports for this cube.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( CancellationToken cancellationToken = default );

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
        /// <returns></returns>
        public IEssScript GetScript( string scriptName );

        /// <summary>
        /// Returns a specific script in a cube
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssScript> GetScriptAsync( string scriptName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of scripts in a cube
        /// </summary>
        public List<IEssScript> GetScripts();

        /// <summary>
        /// Returns a list of scripts in a cube
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssScript>> GetScriptsAsync( CancellationToken cancellationToken = default );

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
        /// <param name="cancellationToken" />
        public static async Task<IEssDrillthroughReport> GetDrillthroughReportAsync( this Task<IEssCube> cubeTask, string reportName, CancellationToken cancellationToken = default ) =>
            await (await cubeTask).GetDrillthroughReportAsync(reportName, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously gets the list of drillthrough reports.
        /// </summary>
        /// <param name="cancellationToken" />
        public static async Task<List<IEssDrillthroughReport>> GetDrillthroughReportsAsync( this Task<IEssCube> cubeTask, CancellationToken cancellationToken = default ) =>
            await (await cubeTask).GetDrillthroughReportsAsync(cancellationToken).ConfigureAwait(false);
    }
}
