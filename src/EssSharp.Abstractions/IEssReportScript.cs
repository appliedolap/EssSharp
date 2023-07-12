using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssReportScript : IEssObject, IEssScript
    {
        #region Properties 

        /// <summary>
        /// The report generated from executing the script.
        /// </summary>
        public string Report { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Synchronously sets the Report property and returns the generated report from the job details.
        /// </summary>
        /// <returns></returns>
        public string Query();

        /// <summary>
        /// Asynchronously sets the Report property and returns the generated report from the job details.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<string> QueryAsync( CancellationToken cancellationToken = default );

        #endregion
    }
}
