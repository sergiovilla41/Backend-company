using SimemNetAdmin.Domain.Models.Menu;

namespace SimemNetAdmin.Application.Interfaces.MenuServices
{
    public interface IMenuService
    {
        public Task<List<MenuJsonModel>> GetRecords(string projectName);
    }
}
