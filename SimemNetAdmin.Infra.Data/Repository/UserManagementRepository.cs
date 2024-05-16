using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly SimemNetAdminDbContext _baseContext;
        public UserManagementRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }

        #region Public Methods
        public async Task<GestionUsuarios> UpdateUser(GestionUsuarios gestionUsuarios)
        {
            GestionUsuarios? response = new();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            try
            {
                response = await _baseContext.GestionUsuarios.FirstOrDefaultAsync(x => x.IdUsuario == gestionUsuarios.IdUsuario);

                if (response != null)
                {
                    response.APP = gestionUsuarios.APP;
                    response.Permisos = gestionUsuarios.Permisos;
                    response.Estado = gestionUsuarios.Estado;
                    response.FechaIniUsuario = gestionUsuarios.FechaIniUsuario;
                    response.FechaFinUsuario = gestionUsuarios.FechaFinUsuario;
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new ArgumentNullException(ex.Message);
            }
            return response!;
        }

        public async Task<GestionUsuarios> InsertUser(GestionUsuarios gestionUsuarios)
        {
            try
            {
                gestionUsuarios.IdUsuario = Guid.NewGuid();
                _baseContext.GestionUsuarios.Add(gestionUsuarios!);
                await _baseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            return gestionUsuarios!;
        }

        public async Task<List<GestionUsuarios>> GetUsers()
        {
            List<GestionUsuarios>? response;
            try
            {
                response = await _baseContext.GestionUsuarios.ToListAsync();
            }
            catch (Exception)
            {
                response = null;
            }
            return response!;
        }

        public async Task<List<EmpresaDominio>> ConsultCompanyByDomain()
        {
            List<EmpresaDominio> empresasDominios;
            try
            {
                empresasDominios = await _baseContext.EmpresaDominio.AsQueryable().
                                                                   Include(d => d.Empresa).
                                                                   Include(d => d.SeguridadDominio).ToListAsync();
            }
            catch (Exception)
            {
                empresasDominios = null!;
            }
            return empresasDominios!;
        }
        #endregion
    }
}
