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
        /// <param name="gridSelection"></param>
        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default );

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
