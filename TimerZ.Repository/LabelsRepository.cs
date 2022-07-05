using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimerZ.Common.Interfaces.Repositories.Commands;
using TimerZ.Common.Interfaces.Repositories.Queries;
using TimerZ.DAL;
using TimerZ.Domain.Models;


namespace TimerZ.Repository
{
    public class LabelsRepository : IlabelsQueryRepository, ILabelsCommandRepository
    {
        private readonly TimerZDbContext _context;

        public LabelsRepository(TimerZDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Label>> GetAllLabels()
        {
            return await _context.Labels.ToListAsync();
        }
        public async Task<Label> GetLabel(string name)
        {

            return await _context.Labels.FirstOrDefaultAsync(l => l.Name == name);
        }
        public async Task<Label> GetLabel(int id)
        {
            return await _context.Labels.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task AddNewLabel(Label label)
        {
            _context.Labels.Add(label);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLabel(int id)
        {
            var label = await GetLabel(id);
            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
        }
    }
}
