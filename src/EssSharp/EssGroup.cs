using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssGroup : EssObject, IEssGroup
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly GroupBean _group;

        #endregion

        #region Constructors

        /// <summary />
        internal EssGroup( GroupBean group, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _group = group ?? 
                throw new ArgumentNullException(nameof(group), $"An API model {nameof(group)} is required to create an {nameof(EssGroup)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssGroup)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _group?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Group;

        #endregion

        #region IEssServer Properties

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string Description => _group?.Description;

        /// <inheritdoc />
        public List<string> Groups => _group?.Groups;

        /// <inheritdoc />
        public string Role => _group?.Role;

        #endregion

        #region IEssGroup Members

        /// <inheritdoc />
        public List<IEssUser> GetUsers() => GetUsersAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                if (await api.GroupsGetUserMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } users )
                    throw new Exception("Could not get members of group.");

                return users?.ToEssSharpList(Server as EssServer) ?? new List<IEssUser>();
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get members of group: ""{Name}"". {e.Message}", e);
            }
        }
        
        /// <inheritdoc />
        /// <returns> a List of <see cref="IEssGroup"/> objects. </returns>
        public List<IEssGroup> GetGroups() => GetGroupsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <param name="cancellationToken"></param>
        /// <returns>A Task<list> of <see cref="IEssGroup"/> objects. </list></returns>
        public async Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();
            if ( await api.GroupsGetGroupMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groups )
                throw new Exception("Could not get groups.");

            return groups?.ToEssSharpList(Server as EssServer) ?? new List<IEssGroup>();
        }


        #endregion
    }
}
