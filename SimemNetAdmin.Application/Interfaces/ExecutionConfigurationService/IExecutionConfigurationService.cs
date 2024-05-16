using SimemNetAdmin.Domain.Models.Execution;

namespace SimemNetAdmin.Application.Interfaces.ConfiguracionEjecucionService
{
    public interface IExecutionConfigurationService
    {
        public Task<List<ExecutionModel>> ExecutionConfigurationData(Guid idExtraccion);
    }
}
