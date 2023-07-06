using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

// Set the default collection orderer.
[assembly: TestCollectionOrderer("EssSharp.Integration.Setup.TestCollectionOrderer", "EssSharp.Integration")]

// Set the default (test) case orderer.
[assembly: TestCaseOrderer($@"EssSharp.Integration.Setup.TestPriorityOrderer", "EssSharp.Integration")]

// Turn off test parallelization to enforce case ordering.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace EssSharp.Integration.Setup
{
    [CollectionDefinition("EssSharp Integration Tests")]
    public class TestsCollection : ICollectionFixture<CollectionFixture> { }

    public class CollectionFixture : IDisposable
    {
        public CollectionFixture( IMessageSink sink )
        {
            IConfigurationRoot config = null;

            try
            {
                // Attempt to build a configuration around a local settings file.
                config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.local.json")
                    .Build();
            }
            catch ( FileNotFoundException )
            {
                // Swallow a FileNotFoundException, which occurs when a local configuration does not exist.
            }

            try
            {
                // Attempt to build a configuration around the default settings file,
                // if a local configuration was not available.
                config ??= new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
            catch ( FileNotFoundException )
            {
                // Swallow a FileNotFoundException, which occurs when the default settings file does not exist.
            }

            // If connections could be obtained from the configuration, make them available to the EssServerFactory.
            if ( config?.GetSection("Settings")?.Get<IntegrationTestSettings>().Connections is { Length: > 0 } connections )
                IntegrationTestFactory.Connections = connections;
            else
            {
                IntegrationTestFactory.Connections = new IntegrationTestSettingsConnection[]
                {
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "admin",
                        Password = "welcome1",
                        Role     = Role.ServiceAdministrator
                    },
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "poweruser",
                        Password = "welcome2",
                        Role     = Role.PowerUser
                    },
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "user",
                        Password = "welcome3",
                        Role     = Role.User
                    }
                };
            }

            // Do "global" initialization here; Only called once.
            var databaseTask = IntegrationTestFactory.InitializeDatabaseContainerAsync(sink);
            var essbaseTask  = IntegrationTestFactory.InitializeEssbaseContainerAsync(sink);

            Task.WhenAll(databaseTask, essbaseTask).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
            IntegrationTestFactory.DisposeAsync().GetAwaiter().GetResult();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionPriorityAttribute : Attribute
    {
        public CollectionPriorityAttribute( int priority )
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute( int priority )
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }

    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections( IEnumerable<ITestCollection> testCollections )
        {
            var sortedCollections = new SortedDictionary<int, List<ITestCollection>>();

            foreach ( ITestCollection testCollection in testCollections )
            {
                int priority = 0;

                foreach ( IAttributeInfo attr in testCollection.CollectionDefinition.GetCustomAttributes(typeof(CollectionPriorityAttribute).AssemblyQualifiedName) )
                    priority = attr.GetNamedArgument<int>("Priority");

                GetOrCreate(sortedCollections, priority).Add(testCollection);
            }

            foreach ( var list in sortedCollections.Keys.Select(priority => sortedCollections[priority]) )
            {
                list.Sort(( x, y ) => StringComparer.OrdinalIgnoreCase.Compare(x.CollectionDefinition.Name, y.CollectionDefinition.Name));
                foreach ( ITestCollection testCollection in list ) yield return testCollection;
            }
        }

        static TValue GetOrCreate<TKey, TValue>( IDictionary<TKey, TValue> dictionary, TKey key )
            where TValue : new()
        {
            TValue result;

            if ( dictionary.TryGetValue(key, out result) ) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }

    public class TestPriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>( IEnumerable<TTestCase> testCases ) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach ( TTestCase testCase in testCases )
            {
                int priority = 0;

                foreach ( IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(PriorityAttribute).AssemblyQualifiedName)) )
                    priority = attr.GetNamedArgument<int>("Priority");

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach ( var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]) )
            {
                list.Sort(( x, y ) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach ( TTestCase testCase in list ) yield return testCase;

            }
        }

        static TValue GetOrCreate<TKey, TValue>( IDictionary<TKey, TValue> dictionary, TKey key )
            where TValue : new()
        {
            TValue result;

            if ( dictionary.TryGetValue(key, out result) ) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }

    public class IntegrationTestBase
    {
        /// <summary />
        protected string Database => IntegrationTestFactory.DatabaseContainerId;

        /// <summary />
        protected string Essbase => IntegrationTestFactory.EssbaseContainerId;

        /// <summary />
        /// <param name="id" />
        /// <param name="command" />
        /// <param name="cancellationToken" />
        protected async Task<(ContainerExecInspectResponse details, string stdout)> ExecAsync( string id, string[] command, CancellationToken cancellationToken = default )
        {
            var execParams = new ContainerExecCreateParameters()
            {
                AttachStderr = true,
                AttachStdout = true,
                Cmd = command,
            };

            using var client = GetClient();

            // Create the exec instance, it is not started yet.
            var exec = await client.Exec.ExecCreateContainerAsync(id, execParams, cancellationToken);
            // Start the exec instance and capture the output stream.
            using var stream = await client.Exec.StartAndAttachContainerExecAsync(exec.ID, false, cancellationToken);
            var (stdout, stderr) = await stream.ReadOutputToEndAsync(cancellationToken);

            var details = await client.Exec.InspectContainerExecAsync(exec.ID, cancellationToken);

            return (details, stdout);
        }

        /// <summary />
        protected DockerClient GetClient() => IntegrationTestFactory.GetDockerClient();

        /// <summary />
        protected IntegrationTestSettingsConnection GetEssConnection() => GetEssConnection(Role.ServiceAdministrator);

        /// <summary />
        protected IntegrationTestSettingsConnection GetEssConnection( Role role ) => IntegrationTestFactory.GetEssConnection(role);

        /// <summary />
        protected IEssServer GetEssServer() => GetEssServer(Role.ServiceAdministrator);

        /// <summary />
        /// <param name="role" />
        protected IEssServer GetEssServer( Role role ) => IntegrationTestFactory.GetEssServer(role);
    }
}
