using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;

namespace TimerZ.Api.Dtos
{
    public class TimerEntryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
        public int Elapsed { get; set; }
        public Label[] Labels { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimerState State { get; set; }
    }
}
