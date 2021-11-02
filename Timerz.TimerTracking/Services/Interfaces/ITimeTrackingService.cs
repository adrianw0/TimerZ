using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Api.Dtos;

namespace TimerZ.TimerTracking.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        public Task<IEnumerable<TimerEntryDTO>> GetEntries();
        public Task<TimerEntryDTO> AddEntry(TimerEntryDTO dtoEntry, Guid userId, bool ignoreQueryFilters = false);
        public Task<TimerEntryDTO> GetRunningEntry();
        public Task DeleteTimerEntry(int id);

    }
}
