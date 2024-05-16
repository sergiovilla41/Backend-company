using EnviromentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimemNetAdmin.Application.Services.GeneracionArchivos;
using SimemNetAdmin.Domain.Common;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GeneracionArchivoServicesTest
{
    [TestClass]
    public class GeneracionArchivoServicesTest
    {
        private readonly GeneracionArchivoService _generacionArchivoService;

        public GeneracionArchivoServicesTest()
        {
            Connection.ConfigureConnections();
            _generacionArchivoService = new GeneracionArchivoService();
        }



        #region Test Service
        [TestMethod]
        public async Task TestSelectProperties_StatusCodeOK()
        {
            string type = "columnadestino";
            string? project = "";
            var response = await _generacionArchivoService.SelectProperties(type, project);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
        }

        [TestMethod]
        public async Task TestUpdateColumnsDestinityVersion_StatusCodeOK()
        {
            ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
            {
                IdColumnaDestino = Guid.Parse("81BD94CE-8A0A-4C64-BCAC-E76F0A15D0C7"),
                IdColumnaOrigen = Guid.Parse("3CA12E8B-C1A7-45C3-B59B-003B03B93E69"),
                IdExtraccion = Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F"),
                ColumnaOrigen = "Prueba",
                Numeracion = 23
            };
            var response = await _generacionArchivoService.UpdateColumnsDestinityVersion(columnasOrigenJson);
            Assert.IsNotNull(response);
            Assert.IsTrue(string.IsNullOrEmpty(response));
        }
        #endregion


        #region Métodos privados
        private static async Task<List<SelectPropertiesJson>> SelectPropertiesResultOKTest()
        {
            var lstResponse = new List<SelectPropertiesJson>()
            {
                new() {
                    descripcion = "Porcentaje de cobertura de la demanda regulada y no regulada con contratos fuera del SICEP y con atención directa de la demanda (sin intermediación)",
                    Id = null,
                    IdColumnasDestino = Guid.NewGuid(),
                    IdConfiguracionclasificacionregulatoria = 0,
                    TipoDato = "flotante",
                    Value = "PrcjEstimadoCobertura"
                }
            };
            return await Task.FromResult(lstResponse);
        }
        #endregion
    }
}
