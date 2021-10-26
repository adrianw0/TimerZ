using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryReadRepository
    {
        Task<IEnumerable<TimerEntry>> GetAllEntries();
        Task<TimerEntry> GetRunning();
    }
}
