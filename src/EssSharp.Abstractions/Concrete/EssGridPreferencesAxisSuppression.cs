using System;

namespace EssSharp
{
    /// <summary />
    public class EssGridPreferencesAxisSuppression : IEquatable<EssGridPreferencesAxisSuppression>
    {
        #region Public Properties
        /// <summary />
        public bool Derived { get; set; }

        /// <summary />
        public bool EmptyBlocks { get; set; }

        /// <summary />
        public bool Error { get; set; }

        /// <summary />
        public bool Invalid { get; set; }

        /// <summary />
        public bool Missing { get; set; }

        /// <summary />
        public bool NoAccess { get; set; }

        /// <summary />
        public bool UnderScore { get; set; }

        /// <summary />
        public bool Zero { get; set; }

        #endregion

        #region IEquatable Members

        /// <inheritdoc />
        public bool Equals( EssGridPreferencesAxisSuppression other ) => Equals(other as object);

        #endregion

        #region IEquatable Member Support

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            // Return false if the other properties object is null or a different type.
            if ( obj is not EssGridPreferencesAxisSuppression other )
                return false;

            if ( Derived != other.Derived )
                return false;

            if ( EmptyBlocks != other.EmptyBlocks )
                return false;

            if ( Error != other.Error )
                return false;

            if ( Invalid != other.Invalid )
                return false;

            if ( Missing != other.Missing )
                return false;

            if ( NoAccess != other.NoAccess )
                return false;

            if ( UnderScore != other.UnderScore )
                return false;

            if ( Zero != other.Zero )
                return false;

            // Return true if the properties are the same.
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode() => (Derived, EmptyBlocks, Error, Invalid, Missing, NoAccess, UnderScore, Zero).GetHashCode();

        #endregion
    }
}
