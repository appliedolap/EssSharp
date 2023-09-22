using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;


namespace EssSharp
{
    /// <summary />
    public class EssGrid : EssObject, IEssGrid
    {
        #region Private Data

        private readonly EssCube _cube;
        private Grid _grid;
         
        #endregion

        #region Constructors

        /// <summary />
        internal EssGrid( Grid grid, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _grid = grid ??
                throw new ArgumentNullException(nameof(grid), $"An API model {nameof(grid)} is required to create an {nameof(EssGrid)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region EssObject Properties

        /// <inheritdoc />
        public override string Name => _grid.Alias;

        /// <inheritdoc />
        public override EssType Type => EssType.Grid;

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public string Alias => _grid.Alias;

        /// <inheritdoc />
        public List<EssGridDimension> Dimensions => _grid.Dimensions.ToEssGridDimension();

        /// <inheritdoc />
        public EssGridSlice Slice => _grid.Slice.ToEssGridSlice();

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        /// <returns></returns>
        public IEssGrid KeepOnly( EssGridSelection gridSelection ) => KeepOnlyAsync( gridSelection ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public IEssGrid KeepOnly( List<EssGridSelection> gridSelection ) => KeepOnlyAsync(gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public Task<IEssGrid> KeepOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => KeepOnlyAsync(new List<EssGridSelection>() { gridSelection }, cancellationToken);

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<IEssGrid> KeepOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperations(action:GridOperation.ActionEnum.Keeponly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to keep only specified coordinates of grid ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Refresh() => RefreshAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> RefreshAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperations(action: GridOperation.ActionEnum.Refresh, cancellationToken: cancellationToken).ConfigureAwait( false );
                
                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to refresh grid ""{Name}"". {e.Message}", e);
            }
        }

        public IEssGrid RemoveOnly( EssGridSelection gridSelection ) => RemoveOnlyAsync(gridSelection).GetAwaiter().GetResult();

        public IEssGrid RemoveOnly( List<EssGridSelection> gridSelection ) => RemoveOnlyAsync(gridSelection).GetAwaiter().GetResult();

        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => RemoveOnlyAsync(new List<EssGridSelection>() { gridSelection }, cancellationToken );

        public async Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperations(action: GridOperation.ActionEnum.Removeonly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception e )
            {
                throw new Exception($@"Unable to refresh grid ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Zoom( EssGridZoomType zoomOption, EssGridSelection gridSelection ) => ZoomAsync(zoomOption, gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection ) => ZoomAsync(zoomOption, gridSelection ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => ZoomAsync(zoomOption, new List<EssGridSelection>() { gridSelection }, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperations(action: zoomOption.ToEssGridActionType(), gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to refresh grid ""{Name}"". {e.Message}", e);
            }
        }
        #endregion

        #region Private Methods

        private async Task ExecuteGridOperations( GridOperation.ActionEnum action, List<EssGridSelection> gridSelection = null, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GridApi>();

            if ( action != GridOperation.ActionEnum.Refresh && gridSelection is null )
                throw new ArgumentException(nameof(gridSelection), $"A list of cell coordinants is required to zoom into a grid.");

            var ranges = new List<List<int>>();

            gridSelection?.ForEach(selection => ranges.Add(new List<int>() { selection.startRow, selection.startColumn, selection.rowCount, selection.columnCount }));

            var body = new GridOperation()
            {
                Grid = this.ToModelBean(),
                Action = action,
                Alias = this.Alias,
                Ranges = ranges ?? null
            };

            if ( await api.GridExecuteAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                throw new Exception($@"Cannot zoom into grid ""{Name}"" at coordinants {ranges}.");

            _grid = grid;
        }

        #endregion
    }
}
