using System;
using System.IO;
using System.Threading.Tasks;
using GarlicBread.Persistence.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GarlicBread.Persistence.Relational
{
    public class GarlicBreadPersistenceContext : DbContext
    {
        public GarlicBreadPersistenceContext(DbContextOptions<GarlicBreadPersistenceContext> options) : base(options)
        {
        }

        public GarlicBreadPersistenceContext()
        {
        }

        public DbSet<JsonRow<GuildConfig>> GuildConfigurations { get; set; }

        public DbSet<CustomizableRole> CustomizableRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Constants.CONFIGURATION_FILENAME)
                .Build();

            var connectionString = configuration.GetConnectionString("Database");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomizableRole>().HasKey(cr => new {cr.GuildId, cr.UserId, cr.RoleId});
        }

        public async Task<TJsonObject> GetJsonObjectAsync<TJsonObject>(
            Func<GarlicBreadPersistenceContext, DbSet<JsonRow<TJsonObject>>> accessor, ulong guildId)
            where TJsonObject : JsonRootObject<TJsonObject>, new()
        {
            var row = accessor(this);
            var rowResult = await row.FindAsync(guildId);
            if (rowResult != null) return rowResult.Data;
            rowResult = new JsonRow<TJsonObject> {GuildId = guildId};
            row.Add(rowResult);
            await SaveChangesAsync();
            return rowResult.Data;
        }

        public async Task<TJsonObject> ModifyJsonObjectAsync<TJsonObject>(
            Func<GarlicBreadPersistenceContext, DbSet<JsonRow<TJsonObject>>> accessor, ulong guildId,
            Action<TJsonObject> modifier)
            where TJsonObject : JsonRootObject<TJsonObject>, new()
        {
            var row = accessor(this);
            var rowResult = await row.FindAsync(guildId);
            if (rowResult == null)
            {
                rowResult = new JsonRow<TJsonObject>
                {
                    GuildId = guildId
                };
                row.Add(rowResult);
            }

            modifier(rowResult.Data);
            Entry(rowResult).Property(d => d.Data).IsModified = true;
            await SaveChangesAsync();
            return rowResult.Data;
        }
    }
}