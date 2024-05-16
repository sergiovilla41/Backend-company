using EnviromentConfig;
using SimemNetAdmin.Infra.Data.Repository.Menu;


namespace SimemNetAdmin.UnitTest.RepositoryTest
{
    [TestClass]
    public class MenuRepositoryTest
    {
        private readonly MenuRepository menuRepository = new();

        public MenuRepositoryTest()
        {
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task GetRecordsTest1()
        {
            try
            {
                var resultado = await menuRepository.GetRecords("simem");
                Assert.IsTrue(resultado.Count >= 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
