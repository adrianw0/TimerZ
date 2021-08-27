

using System;
using System.Linq;
using TimerZ.Api.Dtos;
using TimerZ.Api.Mapper.Helpers;
using TimerZ.Domain.Models;

namespace TimerZ.Api.Mapper
{
    public class TimerEntryMapper
    {
        public static TimerEntry DtoToTimerEntry(TimerEntryDTO dto)
        {
            var entry = TimerEntry.GetEmpty();
            entry.Id = dto.Id;
            entry.Description = dto.Description;
            entry.StartDate = dto.StartDate;
            entry.EndDate = dto.EndDate;
            entry.ProjectId = dto.Project?.Id;
            entry.Labels = dto.Labels;
            entry.State = dto.State;
            return entry;
        }

        public static TimerEntryDTO TimerEntryToDto(TimerEntry timer)
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
                Elapsed = Helper.CalculateElapsedTime(timer.StartDate ??  DateTime.Now, timer.EndDate ?? DateTime.Now)
            };
            return dto;
        }
    }
}
