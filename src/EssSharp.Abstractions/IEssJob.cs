using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssJob : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        /// <returns>The server that contains this job.</returns>
        IEssServer Server { get; }

        /// <summary>
        /// Returns the ID of this job.
        /// </summary>
        long JobID { get; }

        /// <summary>
        /// Returns the status of this job.
        /// </summary>
        EssJobStatus JobStatus { get; }

        /// <summary>
        /// Returns the type of this job.
        /// </summary>
        EssJobType JobType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Reruns the job, returning the new job ID.
        /// </summary>
        long ReRun();

        /// <summary>
        /// Asynchronously reruns the job, returning the new job ID.
        /// </summary>
        Task<long> ReRunAsync( CancellationToken cancellationToken = default );

        #endregion
    }
}
