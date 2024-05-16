using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Application.Interfaces.NotificationService;
using SimemNetAdmin.Application.Services.NotificationService;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.v1
{
    [Route("Monitoring")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class MonitoringController : ControllerBase
    {
        private readonly INotificationService _NotificationService;

        public MonitoringController(INotificationService notificationService)
        {
            _NotificationService = notificationService;
        }

        /// <summary>
        ///  Return a list corresponding to the execution and error monitoring.
        /// </summary>
        /// <response code="200">Returns a json object containing a list corresponding to the execution and error monitoring.</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        [Route("ExecutionAndError")]
        [HttpGet]
        public async Task<IActionResult> GetExecutionAndErrorMonitoring()
        {
            try
            {
                List<Domain.ViewModel.NotificationViewModel.ExecutionAndErrorMonitoringViewModel> executionMonitoringResult = await _NotificationService.GetExecutionAndErrorMonitoring();
                if (executionMonitoringResult.Count == 0)
                    return NoContent();
                return Ok(executionMonitoringResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
