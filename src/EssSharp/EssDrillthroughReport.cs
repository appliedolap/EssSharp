using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                if (_cube?.Application?.Server is EssServer server && await server.FeatureSupport.GetNotSupportedExceptionAsync(EssFeature.DrillthroughReportExecution, e, cancellationToken).ConfigureAwait(false) is { } notSupportedException)
                    throw notSupportedException;

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
                // Otherwise, if typed values should be returned AND we have at least one data row AND at least one column type is not a string...
                // (The prefixing pass above already typed the numeric columns when typed values were requested, and the string
                //  columns it prefixed must not be read again, since that would drop the prefix.)
                else if (options.ReturnTypedValues && lastSourceIndex >= 2 && columnTypes.Any(ct => !string.Equals("string", ct, StringComparison.OrdinalIgnoreCase)))
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

                // If typed values should be returned AND we have at least one data row, return a string column that holds dates
                // throughout as dates: Essbase types a date column as a string, and its text is the JDBC driver's, in the escape
                // form the JDBC specification fixes, so the form is the only signal there is and it is the same on every server.
                if (options.ReturnTypedValues && lastSourceIndex >= 2)
                {
                    int firstDataRow = options.IncludeColumnHeaders && firstSourceIndex < sourceDataIndex ? 1 : 0;

                    for ( int c = 0; c < secondDimLength; ++c)
                    {
                        if (columnTypes[c]?.ToLowerInvariant() is "double" or "long")
                            continue;

                        ReadDateColumn(report, c, firstDataRow, options.PrefixStringValuesForExcel);
                    }
                }
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( IndexOutOfRangeException )
            {
                throw new InvalidOperationException($"The given {nameof(JArray)} is not rectangular.");
            }

            return (report, columnTypes);
        }

        /// <summary>
        /// Replaces a column's strings with <see cref="DateTime"/> values when every one of them is a date as a JDBC driver writes
        /// one (see <see cref="TryReadDriverDate"/>). A column with any other text, or with no text at all, is left as it is, so a
        /// column is always one type; an empty cell stays empty.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="column">The column.</param>
        /// <param name="firstRow">The first data row, below the column headers when the report has them.</param>
        /// <param name="prefixed">Whether string values carry the apostrophe prefix for Excel, which a date does not keep.</param>
        private static void ReadDateColumn( object[,] report, int column, int firstRow, bool prefixed )
        {
            int rows   = report.GetLength(0);
            var dates  = new DateTime?[rows];
            var blanks = new bool[rows];
            bool any   = false;

            for ( int r = firstRow; r < rows; r++ )
            {
                if (report[r, column] is not string text)
                    continue;

                // The prefix for Excel goes on every string, an empty one included.
                if (prefixed && text.Length > 0 && text[0] == '\'')
                    text = text.Substring(1);

                if (text.Length is 0)
                {
                    blanks[r] = true;
                    continue;
                }

                if (!TryReadDriverDate(text, out var date))
                    return;

                dates[r] = date;
                any      = true;
            }

            if (!any)
                return;

            // The column is dates: each date as a value, and an empty cell empty, with no prefix to show.
            for ( int r = firstRow; r < rows; r++ )
            {
                if (dates[r] is { } date)
                    report[r, column] = date;
                else if (blanks[r])
                    report[r, column] = null;
            }
        }

        /// <summary>
        /// Reads a date as a JDBC driver writes one with <c>getString</c>: the JDBC escape form <c>yyyy-MM-dd</c>, optionally
        /// followed by <c> HH:mm:ss</c>, optionally followed by a fraction of a second of up to nine digits, of which the seven
        /// that <see cref="DateTime"/> keeps are read. The date is read in the invariant culture with an unspecified kind.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="date">The date, when the text is one.</param>
        private static bool TryReadDriverDate( string text, out DateTime date )
        {
            date = default;

            int length = text?.Length ?? 0;

            if (length is not (10 or 19) && (length < 21 || length > 29))
                return false;

            if (!AreDigits(text, 0, 4) || text[4] != '-' || !AreDigits(text, 5, 2) || text[7] != '-' || !AreDigits(text, 8, 2))
                return false;

            if (length is 10)
                return DateTime.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (text[10] != ' ' || !AreDigits(text, 11, 2) || text[13] != ':' || !AreDigits(text, 14, 2) || text[16] != ':' || !AreDigits(text, 17, 2))
                return false;

            if (length is 19)
                return DateTime.TryParseExact(text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (text[19] != '.' || !AreDigits(text, 20, length - 20))
                return false;

            // The fraction, cut to the seven digits a DateTime keeps.
            return DateTime.TryParseExact(text.Substring(0, 20 + Math.Min(7, length - 20)), "yyyy-MM-dd HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        /// <summary>
        /// Whether the given run of characters is all digits.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="start">The first character of the run.</param>
        /// <param name="count">The number of characters in the run.</param>
        private static bool AreDigits( string text, int start, int count )
        {
            for ( int i = start; i < start + count; i++ )
            {
                if (text[i] < '0' || text[i] > '9')
                    return false;
            }

            return true;
        }

        #endregion
    }
}