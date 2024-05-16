using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Transversal.Helper;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Transversal.KeyVault;
using SimemNetAdmin.Domain.Models.SeguridadTercero;

namespace SimemNetAdmin.Infra.Data.Repository
{
    public class SecurityRepository
    {
        private readonly SimemNetAdminDbContext _baseContext;

        #region Constructor
        public SecurityRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }
        #endregion

        private static void ValidateUser(GestionUsuarios? user, string app)
        {
            ArgumentNullException.ThrowIfNull(user);
            if (!(user.FechaIniUsuario.CompareTo(DateTime.Now) < 0 && DateTime.Now.CompareTo(user.FechaFinUsuario) < 0)) throw new ArgumentException("Fechas");
            if (user.Estado.ToLower().Equals("pendiente registro")) throw new ArgumentException("Pendiente Registro");
            if (user.Estado.ToLower().Equals("no procede")) throw new ArgumentException("No procede");
            if ((user.APP == null || !user.APP.Contains(app, StringComparison.CurrentCultureIgnoreCase)) && !(user.IsAdmin ?? false)) throw new ArgumentException("APP");
            if (user.Estado.ToLower().Equals("inactivo")) throw new ArgumentException("Inactivo");
            if (
                user.Permisos == null
                || !(user.Permisos.Contains("Lectura") || user.Permisos.Contains("Escritura"))
                )
                throw new ArgumentException("Permisos");
        }

        private static string CheckEmail(UserTerceros user)
        {
            if (user.Correo != null && user.Correo.Split("@").Length > 1) return user.Correo.Split("@")[1];
            else throw new ArgumentException("Correo invalido");
        }

        public async Task<AllowedUser> ValidateUser(UserTerceros user, string app)
        {
            string? domain = CheckEmail(user);

            var empresaDominio = _baseContext.EmpresaDominio.Include(a => a.SeguridadDominio)
                .Where(a => a.SeguridadDominio.Dominio.Equals(domain)).FirstOrDefault()
                ?? throw new ArgumentException("Dominio");

            var dbUser = await _baseContext.GestionUsuarios.Where(a => a.Correo.Equals(user.Correo)).FirstOrDefaultAsync();
            if (dbUser == null && user.Correo != null && app.Contains("terceros", StringComparison.CurrentCultureIgnoreCase))
            {
                var newUser = new GestionUsuarios
                {
                    IdUsuario = Guid.NewGuid(),
                    Correo = user.Correo,
                    APP = "Terceros",
                    Permisos = "Lectura",
                    Estado = "Pendiente Registro",
                    Nombre = user.Nombre,
                    Telefono = user.Telefono,
                    Observacion = null,
                    FechaFinUsuario = DateTime.UtcNow.AddYears(1).AddHours(-5),
                };

                await _baseContext.GestionUsuarios.AddAsync(newUser);
                await _baseContext.SaveChangesAsync();
                dbUser = await _baseContext.GestionUsuarios.Where(a => a.Correo.Equals(user.Correo)).FirstOrDefaultAsync();
            }

            ValidateUser(dbUser, app);

            string token = JwtHandler.GenerateJwtToken(KeyVaultManager.GetSecretValue(KeyVaultTypes.JwtKey), dbUser!);
            List<Empresa> empresas = [];


            if (dbUser != null && dbUser.IsAdmin == true)
            {
                empresas = await _baseContext.Empresa.ToListAsync();
            }
            else
            {
                List<EmpresaDominio> empresasDominios = await _baseContext.EmpresaDominio
                .Include(a => a.Empresa)
                .Include(a => a.SeguridadDominio)
                .Where(a => a.SeguridadDominio.IdDominio.Equals(empresaDominio.SeguridadDominio.IdDominio))
                .ToListAsync();

                foreach (var item in empresasDominios.Where(a => a.Empresa != null))
                {
                    empresas.Add(item.Empresa);
                }
            }

            return new AllowedUser
            {
                Token = token,
                Empresas = empresas,
                Permisos = dbUser!.Permisos ?? ""
            };
        }
    }
}
