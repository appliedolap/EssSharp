﻿using System;
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
        public bool UseAliases { get; set; } = true;

        /// <inheritdoc />
        public EssGridPreferences Preferences { get; set; }

        public bool WriteAllNonNumericValuesOnRetrieve { get; set; } = false;

        public List<string> lastInternalGridUniqueMemberNames { get; set; } = new List<string>();

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



                //if ( action is not GridOperation.ActionEnum.Submit )
                //RemoveDataBlock();

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
                _grid = grid;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to perform grid operation {action} on ""{Name}"" grid. {e.Message}", e);
            }
        }

        // TODO: drop method?
        /*
        private void PrepareSliceForOperation_old( Action action )
        {
            // set a local flag to indicate if this is an "Update" operation
            bool isUpdate = action is Action.Submit;
            bool isDataCell = false;

            // for each cell in the internal Values array, add a Cell element
            //
            // note: ReadGrid sets the internal start row/column to (1,1)
            //       even if the first cell of the defined retrieve range is not (1,1),
            //       so the StartCellRow and StartCellColumn are (1,1).
            //
            string cellValue = null;
            var cellType = "7";
            string lastCellValue = null;
            bool appendCell = false;

            // get a local reference to the internal grid's values array.
            var cellValues = Slice.Data.Ranges[0].Values;
            var cellTypes = Slice.Data.Ranges[0].Types;

            bool isNumeric = false;
            bool hasInvalidChar;

            // Used by the SendBlanksAsMissing logic.
            bool isDataRow = false;
            bool detectedFirstNonBlankCellInDataRow;
            bool isBlankCell;

            // The column number is one-based and is relative to the first column in the range, which is 1.
            int firstColumnNumberWithNonBlankCell = -1;

            // Hardcode these to false for now.
            bool useUniqueMemberNames = false;
            bool sendBlanksAsMissing = !string.IsNullOrEmpty(Preferences.MissingText) || !string.IsNullOrEmpty(Preferences.NoAccessText);

            var values = new List<string>(Slice.Data.Ranges[0].Values.Count);
            var types =  new List<string>(Slice.Data.Ranges[0].Types.Count);

            //bool WriteAllNonNumericValuesOnRetrieve = false;
            //var lastInternalGridUniqueMemberNames = new List<string>();

            // If sending blanks as missing, find the first column in the send range that contains non-blank values.
            // Typically, this will be the first column, but allow for the left-most column(s) to contain all blanks.
            // Set the firstColumnNumberWithNonBlankCells to indicate the number of the column without all blanks.
            if ( isUpdate && sendBlanksAsMissing )
            {
                for ( int index = 0; index < Slice.Data.Ranges[0].End; index++ )
                {
                    if ( !string.IsNullOrEmpty(cellValue = Slice.Data.Ranges[0].Values[index]) )
                    {
                        firstColumnNumberWithNonBlankCell = index % Slice.Columns + 1;
                        break;
                    }
                }

                /*
                for ( int colIndex = internalGrid.StartCellColumn - internalGrid.ColumnOffset, colNumber = 1;
                          colIndex <= internalGrid.EndCellColumn - internalGrid.ColumnOffset;
                          colIndex++, colNumber++ )
                {
                    for ( int rowIndex = internalGrid.StartCellRow - internalGrid.RowOffset;
                              rowIndex <= internalGrid.EndCellRow - internalGrid.RowOffset;
                              rowIndex++ )
                    {
                        if ( cellValues[rowIndex, colIndex] != null )
                        {
                            firstColumnNumberWithNonBlankCell = colNumber;

                            break;
                        }
                    }

                    if ( firstColumnNumberWithNonBlankCell > -1 )
                        break;
                }
                
            }

            for ( int index = 0; index < (Slice.Data.Ranges[0].End + 1); index++ )
            {
                detectedFirstNonBlankCellInDataRow = false;

                cellValue = cellValues[index];
                cellType = "7";

                isBlankCell = string.IsNullOrEmpty(cellValue);

                // If the cell value is non-null OR the grid is sending blanks as #MISSING and the cell is blank...
                if ( !isBlankCell || (isUpdate && sendBlanksAsMissing) )
                {
                    isNumeric = false;

                    // The isDataCell flag is used when processing an update when unique member names exist.
                    isDataCell = false;

                    if ( !isBlankCell )
                    {
                        // If we have a non-blank cell start by assuming that the cell represents a member.
                        cellType = "0";

                        // If the cell value does not contain trailing blanks...
                        // Note: When a cell value contains trailing blanks, both leading and trailing blanks are retained.
                        //       This approach is consistent with the Classic Add-In.
                        //
                        // This code was added to address an incompatibility with the Classic Add-In.
                        // The GridExcel.ReadGrid was also modified to not trim leading blanks when trailing blankis exist.
                        // The SpreadsheetGear.ReadGrid was NOT modified, so for the SmartClient, leading blanks will not be retained, but this may change in the future.
                        if ( !cellValue.EndsWith(" ") )
                            cellValue = cellValue.TrimStart();
                    }
                }

                // If the cell value is non-empty OR the grid is sending blanks as #MISSING and the cell is blank...
                if ( cellValue.Length > 0 || (isUpdate && sendBlanksAsMissing) )
                {
                    // for an update operation, all cells should be included in the request
                    if ( isUpdate )
                    {
                        // When sending blanks as missing...
                        // Note: Only blank cells to the right of the first non-blank cell on a data row should be sent as #MISSING.
                        if ( sendBlanksAsMissing )
                        {
                            // If the first data row has not been detected...
                            if ( !isDataRow )
                            {
                                // When the column is the first column in the range with a non-blank cell and the current cell is non-blank,
                                // treat the row and all subsequent rows as data rows.
                                if ( index % Slice.Columns == firstColumnNumberWithNonBlankCell && !isBlankCell )
                                    isDataRow = true;
                            }

                            // If a data row...
                            if ( isDataRow )
                            {
                                // If the first non-blank cell in the row has not been detected...
                                if ( !detectedFirstNonBlankCellInDataRow )
                                {
                                    // If the cell is non-blank, any blank cell to the right of the current cell is sent as #MISSING.
                                    if ( !isBlankCell )
                                        detectedFirstNonBlankCellInDataRow = true;
                                }
                            }
                        }

                        // If sending blanks as missing and the cell is blank...
                        if ( sendBlanksAsMissing && isBlankCell )
                        {
                            // If this is a data row and the first non-blank cell in the row has been detected...
                            if ( isDataRow && detectedFirstNonBlankCellInDataRow )
                            {
                                // Set flags to append the cell and to treat the cell as a data cell.
                                appendCell = true;
                                isDataCell = true;

                                // Set the value to #MISSING.
                                cellValue = "#MISSING";
                            }
                            /*
                            else
                            {
                                // Skip to the next cell.
                                continue;
                            }
                            
                        }
                        else
                        {
                            appendCell = true;                  // append both member and data cells for an update operation

                            isNumeric = int.TryParse(cellValue, out _) || double.TryParse(cellValue, out _);

                            
                            // Determine whether the value is numeric.
                            isNumeric = this.IsNumeric(cellValue, numberFormatProvider);
                            

                            // If unique member names exist, determine whether the cell represents a data cell.
                            // The isDataCell flag is used to determine whether to check for a unique member name for the current cell.
                            if ( useUniqueMemberNames )
                            {
                                if ( Double.TryParse(cellValue, out var doubleValue) || int.TryParse(cellValue, out var intValue) )
                                {
                                    isDataCell = true;
                                    isNumeric = true;
                                }
                                else if ( string.Equals(cellValue, Preferences.MissingText) )
                                    isDataCell = true;
                                else if ( string.Equals(cellValue, Preferences.NoAccessText) )
                                    isDataCell = true;
                            }

                            if ( isNumeric || isDataCell )
                                cellType = "2";
                        }
                    }
                    else
                    {
                        // for a non-update operation, only member cells should be included in the request.
                        // if numeric or the value is the missing text string, treat as a data cell, since numeric member names in the Values array
                        // are prepended with a single quote.
                        //
                        if ( Double.TryParse(cellValue, out _) || int.TryParse(cellValue, out _) )
                        {
                            appendCell = false;             // do not append numeric cells for a non-update operation unless WriteAllNonNumericValuesOnRetrieve is True
                            isNumeric = true;
                        }
                        else if ( string.Equals(cellValue, Preferences.MissingText) )
                            appendCell = false;             // treat the missing text string value as a numeric cell - do not append numeric cells for a non-update operation
                        else if ( string.Equals(cellValue, Preferences.NoAccessText) )
                            appendCell = false;             // treat the "no access" string value as a numerid cell - do not append numeric cells for a non-update operation
                        else
                            appendCell = true;              // append non-numeric cells for a non-update operation
                    }

                    if ( !appendCell )
                    {
                        cellValue = string.Empty;
                        cellType = "7";
                    }

                    // If unique names exist...
                    if ( useUniqueMemberNames )
                    {
                        // If not updating OR if updating and the cell is likely a member cell, determine whether to use the actual cell value or the unique member name.
                        if ( !isUpdate || (isUpdate && !isDataCell) )
                        {
                            if ( lastInternalGridUniqueMemberNames[index] != null )
                            {
                                // Get the last cell value as a string.
                                // Note: To ensure that formatted numeric strings are detected as numbers, use the culture-compatible format provider.
                                lastCellValue = Convert.ToString(cellValues[index]);

                                // Trim leading and trailing blanks.
                                lastCellValue = lastCellValue.Trim();

                                if ( lastCellValue.Length > 0 )
                                {
                                    // If the current cell value and the corresponding last cell value are the same, based on a case-insensitive comparison,
                                    // use the unique name, if a unique name exists.
                                    if ( string.Compare(cellValue, lastCellValue, true) == 0 )
                                    {
                                        cellValue = lastInternalGridUniqueMemberNames[index];
                                    }
                                }
                            }
                        }
                    }

                    /*
                    if ( isUpdate || !isDataCell )
                    {
                        values[index] = cellValue;
                        types[index] = cellType;
                    }
                    else
                    {
                        values[index] = "";
                        types[index] = "7";
                    }
                   

                    
                    // add a Cell element with row and column attributes
                    this.WriteStartElement(xmlWriter, "Cell");
                    this.WriteAttributeString(xmlWriter, "r", rowNumber.ToString());
                    this.WriteAttributeString(xmlWriter, "c", colNumber.ToString());

                    hasInvalidChar = false;

                    if ( !isNumeric )
                    {
                        // Determine whether the cell value contains an invalid Xml character.
                        hasInvalidChar = StringUtility.ContainsInvalidXmlCharacter(cellValue);
                    }

                    // If the cell value contains an invalid Xml character, base64 encode the value.
                    if ( hasInvalidChar )
                    {
                        // The cell value contains an invalid Xml character.
                        // Base64 encode the cell value.

                        // Flag the cell value as base64 encoded.
                        // dt:dt="bin.base64" xmlns:dt="urn:schemas-microsoft-com:datatypes"
                        //xmlWriter.WriteAttributeString("dt", "dt", "urn:schemas-microsoft-com:datatypes", "bin.base64");
                        this.WriteAttributeString(xmlWriter, "dt", "bin.base64", "dt", "urn:schemas-microsoft-com:datatypes");

                        // Convert the cell value to a byte array.
                        byte[] bytes = Encoding.UTF8.GetBytes(cellValue);

                        // Base64 encode the byte array.
                        string base64String = Convert.ToBase64String(bytes);

                        // Write the encoded string.
                        this.WriteString(xmlWriter, base64String);
                    }
                    else
                    {
                        this.WriteString(xmlWriter, cellValue);
                    }

                    this.WriteEndElement(xmlWriter);
                }

                // Add the value and type to the collections.
                values.Add(cellValue);
                types.Add(cellType);
            }

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
        }*/

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
                int dataBlockStartIndex;
                int dataBlockEndIndex;
                var dataGridFirstCell = new EssGridSelection(0, 0);
                int firstRowIndex = 0;
                bool isUpdate = action == GridOperation.ActionEnum.Submit;
                bool sendBlanksAsMissing = Preferences.SendBlanksAsMissing;
                var types = new List<string>();
                var values = new List<string>();

                // Find the row the data block starts
                for ( int index = 0; index < (Slice?.Data?.Ranges[0]?.End + 1); index++ )
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

                // Find the first index of the data block and the last index of the data block for the first row
                dataBlockStartIndex = GetCoordinate(dataGridFirstCell, Slice.Columns);
                dataBlockEndIndex = (dataBlockStartIndex / Slice.Columns + 1) * Slice.Columns - 1;

                // Loop through entire grid and decide the type of each cell.
                for ( int index = 0; index < (Slice.Data.Ranges[0].End + 1); index++ )
                {
                    if ( dataBlockStartIndex <= index && dataBlockEndIndex >= index )
                    {
                        if ( isUpdate )
                        {
                            cellValue = cellValues[index];

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

                    // Set data blocks start and end indexes to next row.
                    if ( index == dataBlockEndIndex )
                    {
                        dataBlockStartIndex += Slice.Columns;
                        dataBlockEndIndex += Slice.Columns;
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
