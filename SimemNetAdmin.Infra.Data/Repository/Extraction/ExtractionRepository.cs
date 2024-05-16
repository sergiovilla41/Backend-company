using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Domain.ViewModel.Extracciones;
using System.Diagnostics.CodeAnalysis;
using SimemNetAdmin.Domain.Models.Extraction;

namespace SimemNetAdmin.Infra.Data.Repository.Extraction
{
    public class ExtractionRepository : IExtractionRepository
    {
        #region Public Methods
        public async Task<List<ExtractionsModel>?> GetById(string? idExtraccion, string? idDataSet)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            if (!string.IsNullOrEmpty(idExtraccion))
                return await _baseContext.ExtractionsModel.Where(w => w.IdExtraccion.Equals(new(idExtraccion!))).ToListAsync();

            return await _baseContext.ExtractionsModel.Where(w => w.IdConfiguracionGeneracionArchivos.Equals(new(idDataSet!))).ToListAsync();
        }

        [ExcludeFromCodeCoverage]
        public async Task<string> Save(ExtractionsModelDto extractionsModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                ExtractionsModel extractions = new()
                {
                    IdConfiguracionGeneracionArchivos = new(extractionsModel.IdConfiguracionGeneracionArchivos!),
                    Proyecto = "SIMEM",
                    Tema = extractionsModel.Tema,
                    NombreExtraccion = extractionsModel.NombreExtraccion!,
                    Periodicidad = extractionsModel.Periodicidad,
                    SecretoKeyVaultOrigenDBXM = "cndbsdatalakesqlConfigXM",
                    IntervaloPeriodicidad = extractionsModel.IntervaloPeriodicidad,
                    FechaDeltaInicial = extractionsModel.FechaDeltaInicial != null ? Convert.ToDateTime(Convert.ToDateTime(extractionsModel.FechaDeltaInicial).ToString("yyy-MM-dd")) : null,
                    FechaDeltaFinal = extractionsModel.FechaDeltaFinal != null ? Convert.ToDateTime(Convert.ToDateTime(extractionsModel.FechaDeltaFinal).ToString("yyyy-MM-dd")) : null,
                    FechaCreacion = DateTime.Now.ToUniversalTime().AddHours(-5.0)
                };
                await _baseContext.ExtractionsModel.AddAsync(extractions);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                response = ex.Message;
                transaction.Rollback();
            }
            return response;
        }

        [ExcludeFromCodeCoverage]
        public async Task<string> Update(ExtractionsModelDto extractionsModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                List<ExtractionsModel>? extractionsModels = await GetById(extractionsModel.IdExtraccion!.ToString(), string.Empty);
                if (extractionsModels!.Count == 0)
                    return "No se encontro información con el id de extracción proporcionado";

                ExtractionsModel extraction = extractionsModels!.FirstOrDefault()!;

                extraction.Tema = extractionsModel.Tema;
                extraction.NombreExtraccion = extractionsModel.NombreExtraccion!;
                extraction.Periodicidad = extractionsModel.Periodicidad;
                extraction.IntervaloPeriodicidad = extractionsModel.IntervaloPeriodicidad;
                extraction.FechaDeltaInicial = extractionsModel.FechaDeltaInicial != null ? Convert.ToDateTime(Convert.ToDateTime(extractionsModel.FechaDeltaInicial).ToString("yyy-MM-dd")) : null;
                extraction.FechaDeltaFinal = extractionsModel.FechaDeltaFinal != null ? Convert.ToDateTime(Convert.ToDateTime(extractionsModel.FechaDeltaFinal).ToString("yyy-MM-dd")) : null;
                extraction.FechaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5.0);
                _baseContext.ExtractionsModel.Update(extraction!);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                response = ex.Message;
                transaction.Rollback();
            }
            return response;
        }

        [ExcludeFromCodeCoverage]
        public async Task<string> Delete(string idExtraccion)
        {
            if (string.IsNullOrEmpty(idExtraccion))
                return "Debe especificar un id de extracción valido.";

            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                List<ExtractionsModel>? extractionsModels = await GetById(idExtraccion, string.Empty);
                if (extractionsModels!.Count == 0)
                    return "No se encontro información con el id de extracción proporcionado";

                _baseContext.ExtractionsModel.Remove(extractionsModels!.FirstOrDefault()!);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                response = ex.Message;
                transaction.Rollback();
            }
            return response;
        }

        [ExcludeFromCodeCoverage]
        public async Task<ExtractionsModel?> GetExtraccionesDataByIdConfiguracionGeneracionArchivos(Guid idConfiguracionGeneracionArchivos)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            return await _baseContext.ExtractionsModel.FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos == idConfiguracionGeneracionArchivos);
        }

        #endregion
    }
}
