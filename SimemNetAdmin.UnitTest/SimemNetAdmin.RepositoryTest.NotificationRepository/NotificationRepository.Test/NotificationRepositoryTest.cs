
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnviromentConfig;
using SimemNetAdmin.Domain.Interfaces.NotificationDomain;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using SimemNetAdmin.Infra.Data.Repository.NotificationRepository;


namespace SimemNetAdmin.UnitTest.RepositoryTest.NotificationParametersRepository.Test
{
    [TestClass]
    public class NotificationRepositoryTest
    {
        private readonly NotificationRepository notificationRepository = new();

        public NotificationRepositoryTest() {
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task Test1SendNotificacionRepositoryTest1()
        {
            try
            {
                var resultado = await notificationRepository.GetLogExecution();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public async Task Test2NotificacionDataSetRegulatorio()
        {
            try
            {
                var resultado = await notificationRepository.NotificacionDataSetRegulatorio();
                Assert.IsTrue(resultado.Count >=0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test3UpdateSubmittedErrors()
        {
            try
            {
                var notificationResults = await notificationRepository.GetLogExecution();
                var resultado = notificationRepository.UpdateSubmittedErrors(notificationResults!);
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test4SaveLog()
        {
            try
            {
                LogSendNotificationViewModel logSend = new()
                {

                    IdTipoNotificacion = Guid.Parse("2300ca90-4e76-4a2d-b20d-9b0099158765"),
                    IdParametroNotificacion = Guid.Parse("83fe18f6-eaad-4013-8740-a21efbb03662"),
                    IdCorreoNotificacion = "ruben.munoz@globalmvm.com",
                    DescripcionError = "",
                    MetodoError = "",
                    EstadoEnvio = "Enviado",
                    NumeroRegistros = 5,
                    FechaEjecucion = DateTime.Now
                };
                var resultado =  notificationRepository.SaveLog(logSend);
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test5GetExecutionAndErrorMonitoring()
        {
            try
            {
                var resultado = await notificationRepository.GetExecutionAndErrorMonitoring();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
