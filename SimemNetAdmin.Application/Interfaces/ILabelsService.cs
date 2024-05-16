using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface ILabelsService
    {
        Task<List<LabelsDto>> ListLabels();
        Task<LabelsDto> GetLabelById(Guid id);
        Task<bool> UpdateLabel(LabelsDto labelDto);

        Task<bool> CreateLabel(LabelsDto labelDto);

    }
}
