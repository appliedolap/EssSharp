using System;
using System.Collections.Generic;
using System.Data;
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

    public class EssGridPreferences : ICloneable, IEquatable<EssGridPreferences>
    {
        #region Public Properties

        public bool CellText { get; set; } = true;

        public EssGridPreferencesAxisSuppression ColumnSupression { get; set; } = new EssGridPreferencesAxisSuppression();

        public EssGridPreferencesFormulaRetention FormulaRetention { get; set; } = new EssGridPreferencesFormulaRetention();

        public bool IncludeDescriptionLabel { get; set; } = false;

        public bool IncludeSelection { get; set; } = true;

        public IndentationType Indentation { get; set; } = IndentationType.SUBITEMS;

        public long MaxColumns { get; set; } = 0;

        public long MaxRows { get; set; } = 0;

        public string MissingText { get; set; } = "#Missing";

        public bool Navigate { get; set; } = true;

        public string NoAccessText { get; set; } = "#No Access";

        public bool RemoveUnSelectedGroup { get; set; } = false;

        public bool RepeatMemberLabels { get; set; } = true;

        public EssGridPreferencesAxisSuppression RowSupression { get; set; } = new EssGridPreferencesAxisSuppression();

        public bool SendBlanksAsMissing { get; set; } = true;

        public bool WithinSelectedGroup { get; set; } = false;

        public EssGridPreferencesZoomIn ZoomIn { get; set; } = new EssGridPreferencesZoomIn();

        public bool UseAuditLog { get; set; } = false;

        #endregion

        #region ICloneable Members

        /// <inheritdoc />
        public object Clone() => new EssGridPreferences()
        {
            CellText                = this.CellText,
            ColumnSupression        = new EssGridPreferencesAxisSuppression()
            {
                Derived             = this.ColumnSupression.Derived,
                EmptyBlocks         = this.ColumnSupression.EmptyBlocks,
                Error               = this.ColumnSupression.Error,
                Invalid             = this.ColumnSupression.Invalid,
                Missing             = this.ColumnSupression.Missing,
                NoAccess            = this.ColumnSupression.NoAccess,
                UnderScore          = this.ColumnSupression.UnderScore,
                Zero                = this.ColumnSupression.Zero
            },
            FormulaRetention        = new EssGridPreferencesFormulaRetention()
            {
                Comments            = this.FormulaRetention.Comments,
                Fill                = this.FormulaRetention.Fill,
                Focus               = this.FormulaRetention.Focus,
                Retrieve            = this.FormulaRetention.Retrieve,
                Zoom                = this.FormulaRetention.Zoom
            },
            IncludeDescriptionLabel = this.IncludeDescriptionLabel,
            IncludeSelection        = this.IncludeSelection,
            Indentation             = this.Indentation,
            MaxColumns              = this.MaxColumns,
            MaxRows                 = this.MaxRows,
            MissingText             = this.MissingText,
            Navigate                = this.Navigate,
            NoAccessText            = this.NoAccessText,
            RemoveUnSelectedGroup   = this.RemoveUnSelectedGroup,
            RepeatMemberLabels      = this.RepeatMemberLabels,
            RowSupression           = new EssGridPreferencesAxisSuppression()
            {
                Derived             = this.RowSupression.Derived,
                EmptyBlocks         = this.RowSupression.EmptyBlocks,
                Error               = this.RowSupression.Error,
                Invalid             = this.RowSupression.Invalid,
                Missing             = this.RowSupression.Missing,
                NoAccess            = this.RowSupression.NoAccess,
                UnderScore          = this.RowSupression.UnderScore,
                Zero                = this.RowSupression.Zero
            },
            SendBlanksAsMissing     = this.SendBlanksAsMissing,
            WithinSelectedGroup     = this.WithinSelectedGroup,
            ZoomIn                  = new EssGridPreferencesZoomIn()
            {
                Ancestor            = this.ZoomIn.Ancestor,
                Mode                = this.ZoomIn.Mode
            }
        };

        #endregion

        #region IEquatable Members

        /// <inheritdoc />
        public bool Equals( EssGridPreferences other ) => Equals(other as object);

        #endregion

        #region IEquatable Member Support

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            // Return false if the other properties object is null or a different type.
            if ( obj is not EssGridPreferences other )
                return false;

            if ( CellText != other.CellText )
                return false;

            if ( !ColumnSupression.Equals(other.ColumnSupression) )
                return false;

            if ( !FormulaRetention.Equals(other.FormulaRetention) )
                return false;

            if ( IncludeDescriptionLabel != other.IncludeDescriptionLabel )
                return false;

            if ( IncludeSelection != other.IncludeSelection )
                return false;

            if ( Indentation != other.Indentation )
                return false;

            if ( MaxColumns != other.MaxColumns )
                return false;

            if ( MaxRows != other.MaxRows )
                return false;

            if ( !string.Equals(MissingText, other.MissingText, StringComparison.Ordinal) )
                return false;

            if ( Navigate != other.Navigate )
                return false;

            if ( !string.Equals(NoAccessText, other.NoAccessText, StringComparison.Ordinal) )
                return false;

            if ( RemoveUnSelectedGroup != other.RemoveUnSelectedGroup )
                return false;

            if ( RepeatMemberLabels != other.RepeatMemberLabels )
                return false;

            if ( !RowSupression.Equals(other.RowSupression) )
                return false;

            if ( SendBlanksAsMissing != other.SendBlanksAsMissing )
                return false;

            if ( WithinSelectedGroup != other.WithinSelectedGroup )
                return false;

            if ( !ZoomIn.Equals(other.ZoomIn) )
                return false;

            // Return true if the properties are the same.
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() => 
        (
            CellText, 
            ColumnSupression,
            FormulaRetention,
            IncludeDescriptionLabel, 
            IncludeSelection,
            Indentation,
            MaxColumns, 
            MaxRows, 
            MissingText, 
            Navigate, 
            NoAccessText, 
            RemoveUnSelectedGroup, 
            RepeatMemberLabels,
            RowSupression,
            SendBlanksAsMissing,
            WithinSelectedGroup,
            ZoomIn
        ).GetHashCode();

        #endregion
    }
}
