using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface IAssociatedDataSetService
    {
        Task<List<LabelDataSetDto>> GetDataDto();
    }
}
