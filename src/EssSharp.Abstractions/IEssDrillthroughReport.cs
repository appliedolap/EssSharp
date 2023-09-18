﻿using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDrillthroughReport : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the cube of this report.
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// Returns a dictionary with of the dimensions
        /// </summary>
        public Dictionary<string, EssColumnMapping> dimensions { get; }

        #endregion

        /// <summary>
        /// Executes the drillthrough report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        public (object[,] report, string[] columnTypes) Execute( IEssDrillthroughRange context, IEssDrillthroughOptions options );

        /// <summary>
        /// Executes the drillthrough report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        public (object[,] report, string[] columnTypes) Execute( IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options );

        /// <summary>
        /// Asynchronously executes the drillthrough report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEssDrillthroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously executes the drillthrough report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the full report specification.
        /// </summary>
        public Dictionary<string, EssColumnMapping> GetDetails();

        /// <summary>
        /// Asynchronously gets the full report specification.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<Dictionary<string, EssColumnMapping>> GetDetailsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Deletes this report from the cube.
        /// </summary>
        public void Delete();

        /// <summary>
        /// Asynchronously deletes this report from the cube.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="drillthroughReportTask" />
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillthroughReport> drillthroughReportTask, IEssDrillthroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillthroughReportTask.ConfigureAwait(false)).ExecuteAsync(new List<IEssDrillthroughRange>() { context }, options, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="drillthroughReportTask" />
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillthroughReport> drillthroughReportTask, IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillthroughReportTask.ConfigureAwait(false)).ExecuteAsync(context, options, cancellationToken).ConfigureAwait(false);
    }
}
