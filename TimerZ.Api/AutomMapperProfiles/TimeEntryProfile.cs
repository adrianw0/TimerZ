using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TimerZ.Api.DTOs;
using TimerZ.Api.Mapper.Helpers;
using TimerZ.Domain.Models;

namespace TimerZ.Api.AutomMapperProfiles
{
    public class TimeEntryProfile : Profile
    {
        public TimeEntryProfile()
        {
            CreateMap<TimerEntry, TimerEntryDto>()
                .ForMember(target => target.Elapsed, opt =>
                {
                    opt.MapFrom(e => GetElapsedTime(e.StartDate.Value,  e.EndDate.HasValue ? e.EndDate.Value : DateTime.UtcNow));
                }
                )
                .ReverseMap();
        }

        private int GetElapsedTime(DateTime startDate, DateTime endDate)
        {
            var span = endDate - startDate;
            return (int)span.TotalMilliseconds;
        }
    }
}
