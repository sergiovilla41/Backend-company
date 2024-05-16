using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository.ConfiguracionEjecucion
{
    public class ExecutionConfigurationRepository: IExecutionConfigurationRepository
    {

        public async Task<List<ExecutionModel>> ExecutionConfigurationData(Guid idExtraccion)
        {
            using var _baseContext = new SimemNetAdminDbContext();
            return await _baseContext.ExecutionModel.Where(w => w.IdConfiguracionGeneracionArchivos == idExtraccion).ToListAsync();
        }
    }
}
