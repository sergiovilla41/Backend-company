using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Web.Api.Controllers.AssociatedDataSet;

namespace SimemNetAdmin.Controller.Test.AssociatedDataSet
{
    [TestClass]
    public class AssociatedDataSetTest
    {
        private Mock<IAssociatedDataSetService> _associatedDataSetService;
        private AssociatedDataSetController _controller;
        public AssociatedDataSetTest()
        {
            _associatedDataSetService = new Mock<IAssociatedDataSetService>();
            _controller = new AssociatedDataSetController(_associatedDataSetService.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _associatedDataSetService = new Mock<IAssociatedDataSetService>();
            _controller = new AssociatedDataSetController(_associatedDataSetService.Object);
        }

        [TestMethod]
        [TestCategory("GetConjuntoDatos")]
        public async Task GetConjuntoDatos_Returns_OkResult_With_Data()
        {
            // Arrange
            var conjuntoDatosDtoList = new List<LabelDataSetDto>
            {
                new LabelDataSetDto
  {
    Id = Guid.Parse("931d1f74-fe46-492c-9e62-01ac68a4c3bd"),
    Titulo = "Aportes Hídricos",
    Estado =  true,
    GeneracionArchivos = new List<Domain.ViewModel.FileGenerationModel.FileGenerationModelDto>
    {
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hidráulicos Porcentaje"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hídricos en Porcentaje"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hídricos en Energía"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hídricos en Masa"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hidráulicos Energía"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Energético Mediano Plazo"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Energético Largo Plazo"
        },
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Aportes Hidráulicos"
        },
    }
  },
                 new LabelDataSetDto
  {
                     Id = Guid.Parse("305eca0e-6f02-40e4-8ded-084d76024309"),
    Titulo = "Mercado regulado",
    Estado =  true,
    GeneracionArchivos = new List<Domain.ViewModel.FileGenerationModel.FileGenerationModelDto>
    {
        new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
        {
        Titulo = "Datos soporte del proceso de liquidación por Código sic agente, Recurso, Tipo combustible, versión y mensual"
        }
    }
                 }
            };
            _associatedDataSetService.Setup(x => x.GetDataDto()).ReturnsAsync(conjuntoDatosDtoList);

            // Act
            var actionResult = await _controller.GetDataSet();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<List<LabelDataSetDto>>));

            var okObjectResult = actionResult.Result as OkObjectResult;
            if (okObjectResult != null)
            {
                Assert.IsInstanceOfType(okObjectResult.Value, typeof(List<LabelDataSetDto>));
                var resultValue = okObjectResult.Value as List<LabelDataSetDto>;
                Assert.IsNotNull(resultValue);
                Assert.AreEqual(conjuntoDatosDtoList.Count, resultValue.Count);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }
        [TestMethod]
        [TestCategory("GetConjuntoDatos")]
        public async Task GetConjuntoDatos_Returns_NoContent_When_No_Data()
        {
            // Arrange
            _associatedDataSetService.Setup(x => x.GetDataDto()).ReturnsAsync(new List<LabelDataSetDto>());

            // Act
            var result = await _controller.GetDataSet();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        [TestCategory("GetConjuntoDatos")]
        public async Task GetConjuntoDatos_Returns_BadRequest_When_Exception()
        {
            // Arrange
            var errorMessage = "Test exception";
            _associatedDataSetService.Setup(x => x.GetDataDto()).ThrowsAsync(new Exception(errorMessage));

            // Act
            var actionResult = await _controller.GetDataSet();
            var result = actionResult.Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(errorMessage, result.Value);
        }
    }
}
