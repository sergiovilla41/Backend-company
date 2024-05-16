using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Publication;
using SimemNetAdmin.Domain.ViewModel.Publication;
using SimemNetAdmin.Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Infra.Data.Repository.Publication
{
    public class PublicationRepository : IPublicationRepository
    {
        #region Public Methods
        public async Task<List<PublicationModel>?> GetById(string idDataSet)
        {
            using var _baseContext = new SimemNetAdminDbContext();

            return await _baseContext.PublicationModel.Where(w => w.IdConfiguracionGeneracionArchivos.Equals(new(idDataSet!))).ToListAsync();
        }

        [ExcludeFromCodeCoverage]
        public async Task<string> Save(PublicationDto publicationModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                PublicationModel entity = new()
                {
                    IdConfiguracionGeneracionArchivos = new(publicationModel.IdConfiguracionGeneracionArchivos!),
                    Dia = publicationModel.Dia,
                    Mes = publicationModel.Mes,
                    DiaSemana = publicationModel.DiaSemana,
                    IndDiaHabil = publicationModel.IndDiaHabil,
                    FechaCreacion = DateTime.Now.ToUniversalTime().AddHours(-5.0)
                };
                await _baseContext.PublicationModel.AddAsync(entity);
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
        public async Task<string> Update(PublicationDto publicationModel)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                PublicationModel? oldEntity = await _baseContext.PublicationModel.FirstOrDefaultAsync(x => x.IdPublicacionRegulatoria.Equals(new(publicationModel.IdPublicacionRegulatoria!)));

                if (oldEntity == null)
                    return "No se encontró información con el id de ejecución proporcionado";

                PublicationModel entity = oldEntity!;

                entity.Dia = publicationModel.Dia;
                entity.Mes = publicationModel.Mes;
                entity.DiaSemana = publicationModel.DiaSemana;
                entity.IndDiaHabil = publicationModel.IndDiaHabil;

                _baseContext.PublicationModel.Update(entity!);
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
        public async Task<string> Delete(string idPublicacionRegulatoria)
        {
            if (string.IsNullOrEmpty(idPublicacionRegulatoria))
                return "Debe especificar un id del conjunto de datos valido.";

            using var _baseContext = new SimemNetAdminDbContext();
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _baseContext.Database.BeginTransaction();
            string? response = string.Empty;

            try
            {
                PublicationModel? entity = await _baseContext.PublicationModel.FirstOrDefaultAsync(x => x.IdPublicacionRegulatoria.Equals(new(idPublicacionRegulatoria!)));

                if (entity == null)
                    return "No se encontró información con el id de ejecución proporcionado";

                _baseContext.PublicationModel.Remove(entity!);
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
