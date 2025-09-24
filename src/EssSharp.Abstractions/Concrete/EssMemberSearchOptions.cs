using System;

namespace EssSharp
{
    [Flags]
    public enum EssMemberSearchOptions
    {
        membersOnly                = 1 << 0,
        membersAndAliases          = 1 << 1,
        forceCaseSensitive         = 1 << 2
    }
}
