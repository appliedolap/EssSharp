using System;

namespace EssSharp
{
    public enum ZoomInAncestor
    {
        UNKNOWN = 0,

        TOP = 1,

        BOTTOM = 2
    };

    public enum ZoomInMode
    {
        UNKNOWN = 0,

        CHILDREN = 1,

        DESCENDENTS = 2,

        BASE = 3
    };

    public class EssGridPreferencesZoomIn : IEquatable<EssGridPreferencesZoomIn>
    {
        #region Public Properties

        public ZoomInAncestor Ancestor { get; set; } = ZoomInAncestor.TOP;

        public ZoomInMode Mode { get; set; } = ZoomInMode.CHILDREN;

        #endregion

        #region IEquatable Members

        /// <inheritdoc />
        public bool Equals( EssGridPreferencesZoomIn other ) => Equals(other as object);

        #endregion

        #region IEquatable Member Support

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            // Return false if the other properties object is null or a different type.
            if ( obj is not EssGridPreferencesZoomIn other )
                return false;

            if ( Ancestor != other.Ancestor )
                return false;

            if ( Mode != other.Mode )
                return false;

            // Return true if the properties are the same.
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() => (Ancestor, Mode).GetHashCode();

        #endregion
    }
}
