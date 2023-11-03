using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;
using RestSharp;
using static System.Collections.Specialized.BitVector32;


namespace EssSharp
{
    /// <summary />
    public class EssGrid : EssObject, IEssGrid
    {
        #region Private Data

        private readonly EssCube _cube;
        private Grid _grid;
        private string _essGridAlias = "";
        private List<EssGridDimension> _essGridDimension = new List<EssGridDimension>();
        private EssGridSlice _essGridSlice = new EssGridSlice();

        #endregion

        #region Constructors

        /// <summary />
        internal EssGrid( EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

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
        public override string Name => $@"{_cube.Application.Name}.{_cube.Name}";

        /// <inheritdoc />
        public override EssType Type => EssType.Grid;

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public string Alias //=> _grid.Alias;
        {
            get
            {
                if ( _grid?.Alias is { } alias )
                    return alias;

                return _essGridAlias;
            }
            set
            {
                if ( _grid is not null )
                    _grid.Alias = value;
                else 
                    _essGridAlias = value;
            }
        }
        /// <inheritdoc />
        public List<EssGridDimension> Dimensions //=> _grid.Dimensions.ToEssGridDimension();
        {
            get
            {
                if ( _grid?.Dimensions?.ToEssGridDimension() is { } dimensions )
                    return dimensions;

                return _essGridDimension;
            }
            set
            {
                if ( _grid is not null )
                    _grid.Dimensions = value?.ToModelBean();
                else
                    _essGridDimension = value;
            }
        } 

        /// <inheritdoc />
        public List<EssGridSelection> Selection { get; set; } = new List<EssGridSelection>();

        /// <inheritdoc />
        public EssGridSlice Slice //=> _grid?.Slice?.ToEssGridSlice() ?? new EssGridSlice();
        {
            get
            {
                if ( _grid?.Slice?.ToEssGridSlice() is { } slice )
                    return slice;

                return _essGridSlice;
            }
            set
            {
                if ( _grid is not null )
                    _grid.Slice = value?.ToModelBean();
                else
                    _essGridSlice = value;
            }
        }

        /// <inheritdoc />
        public EssGridPreferences Preferences { get; set; }

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        public IEssLayout GetGridLayout() => GetGridLayoutAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<IEssLayout> GetGridLayoutAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GridApi>();

                if ( await api.GridGetLayoutGridAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: _grid, cancellationToken: cancellationToken) is not { } layout )
                    throw new Exception("Cannot get grid layout.");

                return new EssLayout(layout, _cube);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get grid layout for ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public EssGridPreferences GetGridPreferences() => GetGridPreferencesAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssGridPreferences> GetGridPreferencesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GridPreferencesApi>();

                if ( await api.GridPreferencesGetAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { } preferences )
                    throw new Exception("Cannot get grid preferences.");

                Preferences = preferences.ToEssGridPreferences();

