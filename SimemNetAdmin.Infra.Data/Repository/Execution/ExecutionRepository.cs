using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.ViewModel.Execution;
using SimemNetAdmin.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Infra.Data.Repository.Execution
{
    public class ExecutionRepository : IExecutionRepository
    {
        #region Public Methods
        public async Task<List<ExecutionModel>?> GetById(string idDataSet)
        {
            using var _baseContext = new SimemNetAdminDbContext();

            return await _baseContext.ExecutionModel.Where(w => w.IdConfiguracionGeneracionArchivos.Equals(new(idDataSet!))).ToListAsync();
        }

        [ExcludeFromCodeCoverage]
        public async Task<string> Save(ExecutionDto executionModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                ExecutionModel entity = new()
                {
                    IdConfiguracionGeneracionArchivos = new(executionModel.IdConfiguracionGeneracionArchivos!),
                    Dia = executionModel.Dia,
                    Mes = executionModel.Mes,
                    Hora = executionModel.Hora,
                    DiaSemana = executionModel.DiaSemana,
                    IndDiaHabil = executionModel.IndDiaHabil,
                    IndActivo = executionModel.IndActivo,
                    FechaCreacion = DateTime.Now.ToUniversalTime().AddHours(-5.0)
                };
                await _baseContext.ExecutionModel.AddAsync(entity);
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
        public async Task<string> Update(ExecutionDto executionModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                ExecutionModel? oldEntity = await _baseContext.ExecutionModel.FirstOrDefaultAsync(x => x.IdEjecucion.Equals(new(executionModel.IdEjecucion!)));
                if (oldEntity == null)
                    return "No se encontro información con el id de ejecución proporcionado";

                ExecutionModel entity = oldEntity!;

                entity.Dia = executionModel.Dia;
                entity.Mes = executionModel.Mes;
                entity.Hora = executionModel.Hora;
                entity.DiaSemana = executionModel.DiaSemana;
                entity.IndDiaHabil = executionModel.IndDiaHabil;
                entity.IndActivo = executionModel.IndActivo;
                entity.FechaActualizacion = DateTime.Now.ToUniversalTime().AddHours(-5.0);

                _baseContext.ExecutionModel.Update(entity!);
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
        public async Task<string> Delete(string idEjecucion)
        {
            if (string.IsNullOrEmpty(idEjecucion))
                return "Debe especificar un id de ejecución valido.";

            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                ExecutionModel? execution = await _baseContext.ExecutionModel.FirstOrDefaultAsync(w => w.IdEjecucion.Equals(new(idEjecucion!)));
                if (execution == null)
                    return "No se encontro información con el id de ejecución proporcionado";

                _baseContext.ExecutionModel.Remove(execution!);
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
        #endregion
    }
}
