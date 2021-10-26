using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.TimerTracking.Services.Interfaces
{
    public interface IProjectsService
    {
        public Task<IEnumerable<Project>> GetProjects();
        public Task AddProject(Project project, Guid userId);
        public Task DeleteProject(int id);
    }
}
