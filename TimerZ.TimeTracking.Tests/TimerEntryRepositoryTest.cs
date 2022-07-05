using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimerZ.DAL;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;
using TimerZ.Repository;
using Xunit;

namespace TimerZ.TimeTracking.Tests
{
    public class TimerEntryRepositoryTest: RepositoryTest
    {

        [Fact]
        public async Task GetRunning_ShouldReturnRunningEntry_WhenRunningEntryExists()
        {
            //Arrange
            await using (var context = new TimerZDbContext(ContextOptions, new OperationalStoreOptionsMigrations(), new UserProvider()))
            {
                await context.Database.EnsureCreatedAsync();

                var running = _fixture.Create<TimerEntry>();
                running.State = TimerState.Running;
                running.EndDate = null;

                await context.AddAsync(running);
                await context.SaveChangesAsync();

                //Act    
                var _sut = new TimerEntryRepository(context);
                var result = await _sut.GetRunning(true);

                //Assert

                Assert.Equal(running.EndDate, result.EndDate);
                Assert.Equal(running.State, result.State);
                Assert.Equal(TimerState.Running, result.State );
            }
        }

        [Fact]
        public async Task GetRunning_ShouldReturnNull_WhenRunningEntryNotExists()
        {
            //Arrange
            _fixture.Customize<TimerEntry>(x => x
                .With(t => t.EndDate, DateTime.Now)
            );

            await using (var context = new TimerZDbContext(ContextOptions, new OperationalStoreOptionsMigrations(), new UserProvider()))
            {
                await context.Database.EnsureCreatedAsync();
                var data = _fixture.CreateMany<TimerEntry>().Where(x => x.State != TimerState.Running).ToList();

                await context.AddRangeAsync(data);
                await context.SaveChangesAsync();
                var _sut = new TimerEntryRepository(context);

            //Act
                var result = await _sut.GetRunning();

            //Assert
                Assert.Null(result);
            }

        }

        [Fact]
        public async Task GetAllEntries_ShouldReturnList_WhenAnyExists()
        {
            //Arrange
            var data = _fixture.CreateMany<TimerEntry>().ToList();

            await using (var context = new TimerZDbContext(ContextOptions, new OperationalStoreOptionsMigrations(), new UserProvider()))
            {
                await context.Database.EnsureCreatedAsync();
                await context.AddRangeAsync(data);
                await context.SaveChangesAsync();

                var _sut = new TimerEntryRepository(context);
                //Act
                var result = await _sut.GetAllEntries(true);

                //Assert
                Assert.True(result.Any());
            }
        }

        [Fact]
        public async Task AddTimer_ShouldReturnNewTimer_WhenCreated()
        {
            var entry = _fixture.Create<TimerEntry>();

            await using (var context = new TimerZDbContext(ContextOptions, new OperationalStoreOptionsMigrations(), new UserProvider()))
            {
                //Arrange
                await context.Database.EnsureCreatedAsync();
                await context.AddAsync(entry);
                await context.SaveChangesAsync();

                var _sut = new TimerEntryRepository(context);

                //Act
                var result = await _sut.AddOrUpdateTimerEntry(entry, true);

                //Assert
                Assert.NotNull(result);
            }
        }

        public TimerEntryRepositoryTest():base(
            new DbContextOptionsBuilder<TimerZDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options)
        {
        }
    }
}
