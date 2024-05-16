using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces.Dcatservice;
using SimemNetAdmin.Domain.Models.Dcat;

namespace SimemNetAdmin.Web.Api.Controllers.Dcat
{
    [Route("Dcat")]
    [ApiController]
    public class DcatController : ControllerBase
    {
        #region Constructor
        private readonly IDcatService _dcatService;
        public DcatController(IDcatService dcatService)
        {
            _dcatService = dcatService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a dcat properties json format.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetResource()
        {
            try
            {
                List<DcatJsonModel> dcatResult = await _dcatService.GetResource();
                if (dcatResult.Count == 0)
                    return NoContent();

                return Ok(dcatResult[0]);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}