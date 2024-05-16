using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.Categorias;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Domain.Models.DuracionISO;
using SimemNetAdmin.Domain.Models.Etiqueta;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Domain.Models.Granularidad;
using SimemNetAdmin.Domain.Models.Menu;
using SimemNetAdmin.Domain.Models.Periodicidad;
using SimemNetAdmin.Domain.Models.TipoVista;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace SimemNetAdmin.Infra.Data.Repository.GeneracionArchivos
{
    public class GeneracionArchivoRepository : IGeneracionArchivoRepository
    {
        #region Variables

        private readonly SimemNetAdminDbContext _baseContext;

        #endregion Variables

        #region Constructor

        public GeneracionArchivoRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }

        #endregion Constructor

        #region Public Methods Simem

        public async Task<List<GeneracionArchivoJson>> GetRecords()
        {
            List<GeneracionArchivoJson> generacionArchivosJson = [];
            try
            {
                generacionArchivosJson = await _baseContext.GeneracionArchivosJson.FromSqlInterpolated($" EXEC [Configuracion].[sp_ObtenerGestionConjuntoDatosSimem]").ToListAsync();
                foreach (GeneracionArchivoJson item in generacionArchivosJson)
                {
                    Domain.Models.Notification.ExecutionLogModel? logExecution = await _baseContext.ExecutionLogModel.FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos.Equals(item.IdConfiguracionGeneracionArchivos) && f.Estado == "Iniciado")!;
                    item.PipelineRunId = logExecution?.PipelineRunId;
                }
            }
            catch (Exception ex)
            {
                generacionArchivosJson.Add(new GeneracionArchivoJson() { Fail = ex.Message });
                return generacionArchivosJson;
            }

            return generacionArchivosJson;
        }

        public async Task<DatosBasicosJson> GetDataSetBasicData(Guid idDataset)
        {
            FileGenerationModel? generacionArchivos = await _baseContext.FileGenerationModel.FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos.Equals(idDataset));
            if (generacionArchivos == null)
                return null!;

            List<GeneracionArchivoEtiqueta> generacionArchivoEtiqueta = await GetEtiquetasByIdDataset(idDataset);
            List<Categoria> categorias = await GetSelectorDataByType("Categorias", string.Empty);

            return new DatosBasicosJson()
            {
                IdConfiguracionGeneracionArchivos = generacionArchivos.IdConfiguracionGeneracionArchivos,
                Tema = generacionArchivos.Tema,
                NombreArchivoDestino = generacionArchivos.NombreArchivoDestino,
                DatoObligatorio = generacionArchivos.DatoObligatorio,
                IndRegulatorio = generacionArchivos.IndRegulatorio,
                SelectXM = generacionArchivos.SelectXM,
                NBSynapse = generacionArchivos.NBSynapse,
                IdDuracionISO = generacionArchivos.IdDuracionISO,
                ValorDeltaInicial = generacionArchivos.ValorDeltaInicial.ToShortDateString(),
                ValorDeltaFinal = generacionArchivos.ValorDeltaFinal == null ? null : Convert.ToDateTime(generacionArchivos.ValorDeltaFinal).ToShortDateString(),
                UltimaFechaIndexado = generacionArchivos.UltimaFechaIndexado == null ? null : Convert.ToDateTime(generacionArchivos.UltimaFechaIndexado).ToShortDateString(),
                UltimaFechaActualizado = generacionArchivos.UltimaFechaActualizado == null ? null : Convert.ToDateTime(generacionArchivos.UltimaFechaActualizado).ToShortDateString(),
                IdPeriodicidad = generacionArchivos.IdPeriodicidad,
                Titulo = generacionArchivos.Titulo,
                Descripcion = generacionArchivos.Descripcion,
                Privacidad = generacionArchivos.Privacidad,
                IdCategoria = generacionArchivos.IdCategoria,
                NombreCategoria = _baseContext.Categorias.FirstOrDefault(f => f.Id == generacionArchivos.IdCategoria) != null ? _baseContext.Categorias.FirstOrDefault(f => f.Id == generacionArchivos.IdCategoria)!.Titulo : string.Empty,
                IdTipoVista = generacionArchivos.IdTipoVista,
                IdGranularidad = generacionArchivos.IdGranularidad,
                EntidadOrigen = generacionArchivos.EntidadOrigen,
                Estado = generacionArchivos.Estado,
                IdConfiguracionClasificacionRegulatoria = generacionArchivos.IdConfiguracionClasificacionRegulatoria,
                Etiquetas = generacionArchivoEtiqueta.Count > 0 ? generacionArchivoEtiqueta.Select(s => s.EtiquetaId.ToString()).ToList() : []
            };
        }

        public async Task<dynamic> GetSelectorDataByType(string selectorType, string? idDataset)
        {
            switch (selectorType)
            {
                case "DuracionISO":
                    return await GetConfiguracionDuracionISO();
                case "Granularidad":
                    return await GetGranularidad();
                case "Periodicidad":
                    return await GetPeriodicidad();
                case "Etiquetas":
                    return await GetEtiquetas();
                case "EtiquetasByIdDataset":
                    return string.IsNullOrEmpty(idDataset) ? null! : await GetEtiquetasByIdDataset(new(idDataset!));
                case "Categorias":
                    return await GetCategoriaRecords();
                case "Clasificacionregulatoria":
                    List<ClasificacionRegulatoriaModel> clasificacionRegulatoria = await GetClasificacionregulatoria();
                    List<dynamic> clasificacionRegulatoriaresult = [];

                    foreach (ClasificacionRegulatoriaModel item in clasificacionRegulatoria)
                    {
                        var clasificacionRegulatoriaObj = new
                        {
                            item.IdConfiguracionClasificacionRegulatoria,
                            item.CodigoDelta,
                            item.Descripcion
                        };

                        clasificacionRegulatoriaresult.Add(clasificacionRegulatoriaObj);
                    }

                    return clasificacionRegulatoriaresult;
                case "TipoVista":
                    return await GetTipoVista();
                default:
                    return null!;
            }
        }

        [ExcludeFromCodeCoverage]
        public async Task<bool> UpdateDatosBasicos(DatosBasicosJson datosBasicos)
        {
            bool response = false;

            if (datosBasicos.Etiquetas != null && !await DeleteAndSaveGeneracionArchivoEtiquetaRecords(datosBasicos.IdConfiguracionGeneracionArchivos, datosBasicos.Etiquetas!))
                return false;

            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            try
            {
                FileGenerationModel? generacionArchivos = await _baseContext.FileGenerationModel.AsNoTracking().FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos.Equals(datosBasicos.IdConfiguracionGeneracionArchivos));
                if (generacionArchivos == null)
                    return false;

                generacionArchivos.FechaActualizacion = DateTime.Now;
                generacionArchivos.IdConfiguracionGeneracionArchivos = datosBasicos.IdConfiguracionGeneracionArchivos;
                generacionArchivos.Tema = datosBasicos.Tema;
                generacionArchivos.NombreArchivoDestino = datosBasicos.NombreArchivoDestino;
                generacionArchivos.DatoObligatorio = datosBasicos.DatoObligatorio;
                generacionArchivos.IndRegulatorio = datosBasicos.IndRegulatorio;
                generacionArchivos.SelectXM = datosBasicos.SelectXM ?? null;
                generacionArchivos.NBSynapse = datosBasicos.NBSynapse ?? null;
                generacionArchivos.IdDuracionISO = datosBasicos.IdDuracionISO == null ? null : datosBasicos.IdDuracionISO;
                generacionArchivos.ValorDeltaInicial = DateTime.ParseExact(Convert.ToDateTime(datosBasicos.ValorDeltaInicial).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                generacionArchivos.ValorDeltaFinal = datosBasicos.ValorDeltaFinal == null ? null : DateTime.ParseExact(Convert.ToDateTime(datosBasicos.ValorDeltaFinal).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                generacionArchivos.UltimaFechaIndexado = datosBasicos.UltimaFechaIndexado == null ? null : DateTime.ParseExact(Convert.ToDateTime(datosBasicos.UltimaFechaIndexado).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                generacionArchivos.UltimaFechaActualizado = datosBasicos.UltimaFechaActualizado == null ? null : DateTime.ParseExact(Convert.ToDateTime(datosBasicos.UltimaFechaActualizado).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                generacionArchivos.IdPeriodicidad = datosBasicos.IdPeriodicidad != null ? datosBasicos.IdPeriodicidad : null;
                generacionArchivos.Titulo = datosBasicos.Titulo ?? null;
                generacionArchivos.Descripcion = datosBasicos.Descripcion ?? null;
                generacionArchivos.Privacidad = datosBasicos.Privacidad;
                generacionArchivos.IdCategoria = datosBasicos.IdCategoria == null ? null : datosBasicos.IdCategoria;
                generacionArchivos.IdTipoVista = datosBasicos.IdTipoVista == null ? null : datosBasicos.IdTipoVista;
                generacionArchivos.IdGranularidad = datosBasicos.IdGranularidad == null ? null : datosBasicos.IdGranularidad;
                generacionArchivos.EntidadOrigen = datosBasicos.EntidadOrigen ?? null;
                generacionArchivos.Estado = datosBasicos.Estado;
                generacionArchivos.IdConfiguracionClasificacionRegulatoria = datosBasicos.IdConfiguracionClasificacionRegulatoria == null ? null : datosBasicos.IdConfiguracionClasificacionRegulatoria;

                _baseContext.FileGenerationModel.Update(generacionArchivos);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
                response = true;
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            return response;
        }

        public async Task<bool> HasRecordsSaved(Guid idgeneracionArchivos)
        {
            return await _baseContext.Archivo.Where(w => w.IdConfiguracionGeneracionArchivo.Equals(idgeneracionArchivos)).AnyAsync();
        }

        public async Task<List<SelectPropertiesJson>> SelectProperties(string type, string? project)
        {
            List<SelectPropertiesJson> selectPropertiesJson = [];
            List<FileGenerationModel> configuracionExtraccionesResult = await _baseContext.FileGenerationModel.ToListAsync();

            if (configuracionExtraccionesResult.Count == 0)
                return selectPropertiesJson;

            switch (type.ToLower())
            {
                case Domain.Utilidades.Constantes.SelectPropertiesJson.ColumnaDestino:
                    selectPropertiesJson = await ColumnaDestinoProperty();
                    break;
                case Domain.Utilidades.Constantes.SelectPropertiesJson.ColumnaOrigen:
                    selectPropertiesJson = await ColumnaOrigenProperty();
                    break;
            }
            return selectPropertiesJson;
        }


        public async Task<string> UpdateColumnsDestinityVersion(ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;
            try
            {
                FileGenerationModel? configuracionExtracciones = await _baseContext.FileGenerationModel.FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos == columnasOrigenJson.IdExtraccion);
                if (configuracionExtracciones == null) return "Error: No se encuentra información.";

                configuracionExtracciones.IdColumnaDestino = columnasOrigenJson.ExtraccionIdColumnaDestino;
                configuracionExtracciones.IdColumnaVersion = columnasOrigenJson.ExtraccionColumnaVersion;
                _baseContext.FileGenerationModel.Update(configuracionExtracciones);

                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                response = ex.InnerException!.Message;
                transaction.Rollback();
            }

            return response;
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<GeneracionArchivoDto>> GetDataList(Paginator? paginator, Guid? idEmpresa)
        {
            var previousResult = (from generacion in _baseContext.FileGenerationModel
                                  join DIR in _baseContext.InformacionDeltaRegulatorio
                                  on generacion.IdConfiguracionGeneracionArchivos equals DIR.IdConfiguracionGeneracionArchivos into gj
                                  from DIR in gj.DefaultIfEmpty()
                                  where generacion.IdEmpresa == idEmpresa
                                  group new { generacion, DIR } by new
                                  {
                                      generacion.IdConfiguracionGeneracionArchivos,
                                      generacion.IdDataSet,
                                      generacion.Tema,
                                      generacion.NombreArchivoDestino,
                                      generacion.Descripcion,
                                      generacion.Titulo,
                                      generacion.Periodicidad,
                                      generacion.ValorDeltaInicial,
                                      generacion.ValorDeltaFinal,
                                      generacion.IndRegulatorio
                                  } into g
                                  select new
                                  {
                                      g.Key.IdConfiguracionGeneracionArchivos,
                                      g.Key.Tema,
                                      g.Key.NombreArchivoDestino,
                                      g.Key.ValorDeltaInicial,
                                      g.Key.ValorDeltaFinal,
                                      g.Key.Periodicidad,
                                      g.Key.Titulo,
                                      g.Key.Descripcion,
                                      g.Key.IdDataSet,
                                      g.Key.IndRegulatorio,
                                      FechaProximaPublicacion = g.Max(x => x.DIR.MaximaFechaRegulatoria)
                                  });



            var dataResult = await previousResult.ToListAsync();

            List<GeneracionArchivoDto> result = [];
            foreach (var item in dataResult)
            {
                GeneracionArchivoDto data = new()
                {
                    IdConfiguracionGeneracionArchivos = item.IdConfiguracionGeneracionArchivos,
                    IdDataSet = item.IdDataSet,
                    Tema = item.Tema,
                    NombreArchivoDestino = item.NombreArchivoDestino,
                    Descripcion = item.Descripcion,
                    Titulo = item.Titulo,
                    Periodicidad = item.Periodicidad,
                    MaximaFechaRegulatoria = item.FechaProximaPublicacion,
                    ValorDeltaInicial = item.ValorDeltaInicial,
                    ValorDeltaFinal = item.ValorDeltaFinal,
                    IndRegulatorio = item.IndRegulatorio
                };
                result.Add(data);
            }

            if (paginator != null)
            {
                _ = result.Skip(paginator.PageIndex * paginator.PageSize).Take(paginator.PageSize).ToList();
            }

            return result;
        }
        #endregion Public Methods

        #region Private Methods

        private async Task<List<ConfiguracionDuracionIso>> GetConfiguracionDuracionISO() =>
            await _baseContext.ConfiguracionDuracionISO.ToListAsync();

        private async Task<List<Granularidad>> GetGranularidad() =>
           await _baseContext.Granularidad.ToListAsync();

        private async Task<List<ConfiguracionPeriodicidad>> GetPeriodicidad() =>
           await _baseContext.ConfiguracionPeriodicidad.ToListAsync();

        private async Task<List<Etiqueta>> GetEtiquetas() =>
          await _baseContext.Etiquetas.Where(w => w.Estado).ToListAsync();

        private async Task<List<GeneracionArchivoEtiqueta>> GetEtiquetasByIdDataset(Guid idDataset) =>
        await _baseContext.GeneracionArchivoEtiquetas.Where(w => w.IdConfiguracionGeneracionArchivo.Equals(idDataset)).ToListAsync();

        private async Task<List<ClasificacionRegulatoriaModel>> GetClasificacionregulatoria() =>
            await _baseContext.ClasificacionRegulatoriaModel.ToListAsync();

        private async Task<List<TipoVista>> GetTipoVista() =>
          await _baseContext.TiposVista.ToListAsync();

        #region Categoria
        [ExcludeFromCodeCoverage]
        private static async Task<List<Categoria>> GetCategoriaRecords()
        {
            using var context = new SimemNetAdminDbContext();
            List<Categoria> categoriaRecords = await context.Categorias.ToListAsync();
            List<Categoria> categoriaRecordsLst = [];

            foreach (Categoria? records in categoriaRecords.Where(w => w.IdCategoria == null && w.Estado))
            {
                if (HasChildren(categoriaRecords, records!.Id))
                    records.Children = GetMenuChildrens(categoriaRecords, records.Id);

                categoriaRecordsLst.Add(records);
            }

            return categoriaRecordsLst;
        }

        [ExcludeFromCodeCoverage]
        private static List<dynamic> GetMenuChildrens(List<Categoria> categoriaLst, Guid? id)
        {
            List<dynamic> menuChildrens = [];
            foreach (Categoria? children in categoriaLst.Where(w => w.IdCategoria == id))
            {
                if (HasChildren(categoriaLst, children.Id))
                    children.Children = GetMenuChildrens(categoriaLst, children.Id);

                menuChildrens.Add(children);
            }

            return menuChildrens;
        }

        [ExcludeFromCodeCoverage]
        private static bool HasChildren(List<Categoria> categoriaLst, Guid? id)
        {
            if (categoriaLst.Any(w => w.IdCategoria == id!))
                return true;
            return false;
        }
        #endregion Categoria

        [ExcludeFromCodeCoverage]
        private async Task<bool> DeleteAndSaveGeneracionArchivoEtiquetaRecords(Guid idDataset, List<string> idEtiquetas)
        {
            List<GeneracionArchivoEtiqueta> generacionArchivoEtiquetas = [];
            foreach (string item in idEtiquetas)
            {
                generacionArchivoEtiquetas.Add(new GeneracionArchivoEtiqueta
                {
                    IdConfiguracionGeneracionArchivo = idDataset,
                    EtiquetaId = new(item),
                    FechaCreacion = DateTime.Now
                });
            }

            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            try
            {

                _baseContext.GeneracionArchivoEtiquetas.AsNoTracking().ToList().RemoveAll(w => w.IdConfiguracionGeneracionArchivo.Equals(idDataset));
                if (generacionArchivoEtiquetas.Count > 0)
                    _baseContext.GeneracionArchivoEtiquetas.AddRange(generacionArchivoEtiquetas);

                await _baseContext.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }


        private async Task<List<SelectPropertiesJson>> ColumnaDestinoProperty()
        {
            List<ConfiguracionColumnasDestino> configuracionColumnasDestinoResult = await _baseContext.ColumnasDestino.Where(w => w.TipoDato == "fecha" || w.TipoDato == "fecha hora").ToListAsync();
            return GetColumnsDestinoValues(configuracionColumnasDestinoResult);
        }

        private static List<SelectPropertiesJson> GetColumnsDestinoValues(List<ConfiguracionColumnasDestino> configuracionColumnasorigenResult)
        {
            List<SelectPropertiesJson> selectPropertiesJson = [];
            foreach (Guid item in configuracionColumnasorigenResult.Select(s => s.IdColumnaDestino).ToList())
            {
                SelectPropertiesJson propertiesJson = new()
                {
                    IdColumnasDestino = item,
                    Value = configuracionColumnasorigenResult.FirstOrDefault(f => f.IdColumnaDestino == item)!.NombreColumnaDestino,
                    TipoDato = configuracionColumnasorigenResult.FirstOrDefault(f => f.IdColumnaDestino == item)!.TipoDato,
                    descripcion = configuracionColumnasorigenResult.FirstOrDefault(f => f.IdColumnaDestino == item)!.Descripcion,
                };
                selectPropertiesJson.Add(propertiesJson);
            }

            return selectPropertiesJson;
        }

        private async Task<List<SelectPropertiesJson>> ColumnaOrigenProperty()
        {
            List<ConfiguracionColumnasDestino> configuracionColumnasorigenResult = await _baseContext.ColumnasDestino.ToListAsync();
            return GetColumnsDestinoValues(configuracionColumnasorigenResult);
        }
        #endregion Private Methods
    }
}