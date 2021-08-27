using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ITimerEntryWriteRepository
    {
        TimerEntry AddOrUpdateTimerEntry(TimerEntry timer);
        void DeleteTimerEntry(int id);
    }
}
