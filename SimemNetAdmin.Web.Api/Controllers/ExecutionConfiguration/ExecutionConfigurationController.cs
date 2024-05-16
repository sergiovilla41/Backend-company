using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.ConfiguracionEjecucionService;
using SimemNetAdmin.Domain.Models.Execution;

namespace SimemNetAdmin.Web.Api.Controllers.ExecutionConfiguration
{
    [Route("ExecutionConfiguration")]
    [ApiController]
    public class ExecutionConfigurationController : ControllerBase
    {
        private readonly IExecutionConfigurationService _ExecutionConfiguration;

        public ExecutionConfigurationController(IExecutionConfigurationService configuracionEjecucion)
        {
            _ExecutionConfiguration = configuracionEjecucion ?? throw new ArgumentNullException(nameof(configuracionEjecucion));
        }

        /// <summary>
        /// Return a columnas origen table data list.
        /// </summary>
        /// <param name="idExtraccion">id</param>
        /// <response code="200">Returns a json object that contains an object with all data created.</response>
        /// <response code="204">No data found.</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ByIdExtraccion([BindRequired] Guid idExtraccion)
        {
            try
            {
                List<ExecutionModel> configuracionEjecucionData = await _ExecutionConfiguration.ExecutionConfigurationData(idExtraccion);
                if (configuracionEjecucionData.Count == 0)
                    return NoContent();

                return Ok(configuracionEjecucionData);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error: {ex.Message}");
            }
        }
    }
}
