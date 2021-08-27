using System;
using System.Collections.Generic;

namespace TimerZ.Domain.Models
{
    public interface ITimerEntry
    {
        public int Id { get; set; }
        string Description { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        Project Project { get; set; }
        ICollection<Label> Labels { get; set; }

    }
}