using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Queries
{
    public interface IProjectsQueryRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProject(string name);
        Task<Project> GetProject(int id);
    }
}
