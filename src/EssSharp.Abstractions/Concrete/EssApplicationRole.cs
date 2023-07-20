using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    [Flags]
    public enum EssApplicationRole
    {
        Unknown = 0,

        None = 1,

        db_access = 2,

        db_update = 3,

        db_manager = 4,

        app_manager = 5,

        All = 6
    }
}
