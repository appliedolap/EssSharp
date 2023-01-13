using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a user session for a given server.
    /// </summary>
    public class EssUserSession : IEssUserSession
    {
        #region Private Data

        private readonly UserBean _user;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUserSession( UserBean user )
        {
            _user = user ??
                throw new ArgumentNullException(nameof(user), $"An API model {nameof(user)} is required to create an {nameof(EssUserSession)}.");
        }

        #endregion

        #region IEssUserSession Members

        /// <inheritdoc />
        public virtual string Token  => _user.Token;

        /// <inheritdoc />
        public virtual string UserId => _user.Id;

        #endregion
    }
}
