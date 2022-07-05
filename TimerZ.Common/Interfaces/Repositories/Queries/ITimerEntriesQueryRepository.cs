using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Queries
{
    public interface ITimerEntriesQueryRepository
    {
        Task<IEnumerable<TimerEntry>> GetAllEntries();
        Task<TimerEntry> GetRunning();
    }
}
