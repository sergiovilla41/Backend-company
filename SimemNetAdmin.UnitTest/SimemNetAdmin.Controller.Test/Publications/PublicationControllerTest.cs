using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.ExecutionService;
using SimemNetAdmin.Application.Interfaces.PublicationService;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.Models.Publication;
using SimemNetAdmin.Web.Api.Controllers.Executions;
using SimemNetAdmin.Web.Api.Controllers.Publications;

namespace SimemNetAdmin.Controller.Test.Publications
{
    [TestClass]
    public class PublicationControllerTest
    {
        private readonly PublicationController _publicationController;
        private readonly Mock<IPublicationService> _mockPublicationService;

        public PublicationControllerTest()
        {
            _mockPublicationService = new Mock<IPublicationService>();
            _publicationController = new PublicationController(_mockPublicationService.Object);
        }

        [TestMethod]
        public async Task GetRecords_Sucess_Test()
        {
            Mock<IPublicationService> mockServicio = new Mock<IPublicationService>();
            PublicationController controller = new(mockServicio.Object);

            PublicationModel model = new();
            List<PublicationModel> modelObject = [model];

            mockServicio.Setup(s => s.GetById("ade905f7-cb05-4ff9-90d9-00979f798894")).ReturnsAsync(modelObject);
            IActionResult result = await controller.GetRecordsById("ade905f7-cb05-4ff9-90d9-00979f798894");
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetRecords_NoContent_Test()
        {
            Mock<IPublicationService> mockServicio = new Mock<IPublicationService>();
            PublicationController controller = new(mockServicio.Object);

            PublicationModel model = new();
            List<PublicationModel> modelObject = [model];

            mockServicio.Setup(s => s.GetById(string.Empty)).ReturnsAsync(modelObject);
            IActionResult result = await controller.GetRecordsById(string.Empty);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NoContentResult>(result);
        }
    }
}
