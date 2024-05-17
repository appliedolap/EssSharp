using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EssSharp
{

    /// <summary>
    /// Defines Type
    /// </summary>
    public enum NamedQueryType
    {
        /// <summary>
        /// Enum Unknown for value: Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Enum MDX for value: MDX
        /// </summary>
        MDX = 1,

        /// <summary>
        /// Enum DATASOURCE for value: DATA_SOURCE
        /// </summary>
        DATASOURCE = 2

    }

    public class EssNamedQuery
    {
        /// <summary>
        /// Enum value for QUERYNAME
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// Enum value for QUERYTYPE
        /// </summary>
        public NamedQueryType QueryType { get; set; } = NamedQueryType.Unknown;

        /// <summary>
        /// Enum value for QUERY
        /// </summary>
        public EssQuery query { get; set; } = new EssQuery();
    }
}
