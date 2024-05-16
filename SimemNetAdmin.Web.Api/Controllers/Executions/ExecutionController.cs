using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.ExecutionService;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.ViewModel.Execution;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.Executions
{
    [Route("Execution")]
    [ApiController]
    public class ExecutionController : ControllerBase
    {
        private readonly IExecutionService _executionService;

        public ExecutionController(IExecutionService executionService)
        {
            _executionService = executionService ?? throw new ArgumentNullException(nameof(executionService));
        }

        #region Api endpoint Methods
        /// <summary>
        /// Returns the configured executions to a specific dataset.
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
                if (string.IsNullOrEmpty(idDataset))
                    return NoContent();

                List<ExecutionModel>? executions = await _executionService.GetById(idDataset);
                return Ok(executions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a execution to a specific dataset.
        /// </summary>
        /// <param name="executionModel">Execution object to add</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Add([BindRequired] ExecutionDto executionModel)
        {
            try
            {
                string result = await _executionService.Save(executionModel);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro agregado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// update a execution to a specific dataset.
        /// </summary>
        /// <param name="executionModel">Execution object to update record</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Update([BindRequired] ExecutionDto executionModel)
        {
            try
            {
                string result = await _executionService.Update(executionModel);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a execution to a specific dataset.
        /// </summary>
        /// <param name="idEjecucion">unique dataset identifier</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Remove")]
        [HttpDelete]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Remove([BindRequired] string idEjecucion)
        {
            try
            {
                string result = await _executionService.Delete(idEjecucion);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest($"Error: {result}");

                return Ok("Registro eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}
