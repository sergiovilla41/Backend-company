using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimemNetAdmin.Application.Services;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UserManagementTest
{
    [TestClass]
    public class UserManagementServicesTest
    {
        private readonly UserManagementService _userManagementService;
        private readonly Mock<IUserManagementRepository> _mockUserManagementRepository;

        public UserManagementServicesTest()
        {
            _mockUserManagementRepository = new Mock<IUserManagementRepository>();
            _userManagementService = new UserManagementService(_mockUserManagementRepository.Object);
        }


        #region Test service
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

            _mockUserManagementRepository.Setup(x => x.UpdateUser(gestionUsuarios)).Returns(UpdateOKResultTest);
            var response = await _userManagementService.UpdateUser(gestionUsuarios);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Contains("Se actualizó correctamente la información del registro"));
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

            _mockUserManagementRepository.Setup(x => x.UpdateUser(gestionUsuarios)).Returns(UpdateBadRequestResultTest);
            var response = await _userManagementService.UpdateUser(gestionUsuarios);
            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task TestUpdateUser_StatusCodeException()
        {
            GestionUsuarios? gestionUsuarios = null;

            _mockUserManagementRepository.Setup(x => x.UpdateUser(gestionUsuarios!)).ThrowsAsync(new Exception("Mensaje de error"));
            var response = await _userManagementService.UpdateUser(gestionUsuarios!);
            Assert.IsNotNull(response);
            AssertFailedException.ReferenceEquals(response, null);
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

            _mockUserManagementRepository.Setup(x => x.InsertUser(gestionUsuarios)).Returns(UpdateOKResultTest);
            var response = await _userManagementService.InsertUser(gestionUsuarios);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Contains("Se creo correctamente el usuario con id "));
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

            _mockUserManagementRepository.Setup(x => x.InsertUser(gestionUsuarios)).Returns(UpdateBadRequestResultTest);
            var response = await _userManagementService.InsertUser(gestionUsuarios);
            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task TestInsertUser_StatusCodeException()
        {
            GestionUsuarios? gestionUsuarios = null;

            _mockUserManagementRepository.Setup(x => x.InsertUser(gestionUsuarios!)).ThrowsAsync(new Exception("Mensaje de error"));
            var response = await _userManagementService.InsertUser(gestionUsuarios!);
            Assert.IsNotNull(response);
            AssertFailedException.ReferenceEquals(response, null);
        }




        [TestMethod]
        public async Task TestGetUser_StatusCodeOK()
        {
            _mockUserManagementRepository.Setup(x => x.GetUsers()).Returns(GetUsersOKResultTest);
            _mockUserManagementRepository.Setup(y => y.ConsultCompanyByDomain()).Returns(ConsultCompanyByDomainOKResultTest);
            var response = await _userManagementService.GetUsers();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
        }




        [TestMethod]
        public async Task TestConsultCompanyByDomain_StatusCodeOK()
        {
            _mockUserManagementRepository.Setup(x => x.ConsultCompanyByDomain()).Returns(ConsultCompanyByDomainOKResultTest);
            var response = await _userManagementService.ConsultCompanyByDomain();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
        }

        #endregion



        #region Métodos privados
        private async Task<GestionUsuarios> UpdateOKResultTest()
        {
            GestionUsuarios response = new()
            {
                IdUsuario = Guid.NewGuid() ,
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
            
            return await Task.FromResult(response);
        }

        private async Task<GestionUsuarios> UpdateBadRequestResultTest()
        {
            GestionUsuarios? response = null;

            return await Task.FromResult(response!);
        }

        private async Task<List<GestionUsuarios>> GetUsersOKResultTest()
        {
            List<GestionUsuarios> response = new()
            {
                new GestionUsuarios{
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
                IsAdmin = true,
                Empresa = ""
                }
            };

            return await Task.FromResult(response);
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

        #endregion
    }
}