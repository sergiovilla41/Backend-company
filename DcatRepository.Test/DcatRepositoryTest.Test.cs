namespace DcatRepositoryTest.Test
{
    [TestClass]
    public class DcatRepositoryTest
    {
        private readonly DcatRepository dcatRepository = new();

        public DcatRepositoryTest()
        {
            Connection.ConfigureConnections();
        }

        [TestMethod]
        public async Task GetRecords()
        {
            var resultado = await dcatRepository.GetResource();
            Assert.IsTrue(resultado.Count >= 0);
        }
    }
}
