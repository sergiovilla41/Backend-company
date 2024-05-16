using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.GeneracionArchivos
{
    [Route("GeneracionArchivos")]
    [ApiController]
    public class GeneracionArchivoController(IGeneracionArchivoService generacionArchivoService) : Controller
    {
        #region Api endpoint Methods
        /// <summary>
        /// Returns dataset data.
        /// </summary>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Records")]
        [HttpGet]
        public async Task<IActionResult> GetRecords()
        {
            try
            {
                List<GeneracionArchivoJson> generacionArchivosResult = await generacionArchivoService.GetRecords();
                if (generacionArchivosResult.Count == 0)
                    return NoContent();

                if (!string.IsNullOrEmpty(generacionArchivosResult.FirstOrDefault()!.Fail))
                    return BadRequest($"Error: {generacionArchivosResult.FirstOrDefault()!.Fail}");

                return Ok(generacionArchivosResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Return a basic data for a specific dataset.
        /// </summary>
        /// <param name="idConfiguracionGeneracionArchivos">unique identifier</param>
        /// <returns></returns>
        [Route("PreLoadBasicData")]
        [HttpGet]
        public async Task<IActionResult> PreLoadBasicData([BindRequired] string idConfiguracionGeneracionArchivos)
        {
            if (string.IsNullOrEmpty(idConfiguracionGeneracionArchivos))
                return BadRequest("Error: Debe especificar el id del conjunto de datos a consultar.");

            try
            {
                Guid idDataset = new(idConfiguracionGeneracionArchivos);
                DatosBasicosJson datosBasicos = await generacionArchivoService.GetDataSetBasicData(idDataset);

                if (datosBasicos == null || datosBasicos.IdConfiguracionGeneracionArchivos.ToString() == "00000000-0000-0000-0000-000000000000")
                    return NoContent();

                return Ok(datosBasicos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns the data based on the selector to load.
        /// </summary>
        /// <param name="selectorType">type of selector to load data</param>
        /// <param name="idDataset">unique identifier</param>
        /// <returns></returns>
        [Route("SelectorDataByType")]
        [HttpGet]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> SelectorDataByType([BindRequired] string selectorType, string? idDataset)
        {
            if (string.IsNullOrEmpty(selectorType))
                return BadRequest("Error: Debe especificar un tipo de selector de datos a consultar.");
            try
            {
                var data = await generacionArchivoService.GetSelectorDataByType(selectorType, idDataset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a specific record
        /// </summary>
        /// <response code="200">Register was updated.</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <response code="409">Indicates that the request cannot be completed due to a conflict with the current state of the resource.</response>
        [HttpPost]
        [Route("Update")]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> UpdateDatosBasicos([BindRequired] DatosBasicosJson datosBasicos)
        {
            try
            {
                if (datosBasicos == null)
                    return BadRequest("Error: Debe contener el objeto a modificar.");

                bool updateSuccess = await generacionArchivoService.UpdateDatosBasicos(datosBasicos);
                if (!updateSuccess)
                    return StatusCode(409, new { message = "Error al modificar la información." });

                return Ok(new { message = "Datos modificados exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("HasRecords")]
        public async Task<IActionResult> HasRecords([BindRequired] string idgeneracionArchivos)
        {
            try
            {
                if (string.IsNullOrEmpty(idgeneracionArchivos))
                    return BadRequest("Error: Debe especificar el id del dataset.");

                bool updateSuccess = await generacionArchivoService.HasRecordsSaved(new(idgeneracionArchivos));
                return Ok(new JsonResult(new { hasRecords = updateSuccess }).Value);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        /// <summary>
        /// Load a specific data to show into the select front element design.
        /// </summary>
        /// <param name="type">object name</param>
        /// <param name="project">Project name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SelectProperties")]
        public async Task<IActionResult> SelectProperties([BindRequired] string type, string? project)
        {
            try
            {
                return Ok(await generacionArchivoService.SelectProperties(type, project));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the columna origen and columna destino table data.
        /// </summary>
        /// <param name="columnasOrigenJson">Object data to update</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateColumns")]
        public async Task<IActionResult> UpdateColumnsDestinityVersion([BindRequired] ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            try
            {
                string result = await generacionArchivoService.UpdateColumnsDestinityVersion(columnasOrigenJson);
                if (!string.IsNullOrEmpty(result))
                    return new BadRequestObjectResult($"Error: {result}");

                return Ok(new { message = "Registros actualizados exitosamente." });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}