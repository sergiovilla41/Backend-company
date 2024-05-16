using EnviromentConfig;
using SimemNetAdmin.Domain.Interfaces.NotificationDomain;
using SimemNetAdmin.Infra.Data.Repository.NotificationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.UnitTest.RepositoryTest.NotificationParametersRepository.Test
{
    [TestClass]
    public class ParameterNotificationRepositoryTest
    {
        private readonly ParameterNotificationRepository parameterNotificationRepository = new();

        public ParameterNotificationRepositoryTest() {
            Connection.ConfigureConnections();
        }


        [TestMethod]
        public async Task GetNotificationEmailTest1()
        {
            try
            {
                var resultado = await parameterNotificationRepository.GetNotificationEmail(Guid.Parse("2300ca90-4e76-4a2d-b20d-9b0099158765"));
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task GetParameterNotificationTest2()
        {
            try
            {
                var resultado = await parameterNotificationRepository.GetParameterNotification();
                Assert.IsNotNull(resultado);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
