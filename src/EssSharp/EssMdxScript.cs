using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        #region Private Data

        private readonly EssCube _cube;
        private Script  _script;

        #endregion

        #region Constructors

        /// <summary />
        internal EssMdxScript( Script script, EssCube cube ) : base(script, cube)
        {
            _script = script ??
                throw new ArgumentNullException(nameof(script), $"An API model {nameof(script)} is required to create an {nameof(EssMdxScript)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An API model {nameof(cube)} is required to create an {nameof(EssMdxScript)}.");
        }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.MDX;

        #endregion

        #region IEssScript Methods

        /// <inheritdoc />
        /// <returns><see cref="EssQueryReport"/></returns>
        public EssQueryReport Query( EssQueryPreferences preferences = null ) => QueryAsync( preferences ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="EssQueryReport" /></returns>
        public async Task<EssQueryReport> QueryAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(Content) )
                    try { await GetContentAsync().ConfigureAwait(false); }
                    catch { throw new ArgumentException($@"{nameof(Content)} is required to execute MDX Query."); }

                preferences ??= new EssQueryPreferences();

                var body = new MDXInput( query: Content, preferences.ToNamedQueriesPreferences() );

                var api = GetApi<ExecuteMDXApi>();

                if ( await api.MDXExecuteMDXAsync(application: Cube.Application.Name, database: Cube.Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not JObject mdxResponse )
                    throw new Exception($@"Could not execute query: {Content}");

                //return an EssNamedQuery with this so you can save it?
                var report = new EssQueryReport
                {
                    Metadata = new EssQueryReport.ReportMetadata()
                    {
                        PageDimensionMembers = new List<string>(mdxResponse["metadata"]["page"]?.ToObject<string[]>() ?? new string[0]),
                        ColumnDimensionMembers = new List<string>(mdxResponse["metadata"]["column"]?.ToObject<string[]>() ?? new string[0]),
                        RowDimensionMembers = new List<string>(mdxResponse["metadata"]["row"]?.ToObject<string[]>() ?? new string[0])
                    },

                    //TODO: add mdxResponse["data"]
                    Data = new object[0,0]
                };

                return report;

            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to execute MDX query on cube ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns><see cref="IEssGrid"/></returns>
        public IEssGrid GetGrid() => GetGridAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns><see cref="IEssGrid"/></returns>
        public async Task<IEssGrid> GetGridAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(Content) ) 
                {
                    try { GetContent(); }
                    catch { throw new Exception($@"{nameof(Content)} is required to get a {nameof(EssGrid)}."); }
                }

                var body = new MDXOperation(Content);

                var api = GetApi<GridApi>();

                if ( await api.GridExecuteMDXAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception("Unable to get grid.");

                return new EssGrid(grid, _cube);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to execute MDX query on cube ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
