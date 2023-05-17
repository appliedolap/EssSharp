using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{

    public enum DatabaseType
    {
        ASO,

        BSO
    }

    public class EssDatabaseCreationOptions
    {

        public EssDatabaseCreationOptions( DatabaseType dbType = DatabaseType.BSO, bool enableScenarios = false, bool allowDuplicates = false )
        {
            DatabaseType = dbType;

            EnableScenarios = enableScenarios;

            AllowDuplicates = allowDuplicates;
        }


        public DatabaseType DatabaseType { get; set; }

        public bool EnableScenarios { get; set; }

        public bool AllowDuplicates { get; set; }

    }
}
