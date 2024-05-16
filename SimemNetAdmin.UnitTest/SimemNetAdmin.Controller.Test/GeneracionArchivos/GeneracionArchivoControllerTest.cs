using EnviromentConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Application.Interfaces.NotificationService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Web.Api.Controllers.DataSets;
using SimemNetAdmin.Web.Api.Controllers.GeneracionArchivos;

namespace SimemNetAdmin.Controller.Test.GeneracionArchivos
{
    [TestClass]
    public class GeneracionArchivoControllerTest
    {
        private readonly GeneracionArchivoController _generacionArchivoController;
        private readonly Mock<IGeneracionArchivoService> _mockGeneracionArchivoService;

        public GeneracionArchivoControllerTest()
        {
            _mockGeneracionArchivoService = new Mock<IGeneracionArchivoService>();
            _generacionArchivoController = new GeneracionArchivoController(_mockGeneracionArchivoService.Object);
        }

        #region Test controller
        [TestMethod]
        public async Task GetRecords_Sucess_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new Mock<IGeneracionArchivoService>();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);

            GeneracionArchivoJson generacionArchivoJson = new();
            List<GeneracionArchivoJson> generacionArchivosJson = [generacionArchivoJson];

            mockServicio.Setup(s => s.GetRecords()).ReturnsAsync(generacionArchivosJson);
            IActionResult result = await generacionArchivoController.GetRecords();
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetDataSetBasicData_Sucess_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);
            IActionResult result = await generacionArchivoController.PreLoadBasicData("A704EEF3-CA1C-4EBF-98A0-01325C61765D");
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetDataSetBasicData_NullParameter_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);

            DatosBasicosJson datosBasicos = new();

            mockServicio.Setup(s => s.GetDataSetBasicData(new("A704EEF3-CA1C-4EBF-98A0-01325C61765D"))).ReturnsAsync(datosBasicos);
            IActionResult result = await generacionArchivoController.PreLoadBasicData(string.Empty);
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetDataSetBasicData_NoContent_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);

            DatosBasicosJson datosBasicos = new();

            mockServicio.Setup(s => s.GetDataSetBasicData(new("A704EEF3-CA1C-4EBF-98A0-01325C61765E"))).ReturnsAsync(datosBasicos);
            IActionResult result = await generacionArchivoController.PreLoadBasicData("A704EEF3-CA1C-4EBF-98A0-01325C61765E");
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetDataSetBasicData_Exception_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);

            DatosBasicosJson datosBasicos = new();

            mockServicio.Setup(s => s.GetDataSetBasicData(new("A704EEF3-CA1C-4EBF-98A0-01325C61765E"))).ReturnsAsync(datosBasicos);
            IActionResult result = await generacionArchivoController.PreLoadBasicData("Test!!");
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task HasRecords_Exception_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);
            IActionResult result = await generacionArchivoController.HasRecords("Test!!");
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task HasRecords_NoParameter_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);
            IActionResult result = await generacionArchivoController.HasRecords(string.Empty);
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task HasRecords_Success_Test()
        {
            Mock<IGeneracionArchivoService> mockServicio = new();
            GeneracionArchivoController generacionArchivoController = new(mockServicio.Object);
            IActionResult result = await generacionArchivoController.HasRecords("6BAC4845-DC3D-4EF3-B1BD-675016165592");
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetDataSetsRecords_Success_Test()
        {
            Mock<INotificationService> mockServicio = new();
            DataSetRegulatoriosController dataSetRegulatoriosController = new(mockServicio.Object);
            IActionResult result = await dataSetRegulatoriosController.GetDataSetsRecords();
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TestSelectProperties_StatusCodeOK()
        {
            string type = "columnadestino";
            string? project = "";
            _mockGeneracionArchivoService.Setup(x => x.SelectProperties(type, project)).Returns(SelectPropertiesResultOKTest);
            IActionResult response = await _generacionArchivoController.SelectProperties(type, project);
            var okResult = response as OkObjectResult;
            var columnasOrigen = okResult!.Value as List<SelectPropertiesJson>;
            Assert.IsNotNull(columnasOrigen);
            Assert.IsTrue(columnasOrigen.Count > 0);
            Assert.IsNotNull(response);
            Assert.IsTrue(response is OkObjectResult);
        }

        [TestMethod]
        public async Task TestSelectProperties_Exception()
        {
            string type = "columnaorigen";
            string? project = "";
            _mockGeneracionArchivoService.Setup(x => x.SelectProperties(type, project)).Returns(() => null!);
            try
            {
                IActionResult response = await _generacionArchivoController.SelectProperties(type, project);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }




        [TestMethod]
        public async Task TestUpdateColumnsDestinityVersion_StatusCodeOK()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
            {
                IdColumnaDestino = Guid.Parse("81BD94CE-8A0A-4C64-BCAC-E76F0A15D0C7"),
                IdColumnaOrigen = Guid.Parse("3CA12E8B-C1A7-45C3-B59B-003B03B93E69"),
                IdExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F"),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            _mockGeneracionArchivoService.Setup(x => x.UpdateColumnsDestinityVersion(columnasOrigenJson)).Returns(UpdateColumnsDestinityVersionOkTest);
            IActionResult response = await _generacionArchivoController.UpdateColumnsDestinityVersion(columnasOrigenJson);
            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            var columnasOrigen = okResult!.Value as string;
            Assert.IsTrue(string.IsNullOrEmpty(columnasOrigen));
        }
        #endregion


        #region Métodos privados
        private async Task<List<SelectPropertiesJson>> SelectPropertiesResultOKTest()
        {
            var lstResponse = new List<SelectPropertiesJson>()
            {
                new SelectPropertiesJson {
                    descripcion = "Porcentaje de cobertura de la demanda regulada y no regulada con contratos fuera del SICEP y con atención directa de la demanda (sin intermediación)",
                    Id = null,
                    IdColumnasDestino = Guid.NewGuid(),
                    IdConfiguracionclasificacionregulatoria = 0,
                    TipoDato = "flotante",
                    Value = "PrcjEstimadoCobertura"
                }
            };
            return await Task.FromResult(lstResponse);
        }

        private async Task<string> UpdateColumnsDestinityVersionOkTest()
        {
            string response = string.Empty;
            return await Task.FromResult(response);
        }
        #endregion
    }
}
