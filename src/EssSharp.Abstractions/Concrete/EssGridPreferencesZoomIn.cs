using System;

namespace EssSharp
{
    /// <summary>
    /// Defines ZoomInAncestor
    /// </summary>
    public enum ZoomInAncestor
    {
        /// <summary>
        /// Enum value for UNKNOWN
        /// </summary>
        UNKNOWN = 0,
        /// <summary>
        /// Enum value for TOP
        /// </summary>
        TOP = 1,
        /// <summary>
        /// Enum value for BOTTOM
        /// </summary>
        BOTTOM = 2
    };

    /// <summary>
    /// Defines ZoomInMode
    /// </summary>
    public enum ZoomInMode
    {
        /// <summary>
        /// Enum value for UNKNOWN
        /// </summary>
        UNKNOWN = 0,
        /// <summary>
        /// Enum value for CHILDREN
        /// </summary>
        CHILDREN = 1,
        /// <summary>
        /// Enum value for DESCENDANTS
        /// </summary>
        DESCENDENTS = 2,
        /// <summary>
        /// Enum value for BASE
        /// </summary>
        BASE = 3
    };

    /// <summary />
    public class EssGridPreferencesZoomIn : IEquatable<EssGridPreferencesZoomIn>
    {
        #region Public Properties

        /// <inheritdoc />
        public ZoomInAncestor Ancestor { get; set; } = ZoomInAncestor.TOP;

        /// <inheritdoc />
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
