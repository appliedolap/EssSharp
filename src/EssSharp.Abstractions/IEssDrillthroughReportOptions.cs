namespace EssSharp
{
    /// <summary />
    public interface IEssDrillthroughOptions
    {
        #region Properties

        /// <summary>
        /// Returns or sets the alias table that was used to produce the associated drillthrough ranges.
        /// </summary>
        public string AliasTable { get; set; }

        /// <summary>
        /// Returns or sets whether to include column headers in the report.
        /// </summary>
        bool IncludeColumnHeaders { get; set; }

        /// <summary>
        /// Returns or sets whether to include only column types and, optionally, column headers in the report.
        /// </summary>
        bool IncludeMetadataOnly { get; set; }

        /// <summary>
        /// Returns or sets the whether to prefix string values with an apostrophe (') for use in Excel.
        /// </summary>
        bool PrefixStringValuesForExcel { get; set; }

        /// <summary>
        /// Returns or sets whether report values should be converted to the column types indicated by the server: a
        /// <c>DOUBLE</c> column as doubles and a <c>LONG</c> column as longs. A string column whose every value is a date as the
        /// server's JDBC driver writes one (<c>yyyy-MM-dd</c>, optionally with a time and a fraction of a second) is returned as
        /// <see cref="System.DateTime"/> values as well, since Essbase types a date column as a string and the driver's form is
        /// fixed; a column with any other text stays text.
        /// </summary>
        bool ReturnTypedValues { get; set; }

        #endregion
    }
}
