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
        /// <returns></returns>
        public IEssGrid ZoomIn( List<List<int>> ranges );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEssGrid> ZoomInAsync( List<List<int>> ranges, CancellationToken cancellationToken = default );

        /// <summary>
        /// Zooms in or out of grid and returns it.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="ranges"></param>
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<List<int>> ranges );

        /// <summary>
        /// Zooms in or out of grid and returns it.
        /// </summary>
        /// <param name="zoomOption"></param>
        /// <param name="ranges"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, List<List<int>> ranges, CancellationToken cancellationToken = default );
    }
}
