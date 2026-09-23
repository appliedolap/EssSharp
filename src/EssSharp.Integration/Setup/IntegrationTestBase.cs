using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;
using Xunit.v3;

// Create the containers once for the whole test assembly (xUnit.net v3 supports assembly fixtures natively).
[assembly: AssemblyFixture(typeof(EssSharp.Integration.Setup.AssemblyFixture))]

// Set the default collection orderer.
[assembly: TestCollectionOrderer(typeof(EssSharp.Integration.Setup.TestCollectionOrderer))]

// Set the default (test) case orderer.
[assembly: TestCaseOrderer(typeof(EssSharp.Integration.Setup.TestPriorityOrderer))]

// Set the default (test) method and class orderers; since xUnit.net v3 4.0 tests are ordered collection, class, method, case,
// so the priority must be applied at the method level and the class order must be deterministic.
[assembly: TestMethodOrderer(typeof(EssSharp.Integration.Setup.TestPriorityMethodOrderer))]
[assembly: TestClassOrderer(typeof(EssSharp.Integration.Setup.TestNameClassOrderer))]

// Turn off test parallelization to enforce case ordering.
[assembly: Parallelization(Mode = ParallelMode.None)]

namespace EssSharp.Integration.Setup
{
    /// <summary />
    /// <param name="messageSink" />
    public class AssemblyFixture(IMessageSink messageSink) : IAsyncLifetime
    {
        private readonly IMessageSink _messageSink = messageSink;

        /// <inheritdoc />
        public async ValueTask InitializeAsync()
        {
            var localSettings   = default(IntegrationTestSettings);
            var defaultSettings = default(IntegrationTestSettings);

            try
            {
                // Attempt to build a configuration around and get the local settings.
                localSettings = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.local.json")
                    .Build()
                    .GetSection("Settings")
                    .Get<IntegrationTestSettings>();
            }
            catch (FileNotFoundException)
            {
                // Swallow a FileNotFoundException, which occurs when a local configuration does not exist.
            }

            try
            {
                // Attempt to build a configuration around and get the default settings.
                defaultSettings = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("Settings")
                    .Get<IntegrationTestSettings>();
            }
            catch (FileNotFoundException)
            {
                // Swallow a FileNotFoundException, which occurs when the default settings file does not exist.
            }

            // If connections could be obtained from either configuration, make them available to the EssServerFactory.
            if (localSettings?.Connections is { Length: > 0 } localConnections)
                IntegrationTestFactory.Connections = localConnections;
            else if (defaultSettings?.Connections is { Length: > 0 } defaultConnections)
                IntegrationTestFactory.Connections = defaultConnections;
            else
            {
                IntegrationTestFactory.Connections = new IntegrationTestSettingsConnection[]
                {
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "admin",
                        Password = "welcome1",
                        Role     = EssServerRole.ServiceAdministrator
                    },
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "poweruser",
                        Password = "welcome2",
                        Role     = EssServerRole.PowerUser
                    },
                    new IntegrationTestSettingsConnection()
                    {
                        Server   = "http://localhost:9000/essbase",
                        Username = "user",
                        Password = "welcome3",
                        Role     = EssServerRole.User
                    }
                };
            }

            // If an images list could be obtained from either configuration, make them available to the EssServerFactory.
            if (localSettings?.Images is { Length: > 0 } localImages)
                IntegrationTestFactory.Images = localImages;
            else if (defaultSettings?.Images is { Length: > 0 } defaultImages)
                IntegrationTestFactory.Images = defaultImages;
            else
            {
                IntegrationTestFactory.Images = new[]
                {
                    "appliedolap/essbase:21.7.0"
                };
            }

            // Do "global" initialization here; Only called once.
            var databaseTask = IntegrationTestFactory.InitializeDatabaseContainerAsync(_messageSink);
            var essbaseTask  = IntegrationTestFactory.InitializeEssbaseContainerAsync(_messageSink);

