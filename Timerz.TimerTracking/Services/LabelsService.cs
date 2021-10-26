using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class LabelsService : ILabelsService
    {
        private readonly ILabelsReadRepository _labelsReadRepo;
        private readonly ILabelsWriteRepository _labelsWriteRepo;



        public LabelsService(ILabelsWriteRepository labelsWriteRepo, ILabelsReadRepository labelsReadRepo)
        {
            _labelsWriteRepo = labelsWriteRepo;
            _labelsReadRepo = labelsReadRepo;
        }

        public async Task<IEnumerable<Label>> GetLabels()
        {
            return await _labelsReadRepo.GetAllLabels();
        }

        public async Task AddLabel(Label label, Guid userId)
        {
            label.UserId = userId;
            await _labelsWriteRepo.AddNewLabel(label);
        }

        public async Task DeleteLabel(int id)
        {
            await _labelsWriteRepo.DeleteLabel(id);
        }
    }
}
