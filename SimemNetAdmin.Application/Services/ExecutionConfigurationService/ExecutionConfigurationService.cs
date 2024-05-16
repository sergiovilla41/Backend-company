using SimemNetAdmin.Application.Interfaces.ConfiguracionEjecucionService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;

namespace SimemNetAdmin.Application.Services.ConfiguracionEjecucionService
{
    public class ExecutionConfigurationService : IExecutionConfigurationService
    {
        private readonly IExecutionConfigurationRepository _configuracionEjecucionRepository;

        public ExecutionConfigurationService(IExecutionConfigurationRepository configuracionEjecucionRepository)
        {
            _configuracionEjecucionRepository = configuracionEjecucionRepository ?? throw new ArgumentNullException(nameof(configuracionEjecucionRepository));
        }

        public async Task<List<ExecutionModel>> ExecutionConfigurationData(Guid idExtraccion)
        {
            return await _configuracionEjecucionRepository.ExecutionConfigurationData(idExtraccion)!;
        }
    }
}
