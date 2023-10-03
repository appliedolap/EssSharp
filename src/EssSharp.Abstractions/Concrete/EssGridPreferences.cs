using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EssSharp
{
    /// <summary>
    /// Defines Indentation
    /// </summary>
    public enum IndentationType
    {
        UNKNOWN = 0,
        
        NONE = 1,

        SUBITEMS = 2,

        TOTALS = 3

    }

    public class EssGridPreferences
    {
        public bool CellText { get; set; }

        public EssGridPreferencesAxisSuppression ColumnSupression { get; set; }

        public EssGridPreferencesFormulaRetention FormulaRetention { get; set; }

        public bool IncludeDescriptionLabel { get; set; }

        public IndentationType Indentation { get; set; }

        public long MaxColumns { get; set; }

        public long MaxRows { get; set; }

        public string MissingText { get; set; }

        public bool Navigate { get; set; }

        public string NoAccessText { get; set; }

        public bool RemoveUnSelectedGroup { get; set; }

        public bool RepeatMemberLabels { get; set; }

        public EssGridPreferencesAxisSuppression RowSupression { get; set; }

        public bool WithinSelectedGroup { get; set; }

        public EssGridPreferencesZoomIn ZoomIn { get; set; }
    }
}
