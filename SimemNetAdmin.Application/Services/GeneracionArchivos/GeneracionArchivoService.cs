using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Repository.GeneracionArchivos;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Application.Services.GeneracionArchivos
{
    [ExcludeFromCodeCoverage]
    public class GeneracionArchivoService : IGeneracionArchivoService
    {
        #region Variables
        private readonly GeneracionArchivoRepository _repository;
        #endregion

        #region Contructor
        public GeneracionArchivoService()
        {
            _repository = new GeneracionArchivoRepository();
        }
        #endregion

        #region Methods
        [ExcludeFromCodeCoverage]
        public async Task<List<GeneracionArchivoJson>> GetRecords()
        {
            return await _repository.GetRecords();
        }

        public async Task<DatosBasicosJson> GetDataSetBasicData(Guid idDataset)
        {
            return await _repository.GetDataSetBasicData(idDataset);
        }

        public async Task<dynamic> GetSelectorDataByType(string selectorType, string? idDataset)
        {
            return await _repository.GetSelectorDataByType(selectorType, idDataset);
        }

        public async Task<bool> UpdateDatosBasicos(DatosBasicosJson datosBasicos)
        {
            return await _repository.UpdateDatosBasicos(datosBasicos);
        }

        public async Task<bool> HasRecordsSaved(Guid idgeneracionArchivos)
        {
            return await _repository.HasRecordsSaved(idgeneracionArchivos);
        }

        public async Task<List<SelectPropertiesJson>> SelectProperties(string type, string? project)
        {
            return await _repository.SelectProperties(type, project)!;
        }

        public async Task<string> UpdateColumnsDestinityVersion(ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            return await _repository.UpdateColumnsDestinityVersion(columnasOrigenJson)!;
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<GeneracionArchivoDto>> GetDataList(Paginator? paginator, Guid? idEmpresa)
        {
            return await _repository.GetDataList(paginator, idEmpresa);
        }

        #endregion
    }
}