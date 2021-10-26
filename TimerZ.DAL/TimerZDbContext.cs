using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using TimerZ.Common;
using TimerZ.DAL.EntityConfigurations;
using TimerZ.Domain.Models;

namespace TimerZ.DAL
{
    public sealed class TimerZDbContext : KeyApiAuthorizationDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IUserProvider _userProvider;
   

        public TimerZDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, IUserProvider userProvider) : base (options, operationalStoreOptions)
        {
            _userProvider = userProvider;
        }

        private Guid UserId => _userProvider.GetUserId();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TimerEntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LabelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            

           
            modelBuilder.Entity<TimerEntry>().HasQueryFilter(e => e.UserId == UserId);
            modelBuilder.Entity<Label>().HasQueryFilter(l => l.UserId == UserId);
            modelBuilder.Entity<Project>().HasQueryFilter(p => p.UserId == UserId);

        }

        public DbSet<TimerEntry> TimerEntries { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Project> Projects { get; set; }
        
    }
}
