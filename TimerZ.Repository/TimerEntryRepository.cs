using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<TimerEntry> GetAllEntries()
        {
            return _context.TimerEntries
                .AsNoTracking()
                .Include(te=>te.Project)
                .Include(te => te.Labels)
                .ToList();
        }

        public TimerEntry GetRunning()
        {
            var result = _context.TimerEntries
                .Include(te=>te.Project)
                .Include(te => te.Labels)
                .FirstOrDefault(e => e.State == TimerState.Running && !e.EndDate.HasValue);
            return result;
        }


        public TimerEntry AddOrUpdateTimerEntry(TimerEntry timer)
        {

            var _timer = _context.TimerEntries.Include(t => t.Labels).FirstOrDefault(t => t.Id == timer.Id);

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

            _context.SaveChanges();

            return _context.TimerEntries
                .AsNoTracking()
                .Include(te => te.Project)
                .Include(te => te.Labels)
                .SingleOrDefault(t => t.Id == timer.Id);
        }

        public void DeleteTimerEntry(int id)
        {
            var timer = _context.TimerEntries.Find(id);
            _context.TimerEntries.Remove(timer);

            _context.SaveChanges();
        }
    }
}
