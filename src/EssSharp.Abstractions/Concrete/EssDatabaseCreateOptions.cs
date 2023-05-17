using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{

    public enum ApplicationType
    {
        ASO,

        BSO
    }

    public class EssDatabaseCreateOptions
    {

        public EssDatabaseCreateOptions( bool enableScenarios = false, bool allowDuplicates = false )
        {
            DatabaseType = ApplicationType.BSO.ToString();

            EnableScenarios = enableScenarios;

            AllowDuplicates = allowDuplicates;
        }

        public string DatabaseType { get; }

        public bool EnableScenarios { get; }

        public bool AllowDuplicates { get; }

    }
}
