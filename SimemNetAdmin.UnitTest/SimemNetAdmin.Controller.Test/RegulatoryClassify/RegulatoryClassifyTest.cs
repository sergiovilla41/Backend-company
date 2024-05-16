using EnviromentConfig;
using Microsoft.AspNetCore.Mvc;
using SimemNetAdmin.Application.Services;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Web.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Controller.Test.RegulatoryClassify
{
    [TestClass]
    public class RegulatoryClassifyTest
    {
        private readonly RegulatoryClassifyController _controller;
        private readonly RegulatoryClassifyService _servive;
        public RegulatoryClassifyTest()
        {
            Connection.ConfigureConnections();
            _servive = new RegulatoryClassifyService();
            _controller = new RegulatoryClassifyController(_servive);
        }

        [TestMethod]
        public async Task CrudMethodsTest()
        {
            IActionResult GetResult = await _controller.Get();
            Assert.IsTrue(GetResult.GetType() == typeof(OkObjectResult));

            ConfiguracionClasificacionRegulatoriaDto TestSubject = new();
            TestSubject.IdConfiguracionClasificacionRegulatoria = new Guid();
            TestSubject.CodigoDelta = "TestMethod01";
            TestSubject.Descripcion = "Descripcion de pruebas RD";
            TestSubject.FechaCreacion = DateTime.Now;
            TestSubject.DeltaInicialDiaMes = 1;
            TestSubject.DeltaInicialDias = 1;
            TestSubject.DeltaInicialDiaSemana = 1;
            TestSubject.DeltaInicialSemanas = 1;
            TestSubject.DeltaInicialMes = 1;
            TestSubject.DeltaInicialMeses = 1;
            TestSubject.DeltaInicialAno = 1;
            TestSubject.DeltaInicialPeriodo = null;
            TestSubject.DeltaFinalDiaMes = 1;
            TestSubject.DeltaFinalDias = 1;
            TestSubject.DeltaFinalDiaSemana = 1;
            TestSubject.DeltaFinalSemanas = 1;
            TestSubject.DeltaFinalMes = 1;
            TestSubject.DeltaFinalMeses = 1;
            TestSubject.DeltaFinalAno = 1;
            TestSubject.DeltaFinalPeriodo = null;

            try
            {
                IActionResult CreateResult = await _controller.Create(TestSubject);

                Assert.IsTrue(CreateResult.GetType() == typeof(OkObjectResult));


                if (CreateResult is ObjectResult okObjectResult)
                {
                    string? idRegulatoryClassify = okObjectResult.Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(idRegulatoryClassify))
                    {
                        TestSubject.IdConfiguracionClasificacionRegulatoria = Guid.Parse(idRegulatoryClassify);
                        TestSubject.Descripcion = TestSubject.Descripcion + " Updt";
                    }
                }

                IActionResult UpdateResult = _controller.Update(TestSubject);
                Assert.IsTrue(UpdateResult.GetType() == typeof(OkResult));

                _servive.DeleteRegulatoryClassifyForTest(TestSubject.IdConfiguracionClasificacionRegulatoria);

            }
            catch (Exception)
            {

                _servive.DeleteRegulatoryClassifyForTest(TestSubject.IdConfiguracionClasificacionRegulatoria);
            }


        }

        [TestMethod]
        public async Task CrudMethodsTestFail()
        {
            ConfiguracionClasificacionRegulatoriaDto TestSubject = new();
            TestSubject.IdConfiguracionClasificacionRegulatoria = new Guid();

            IActionResult UpdateResult = _controller.Update(TestSubject);
            Assert.IsTrue(UpdateResult.GetType() == typeof(BadRequestObjectResult));

            TestSubject.DeltaFinalPeriodo = "TEXTO DE PRUEBAS QUE EXCEDE LOS 10 CARACTERES";

            IActionResult CreateResult = await _controller.Create(TestSubject);

            Assert.IsTrue(CreateResult.GetType() == typeof(BadRequestObjectResult));
        }

    }
}
