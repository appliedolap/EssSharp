using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    [Flags]
    public enum EssMemberSearchOptions
    {
        MEMBERSONLY                = 1 << 0,
        MEMBERSANDALIASES          = 1 << 1,
        FORCECASESENSITIVE         = 1 << 2
    }
}
