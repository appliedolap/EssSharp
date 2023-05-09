using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace EssSharp.Integration.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, DbContext>>
    {
        public readonly IntegrationTestFactory<Program, DbContext> Factory;
        //public readonly DbContext DbContext;
        public readonly DockerContainer Database;

        public IntegrationTestBase( IntegrationTestFactory<Program, DbContext> factory )
        {
            Factory = factory;
            //DbContext = factory.Services.CreateScope().ServiceProvider.GetRequiredService<DbContext>();
            Database = factory.Database;
        }
    }
}
