using SimemNetAdmin.Domain.Models.Menu;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IMenuRepository
    {
        public Task<List<MenuJsonModel>> GetRecords(string projectName);
    }
}
