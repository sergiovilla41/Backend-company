using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ColumnasOrigenTest
{
    [TestClass]
    public class OriginColumnsServicesTest
    {
        private readonly OriginColumnsService _columnasOrigenService;
        private readonly Mock<IOriginColumnsRepository> _mockColumnasOrigenRepository;

        public OriginColumnsServicesTest()
        {
            _mockColumnasOrigenRepository = new Mock<IOriginColumnsRepository>();

            _columnasOrigenService = new OriginColumnsService(_mockColumnasOrigenRepository.Object);
        }


        #region Test service
        [TestMethod]
        public async Task TestGetColumnasOrigenJson_StatusCodeOK()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockColumnasOrigenRepository.Setup(x => x.GetColumnasOrigenJson(idExtraccion)).Returns(GetColumnasOrigenJsonTestOK);
            List<ConfiguracionColumnasOrigenJson> columnasOrigen = await _columnasOrigenService.GetColumnasOrigenJson(idExtraccion);
            Assert.IsNotNull(columnasOrigen);
            Assert.IsTrue(columnasOrigen.Count > 0);
        }

        [TestMethod]
        public async Task TestUpdateColumnasOrigen_StatusCodeOK()
        {
            ConfiguracionColumnasOrigenJson configuracionColumnasOrigenJson = new ConfiguracionColumnasOrigenJson()
            {
                IdColumnaDestino = Guid.NewGuid(),
                IdColumnaOrigen = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };

            _mockColumnasOrigenRepository.Setup(x => x.UpdateColumnasOrigen(configuracionColumnasOrigenJson)).Returns(UpdateOKResultTest);
            var response = await _columnasOrigenService.UpdateColumnasOrigen(configuracionColumnasOrigenJson);
            Assert.IsTrue(string.IsNullOrEmpty(response));
        }
        #endregion



        #region Métodos privados
        private async Task<List<ConfiguracionColumnasOrigenJson>> GetColumnasOrigenJsonTestOK()
        {
            var lstConfiguracionColumnasOrigen = new List<ConfiguracionColumnasOrigenJson>()
            {
                new ConfiguracionColumnasOrigenJson(){
                IdColumnaOrigen = Guid.NewGuid(),
                Numeracion = 3,
                ColumnaOrigen = "",
                IdColumnaDestino = Guid.NewGuid(),
                IdExtraccion = Guid.NewGuid(),
                }
            };
            return await Task.FromResult(lstConfiguracionColumnasOrigen);
        }

        private async Task<string> UpdateOKResultTest()
        {
            var response = string.Empty;
            return await Task.FromResult(response);
        }
        #endregion
    }
}
