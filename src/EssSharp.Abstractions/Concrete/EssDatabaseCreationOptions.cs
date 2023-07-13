using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{
    public enum EssDatabaseType
    {
        ASO,

        BSO
    }

    public class EssDatabaseCreationOptions
    {

        public EssDatabaseCreationOptions( EssDatabaseType dbType = EssDatabaseType.BSO, bool enableScenarios = false, bool allowDuplicates = false )
        {
            DatabaseType = dbType;

            EnableScenarios = enableScenarios;

            AllowDuplicates = allowDuplicates;
        }


        public EssDatabaseType DatabaseType { get; set; }

        public bool EnableScenarios { get; set; }

        public bool AllowDuplicates { get; set; }

    }
}
