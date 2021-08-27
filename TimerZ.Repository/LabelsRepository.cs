using System.Collections.Generic;
using System.Linq;
using TimerZ.DAL;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Repository
{
    public class LabelsRepository : ILabelsReadRepository, ILabelsWriteRepository
    {
        private readonly TimerZDbContext _context;

        public LabelsRepository(TimerZDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Label> GetAllLabels()
        {
            return _context.Labels.ToList();
        }
        public Label GetLabel(string name)
        {

            return _context.Labels.FirstOrDefault(l => l.Name == name);
        }
        public Label GetLabel(int id)
        {
            return _context.Labels.FirstOrDefault(l => l.Id == id);
        }
        public void AddNewLabel(Label label)
        {
            _context.Labels.Add(label);
            _context.SaveChanges();
        }

        public void DeleteLabel(int id)
        {
            var label = GetLabel(id);
            _context.Labels.Remove(label);
            _context.SaveChanges();
        }
    }
}
