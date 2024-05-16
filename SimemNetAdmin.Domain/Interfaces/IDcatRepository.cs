using SimemNetAdmin.Domain.Models.Dcat;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IDcatRepository
    {
        public Task<List<DcatJsonModel>> GetResource();
    }
}
