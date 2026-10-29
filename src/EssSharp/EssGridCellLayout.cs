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
    /// Numbers are read in the invariant culture, as Essbase writes them. A data cell sits at a data position, with a member to
    /// its left in its row and a member above it in its column; the data block is the rectangle those cells span.
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

        private readonly int[]        _firstMemberInRow;
        private readonly int[]        _firstMemberInColumn;
        private readonly ISet<string> _missingLabels;
        private readonly ISet<string> _noWriteLabels;

        #endregion

        #region Constructors

        /// <summary>
        /// Types the cells of a grid of the given width.
        /// </summary>
        /// <param name="values">The grid's values, row by row.</param>
        /// <param name="givenTypes">The types the caller gives, by the same index, if any; a missing or empty type is inferred.</param>
        /// <param name="columns">The grid's column count.</param>
        /// <param name="preferences">The grid's preferences, whose missing and no-access labels are data.</param>
        internal EssGridCellLayout( IReadOnlyList<string> values, IReadOnlyList<string> givenTypes, int columns, EssGridPreferences preferences )
        {
            Columns = Math.Max(columns, 1);
            Count   = values?.Count ?? 0;

            _missingLabels = Labels(DefaultMissingLabels, preferences?.MissingText);
            _noWriteLabels = Labels(DefaultNoWriteLabels, preferences?.NoAccessText);

            Kinds = new string[Count];

            for ( var index = 0; index < Count; index++ )
                Kinds[index] = KindOf(values[index], givenTypes is not null && index < givenTypes.Count ? givenTypes[index] : null);

            var rows = (Count + Columns - 1) / Columns;

            _firstMemberInRow    = Enumerable.Repeat(-1, rows).ToArray();
            _firstMemberInColumn = Enumerable.Repeat(-1, Columns).ToArray();

            for ( var index = 0; index < Count; index++ )
            {
                if (Kinds[index] != Member)
                    continue;

                var (row, column) = (index / Columns, index % Columns);

                if (_firstMemberInRow[row] < 0)
                    _firstMemberInRow[row] = column;

                if (_firstMemberInColumn[column] < 0)
                    _firstMemberInColumn[column] = row;
            }

            // The data block spans the data cells that sit at a data position.
            for ( var index = 0; index < Count; index++ )
            {
                if (Kinds[index] != Data || !IsDataPosition(index))
                    continue;

                var (row, column) = (index / Columns, index % Columns);

                Block = Block is { } block
                    ? (Math.Min(block.Top, row), Math.Min(block.Left, column), Math.Max(block.Bottom, row), Math.Max(block.Right, column))
                    : (row, column, row, column);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The data block's first and last rows and columns, or <see langword="null"/> when the grid has no data cell.
        /// </summary>
        internal (int Top, int Left, int Bottom, int Right)? Block { get; }

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

        #endregion

        #region Methods

        /// <summary>
        /// Whether the cell at the given index lies in the data block: inside its rectangle, at a data position.
        /// </summary>
        /// <param name="index">The cell's index.</param>
        internal bool IsInDataBlock( int index ) =>
            Block is { } block &&
            index / Columns >= block.Top && index / Columns <= block.Bottom &&
            index % Columns >= block.Left && index % Columns <= block.Right &&
            IsDataPosition(index);

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
        /// Whether the cell at the given index has a member to its left in its row and a member above it in its column.
        /// </summary>
        /// <param name="index">The cell's index.</param>
        private bool IsDataPosition( int index )
        {
            var (row, column) = (index / Columns, index % Columns);
            var left          = _firstMemberInRow[row];
            var above         = _firstMemberInColumn[column];

            return left >= 0 && left < column && above >= 0 && above < row;
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
