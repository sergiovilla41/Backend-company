using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.Extraction;
using SimemNetAdmin.Domain.ViewModel.Extracciones;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface IExtractionService
    {
        #region simem
        public Task<List<ExtractionsModel>?> GetById(string? idExtraccion, string? idDataSet);
        public Task<string> Save(ExtractionsModelDto extractionsModel);
        public Task<string> Update(ExtractionsModelDto extractionsModel);
        public Task<string> Delete(string idExtraccion);
        #endregion

        #region Terceros
        public Task<ExtractionsModel?> GetExtraccionesDataByIdConfiguracionGeneracionArchivos(Guid idConfiguracionGeneracionArchivos);
        #endregion
    }
}
