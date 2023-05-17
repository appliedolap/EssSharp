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
        bool PrefixStringValuesAsExcelText { get; set; }

        #endregion
    }
}
