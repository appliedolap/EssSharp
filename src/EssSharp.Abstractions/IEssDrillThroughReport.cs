﻿using System.Threading.Tasks;
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
        public (string[,] report, string[] columnTypes) Execute( IEssDrillThroughRange context, string aliasTable = null );

        /// <summary>
        /// Executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        public (string[,] report, string[] columnTypes) Execute( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        /// <param name="cancellationToken" />
        public Task<(string[,] report, string[] columnTypes)> ExecuteAsync( IEssDrillThroughRange context, string aliasTable = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously executes the drill-through report and returns records.
        /// </summary>
        /// <param name="context" />
        /// <param name="aliasTable" />
        /// <param name="cancellationToken" />
        public Task<(string[,] report, string[] columnTypes)> ExecuteAsync( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null, CancellationToken cancellationToken = default );
    }
}