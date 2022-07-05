using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimerZ.Common.Interfaces.Repositories.Commands;
using TimerZ.Common.Interfaces.Repositories.Queries;
using TimerZ.Domain.Models;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimerEntriesQueryRepository _timerEntryReadRepo;
        private readonly ITimerEntriesCommandRepository _timerEntryWriteRepo;

        public TimeTrackingService(ITimerEntriesQueryRepository timerEntryReadRepo, ITimerEntriesCommandRepository timerEntryWriteRepo )
        {
            _timerEntryReadRepo = timerEntryReadRepo;
            _timerEntryWriteRepo = timerEntryWriteRepo;

        }

        public async Task<IEnumerable<TimerEntry>> GetEntries()
        {
            var entries = await _timerEntryReadRepo.GetAllEntries();

            return entries.OrderByDescending(e => e.StartDate);

        }

        public async Task<TimerEntry> AddEntry(TimerEntry entry, Guid userId)
        {
             
            entry.UserId = userId;

            var newEntry = await _timerEntryWriteRepo.AddOrUpdateTimerEntry(entry);

            return newEntry;

        }

        public async Task<TimerEntry> GetRunningEntry()
        {
            var runningEntry = await _timerEntryReadRepo.GetRunning();
            if (runningEntry == null) return null;

           return runningEntry;
        }

        public async Task DeleteTimerEntry(int id)
        {
            await _timerEntryWriteRepo.DeleteTimerEntry(id);
        }

    }
}
