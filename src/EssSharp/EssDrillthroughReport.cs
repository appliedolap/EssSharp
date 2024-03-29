﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

using Newtonsoft.Json.Linq;

namespace EssSharp
{
    /// <summary />
    public class EssDrillthroughReport : EssObject, IEssDrillthroughReport
    {
        #region Private Data

        private readonly EssCube    _cube;
        private readonly ReportBean _report;

        private DrillthroughBean _definition;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDrillthroughReport( DrillthroughBean report, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _definition = report ??
                throw new ArgumentNullException(nameof(report), $"An API model {nameof(report)} is required to create an {nameof(EssDrillthroughReport)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssDrillthroughReport)}.");
        }

        /// <summary />
        internal EssDrillthroughReport( ReportBean report, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _report = report ??
                throw new ArgumentNullException(nameof(report), $"An API model {nameof(report)} is required to create an {nameof(EssDrillthroughReport)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssDrillthroughReport)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _definition?.Name ?? _report?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.DrillthroughReport;

        #endregion

        #region IEssDrillthroughReport Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public EssDrillthroughDetails Details => _definition?.ToEssSharpDetails();

        /// <inheritdoc />
        public (object[,] report, string[] columnTypes) Execute( IEssDrillthroughRange context, IEssDrillthroughOptions options = null ) => Execute(new List<IEssDrillthroughRange>() { context }, options);

        /// <inheritdoc />
        public (object[,] report, string[] columnTypes) Execute( IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null ) => ExecuteAsync(context, options)?.GetAwaiter().GetResult() ?? (new string[0, 0], new string[0]);

        /// <inheritdoc />
        public Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEssDrillthroughRange context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default ) => ExecuteAsync(new List<IEssDrillthroughRange>() { context }, options, cancellationToken);

        /// <inheritdoc />
        public async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( IEnumerable<IEssDrillthroughRange> context, IEssDrillthroughOptions options = null, CancellationToken cancellationToken = default )
        {
            if ( context?.Any(dtr => dtr is not null) is not true )
                throw new ArgumentException($"At least one {nameof(EssDrillthroughRange)} is required to execute an {nameof(EssDrillthroughReport)}.", nameof(context));

            try
            {
                var api = GetApi<DrillThroughReportsApi>();
                var response = await api.DrillThroughReportsExecuteWithHttpInfoAsync(_cube?.Application?.Name, _cube?.Name, _definition?.Name ?? _report?.Name, context.ToModelBean(options), 0, cancellationToken).ConfigureAwait(false);

                if ( response?.Data is not JArray jArray )
                    throw new Exception($"Received an empty or invalid response. {response?.RawContent}".TrimEnd());

                return To2DReport(jArray, options);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( e is WebException { Response: WebExceptionRestResponse { StatusCode: HttpStatusCode.MethodNotAllowed } } )
                    throw new NotSupportedException($@"Drillthrough report execution is not supported by this server version. {e.Message}", e);

                throw new Exception($@"Unable to execute or process the drillthrough report ""{_definition?.Name ?? _report?.Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void GetDetails() => GetDetailsAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task GetDetailsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DrillThroughReportsApi>();

                if ( await api.DrillThroughReportsGetReportAsync(Cube?.Application?.Name, _cube?.Name, _definition?.Name ?? _report?.Name, 0, cancellationToken).ConfigureAwait(false) is not { } definition )
                    throw new Exception("Received an empty or invalid response.");

                _definition = definition;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the details for report ""{_definition?.Name ?? _report?.Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DrillThroughReportsApi>();
                await api.DrillThroughReportsDeleteAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, name: Name, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to delete report ""{Name}"". {e.Message}", e);
            }
        }

        #endregion

        #region Private Methods

        /// <summary />
        /// <param name="source" />
        /// <param name="options" />
        /// <exception cref="InvalidOperationException" />
        private (object[,] report, string[] columnTypes) To2DReport( JArray source, IEssDrillthroughOptions options = null )
        {
            // Construct a new EssDrillthroughOptions if one is not given.
            options ??= new EssDrillthroughOptions();

            if ( source is null )
                return (null, null);

            if ( source.Count is 0 )
                return (new string[0, 0], new string[0]);

            // Skip the headers row and capture metadata only if requested.
            int firstSourceIndex = options.IncludeColumnHeaders ? 1 : 2;
            int lastSourceIndex  = options.IncludeMetadataOnly ? 1 : source.Count - 1;

            int sourceDataIndex = lastSourceIndex < 2 ? -1 : 2;

            var secondDimLength = source[0].Value<JArray>().Count;
            var report          = new object[lastSourceIndex - firstSourceIndex + 1, secondDimLength];
            var columnTypes     = new string[secondDimLength];

            // Capture the column types from the first row of the report.
            for ( int c = 0; c < secondDimLength; ++c )
                columnTypes[c] = source[0][c].Value<string>();

            try
            {
                // If string values should be prefixed as Excel text AND we have at least one data row AND at least one column type is a string...
                if ( options.PrefixStringValuesForExcel && lastSourceIndex >= 2 && columnTypes.Any(ct => string.Equals("string", ct, StringComparison.OrdinalIgnoreCase)) )
                {
                    // Iterate over columns first, since we need to prepend every value in at least one column with an apostrophe.
                    for ( int c = 0; c < secondDimLength; ++c )
                    {
                        switch ( columnTypes[c]?.ToLowerInvariant() )
                        {
                            case "double":
                            {
                                // If values should be typed...
                                if ( options.ReturnTypedValues )
                                {
                                    // If there is a column header row, process that first.
                                    if ( options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex )
                                    {
                                        // Process the column header row first.
                                        report[0, c] = source[firstSourceIndex][c].Value<string>();

                                        // Iterate over data rows, capturing each cell value as a double.
                                        for ( int ri = 1, si = firstSourceIndex + 1; si <= lastSourceIndex; ri++, si++ )
                                            report[ri, c] = source[si][c].Value<double>();
                                    }
                                    else
                                    {
                                        // Iterate over rows, capturing each cell value as a double.
                                        for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                            report[ri, c] = source[si][c].Value<double>();
                                    }
                                }
                                else
                                {
                                    // Iterate over rows, capturing each cell value as a string.
                                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<string>();
                                }
                                break;
                            }

                            case "long":
                            {
                                // If values should be typed...
                                if ( options.ReturnTypedValues )
                                {
                                    // If there is a column header row, process that first.
                                    if ( options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex )
                                    {
                                        // Process the column header row first.
                                        report[0, c] = source[firstSourceIndex][c].Value<string>();

                                        // Iterate over data rows, capturing each cell value as a long.
                                        for ( int ri = 1, si = firstSourceIndex + 1; si <= lastSourceIndex; ri++, si++ )
                                            report[ri, c] = source[si][c].Value<long>();
                                    }
                                    else
                                    {
                                        // Iterate over rows, capturing each cell value as a long.
                                        for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                            report[ri, c] = source[si][c].Value<long>();
                                    }
                                }
                                else
                                {
                                    // Iterate over rows, capturing each cell value as a string.
                                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<string>();
                                }
                                break;
                            }

                            default:
                            {
                                // If there is a column header row, process that first.
                                if ( options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex )
                                {
                                    // Process the column header row first.
                                    report[0, c] = source[firstSourceIndex][c].Value<string>();

                                    // Iterate over data rows, prepending each cell value with an apostrophe.
                                    for ( int ri = 1, si = firstSourceIndex + 1; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = '\'' + source[si][c].Value<string>();
                                }
                                else
                                {
                                    // Iterate over rows, prepending each cell value with an apostrophe.
                                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = '\'' + source[si][c].Value<string>();
                                }
                                break;
                            }
                        }
                    }
                }
                // If typed values should be returned AND we have at least one data row AND at least one column type is not a string...
                if ( options.ReturnTypedValues && lastSourceIndex >= 2 && columnTypes.Any(ct => !string.Equals("string", ct, StringComparison.OrdinalIgnoreCase)) )
                {
                    // Iterate over columns first, since we need to convert every value to the appropriate return type.
                    for ( int c = 0; c < secondDimLength; ++c )
                    {
                        switch ( columnTypes[c]?.ToLowerInvariant() )
                        {
                            case "double":
                            {
                                // If there is a column header row, process that first.
                                if ( options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex )
                                {
                                    // Process the column header row first.
                                    report[0, c] = source[firstSourceIndex][c].Value<string>();

                                    // Iterate over data rows, capturing each cell value as a double.
                                    for ( int ri = 1, si = firstSourceIndex + 1; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<double>();
                                }
                                else
                                {
                                    // Iterate over rows, capturing each cell value as a double.
                                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<double>();
                                }
                                break;
                            }

                            case "long":
                            {
                                // If there is a column header row, process that first.
                                if ( options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex )
                                {
                                    // Process the column header row first.
                                    report[0, c] = source[firstSourceIndex][c].Value<string>();

                                    // Iterate over data rows, capturing each cell value as a long.
                                    for ( int ri = 1, si = firstSourceIndex + 1; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<long>();
                                }
                                else
                                {
                                    // Iterate over rows, capturing each cell value as a long.
                                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                        report[ri, c] = source[si][c].Value<long>();
                                }
                                break;
                            }

                            default:
                            {
                                // Iterate over rows, capturing each cell value as a string.
                                for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                                    report[ri, c] = source[si][c].Value<string>();

                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Iterate over rows, capturing each cell value as a string.
                    for ( int ri = 0, si = firstSourceIndex; si <= lastSourceIndex; ri++, si++ )
                        for ( int c = 0; c < secondDimLength; ++c )
                            report[ri, c] = source[si][c].Value<string>();
                }
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( IndexOutOfRangeException )
            {
                throw new InvalidOperationException($"The given {nameof(JArray)} is not rectangular.");
            }

            return (report, columnTypes);
        }

        #endregion
    }
}