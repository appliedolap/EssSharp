using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public enum EssUserPermissionRole
    {
        Unknown = 0,

        None = 1,

        db_access = 2,

        db_update = 3,

        db_manager = 4,

        app_manager = 5
    }
}
