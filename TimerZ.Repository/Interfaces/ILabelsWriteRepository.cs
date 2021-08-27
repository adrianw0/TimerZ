using TimerZ.Domain.Models;

namespace TimerZ.Repository.Interfaces
{
    public interface ILabelsWriteRepository
    {
        void AddNewLabel(Label label);
        void DeleteLabel(int id);
    }
}
