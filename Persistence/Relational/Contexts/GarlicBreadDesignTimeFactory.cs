using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GarlicBread.Persistence.Relational
{
    public class GarlicBreadDesignTimeFactory : IDesignTimeDbContextFactory<GarlicBreadPersistenceContext>
    {
        public GarlicBreadPersistenceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GarlicBreadPersistenceContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Constants.CONFIGURATION_FILENAME)
                .Build();

            var connectionString = configuration.GetConnectionString("Database");

            optionsBuilder.UseNpgsql(connectionString);

            return new GarlicBreadPersistenceContext(optionsBuilder.Options);
        }
    }
}