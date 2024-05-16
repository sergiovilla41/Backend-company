using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Domain.ViewModel;

namespace SimemNetAdmin.Application.Interfaces.GeneracionArchivos
{
    public interface IGeneracionArchivoService
    {
        #region Simem
        public Task<List<GeneracionArchivoJson>> GetRecords();
        public Task<DatosBasicosJson> GetDataSetBasicData(Guid idDataset);
        public Task<dynamic> GetSelectorDataByType(string selectorType, string? idDataset);
        public Task<bool> UpdateDatosBasicos(DatosBasicosJson datosBasicos);
        public Task<bool> HasRecordsSaved(Guid idgeneracionArchivos);
        public Task<List<SelectPropertiesJson>> SelectProperties(string type, string? project);
        public Task<string> UpdateColumnsDestinityVersion(ConfiguracionColumnasOrigenJson columnasOrigenJson);
        #endregion

        #region Terceros
        public Task<List<GeneracionArchivoDto>> GetDataList(Paginator? paginator, Guid? idEmpresa);
        #endregion
    }
}
