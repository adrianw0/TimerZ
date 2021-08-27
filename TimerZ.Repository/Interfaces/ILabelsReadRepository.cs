using System.Collections.Generic;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ILabelsReadRepository
    {
        IEnumerable<Label> GetAllLabels();
        Label GetLabel(string name);
        Label GetLabel(int id);
    }
}
