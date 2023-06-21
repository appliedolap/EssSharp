using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{
    /// <summary>
    /// Defines MemberIdentifierType
    /// </summary>
    public enum EssMemberIdentifierType
    {
        /// <summary>
        /// Enum NAME for value: NAME
        /// </summary>
        NAME = 1,

        /// <summary>
        /// Enum ALIAS for value: ALIAS
        /// </summary>
        ALIAS = 2,

        /// <summary>
        /// Enum UNIQUENAME for value: UNIQUE_NAME
        /// </summary>
        UNIQUENAME = 3
    }


    public class EssQueryPreferences
    {

        public EssQueryPreferences( bool dataless = default(bool), bool hideRestrictedData = default(bool), bool cellAttributes = default(bool), bool formatString = default(bool), bool formatValues = default(bool), bool meaninglessCells = default(bool), bool textList = default(bool), bool urlDrillThrough = default(bool), EssMemberIdentifierType? memberIdentifierType = null )
        {
            Dataless = dataless;
            HideRestrictedData = hideRestrictedData;
            CellAttributes = cellAttributes;
            FormatString = formatString;
            FormatValues = formatValues;
            MeaninglessCells = meaninglessCells;
            TextList = textList;
            UrlDrillThrough = urlDrillThrough;
            MemberIdentifierType = memberIdentifierType;
        }

        /// <summary>
        /// Gets or Sets Dataless
        /// </summary>
        public bool Dataless { get; set; }

        /// <summary>
        /// Gets or Sets HideRestrictedData
        /// </summary>
        public bool HideRestrictedData { get; set; }

        /// <summary>
        /// Gets or Sets CellAttributes
        /// </summary>
        public bool CellAttributes { get; set; }

        /// <summary>
        /// Gets or Sets FormatString
        /// </summary>
        public bool FormatString { get; set; }

        /// <summary>
        /// Gets or Sets FormatValues
        /// </summary>
        public bool FormatValues { get; set; }

        /// <summary>
        /// Gets or Sets MeaninglessCells
        /// </summary>
        public bool MeaninglessCells { get; set; }

        /// <summary>
        /// Gets or Sets TextList
        /// </summary>
        public bool TextList { get; set; }

        /// <summary>
        /// Gets or Sets UrlDrillThrough
        /// </summary>
        public bool UrlDrillThrough { get; set; }

        /// <summary>
        /// Gets or Sets Member identifier Type
        /// </summary>
        public EssMemberIdentifierType? MemberIdentifierType { get; set; } = null;
    }
}
