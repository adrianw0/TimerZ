using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimerZ.Api.Dtos;
using TimerZ.Api.Mapper;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimerEntryReadRepository _timerEntryReadRepo;
        private readonly ITimerEntryWriteRepository _timerEntryWriteRepo;
        private readonly ITimerEntryMapper _timerEntryMapper;

        public TimeTrackingService(ITimerEntryReadRepository timerEntryReadRepo, ITimerEntryWriteRepository timerEntryWriteRepo, ITimerEntryMapper timerEntryMapper)
        {
            _timerEntryReadRepo = timerEntryReadRepo;
            _timerEntryWriteRepo = timerEntryWriteRepo;
            _timerEntryMapper = timerEntryMapper;
        }

        public async Task<IEnumerable<TimerEntryDTO>> GetEntries()
        {
            var entries = await _timerEntryReadRepo.GetAllEntries();

            return entries.Select(_timerEntryMapper.TimerEntryToDto).OrderByDescending(e => e.StartDate);

        }

        public async Task<TimerEntryDTO> AddEntry(TimerEntryDTO dtoEntry, Guid userId)
        {
            var entry = _timerEntryMapper.DtoToTimerEntry(dtoEntry);
            entry.UserId = userId;
    
            var newEntry = await _timerEntryWriteRepo.AddOrUpdateTimerEntry(entry);

            return _timerEntryMapper.TimerEntryToDto(newEntry);

        }

        public async Task<TimerEntryDTO> GetRunningEntry()
        {
            var runningEntry = await _timerEntryReadRepo.GetRunning();
            if (runningEntry == null) return null;

           return _timerEntryMapper.TimerEntryToDto(runningEntry);
        }

        public async Task DeleteTimerEntry(int id)
        {
            await _timerEntryWriteRepo.DeleteTimerEntry(id);
        }
    }
}
