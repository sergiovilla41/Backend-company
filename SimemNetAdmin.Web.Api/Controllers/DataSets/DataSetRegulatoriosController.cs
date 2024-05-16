using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimemNetAdmin.Application.Interfaces.NotificationService;
using SimemNetAdmin.Domain.Models.SeguridadTercero;

namespace SimemNetAdmin.Web.Api.Controllers.DataSets
{
    [Route("DataSets")]
    [ApiController]
    public class DataSetRegulatoriosController : ControllerBase
    {

        private readonly INotificationService _notificationService;

        public DataSetRegulatoriosController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        ///  Return a list of datasets.
        /// </summary>
        /// <response code="200">Returns a json object that containsa list of regulatory datasets</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        [Route("Regulatory")]
        [HttpGet]
        public async Task<IActionResult> GetDataSetsRecords()
        {
            try
            {
                string idEmpresa = (HttpContext.Items["company"] as string)!;
                List<Domain.ViewModel.NotificationViewModel.NotificacionDataSetRegulatorioViewModel> datasetResult = await _notificationService.GenerateSendRegulatoryDataSet(true, idEmpresa);
                if (datasetResult.Count == 0)
                    return NoContent();

                return Ok(datasetResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
