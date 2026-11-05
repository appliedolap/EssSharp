using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents an MDX script that is specific to a cube.
    /// </summary>
    public class EssCalcScript : EssScript, IEssCalcScript
    {
        #region Constructors

        /// <summary />
        internal EssCalcScript( Script script, EssCube cube ) : base(script, cube) { }

        #endregion

        #region IEssScript Members

        /// <inheritdoc />
        public override EssScriptType ScriptType => EssScriptType.Calc;

        #endregion

        #region IEssCalcScript Members

        /// <inheritdoc />
        public List<IEssRuntimeSubstitutionVariable> GetRuntimeSubstitutionVariables() => GetRuntimeSubstitutionVariablesAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<IEssRuntimeSubstitutionVariable>> GetRuntimeSubstitutionVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ScriptsApi>();

                // The server gives a good error message when the script doesn't exist.
                var list = await api.ScriptsGetRTSVsForScriptsAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, scriptName: Name, cancellationToken: cancellationToken).ConfigureAwait(false);

                return list?.Items?.Where(rtsv => rtsv is not null)
                                   .Select(rtsv => new EssRuntimeSubstitutionVariable(rtsv, this) as IEssRuntimeSubstitutionVariable)
                                   .ToList() ?? new List<IEssRuntimeSubstitutionVariable>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the runtime substitution variables of calc script {Name}. {e.Message}", e);
            }
        }

        #endregion
    }
}
