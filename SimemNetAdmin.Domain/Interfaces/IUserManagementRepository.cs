using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IUserManagementRepository
    {
        public Task<GestionUsuarios> UpdateUser(GestionUsuarios gestionUsuarios);

        public Task<GestionUsuarios> InsertUser(GestionUsuarios gestionUsuarios);

        public Task<List<GestionUsuarios>> GetUsers();

        public Task<List<EmpresaDominio>> ConsultCompanyByDomain();
    }
}
