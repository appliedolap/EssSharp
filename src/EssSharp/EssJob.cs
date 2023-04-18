using System;
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

        private readonly EssServer     _server;
        private readonly JobRecordBean _job;

        #endregion

        #region Constructors

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
        public long JobID => _job.JobID;

        /// <inheritdoc />
        public EssJobStatus JobStatus => _job.StatusCode switch
        {
            100 => EssJobStatus.InProgress,
            200 => EssJobStatus.Completed,
            300 => EssJobStatus.CompletedWithWarnings,
            400 => EssJobStatus.Failed,
            _   => EssJobStatus.Unknown
        };

        /// <inheritdoc />
        public EssJobType JobType => Extensions.ToValueFromDescription<EssJobType>(_job.JobType);

        /// <inheritdoc />
        public IEssServer Server => _server;

        #endregion

        #region IEssJob Methods

        /// <inheritdoc />
        public long ReRun() => ReRunAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<long> ReRunAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<JobsApi>();
                if ( await api.JobsExecuteByJobIdAsync(_job.JobID, 0, cancellationToken).ConfigureAwait(false) is { } job )
                    return job.JobID;

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
