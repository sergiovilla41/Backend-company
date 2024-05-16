using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Etiqueta;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Infra.Data.Mapping;

namespace SimemNetAdmin.Infra.Data.Repository
{
    public class LabelsRepository : ILabelsRepository
    {
        #region Variables
        private readonly SimemNetAdminDbContext _dbContext;
        #endregion
        #region Constructor       

        public LabelsRepository()
        {
            _dbContext ??= new SimemNetAdminDbContext();
        }
        #endregion
        //listing tags
        public async Task<List<LabelsDto>> ListLabels()
        {

            var labels = await _dbContext.Etiquetas.ToListAsync();
            var labelsDto = DataProfile.Mapper.Map<List<LabelsDto>>(labels);
            return labelsDto;
        }
        //listing tags by id
        public async Task<LabelsDto> GetLabelById(Guid id)
        {
            var label = await _dbContext.Etiquetas.FindAsync(id);
            return DataProfile.Mapper.Map<LabelsDto>(label);
        }
        //update tags
        public async Task<bool> UpdateLabel(LabelsDto labelDto)
        {
            var label = await _dbContext.Etiquetas.FindAsync(labelDto.Id);
            if (label == null)
            {
                return false;
            }

            label.Titulo = labelDto.Titulo;
            label.Estado = labelDto.Estado;

            _dbContext.Etiquetas.Update(label);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
        //Add tag
        public async Task<bool> CreateLabel(LabelsDto labelDto)
        {
            labelDto.Id = Guid.NewGuid();
            // Map the EtiquetaDto to an Etiqueta entity
            var label = DataProfile.Mapper.Map<Etiqueta>(labelDto);

            // Add the new etiqueta to the DbContext
            await _dbContext.Etiquetas.AddAsync(label);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return true;

        }

    }
}
