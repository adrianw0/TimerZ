using System;
using System.Collections.Generic;
using TimerZ.Domain.Enums;

namespace TimerZ.Domain.Models
{
    public class TimerEntry : ITimerEntry
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimerState State { get; set; }
        public int? ProjectId { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Label> Labels { get; set; }

        public TimerEntry()
        {
            this.State = TimerState.New;
            //this.Id = 0;
            Labels = new HashSet<Label>();
        }
    }
}
