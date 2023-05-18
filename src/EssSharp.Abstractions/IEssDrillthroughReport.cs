﻿using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDrillthroughReport : IEssObject
    {
        /// <summary>
        /// Returns the cube of this report.
        /// </summary>
        public IEssCube Cube { get; }

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
    }

    /// <summary>
    /// Fluent extensions for <see cref="IEssDrillthroughReport"/>.
    /// </summary>
    public static class IEssDrillthroughReportExtensions
    {
        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillthroughReport> drillThroughReportTask, IEssDrillthroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillThroughReportTask).ExecuteAsync(new List<IEssDrillthroughRange>() { context }, options, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillthroughReport> drillThroughReportTask, IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillThroughReportTask).ExecuteAsync(context, options, cancellationToken).ConfigureAwait(false);
    }
}