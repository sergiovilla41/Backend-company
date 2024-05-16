using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel.Labels;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Web.Api.Controllers.Labels
{
    [Route("Labels")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsService _labelsService;

        public LabelsController(ILabelsService labelsService) {
            _labelsService = labelsService ?? throw new ArgumentNullException(nameof(labelsService));
        }
        /// <summary>
        /// Return a list of etiquetas.
        /// </summary>
        /// <response code="200">Returns a json object containing a list of etiquetas.</response>
        /// <response code="204">No data found</response>
        /// <response code="400">Returns a BadRequest object that contains an error message.</response>
        [HttpGet]
        [Route("GetLabels")]
        public async Task<IActionResult> GetLabels()
        {
            try
            {

                List<LabelsDto>? labels = await _labelsService.ListLabels();
                if (labels.Count == 0)
                {
                    return NoContent();


                }
                return Ok(labels);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves an etiqueta by its ID.
        /// </summary>
        /// <param name="id">The ID of the etiqueta to retrieve.</param>
        /// <response code="200">Returns a json object containing the etiqueta with the specified ID.</response>
        /// <response code="404">If no etiqueta is found with the specified ID.</response>
        /// <response code="400">Returns a BadRequest object that contains an error message.</response>
        [HttpGet]
        [Route("GetLabelById")]
        public async Task<IActionResult> GetLabelById(Guid id)
        {
            try
            {
                var label = await _labelsService.GetLabelById(id);
                if (label == null)
                {
                    return NotFound();
                }
                return Ok(label);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updates an etiqueta with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the etiqueta to update.</param>
        /// <param name="etiquetaDto">The data to update the etiqueta with.</param>
        /// <response code="200">Returns true if the etiqueta was updated successfully.</response>
        /// <response code="404">If no etiqueta is found with the specified ID.</response>
        /// <response code="400">Returns a BadRequest object that contains an error message.</response>
        [HttpPut]
        [Route("UpdateLabel")]
        public async Task<IActionResult> UpdateLabel([FromBody] LabelsDto labelDto)
        {
            try
            {
                var success = await _labelsService.UpdateLabel(labelDto);
                if (!success)
                {
                    return NotFound();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Add a new etiqueta.
        /// </summary>
        /// <param name="labelDto">The DTO containing information about the etiqueta to add.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost]
        [Route("InsertLabel")]
        public async Task<IActionResult> AddTag([FromBody] LabelsDto labelDto)
        {
            try
            {
                var result = await _labelsService.CreateLabel(labelDto);
                if (result)
                {
                    return Ok("Etiqueta creada correctamente");
                }
                else
                {
                    return BadRequest("Error al crear la etiqueta");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
