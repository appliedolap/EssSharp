using System;
using System.ComponentModel.DataAnnotations;

namespace EssSharp.Model
{
    /// <summary />
    public partial class MemberBean : IEquatable<MemberBean>, IValidatableObject
    {
        /// <summary>
        /// Whether the member is a shared member.
        /// </summary>
        public bool IsSharedMember => DataStorageType?.StartsWith("SHARED", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
