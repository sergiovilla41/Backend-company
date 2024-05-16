using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IAssociatedDataSetRepository
    {
        Task<List<LabelDataSetDto>> GetDataDto();
    }
}
