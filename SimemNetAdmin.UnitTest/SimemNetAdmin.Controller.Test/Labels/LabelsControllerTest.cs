using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Web.Api.Controllers.Labels;

namespace SimemNetAdmin.Controller.Test.Labels
{
    [TestClass]
    public class LabelsControllerTest
    {
        private Mock<ILabelsService> _labelsServiceMock;
        private LabelsController _controller;

        public LabelsControllerTest()
        {
            _labelsServiceMock = new Mock<ILabelsService>();
            _controller = new LabelsController(_labelsServiceMock.Object);

        }

        [TestInitialize]
        public void Setup()
        {
            _labelsServiceMock = new Mock<ILabelsService>();
            _controller = new LabelsController(_labelsServiceMock.Object);
        }

        [TestMethod]
        [TestCategory("ListarEtiquetas")]
        public async Task GetLabels_Returns_OkResult_With_Data()
        {
            // Arrange
            var labelsDtoList = new List<LabelsDto>
            {
                new LabelsDto { Id = Guid.NewGuid(), Titulo = "Etiqueta1", Estado = true },
                new LabelsDto { Id = Guid.NewGuid(), Titulo = "Etiqueta2", Estado = true }
            };
            _labelsServiceMock.Setup(x => x.ListLabels()).ReturnsAsync(labelsDtoList);

            // Act
            var result = await _controller.GetLabels() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(labelsDtoList, result.Value);
        }

        [TestMethod]
        [TestCategory("ListarEtiquetas")]
        public async Task GetLabels_Returns_NoContent_When_No_Data()
        {
            // Arrange
            _labelsServiceMock.Setup(x => x.ListLabels()).ReturnsAsync(new List<LabelsDto>());

            // Act
            var result = await _controller.GetLabels() as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("ListarEtiquetas")]
        public async Task GetLabel_Returns_BadRequest_When_Exception()
        {
            // Arrange
            _labelsServiceMock.Setup(x => x.ListLabels()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetLabels() as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Test exception", result.Value);
        }

        [TestMethod]
        [TestCategory("ListarPorId")]
        public async Task GetLabelById_Returns_OkResult_With_Data()
        {
            // Arrange
            var etiquetaDto = new LabelsDto { Id = Guid.NewGuid(), Titulo = "Etiqueta1", Estado = true };
            _labelsServiceMock.Setup(x => x.GetLabelById(It.IsAny<Guid>())).ReturnsAsync(etiquetaDto);
            var id = Guid.NewGuid();

            // Act
            var result = await _controller.GetLabelById(id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(etiquetaDto, result.Value);
        }

        [TestMethod]
        [TestCategory("ListarPorId")]
        public async Task GetLabelById_Returns_NotFound_When_No_Data()
        {
            // Arrange
            _labelsServiceMock.Setup(x => x.GetLabelById(It.IsAny<Guid>())).Returns(Task.FromResult<LabelsDto>(null!));
            var id = Guid.NewGuid();

            // Act
            var result = await _controller.GetLabelById(id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        [TestCategory("ListarPorId")]
        public async Task GetLabelById_Returns_BadRequest_When_Exception()
        {
            // Arrange
            _labelsServiceMock.Setup(x => x.GetLabelById(It.IsAny<Guid>())).ThrowsAsync(new Exception("Test exception"));
            var id = Guid.NewGuid();

            // Act
            var result = await _controller.GetLabelById(id) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Test exception", result.Value);
        }

        [TestMethod]
        [TestCategory("AgregarEtiqueta")]
        public async Task AddTag_Returns_OkResult_When_Tag_Added_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid(); // ID deseado para la nueva etiqueta
            var etiquetaDto = new LabelsDto { Id = id, Titulo = "test unit1", Estado = true };
            var etiquetasServiceMock = new Mock<ILabelsService>();
            etiquetasServiceMock.Setup(x => x.CreateLabel(It.IsAny<LabelsDto>())).ReturnsAsync(true);
            var controller = new LabelsController(etiquetasServiceMock.Object);

            // Act
            var result = await controller.AddTag(etiquetaDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Etiqueta creada correctamente", result.Value);
        }

        [TestMethod]
        [TestCategory("AgregarEtiqueta")]
        public async Task AddTag_Returns_BadRequest_When_Tag_Creation_Fails()
        {
            // Arrange
            var etiquetaDto = new LabelsDto(); // Fill with required properties
            _labelsServiceMock.Setup(x => x.CreateLabel(It.IsAny<LabelsDto>())).ReturnsAsync(false);

            // Act
            var result = await _controller.AddTag(etiquetaDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Error al crear la etiqueta", result.Value);
        }

        [TestMethod]
        [TestCategory("ActualizarEtiqueta")]
        public async Task ActualizarEtiqueta_Returns_OkResult_When_Tag_Updated_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid(); // ID de la etiqueta a actualizar
            var etiquetaDto = new LabelsDto { Titulo = "test unit1 modificado", Estado = false }; // Nuevos datos de la etiqueta
            var etiquetasServiceMock = new Mock<ILabelsService>();
            etiquetasServiceMock.Setup(x => x.UpdateLabel(etiquetaDto)).ReturnsAsync(true);
            var controller = new LabelsController(etiquetasServiceMock.Object);

            // Act
            var result = await controller.UpdateLabel(etiquetaDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("ActualizarEtiqueta")]
        public async Task UpdateLabel_Returns_NotFound_When_Tag_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid(); // ID de una etiqueta que no existe
            var etiquetaDto = new LabelsDto(); // Nuevos datos de la etiqueta
            _labelsServiceMock.Setup(x => x.UpdateLabel(etiquetaDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateLabel(etiquetaDto) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("ActualizarEtiqueta")]
        public async Task UpdateLabel_Returns_BadRequest_When_Exception()
        {
            // Arrange
            var id = Guid.NewGuid(); // ID de la etiqueta a actualizar
            var etiquetaDto = new LabelsDto(); // Nuevos datos de la etiqueta
            _labelsServiceMock.Setup(x => x.UpdateLabel(etiquetaDto)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.UpdateLabel(etiquetaDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Test exception", result.Value);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Limpiar recursos si es necesario
        }
    }
}
