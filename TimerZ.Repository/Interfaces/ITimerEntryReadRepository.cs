using System.Collections.Generic;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryReadRepository
    {
        IEnumerable<TimerEntry> GetAllEntries();
        TimerEntry GetRunning();
    }
}
