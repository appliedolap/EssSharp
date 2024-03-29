﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
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
        /// <returns>An <see cref="EssGrid"/> object.</returns>
        public IEssGrid GetGrid( EssQueryPreferences preferences = null ) => GetGridAsync(preferences).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssGrid"/> object.</returns>
        public async Task<IEssGrid> GetGridAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default )
        {
            try
            {
                // Create new preferences, if necessary, and ensure cell types are captured.
                preferences ??= new EssQueryPreferences();
                preferences.CaptureCellTypes = true;

                // Get the report and build a grid with it.
                var report = await GetReportAsync(preferences: preferences, cancellationToken).ConfigureAwait(false);
                var operation = new GridOperation(alias: preferences.AliasTable, grid: report.ToModelGrid(), action: GridOperation.ActionEnum.Refresh);

                var api = GetApi<GridApi>();
                if ( await api.GridExecuteAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: operation, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception("Could not get a grid query result with the report.");

                return new EssGrid(grid, Cube as EssCube);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get a grid from {Cube.Application.Name}.{Cube.Name} with the ""{Name}"" report query. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssQueryReport" /> object.</returns>
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

                // Create new preferences, if necessary.
                preferences ??= new EssQueryPreferences();

                // If we need to capture primitive cell types, quote member names.
                if ( preferences.CaptureCellTypes )
                {
                    // To avoid issues with numeric member names/aliases, prepend the report spec with <QUOTEMBRNAMES, if necessary.
                    if (query.IndexOf("<QUOTEMBRNAMES", StringComparison.OrdinalIgnoreCase) is -1)
                        query = $"<QUOTEMBRNAMES\n{query}";
                }

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
                throw new Exception($@"Unable to get a report from {Cube.Application.Name}.{Cube.Name} with the ""{Name}"" report query. {e.Message}", e);
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

            // Note: At this point, we only utilize the query preferences to tell whether to capture cell types.
            preferences ??= new EssQueryPreferences();

            // Set up the CSV parser to process the raw report string.
            csvParserOptions = new CsvParserOptions(false, new StringSplitTokenizer(new []{ '\t' }, false));
            csvReaderOptions = new CsvReaderOptions(new [] { Environment.NewLine, "\n" });
            csvParser        = new CsvParser<string[]>(csvParserOptions, new CsvStringArrayMapping());

            int reportRowCount = 0;
            int reportColCount = 0;

            // Parse the raw report as a tab-delimited csv and suffer multiple enumerations to capture the row and column counts.
            // If either the row count or longest column count is 0, return an empty data array.
            if ( csvParser.ReadFromString(csvReaderOptions, rawReport) is not {} parseResults || 
               ( reportRowCount = parseResults.Count() ) is 0 ||
               ( reportColCount = parseResults.Aggregate((longest, current) => longest.Result.Length > current.Result.Length ? longest : current).Result.Length ) is 0 )
                return new EssQueryReport
                {
                    Metadata = metadata,
                    Data     = new object[reportColCount, reportRowCount]
                };

            // Declare and initialize report and types arrays of the appropriate size.
            object[,] report = new object[reportRowCount, reportColCount];
            int   [,] types  = new int   [reportRowCount, reportColCount];

            // Capture primitive cell types if requested.
            if ( preferences.CaptureCellTypes )
            {
                string trimmed;

                // Fill the report and cell types in the least performant way possible.
                foreach ( var row in parseResults )
                {
                    for ( int ci = 0; ci < reportColCount; ci++ )
                    {
                        // Capture the cell value.
                        string value = ci < row.Result.Length ? row.Result[ci] : string.Empty;

                        // Trim quoted member names and identify any quoted cell value as a member.
                        report[row.RowIndex, ci] = trimmed = value.Trim().Trim('"');
                        types [row.RowIndex, ci] = !string.Equals(value.Trim(), trimmed, StringComparison.Ordinal) ? 0 : 7;
                    }
                }
            }
            else
            {
                // Fill the report.
                foreach ( var row in parseResults )
                    for ( int ci = 0; ci < reportColCount; ci++ )
                        report[row.RowIndex, ci] = ci < row.Result.Length ? row.Result[ci] : string.Empty;
            }

            // Return a new EssQueryReport with the data.
            return new EssQueryReport
            {
                Metadata = metadata,
                Data     = report,
                Types    = types
            };
        }

        #endregion
    }
}
