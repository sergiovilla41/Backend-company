using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository
{
    public class AssociatedDataSetRepository : IAssociatedDataSetRepository
    {

        #region Variables
        private readonly SimemNetAdminDbContext _baseContext;
        #endregion
        #region Constructor       

        public AssociatedDataSetRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }
        #endregion
        ///Get Conjunto de datos asociados 

        public async Task<List<LabelDataSetDto>> GetDataDto()
        {
            var res = await _baseContext.Etiquetas.Select(a => new LabelDataSetDto
            {
                Id = a.Id,
                Estado = a.Estado,
                Titulo = a.Titulo,
                GeneracionArchivos = a.GeneracionArchivos!.Select(b => new Domain.ViewModel.FileGenerationModel.FileGenerationModelDto
                {
                    Titulo = b.Titulo

                }).ToList(),

            }).ToListAsync();
            return res;
        }
    }
}
