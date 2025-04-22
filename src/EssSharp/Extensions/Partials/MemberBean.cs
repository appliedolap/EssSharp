using System;

namespace EssSharp.Model
{
    /// <summary />
    public partial class MemberBean
    {
        /// <summary>
        /// Whether the member is a shared member.
        /// </summary>
        public bool IsSharedMember => DataStorageType?.StartsWith("SHARED", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
