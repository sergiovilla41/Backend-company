using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Application.Services
{
    public class LabelsService : ILabelsService
    {
        private readonly ILabelsRepository _labelRepository;

        public LabelsService(ILabelsRepository labelRepository)
        {
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        }
        public async Task<List<LabelsDto>> ListLabels()
        {
            return await _labelRepository.ListLabels();
        }
        public async Task<LabelsDto> GetLabelById(Guid id)
        {
            return await _labelRepository.GetLabelById(id);

        }
        public async Task<bool> UpdateLabel(LabelsDto labelDto)
        {
            return await _labelRepository.UpdateLabel(labelDto);
        }

        public async Task<bool> CreateLabel(LabelsDto labelDto)
        {
            return await _labelRepository.CreateLabel(labelDto);
        }
    }
}
