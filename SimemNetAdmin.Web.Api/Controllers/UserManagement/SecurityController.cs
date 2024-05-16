using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Services;
using SimemNetAdmin.Domain.ViewModel;

namespace SimemNetAdmin.Web.Api.Controllers.UserManagement
{
    /// <summary>
    /// Controller for all aspects related to security such as authorization and authentication.
    /// </summary>
    [ApiController]
    [Route("Security")]
    public class SecurityController(ISecurityService securityService) : Controller
    {
        #region Api endpoint Methods
        /// <summary>
        /// Returns token if user is authorized
        /// </summary>
        /// <param name="user">User information.</param>
        /// <param name="app">The app from which is dispatched the call.</param>
        /// <returns></returns>
        [Route("ValidateUser")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser(UserTerceros user, string app)
        {
            try
            {
                return Ok(await securityService.ValidateUser(user, app));
            }
            catch (Exception e)
            {
                return new UnauthorizedObjectResult(new { message = e.Message });
            }

        }
        #endregion
    }
}
