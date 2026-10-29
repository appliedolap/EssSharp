using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

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

        #endregion

        #region Constructors

        /// <summary />
        public EssGrid( EssCube cube ) : base(cube?.Configuration, cube?.Client)
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

            _oldValues = _grid?.Slice?.Data?.Ranges[0]?.Values?.ToList() ?? new List<string>();
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
                    _essGridDimension = dimensions;

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
                    _essGridSlice = slice;

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
        public bool UseAliases { get; set; } = true;

        /// <inheritdoc />
        public EssGridPreferences Preferences { get; set; }

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        public List<IEssDrillthroughReport> GetDrillThroughReportForCells(List<EssDrillthroughRange> dtr, bool getDetails = false) => GetDrillThroughReportForCellsAsync(dtr, getDetails).GetAwaiter().GetResult();
        
        /// <inheritdoc />
        public async Task<List<IEssDrillthroughReport>> GetDrillThroughReportForCellsAsync( List<EssDrillthroughRange> dtr , bool getDetails = false, CancellationToken cancellationToken = default )
        {
            try
            {
                if (dtr is not { } cells)
                    throw new ArgumentException("An EssDrillthroughRange is required.");

                List<IEssDrillthroughReport> dtrs = new List<IEssDrillthroughReport>();

                var api = GetApi<DrillThroughReportsApi>();

                var reports = (await api.DrillThroughReportsGetReportsForIntersectionsAsync(applicationName: Cube.Application.Name, databaseName: Cube.Name, body: dtr.ToModelBean(new EssDrillthroughOptions(aliasTable: Alias)), cancellationToken: cancellationToken).ConfigureAwait(false));
                
                foreach( var report in reports)
                    dtrs.Add(await Cube.GetDrillthroughReportAsync(report, getDetails, cancellationToken));

                return dtrs;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if (Cube?.Application?.Server is EssServer server && await server.FeatureSupport.GetNotSupportedExceptionAsync(EssFeature.DrillthroughReportsForCells, e, cancellationToken).ConfigureAwait(false) is { } notSupportedException)
                    throw notSupportedException;

                throw new Exception($@"Unable to get the drillthrough reports for the given cells of the ""{Name}"" grid. {e.Message}", e);
            }
        }
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
                // Verify that there is a data slice to send.
                if ( Slice?.Data?.Ranges?.FirstOrDefault()?.Values?.ToList() is not { } submitValues )
                    throw new Exception("The data slice contains no ranges or values.");

                // A refresh must be performed first if we are tracking data changes.
                if ( Preferences?.TrackDataChanges is true )
                {
                    try
                    {
                        await RefreshAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                    }
                    finally
                    {
                        if ( _grid?.Slice?.Data?.Ranges?.FirstOrDefault()?.Values is not null )
                            _grid.Slice.Data.Ranges[0].Values = submitValues;
                    }
                }

                // Submit the data.
                await ExecuteGridOperationAsync(action: GridOperation.ActionEnum.Submit, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to submit new values for ""{Name}"" grid. {e.Message}", e);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="gridSelection"></param>
        /// <param name="newPosition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                
                // Type each cell for Essbase, and clear the data block for any operation but a submit (#72).
                var slice = PrepareSliceForOperation(action);

                var body = GetGridOperation(action, slice, gridSelection, newPosition);

                if ( await api.GridExecuteAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, svParity: Preferences?.SmartViewParity, body: body, preferences: Preferences, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } grid )
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
                if ( action == GridOperation.ActionEnum.Submit && Preferences.TrackDataChanges )
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

            var dataBlockStartAddress = GetDataBlockStartAddress(Slice);
            var dataGridStartIndex = GetCoordinate(dataBlockStartAddress, Slice.Columns);

            var dimMemberDict = new Dictionary<string, string>();

            changes.Application = Cube.Application.Name;
            changes.Database = Cube.Name;
            changes.Server = Cube.Application.Server.Name;
            changes.User = Configuration.Username;
            
            // loop through each cell...
            for ( int index = 0; index < (valuesCount + 1); index++ )
            {
                // and determine if the value has changed.
                if ( !string.Equals(oldValues[index], newValues[index]) )
                {
                    // only hit cells within the data block.
                    if ( (index % Slice.Columns) >= dataBlockStartAddress.startColumn && (index / Slice.Columns) >= dataBlockStartAddress.startRow )//( dataBlockStartIndex <= index && dataBlockEndIndex >= index )
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
                            OldValue = double.TryParse(oldValues[index], NumberStyles.Float, CultureInfo.InvariantCulture, out var ov) ? ov : null,
                            // the grid's new value
                            NewValue = double.TryParse(newValues[index], NumberStyles.Float, CultureInfo.InvariantCulture, out var nv) ? nv : null,
                            // Grid Dimension and Dimension member information
                            DataPoints = dimMemberDict//newGrid.Dimensions
                            .Select(gd => new EssDataPoint()
                            {
                                DimensionName = gd.Key,
                                DimensionNumber = Cube.Dimensions.FirstOrDefault(cd => string.Equals(cd.Name, gd.Key)).DimensionNumber,
                                // first value of the tuple returned from the GetDimensionMembersForDataCell() method, null if no value.
                                Alias = UseAliases ? string.Empty /*dimMemberDict[gd.Name].Item1*/ : null,
                                // second value of the tuple returned from the GetDimensionMembersForDataCell() method, null if no value.
                                Member = UseAliases ? dimMemberDict[gd.Key]/*dimMemberDict[gd.Name].Item2*/ : null
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
            var dataGridStartIndex = GetCoordinate(GetDataBlockStartAddress(Slice), Slice.Columns);


            int currRowIndex = (dataCellIndex / columnCount) * columnCount;

            for ( int i = 0; i < (dataGridStartIndex % columnCount); i++ )
            {
                var index = currRowIndex;
                while ( string.IsNullOrEmpty(values[index]) )
                    index -= columnCount;

                memDict[dimensions.FirstOrDefault(dim => dim.Column == (currRowIndex % columnCount)).Name] = values[index];
                currRowIndex++;
            }

            // Find the row index where the data block starts
            var dbStartRow = dataGridStartIndex / columnCount;

            // and use them to find the index of the current cells dimension member
            var columnMemberIndex = dataCellIndex - (((dataCellIndex / columnCount) + 1 - dbStartRow) * Slice.Columns);
            var columnLimiter = (columnMemberIndex / columnCount + 1);
            var columnStartIndex = dataGridStartIndex - grid.Slice.Columns;
            var columnEndIndex = (columnStartIndex / grid.Slice.Columns + 1) * grid.Slice.Columns - 1;

            for ( int i = 0; i < columnLimiter; i++ )
            {
                var index = columnMemberIndex;
                var distance = 1;

                while ( string.IsNullOrEmpty(values[index]) )
                {
                    if ( !string.IsNullOrEmpty(values[index - distance]) && (index - distance) != columnStartIndex )
                    {
                        index -= distance;
                        break;
                    }
                    else if ( !string.IsNullOrEmpty(values[index] + distance) && (index - distance) != columnEndIndex )
                    {
                        index += distance;
                        break;
                    }
                    else
                    {
                        distance++;
                    }
                }
                memDict[dimensions.FirstOrDefault(dim => dim.Row == i).Name] = values[index];
                columnMemberIndex -= columnCount;
                columnEndIndex -= columnCount;
                columnStartIndex -= columnCount;
            }
            return memDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private int GetDimensionMemberLength(Grid grid)
        {
            var dataGridStartIndex = GetCoordinate(GetDataBlockStartAddress(Slice), Slice.Columns);
            var memberHeaderStartIndex = dataGridStartIndex - grid.Slice.Columns;
            var memberHeaderEndIndex = (memberHeaderStartIndex / grid.Slice.Columns + 1) * grid.Slice.Columns - 1;
            var memberHeaderRowCount = memberHeaderStartIndex / grid.Slice.Columns + 1;
            var loopCount = grid.Slice.Columns - (memberHeaderStartIndex % grid.Slice.Columns);
            var dataMemberCounts = new List<int>();
            var values = grid.Slice.Data.Ranges[0].Values;

            for ( var rowCount = 0; rowCount < memberHeaderRowCount; rowCount++ )
            {
                var count = 1;
                for ( var i = 1; i < loopCount; i++ )
                {
                    var firstMember = values[memberHeaderStartIndex];

                    if ( !string.IsNullOrEmpty(firstMember) && !string.Equals(firstMember, values[memberHeaderStartIndex + i]) )
                        count++;
                }
                dataMemberCounts.Add(count);
                memberHeaderStartIndex -= grid.Slice.Columns;
                memberHeaderEndIndex -= grid.Slice.Columns;
            }

            return dataMemberCounts.Max();
        }

        /// <summary>
        /// Returns the slice to send Essbase for the given operation: a copy of the grid's slice, with each cell typed by its
        /// value and the caller's type, as <see cref="EssGridCellLayout"/> describes, and text in the data block sent blank as
        /// the comment it is. A submit sends the data block's values,
        /// a missing label as a missing value, and a blank data cell as missing when
        /// <see cref="EssGridPreferences.SendBlanksAsMissing"/> is set, and never writes a no-access cell or a number outside
        /// the data block. Any other operation sends every data cell blank, since Essbase reads the layout from the members.
        /// The grid's own slice is left as it is, so a failed operation leaves the grid unchanged (#72).
        /// </summary>
        /// <param name="action">The operation.</param>
        private EssGridSlice PrepareSliceForOperation( Action action )
        {
            var slice = Slice;

            if (slice?.Data?.Ranges?.FirstOrDefault() is not { } range)
                return new EssGridSlice();

            var count               = Math.Max(range.End + 1, 0);
            var values              = Enumerable.Range(0, count).Select(index => index < (range.Values?.Count ?? 0) ? range.Values[index] ?? string.Empty : string.Empty).ToList();
            var layout              = new EssGridCellLayout(values, range.Types, slice.Columns, Preferences);
            var isSubmit            = action == Action.Submit;
            var sendBlanksAsMissing = Preferences?.SendBlanksAsMissing is true;
            var sentValues          = new List<string>(count);
            var sentTypes           = new List<string>(count);
            var dirtyCells          = new List<int>();

            for (var index = 0; index < count; index++)
            {
                var value = values[index];

                var (sentValue, sentType) = layout.Kinds[index] switch
                {
                    // Text in the data block is a comment, which Essbase cannot keep in a grid it is sent.
                    EssGridCellLayout.Member when layout.IsComment(index) => (string.Empty, EssGridCellLayout.Blank),
                    EssGridCellLayout.Member => (value, EssGridCellLayout.Member),

                    // Only a submit sends data, and only to the data block's writable cells.
                    EssGridCellLayout.Data when !isSubmit || !layout.IsInDataBlock(index) || layout.IsNoWriteLabel(value) => (string.Empty, EssGridCellLayout.Blank),
                    EssGridCellLayout.Data when layout.IsMissingLabel(value) => (string.Empty, EssGridCellLayout.Data),
                    EssGridCellLayout.Data when string.IsNullOrWhiteSpace(value) => (string.Empty, sendBlanksAsMissing ? EssGridCellLayout.Data : EssGridCellLayout.Blank),
                    EssGridCellLayout.Data => (value, EssGridCellLayout.Data),

                    // A blank cell inside the data block is a missing value when the preferences send blanks as missing.
                    _ when isSubmit && sendBlanksAsMissing && layout.IsInDataBlock(index) => (string.Empty, EssGridCellLayout.Data),
                    _ => (string.Empty, EssGridCellLayout.Blank)
                };

                sentValues.Add(sentValue);
                sentTypes.Add(sentType);

                if (isSubmit && sentType == EssGridCellLayout.Data)
                    dirtyCells.Add(index);
            }

            var sentData = new EssGridSliceData();

            sentData.Ranges.Add(new EssGridRange()
            {
                DataFormats = range.DataFormats,
                End         = range.End,
                EnumIds     = range.EnumIds,
                Filters     = range.Filters,
                Start       = range.Start,
                Statuses    = range.Statuses,
                Texts       = range.Texts,
                Types       = sentTypes,
                Values      = sentValues
            });

            sentData.Ranges.AddRange(slice.Data.Ranges.Skip(1));

            return new EssGridSlice()
            {
                Columns    = slice.Columns,
                Data       = sentData,
                DirtyCells = isSubmit ? dirtyCells : slice.DirtyCells,
                DirtyTexts = slice.DirtyTexts,
                Rows       = slice.Rows
            };
        }

        /// <summary>
        /// Returns the first row and column of the given slice's data block, from its cells' kinds as
        /// <see cref="EssGridCellLayout"/> types them, or the grid's first cell when it has no data cell (#72).
        /// </summary>
        /// <param name="slice">The slice.</param>
        private EssGridSelection GetDataBlockStartAddress( EssGridSlice slice )
        {
            if (slice?.Data?.Ranges?.FirstOrDefault() is not { } range)
                return new EssGridSelection(0, 0);

            var layout = new EssGridCellLayout(range.Values ?? new List<string>(), range.Types, slice.Columns, Preferences);

            return layout.Start is { } start ? new EssGridSelection(start.Row, start.Column) : new EssGridSelection(0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="slice">The slice to send, as <see cref="PrepareSliceForOperation"/> prepared it.</param>
        /// <param name="gridSelection"></param>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        private GridOperation GetGridOperation( GridOperation.ActionEnum action, EssGridSlice slice, List<EssGridSelection> gridSelection = null, EssGridSelection newPosition = null )
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
                    Grid = new Grid()
                    {
                        Alias      = _grid?.Alias,
                        Dimensions = _grid?.Dimensions ?? this.Dimensions.ToModelBean(),
                        Slice      = slice.ToModelBean()
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

        #endregion
    }
}
