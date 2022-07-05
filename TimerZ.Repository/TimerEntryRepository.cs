using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimerZ.DAL;
using TimerZ.Domain.Enums;
using TimerZ.Domain.Models;
using TimerZ.Common.Interfaces.Repositories.Commands;
using TimerZ.Common.Interfaces.Repositories.Queries;

namespace TimerZ.Repository
{
    public class TimerEntryRepository : ITimerEntriesCommandRepository, ITimerEntriesQueryRepository
    {
        private readonly TimerZDbContext _context;

        public TimerEntryRepository(TimerZDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TimerEntry>> GetAllEntries()
        {
            var query = _context.TimerEntries
                .Include(te => te.Project)
                .Include(te => te.Labels)
                .AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TimerEntry> GetRunning()
        {
            //var query = ignoreQueryFilter ? _context.TimerEntries.IgnoreQueryFilters() : _context.TimerEntries;
            var query = _context.TimerEntries
                 .Include(te => te.Project)
                 .Include(te => te.Labels);

            var result = await query.FirstOrDefaultAsync(e => e.State == TimerState.Running && !e.EndDate.HasValue);
            return result;
        }


        public async Task<TimerEntry> AddOrUpdateTimerEntry(TimerEntry timer)
        {

            var _timer = await GetTimerById(timer.Id);

            if (_timer == null) 
                await AddTimer(timer);
            else 
                await UpdateTimerEntry(timer);

            return await GetTimerById(timer.Id);

        }

        public async Task DeleteTimerEntry(int id)
        {
            var timer = await _context.TimerEntries.FindAsync(id);
            if (timer.Labels.Any())
            {
                foreach (var label in timer.Labels)
                {
                    _context.Attach(label);
                }
            }
            
            _context.TimerEntries.Remove(timer);

            await _context.SaveChangesAsync();
        }

        private async Task AddTimer(TimerEntry timer)
        {
            if(timer.Project != null)
                _context.Attach(timer.Project);
            if (timer.Labels.Any())
            {
                foreach (var label in timer.Labels)
                {
                    _context.Attach(label);
                }
            }

                var newTimer = await _context.TimerEntries.AddAsync(timer);

            if (newTimer.Entity.Labels != null)
                foreach (var label in newTimer.Entity.Labels)
                {
                    newTimer.Context.Entry(label).State = EntityState.Unchanged;
                }

            await _context.SaveChangesAsync();



        }

        private async Task UpdateTimerEntry(TimerEntry timer)
        {
            var _timer = await GetTimerById(timer.Id);
            _context.Entry(_timer).CurrentValues.SetValues(timer);

    

            var _labels = _timer.Labels.ToList();

            _context.AttachRange(_labels);

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

            await _context.SaveChangesAsync();
        }
        private async Task<TimerEntry> GetTimerById(int id)
        {
            //var query = ignoreQueryFilters ?  _context.TimerEntries.IgnoreQueryFilters() : _context.TimerEntries;
            var query = _context.TimerEntries
                .Include(te => te.Project)
                .Include(te => te.Labels);

            return await query.SingleOrDefaultAsync(t => t.Id == id);
        }

    }
}
