using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssCube : IEssObject
    {
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken );
    }
}
