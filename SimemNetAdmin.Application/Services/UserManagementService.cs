using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;

namespace SimemNetAdmin.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
        }


        public async Task<string> UpdateUser(GestionUsuarios gestionUsuarios)
        {
            string? response;
            try
            {
                GestionUsuarios result = await _userManagementRepository.UpdateUser(gestionUsuarios);

                response = result == null ? null : $"Se actualizó correctamente la información del registro {result!.IdUsuario}";
            }
            catch (Exception ex)
            {
                return $"Error: Se presento un error interno, {ex.InnerException}";
            }
            return response!;
        }


        public async Task<string> InsertUser(GestionUsuarios gestionUsuarios)
        {
            string? response;
            try
            {
                GestionUsuarios result = await _userManagementRepository.InsertUser(gestionUsuarios);
                response = result == null ? null : $"Se creo correctamente el usuario con id {result.IdUsuario}";
            }
            catch (Exception ex)
            {
                return $"Error: Se presento un error interno, {ex.InnerException}";
            }
            return response!;
        }


        public async Task<List<GestionUsuarios>> GetUsers()
        {
            List<GestionUsuarios>? response = new();
            try
            {
                response = await _userManagementRepository.GetUsers();
                List<(string empresa, string dominio)> LstCompanyDomain = await QueryCompanyByDomainLst();

                foreach (GestionUsuarios user in response)
                {
                    string domain = CheckEmail(user);
                    LstCompanyDomain.ForEach(e => {
                        if (e.dominio == domain)
                        {
                            user.Empresa = e.empresa;
                        }
                    });
                }
            }
            catch (Exception)
            {
                response = null;
            }
            return response!;
        }

        public async Task<List<EmpresaDominio>> ConsultCompanyByDomain()
        {
            return await _userManagementRepository.ConsultCompanyByDomain();
        }


        #region Métodos privados
        private async Task<List<(string empresa, string dominio)>> QueryCompanyByDomainLst()
        {
            List<(string empresa, string dominio)> listaEmpresasDominios;
            try
            {
                List<EmpresaDominio> empresaDominios = await _userManagementRepository.ConsultCompanyByDomain();
                listaEmpresasDominios = [];

                foreach (var obj in empresaDominios)
                {
                    string? nombreEmpresa = obj.Empresa.Nombre;
                    string? nombreDominio = obj.SeguridadDominio.Dominio;

                    listaEmpresasDominios.Add((nombreEmpresa!, nombreDominio!));
                }
            }
            catch (Exception)
            {
                listaEmpresasDominios = null!;
            }
            return listaEmpresasDominios;
        }


        private static string CheckEmail(GestionUsuarios user)
        {
            string email;
            email = user.Correo != null && user.Correo.Split("@").Length > 1 ? user.Correo.Split("@")[1] : string.Empty;
            return email;
        }
        #endregion
    }
}