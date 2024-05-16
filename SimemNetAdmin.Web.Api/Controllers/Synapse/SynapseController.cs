using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Transversal.Helper;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.Synapse
{
    [Route("Synapse")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class SynapseController : Controller
    {
        private readonly IExecutionLogService _executionLogService;

        public SynapseController(IExecutionLogService executionLogService)
        {
            _executionLogService = executionLogService ?? throw new ArgumentNullException(nameof(executionLogService));
        }

        /// <summary>
        /// Execute a new pipeline run.
        /// </summary>
        /// <param name="nbSynapseName">Notebook to execute</param>
        /// <response code="200">Returns the pipeline execution id</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("Execute")]
        public IActionResult ExecutePipeline(string nbSynapseName)
        {
            try
            {
                string executionResult = SynapseHelper.ExecutePipeline(nbSynapseName);
                return Ok(executionResult);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Cancel a running pipeline.
        /// </summary>
        /// <param name="pipelineRunId">Pipeline running identifier</param>
        /// <response code="200">Returns the pipeline execution id</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("Cancel")]
        public async Task<IActionResult> CancelRunningPipeline(string pipelineRunId)
        {

            string executionResult = SynapseHelper.CancelPipelineRunning(pipelineRunId);
            if (executionResult.Contains("Error:"))
                return new BadRequestObjectResult(executionResult);

            string? result = await _executionLogService.CancelPipeline(pipelineRunId);

            if (string.IsNullOrEmpty(result))
                return Ok(executionResult);

            return new BadRequestObjectResult($"Error: {result}");
        }
    }
}
