using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    [Flags]
    public enum EssPermissionType
    {
        User = 0,

        Group = 1,

        All = 2
    }
}
