using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.ConfiguracionEjecucionService;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Web.Api.Controllers.ExecutionConfiguration;

namespace SimemNetAdmin.Controller.Test.ExecutionConfiguration
{
    [TestClass]
    public class ExecutionConfigurationControllerTest
    {
        private readonly ExecutionConfigurationController _ExecutionConfigurationController;
        private readonly Mock<IExecutionConfigurationService> _mockExecutionConfigurationService;


        public ExecutionConfigurationControllerTest()
        {
            _mockExecutionConfigurationService = new Mock<IExecutionConfigurationService>();
            _ExecutionConfigurationController = new ExecutionConfigurationController(_mockExecutionConfigurationService.Object);               
        }

        #region Test controller
        [TestMethod]
        public async Task TestByIdExtraccion_StatusCodeOK()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockExecutionConfigurationService.Setup(x => x.ExecutionConfigurationData(idExtraccion)).Returns(ByIdExtraccionResultOKTest);
            IActionResult response = await _ExecutionConfigurationController.ByIdExtraccion(idExtraccion);
            var okResult = response as OkObjectResult;
            var columnasOrigen = okResult!.Value as List<ExecutionModel>;
            Assert.IsNotNull(columnasOrigen);
            Assert.IsTrue(columnasOrigen.Count > 0);
            Assert.IsNotNull(response);
            Assert.IsTrue(response is OkObjectResult);
        }

        [TestMethod]
        public async Task TestByIdExtraccion_StatusCodeNoContent()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockExecutionConfigurationService.Setup(x => x.ExecutionConfigurationData(idExtraccion)).Returns(ByIdExtraccionResultNoContentTest);
            IActionResult response = await _ExecutionConfigurationController.ByIdExtraccion(idExtraccion);
            Assert.IsNotNull(response); // Asegúrate de que el resultado no sea nulo
            Assert.IsInstanceOfType<NoContentResult>(response);
        }

        [TestMethod]
        public async Task TestByIdExtraccion_Exception()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockExecutionConfigurationService.Setup(x => x.ExecutionConfigurationData(idExtraccion)).Returns(() => null!);
            try
            {
                IActionResult response = await _ExecutionConfigurationController.ByIdExtraccion(idExtraccion);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        #endregion





        #region Métodos privados
        private async Task<List<ExecutionModel>> ByIdExtraccionResultOKTest()
        {
            var lstResponse = new List<ExecutionModel>()
            {
                new ExecutionModel
                {
                    Dia = 5,
                    DiaSemana = null,
                    FechaActualizacion = null,
                    FechaCreacion = DateTime.Now,
                    Hora = 23,
                    IdConfiguracionGeneracionArchivos = Guid.NewGuid(),
                    IdEjecucion = Guid.NewGuid(),
                    IndActivo = true,
                    IndDiaHabil = false,
                    Mes = null
                }
            };
            return await Task.FromResult(lstResponse);
        }

        private async Task<List<ExecutionModel>> ByIdExtraccionResultNoContentTest()
        {
            var lstResponse = new List<ExecutionModel>()
            {
                
            };
            return await Task.FromResult(lstResponse);
        }
        #endregion
    }
}
