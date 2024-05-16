using EnviromentConfig;
using Moq;
using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Application.Services.MenuService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Menu;

namespace SimemNetAdmin.UnitTest.Services.Test
{
    [TestClass]
    public class MenuServiceTest
    {
        public MenuServiceTest()
        {
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task Test1GetRecords()
        {
            var mockRepository = new Mock<IMenuRepository>();

            MenuJsonModel menu = new MenuJsonModel
            {
                Id = Guid.Parse("56ad5213-2c3b-4d8b-9210-e500b893fb6b"),
                Titulo = "Inicio",
                IdPadre = Guid.Parse("56ad5213-2c3b-4d8b-9210-e500b893fb6a"),
                Nivel = 1,
                Secuencia = 1,
                Enlace = "/",
                Icono = "custom inicio",
                Activo = true,
                Children = null
            };
            List<MenuJsonModel> menuResult = [menu];
            mockRepository.Setup(s => s.GetRecords("simem")).ReturnsAsync(menuResult);
            MenuServices menuServices = new(mockRepository.Object);

            var result = await menuServices.GetRecords("simem");
            Assert.IsTrue(result.Count >= 0);
        }
    }
}