                return Preferences;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get grid preferences for ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid KeepOnly() => KeepOnlyAsync(Selection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid KeepOnly( EssGridSelection gridSelection ) => KeepOnlyAsync( gridSelection ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid KeepOnly( List<EssGridSelection> gridSelection ) => KeepOnlyAsync(gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> KeepOnlyAsync( CancellationToken cancellationToken = default ) => KeepOnlyAsync(Selection, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> KeepOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => KeepOnlyAsync(new List<EssGridSelection>() { gridSelection }, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> KeepOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperationAsync(action:GridOperation.ActionEnum.KeepOnly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to keep only specified coordinates of ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Pivot() => PivotAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Pivot( EssGridSelection currentPosition = null, EssGridSelection newPosition = null ) => PivotAsync(currentPosition, newPosition).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> PivotAsync( EssGridSelection currentPosition = null, EssGridSelection newPosition = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( currentPosition is null )
                    currentPosition = Selection[0] ??
                        throw new ArgumentException(nameof(currentPosition), $"An {nameof(EssGridSelection)} object, or setting the {nameof(EssGrid.Selection)} property is required to perform pivot on grid.");

                if ( newPosition is null && Selection.ElementAtOrDefault(1) is not null )
                    newPosition = Selection[1];

                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Pivot, gridSelection: new List<EssGridSelection>() { currentPosition }, newPosition: newPosition, cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to pivot to pov on ""{Name}"" grid. {e.Message}", e);
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
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Refresh, cancellationToken: cancellationToken).ConfigureAwait( false );
                
                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to refresh ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid RemoveOnly( ) => RemoveOnlyAsync(Selection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid RemoveOnly( EssGridSelection gridSelection ) => RemoveOnlyAsync(gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid RemoveOnly( List<EssGridSelection> gridSelection ) => RemoveOnlyAsync(gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> RemoveOnlyAsync( CancellationToken cancellationToken = default ) => RemoveOnlyAsync(Selection, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => RemoveOnlyAsync(new List<EssGridSelection>() { gridSelection }, cancellationToken );

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.RemoveOnly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception e )
            {
                throw new Exception($@"Unable to refresh ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void SetGridPreferences( EssGridPreferences gridPreferences = null ) => SetGridPreferencesAsync( gridPreferences ).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task SetGridPreferencesAsync( EssGridPreferences gridPreferences = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( gridPreferences == null )
                {
                    if ( Preferences is null )
                        await GetGridPreferencesAsync();
                    gridPreferences = Preferences;
                }

                var api = GetApi<GridPreferencesApi>();
                
                await api.GridPreferencesSetAsync(body: gridPreferences.ToPreferencesObject(), cancellationToken: cancellationToken);

                await GetGridPreferencesAsync();

            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to set grid preferences for ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Submit( ) => SubmitAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> SubmitAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                if (_grid is not null)
                    _grid.Slice.DirtyCells = new List<int>();
                else 
                    _essGridSlice.DirtyCells ??= new List<int>();

                for ( int i = 0; i < Slice.Data.Ranges[0].Types.Count; i++ )
                {
                    if ( Slice.Data.Ranges[0].Types[i] == "2" )
                        if ( _grid is not null )
                            _grid.Slice.DirtyCells.Add(i);
                        else
                            _essGridSlice.DirtyCells.Add(i);
                }

                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Submit, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to submit new value for ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Zoom( EssGridZoomType zoomOption ) => ZoomAsync(zoomOption, Selection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Zoom( EssGridZoomType zoomOption, EssGridSelection gridSelection ) => ZoomAsync(zoomOption, gridSelection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection ) => ZoomAsync(zoomOption, gridSelection ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, CancellationToken cancellationToken = default ) => ZoomAsync(zoomOption, Selection, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => ZoomAsync(zoomOption, new List<EssGridSelection>() { gridSelection }, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> ZoomAsync( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperationAsync(action: zoomOption.ToEssGridActionType(), gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to refresh ""{Name}"" grid. {e.Message}", e);
            }
        }
        #endregion

        #region Private Methods

        private async Task ExecuteGridOperationAsync( GridOperation.ActionEnum action, List<EssGridSelection> gridSelection = null, EssGridSelection newPosition = null, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GridApi>();

                // Clear/process the grid selection, depending on the operation.
                gridSelection = action switch
                {
                    // Refresh and submit don't use a grid selection.
                    GridOperation.ActionEnum.Refresh => null,
                    GridOperation.ActionEnum.Submit  => null,
                    // Other operations do; use the given selection, fall back to the current selection, or throw.
                    _ => gridSelection ??= Selection ??
                        throw new ArgumentNullException(nameof(gridSelection), $"At least one {nameof(EssGridSelection)} or a current {nameof(EssGrid)}.{nameof(EssGrid.Selection)} is required to perform {action} on grid.")
                };

                var body = GetGridOperation(action, gridSelection, newPosition);

                if ( await api.GridExecuteAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception($@"Could not get a grid via the {action} operation.");

                _grid = grid;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to perform grid operation {action} on ""{Name}"" grid. {e.Message}", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="gridSelection"></param>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        private GridOperation GetGridOperation( GridOperation.ActionEnum action, List<EssGridSelection> gridSelection = null, EssGridSelection newPosition = null )
        {
            try
            {
                var ranges = default(List<List<int>>);
                var coordinates = default(List<int>);

                switch ( action )
                {
                    case GridOperation.ActionEnum.Pivot:
                    case GridOperation.ActionEnum.PivotToPOV:
                        coordinates = new List<int>() { GetCoordinate(gridSelection: gridSelection[0], columnCount: Slice.Columns) };
                        if ( newPosition is not null )
                            coordinates.Add(GetCoordinate(gridSelection: newPosition, columnCount: Slice.Columns));
                        break;
                    case GridOperation.ActionEnum.ZoomIn:
                    case GridOperation.ActionEnum.ZoomOut:
                    case GridOperation.ActionEnum.KeepOnly:
                    case GridOperation.ActionEnum.RemoveOnly:
                        ranges = new List<List<int>>();
                        gridSelection?.ForEach(selection => ranges.Add(new List<int>() { selection.startRow, selection.startColumn, selection.rowCount, selection.columnCount }));
                        break;
                    case GridOperation.ActionEnum.Refresh:
                    case GridOperation.ActionEnum.Submit:
                    default:
                        ranges = null;
                        coordinates = null;
                        break;
                }

                return new GridOperation()
                        {
                            Grid = _grid ?? new Grid() 
                            {
                                Alias = this.Alias,
                                Dimensions = this.Dimensions.ToModelBean(),
                                Slice = this.Slice.ToModelBean(),  
                            },
                            Action = action,
                            Alias = this.Alias,
                            Ranges = ranges,
                            Coordinates = coordinates
                        };
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Cannot create {nameof(GridOperation)} response body for ""{Name}"" grid.", e);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSelection"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        private int GetCoordinate(EssGridSelection gridSelection, int columnCount ) =>
            (gridSelection.startRow * columnCount) + gridSelection.startColumn;

        #endregion
    }
}
