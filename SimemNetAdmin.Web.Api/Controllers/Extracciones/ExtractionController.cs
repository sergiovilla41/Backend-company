using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.Extraction;
using SimemNetAdmin.Domain.ViewModel.Extracciones;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.Extracciones
{
    [Route("Extraction")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ExtractionController : ControllerBase
    {
        private readonly IExtractionService _extractionService;

        public ExtractionController(IExtractionService extractionService)
        {
            _extractionService = extractionService ?? throw new ArgumentNullException(nameof(extractionService));
        }
       
        #region Api endpoint Methods
        /// <summary>
        /// Returns the configured extractions to a specific dataset.
        /// </summary>
        /// <param name="idDataset">unique dataset identifier</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Records")]
        [HttpGet]
        public async Task<IActionResult> GetRecordsById([BindRequired] string idDataset)
        {
            try
            {
                List<ExtractionsModel>? extractionsModels = await _extractionService.GetById(string.Empty, idDataset);
                return Ok(extractionsModels);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a extraction to a specific dataset.
        /// </summary>
        /// <param name="extractionsModel">Exraction object to add</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> AddRecord([BindRequired] ExtractionsModelDto extractionsModel)
        {
            try
            {
                string result = await _extractionService.Save(extractionsModel);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro agregado exitosamente!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// update a extraction to a specific dataset.
        /// </summary>
        /// <param name="extractionsModel">Exraction object to update record</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("UpdateExtraction")]
        [HttpPut]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Update([BindRequired] ExtractionsModelDto extractionsModel)
        {
            try
            {
                string result = await _extractionService.Update(extractionsModel);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro actualizado exitosamente!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Dlete a extraction to a specific dataset.
        /// </summary>
        /// <param name="idExtraccion">unique dataset identifier</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Remove")]
        [HttpDelete]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Remove([BindRequired] string idExtraccion)
        {
            try
            {
                string result = await _extractionService.Delete(idExtraccion);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro removido exitosamente!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}