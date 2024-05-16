using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Labels;

namespace SimemNetAdmin.Application.Services
{
    public class AssociatedDataSetService : IAssociatedDataSetService
    {
        private readonly IAssociatedDataSetRepository _associatedDataSetRepository;

        public AssociatedDataSetService(IAssociatedDataSetRepository associatedDataSetRepository)
        {
            _associatedDataSetRepository = associatedDataSetRepository ?? throw new ArgumentNullException(nameof(associatedDataSetRepository));
        }
        public async Task<List<LabelDataSetDto>> GetDataDto()
        {
            return await _associatedDataSetRepository.GetDataDto();
        }
    }
}
