using EnviromentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Infra.Data.Repository.ColumnasOrigenRepository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ColumnasOrigenTest
{
    [TestClass]
    public class OriginColumnsRepositoryTest
    {
        private readonly OriginColumnsRepository _columnasOiregnRepository;

        public OriginColumnsRepositoryTest()
        {
            _columnasOiregnRepository = new OriginColumnsRepository();
            Connection.ConfigureConnections();
        }

        #region Test controller
        [TestMethod]
        public async Task TestGetColumnasOrigenJson_StatusCodeOK()
        {
            Guid idExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F");
            var response = await _columnasOiregnRepository.GetColumnasOrigenJson(idExtraccion);
            Assert.IsTrue(response.Count > 0);
        }

        [TestMethod]
        public async Task TestGetColumnasOrigenJson_StatusEmptyLst()
        {
            Guid idExtraccion = Guid.NewGuid();
            var response = await _columnasOiregnRepository.GetColumnasOrigenJson(idExtraccion);
            Assert.IsTrue(response.Count == 0);
        }




        [TestMethod]
        public async Task TestUpdateColumnasOrigen_StatusCodeOK()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
            {
                IdColumnaDestino = Guid.Parse("81BD94CE-8A0A-4C64-BCAC-E76F0A15D0C7"),
                IdColumnaOrigen = Guid.Parse("3CA12E8B-C1A7-45C3-B59B-003B03B93E69"),
                IdExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F"),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            var response = await _columnasOiregnRepository.UpdateColumnasOrigen(columnasOrigenJson);

            Assert.IsTrue(string.IsNullOrEmpty(response));
        }

        [TestMethod]
        public async Task TestUpdateColumnasOrigen_StatusCodeNull()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F"),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            var response = await _columnasOiregnRepository.UpdateColumnasOrigen(columnasOrigenJson);
            Assert.IsTrue(!string.IsNullOrEmpty(response));
            Assert.AreEqual("Error: No se encuentra información.", response);
        }

        [TestMethod]
        public async Task TestUpdateColumnasOrigen_StatusCodeException()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = null!;
            var response = await _columnasOiregnRepository.UpdateColumnasOrigen(columnasOrigenJson);
            Assert.IsTrue(!string.IsNullOrEmpty(response));
            AssertFailedException.ReferenceEquals(response, null);
        }

        #endregion


        #region Métodos privados

        #endregion
    }
}
