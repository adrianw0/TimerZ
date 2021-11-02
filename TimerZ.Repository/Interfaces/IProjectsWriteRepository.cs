using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface IProjectsWriteRepository
    {
        Task AddNewProject(Project project);
        Task DeleteProject(int id);
    }
}
