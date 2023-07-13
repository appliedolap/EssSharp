using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary>
    /// An Essbase MDX script.
    /// </summary>
    public interface IEssMdxScript : IEssScript
    {
        #region Methods

        /// <summary>
        /// Executes an MDX query and returns a grid with the result.
        /// </summary>
        public IEssGrid GetGrid();

        /// <summary>
        /// Asynchronously executes an MDX query and returns a grid with the result.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<IEssGrid> GetGridAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes an MDX query and returns a report with the result.
        /// </summary>
        /// <param name="preferences">(optional) The preferences that configure the returned report.</param>
        public EssQueryReport GetReport( EssQueryPreferences preferences = null );

        /// <summary>
        /// Asynchronously executes an MDX query and returns a report with the result.
        /// </summary>
        /// <param name="preferences">(optional) The preferences that configure the returned report.</param>
        /// <param name="cancellationToken" />
        public Task<EssQueryReport> GetReportAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        #endregion
    }
}
