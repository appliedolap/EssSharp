using System;
using EssSharp.Model;
using static System.Net.Mime.MediaTypeNames;

namespace EssSharp
{
    /// <summary />
    public class EssLock : EssObject, IEssLock
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly LockObject _lock;

        #endregion

        #region Constructors

        /// <summary />
        internal EssLock( LockObject lockObject, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _lock = lockObject ?? 
                throw new ArgumentNullException(nameof(lockObject), $"An API model {nameof(lockObject)} is required to create an {nameof(EssLock)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssLock)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _lock?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Lock;

        #endregion

        #region IEssLock Members

        /// <inheritdoc />
        public EssLockType LockType => Enum.IsDefined(typeof(EssLockType), _lock.Type) ? (EssLockType) _lock?.Type : EssLockType.UNKNOWN;

        /// <inheritdoc />
        public string User => _lock?.User;

        /// <inheritdoc />
        public DateTime Time => DateTimeOffset.FromUnixTimeMilliseconds(_lock.Time).DateTime;

        #endregion

    }
}
