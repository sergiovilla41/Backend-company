using SimemNetAdmin.Domain.Models.Extraction;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Domain.ViewModel.Extracciones;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IExtractionRepository
    {
        public Task<List<ExtractionsModel>?> GetById(string? idExtraccion, string? idDataSet);
        public Task<string> Save(ExtractionsModelDto extractionsModel);
        public Task<string> Update(ExtractionsModelDto extractionsModel);
        public Task<string> Delete(string idExtraccion);
        public Task<ExtractionsModel?> GetExtraccionesDataByIdConfiguracionGeneracionArchivos(Guid idConfiguracionGeneracionArchivos);

    }
}
