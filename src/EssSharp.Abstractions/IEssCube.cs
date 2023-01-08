﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssCube : IEssObject
    {
        /// <summary>
        /// Gets the list of cube-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssVariable"/> objects.</returns>
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );
    }
}
