using System;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;
using static System.Net.Mime.MediaTypeNames;

namespace EssSharp
{
    /// <summary />
    public class EssLock : EssObject, IEssLock
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly LockBlock _lockBlock = null;
        private readonly LockObject _lockObject = null;

        #endregion

        #region Constructors

        /// <summary />
        internal EssLock( LockBlock lockBlock, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _lockBlock = lockBlock ??
                throw new ArgumentException(nameof(lockBlock), $"An API model {nameof(lockBlock)} is required to create an {nameof(EssLock)}."); 
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssLock)}.");
        }

        /// <summary />
        internal EssLock( LockObject lockObject, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _lockObject= lockObject ??
                throw new ArgumentException(nameof(lockObject), $"An API model {nameof(lockObject)} is required to create an {nameof(EssLock)}.");
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssLock)}.");
        }

        #endregion

        #region IEssObject Members Properties

        /// <inheritdoc />
        public override EssType Type => EssType.Lock;

        #endregion

        #region IEssLock Members Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual EssLockType LockType { get; } = EssLockType.Unknown;

        #endregion

        #region IEssLock Methods 

        /// <inheritdoc />
        public void Unlock() => UnlockAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task UnlockAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<LocksApi>();

                if ( Equals(LockType, EssLockType.Block) )
                    await api.LocksUnLockBlockAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: _lockBlock, cancellationToken: cancellationToken).ConfigureAwait(false);
                else if ( Equals(LockType, EssLockType.Object) )
                    await api.LocksUnLockObjectAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: _lockObject, cancellationToken: cancellationToken).ConfigureAwait(false);
                else
                    throw new Exception($@"Unable to unlock {(Equals(LockType, EssLockType.Object) ? "object" : @"block")}.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unlock { (Equals(LockType, EssLockType.Object) ? "object" : @"block") } on cube ""{Name}"". {e.Message}", e);
            }
        }

        #endregion

    }
}
