using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssJob : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the error message of this job (if available).
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Returns the info message of this job (if available).
        /// </summary>
        string InfoMessage { get; }

        /// <summary>
        /// Returns the ID of this job.
        /// </summary>
        long JobID { get; }

        /// <summary>
        /// Returns the output information dictionary of this job (if available).
        /// </summary>
        Dictionary<string, object> JobOutputInfo { get; }

        /// <summary>
        /// Returns the status of this job.
        /// </summary>
        EssJobStatus JobStatus { get; }

        /// <summary>
        /// Returns the type of this job.
        /// </summary>
        EssJobType JobType { get; }

        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        IEssServer Server { get; }

        /// <summary>
        /// Returns the status message of this job (if available).
        /// </summary>
        string StatusMessage { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes (or re-runs) this job, updating its status and returning the updated job if the job completes successfully.
        /// </summary>
        IEssJob Execute();

        /// <summary>
        /// Asynchronously executes (or re-runs) this job, updating its status and returning the updated job if the job completes successfully.
        /// </summary>
        Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Re-runs an already executed job, returning the updated job if the job completes successfully.
        /// </summary>
        IEssJob ReRun();

        /// <summary>
        /// Asynchronously re-runs an already executed job, returning the updated job if the job completes successfully.
        /// </summary>
        Task<IEssJob> ReRunAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Throws an <see cref="System.Exception" /> containing any available error message if the job failed.
        /// </summary>
        /// <exception cref="System.Exception" />
        void ThrowIfFailed();

        #endregion
    }
}
