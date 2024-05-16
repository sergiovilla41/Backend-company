using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Domain.ViewModel;

namespace SimemNetAdmin.Domain.Interfaces.GeneracionArchivos
{
    public interface IGeneracionArchivoRepository
    {
        #region Simem
        public Task<List<GeneracionArchivoJson>> GetRecords();
        public Task<DatosBasicosJson> GetDataSetBasicData(Guid idDataset);
        public Task<dynamic> GetSelectorDataByType(string selectorType, string? idDataset);
        public Task<bool> UpdateDatosBasicos(DatosBasicosJson datosBasicos);
        public Task<bool> HasRecordsSaved(Guid idgeneracionArchivos);
        #endregion

        #region Terceros
        public Task<List<GeneracionArchivoDto>> GetDataList(Paginator? paginator, Guid? idEmpresa);
        #endregion
    }
}