using SimemNetAdmin.Domain.Common;

namespace GeneracionArchivoTest
{
    [TestClass]
    public class GeneracionArchivoRepositoryTest
    {
        private readonly GeneracionArchivoRepository generacionArchivoRepository = new();

        public GeneracionArchivoRepositoryTest()
        {
            Connection.ConfigureConnections();
        }

        #region Test repository
        [TestMethod]
        public async Task GetRecords()
        {
            var resultado = await generacionArchivoRepository.GetRecords();
            Assert.IsTrue(resultado.Count >= 0);
        }

        [TestMethod]
        public async Task GetDataSetBasicData_Success()
        {
            var resultado = await generacionArchivoRepository.GetDataSetBasicData(new("A704EEF3-CA1C-4EBF-98A0-01325C61765D"));
            Assert.IsTrue(resultado != null);
        }

        [TestMethod]
        public async Task GetDataSetBasicData_Null()
        {
            var resultado = await generacionArchivoRepository.GetDataSetBasicData(new("A704EEF3-CA1C-4EBF-98A0-01325C61765E"));
            Assert.IsTrue(resultado == null);
        }

        [TestMethod]
        public async Task GetSelectorDataByType_DuracionISO()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("DuracionISO", string.Empty);
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_Granularidad()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("Granularidad", string.Empty);
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_Periodicidad()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("Periodicidad", string.Empty);
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_Etiquetas()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("Etiquetas", string.Empty);
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_EtiquetasByIdDataset()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("EtiquetasByIdDataset", "AECA287E-46A4-498B-8771-4399B96BE855");
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_Clasificacionregulatoria()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("Clasificacionregulatoria", string.Empty);
            Assert.IsTrue(resultado != null);
        }
        [TestMethod]
        public async Task GetSelectorDataByType_Default()
        {
            var resultado = await generacionArchivoRepository.GetSelectorDataByType("", string.Empty);
            Assert.IsTrue(resultado == null);
        }

        [TestMethod]
        public async Task HasRecordsSaved()
        {
            bool resultado = await generacionArchivoRepository.HasRecordsSaved(new("6BAC4845-DC3D-4EF3-B1BD-675016165592"));
            Assert.IsTrue(resultado == true);
        }

        [TestMethod]
        public async Task TestSelectProperties_StatusCodeOKColumnaOrigen()
        {
            string type = "columnaorigen";
            string? project = string.Empty;
            var response = await generacionArchivoRepository.SelectProperties(type, project);
            Assert.IsTrue(response.Count > 0);
        }

        [TestMethod]
        public async Task TestSelectProperties_StatusCodeOKColumnaDestino()
        {
            string type = "columnadestino";
            string? project = string.Empty;
            var response = await generacionArchivoRepository.SelectProperties(type, project);
            Assert.IsTrue(response.Count > 0);
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
            var response = await generacionArchivoRepository.UpdateColumnsDestinityVersion(columnasOrigenJson);

            Assert.IsTrue(string.IsNullOrEmpty(response));
        }

        [TestMethod]
        public async Task TestUpdateColumnsDestinityVersion_StatusCodeNull()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            var response = await generacionArchivoRepository.UpdateColumnsDestinityVersion(columnasOrigenJson);
            Assert.IsTrue(!string.IsNullOrEmpty(response));
            Assert.AreEqual("Error: No se encuentra información.", response);
        }

        [TestMethod]
        public async Task TestUpdateColumnsDestinityVersion_StatusCodeException()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = null!;
            var response = await generacionArchivoRepository.UpdateColumnsDestinityVersion(columnasOrigenJson);
            Assert.IsTrue(!string.IsNullOrEmpty(response));
            AssertFailedException.ReferenceEquals(response, null);
        }

        #endregion
    }
}
