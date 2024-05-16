using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Interfaces.Columns;
using SimemNetAdmin.Application.Services;

using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SimemNetAdmin.Web.Api.Controllers.Columns
{
    [Route("ConfiguracionColumnasDestinoController")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ConfiguracionColumnasDestinoController : ControllerBase
    {
       
              
        private readonly IConfiguracionColumnasDestinoService _configuracionColumnasDestinoService;
        
        public ConfiguracionColumnasDestinoController(IConfiguracionColumnasDestinoService configuracionColumnasDestinoService)
        {
            _configuracionColumnasDestinoService = configuracionColumnasDestinoService;
            
        }
        /// <summary>
        /// Return a list of Colmuns.
        /// </summary>
        /// <response code="200">Returns a json object containing a list of columns.</response>        
        /// <response code="400">Returns a BadRequest object that contains an error message.</response>
        [HttpGet]
        [Route("ListColumnaDestino")]
        public async Task<IActionResult> ListColumnaDestino()
        {
            try
            {

                List<ConfiguracionColumnasDestinoDTO>? columns = await _configuracionColumnasDestinoService.ListColumnaDestino();
                if (columns.Count == 0)
                {
                    return NoContent();


                }
                return Ok(columns);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        // <summary>
        /// Actualiza una columna de destino en el sistema.
        /// </summary>
        /// <param name="columnaDestinoDTO">Los datos para actualizar la columna de destino.</param>
        /// <response code="200">La columna de destino se actualizó correctamente.</response>
        /// <response code="400">Error al procesar la solicitud.</response>
        [HttpPut]
        [Route("UpdateColumnaDestino")]
        public async Task<IActionResult> UpdateColumnaDestino(ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
            try
            {
                await _configuracionColumnasDestinoService.UpdateColumnaDestinoAsync(columnaDestinoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Crea una nueva columna de destino en el sistema.
        /// </summary>
        /// <param name="columnaDestinoDTO">Los datos de la nueva columna de destino.</param>
        /// <response code="200">La columna de destino se creó correctamente.</response>
        /// <response code="400">Error al procesar la solicitud.</response>
        [HttpPost("CreateColumnaDestino")]
        public async Task<IActionResult> CreateColumnaDestino([FromBody] ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
            try
            {
                var nuevaColumnaId = await _configuracionColumnasDestinoService.CreateColumnaDestino(columnaDestinoDTO);
                return Ok(columnaDestinoDTO); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }








    }


}

