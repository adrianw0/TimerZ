using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Queries
{
    public interface IlabelsQueryRepository
    {
        Task<IEnumerable<Label>> GetAllLabels();
        Task<Label> GetLabel(string name);
        Task<Label> GetLabel(int id);
    }
}
