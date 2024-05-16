using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface ILabelsRepository
    {

        Task<List<LabelsDto>> ListLabels();
        Task<LabelsDto> GetLabelById(Guid id);
        Task<bool> UpdateLabel(LabelsDto labelDto);

        Task<bool> CreateLabel(LabelsDto labelDto);

    }
}
