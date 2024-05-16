using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.OriginColumns
{
    [Route("OriginColumns")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class OriginColumnsController : ControllerBase
    {
        private readonly IOriginColumnsService _columnasOrigenService;

        public OriginColumnsController(IOriginColumnsService columnasOrigenService)
        {
            _columnasOrigenService = columnasOrigenService ?? throw new ArgumentNullException(nameof(columnasOrigenService));
        }

        /// <summary>
        /// Update the columna origen and columna destino table data.
        /// </summary>
        /// <param name="columnasOrigenJson">Object data to update</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([BindRequired] ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            try
            {
                string result = await _columnasOrigenService.UpdateColumnasOrigen(columnasOrigenJson);
                if (!string.IsNullOrEmpty(result))
                    return new BadRequestObjectResult($"Error: {result}");

                return Ok(new { message = "Registros actualizados exitosamente." });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Return a columnas origen table data list.
        /// </summary>
        /// <param name="idExtraccion">id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByIdExtraccion")]
        public async Task<IActionResult> ByIdExtraccion([BindRequired] Guid idExtraccion)
        {
            try
            {
                List<ConfiguracionColumnasOrigenJson> columnasOrigenJson = await _columnasOrigenService.GetColumnasOrigenJson(idExtraccion);
                return Ok(columnasOrigenJson);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error: {ex.Message}");
            }
        }
    }
}
