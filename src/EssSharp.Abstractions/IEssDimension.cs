﻿using System.Threading.Tasks;
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
        /// 
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Members { get; }

        /// <summary>
        /// 
        /// </summary>
        public int StoredMembers { get; }

}
}
