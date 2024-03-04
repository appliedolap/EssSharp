using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using EssSharp.Api;
using EssSharp.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Collections.Specialized.BitVector32;

using Action = EssSharp.Model.GridOperation.ActionEnum;

namespace EssSharp
{
    [Flags]
    internal enum Status
    {
        None        = 0x0,
        Missing     = 0x2000,    // 8,192
        Meaningless = 0x10000001 // 268,435,457

    };

    /// <summary />
    public class EssGrid : EssObject, IEssGrid
    {
        #region Private Data

        private readonly EssCube _cube;
        private Grid _grid;
        private string _essGridAlias = "";
        private List<EssGridDimension> _essGridDimension = new List<EssGridDimension>();
        private EssGridSlice _essGridSlice = new EssGridSlice();
        private List<string> _oldValues = new List<string>();
        private int _dataGridStartIndex;

        #endregion

        #region Constructors

        /// <summary />
        public EssGrid( EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        /// <summary />
        public EssGrid( Grid grid, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _grid = grid ??
                throw new ArgumentNullException(nameof(grid), $"An API model {nameof(grid)} is required to create an {nameof(EssGrid)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");

            _oldValues = _grid.Slice.Data.Ranges[0].Values.ToList();
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

                return _essGridAlias ??= "";
            }
            set
            {
                if ( _grid is not null )
                    _grid.Alias = value ?? "";
                else
                    _essGridAlias = value ?? "";
            }
        }

