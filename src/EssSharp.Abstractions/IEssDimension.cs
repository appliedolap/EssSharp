using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDimension
    {
        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the deminsion type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets deminsion members.
        /// </summary>
        public int Members { get; }

        /// <summary>
        /// Gets stored deminsion members.
        /// </summary>
        public int StoredMembers { get; }

}
}
