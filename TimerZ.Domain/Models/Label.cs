using System.Collections.Generic;

namespace TimerZ.Domain.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TimerEntry> TimerEntries { get; set; }
    }
}
