using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Web.Api.Controllers.Extracciones;

namespace SimemNetAdmin.Controller.Test.Extraction
{
    [TestClass]
    public class ExtractionControllerTest
    {
        private readonly ExtractionController _extractionController;
        private readonly Mock<IExtractionService> _mockExtractionService;

        public ExtractionControllerTest()
        {
            _mockExtractionService = new Mock<IExtractionService>();
            _extractionController = new ExtractionController(_mockExtractionService.Object);
        }

        //#region Test controller
        //[TestMethod]
        //public async Task TestSelectProperties_StatusCodeOK()
        //{
        //    string type = "columnadestino";
        //    string? project = "";
        //    _mockExtractionService.Setup(x => x.SelectProperties(type, project)).Returns(SelectPropertiesResultOKTest);
        //    IActionResult response = await _extractionController.SelectProperties(type, project);
        //    var okResult = response as OkObjectResult;
        //    var columnasOrigen = okResult!.Value as List<SelectPropertiesJson>;
        //    Assert.IsNotNull(columnasOrigen);
        //    Assert.IsTrue(columnasOrigen.Count > 0);
        //    Assert.IsNotNull(response);
        //    Assert.IsTrue(response is OkObjectResult);
        //}

        //[TestMethod]
        //public async Task TestSelectProperties_Exception()
        //{
        //    string type = "columnaorigen"; 
        //    string? project = "";
        //    _mockExtractionService.Setup(x => x.SelectProperties(type, project)).Returns(() => null!);
        //    try
        //    {
        //        IActionResult response = await _extractionController.SelectProperties(type, project);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsNotNull(ex);
        //    }
        //}
        //#endregion

        //#region Métodos privados
        //private async Task<List<SelectPropertiesJson>> SelectPropertiesResultOKTest()
        //{
        //    var lstResponse = new List<SelectPropertiesJson>()
        //    {
        //        new SelectPropertiesJson {
        //            descripcion = "Porcentaje de cobertura de la demanda regulada y no regulada con contratos fuera del SICEP y con atención directa de la demanda (sin intermediación)",
        //            Id = null,
        //            IdColumnasDestino = Guid.NewGuid(),
        //            IdConfiguracionclasificacionregulatoria = 0,
        //            TipoDato = "flotante",
        //            Value = "PrcjEstimadoCobertura"
        //        }
        //    };
        //    return await Task.FromResult(lstResponse);
        //}

        //#endregion
    }
}
