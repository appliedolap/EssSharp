using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplication : IEssObject
    {
        public Task StartAsync( CancellationToken cancellationToken );
        public Task StopAsync( CancellationToken cancellationToken );

    }
}
