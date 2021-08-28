using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TimerZ.Common;
using TimerZ.DAL.EntityConfigurations;
using TimerZ.Domain.Models;

namespace TimerZ.DAL
{
    public sealed class TimerZDbContext : ApiAuthorizationDbContext<User>
    {
        private readonly IUserProvider _userProvider;

        public TimerZDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, IUserProvider userProvider) : base (options, operationalStoreOptions)
        {
            _userProvider = userProvider;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TimerEntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LabelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());

            var userId = _userProvider.GetUserId();
            modelBuilder.Entity<TimerEntry>().HasQueryFilter(e => e.UserId == userId);
            modelBuilder.Entity<Label>().HasQueryFilter(l => l.UserId == userId);
            modelBuilder.Entity<Project>().HasQueryFilter(p => p.UserId == userId);
        }

        public DbSet<TimerEntry> TimerEntries { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Project> Projects { get; set; }
        
    }
}
