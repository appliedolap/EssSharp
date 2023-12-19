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
    /// <summary>
    /// Represents an MDX script that is specific to a cube.
    /// </summary>
    public class  EssMdxScript : EssScript, IEssMdxScript
    {
        #region Constructors

        /// <summary />
        internal EssMdxScript( Script script, EssCube cube ) : base(script, cube) { }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.MDX;

        #endregion

        #region IEssMdxScript Methods

        /// <inheritdoc />
        /// <returns>An <see cref="EssGrid"/> object.</returns>
        public IEssGrid GetGrid() => GetGridAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssGrid"/> object.</returns>
        public async Task<IEssGrid> GetGridAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                // If the script/query content has not been fetched or set by a consumer,
                // attempt to get it from the server.
                if ( string.IsNullOrEmpty(Content) )
                    await GetContentAsync(cancellationToken).ConfigureAwait(false);

                // If the script/query content is still empty, throw an exception.
                if ( string.IsNullOrEmpty(Content) )
                    throw new ArgumentException($@"Script content is required to get an {nameof(EssGrid)}.", nameof(Content));

                var mdxOperation = new MDXOperation(Content);

                var api = GetApi<GridApi>();
                if ( await api.GridExecuteMDXAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: mdxOperation, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception("Could not execute the query and/or get the resulting grid.");

                return new EssGrid(grid, Cube as EssCube);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get a grid from {Cube.Application.Name}.{Cube.Name} with the ""{Name}"" MDX query. {e.Message}", e);
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

                // If the script/query content is still empty, throw an exception.
                if ( string.IsNullOrEmpty(Content) )
                    throw new ArgumentException($@"Script content is required to get an {nameof(EssQueryReport)}.", nameof(Content));

                // Construct new preferences if none were given.
                preferences ??= new EssQueryPreferences();

                var mdxInput = new MDXInput(query: Content, preferences.ToNamedQueriesPreferences() );

                var api = GetApi<ExecuteMDXApi>();

                if ( await api.MDXExecuteMDXAsync(application: Cube.Application.Name, database: Cube.Name, body: mdxInput, cancellationToken: cancellationToken).ConfigureAwait(false) is not JObject response )
                    throw new Exception($@"Could not execute the query and/or get the resulting report.");

                return BuildQueryReport(response, preferences);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get a report from {Cube.Application.Name}.{Cube.Name} with the ""{Name}"" MDX query. {e.Message}", e);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds and returns a query report from a given MDX output set and preferences.
        /// </summary>
        private EssQueryReport BuildQueryReport( JObject response, EssQueryPreferences preferences = null )
        {
            if ( response["metadata"] is not { } metadata )
                throw new Exception("Unable to capture result metadata.");

            var pageDimensionMembers   = metadata["page"]  ?.ToObject<string[]>() ?? new string[0];
            var columnDimensionMembers = metadata["column"]?.ToObject<string[]>() ?? new string[0];
            var rowDimensionMembers    = metadata["row"]   ?.ToObject<string[]>() ?? new string[0];

            if ( response["data"] is not JArray source )
                throw new Exception("Unable to capture result data.");

            // Construct new preferences if none were given.
            preferences ??= new EssQueryPreferences();

            // Identify which axes to include.
            bool includePageAxis   = preferences.Axes.HasFlag(EssQueryReport.ReportAxes.Pages);
            bool includeColumnAxis = preferences.Axes.HasFlag(EssQueryReport.ReportAxes.Columns);

            var sourceFirstIndex = 0;
            var sourceFirstRow = source.FirstOrDefault()?.Value<JArray>();
            var sourceColumnCount = sourceFirstRow?.Count ?? 0;
            var sourceRowCount = source.Count;

            var reportFirstIndex = 0;
            var reportColumnCount = sourceColumnCount;
            var reportRowCount = sourceRowCount;

            object[,] report = null;

            // Identify the first column axis column index using the index of the first source row cell with a value.
            var firstColumnAxisIndex =
            sourceFirstRow?.TakeWhile(t => string.IsNullOrEmpty(t?.Value<string>()))?.Count() ?? 0;

            // If the page axis is included and there are page dimension members.
            if ( includePageAxis && pageDimensionMembers.Any() )
            {
                // Update the column and row counts.
                reportColumnCount = Math.Max(sourceColumnCount,
                    Math.Max(firstColumnAxisIndex, 1) + pageDimensionMembers.Length);
                reportRowCount += includeColumnAxis ? 1 : 0;

                // Create the report.
                report = new object[reportRowCount, reportColumnCount];

                // Use the the first column axis member index as the first page column index.
                var pageColumnIndex = Math.Max(firstColumnAxisIndex, rowDimensionMembers.Any() ? 1 : 0);

                // Write the page axis dimension members.
                foreach ( var pageDimensionMember in pageDimensionMembers )
                    report[0, pageColumnIndex++] = pageDimensionMember;

                // Update the first index of the report.
                reportFirstIndex = 1;

                // Return the report if there are no source rows to add.
                if ( reportRowCount is 1 )
                {
                    return new EssQueryReport
                    {
                        Metadata = new EssQueryReport.ReportMetadata()
                        {
                            PageDimensionMembers   = new List<string>(pageDimensionMembers),
                            ColumnDimensionMembers = new List<string>(columnDimensionMembers),
                            RowDimensionMembers    = new List<string>(rowDimensionMembers)
                        },
                        Data = report
                    };
                }
            }

            if ( !includeColumnAxis && columnDimensionMembers.Any() )
            {
                // If the page axis is not included, reduce the report row count by one.
                // Note: If the page axis is included, the exclusion of the column axis
                //       is already accounted for.
                if ( !includePageAxis )
                    reportRowCount -= 1;

                // Shift the index of the first source row down by one.
                sourceFirstIndex = 1;
            }

            // Return an empty report if necessary.
            if ( reportRowCount is 0 )
            {
                return new EssQueryReport
                {
                    Metadata = new EssQueryReport.ReportMetadata()
                    {
                        PageDimensionMembers   = new List<string>(pageDimensionMembers),
                        ColumnDimensionMembers = new List<string>(columnDimensionMembers),
                        RowDimensionMembers    = new List<string>(rowDimensionMembers)
                    },
                    Data = new object[0, 0]
                };
            }

            // Create the report if it has not been created yet.
            report ??= new object[reportRowCount, reportColumnCount];

            // Create an array to hold the sequence of source columns.
            var sourceColumnSequence = Enumerable.Range(0, sourceColumnCount).ToArray();

            // If dimension property columns and rows should be relocated, do so.
            if ( preferences.RelocateDimensionPropertyColumnsAndRows )
            {
                // Now, relocate dimension property columns such that they come before column axis member columns and data.
                int relocatedColumns = 0;
                for (int c = 0; c < sourceColumnCount; c++)
                {
                    // If the current index is less than the number of row dimension members, relocate the column
                    if (c < rowDimensionMembers.Length && firstColumnAxisIndex > 0)
                        sourceColumnSequence[c] = firstColumnAxisIndex - rowDimensionMembers.Length + relocatedColumns++;
                    else if (relocatedColumns > 0 && c <= firstColumnAxisIndex - relocatedColumns)
                        sourceColumnSequence[c] = c - relocatedColumns;
                    else
                        sourceColumnSequence[c] = c;
                }
            }

            for ( int c = 0; c < sourceColumnCount; c++ )
            {
                // Get the report column index for the current source column index.
                var rc = sourceColumnSequence[c];

                // Iterate over rows, capturing each cell value as a string.
                for ( int ri = reportFirstIndex, si = sourceFirstIndex; si < sourceRowCount; ri++, si++ )
                    report[ri, rc] = source[si][c].Value<string>();
            }

            return new EssQueryReport
            {
                Metadata = new EssQueryReport.ReportMetadata()
                {
                    PageDimensionMembers   = new List<string>(pageDimensionMembers),
                    ColumnDimensionMembers = new List<string>(columnDimensionMembers),
                    RowDimensionMembers    = new List<string>(rowDimensionMembers)
                },
                Data = report
            };
        }

        #endregion
    }
}
