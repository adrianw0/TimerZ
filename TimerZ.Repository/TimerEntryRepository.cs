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

            var _timer = await GetTimerById(timer.Id);

            if (_timer == null) 
                await AddTimer(timer);
            else 
                await UpdateTimerEntry(timer, _timer);

            return await GetTimerById(timer.Id);
        }

        public async Task DeleteTimerEntry(int id)
        {
            var timer = await _context.TimerEntries.FindAsync(id);
            _context.TimerEntries.Remove(timer);

            await _context.SaveChangesAsync();
        }

        private async Task AddTimer(TimerEntry timer)
        {
            var newTimer = _context.TimerEntries.Add(timer);

            if (newTimer.Entity.Labels != null)
                foreach (var label in newTimer.Entity.Labels)
                {
                    newTimer.Context.Entry(label).State = EntityState.Unchanged;
                }

            await _context.SaveChangesAsync();
        }

        private async Task UpdateTimerEntry(TimerEntry timer, TimerEntry existingTimer)
        {
            _context.Entry(existingTimer).CurrentValues.SetValues(timer);
            var _labels = existingTimer.Labels.ToList();

            foreach (var timerLabel in timer.Labels)
            {
                if (_labels.All(l => l.Id != timerLabel.Id))
                {
                    existingTimer.Labels.Add(timerLabel);
                }
            }

            foreach (var timerLabel in _labels)
            {
                if (timer.Labels.FirstOrDefault(l => l.Id == timerLabel.Id) == null)
                {
                    existingTimer.Labels.Remove(timerLabel);
                }
            }

            await _context.SaveChangesAsync();
        }
        private async Task<TimerEntry> GetTimerById(int id)
        {
            return await _context.TimerEntries
                .AsNoTracking()
                .Include(te => te.Project)
                .Include(te => te.Labels)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

    }
}
