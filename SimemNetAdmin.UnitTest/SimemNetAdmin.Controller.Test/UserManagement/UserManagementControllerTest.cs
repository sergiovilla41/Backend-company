using Microsoft.AspNetCore.Mvc;
using Moq;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using SimemNetAdmin.Web.Api.Controllers.UserManagement;

namespace SimemNetAdmin.Controller.Test.UserManagement
{
    [TestClass]
    public class UserManagementControllerTest
    {
        private readonly UserManagementController _userManagementController;
        private readonly Mock<IUserManagementService> _mockUserManagementService;

        public UserManagementControllerTest()
        {
            _mockUserManagementService = new Mock<IUserManagementService>();
            _userManagementController = new UserManagementController(_mockUserManagementService.Object);
        }

        #region Test controller
        [TestMethod]
        public async Task TestUpdateUser_StatusCodeOK()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.UpdateUser(gestionUsuarios)).Returns(UpdateOKResultTest);
            IActionResult response = await _userManagementController.UpdateUser(gestionUsuarios);

            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(response is OkObjectResult);
        }


        [TestMethod]
        public async Task TestUpdateUser_StatusCodeBadRequest()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.UpdateUser(gestionUsuarios)).Returns(UpdateBadRequestResultTest);
            IActionResult response = await _userManagementController.UpdateUser(gestionUsuarios);

            var okResult = response as OkObjectResult;
            Assert.IsNull(okResult);
        }


        [TestMethod]
        public async Task TestUpdateUser_StatusCodeException()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.UpdateUser(gestionUsuarios)).Returns(() => null!);
            try
            {
                IActionResult response = await _userManagementController.UpdateUser(gestionUsuarios);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }




        [TestMethod]
        public async Task TestInsertUser_StatusCodeOK()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.InsertUser(gestionUsuarios)).Returns(InsertUserOKResultTest);
            IActionResult response = await _userManagementController.InsertUser(gestionUsuarios);

            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(response is OkObjectResult);
        }


        [TestMethod]
        public async Task TestInsertUser_StatusCodeBadRequest()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.InsertUser(gestionUsuarios)).Returns(UpdateBadRequestResultTest);
            IActionResult response = await _userManagementController.InsertUser(gestionUsuarios);

            var okResult = response as OkObjectResult;
            Assert.IsNull(okResult);
        }


        [TestMethod]
        public async Task TestInsertUser_StatusCodeException()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = "Prueba insert",
                Correo = "mario.rodriguez",
                Telefono = "123654789",
                Observacion = "Prueba creación usuario",
                APP = "DEV",
                Estado = "Activo",
                FechaIniUsuario = DateTime.Now,
                FechaFinUsuario = null,
                Permisos = "ALL",
                IsAdmin = true
            };

            _mockUserManagementService.Setup(x => x.InsertUser(gestionUsuarios)).Returns(() => null!);
            try
            {
                IActionResult response = await _userManagementController.InsertUser(gestionUsuarios);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }




        [TestMethod]
        public async Task TestGetUser_StatusCodeOK()
        {
            _mockUserManagementService.Setup(x => x.GetUsers()).Returns(GetUsersOKResultTest);
            IActionResult response = await _userManagementController.GetUsers();

            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(response is OkObjectResult);
            var users = okResult!.Value as List<GestionUsuarios>;
            Assert.IsTrue(users!.Count > 0);
        }


        [TestMethod]
        public async Task TestGetUsers_StatusCodeBadRequest()
        {
            _mockUserManagementService.Setup(x => x.GetUsers()).Returns(GetUsersBadRequestResultTest);
            IActionResult response = await _userManagementController.GetUsers();

            var okResult = response as OkObjectResult;
            Assert.IsNull(okResult);
        }


        [TestMethod]
        public async Task TestGettUser_StatusCodeException()
        {
            _mockUserManagementService.Setup(x => x.GetUsers()).Returns(() => null!);
            try
            {
                IActionResult response = await _userManagementController.GetUsers();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public async Task TestConsultCompanyByDomain_StatusCodeOK()
        {
            _mockUserManagementService.Setup(x => x.ConsultCompanyByDomain()).Returns(ConsultCompanyByDomainOKResultTest);
            IActionResult response = await _userManagementController.ConsultCompanyByDomain();

            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(response is OkObjectResult);
            var users = okResult!.Value as List<EmpresaDominio>;
            Assert.IsTrue(users!.Count > 0);
        }


        [TestMethod]
        public async Task TestConsultCompanyByDomain_StatusCodeBadRequest()
        {
            _mockUserManagementService.Setup(x => x.ConsultCompanyByDomain()).Returns(ConsultCompanyByDomainBadRequestResultTest);
            IActionResult response = await _userManagementController.ConsultCompanyByDomain();

            var okResult = response as BadRequestObjectResult;
            Assert.AreEqual(400, okResult!.StatusCode);
            string expectedMessage = "Error: No se obtuvo información de la tabla";
            string? resultMsg = okResult.Value != null ? okResult.Value!.ToString() : string.Empty;
            Assert.IsTrue(resultMsg!.Contains(expectedMessage));
        }


        [TestMethod]
        public async Task TestConsultCompanyByDomain_StatusCodeException()
        {
            _mockUserManagementService.Setup(x => x.ConsultCompanyByDomain()).Returns(() => null!);
            try
            {
                IActionResult response = await _userManagementController.ConsultCompanyByDomain();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }
        #endregion



        #region Métodos privados
        private async Task<string> UpdateOKResultTest()
        {
            var response = $"Se actualizó correctamente la información del registro ";
            return await Task.FromResult(response);
        }

        private async Task<string> UpdateBadRequestResultTest()
        {
            string? response = null!;
            return await Task.FromResult(response);
        }

        private async Task<string> InsertUserOKResultTest()
        {
            var response = $"Se creo correctamente el usuario con id ";
            return await Task.FromResult(response);
        }

        private async Task<List<GestionUsuarios>> GetUsersOKResultTest()
        {
            List<GestionUsuarios> gestionUsuarios = new()
            {
                new()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nombre = "Prueba insert",
                    Correo = "mario.rodriguez",
                    Telefono = "123654789",
                    Observacion = "Prueba creación usuario",
                    APP = "Terceros",
                    Estado = "Activo",
                    FechaIniUsuario = DateTime.Now,
                    FechaFinUsuario = null,
                    Permisos = "All",
                    IsAdmin = true
                },
                new()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nombre = "Prueba insert 2",
                    Correo = "mario.rodriguez1",
                    Telefono = "123654780",
                    Observacion = "Prueba creación usuario 2",
                    APP = "Terceros",
                    Estado = "Inactivo",
                    FechaIniUsuario = DateTime.Now,
                    FechaFinUsuario = null,
                    Permisos = "Lectura",
                    IsAdmin = true
                }
            };
            return await Task.FromResult(gestionUsuarios);
        }

        private async Task<List<GestionUsuarios>> GetUsersBadRequestResultTest()
        {
            List<GestionUsuarios> gestionUsuarios = [];
            return await Task.FromResult(gestionUsuarios);
        }


        private async Task<List<EmpresaDominio>> ConsultCompanyByDomainOKResultTest()
        {
            List<EmpresaDominio> companyByDomain = new()
            {
                new()
                {
                    IdEmpresaDominio = Guid.Parse("79beedbe-e0f7-4e28-94a6-eac77b62df02"),
                    IdEmpresa = Guid.Parse( "31f96d6d-ce6c-4ccc-b37c-071ac92f7616"),
                     Empresa = new()
                     {
                         IdEmpresa = Guid.Parse( "31f96d6d-ce6c-4ccc-b37c-071ac92f7616"),
                         Nombre = "Globalmvm"
                     },
                    IdDominio = Guid.Parse("d572c5eb-5d7c-41d5-aee7-4dc28946dbdd"),
                    SeguridadDominio = new(){
                      IdDominio = Guid.Parse("d572c5eb-5d7c-41d5-aee7-4dc28946dbdd"),
                      Dominio = "globalmvm.com"
                    }
                },
                new()
                {
                    IdEmpresaDominio = Guid.Parse("defa1641-c1d8-431d-bced-cec03a293cd4"),
                    IdEmpresa = Guid.Parse( "31f96d6d-ce6c-4ccc-b37c-071ac92f7616"),
                    Empresa = new(){
                      IdEmpresa = Guid.Parse( "31f96d6d-ce6c-4ccc-b37c-071ac92f7616"),
                      Nombre = "Globalmvm"
                    },
                    IdDominio = Guid.Parse("d80d5c0b-a226-488e-9d7d-9395a1ff2b88"),
                    SeguridadDominio = new(){
                      IdDominio = Guid.Parse("d80d5c0b-a226-488e-9d7d-9395a1ff2b88"),
                      Dominio = "mvm.com.co"
                    }
                }
            };
            return await Task.FromResult(companyByDomain);
        }

        private async Task<List<EmpresaDominio>> ConsultCompanyByDomainBadRequestResultTest()
        {
            List<EmpresaDominio>? companyByDomain = null;
            return await Task.FromResult(companyByDomain!);
        }
        #endregion
    }
}
