using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Repository;

namespace SimemNetAdmin.Application.Services
{
    public class SecurityService: ISecurityService
    {
        private readonly SecurityRepository _menuRepository;

        public SecurityService()
        {
            _menuRepository = new SecurityRepository();
        }
        public async Task<AllowedUser> ValidateUser(UserTerceros user, string app) {
            return await _menuRepository.ValidateUser(user, app);
        }
    }
}
