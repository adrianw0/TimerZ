using System;
using System.Collections.Generic;

namespace TimerZ.Domain.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TimerEntry> TimerEntries { get; set; }
    }
}
