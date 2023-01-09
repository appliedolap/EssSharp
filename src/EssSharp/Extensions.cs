using System.Collections.Generic;
using System.Linq;

using EssSharp.Model;

namespace EssSharp
{
    internal static class Extensions
    {
        /// <summary>
        /// Returns an <see cref="List{T}"/> of <see cref="IEssApplication"/> objects associated with the given <see cref="EssServer"/>.
        /// </summary>
        /// <param name="applicationList" />
        /// <param name="server" />
        internal static List<IEssApplication> ToEssApplicationList(this ApplicationList applicationList, EssServer server) =>
            applicationList?
                .Items?
                .Select(application => new EssApplication(server, application) as IEssApplication)?
                .ToList() ?? new List<IEssApplication>();
    }
}
