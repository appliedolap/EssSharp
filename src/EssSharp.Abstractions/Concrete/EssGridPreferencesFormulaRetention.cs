using System;

namespace EssSharp
{
    public class EssGridPreferencesFormulaRetention : IEquatable<EssGridPreferencesFormulaRetention>
    {
        #region Public Properties

        public bool Comments { get; set; }

        public bool Fill { get; set; }

        public bool Focus { get; set; }

        public bool Retrieve { get; set; }

        public bool Zoom { get; set; }

        #endregion

        #region IEquatable Members

        /// <inheritdoc />
        public bool Equals( EssGridPreferencesFormulaRetention other ) => Equals(other as object);

        #endregion

        #region IEquatable Member Support

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            // Return false if the other properties object is null or a different type.
            if ( obj is not EssGridPreferencesFormulaRetention other )
                return false;

            if ( Comments != other.Comments )
                return false;

            if ( Fill != other.Fill )
                return false;

            if ( Focus != other.Focus )
                return false;

            if ( Retrieve != other.Retrieve )
                return false;

            if ( Zoom != other.Zoom )
                return false;

            // Return true if the properties are the same.
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() => (Comments, Fill, Focus, Retrieve, Zoom).GetHashCode();

        #endregion
    }
}
