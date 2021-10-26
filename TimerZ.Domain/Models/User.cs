using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TimerZ.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TimerEntry> TimerEntries { get; set; }
    }
}
