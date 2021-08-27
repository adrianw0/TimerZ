using System.Collections.Generic;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface IProjectsReadRepository
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(string name);
        Project GetProject(int id);
    }
}
