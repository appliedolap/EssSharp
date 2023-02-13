using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

using Newtonsoft.Json.Linq;

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
        public (string[,] report, string[] columnTypes) Execute( IEssDrillThroughRange context, string aliasTable = null ) => Execute(new List<IEssDrillThroughRange>() { context }, aliasTable);

        /// <inheritdoc />
        public (string[,] report, string[] columnTypes) Execute( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null ) => ExecuteAsync(context, aliasTable)?.GetAwaiter().GetResult() ?? (new string[0, 0], new string[0]);

        /// <inheritdoc />
        public Task<(string[,] report, string[] columnTypes)> ExecuteAsync( IEssDrillThroughRange context, string aliasTable = null, CancellationToken cancellationToken = default ) => ExecuteAsync(new List<IEssDrillThroughRange>() { context }, aliasTable, cancellationToken);

        /// <inheritdoc />
        public async Task<(string[,] report, string[] columnTypes)> ExecuteAsync( IEnumerable<IEssDrillThroughRange> context, string aliasTable = null, CancellationToken cancellationToken = default )
        {
            if ( context?.Any(dtr => dtr is not null) is not true )
                throw new ArgumentException($"At least one {nameof(EssDrillThroughRange)} is required to execute an {nameof(EssDrillThroughReport)}.", nameof(context));

            try
            {
                var api = GetApi<DrillThroughReportsApi>();
                var response = await api.DrillThroughReportsExecuteWithHttpInfoAsync(_cube?.Application?.Name, _cube?.Name, _definition?.Name ?? _report?.Name, context.ToModelBean(aliasTable), 0, cancellationToken).ConfigureAwait(false);

                if ( response.Data is JArray jArray )
                    return To2DReport(jArray);

                throw new Exception($"Received an empty or invalid response. {response.RawContent}");
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to execute the drill-through report ""{_definition?.Name ?? _report?.Name}"". {e.Message}", e);
            }
        }

        #endregion

        #region Private Methods

        private (string[,] report, string[] columnTypes) To2DReport( JArray source, bool assertLength = false )
        {
            if ( source is null )
                return (null, null);

            if ( source.Count is 0 )
                return (new string[0, 0], new string[0]);

            var secondDimLength = source[0].Value<JArray>().Count;
            var report          = new string[source.Count, secondDimLength];
            var columnTypes     = new string[secondDimLength];

            if ( assertLength )
            {
                for ( int j = 0; j < secondDimLength; ++j )
                    columnTypes[j] = source[0][j].Value<string>();

                for ( int ri = 0, si = 1; si < source.Count; ri++, si++ )
                {
                    if ( source[si].Value<JArray>().Count != secondDimLength )
                        throw new InvalidOperationException($"The given {nameof(JArray)} is not rectangular.");

                    for ( int j = 0; j < secondDimLength; ++j )
                        report[ri, j] = source[si][j].Value<string>();
                }
            }
            else
            {
                for ( int j = 0; j < secondDimLength; ++j )
                    columnTypes[j] = source[0][j].Value<string>();

                for ( int ri = 0, si = 1; si < source.Count; ri++, si++ )
                    for ( int j = 0; j < secondDimLength; ++j )
                        report[ri, j] = source[si][j].Value<string>();
            }

            return (report, columnTypes);
        }

        #endregion
    }
}