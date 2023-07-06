using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssLockOptions
    {
        public EssLockOptions( string fileName, EssLockedFileType? lockFileType )
        {
            LockObjectName = fileName ??
                throw new ArgumentException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(IEssFile)} object.");

            LockedFileType = lockFileType ??
                throw new ArgumentNullException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(EssLockedFileType)} object.");
        }

        public EssLockOptions(IEssFile lockObject, EssLockedFileType? lockFileType) 
        {
            LockObjectName = lockObject?.Name ??
                throw new ArgumentException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(IEssFile)} object.");

            LockedFileType = lockFileType??
                throw new ArgumentNullException($@"This {nameof(EssLockOptions)} constructor requires an {nameof(EssLockedFileType)} object.");
        }

        public string LockObjectName { get; }

        public EssLockedFileType? LockedFileType { get; }
    }
}
 