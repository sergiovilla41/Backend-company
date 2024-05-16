using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimemNetAdmin.Application.Services.ConfiguracionEjecucionService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ConfiguracionEjecucionTest
{
    [TestClass]
    public class ExecutionConfigurationServicesTest
    {
        private readonly ExecutionConfigurationService _configuracionEjecucionService;
        private readonly Mock<IExecutionConfigurationRepository> _mockConfiguracionEjecucionRepository;

        public ExecutionConfigurationServicesTest()
        {
            _mockConfiguracionEjecucionRepository = new Mock<IExecutionConfigurationRepository>();
            _configuracionEjecucionService = new ExecutionConfigurationService(_mockConfiguracionEjecucionRepository.Object);
        }

        #region Test service
        [TestMethod]
        public async Task TestConfiguracionEjecucionData_StatusCodeOK()
        {
            Guid idExtraccion = Guid.NewGuid();
            _mockConfiguracionEjecucionRepository.Setup(x => x.ExecutionConfigurationData(idExtraccion)).Returns(ConfiguracionEjecucionDataResultTestOK);
            var response = await _configuracionEjecucionService.ExecutionConfigurationData(idExtraccion);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
            Assert.IsNotNull(response);
        }


        #endregion

        #region Métodos privados
        private async Task<List<ExecutionModel>> ConfiguracionEjecucionDataResultTestOK()
        {
            var lstResponse = new List<ExecutionModel>
            {
                new()
                {
                    Dia = 5,
                    DiaSemana = null,
                    FechaActualizacion = null,
                    FechaCreacion = DateTime.Now,
                    Hora = 23,
                    IdConfiguracionGeneracionArchivos = Guid.NewGuid(),
                    IdEjecucion = Guid.NewGuid(),
                    IndActivo = true,
                    IndDiaHabil = false,
                    Mes = null
                }
            };

            return await Task.FromResult(lstResponse);
        }

        #endregion


    }
}
