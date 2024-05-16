using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.Extraction;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Domain.ViewModel.Extracciones;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Application.Services.ExtractionService
{
    public class ExtractionService : IExtractionService
    {
        private readonly IExtractionRepository _extractionRepository;

        public ExtractionService(IExtractionRepository extractionRepository)
        {
            _extractionRepository = extractionRepository ?? throw new ArgumentNullException(nameof(extractionRepository));
        }

        #region Simem
        public async Task<List<ExtractionsModel>?> GetById(string? idExtraccion, string? idDataSet)
        {
            return await _extractionRepository.GetById(idExtraccion, idDataSet)!;
        }

        public async Task<string> Save(ExtractionsModelDto extractionsModel)
        {
            return await _extractionRepository.Save(extractionsModel)!;
        }

        public async Task<string> Delete(string idExtraccion)
        {
            return await _extractionRepository.Delete(idExtraccion)!;
        }

        public async Task<string> Update(ExtractionsModelDto extractionsModel)
        {
            return await _extractionRepository.Update(extractionsModel)!;
        }
        #endregion

        #region Terceros
        [ExcludeFromCodeCoverage]
        public async Task<ExtractionsModel?> GetExtraccionesDataByIdConfiguracionGeneracionArchivos(Guid idConfiguracionGeneracionArchivos)
        {
            return await _extractionRepository.GetExtraccionesDataByIdConfiguracionGeneracionArchivos(idConfiguracionGeneracionArchivos);
        }
        #endregion
    }
}
