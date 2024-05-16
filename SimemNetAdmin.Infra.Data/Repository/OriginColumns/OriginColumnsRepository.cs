using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository.ColumnasOrigenRepository
{
    public class OriginColumnsRepository : IOriginColumnsRepository
    {
        public async Task<string> UpdateColumnasOrigen(ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                ConfiguracionColumnasOrigen? columnasOrigen = await _baseContext.ColumnasOrigen.FirstOrDefaultAsync(f => f.IdColumnaOrigen == columnasOrigenJson.IdColumnaOrigen);
                if (columnasOrigen == null) return "Error: No se encuentra información.";

                columnasOrigen.NumeracionColumna = columnasOrigenJson.Numeracion;
                columnasOrigen.NombreColumnaOrigen = columnasOrigenJson.ColumnaOrigen;
                columnasOrigen.IdColumnaDestino = columnasOrigenJson.IdColumnaDestino;
                _baseContext.ColumnasOrigen.Update(columnasOrigen);

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

        public async Task<List<ConfiguracionColumnasOrigenJson>> GetColumnasOrigenJson(Guid idExtraccion)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            List<ConfiguracionColumnasOrigenJson> columnasOrigenJsons = [];
            List<ConfiguracionColumnasOrigen> columnasOrigen = await _baseContext.ColumnasOrigen.Where(w => w.IdConfiguracionGeneracionArchivos == idExtraccion).ToListAsync();
            FileGenerationModel? configuracionExtracciones;

            foreach (ConfiguracionColumnasOrigen columna in columnasOrigen)
            {
                columna.ColumnasDestino = await _baseContext.ColumnasDestino.FirstOrDefaultAsync(f => f.IdColumnaDestino == columna.IdColumnaDestino);
                configuracionExtracciones = await _baseContext.FileGenerationModel.FirstOrDefaultAsync(f => f.IdConfiguracionGeneracionArchivos == columna.IdConfiguracionGeneracionArchivos);

                ConfiguracionColumnasOrigenJson columnasOrigenJson = new()
                {
                    IdColumnaOrigen = columna.IdColumnaOrigen,
                    Numeracion = columna.NumeracionColumna,
                    ColumnaOrigen = columna.NombreColumnaOrigen!,
                    IdColumnaDestino = columna.IdColumnaDestino,
                    NombreColumnaDestino = columna.ColumnasDestino!.NombreColumnaDestino,
                    TipoDato = columna.ColumnasDestino!.TipoDato,
                    Descripcion = columna.ColumnasDestino!.Descripcion,
                    IdExtraccion = configuracionExtracciones!.IdConfiguracionGeneracionArchivos,
                    ExtraccionIdColumnaDestino = configuracionExtracciones.IdColumnaDestino,
                    ExtraccionColumnaVersion = configuracionExtracciones.IdColumnaVersion
                };
                columnasOrigenJsons.Add(columnasOrigenJson);
            }
            return columnasOrigenJsons;
        }

    }
}
