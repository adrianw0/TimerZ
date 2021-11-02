using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using TimerZ.DAL;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;
using TimerZ.Repository;
using Xunit;

namespace TimerZ.TimeTracking.Tests
{
    public class TimerEntryRepositoryTest
    {
        private readonly Fixture _fixture;

        public TimerEntryRepositoryTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private static TimerEntryRepository PrepareRepo(ICollection<TimerEntry> data)
        {
            
            var mockContext = new Mock<TimerZDbContext>(new DbContextOptionsBuilder().Options, null, null);
           
            mockContext
                .Setup(x => x.TimerEntries)
                .ReturnsDbSet(data);

            return new TimerEntryRepository(mockContext.Object);

            
        }

        [Fact]
        public async Task GetRunning_ShouldReturnRunningEntry_WhenRunningEntryExists()
        {
            //Arrange
            var running = new TimerEntry { EndDate = null, State = TimerState.Running };
            var data = new List<TimerEntry>
            {
                new() { EndDate = DateTime.Now, State = TimerState.Finished },
                running
            };
            var _sut = PrepareRepo(data);

            //Act
            var returned = await _sut.GetRunning();

            //Assert
            Assert.Equal(running.EndDate, returned.EndDate);
            Assert.Equal(running.State, returned.State);
        }

        [Fact]
        public async Task GetRunning_ShouldReturnNull_WhenRunningEntryNotExists() 
        {
            //Arrange
            _fixture.Customize<TimerEntry>(x=> x
                .With(t=>t.EndDate, DateTime.Now)
            );
            var data = _fixture.CreateMany<TimerEntry>().Where(x => x.State != TimerState.Running).ToList();

            var _sut = PrepareRepo(data);

            //Act
            var returned = await _sut.GetRunning();

            //Assert
            Assert.Null(returned);
            
        }

        [Fact]
        public async Task GetAllEntries_ShouldReturnList_WhenAnyExists()
        {
            //Arrange
            var data = _fixture.CreateMany<TimerEntry>().ToList();
            var _sut = PrepareRepo(data);

            //Act
            var result = await _sut.GetAllEntries();

            //Assert
            Assert.True(result.Any());



        }

        [Fact]
        public async Task AddTimer_ShouldReturnNewTimer_WhenCreated()
        {




        }
        

    }
}
