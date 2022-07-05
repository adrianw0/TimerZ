using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Common.Interfaces.Repositories.Commands;
using TimerZ.Common.Interfaces.Repositories.Queries;
using TimerZ.Domain.Models;

using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsQueryRepository _projectsReadRepo;
        private readonly IProjectsCommandRepository _projectWriteRepo;

        public ProjectsService(IProjectsCommandRepository projectWriteRepo, IProjectsQueryRepository projectsReadRepo)
        {
            _projectWriteRepo = projectWriteRepo;
            _projectsReadRepo = projectsReadRepo;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectsReadRepo.GetAllProjects();
        }

        public async Task AddProject(Project project, Guid userId)
        {
            project.UserId = userId;
            await _projectWriteRepo.AddNewProject(project);
        }

        public async Task DeleteProject(int id)
        {
            await _projectWriteRepo.DeleteProject(id);
        }
    }
}
