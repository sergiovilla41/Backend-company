using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Transversal.Helper;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.DataSets
{
    /// <summary>
    /// Gets the data set items from database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("DataSet")]
    public class DataSetController(IGeneracionArchivoService generacionArchivoService) : Controller
    {
        #region Api endpoint Methods
        /// <summary>
        ///  Return a list of values to generate a list of data set by user.
        /// </summary>
        /// <response code="200">Returns a json object that contains the data set records by user</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        [HttpGet]
        public async Task<IActionResult> GetDataSet(int? pageIndex, int? pageSize)
        {
            try
            {
                Guid? company = Guid.Parse((HttpContext.Items["company"] as string)!);
                Paginator? paginator = null;
                if (pageIndex != null && pageSize != null)
                {
                    paginator = new Paginator() { PageIndex = (int)pageIndex, PageSize = (int)pageSize };
                }


                List<GeneracionArchivoDto> result = await generacionArchivoService.GetDataList(paginator, company);
                if (result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}
