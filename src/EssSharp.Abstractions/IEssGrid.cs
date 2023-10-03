using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

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
        public string Alias { get; }

        /// <summary>
        /// Gets grid dimension.
        /// </summary>
        public List<EssGridDimension> Dimensions { get; }

        /// <summary>
        /// Gets grid slice.
        /// </summary>
        public EssGridSlice Slice { get; }

        /// <summary>
        /// Current selected cell(s)
        /// </summary>
        public List<EssGridSelection> Selection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EssGridPreferences Preferences { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void GetGridPreferences();

        /// <summary>
        /// 
        /// </summary>
        public Task GetGridPreferencesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid KeepOnly();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid KeepOnly( EssGridSelection gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid KeepOnly( List<EssGridSelection> gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> KeepOnlyAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> KeepOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
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
        /// Refresh grid.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> RefreshAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid RemoveOnly();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid RemoveOnly( EssGridSelection gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        public IEssGrid RemoveOnly( List<EssGridSelection> gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> RemoveOnlyAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );

        public void SetGridPreferences( EssGridPreferences gridPreferences = null );

        /// <inheritdoc />
        public Task SetGridPreferencesAsync( EssGridPreferences gridPreferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="newValue"></param>
        public IEssGrid SubmitNewValue();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="newValue"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> SubmitNewValueAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public IEssGrid Zoom( EssGridZoomType zoomOption );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public IEssGrid Zoom( EssGridZoomType zoomOption, EssGridSelection gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <remarks>Requires the EssGrid.Selection attribute to be set.</remarks>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );
    }
}
