using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface IUserManagementService
    {
        public Task<string> UpdateUser(GestionUsuarios gestionUsuarios);

        public Task<string> InsertUser(GestionUsuarios gestionUsuarios);

        public Task<List<GestionUsuarios>> GetUsers();

        public Task<List<EmpresaDominio>> ConsultCompanyByDomain();
    }
}
