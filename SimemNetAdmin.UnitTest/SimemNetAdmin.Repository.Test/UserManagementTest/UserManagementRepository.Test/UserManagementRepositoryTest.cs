using EnviromentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using SimemNetAdmin.Infra.Data.Repository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UserManagementRepositoryTest
{
    [TestClass]
    public class UserManagementRepositoryTest
    {
        private readonly UserManagementRepository _userManagementRepository;

        public UserManagementRepositoryTest()
        {
            _userManagementRepository = new UserManagementRepository();
            Connection.ConfigureConnections();
        }

        #region Test controller
        [TestMethod]
        public async Task TestUpdate_StatusCodeOK()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.Parse("7FD3D893-7CC8-474E-A3D6-296F5DCFB98E"),
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
            var response = await _userManagementRepository.UpdateUser(gestionUsuarios);
            Assert.IsTrue(response != null);
        }


        [TestMethod]
        public async Task TestUpdate_StatusCodeException()
        {
            GestionUsuarios? gestionUsuarios = null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userManagementRepository.UpdateUser(gestionUsuarios!));
        }




        [TestMethod]
        public async Task TestInsertUser_StatusCodeOK()
        {
            GestionUsuarios gestionUsuarios = new()
            {
                IdUsuario = Guid.Parse("7FD3D893-7CC8-474E-A3D6-296F5DCFB98E"),
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
            var response = await _userManagementRepository.InsertUser(gestionUsuarios);
            Assert.IsTrue(response != null);
        }


        [TestMethod]
        public async Task TestInsertUser_StatusCodeException()
        {
            GestionUsuarios? gestionUsuarios = null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userManagementRepository.InsertUser(gestionUsuarios!));
        }




        [TestMethod]
        public async Task TestGetUsers_StatusCodeOK()
        {
            var response = await _userManagementRepository.GetUsers();
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Count > 0);
        }


        [TestMethod]
        public async Task TestConsultCompanyByDomain_StatusCodeOK()
        {
            var response = await _userManagementRepository.ConsultCompanyByDomain();
            Assert.IsTrue(response != null);
            Assert.IsTrue(response!.Count > 0);
        }
        #endregion
    }
}
