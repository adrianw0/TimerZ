using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects.AsNoTracking().ToList();
        }
        public Project GetProject(string name)
        {
            return _context.Projects.FirstOrDefault(p => p.Name == name);
        }
        public Project GetProject(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }
        public void AddNewProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            var project = GetProject(id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
    }
}
