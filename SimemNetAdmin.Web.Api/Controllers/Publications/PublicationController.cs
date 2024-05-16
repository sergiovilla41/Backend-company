using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimemNetAdmin.Application.Interfaces.PublicationService;
using SimemNetAdmin.Domain.Models.Publication;
using SimemNetAdmin.Domain.ViewModel.Publication;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.Publications
{
    [Route("Publication")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
        }

        #region Api endpoint Methods
        /// <summary>
        /// Returns the configured publications to a specific dataset.
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

                List<PublicationModel>? publications = await _publicationService.GetById(idDataset);
                return Ok(publications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a publication to a specific dataset.
        /// </summary>
        /// <param name="publicationModel">Publication object to add</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Add([BindRequired] PublicationDto publicationModel)
        {
            try
            {
                string result = await _publicationService.Save(publicationModel);
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
        /// update a publication to a specific dataset.
        /// </summary>
        /// <param name="publicationModel">Publication object to update record</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Update([BindRequired] PublicationDto publicationModel)
        {
            try
            {
                string result = await _publicationService.Update(publicationModel);
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
        /// Delete a publication to a specific dataset.
        /// </summary>
        /// <param name="publicationModel">Publication object to remove record</param>
        /// <response code="200">Returns a json object contains a list of records saved</response>
        /// <response code="400">Returns a Badrequest object that constains a error message.</response>
        /// <returns></returns>
        [Route("Remove")]
        [HttpDelete]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> Remove([BindRequired] string idPublicacionRegulatoria)
        {
            try
            {
                string result = await _publicationService.Delete(idPublicacionRegulatoria);
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
