using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Menu;

namespace SimemNetAdmin.Application.Services.MenuService
{
    public class MenuServices : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuServices(IMenuRepository menuRepository) {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuJsonModel>> GetRecords(string projectName)
        {
            return await _menuRepository.GetRecords(projectName);
        }
    }
}
