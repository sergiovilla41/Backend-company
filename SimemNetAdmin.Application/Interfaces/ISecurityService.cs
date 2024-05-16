using SimemNetAdmin.Domain.ViewModel;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface ISecurityService
    {
        public Task<AllowedUser> ValidateUser(UserTerceros user, string app);
    }
}
