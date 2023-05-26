using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssJob : EssObject, IEssJob
    {
        #region Private Data

        private readonly EssServer      _server;
        private readonly IEssJobOptions _options;

        private JobRecordBean _job;

        #endregion

        #region Constructors

        internal EssJob( IEssJobOptions options, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            if ( options is not EssJobOptions jobOptions )
                throw new ArgumentException($@"A specific type of {nameof(EssJobOptions)} is required to create an {nameof(EssJob)}.", nameof(options));

            _options = options;

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssJob)}.");

            _job = new JobRecordBean()
            {
                AppName    = jobOptions.ApplicationName,
                DbName     = jobOptions.CubeName,
                JobID      = -1,
                JobType    = jobOptions.JobType.ToModelEnum().ToMemberValue(),
                StatusCode = (int)EssJobStatus.New
            };
        }

        /// <summary />
        internal EssJob( JobRecordBean job, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _job = job ?? 
                throw new ArgumentNullException(nameof(job), $"An API model {nameof(job)} is required to create an {nameof(EssJob)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssJob)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => _job?.JobID.ToString();

        /// <inheritdoc />
        public override EssType Type => EssType.Job;

        #endregion

        #region IEssJob Properties

        /// <inheritdoc />
        public string ErrorMessage => JobOutputInfo.TryGetValue("errorMessage", out var value) ? value?.ToString()?.Trim() ?? string.Empty : string.Empty;

        /// <inheritdoc />
        public string InfoMessage => JobOutputInfo.TryGetValue("infoMessage", out var value) ? value?.ToString()?.Trim() ?? string.Empty : string.Empty;

        /// <inheritdoc />
        public long JobID => _job.JobID;

        /// <inheritdoc />
        public Dictionary<string, object> JobOutputInfo => _job.JobOutputInfo ??= new Dictionary<string, object>();

        /// <inheritdoc />
        public EssJobStatus JobStatus => _job.StatusCode switch
        {
             -1 => EssJobStatus.New,
            100 => EssJobStatus.InProgress,
            200 => EssJobStatus.Completed,
            300 => EssJobStatus.CompletedWithWarnings,
            400 => EssJobStatus.Failed,
              _ => EssJobStatus.Unknown
        };

        /// <inheritdoc />
        public EssJobType JobType => Extensions.ToValueFromDescription<EssJobType>(_job.JobType);

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string StatusMessage => JobStatus is EssJobStatus.New ? EssJobStatus.New.ToString() : _job.StatusMessage?.Trim() ?? string.Empty;

        #endregion

        #region IEssJob Methods

        /// <inheritdoc />
        /// <returns>An <see cref="EssJob" /> object.</returns>
        public IEssJob Execute() => ExecuteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssJob" /> object.</returns>
        public async Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default )
        {
            // If the backing bean has a JobID, then it has already been executed. Attempt to re-run the job.
            if ( _job.JobID >= 0 && await ReRunAsync(cancellationToken).ConfigureAwait(false) is EssJob reRunJob )
            {
                // Update the backing job and return this (now updated) job.
                _job = reRunJob._job;
                return this;
            }

            try
            {
                if ( _options is not EssJobOptions jobOptions )
                    throw new ArgumentException($@"A specific type of {nameof(EssJobOptions)} is required to execute an unstarted {nameof(EssJob)}.", "options");

                // Build a jobs input bean with our options.
                var inputBean = new JobsInputBean(jobOptions.ApplicationName, jobOptions.CubeName, jobOptions.JobType.ToModelEnum(), _options.ToModelBean());
                var api = GetApi<JobsApi>();

                // Attempt to execute the job.
                if ( await api.JobsExecuteJobAsync(body: inputBean, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } job )
                    throw new Exception($@"Failed to execute job.");

                // Wait for the job to complete and capture the job info.
                if ( await WaitForJobAsync(api, job.JobID, cancellationToken).ConfigureAwait(false) is not { } jobInfo )
                    throw new Exception("Failed to retrieve job information.");

                // Update the backing job and return this (now updated) job.
                _job = jobInfo;
                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to execute the {JobType.ToDescription().ToLowerInvariant()} job. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssJob" /> object.</returns>
        public IEssJob ReRun() => ReRunAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssJob" /> object.</returns>
        public async Task<IEssJob> ReRunAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                if ( JobStatus is EssJobStatus.New || _job.JobID < 0 )
                    throw new ArgumentException($"An unstarted {nameof(EssJob)} cannot be re-run.");

                var api = GetApi<JobsApi>();

                // Attempt to re-run the existing job by its ID.
                if ( await api.JobsExecuteByJobIdAsync(id: _job.JobID, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } job )
                    throw new Exception($@"Failed to execute job.");

                // Wait for the job to complete and capture the job info.
                if ( await WaitForJobAsync(api, job.JobID, cancellationToken).ConfigureAwait(false) is not { } jobInfo )
                    throw new Exception("Failed to retrieve job information.");

                // Return the re-run job.
                return new EssJob(jobInfo, _server);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to re-run the {JobType.ToDescription().ToLowerInvariant()} job. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssJob" /> object.</returns>
        public IEssJob ThrowIfFailed()
        {
            // Throw an exception if the job failed or has an error message.
            if ( JobStatus is EssJobStatus.Failed || !string.IsNullOrEmpty(ErrorMessage) )
                throw new Exception($@"Unable to successfully execute {JobType.ToDescription().ToLowerInvariant()} job. {ErrorMessage}".TrimEnd());

            // Otherwise, return the job.
            return this;
        }

        #endregion

        #region Private Methods

        private async Task<JobRecordBean> WaitForJobAsync( JobsApi jobsApi, long jobId, CancellationToken cancellationToken = default )
        {
            // Get the JobsAPI if necessary.
            jobsApi ??= GetApi<JobsApi>();

            // Attempt to get the job info.
            if ( await jobsApi.JobsGetJobInfoAsync(id: jobId.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } jobInfo )
                throw new Exception("Failed to retrieve job information.");

            // Wait for the job to complete by polling the job info.
            while ( jobInfo.StatusCode is 100 )
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                jobInfo = await jobsApi.JobsGetJobInfoAsync(id: jobId.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            // Return the completed (or failed) job.
            return jobInfo;
        }

        #endregion
    }
}
