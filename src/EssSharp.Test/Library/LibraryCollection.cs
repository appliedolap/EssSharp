using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// The hand-written tests of the EssSharp library. They run against loopback stubs, so they need no Essbase server,
    /// and they run one at a time because the client keeps some configuration (such as its retry policy) in static state.
    /// </summary>
    [CollectionDefinition(nameof(LibraryCollection), DisableParallelization = true)]
    public class LibraryCollection { }
}
