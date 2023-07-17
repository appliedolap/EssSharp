using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
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
        public string FullName => _user?.Name;

        /// <inheritdoc />
        public string Email => _user?.Email;

        /// <inheritdoc />
        public string Role => _user?.Role;

        /// <inheritdoc />
        public List<string> GroupNames => _user?.Groups;

        #endregion

        #region IEssUser Members

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UsersApi>();

                await api.UsersDeleteAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to delete user ""{Name}"". {e.Message}", e);
            }
        }


        #endregion
    }
}
