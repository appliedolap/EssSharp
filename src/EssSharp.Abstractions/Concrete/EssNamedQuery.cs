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
        [EnumMember(Value = "Unknown")]
        Unknown = 0,

        /// <summary>
        /// Enum MDX for value: MDX
        /// </summary>
        [EnumMember(Value = "MDX")]
        MDX = 1,

        /// <summary>
        /// Enum DATASOURCE for value: DATA_SOURCE
        /// </summary>
        [EnumMember(Value = "DATA_SOURCE")]
        DATASOURCE = 2

    }

    public class EssNamedQuery
    {
        /// <summary>
        /// 
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NamedQueryType QueryType { get; set; } = NamedQueryType.Unknown;

        /// <summary>
        /// 
        /// </summary>
        public EssQuery query { get; set; } = new EssQuery();
    }
}
