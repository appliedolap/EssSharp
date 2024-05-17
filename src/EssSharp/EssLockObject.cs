using System;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssLockObject : EssLock, IEssLockObject
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly LockObject _lockObject;

        #endregion

        #region Constructors

        /// <summary />
        internal EssLockObject( LockObject lockObject, EssCube cube ) : base( lockObject,  cube )
        {
            _lockObject = lockObject ?? 
                throw new ArgumentNullException(nameof(lockObject), $"An API model {nameof(lockObject)} is required to create an {nameof(EssLock)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssLock)}.");
        }

        #endregion

        #region IEssObject Members Properties

        /// <inheritdoc />
        public override string Name => _lockObject?.Name;

        #endregion

        #region IEssLock Members Properties

        /// <inheritdoc />
        public EssLockedFileType LockedFileType => Enum.IsDefined(typeof(EssLockedFileType), (EssLockedFileType) _lockObject.Type) ? (EssLockedFileType) _lockObject?.Type : EssLockedFileType.UNKNOWN;

        /// <inheritdoc />
        public override EssLockType LockType => EssLockType.Object;

        /// <inheritdoc />
        public string User => _lockObject?.User;

        /// <inheritdoc />
        public DateTime Time => DateTimeOffset.FromUnixTimeMilliseconds(_lockObject.Time).DateTime;

        #endregion

        #region IEssLock Methods 

        #endregion

    }
}
