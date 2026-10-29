using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EssSharp
{
    /// <summary>
    /// The kind of each cell of a grid, and where its data block lies, as Essbase needs them to read the grid. Essbase does not
    /// type a grid's cells itself: it reads each cell as the type it is sent, a member, data, or a blank cell. A cell is typed by
    /// its value, as Smart View and the Essbase Java API do: text is a member, a number or a missing or no-access label is data,
    /// and an empty cell is blank. A caller that knows better settles the two cases a value cannot: a numeric member name, which
    /// a caller can instead write with a leading apostrophe, and a blank data cell (#72).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Numbers are read in the invariant culture, as Essbase writes them.
    /// </para>
    /// <para>
    /// The data block is found from the members alone, so it is found in a grid whose data cells are still empty. Its first
    /// row is the first that begins with a row member, in the grid's leftmost member column, below a column member. Its first
    /// column is that of the first column member, past the row members, in the nearest row above that holds one. A data cell
    /// lies in a row of the block that has a row member, under a column member. Text in the block is a comment, which Essbase
    /// REST cannot keep, rather than a member.
    /// </para>
    /// <para>
    /// A number the caller does not type is placed by where it lies. In a column header row that holds members, or in a row
    /// header column that holds members, it is a numeric member name; anywhere else, such as a row of numbers between the
    /// column headers and the row members, it is data.
    /// </para>
    /// </remarks>
    internal sealed class EssGridCellLayout
    {
        #region Constants

        /// <summary>
        /// The type Essbase gives a member cell.
        /// </summary>
        internal const string Member = "0";

        /// <summary>
        /// The type Essbase gives a data cell.
        /// </summary>
        internal const string Data = "2";

        /// <summary>
        /// The type Essbase gives a blank cell.
        /// </summary>
        internal const string Blank = "7";

        #endregion

        #region Private Data

        private static readonly string[] DefaultMissingLabels = { "#Missing" };
        private static readonly string[] DefaultNoWriteLabels = { "#No Access", "#NoAccess", "#Meaningless" };

        private int[]                 _firstMemberInRow;
        private readonly bool[]       _isUntypedNumber;
        private readonly bool[]       _hasColumnMember;
        private readonly ISet<string> _missingLabels;
        private readonly ISet<string> _noWriteLabels;

        #endregion

        #region Constructors

        /// <summary>
        /// Types the cells of a grid of the given width, and finds its data block.
        /// </summary>
        /// <param name="values">The grid's values, row by row.</param>
        /// <param name="givenTypes">The types the caller gives, by the same index, if any; a missing or empty type is inferred.</param>
        /// <param name="columns">The grid's column count.</param>
        /// <param name="preferences">The grid's preferences, whose missing and no-access labels are data.</param>
        internal EssGridCellLayout( IReadOnlyList<string> values, IReadOnlyList<string> givenTypes, int columns, EssGridPreferences preferences )
        {
            Columns = Math.Max(columns, 1);
            Count   = values?.Count ?? 0;
            Rows    = (Count + Columns - 1) / Columns;

            _missingLabels = Labels(DefaultMissingLabels, preferences?.MissingText);
            _noWriteLabels = Labels(DefaultNoWriteLabels, preferences?.NoAccessText);

            Kinds            = new string[Count];
            _isUntypedNumber = new bool[Count];

            for (var index = 0; index < Count; index++)
            {
                var givenType = givenTypes is not null && index < givenTypes.Count ? givenTypes[index] : null;

                Kinds[index]            = KindOf(values[index], givenType);
                _isUntypedNumber[index] = string.IsNullOrEmpty(givenType) && IsNumber(values[index]);
            }

            _firstMemberInRow = FirstMemberInEachRow();
            _hasColumnMember  = new bool[Columns];

            Start = FindStart();

            // An untyped number among the header members is a numeric member name.
            if (Start is { } headers)
            {
                for (var index = 0; index < Count; index++)
                {
                    if (!_isUntypedNumber[index])
                        continue;

                    var (row, column) = (index / Columns, index % Columns);

                    var inColumnHeaders = row < headers.Row && column >= headers.Column &&
                                          Enumerable.Range(headers.Column, Columns - headers.Column).Any(other => IsMember(row, other));

                    var inRowHeaders = row >= headers.Row && column < headers.Column &&
                                       Enumerable.Range(headers.Row, Rows - headers.Row).Any(other => IsMember(other, column));

                    if (inColumnHeaders || inRowHeaders)
                        Kinds[index] = Member;
                }

                _firstMemberInRow = FirstMemberInEachRow();
            }

            // A column of the block holds data only under a column member.
            if (Start is { } start)
            {
                for (var row = 0; row < start.Row; row++)
                    for (var column = start.Column; column < Columns; column++)
                        _hasColumnMember[column] |= IsMember(row, column);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The grid's column count.
        /// </summary>
        internal int Columns { get; }

        /// <summary>
        /// The grid's cell count.
        /// </summary>
        internal int Count { get; }

        /// <summary>
        /// Each cell's kind: <see cref="Member"/>, <see cref="Data"/>, or <see cref="Blank"/>.
        /// </summary>
        internal string[] Kinds { get; }

        /// <summary>
        /// The grid's row count.
        /// </summary>
        internal int Rows { get; }

        /// <summary>
        /// The data block's first row and column, or <see langword="null"/> when the grid has no row members below column members.
        /// </summary>
        internal (int Row, int Column)? Start { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Whether the cell at the given index is text in the data block: a comment, not a member.
        /// </summary>
        /// <param name="index">The cell's index.</param>
        internal bool IsComment( int index ) =>
            Kinds[index] == Member && Start is { } start && index / Columns >= start.Row && index % Columns >= start.Column;

        /// <summary>
        /// Whether the cell at the given index is a data cell of the data block: in a row of the block that has a row member,
        /// under a column member.
        /// </summary>
        /// <param name="index">The cell's index.</param>
        internal bool IsInDataBlock( int index )
        {
            if (Start is not { } start)
                return false;

            var (row, column) = (index / Columns, index % Columns);

            return row >= start.Row && column >= start.Column &&
                   _firstMemberInRow[row] >= 0 && _firstMemberInRow[row] < start.Column &&
                   _hasColumnMember[column];
        }

        /// <summary>
        /// Whether the given value is a missing label, which a submit writes as a missing value.
        /// </summary>
        /// <param name="value">The cell's value.</param>
        internal bool IsMissingLabel( string value ) => value is not null && _missingLabels.Contains(value.Trim());

        /// <summary>
        /// Whether the given value labels a cell that cannot be written, such as a no-access cell.
        /// </summary>
        /// <param name="value">The cell's value.</param>
        internal bool IsNoWriteLabel( string value ) => value is not null && _noWriteLabels.Contains(value.Trim());

        /// <summary>
        /// Whether the given value reads as a number in the invariant culture, as Essbase writes numbers.
        /// </summary>
        /// <param name="value">The cell's value.</param>
        internal static bool IsNumber( string value ) =>
            double.TryParse(value?.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var number) &&
            !double.IsNaN(number) && !double.IsInfinity(number);

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the data block's first row and column, from the members alone.
        /// </summary>
        private (int Row, int Column)? FindStart()
        {
            // The row members begin in the grid's leftmost member column.
            var leftmost = _firstMemberInRow.Where(column => column >= 0).DefaultIfEmpty(-1).Min();

            if (leftmost < 0)
                return null;

            for (var row = 0; row < Rows; row++)
            {
                if (_firstMemberInRow[row] != leftmost)
                    continue;

                // The row members run on from the leftmost column; the data begins past them.
                var rowHeadersEnd = leftmost;

                while (rowHeadersEnd < Columns && IsMember(row, rowHeadersEnd))
                    rowHeadersEnd++;

                // The first data row has a column member above it, past its row members; the nearest row above that holds
                // one is the column header row, whose first such member begins the data columns.
                for (var header = row - 1; header >= 0; header--)
                {
                    var column = Enumerable.Range(rowHeadersEnd, Math.Max(Columns - rowHeadersEnd, 0)).Where(candidate => IsMember(header, candidate)).DefaultIfEmpty(-1).First();

                    if (column >= 0)
                        return (row, column);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the column of each row's first member, or -1 for a row without one.
        /// </summary>
        private int[] FirstMemberInEachRow() =>
            Enumerable.Range(0, Rows).Select(row => Enumerable.Range(0, Columns).Where(column => IsMember(row, column)).DefaultIfEmpty(-1).First()).ToArray();

        /// <summary>
        /// Whether the cell at the given row and column is a member.
        /// </summary>
        private bool IsMember( int row, int column ) => row * Columns + column < Count && Kinds[row * Columns + column] == Member;

        /// <summary>
        /// Returns the kind of a cell from its value and the type the caller gives it.
        /// </summary>
        /// <param name="value">The cell's value.</param>
        /// <param name="givenType">The caller's type for the cell, or <see langword="null"/>.</param>
        private string KindOf( string value, string givenType )
        {
            // A blank cell is data only when the caller says so, as for a missing value Essbase returned.
            if (string.IsNullOrWhiteSpace(value))
                return givenType == Data ? Data : Blank;

            // A leading apostrophe marks a member, whatever follows it.
            if (value.TrimStart().StartsWith("'", StringComparison.Ordinal))
                return Member;

            // A number is data, unless the caller says it is a member with a numeric name.
            if (IsNumber(value))
                return givenType == Member ? Member : Data;

            if (IsMissingLabel(value) || IsNoWriteLabel(value))
                return Data;

            return Member;
        }

        /// <summary>
        /// Returns a case-insensitive set of the given labels and the preference's label, if it has one.
        /// </summary>
        /// <param name="defaults">The labels Essbase uses by default.</param>
        /// <param name="preference">The label the grid's preferences set, if any.</param>
        private static ISet<string> Labels( IEnumerable<string> defaults, string preference )
        {
            var labels = new HashSet<string>(defaults, StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(preference))
                labels.Add(preference.Trim());

            return labels;
        }

        #endregion
    }
}
