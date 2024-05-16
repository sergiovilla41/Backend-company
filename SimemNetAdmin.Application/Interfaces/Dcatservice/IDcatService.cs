using SimemNetAdmin.Domain.Models.Dcat;

namespace SimemNetAdmin.Application.Interfaces.Dcatservice
{
    public interface IDcatService
    {
        public Task<List<DcatJsonModel>> GetResource();
    }
}
