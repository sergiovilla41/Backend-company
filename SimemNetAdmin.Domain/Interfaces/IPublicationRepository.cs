using SimemNetAdmin.Domain.Models.Publication;
using SimemNetAdmin.Domain.ViewModel.Execution;
using SimemNetAdmin.Domain.ViewModel.Publication;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IPublicationRepository
    {
        public Task<List<PublicationModel>?> GetById(string idDataSet);
        public Task<string> Save(PublicationDto publicationModel);
        public Task<string> Update(PublicationDto publicationModel);
        public Task<string> Delete(string idPublicacionRegulatoria);
    }
}
