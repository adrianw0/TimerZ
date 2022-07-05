using System;
using System.Threading.Tasks;
using TimerZ.Domain.Models;

namespace TimerZ.Common.Interfaces.Repositories.Commands
{
    public interface ILabelsCommandRepository
    {
        Task AddNewLabel(Label label);
        Task DeleteLabel(int id);
    }
}
