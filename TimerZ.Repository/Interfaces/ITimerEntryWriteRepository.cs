using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryWriteRepository
    {
        Task<TimerEntry> AddOrUpdateTimerEntry(TimerEntry timer, bool ignoreQueryFilters);
        Task DeleteTimerEntry(int id);
    }
}
