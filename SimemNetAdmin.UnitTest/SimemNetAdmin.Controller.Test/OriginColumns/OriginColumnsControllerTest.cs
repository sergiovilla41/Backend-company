using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Web.Api.Controllers.OriginColumns;

namespace SimemNetAdmin.Controller.Test.ColumnasOrigen
{
    [TestClass]
    public class OriginColumnsControllerTest
    {
        private readonly OriginColumnsController _columnasOrigenController;
        private readonly Mock<IOriginColumnsService> _mockColumnasOrigenService;

        public OriginColumnsControllerTest()
        {
            _mockColumnasOrigenService = new Mock<IOriginColumnsService>();
            _columnasOrigenController = new OriginColumnsController(_mockColumnasOrigenService.Object);
        }

        #region Test controller
        [TestMethod]
        public async Task TestByIdExtraccion_StatusCodeOK()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockColumnasOrigenService.Setup(x => x.GetColumnasOrigenJson(idExtraccion)).Returns(ByIdExtraccionResultTest);
            IActionResult response = await _columnasOrigenController.ByIdExtraccion(idExtraccion);
            var okResult = response as OkObjectResult;
            var columnasOrigen = okResult!.Value as List<ConfiguracionColumnasOrigenJson>;
            Assert.IsNotNull(columnasOrigen);
            Assert.IsTrue(columnasOrigen.Count > 0);
            Assert.IsNotNull(response);
            Assert.IsTrue(response is OkObjectResult);
        }

        [TestMethod]
        public async Task TestByIdExtraccion_Exception()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockColumnasOrigenService.Setup(x => x.GetColumnasOrigenJson(idExtraccion)).Returns(() => null!);
            try
            {
                IActionResult response = await _columnasOrigenController.ByIdExtraccion(idExtraccion);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }



        [TestMethod]
        public async Task TestUpdate_StatusCodeOK()
        {
            ConfiguracionColumnasOrigenJson configuracionColumnasOrigenJson = new ConfiguracionColumnasOrigenJson()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };

            _mockColumnasOrigenService.Setup(x => x.UpdateColumnasOrigen(configuracionColumnasOrigenJson)).Returns(UpdateOKResultTest);
            IActionResult response = await _columnasOrigenController.Update(configuracionColumnasOrigenJson);

            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(response is OkObjectResult);
        }

        [TestMethod]
        public async Task TestUpdate_StatusCodeBadRequest()
        {
            ConfiguracionColumnasOrigenJson configuracionColumnasOrigenJson = new ConfiguracionColumnasOrigenJson()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };

            _mockColumnasOrigenService.Setup(x => x.UpdateColumnasOrigen(configuracionColumnasOrigenJson)).Returns(UpdateBadRequestResultTest);
            IActionResult response = await _columnasOrigenController.Update(configuracionColumnasOrigenJson);

            var okResult = response as OkObjectResult;
            Assert.IsNull(okResult);
        }

        [TestMethod]
        public async Task TestUpdate_Exception()
        {
            ConfiguracionColumnasOrigenJson configuracionColumnasOrigenJson = new ConfiguracionColumnasOrigenJson()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            _mockColumnasOrigenService.Setup(x => x.UpdateColumnasOrigen(configuracionColumnasOrigenJson)).Returns(() => null!);
            try
            {
                IActionResult response = await _columnasOrigenController.Update(configuracionColumnasOrigenJson);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        #endregion



        #region Métodos privados
        private async Task<List<ConfiguracionColumnasOrigenJson>> ByIdExtraccionResultTest()
        {
            var lstConfiguracionColumnasOrigen = new List<ConfiguracionColumnasOrigenJson>()
            {
                new ConfiguracionColumnasOrigenJson(){
                IdColumnaOrigen = Guid.NewGuid(),
                Numeracion = 3,
                ColumnaOrigen = "",
                IdColumnaDestino = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                }
            };
            return await Task.FromResult(lstConfiguracionColumnasOrigen);
        }

        private async Task<string> UpdateOKResultTest()
        {
            var response = string.Empty;
            return await Task.FromResult(response);
        }

        private async Task<string> UpdateBadRequestResultTest()
        {
            var response = "Error";
            return await Task.FromResult(response);
        }
        #endregion
    }
}
