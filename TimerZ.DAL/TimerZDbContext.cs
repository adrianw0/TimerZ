using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TimerZ.DAL.EntityConfigurations;
using TimerZ.Domain.Models;

namespace TimerZ.DAL
{
    public sealed class TimerZDbContext : ApiAuthorizationDbContext<User>
    {
        public TimerZDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base (options, operationalStoreOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TimerEntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LabelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());

        }

        public DbSet<TimerEntry> TimerEntries { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Project> Projects { get; set; }
        
    }
}
