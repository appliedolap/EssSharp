using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssGrid : IEssObject
    {

        /// <summary>
        /// Gets cube.
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets grid dimension.
        /// </summary>
        public List<EssGridDimension> Dimensions { get; set; }

        /// <summary>
        /// Gets grid slice.
        /// </summary>
        public EssGridSlice Slice { get; set; }

        /// <summary>
        /// Current selected cell(s)
        /// </summary>
        public List<EssGridSelection> Selection { get; set; }

        /// <summary>
        /// Current grid preferences.
        /// </summary>
        public EssGridPreferences Preferences { get; set; }

        /// <summary>
        /// True if aliases should be used.
        /// </summary>
        public bool UseAliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseMissingText { get; set; }

        /// <summary>
        /// Gets grid layout.
        /// </summary>
        public IEssLayout GetGridLayout();

        /// <summary>
        /// Asychronously gets grid preferneces.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssLayout> GetGridLayoutAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets grid preferences.
        /// </summary>
        public EssGridPreferences GetGridPreferences();

        /// <summary>
        /// Asynchronously gets grid preferences.
        /// </summary>
        public Task<EssGridPreferences> GetGridPreferencesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Keeps the dimension or dimensions at current selected cell or cells and removes all else.
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid KeepOnly();

        /// <summary>
        /// Keeps the dimension at given cell and removes all else.
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid KeepOnly( EssGridSelection gridSelection );

        /// <summary>
        /// Keeps the dimension at given cells and removes all else.
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid KeepOnly( List<EssGridSelection> gridSelection );

        /// <summary>
        /// Asynchronously keeps the dimension or dimensions at current selected cell or cells and removes all else.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> KeepOnlyAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously keeps the dimension at given cell and removes all else.
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> KeepOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously keeps the dimension at given cells and removes all else.
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> KeepOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        public IEssGrid Pivot();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="newPosition"></param>
        public IEssGrid Pivot( EssGridSelection currentPosition = null, EssGridSelection newPosition = null );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="newPosition"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> PivotAsync( EssGridSelection currentPosition = null, EssGridSelection newPosition = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Refresh grid.
        /// </summary>
        public IEssGrid Refresh();

        /// <summary>
        /// Asynchronously refresh grid.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> RefreshAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Removes dimension or dimensions at current selected cell or cells.
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid RemoveOnly();

        /// <summary>
        /// Removes dimension at given cell.
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid RemoveOnly( EssGridSelection gridSelection );

        /// <summary>
        /// Removes dimension at given cells.
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid RemoveOnly( List<EssGridSelection> gridSelection );

        /// <summary>
        /// Asynchronously removes dimension or dimensions at current selected cell or cells.
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> RemoveOnlyAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously removes dimension at given cell.
        /// </summary>
        /// <param name="gridSelection"></param>
        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously removes dimension at given cells.
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// Sets the values of the grid preferences for this session.
        /// </summary>
        /// <param name="gridPreferences"></param>
        public void SetGridPreferences( EssGridPreferences gridPreferences = null );

        /// <summary>
        /// Asynchronously sets the values of the grid preferences for this session.
        /// </summary>
        /// <param name="gridPreferences"></param>
        /// <param name="cancellationToken"></param>
        public Task SetGridPreferencesAsync( EssGridPreferences gridPreferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Submits new value or values to the grid.
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="newValue"></param>
        public IEssGrid Submit();

        /// <summary>
        /// Asynchronously submits new value or values to the grid.
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="newValue"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> SubmitAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Zooms in or out with current selected cell or cells.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid Zoom( EssGridZoomType zoomOption );

        /// <summary>
        /// Zooms in or out with given cell.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public IEssGrid Zoom( EssGridZoomType zoomOption, EssGridSelection gridSelection );

        /// <summary>
        /// Zooms in or out with given list of cells.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection );

        /// <summary>
        /// Asynchronously zooms in or out with current selected cell or cells.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously zooms in or out with given cell.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously zooms in or out with given list of cells.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously refresh the grid.
        /// </summary>
        /// <param name="gridTask" />
        /// <param name="cancellationToken" />
        public static async Task<IEssGrid> RefreshAsync( this Task<IEssGrid> gridTask, CancellationToken cancellationToken = default ) =>
            await (await gridTask.ConfigureAwait(false)).RefreshAsync(cancellationToken).ConfigureAwait(false);
    }
}
