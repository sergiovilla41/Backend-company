using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers
{
    /// <summary>
    /// Gets, updates and creates record from ConfiguracionClasificacionRegulatoria table.
    /// </summary>
    [ApiController]
    [Route("RegulatoryClassify")]
    public class RegulatoryClassifyController : ControllerBase
    {
        private readonly IRegulatoryClassifyServiceInterface regulatoryClassifyService;

        public RegulatoryClassifyController(IRegulatoryClassifyServiceInterface _regulatoryClassifyService)
        {
            regulatoryClassifyService = _regulatoryClassifyService ?? throw new ArgumentNullException(nameof(_regulatoryClassifyService));
        }

        /// <summary>
        /// Returns a list of ConfiguracionClasificacionRegulatoria table data.
        /// </summary>
        /// <response code="200">Returns a json object that contains an object with all data created.</response>
        /// <response code="204">No data found.</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ConfiguracionClasificacionRegulatoriaDto>? regulatoryClassify = await Task.FromResult(regulatoryClassifyService.GetRegulatoryClassifyList()!);

                if (regulatoryClassify.Count == 0)
                    return NoContent();

                return Ok(regulatoryClassify);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates a  ConfiguracionClasificacionRegulatoria record.
        /// </summary>
        /// <response code="200">The record was updated</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(ConfiguracionClasificacionRegulatoriaDto dto)
        {
            try
            {
                regulatoryClassifyService.UpdateRegulatoryClassify(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Creates a  ConfiguracionClasificacionRegulatoria record.
        /// </summary>
        /// <response code="200">The record was Created and returns the record id</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ConfiguracionClasificacionRegulatoriaDto dto)
        {
            try
            {
                return Ok(await Task.FromResult(regulatoryClassifyService.CreateRegulatoryClassify(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
