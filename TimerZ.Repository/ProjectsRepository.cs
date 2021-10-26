using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimerZ.DAL;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Repository
{
    public class ProjectsRepository : IProjectsWriteRepository, IProjectsReadRepository
    {
        private readonly TimerZDbContext _context;

        public ProjectsRepository(TimerZDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.AsNoTracking().ToListAsync();
        }
        public async Task<Project> GetProject(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Name == name);
        }
        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddNewProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int id)
        {
            var project = await GetProject(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
