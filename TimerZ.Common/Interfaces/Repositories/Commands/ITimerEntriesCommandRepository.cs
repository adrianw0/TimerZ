using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Commands
{
    public interface ITimerEntriesCommandRepository
    {
        Task<TimerEntry> AddOrUpdateTimerEntry(TimerEntry timer);
        Task DeleteTimerEntry(int id);
    }
}