            await Task.WhenAll(databaseTask, essbaseTask).ConfigureAwait(false);
        }

        /// <summary>
        /// Do "global" teardown here; Only called once.
        /// </summary>
        public async ValueTask DisposeAsync() => await IntegrationTestFactory.DisposeAsync().ConfigureAwait(false);

        /// <summary>
        /// Do "global" teardown here; Only called once.
        /// </summary>
        //public void Dispose() => DisposeAsync().GetAwaiter().GetResult();
    }


    /// <summary />
    /// <param name="messageSink" />
    public class CollectionFixture(IMessageSink messageSink) : IAsyncLifetime
    {
        /// <summary />
        private readonly IMessageSink _messageSink = messageSink;

        /// <inheritdoc />
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        /// <inheritdoc />
        public ValueTask InitializeAsync() => ValueTask.CompletedTask;
    }

    /// <summary />
    /// <param name="priority" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionPriorityAttribute( int priority ) : Attribute
    {
        /// <summary />
        public int Priority { get; private set; } = priority;
    }

    /// <summary />
    /// <param name="priority" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute( int priority ) : Attribute
    {
        /// <summary />
        public int Priority { get; private set; } = priority;
    }

    /// <summary>
    /// Orders test collections by their <see cref="CollectionPriorityAttribute" />, then by collection name.
    /// </summary>
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        /// <inheritdoc />
        public IReadOnlyCollection<TTestCollection> OrderTestCollections<TTestCollection>( IReadOnlyCollection<TTestCollection> testCollections )
            where TTestCollection : ITestCollection
        {
            return testCollections
                .OrderBy(testCollection => GetCollectionDefinition(testCollection)?.GetCustomAttribute<CollectionPriorityAttribute>(false)?.Priority ?? 0)
                .ThenBy(testCollection => GetCollectionDefinition(testCollection)?.Name ?? testCollection.TestCollectionDisplayName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static Type GetCollectionDefinition( ITestCollection testCollection ) => (testCollection as IXunitTestCollection)?.CollectionDefinition;
    }

    /// <summary>
    /// Orders the test cases of a single test method (e.g., theory data rows) by method name; since xUnit.net v3 4.0,
    /// cross-method ordering is applied by <see cref="TestPriorityMethodOrderer" />.
    /// </summary>
    public class TestPriorityOrderer : ITestCaseOrderer
    {
        /// <inheritdoc />
        public IReadOnlyCollection<TTestCase> OrderTestCases<TTestCase>( IReadOnlyCollection<TTestCase> testCases )
            where TTestCase : notnull, ITestCase
        {
            return testCases
                .OrderBy(testCase => (testCase as IXunitTestCase)?.TestMethod.Method.GetCustomAttribute<PriorityAttribute>(false)?.Priority ?? 0)
                .ThenBy(testCase => testCase.TestMethod?.MethodName ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }

    /// <summary>
    /// Orders the test methods of a test class by their <see cref="PriorityAttribute" />, then by method name.
    /// </summary>
    public class TestPriorityMethodOrderer : ITestMethodOrderer
    {
        /// <inheritdoc />
        public IReadOnlyCollection<TTestMethod> OrderTestMethods<TTestMethod>( IReadOnlyCollection<TTestMethod> testMethods )
            where TTestMethod : notnull, ITestMethod
        {
            return testMethods
                .OrderBy(testMethod => (testMethod as IXunitTestMethod)?.Method.GetCustomAttribute<PriorityAttribute>(false)?.Priority ?? 0)
                .ThenBy(testMethod => testMethod.MethodName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }

    /// <summary>
    /// Orders the test classes of a test collection by class name, so the order is deterministic.
    /// </summary>
    public class TestNameClassOrderer : ITestClassOrderer
    {
        /// <inheritdoc />
        public IReadOnlyCollection<TTestClass> OrderTestClasses<TTestClass>( IReadOnlyCollection<TTestClass> testClasses )
            where TTestClass : notnull, ITestClass
        {
            return testClasses
                .OrderBy(testClass => testClass.TestClassName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }

    /// <summary />
    /// <param name="outputHelper" />
    public class IntegrationTestBase( ITestOutputHelper outputHelper )
    {
        private ITestOutputHelper _outputHelper = outputHelper;
        private TestOutputLogger  _outputLogger;

        /// <summary />
        protected static string Database => IntegrationTestFactory.DatabaseContainerId;

        /// <summary />
        protected static string Essbase => IntegrationTestFactory.EssbaseContainerId;

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
            var exec = await client.Exec.CreateContainerExecAsync(id, execParams, cancellationToken);
            // Start the exec instance and capture the output stream.
            using var stream = await client.Exec.StartContainerExecAsync(exec.ID, new ContainerExecStartParameters { Detach = false }, cancellationToken);
            var (stdout, stderr) = await stream.ReadOutputToEndAsync(cancellationToken);

            var details = await client.Exec.InspectContainerExecAsync(exec.ID, cancellationToken);

            return (details, stdout);
        }

        /// <summary />
        protected DockerClient GetClient() => IntegrationTestFactory.GetDockerClient();

        /// <summary />
        /// <param name="role" />
        protected IntegrationTestSettingsConnection GetEssConnection( EssServerRole role = EssServerRole.ServiceAdministrator ) => IntegrationTestFactory.GetEssConnection(role);

        /// <summary />
        /// <param name="role" />
        /// <param name="factory" />
        protected IEssServer GetEssServer( EssServerRole role = EssServerRole.ServiceAdministrator, EssServerFactory factory = null ) => IntegrationTestFactory.GetEssServer(role, factory);

        /// <summary />
        /// <param name="essConnection" />
        /// <param name="factory" />
        protected IEssServer GetEssServer( IntegrationTestSettingsConnection essConnection, EssServerFactory factory = null ) => IntegrationTestFactory.GetEssServer(essConnection, factory);

        /// <summary />
        protected TestOutputLogger OutputLogger => _outputLogger ??= new TestOutputLogger(_outputHelper);
    }

    /// <summary />
    /// <param name="outputDirectory" />
    public class FileOutputLogger( DirectoryInfo outputDirectory ) : ILogger
    {
        private readonly DirectoryInfo _outputDirectory = outputDirectory;

        /// <inheritdoc />
        public IDisposable BeginScope<TState>( TState state ) => null;

        /// <inheritdoc />
        public bool IsEnabled( LogLevel logLevel ) => true;

        /// <inheritdoc />
        void ILogger.Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
        {
            if ( eventId.Id is not ((int)EssSharpLogEventType.Request or (int)EssSharpLogEventType.Response) )
                return;

            if ( state?.ToString() is not { Length: > 0 } message )
                return;

            EssSharpLogEventContext context = null;

            try { context = JsonConvert.DeserializeObject<EssSharpLogEventContext>(eventId.Name); } catch { }

            context ??= new EssSharpLogEventContext() { Path = "unknown" };

            // create new file with name in _outputDirectory
            var tenant    = "EssSharp";
            var type      = (EssSharpLogEventType)eventId.Id;
            var time      = context.Time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var path      = string.Join('_', context.Path
                                  .Split('/')
                                  .ToList()
                                  .Where(e => !string.IsNullOrEmpty(e) && !e.Contains(":"))
                                  .ToList()).Replace(":", "");
            var suffix    = string.Empty;
            var extension = "json";

            if ( type is EssSharpLogEventType.Request or EssSharpLogEventType.Response )
                suffix = $@".{type}".ToLowerInvariant().TrimEnd('.');

            var fileName = $@"{tenant}.{path}.{type}.{time:0.000}-{1}{suffix}.{extension}";

            using var file = File.Create($@"{_outputDirectory.FullName}\{fileName}");
            file.Write(Encoding.UTF8.GetBytes(message));
        }
    }

    /// <summary />
    /// <param name="helper" />
    public class TestOutputLogger( ITestOutputHelper helper ) : ILogger
    {
        private readonly ITestOutputHelper _helper = helper;

        /// <inheritdoc />
        public IDisposable BeginScope<TState>( TState state ) => null;

        /// <inheritdoc />
        public bool IsEnabled( LogLevel logLevel ) => true;

        /// <inheritdoc />
        void ILogger.Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
        {
            if ( state?.ToString() is { Length: > 0 } message )
                _helper?.WriteLine(message);
        }
    }

    /// <summary />
    internal class StringLogger : ILogger
    {
        private readonly StringBuilder _builder;

        /// <summary />
        /// <param name="builder" />
        public StringLogger( ref StringBuilder builder ) { _builder = builder; }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>( TState state ) => null;

        /// <inheritdoc />
        public bool IsEnabled( LogLevel logLevel ) => true;

        /// <inheritdoc />
        void ILogger.Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
        {
            if ( state?.ToString() is { Length: > 0 } message )
                _builder?.AppendLine(message);
        }
    }
}
