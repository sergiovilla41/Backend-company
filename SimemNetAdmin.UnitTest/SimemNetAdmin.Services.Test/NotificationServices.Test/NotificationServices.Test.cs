using EnviromentConfig;
using SimemNetAdmin.Application.Services.NotificationService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using System.Collections.Generic;

namespace SimemNetAdmin.UnitTest.Services.Test
{

    [TestClass]
    public class NotificationServicesTest 
    {
        private readonly NotificationServices notificationServices = new();

        public NotificationServicesTest() {
            Connection.ConfigureConnections();
        }


        [TestMethod]
        public async Task Test1GetLogExecutionTest1()
        {
            try
            {
                var resultado = await notificationServices.GetLogExecution();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }



        [TestMethod]
        public async Task Test2GetNotificationEmailTest2()
        {
            try
            {
                var resultado = await notificationServices.GetNotificationEmail(Guid.Parse("2300ca90-4e76-4a2d-b20d-9b0099158765"));
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test3GetParameterNotificationTest3()
        {
            try
            {
                var resultado = await notificationServices.GetParameterNotification();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test4GenerateSendNotificationRegulatorio()
        {
            try
            {
                var parameters = await notificationServices.GetParameterNotification();
                var parameter = parameters.Where(param => param.IdNotificationType == Guid.Parse("2300ca90-4e76-4a2d-b20d-9b0099158765")).ToList();
                notificationServices.GenerateSendNotification(parameter[0]);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test5GenerateSendNotificationError()
        {
            try
            {
                var parameters = await notificationServices.GetParameterNotification();
                var parameter = parameters.Where(param => param.IdNotificationType == Guid.Parse("34a73654-2268-40b3-b984-ff590bfd33bb")).ToList();
                
                notificationServices.GenerateSendNotification(parameter[0]);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test6SaveLog()
        {
            try
            {
                var parameters = await notificationServices.GetParameterNotification();
                var emails = await notificationServices.GetNotificationEmail(Guid.Parse("2300ca90-4e76-4a2d-b20d-9b0099158765"));
                notificationServices.GenerateSendNotification(parameters[1]);
                notificationServices.SaveLog(parameters[0], emails!, 5);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Test7GetExecutionAndErrorMonitoring()
        {
            try
            {
                var resultado = await notificationServices.GetExecutionAndErrorMonitoring();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
