using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimerZ.DAL;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Repository
{
    public class TimerEntryRepository : ITimerEntryWriteRepository, ITimerEntryReadRepository
    {
        private readonly TimerZDbContext _context;

        public TimerEntryRepository(TimerZDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TimerEntry>> GetAllEntries()
        {
            return await _context.TimerEntries
                .AsNoTracking()
                .Include(te=>te.Project)
                .Include(te => te.Labels)
                .ToListAsync();
        }

        public async Task<TimerEntry> GetRunning()
        {
            var  result = await _context.TimerEntries
                .Include(te=>te.Project)
                .Include(te => te.Labels)
                .FirstOrDefaultAsync(e => e.State == TimerState.Running && !e.EndDate.HasValue);
            return result;
        }


        public async Task<TimerEntry> AddOrUpdateTimerEntry(TimerEntry timer)
        {

            var _timer = await _context.TimerEntries.Include(t => t.Labels).FirstOrDefaultAsync(t => t.Id == timer.Id);

            if (_timer == null)
            {
                var newTimer = _context.TimerEntries.Add(timer);
                foreach (var label in  newTimer.Entity.Labels)
                {
                    newTimer.Context.Entry(label).State = EntityState.Unchanged;
                } 
            }
            else{
                _context.Entry(_timer).CurrentValues.SetValues(timer);
                var _labels = _timer.Labels.ToList();
                
                foreach (var timerLabel in timer.Labels)
                {
                    if (_labels.All(l => l.Id != timerLabel.Id))
                    {
                        _timer.Labels.Add(timerLabel);
                    }
                }
            
                foreach (var timerLabel in _labels)
                {
                    if (timer.Labels.FirstOrDefault(l => l.Id == timerLabel.Id) == null)
                    {
                        _timer.Labels.Remove(timerLabel);
                    }
                }

            }

            await _context.SaveChangesAsync();

            return await _context.TimerEntries
                .AsNoTracking()
                .Include(te => te.Project)
                .Include(te => te.Labels)
                .SingleOrDefaultAsync(t => t.Id == timer.Id);
        }

        public async Task DeleteTimerEntry(int id)
        {
            var timer = _context.TimerEntries.Find(id);
            _context.TimerEntries.Remove(timer);

            await _context.SaveChangesAsync();
        }
    }
}
