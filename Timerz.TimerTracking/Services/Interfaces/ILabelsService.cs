
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;


namespace TimerZ.TimerTracking.Services.Interfaces
{
    public interface ILabelsService
    {
        public Task<IEnumerable<Label>> GetLabels();
        public Task AddLabel(Label label, Guid userId);
        public Task DeleteLabel(int id);
    }
}
