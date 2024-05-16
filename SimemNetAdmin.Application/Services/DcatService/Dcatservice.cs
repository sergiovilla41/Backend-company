using SimemNetAdmin.Application.Interfaces.Dcatservice;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Dcat;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Application.Services.DcatService
{
    [ExcludeFromCodeCoverage]
    public class Dcatservice : IDcatService
    {
        private readonly IDcatRepository _dcatRepository;

        public Dcatservice(IDcatRepository dcatRepository)
        {
            _dcatRepository = dcatRepository;
        }

        public Task<List<DcatJsonModel>> GetResource()
        {
            return _dcatRepository.GetResource();
        }
    }
}
