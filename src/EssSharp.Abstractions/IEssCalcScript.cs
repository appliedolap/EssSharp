using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssCalcScript : IEssScript
    {
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
    }
}
