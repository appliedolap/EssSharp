using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{

    public class EssApplicationCreationOptions
    {
        
        public EssApplicationCreationOptions( bool loadData = false, bool executeScript = false, bool createFiles = true, bool deleteExcelOnExecute = false, bool overwrite = true, bool recreateApp = true )
        {
            LoadData = loadData;
            ExecuteScript = executeScript;
            CreateFiles = createFiles;
            DeleteExcelOnExecute = deleteExcelOnExecute;
            Overwrite = overwrite;
            RecreateApp = recreateApp;
        }

        public bool LoadData {  get; set; }

        public bool ExecuteScript { get; set; }

        public bool CreateFiles { get; set; }

        public bool DeleteExcelOnExecute { get; set; }

        public bool Overwrite { get; set; }

        public bool RecreateApp { get; set;}

    }
}
