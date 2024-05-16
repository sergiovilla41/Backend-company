using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Domain.Models.Dcat;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository.Dcat
{
    public class DcatRepository : IDcatRepository
    {
        #region Constants
        private const string IDDATASET = "B38EFCD8-C26F-44BF-ADDD-162E64A788F3"; //B38EFCD8-C26F-44BF-ADDD-162E64A788F3
        #endregion

        #region Public Methods
        public async Task<List<DcatJsonModel>> GetResource()
        {
            using var context = new SimemNetAdminDbContext();
            List<DcatJsonModel> dcatResources = [];
            List<FileGenerationModel> dataSetRecords = await context.FileGenerationModel.Where(f => f.IdConfiguracionGeneracionArchivos == new Guid(IDDATASET)).ToListAsync();

            if (dataSetRecords.Count == 0)
                return dcatResources;

            DcatJsonModel dcatJsonModel = new();
            List<Guid> ids = await context.GeneracionArchivoEtiquetas.Where(x => x.IdConfiguracionGeneracionArchivo == new Guid(IDDATASET)).Select(s => s.EtiquetaId).ToListAsync();
            List<string> tags = await context.Etiquetas.Where(x => ids.Contains(x.Id) && x.Estado).Select(s => s.Titulo).ToListAsync();

            foreach (FileGenerationModel records in dataSetRecords)
            {
                DataSet dcatModel = new()
                {
                    Title = records.Titulo!,
                    Description = records.Descripcion!,
                    Keyword = tags,
                    Modified = Convert.ToDateTime(records.FechaActualizacion).ToString("yyyy-MM-dd")!,
                    Publisher = new Publisher() { Name = records.EntidadOrigen! },
                    Identifier = records.IdConfiguracionGeneracionArchivos.ToString(),
                    AccessLevel = !records.Privacidad ? "public" : "non-public"
                };
                dcatJsonModel.DataSet.Add(dcatModel);
                dcatResources.Add(dcatJsonModel);
            }

            return dcatResources;
        }
        #endregion
    }
}
