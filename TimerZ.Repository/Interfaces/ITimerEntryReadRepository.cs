using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryReadRepository
    {
        Task<IEnumerable<TimerEntry>> GetAllEntries(bool ignoreQueryFilter = false);
        Task<TimerEntry> GetRunning(bool ignoreQueryFilters = false);
    }
}
