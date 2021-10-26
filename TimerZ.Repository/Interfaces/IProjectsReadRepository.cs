using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface IProjectsReadRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProject(string name);
        Task<Project> GetProject(int id);
    }
}
