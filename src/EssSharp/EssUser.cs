using System;
using System.Collections.Generic;
using System.Linq;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUser : EssObject, IEssUser
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly UserBean _user;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUser( UserBean user, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _user = user ?? 
                throw new ArgumentNullException(nameof(user), $"An API model {nameof(user)} is required to create an {nameof(EssUser)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssUser)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _user?.Id;

        /// <inheritdoc />
        public override EssType Type => EssType.User;

        #endregion

        #region IEssUser Properties

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string DisplayName => _user?.Name;

        /// <inheritdoc />
        public string Email => _user?.Email;

        /// <inheritdoc />
        public string Role => _user?.Role;

        /// <inheritdoc />
        public List<string> GroupNames => _user?.Groups;

        #endregion

        #region IEssUser Members


        #endregion
    }
}
