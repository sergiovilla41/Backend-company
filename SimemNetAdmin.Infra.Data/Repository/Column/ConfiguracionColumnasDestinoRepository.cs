using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces.Columns;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.Models.Etiqueta;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SimemNetAdmin.Infra.Data.Repository.Column.ConfiguracionColumnasDestinoRepository;

namespace SimemNetAdmin.Infra.Data.Repository.Column
{
    public class ConfiguracionColumnasDestinoRepository : IConfiguracionColumnasDestinoRepository
    {
        #region Variables
        private readonly SimemNetAdminDbContext _dbContext;
        #endregion
        #region Constructor       

        public ConfiguracionColumnasDestinoRepository()
        {
            _dbContext ??= new SimemNetAdminDbContext();
        }
        #endregion
        
        public async Task<List<ConfiguracionColumnasDestinoDTO>> ListColumnaDestino()
        {

            var columns = await _dbContext.ColumnasDestino.ToListAsync();
            var columnsDto = DataProfile.Mapper.Map<List<ConfiguracionColumnasDestinoDTO>>(columns);
            return columnsDto;
        }


        public async Task UpdateColumnaDestinoAsync(ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
            var existingColumnaDestino = await _dbContext.ColumnasDestino
                .FirstOrDefaultAsync(x => x.IdColumnaDestino == columnaDestinoDTO.IdColumnaDestino);

            if (existingColumnaDestino != null)
            {
                existingColumnaDestino.NombreColumnaDestino = columnaDestinoDTO.NombreColumnaDestino;
                existingColumnaDestino.TipoDato = columnaDestinoDTO.TipoDato;
                existingColumnaDestino.AtributoVariable = columnaDestinoDTO.AtributoVariable;
                existingColumnaDestino.VariableId = columnaDestinoDTO.VariableId;
                existingColumnaDestino.Estado = columnaDestinoDTO.Estado;
                existingColumnaDestino.Descripcion = columnaDestinoDTO.Descripcion;
                existingColumnaDestino.FechaActualizacion = columnaDestinoDTO.FechaActualizacion ?? DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("No se encontró la columna destino con el ID proporcionado.");
            }
        }

        public async Task<Guid> CreateColumnaDestino(ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
            
                var nuevaColumnaDestino = new ConfiguracionColumnasDestino
                {
                    IdColumnaDestino = Guid.NewGuid(),
                    NombreColumnaDestino = columnaDestinoDTO.NombreColumnaDestino,
                    TipoDato = columnaDestinoDTO.TipoDato,
                    AtributoVariable = columnaDestinoDTO.AtributoVariable,
                    VariableId = columnaDestinoDTO.VariableId,
                    Estado = columnaDestinoDTO.Estado,
                    Descripcion = columnaDestinoDTO.Descripcion,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = columnaDestinoDTO?.FechaActualizacion,
                };

                _dbContext.Set<ConfiguracionColumnasDestino>().Add(nuevaColumnaDestino);
                await _dbContext.SaveChangesAsync();

                return nuevaColumnaDestino.IdColumnaDestino;
           
        }
       

    }

}