        /// <inheritdoc />
        public EssDataChanges DataChanges { get; set; } = null;

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
                {
                    _grid.Slice = value?.ToModelBean();
                }
                else
                {
                    _essGridSlice = value;
                }
            }
        }

        /// <inheritdoc />
        public bool UseAliases { get; set; } = true;

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
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid KeepOnly() => KeepOnlyAsync(Selection).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid KeepOnly( EssGridSelection gridSelection ) => KeepOnlyAsync(gridSelection).GetAwaiter().GetResult();

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
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Keeponly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

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
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Refresh, cancellationToken: cancellationToken).ConfigureAwait(false);

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
        public IEssGrid RemoveOnly() => RemoveOnlyAsync(Selection).GetAwaiter().GetResult();

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
        public Task<IEssGrid> RemoveOnlyAsync( EssGridSelection gridSelection, CancellationToken cancellationToken = default ) => RemoveOnlyAsync(new List<EssGridSelection>() { gridSelection }, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> RemoveOnlyAsync( List<EssGridSelection> gridSelection, CancellationToken cancellationToken = default )
        {
            try
            {
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Removeonly, gridSelection: gridSelection, cancellationToken: cancellationToken).ConfigureAwait(false);

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
        public IEssGrid Submit() => SubmitAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> SubmitAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                /*
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
                */

                //SetDirtyCells();

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
        public IEssGrid Zoom( EssGridZoomType zoomOption, List<EssGridSelection> gridSelection ) => ZoomAsync(zoomOption, gridSelection).GetAwaiter().GetResult();

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
                    GridOperation.ActionEnum.Submit => null,
                    // Other operations do; use the given selection, fall back to the current selection, or throw.
                    _ => gridSelection ??= Selection ??
                        throw new ArgumentNullException(nameof(gridSelection), $"At least one {nameof(EssGridSelection)} or a current {nameof(EssGrid)}.{nameof(EssGrid.Selection)} is required to perform {action} on grid.")
                };

                // Get the default grid preferences.
                Preferences ??= await Cube.Application.Server.GetDefaultGridPreferencesAsync();
                
                // Remove the data block for non-submit
                PrepareSliceForOperation(action);

                if ( action == GridOperation.ActionEnum.Submit )
                    SetDirtyCells();

                var body = GetGridOperation(action, gridSelection, newPosition);

                if ( await api.GridExecuteAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: body, preferences: Preferences, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
                    throw new Exception($@"Could not get a grid via the {action} operation.");

                // If MissingText/NoAccessText is non-empty...
                if ( !string.IsNullOrEmpty(Preferences.MissingText) || !string.IsNullOrEmpty(Preferences.NoAccessText) )
                {
                    if ( grid.Slice.Data.Ranges[0] is GridRange dataRange )
                    {
                        // iterate through entire list...
                        for ( var i = 0; i <= dataRange.End; i++ )
                        {
                            // looking for data cells that contain a null or empty value...
                            if ( string.Equals("2", dataRange.Types[i]) && string.IsNullOrEmpty(dataRange.Values[i]) )
                            {
                                var status = (Status)int.Parse(dataRange.Statuses[i]);

                                //replace the value with appropriate property from the Preferences object.
                                if ( (status & Status.Missing) == Status.Missing )
                                    grid.Slice.Data.Ranges[0].Values[i] = Preferences.MissingText;
                                else
                                    grid.Slice.Data.Ranges[0].Values[i] = Preferences.NoAccessText;
                            }
                        }
                    }
                }
                
                // Clear any prior data changes.
                DataChanges = null;

                // If tracking changes...
                if ( action == GridOperation.ActionEnum.Submit && Preferences.UseAuditLog )
                {
                    DataChanges = await CaptureDataChanges(grid, cancellationToken).ConfigureAwait(false);
                }

                _grid = grid;
                _oldValues = Slice.Data.Ranges[0].Values.ToList();
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
        /// <param name="newGrid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<EssDataChanges> CaptureDataChanges( Grid newGrid, CancellationToken cancellationToken )
        {
            var changes = new EssDataChanges();
            var valuesCount = Slice.Data.Ranges[0].End;
            var oldValues = _oldValues; //_grid.Slice.Data.Ranges[0].Values;
            var newValues = newGrid.Slice.Data.Ranges[0].Values;

            var dataBlockIndexes = GetDataBlock(newGrid);
            var dimMemberDict = new Dictionary<string, string>();

            // loop through each cell...
            for ( int index = 0; index < (valuesCount + 1); index++ )
            {
                // and determine if the value has changed.
                if ( !string.Equals(oldValues[index], newValues[index]) )
                {
                    // only hit cells within the data block.
                    if (dataBlockIndexes.Contains(index))//( dataBlockStartIndex <= index && dataBlockEndIndex >= index )
                    {
                        // Gets the dimension members for the cell that has changed.
                        // Returns a Dictionary<string, tuple<string, string>.
                        // The key is the dimension name
                        // tuple value 1 is the dimension members alias, if there is one.
                        // tuple value 2 is the dimension members name.
                        dimMemberDict = GetDimensionMembersForDataCell(index, newGrid);//await GetDimensionMembersForDataCell(rowIndex, dataBlockStartIndex, index, dimMemberDict, cancellationToken).ConfigureAwait(false);

                        // if the Dimensions property on the cube is empty, get the dimensions.
                        if ( Cube.Dimensions.Count == 0 )
                            await Cube.GetDimensionsAsync(cancellationToken);

                        // add new EssDataChange object to the EssDataChanges object.
                        changes.DataChanges.Add(new EssDataChange()
                        {
                            // the grid's original value 
                            OldValue = double.Parse(oldValues[index]),
                            // the grid's new value
                            NewValue = double.Parse(newValues[index]),
                            // Grid Dimension and Dimension member information
                            DataPoints = newGrid.Dimensions
                            .Select(gd => new EssDataPoint()
                            {
                                DimensionName = gd.Name,
                                DimensionNumber = Cube.Dimensions.FirstOrDefault(cd => string.Equals(cd.Name, gd.Name)).DimensionNumber,
                                // first value of the tuple returned from the GetDimensionMembersForDataCell() method, null if no value.
                                Alias = UseAliases ? string.Empty /*dimMemberDict[gd.Name].Item1*/ : null,
                                // second value of the tuple returned from the GetDimensionMembersForDataCell() method, null if no value.
                                Member = UseAliases ? dimMemberDict[gd.Name]/*dimMemberDict[gd.Name].Item2*/ : null
                            })
                            .ToList()
                        });
                    }
                }
            }
            return changes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataCellIndex"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetDimensionMembersForDataCell( int dataCellIndex, Grid grid)
        {
            Dictionary<string, string> memDict = new Dictionary<string, string>();
            int columnCount = grid.Slice.Columns;
            var dimensions = grid.Dimensions;
            var values = grid.Slice.Data.Ranges[0].Values;

            int currRowIndex = (dataCellIndex / columnCount) * columnCount;

            for ( int i = 0; i < (_dataGridStartIndex % columnCount); i++ )
            {
                var index = currRowIndex;
                while ( string.IsNullOrEmpty(values[index]) )
                    index -= columnCount;

                memDict[dimensions.FirstOrDefault(dim => dim.Column == (currRowIndex % columnCount)).Name] = values[index];
                currRowIndex++;
            }

            // Find the row index where the data block starts
            var dbStartRow = _dataGridStartIndex / columnCount;

            // and use them to find the index of the current cells dimension member
            var columnMemberIndex = dataCellIndex - (((dataCellIndex / columnCount) + 1 - dbStartRow) * Slice.Columns);
            var columnLimiter = (columnMemberIndex / columnCount + 1);

            for ( int i = 0; i < columnLimiter; i++ )
            {
                var index = columnMemberIndex;
                if ( string.IsNullOrEmpty(values[index]) )
                {
                    index = (columnMemberIndex / columnCount) * columnCount;
                    while ( string.IsNullOrEmpty(values[index]) )
                        index += 1;
                }
                memDict[dimensions.FirstOrDefault(dim => dim.Row == i).Name] = values[index];
                columnMemberIndex -= columnCount;
            }
            return memDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        private void PrepareSliceForOperation( Action action )
        {
            if ( Slice.Data.Ranges.FirstOrDefault() is not null )
            {
                string cellType;
                string cellValue;
                var cellValues = Slice?.Data?.Ranges[0]?.Values;
                bool isUpdate = action == GridOperation.ActionEnum.Submit;
                bool sendBlanksAsMissing = Preferences.SendBlanksAsMissing;
                var types = new List<string>();
                var values = new List<string>();
                var dataBlockIndexes = GetDataBlock(_grid);

                // Loop through entire grid and decide the type of each cell.
                for ( int index = 0; index < (Slice.Data.Ranges[0].End + 1); index++ )
                {
                    if ( dataBlockIndexes.Contains(index) )
                    {
                        if ( isUpdate )
                        {
                            cellValue = ( string.Equals(Preferences.MissingText, cellValues[index]) || string.Equals(Preferences.MissingText, cellValues[index]) ) ? string.Empty : cellValues[index];

                            // If string is empty...
                            if ( string.IsNullOrEmpty(cellValue) )
                                // and sendBlanksAsMissing is true, send type 2. If false, send type 7.
                                cellType = sendBlanksAsMissing ? "2" : "7";
                            else
                                // Else send type "2"
                                cellType = "2";
                        }
                        else
                        {
                            // Alwasys send empty string and type 7 if not an update
                            cellValue = string.Empty;
                            cellType = "7";
                        }

                    }
                    // If cell is not in the data block and is empty, send empty string and type 7.
                    else if ( string.IsNullOrEmpty(cellValues[index]) )
                    {
                        cellValue = string.Empty;
                        cellType = "7";
                    }
                    // Else, get cell value at index and set type to 0.
                    else
                    {
                        cellValue = cellValues[index];
                        cellType = "0";
                    }

                    // Add type and Value to arrays.
                    values.Add(cellValue);
                    types.Add(cellType);
                }

                // Update the slice's values and types.
                if ( _grid is not null )
                {
                    _grid.Slice.Data.Ranges[0].Values = values ?? new List<string>();
                    _grid.Slice.Data.Ranges[0].Types = types ?? new List<string>();
                }
                else
                {
                    Slice.Data.Ranges[0].Values = values ?? new List<string>();
                    Slice.Data.Ranges[0].Types = types ?? new List<string>();
                }
            }
            else
            {
                Slice = new EssGridSlice();
            }
        }

        private EssGridSelection GetDataBlockStartIndex( EssGridSlice slice )
        {
            List<string> cellValues = slice.Data.Ranges[0].Values;
            int firstRowIndex = 0;
            var dataGridFirstCell = new EssGridSelection(0, 0);
            
            // Find the row the data block starts
            for ( int index = 0; index < (slice?.Data?.Ranges[0]?.End + 1); index++ )
            {
                // Find the first non empty cell at the start of a row...
                if ( index % Slice.Columns == 0 && !string.IsNullOrEmpty(cellValues[index]) )
                {
                    // Retain the index...
                    firstRowIndex = index;
                    // And set the data grid start row.
                    dataGridFirstCell.startRow = index / Slice.Columns;
                    break;
                }
            }

            var columnIndex = firstRowIndex - Slice.Columns < 0 ? 0 : firstRowIndex - Slice.Columns;
            // Find the column the data block starts by moving 1 row above the start of the data grid.
            for ( var index = columnIndex; index < (Slice.Data.Ranges[0].End + 1); index++ )
            {
                // search the row for the first non empty cell
                if ( !string.IsNullOrEmpty(cellValues[index]) )
                {
                    // calculate the column index and set the value in the data grid start column.
                    dataGridFirstCell.startColumn = index % Slice.Columns;
                    break;
                }
            }

            return dataGridFirstCell;
        }

        private List<int> GetDataBlock( Grid grid )
        {

            var indexes = new List<int>();

            var slice = grid?.Slice is not null ? grid?.Slice.ToEssGridSlice() : Slice;
            var sliceEnd = (grid?.Slice?.Data?.Ranges[0] is null ? Slice?.Data?.Ranges[0]?.End : grid.Slice.Data.Ranges[0].End) + 1;

            var dataGridFirstCell = GetDataBlockStartIndex(slice);
            // find the start row index. Used to find the Dimension Members.
            var rowIndex = dataGridFirstCell.startRow * Slice.Columns;
            // first cell index of the data block
            var dataBlockStartIndex = _dataGridStartIndex = GetCoordinate(dataGridFirstCell, Slice.Columns);
            // last cell index, of the first row, of the data block.
            var dataBlockEndIndex = (dataBlockStartIndex / Slice.Columns + 1) * Slice.Columns - 1;

            // loop through each cell...
            for ( int index = 0; index < sliceEnd; index++ )
            {
                if ( dataBlockStartIndex <= index && dataBlockEndIndex >= index )
                    indexes.Add(index);

                // Set row index and data blocks start/end indexes to next row.
                if ( index == dataBlockEndIndex && index != Slice.Data.Ranges[0].End )
                {
                    rowIndex += Slice.Columns;
                    dataBlockStartIndex += Slice.Columns;
                    dataBlockEndIndex += Slice.Columns;
                }
            }
            return indexes;
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
                    case GridOperation.ActionEnum.Zoomin:
                    case GridOperation.ActionEnum.Zoomout:
                    case GridOperation.ActionEnum.Keeponly:
                    case GridOperation.ActionEnum.Removeonly:
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
                        //Alias = this.Alias,
                        Dimensions = this.Dimensions.ToModelBean(),
                        Slice      = this.Slice.ToModelBean(),  
                    },
                    Action      = action,
                    Alias       = UseAliases ? this.Alias : "none",
                    Coordinates = coordinates,
                    Ranges      = ranges
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

        private void SetDirtyCells()
        {
            if ( _grid is not null )
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
        }

        #endregion
    }
}
