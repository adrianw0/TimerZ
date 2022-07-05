using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.TimerTracking.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        public Task<IEnumerable<TimerEntry>> GetEntries();
        public Task<TimerEntry> AddEntry(TimerEntry dtoEntry, Guid userId);
        public Task<TimerEntry> GetRunningEntry();
        public Task DeleteTimerEntry(int id);

    }
}
