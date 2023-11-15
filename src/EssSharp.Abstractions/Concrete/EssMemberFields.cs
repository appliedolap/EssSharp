using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    [Flags]
    public enum EssMemberFields
    {
        name                       = 1 << 0,
        aliases                    = 1 << 1,
        levelNumber                = 1 << 2,
        generationNumber           = 1 << 3,
        dataStorageType            = 1 << 4,
        type                       = 1 << 5,
        numberOfChildren           = 1 << 6,
        uniqueName                 = 1 << 7,
        memberId                   = 1 << 8,
        perviousSiblingCount       = 1 << 9,
        descentantsCount           = 1 << 10,
        dimension                  = 1 << 11,
        dimensionSolveOrder        = 1 << 12,
        dimensionType              = 1 << 13,
        formatString               = 1 << 14,
        dimensionStorageType       = 1 << 15,
        currencyConversionCategory = 1 << 16,
        activeAliasName            = 1 << 17,
        hasUniqueName              = 1 << 18,
        uniqueId                   = 1 << 19,
        memberSolveOrder           = 1 << 20,
        dimensionName              = 1 << 21,
        attribute                  = 1 << 22,
        account                    = 1 << 23,
        uda                        = 1 << 24,
        parentName                 = 1 << 25
    }
}
