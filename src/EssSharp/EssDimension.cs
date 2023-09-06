using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDimension : IEssDimension
    {
        #region Private Data

        private readonly EssCube       _cube;
        private readonly DimensionBean _dimension;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDimension( DimensionBean dimension, EssCube cube )
        {
            _dimension = dimension ??
                throw new ArgumentNullException(nameof(dimension), $"An API model {nameof(dimension)} is required to create an {nameof(EssDimension)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region IEssDimensionMembers

        /// <inheritdoc />
        public string Name => _dimension?.Name;

        /// <inheritdoc />
        public string Type => _dimension?.Type;

        /// <inheritdoc />
        public int MemberCount => _dimension.Members;

        /// <inheritdoc />
        public int StoredMemberCount => _dimension.StoredMembers;

        #endregion

        /// <inheritdoc />
        /// <returns>A List of <see cref="string"/> of Member Names.</returns>
        public List<string> GetMembers() => GetMembersAsync().GetAwaiter().GetResult();

        /// TODO: finish implementation 
        /// <inheritdoc />
        /// <returns>A List of <see cref="string"/> of Member Names.</returns>
        public async Task<List<string>> GetMembersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                return null;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of members from dimension ""{Name}"". {e.Message}", e);
            }
        }
    }
}
