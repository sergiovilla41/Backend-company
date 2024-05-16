using EnviromentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Repository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LabelsTest
{
    [TestClass]
    public class LabelsRepositoryTest
    {
        private readonly LabelsRepository _labelsRepository;

        public LabelsRepositoryTest()
        {
            Connection.ConfigureConnections();
            _labelsRepository = new LabelsRepository();
        }

        #region Test controller
        [TestMethod]
        public async Task TestListLabels_StatusCodeOK()
        {
            var response = await _labelsRepository.ListLabels();
            Assert.IsTrue(response.Count > 0);
        }



        [TestMethod]
        public async Task TestGetLabelById_StatusCodeOK()
        {
            Guid idExtraccion = Guid.Parse("54F22640-C284-41E2-BE78-08DC691BA193");
            var response = await _labelsRepository.GetLabelById(idExtraccion);
            Assert.IsNotNull(response);
            Assert.IsTrue(response !=  default(LabelsDto));
        }




        [TestMethod]
        public async Task TestUpdateLabel_StatusCodeOK()
        {
            LabelsDto labelsDto = new()
            {
                Id = Guid.Parse("54F22640-C284-41E2-BE78-08DC691BA193"),
                Estado = false
            };
            var response = await _labelsRepository.UpdateLabel(labelsDto);
            Assert.IsNotNull(response);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task TestUpdateLabel_NoContent()
        {
            LabelsDto labelsDto = new()
            {
                Id = Guid.NewGuid(),
                Estado = false,
                Titulo = "Recurso hidrico"
            };
            var response = await _labelsRepository.UpdateLabel(labelsDto);
            Assert.IsNotNull(response);
            Assert.IsFalse(response);
        }




        [TestMethod]
        public async Task TestCreateLabel_StatusCodeOK()
        {
            LabelsDto labelsDto = new()
            {
                Estado = false,
                Titulo = "Pruebas unitarias"
            };
            var response = await _labelsRepository.CreateLabel(labelsDto);
            Assert.IsNotNull(response);
            Assert.IsTrue(response);
        }
        #endregion
    }
}
