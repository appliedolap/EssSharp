using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssCalcScript : IEssScript
    {
        #region Methods

        /// <summary>
        /// Returns the runtime substitution variables this calc script declares, or an empty list when it declares none.
        /// </summary>
        public List<IEssRuntimeSubstitutionVariable> GetRuntimeSubstitutionVariables();

        /// <summary>
        /// Asynchronously returns the runtime substitution variables this calc script declares, or an empty list when it
        /// declares none.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssRuntimeSubstitutionVariable>> GetRuntimeSubstitutionVariablesAsync( CancellationToken cancellationToken = default );

        #endregion
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously executes (or re-runs) this job, updating its status and returning the updated job.
        /// </summary>
        /// <param name="scriptTask" />
        /// <param name="cancellationToken" />
        public static async Task<IEssJob> ExecuteAsync( this Task<IEssCalcScript> scriptTask, CancellationToken cancellationToken = default ) =>
            await (await scriptTask.ConfigureAwait(false)).ExecuteAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously returns the runtime substitution variables this calc script declares, or an empty list when it
        /// declares none.
        /// </summary>
        /// <param name="scriptTask" />
        /// <param name="cancellationToken" />
        public static async Task<List<IEssRuntimeSubstitutionVariable>> GetRuntimeSubstitutionVariablesAsync( this Task<IEssCalcScript> scriptTask, CancellationToken cancellationToken = default ) =>
            await (await scriptTask.ConfigureAwait(false)).GetRuntimeSubstitutionVariablesAsync(cancellationToken).ConfigureAwait(false);
    }
}
