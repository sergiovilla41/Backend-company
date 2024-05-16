using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimemNetAdmin.Web.Api.Controllers.UserManagement
{
    [Route("UserManagement")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
        }

        /// <summary>
        /// Update Users.
        /// </summary>
        /// <description>
        /// Update the columns APP, Estado, Permisos, FechaIniUsuario y FechaFinUsuario table [SeguridadTercero].[Usuario].
        /// </description>
        /// <param name="GestionUsuarios">Object data to update</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser(GestionUsuarios gestionUsuarios)
        {
            try
            {
                var result = await _userManagementService.UpdateUser(gestionUsuarios);
                if (result == null)
                    return new BadRequestObjectResult($"Error: No fue posible actualizar el registro con los datos ingresados, {result}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error interno: {ex.Message}");
            }
        }


        /// <summary>
        /// Update Users.
        /// </summary>
        /// <description>
        /// Update the columns APP, Estado, Permisos, FechaIniUsuario y FechaFinUsuario table [SeguridadTercero].[Usuario].
        /// </description>
        /// <param name="GestionUsuarios">Object data to update</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertUser(GestionUsuarios gestionUsuarios)
        {
            try
            {
                var result = await _userManagementService.InsertUser(gestionUsuarios);
                if (result == null)
                    return new BadRequestObjectResult($"Error: No fue posible crear el registro con los datos ingresados, {result}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error interno: {ex.Message}");
            }
        }


        /// <summary>
        /// Get Users.
        /// </summary>
        /// <description>
        /// Get all register of table [SeguridadTercero].[Usuario].
        /// </description>
        /// <param></param>
        /// <returns>List of registers in the table</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _userManagementService.GetUsers();
                if (result.Count == 0)
                    return new BadRequestObjectResult($"Error: No se obtuvo información de la tabla, {result}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error interno: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get domain and company by domain by email.
        /// </summary>
        /// <description>
        /// Get all registers in the table [SeguridadTercero].[Dominio] join [SeguridadTercero].[Empresa]
        /// </description>
        /// <param></param>
        /// <returns>List of email - company in the table</returns>
         [HttpGet("[action]")]
        public async Task<IActionResult> ConsultCompanyByDomain()
        {
            try
            {
                var result = await _userManagementService.ConsultCompanyByDomain();
                if (result == null)
                    return new BadRequestObjectResult($"Error: No se obtuvo información de la tabla, {result}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error interno: {ex.InnerException}");
            }
        }
    }
}
