using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDrillThroughReport : EssObject, IEssDrillThroughReport
    {
        #region Private Data

        private readonly EssCube          _cube;
        private readonly DrillthroughBean _definition;
        private readonly ReportBean       _report;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDrillThroughReport( DrillthroughBean report, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _definition = report ??
                throw new ArgumentNullException(nameof(report), $"An API model {nameof(report)} is required to create an {nameof(EssDrillThroughReport)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssDrillThroughReport)}.");
        }

        /// <summary />
        internal EssDrillThroughReport( ReportBean report, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _report = report ??
                throw new ArgumentNullException(nameof(report), $"An API model {nameof(report)} is required to create an {nameof(EssDrillThroughReport)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssDrillThroughReport)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _definition?.Name ?? _report?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.DrillThroughReport;

        #endregion

        #region IEssDrillThroughReport Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public void Execute( IEssDrillThroughRange context, string aliasTable = null ) => Execute(new List<IEssDrillThroughRange>() { context }, aliasTable);

        /// <inheritdoc />
        public void Execute( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null ) => ExecuteAsync(context, aliasTable)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public Task ExecuteAsync( IEssDrillThroughRange context, string aliasTable = null, CancellationToken cancellationToken = default ) => ExecuteAsync(new List<IEssDrillThroughRange>() { context }, aliasTable, cancellationToken);

        /// <inheritdoc />
        public async Task ExecuteAsync( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null, CancellationToken cancellationToken = default )
        {
            if ( context?.Any(dtr => dtr is not null) is not true )
                throw new ArgumentException($"At least one {nameof(EssDrillThroughRange)} is required to execute an {nameof(EssDrillThroughReport)}.", nameof(context));

            try
            {
                var api = GetApi<DrillThroughReportsApi>();

                var metadata = context.ToModelBean(aliasTable);

                // TODO: Adjust swagger spec to ensure that a report is returned.
                await api.DrillThroughReportsExecuteAsync(_cube?.Application?.Name, _cube?.Name, _definition?.Name ?? _report?.Name, context.ToModelBean(aliasTable), 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}