using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using TimerZ.Common;

namespace TimerZ.DAL
{
    public class TimerTrackContextFactory : IDesignTimeDbContextFactory<TimerZDbContext>
    {
        public TimerZDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimerZDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=TimersDb;Integrated Security=True");

            return new TimerZDbContext(optionsBuilder.Options, new OperationalStoreOptionsMigrations(), new userProvider());
        }
    }
    public class userProvider : IUserProvider
    {
        public Guid GetUserId()
        {
            return new Guid();
        }
    }
    public class OperationalStoreOptionsMigrations :
        IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions()
        {
            DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
            EnableTokenCleanup = false,
            PersistedGrants = new TableConfiguration("PersistedGrants"),
            TokenCleanupBatchSize = 100,
            TokenCleanupInterval = 3600,
        };
    }
}
