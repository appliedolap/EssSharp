using System;
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
    /// Represents a Report script that is specific to a cube.
    /// </summary>
    public class EssReportScript : EssScript, IEssReportScript
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly Script  _script;

        #endregion

        #region Constructors

        /// <summary />
        internal EssReportScript( Script script, EssCube cube ) : base(script, cube)
        {
            _script = script ??
                throw new ArgumentNullException(nameof(script), $"An API model {nameof(script)} is required to create an {nameof(EssScript)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An API model {nameof(cube)} is required to create an {nameof(EssScript)}.");
        }

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
        public string Query() => QueryAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<string> QueryAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var job = await ExecuteAsync().ThrowIfFailed();
                job.JobOutputInfo.TryGetValue("REPORT_OUTPUT", out var report);

                Report = report?.ToString() ?? throw new Exception("Cannot get report.");

                return Report;
            }
            catch (OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get Report from {Name}. {e.Message}", e);
            }
        }

        #endregion
    }
}
