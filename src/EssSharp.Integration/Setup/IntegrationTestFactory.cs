using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Docker.DotNet;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Xunit;

using AccessMode = DotNet.Testcontainers.Configurations.AccessMode;

namespace EssSharp.Integration.Setup
{
    public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class where TDbContext : DbContext
    {
        private readonly DockerContainer _database;

        public IntegrationTestFactory()
        {
            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var msSqlScriptPath = Path.Combine(binPath, "Scripts", "MsSql");

            _database = new MsSqlBuilder()
                .WithImage(@"mcr.microsoft.com/mssql/server:2022-latest")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "StrongPassw0rd")
                .WithPassword("StrongPassw0rd")
                // Does NOT work. .WithResourceMapping is recommeded, but the resource
                // mounts with insufficient permissions AND is not available in time
                // for use with the startup/entrypoint command.
                //.WithResourceMapping(Path.Combine(msSqlScriptPath, @"start-db.sh"), @"/opt/scripts/start-db.sh")
                .WithBindMount(msSqlScriptPath, @"/opt/scripts", AccessMode.ReadWrite)
                .WithCommand(@"/opt/scripts/start-db.sh")
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" })
                .WithCreateParameterModifier(pm => pm.Healthcheck = new Docker.DotNet.Models.HealthConfig() 
                { 
                    Test = new[] { "CMD", "/opt/scripts/healthcheck.sh" },
                    Interval = TimeSpan.FromSeconds(30),
                    Timeout = TimeSpan.FromSeconds(5),
                    StartPeriod = TimeSpan.FromSeconds(30).Ticks,
                    Retries = 10
                })
                .Build();
        }

        public void CreateContainers()
        {
            if ( _database is null )
                CreateClient();
        }

        /// <summary />
        public DockerClient CreateDockerClient()
        {
            if ( _database is null )
                CreateClient();

            using var dockerClientConfiguration = TestcontainersSettings.OS.DockerEndpointAuthConfig.GetDockerClientConfiguration();
            return dockerClientConfiguration.CreateClient();
        }

        /// <summary />
        public DockerContainer Database => _database;

        protected override void ConfigureWebHost( IWebHostBuilder builder )
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<TDbContext>(options => { options.UseSqlServer((_database as MsSqlContainer).GetConnectionString()); });
            });
        }

        public async Task InitializeAsync() => await _database.StartAsync();

        public new async Task DisposeAsync() => await _database.DisposeAsync();
    }
}
