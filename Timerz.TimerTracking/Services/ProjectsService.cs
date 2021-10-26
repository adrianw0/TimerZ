using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsReadRepository _projectsReadRepo;
        private readonly IProjectsWriteRepository _projectWriteRepo;

        public ProjectsService(IProjectsWriteRepository projectWriteRepo, IProjectsReadRepository projectsReadRepo)
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
