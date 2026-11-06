using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(AvailableContainerTests), DisableParallelization = true), CollectionPriority(1)]
    public class AvailableContainerTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Tests for container availability.
    /// </summary>
    [Collection(nameof(AvailableContainerTests)), Trait("type", "container")]
    public class AvailableContainerTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public AvailableContainerTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = "AvailableContainerTests - 01 - DatabaseContainer_AfterInitialization_IsNotNull"), Priority(01)]
        public void DatabaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Database);
        }

        [Fact(DisplayName = "AvailableContainerTests - 02 - DatabaseContainer_AfterInitialization_IsRunning"), Priority(02)]
        public async Task DatabaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the database container.
            var state = (await client.Containers.InspectContainerAsync(Database, TestContext.Current.CancellationToken))?.State;

            // Assert
            Assert.True(state?.Running);
        }

        [Fact(DisplayName = "AvailableContainerTests - 03 - EssbaseContainer_AfterInitialization_IsNotNull"), Priority(03)]
        public void EssbaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Essbase);
        }

        [Fact(DisplayName = "AvailableContainerTests - 04 - EssbaseContainer_AfterInitialization_IsRunning"), Priority(04)]
        public async Task EssbaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the essbase container.
            var state = (await client.Containers.InspectContainerAsync(Essbase, TestContext.Current.CancellationToken))?.State;

            // Assert
            Assert.True(state?.Running);
        }

        [Fact(DisplayName = "AvailableContainerTests - 05 - EssbaseContainer_StartedThroughTheWrapper_RunsTheManagedServerWithItsHeap"), Priority(05)]
        public async Task EssbaseContainer_StartedThroughTheWrapper_RunsTheManagedServerWithItsHeap()
        {
            // Get a docker client.
            using var client = GetClient();

            // The wrapper sets the heap; a container started some other way, such as one a developer started by hand, is not
            // the harness's to judge.
            var config = (await client.Containers.InspectContainerAsync(Essbase, TestContext.Current.CancellationToken))?.Config;

            Assert.SkipUnless(config?.Cmd?.Any(argument => argument.EndsWith("/wrapper.sh", StringComparison.Ordinal)) is true,
                $@"The Essbase container was not started through wrapper.sh (its command is ""{string.Join(" ", config?.Cmd ?? new List<string>())}"").");

            // The heap the wrapper gives Essbase's managed server: the profile's ESSBASE_MANAGED_SERVER_HEAP, or 4g.
            var expected = IntegrationTestFactory.EssbaseProfile.Environment.TryGetValue("ESSBASE_MANAGED_SERVER_HEAP", out var heap) && heap is { Length: > 0 } ? heap : "4g";

            // Read the command line of the managed server's JVM, which the domain starts some minutes after the container.
            var commandLine = string.Empty;
            var deadline    = DateTime.UtcNow.AddMinutes(10);

            while (DateTime.UtcNow < deadline)
            {
                var (_, stdout) = await ExecEssbaseAsync(@"for p in /proc/[0-9]*; do tr '\0' ' ' < $p/cmdline 2>/dev/null; echo; done | grep -- '-Dweblogic.Name=essbase_server1' | grep -v grep", TestContext.Current.CancellationToken);

                if ((commandLine = stdout?.Trim() ?? string.Empty).Length > 0)
                    break;

                await Task.Delay(TimeSpan.FromSeconds(15), TestContext.Current.CancellationToken);
            }

            Assert.False(commandLine.Length == 0, "Essbase's managed server (essbase_server1) did not start within 10 minutes.");

            // The JVM takes the last -Xmx it is given.
            var xmx = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault(argument => argument.StartsWith("-Xmx", StringComparison.Ordinal));

            Assert.Equal($@"-Xmx{expected}", xmx, ignoreCase: true);
        }

        [Fact(DisplayName = "AvailableContainerTests - 06 - EssbaseContainer_StartedThroughTheWrapper_GivesTheIdentityStoreItsPool"), Priority(06)]
        public async Task EssbaseContainer_StartedThroughTheWrapper_GivesTheIdentityStoreItsPool()
        {
            Assert.SkipWhen(await GetEssServer().GetSecurityModeAsync(TestContext.Current.CancellationToken) is EssSecurityMode.IdentityCloud, "A server in Identity Cloud mode has no adapter for the embedded LDAP, so it has no such pool.");

            // Get a docker client.
            using var client = GetClient();

            // The wrapper sets the pool; a container started some other way is not the harness's to judge.
            var config = (await client.Containers.InspectContainerAsync(Essbase, TestContext.Current.CancellationToken))?.Config;

            Assert.SkipUnless(config?.Cmd?.Any(argument => argument.EndsWith("/wrapper.sh", StringComparison.Ordinal)) is true,
                $@"The Essbase container was not started through wrapper.sh (its command is ""{string.Join(" ", config?.Cmd ?? new List<string>())}"").");

            // The pool the wrapper gives the managed server: the profile's ESSBASE_IDENTITY_POOL_SIZE, or 100.
            var expected = IntegrationTestFactory.EssbaseProfile.Environment.TryGetValue("ESSBASE_IDENTITY_POOL_SIZE", out var size) && size is { Length: > 0 } ? int.Parse(size) : 100;

            Assert.Equal(expected, await GetIdentityPoolSizeAsync(TestContext.Current.CancellationToken));
        }

    }
}
