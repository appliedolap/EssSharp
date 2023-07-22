using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Model;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using TinyCsvParser.Tokenizer;

namespace EssSharp
{
    /// <summary>
    /// Represents a report script that is specific to a cube.
    /// </summary>
    public class EssReportScript : EssScript, IEssReportScript
    {
        #region Constructors

        /// <summary />
        internal EssReportScript( Script script, EssCube cube ) : base(script, cube) { }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.Report;

        /// <inheritdoc />
        public string Report { get; set; }

        #endregion

        #region IEssScript Methods

        /// <inheritdoc />
        /// <returns></returns>
        public EssQueryReport GetReport( EssQueryPreferences preferences = null ) => GetReportAsync(preferences).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssQueryReport" /> object.</returns>
        public async Task<EssQueryReport> GetReportAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default )
        {
            try
            {
                // If the script/query content has not been fetched or set by a consumer,
                // attempt to get it from the server.
                if ( string.IsNullOrEmpty(Content) )
                    await GetContentAsync(cancellationToken).ConfigureAwait(false);

                // Capture the script content.
                var query = Content;

                // In order to process the report output, prepend the report spec with {TABDELIMIT}, if necessary.
                if ( query.IndexOf("{TABDELIMIT}", StringComparison.OrdinalIgnoreCase) is -1 )
                    query = $"{{TABDELIMIT}}\n{query}";

                // To avoid issues with numeric member names/aliases, prepend the report spec with <QUOTEMBRNAMES, if necessary.
                if ( query.IndexOf("<QUOTEMBRNAMES", StringComparison.OrdinalIgnoreCase) is -1 )
                    query = $"<QUOTEMBRNAMES\n{query}";

                // Create new preferences, if necessary.
                preferences ??= new EssQueryPreferences();

                // Create new EssJobScriptOptions and set the LockForUpdate parameter.
                var options = new EssJobScriptOptions(essScript: this)
                {
                    ApplicationName = Cube.Application.Name,
                    CubeName        = Cube.Name,
                    LockForUpdate   = preferences.LockForUpdate,
                };

                // If the report spec is not tab delimited and/or member names are not quoted OR if the script does not exist on the server,
                // then we need to execute an arbitrary spec via the IsScriptContent + ReportScriptFilename parameters.
                if ( !string.Equals(query, Content) || await ExistsAsync(cancellationToken).ConfigureAwait(false) is not true )
                {
                    // Set IsScriptContent to true and pass the query directly via the ReportScriptFilename.
                    options.IsScriptContent      = true;
                    options.ReportScriptFilename = query;
                }

                // Execute the script.
                var job = await ExecuteAsync(options, cancellationToken).ConfigureAwait(false);
                job.JobOutputInfo.TryGetValue("REPORT_OUTPUT", out var output);

                if ( output?.ToString() is not { Length: > 0 } rawReport )
                    throw new Exception($@"Could not get the report from job output. {job.InfoMessage}".TrimEnd());

                return BuildQueryReport(query, rawReport, preferences);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get Report from {Name}. {e.Message}", e);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds and returns a query report from a given query, raw report, and preferences.
        /// </summary>
        private EssQueryReport BuildQueryReport( string query, string rawReport, EssQueryPreferences preferences = null )
        {
            // Set up the regex options to extract the dimensions members from each axis specification command.
            var regexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline;

            // Set up the CSV parser to process the captured dimension member groups for each axis.
            var csvParserOptions = new CsvParserOptions(false, new QuotedStringTokenizer('"', '\\', ','));
            var csvReaderOptions = new CsvReaderOptions(new string[0]);
            var csvParser        = new CsvParser<string[]>(csvParserOptions, new CsvStringArrayMapping());

            // Declare and initialize empty dimension member arrays for each axis.
            var pageDimensionMembers   = new string[0];
            var columnDimensionMembers = new string[0];
            var rowDimensionMembers    = new string[0];

            // Attempt to capture dimension members for each axis from the query.
            if ( Regex.Match(query, @"<\s*PAGE\s*\(([^)]*)\)", regexOptions).Groups is { Count: > 0 } pageGroup && pageGroup[1].Value is { Length: > 0 } pageGroupString )
                pageDimensionMembers = csvParser.ReadFromString(csvReaderOptions, pageGroupString)
                    .SelectMany(row   => row.Result)
                    .Select    (value => value.Trim())
                    .Where     (value => !string.IsNullOrEmpty(value))
                    .ToArray();

            if ( Regex.Match(query, @"<\s*COL(?:UMN)?\s*\(([^)]*)\)", regexOptions).Groups is { Count: > 0 } columnGroup && columnGroup[1].Value is { Length: > 0 } columnGroupString )
                columnDimensionMembers = csvParser.ReadFromString(csvReaderOptions, columnGroupString)
                    .SelectMany(row   => row.Result)
                    .Select    (value => value.Trim())
                    .Where     (value => !string.IsNullOrEmpty(value))
                    .ToArray();

            if ( Regex.Match(query, @"<\s*ROW\s*\(([^)]*)\)", regexOptions).Groups is { Count: > 0 } rowGroup && rowGroup[1].Value is { Length: > 0 } rowGroupString )
                rowDimensionMembers = csvParser.ReadFromString(csvReaderOptions, rowGroupString)
                    .SelectMany(row   => row.Result)
                    .Select    (value => value.Trim())
                    .Where     (value => !string.IsNullOrEmpty(value))
                    .ToArray();

            // Build the report metadata.
            var metadata = new EssQueryReport.ReportMetadata()
            {
                PageDimensionMembers   = new List<string>(pageDimensionMembers),
                ColumnDimensionMembers = new List<string>(columnDimensionMembers),
                RowDimensionMembers    = new List<string>(rowDimensionMembers)
            };

            // If there is no raw report, return an EssQueryReport without data.
            if ( string.IsNullOrEmpty(rawReport) )
                return new EssQueryReport
                {
                    Metadata = metadata,
                    Data     = new object[0, 0]
                };

            // Note: At this point, we do not utilize the query preferences when building the report.
            //preferences ??= new EssQueryPreferences();

            // Set up the CSV parser to process the raw report string.
            csvParserOptions = new CsvParserOptions(false, new StringSplitTokenizer(new []{ '\t' }, false));
            csvReaderOptions = new CsvReaderOptions(new [] { Environment.NewLine, "\n" });
            csvParser        = new CsvParser<string[]>(csvParserOptions, new CsvStringArrayMapping());

            int reportRowCount = 0;
            int reportColCount = 0;

            // Parse the raw report as a tab-delimited csv and suffer multiple enumerations to capture
            // the row and column counts. If either the row or column count is 0, return an empty data array.
            if ( csvParser.ReadFromString(csvReaderOptions, rawReport) is not {} parseResults || (reportRowCount = parseResults.Count()) is 0 || (reportColCount = parseResults.FirstOrDefault()?.Result?.Length ?? 0) is 0 )
                return new EssQueryReport
                {
                    Metadata = metadata,
                    Data     = new object[reportColCount, reportRowCount]
                };

            // Declare and initialize a report of the appropriate size.
            object[,] report = new object[reportRowCount, reportColCount];

            // Fill the report.
            foreach ( var row in parseResults )
                for ( int ci = 0; ci < reportColCount; ci++ )
                    report[row.RowIndex, ci] = row.Result[ci];

            // Return a new EssQueryReport with the data.
            return new EssQueryReport
            {
                Metadata = metadata,
                Data     = report
            };
        }

        #endregion
    }
}
