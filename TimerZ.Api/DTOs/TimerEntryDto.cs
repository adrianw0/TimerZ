using System;
using System.Collections.Generic;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;

namespace TimerZ.Api.DTOs
{
    public class TimerEntryDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public TimerState State { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Label> Labels { get; set; }
        public int Elapsed { get; set; }


    }
}
