using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TimerZ.Api.Mapper;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services;
using Xunit;

namespace TimerZ.TimeTracking.Tests
{
    public class TimerTrackingServiceTest
    {
        private readonly TimeTrackingService _sut;

        private readonly Mock<ITimerEntryReadRepository> _timerReadRepoMock = new();
        private readonly Mock<ITimerEntryWriteRepository> _timerWriteRepoMock = new();


        public TimerTrackingServiceTest()
        {
            _sut = new TimeTrackingService(_timerReadRepoMock.Object, _timerWriteRepoMock.Object, new TimerEntryMapper());
        }

        [Fact]
        public async Task GetRunningEntry_ShouldReturnRunningTimerEntry_WhenRunningEntryExists()
        {
            //arrange
            var runningEntry = new TimerEntry()
            {
                Description = "Description",
                EndDate = null,
                Id = -1,
                Labels = new List<Label>(),
                Project = null,
                ProjectId = null,
                StartDate = DateTime.Now,
                State = TimerState.Running
            };
            //act
            _timerReadRepoMock.Setup(tr => tr.GetRunning()).ReturnsAsync(runningEntry);

            var returnedEntry = await _sut.GetRunningEntry();

            //assert
            Assert.NotNull(returnedEntry);
            Assert.Equal(TimerState.Running, returnedEntry.State);
            Assert.Null(returnedEntry.EndDate);
        }

    }
}
