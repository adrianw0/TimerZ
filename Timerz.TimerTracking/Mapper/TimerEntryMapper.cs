using System;
using System.Linq;
using TimerZ.Api.Dtos;
using TimerZ.Api.Mapper.Helpers;
using TimerZ.Domain.Models;
using TimerZ.Common.Providers;

namespace TimerZ.Api.Mapper
{
    public class TimerEntryMapper : ITimerEntryMapper
    {
        private readonly IDateTimeProvider _datetimeProvider;
        public TimerEntryMapper() : this (new DateTimeProvider())
        {
            
        }

        public TimerEntryMapper(IDateTimeProvider dateTimeProvider)
        {
            _datetimeProvider = dateTimeProvider;
        }
        public TimerEntry DtoToTimerEntry(TimerEntryDTO dto)
        {
            var entry = new TimerEntry
            {
                Id = dto.Id,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ProjectId = dto.Project?.Id,
                Labels = dto.Labels,
                State = dto.State
            };
            return entry;
        }

        public TimerEntryDTO TimerEntryToDto(TimerEntry timer)
        {
            var dto = new TimerEntryDTO()
            {
                Id = timer.Id,
                Description = timer.Description,
                StartDate = timer.StartDate,
                EndDate = timer.EndDate,
                Project = timer.Project,
                Labels = timer.Labels.ToArray(),
                State = timer.State,
                Elapsed = Helper.CalculateElapsedTime(timer.StartDate ??  _datetimeProvider.UtcNow, timer.EndDate ?? _datetimeProvider.UtcNow)
            };
            return dto;
        }
    } 
}
