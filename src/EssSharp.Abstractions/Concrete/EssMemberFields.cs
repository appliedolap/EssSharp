using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary>
    /// Define EssMemberFields
    /// </summary>
    [Flags]
    public enum EssMemberFields
    {
        /// <summary>
        /// Enum value for NAME
        /// </summary>
        name                       = 1 << 0,
        /// <summary>
        /// Enum value for ALIASES
        /// </summary>
        aliases                    = 1 << 1,
        /// <summary>
        /// Enum value for LEVELNUMBER
        /// </summary>
        levelNumber                = 1 << 2,
        /// <summary>
        /// Enum value for GENERATIONNUMBER
        /// </summary>
        generationNumber           = 1 << 3,
        /// <summary>
        /// Enum value for DATASTORAGETYPE
        /// </summary>
        dataStorageType            = 1 << 4,
        /// <summary>
        /// Enum value for TYPE
        /// </summary>
        type                       = 1 << 5,
        /// <summary>
        /// Enum value for NUMBEROFCHILDREN
        /// </summary>
        numberOfChildren           = 1 << 6,
        /// <summary>
        /// Enum value for UNIQUENAME
        /// </summary>
        uniqueName                 = 1 << 7,
        /// <summary>
        /// Enum value for MEMBERID
        /// </summary>
        memberId                   = 1 << 8,
        /// <summary>
        /// Enum value for PERVIOUSSIBLINGCOUNT
        /// </summary>
        perviousSiblingCount       = 1 << 9,
        /// <summary>
        /// Enum value for DESCENDANTCOUNT
        /// </summary>
        descendantsCount           = 1 << 10,
        /// <summary>
        /// Enum value for DIMENSION
        /// </summary>
        dimension                  = 1 << 11,
        /// <summary>
        /// Enum value for DIMENSIONSOLVEORDER
        /// </summary>
        dimensionSolveOrder        = 1 << 12,
        /// <summary>
        /// Enum value for DIMENSIONTYPE
        /// </summary>
        dimensionType              = 1 << 13,
        /// <summary>
        /// Enum value for FORMATSTRING
        /// </summary>
        formatString               = 1 << 14,
        /// <summary>
        /// Enum value for DIMENSIONSTORAGETYPE
        /// </summary>
        dimensionStorageType       = 1 << 15,
        /// <summary>
        /// Enum value for CURRENCYCONVERSIONCATEGORY
        /// </summary>
        currencyConversionCategory = 1 << 16,
        /// <summary>
        /// Enum value for ACTIVEALIASNAME
        /// </summary>
        activeAliasName            = 1 << 17,
        /// <summary>
        /// Enum value for HASUNIQUENAME
        /// </summary>
        hasUniqueName              = 1 << 18,
        /// <summary>
        /// Enum value for UNIQUEID
        /// </summary>
        uniqueId                   = 1 << 19,
        /// <summary>
        /// Enum value for MEMBERSOLVEORDER
        /// </summary>
        memberSolveOrder           = 1 << 20,
        /// <summary>
        /// Enum value for DIMENSIONNAME
        /// </summary>
        dimensionName              = 1 << 21,
        /// <summary>
        /// Enum value for ATTRIBUTE
        /// </summary>
        attribute                  = 1 << 22,
        /// <summary>
        /// Enum value for ACCOUNT
        /// </summary>
        account                    = 1 << 23,
        /// <summary>
        /// Enum value for UDA
        /// </summary>
        uda                        = 1 << 24,
        /// <summary>
        /// Enum value for PARENTNAME
        /// </summary>
        parentName                 = 1 << 25
    }
}
