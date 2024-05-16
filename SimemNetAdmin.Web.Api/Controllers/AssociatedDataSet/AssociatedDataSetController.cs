using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Labels;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.AssociatedDataSet
{
    [Route("AssociatedDataSet")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class AssociatedDataSetController : ControllerBase
    {
        private readonly IAssociatedDataSetService _associatedDataSetService;

        public AssociatedDataSetController(IAssociatedDataSetService associatedDataSetService)
        {
            _associatedDataSetService = associatedDataSetService ?? throw new ArgumentNullException(nameof(associatedDataSetService));
        }
        /// <summary>
        /// Retrieves a list of conjunto de datos.
        /// </summary>
        /// <returns>A list of conjunto de datos.</returns>
        /// <response code="200">Returns a JSON object containing a list of conjunto de datos.</response>
        /// <response code="204">No content found.</response>
        /// <response code="400">Returns a BadRequest object that contains an error message.</response>
        [HttpGet]
        [Route("GetDataSet")]
        public async Task<ActionResult<List<LabelDataSetDto>>> GetDataSet()
        {
            try
            {
                var data = await _associatedDataSetService.GetDataDto();

                if (data.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
