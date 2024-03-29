﻿using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssDrillthroughOptions : IEssDrillthroughOptions
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EssDrillthroughOptions"/> class.
        /// </summary>
        public EssDrillthroughOptions() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EssDrillthroughOptions"/> class.
        /// </summary>
        /// <param name="aliasTable">(optional) The alias table that was used to produce the associated drillthrough ranges.</param>
        /// <param name="includeColumnHeaders">(optional) Whether to include column headers in the report.</param>
        /// <param name="includeMetadataOnly">(optional) Whether to include only column types and, optionally, column headers in the report.</param>
        /// <param name="returnTypedValues">(optional) Whether report values should be converted to the column types indicated by the server.</param>
        /// <param name="prefixStringValuesForExcel">(optional) Whether to prefix string values with an apostrophe (') for use in Excel.</param>
        public EssDrillthroughOptions( string aliasTable = null, bool includeColumnHeaders = true, bool includeMetadataOnly = false, bool returnTypedValues = false, bool prefixStringValuesForExcel = false )
        {
            AliasTable                 = aliasTable;
            IncludeColumnHeaders       = includeColumnHeaders;
            IncludeMetadataOnly        = includeMetadataOnly;
            PrefixStringValuesForExcel = prefixStringValuesForExcel;
            ReturnTypedValues          = returnTypedValues;
        }

        #endregion

        #region IEssDrillthroughOptions Members

        /// <inheritdoc />
        public string AliasTable { get; set; } = null;

        /// <inheritdoc />
        public bool IncludeColumnHeaders { get; set; } = true;

        /// <inheritdoc />
        public bool IncludeMetadataOnly { get; set; } = false;

        /// <inheritdoc />
        public bool PrefixStringValuesForExcel { get; set; } = false;

        /// <inheritdoc />
        public bool ReturnTypedValues { get; set; } = false;

        #endregion
    }
}

