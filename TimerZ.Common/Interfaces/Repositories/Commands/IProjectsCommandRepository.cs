using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Commands
{
    public interface IProjectsCommandRepository
    {
        Task AddNewProject(Project project);
        Task DeleteProject(int id);
    }
}
