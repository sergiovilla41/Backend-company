using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Domain.Models.Menu;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.v1
{
    [Route("Menu")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        ///  Return a list of values to generate a menu object.
        /// </summary>
        /// <param name="projectName">Proyect name ("simem" or "terceros")</param>
        /// <response code="200">Returns a json object that contains the menu records based on the project params</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        [HttpGet]
        public async Task<IActionResult> GetMenuTreeData([BindRequired] string projectName)
        {
            try
            {
                List<MenuJsonModel> menuResult = await _menuService.GetRecords(projectName);
                if (menuResult.Count == 0)
                    return NoContent();

                return Ok(menuResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        
    }
}
