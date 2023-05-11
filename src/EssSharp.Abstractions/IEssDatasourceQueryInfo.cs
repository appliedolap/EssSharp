using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDatasourceQueryInfo
    {
        #region Properties

        /// <summary>
        /// Returns or sets the delimiter for the query results.
        /// </summary>
        string Delimiter { get; set; }

        /// <summary>
        /// Returns or sets the query parameter dictionary for the query.
        /// </summary>
        Dictionary<string, List<string>> Parameters { get; set; }

        /// <summary>
        /// Returns or sets the query.
        /// </summary>
        string Query { get; set; }

        #endregion
    }
}
