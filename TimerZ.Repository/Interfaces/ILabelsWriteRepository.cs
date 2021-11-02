using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ILabelsWriteRepository
    {
        Task AddNewLabel(Label label);
        Task DeleteLabel(int id);
    }
}
