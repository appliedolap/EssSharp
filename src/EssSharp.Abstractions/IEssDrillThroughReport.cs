using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDrillThroughReport : IEssObject
    {
        /// <summary>
        /// Returns the cube of this report.
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// Executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        public (object[,] report, string[] columnTypes) Execute( IEssDrillThroughRange context, IEssDrillthroughOptions options );

        /// <summary>
        /// Executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        public (object[,] report, string[] columnTypes) Execute( IEnumerable<IEssDrillThroughRange> context, IEssDrillthroughOptions options );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEssDrillThroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEnumerable<IEssDrillThroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default );
    }

    /// <summary>
    /// Fluent extensions for <see cref="IEssDrillThroughReport"/>.
    /// </summary>
    public static class IEssDrillThroughReportExtensions
    {
        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillThroughReport> drillThroughReportTask, IEssDrillThroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillThroughReportTask).ExecuteAsync(new List<IEssDrillThroughRange>() { context }, options, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( this Task<IEssDrillThroughReport> drillThroughReportTask, IEnumerable<IEssDrillThroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) =>
            await (await drillThroughReportTask).ExecuteAsync(context, options, cancellationToken).ConfigureAwait(false);
    }
}
