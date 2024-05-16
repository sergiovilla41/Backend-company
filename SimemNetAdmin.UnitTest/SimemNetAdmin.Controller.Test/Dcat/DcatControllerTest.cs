using EnviromentConfig;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.Dcatservice;
using SimemNetAdmin.Domain.Models.Dcat;
using SimemNetAdmin.Web.Api.Controllers.Dcat;

namespace SimemNetAdmin.Controller.Test.Dcat
{
    [TestClass]
    public class DcatControllerTest
    {
        public DcatControllerTest()
        {
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task Dcat_Sucess_Test()
        {
            Mock<IDcatService> mockServicio = new Mock<IDcatService>();
            DcatController dcatController = new(mockServicio.Object);

            DcatJsonModel dcatJsonModel = new();
            List<DcatJsonModel> dcatJsonModels = [ dcatJsonModel ];

            mockServicio.Setup(s => s.GetResource()).ReturnsAsync(dcatJsonModels);
            IActionResult result = await dcatController.GetResource();
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Dcat_NoContent_Test()
        {
            Mock<IDcatService> mockServicio = new Mock<IDcatService>();
            DcatController dcatController = new(mockServicio.Object);

            mockServicio.Setup(s => s.GetResource()).ReturnsAsync(new List<DcatJsonModel>());
            IActionResult result = await dcatController.GetResource();
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Dcat_Fail_Test()
        {
            Mock<IDcatService> mockServicio = new Mock<IDcatService>();
            DcatController dcatController = new(mockServicio.Object);
            IActionResult result = await dcatController.GetResource();
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }
    }
}
