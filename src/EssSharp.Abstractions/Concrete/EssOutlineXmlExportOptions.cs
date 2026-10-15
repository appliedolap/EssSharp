using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary>
    /// Options for exporting a cube outline to XML.
    /// </summary>
    public class EssOutlineXmlExportOptions
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EssOutlineXmlExportOptions"/> class.
        /// </summary>
        public EssOutlineXmlExportOptions() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EssOutlineXmlExportOptions"/> class.
        /// </summary>
        /// <param name="dimensions">(optional) The names of the dimensions to export. All dimensions are exported when none are given.</param>
        /// <param name="aliasTable">(optional) The alias table whose alias values are exported. Ignored when <paramref name="tree"/> is true.</param>
        /// <param name="tree">(optional) Whether to export only the member names of the outline.</param>
        public EssOutlineXmlExportOptions( IEnumerable<string> dimensions = null, string aliasTable = null, bool tree = false )
        {
            AliasTable = aliasTable;
            Dimensions = dimensions is null ? new List<string>() : new List<string>(dimensions);
            Tree       = tree;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The alias table whose alias values are exported. When given, the export holds only the alias values of that
        /// table. Ignored when <see cref="Tree"/> is true.
        /// </summary>
        public string AliasTable { get; set; } = null;

        /// <summary>
        /// The names of the dimensions to export. All dimensions are exported when the list is empty.
        /// </summary>
        public List<string> Dimensions { get; set; } = new List<string>();

        /// <summary>
        /// Whether to export only the member names of the outline.
        /// </summary>
        public bool Tree { get; set; } = false;

        #endregion
    }
}
