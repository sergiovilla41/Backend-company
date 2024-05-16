using EnviromentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimemNetAdmin.Infra.Data.Repository.ConfiguracionEjecucion;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ExecutionConfigurationTest
{
    [TestClass]
    public class ExecutionConfigurationRepositoryTest
    {
        private readonly ExecutionConfigurationRepository _configuracionEjecucionRepository;

        public ExecutionConfigurationRepositoryTest()
        {
            _configuracionEjecucionRepository = new ExecutionConfigurationRepository();
            Connection.ConfigureConnections();
        }

        #region Test controller
        [TestMethod]
        public async Task TestConfiguracionEjecucionData_StatusCodeOK()
        {
            Guid idExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F");
            var response = await _configuracionEjecucionRepository.ExecutionConfigurationData(idExtraccion);
            Assert.IsTrue(response.Count > 0);
        }

        #endregion
    }
}
