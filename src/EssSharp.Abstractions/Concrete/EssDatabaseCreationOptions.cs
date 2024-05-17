using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{
    /// <summary>
    /// Defines EssDatabaseType
    /// </summary>
    public enum EssDatabaseType
    {
        /// <summary>
        /// Enum value for ASO
        /// </summary>
        ASO,
        /// <summary>
        /// Enum value for BSO
        /// </summary>
        BSO
    }

    /// <summary />
    public class EssDatabaseCreationOptions
    {
        /// <summary />
        /// <param name="dbType"></param>
        /// <param name="enableScenarios"></param>
        /// <param name="allowDuplicates"></param>
        public EssDatabaseCreationOptions( EssDatabaseType dbType = EssDatabaseType.BSO, bool enableScenarios = false, bool allowDuplicates = false )
        {
            DatabaseType = dbType;

            EnableScenarios = enableScenarios;

            AllowDuplicates = allowDuplicates;
        }

        /// <summary>
        /// Database Type
        /// </summary>
        public EssDatabaseType DatabaseType { get; set; }

        /// <summary>
        /// Enables scenarios if set to true.
        /// </summary>
        public bool EnableScenarios { get; set; }

        /// <summary>
        /// if true, allow duplicates.
        /// </summary>
        public bool AllowDuplicates { get; set; }

    }
}
