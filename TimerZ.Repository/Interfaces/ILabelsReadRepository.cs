using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ILabelsReadRepository
    {
        Task<IEnumerable<Label>> GetAllLabels();
        Task<Label> GetLabel(string name);
        Task<Label> GetLabel(int id);
    }
}
