using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerZ.Api.Dtos;
using TimerZ.Domain.Models;

namespace TimerZ.Api.Mapper
{
    public interface ITimerEntryMapper
    {
        public TimerEntry DtoToTimerEntry(TimerEntryDTO dto);
        public TimerEntryDTO TimerEntryToDto(TimerEntry timer);
    }
}
