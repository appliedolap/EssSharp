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
        /// <param name="aliasTable" />
        public void Execute( IEssDrillThroughRange context, string aliasTable = null );

        /// <summary>
        /// Executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        public void Execute( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        /// <param name="cancellationToken" />
        public Task ExecuteAsync( IEssDrillThroughRange context, string aliasTable = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        /// <param name="cancellationToken" />
        public Task ExecuteAsync( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null, CancellationToken cancellationToken = default );
    }
}
