using System.Data.Common;
using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TimerZ.DAL;

namespace TimerZ.TimeTracking.Tests
{
    public class RepositoryTest
    {
        protected readonly Fixture _fixture;




        protected DbContextOptions<TimerZDbContext> ContextOptions { get; }
        protected RepositoryTest(DbContextOptions<TimerZDbContext> contextOptions)
        {

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            ContextOptions = contextOptions;
            

        }

        protected static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
            
        }


    }
}
