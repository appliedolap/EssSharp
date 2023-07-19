using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary>
    /// An Essbase report script.
    /// </summary>
    public interface IEssReportScript : IEssScript
    {
        #region Properties 

        /// <summary>
        /// The report generated from executing the script.
        /// </summary>
        public string Report { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a report query and returns a report with the result.
        /// </summary>
        /// <param name="preferences">(optional) The preferences that configure the returned report.</param>
        public EssQueryReport GetReport( EssQueryPreferences preferences = null );

        /// <summary>
        /// Asynchronously executes a report query and returns a report with the result.
        /// </summary>
        /// <param name="preferences">(optional) The preferences that configure the returned report.</param>
        /// <param name="cancellationToken" />
        public Task<EssQueryReport> GetReportAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        #endregion
    }
}
