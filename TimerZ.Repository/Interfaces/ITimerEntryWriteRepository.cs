using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryWriteRepository
    {
        Task<TimerEntry> AddOrUpdateTimerEntry(TimerEntry timer);
        Task DeleteTimerEntry(int id);
    }
}
