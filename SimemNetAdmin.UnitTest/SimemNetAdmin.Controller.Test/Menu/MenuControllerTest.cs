using EnviromentConfig;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Domain.Models.Menu;
using SimemNetAdmin.Web.Api.Controllers.v1;

namespace SimemNetAdmin.Controller.Test.Menu
{

    [TestClass]
    public class MenuControllerTest
    {
        
        public MenuControllerTest()
        {
            Connection.ConfigureConnections(); 
        }

        [TestMethod]
        public async Task Menu_Sucess_Test()
        {
            var mockServicio = new Mock<IMenuService>();
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
            List<MenuJsonModel> menuResult = new List<MenuJsonModel>();
            menuResult.Add(menu);
            mockServicio.Setup(s => s.GetRecords("simem")).ReturnsAsync(menuResult);
            MenuController menuController = new(mockServicio.Object);

            IActionResult result = await menuController.GetMenuTreeData("simem");
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }
    }
}
