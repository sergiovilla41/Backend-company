using SimemNetAdmin.Domain.Models.Execution;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IExecutionConfigurationRepository
    {
        public Task<List<ExecutionModel>> ExecutionConfigurationData(Guid idExtraccion);
    }
}
