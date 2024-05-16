using Azure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.ExecutionService;
using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Web.Api.Controllers.Executions;
using SimemNetAdmin.Web.Api.Controllers.GeneracionArchivos;

namespace SimemNetAdmin.Controller.Test.Executions
{
    [TestClass]
    public class ExecutionControllerTest
    {
        private readonly ExecutionController _executionController;
        private readonly Mock<IExecutionService> _mockExecutionService;

        public ExecutionControllerTest()
        {
            _mockExecutionService = new Mock<IExecutionService>();
            _executionController = new ExecutionController(_mockExecutionService.Object);
        }

        [TestMethod]
        public async Task GetRecords_Sucess_Test()
        {
            Mock<IExecutionService> mockServicio = new Mock<IExecutionService>();
            ExecutionController controller = new(mockServicio.Object);

            ExecutionModel model = new();
            List<ExecutionModel> modelObject = [model];

            mockServicio.Setup(s => s.GetById("42D664EC-56BB-40E3-ACAC-A745F505172A")).ReturnsAsync(modelObject);
            IActionResult result = await controller.GetRecordsById("42D664EC-56BB-40E3-ACAC-A745F505172A");
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetRecords_NoContent_Test()
        {
            Mock<IExecutionService> mockServicio = new Mock<IExecutionService>();
            ExecutionController controller = new(mockServicio.Object);

            ExecutionModel model = new();
            List<ExecutionModel> modelObject = [model];

            mockServicio.Setup(s => s.GetById(string.Empty)).ReturnsAsync(modelObject);
            IActionResult result = await controller.GetRecordsById(string.Empty);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NoContentResult>(result);
        }
    }
}
