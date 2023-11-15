using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    [Flags]
    public enum EssMemberFilterOption
    {
        /*
        Consolidation = 1 << 0,
        NumberOfChildren = 1 << 1,
        ActiveAliasName = 1 << 2,
        AttributeType = 1 << 3,
        ShareMembers = 1 << 4,
        MemberHasUniqueName = 1 << 5,
        All = Consolidation | NumberOfChildren | ActiveAliasName | AttributeType | ShareMembers | MemberHasUniqueName*/
        name = 1 << 1,
        aliases = 1 << 2,
        levelNumber = 1 << 3,
        generationNumber = 1 << 4,
        dataStorageType = 1 << 5,
        type = 1 << 6
    }
}