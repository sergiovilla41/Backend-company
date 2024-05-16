using SimemNetAdmin.Application.Interfaces.PublicationService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.Models.Publication;
using SimemNetAdmin.Domain.ViewModel.Execution;
using SimemNetAdmin.Domain.ViewModel.Publication;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Services.PublicationService
{
    [ExcludeFromCodeCoverage]
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
        }

        public async Task<List<PublicationModel>?> GetById(string idDataSet)
        {
            return await _publicationRepository.GetById(idDataSet)!;
        }

        public async Task<string> Save(PublicationDto publicationModel)
        {
            return await _publicationRepository.Save(publicationModel)!;
        }
        public async Task<string> Update(PublicationDto publicationModel)
        {
            return await _publicationRepository.Update(publicationModel)!;
        }

        public async Task<string> Delete(string idPublicacionRegulatoria)
        {
            return await _publicationRepository.Delete(idPublicacionRegulatoria)!;
        }
    }
}
