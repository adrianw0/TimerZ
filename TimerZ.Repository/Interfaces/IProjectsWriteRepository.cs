using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface IProjectsWriteRepository
    {
        void AddNewProject(Project project);
        void DeleteProject(int id);
    }
}
