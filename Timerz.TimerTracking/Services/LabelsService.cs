using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimerZ.Common.Interfaces.Repositories.Commands;
using TimerZ.Common.Interfaces.Repositories.Queries;
using TimerZ.Domain.Models;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.TimerTracking.Services
{
    public class LabelsService : ILabelsService
    {
        private readonly IlabelsQueryRepository _labelsReadRepo;
        private readonly ILabelsCommandRepository _labelsWriteRepo;



        public LabelsService(ILabelsCommandRepository labelsWriteRepo, IlabelsQueryRepository labelsReadRepo)
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
