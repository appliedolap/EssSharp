using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssLockOptions
    {
        /// <summary />
        /// <param name="fileName"></param>
        /// <param name="lockFileType"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public EssLockOptions( string fileName, EssLockedFileType? lockFileType )
        {
            LockObjectName = fileName ??
                throw new ArgumentException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(IEssFile)} object.");

            LockedFileType = lockFileType ??
                throw new ArgumentNullException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(EssLockedFileType)} object.");
        }

        /// <summary />
        /// <param name="lockObject"></param>
        /// <param name="lockFileType"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public EssLockOptions(IEssFile lockObject, EssLockedFileType? lockFileType) 
        {
            LockObjectName = lockObject?.Name ??
                throw new ArgumentException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(IEssFile)} object.");

            LockedFileType = lockFileType??
                throw new ArgumentNullException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(EssLockedFileType)} object.");
        }

        /// <inheritdoc />
        public string LockObjectName { get; }
        /// <inheritdoc />
        public EssLockedFileType? LockedFileType { get; }
    }
}
 