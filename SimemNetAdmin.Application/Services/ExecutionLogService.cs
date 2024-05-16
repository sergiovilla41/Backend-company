using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.Interfaces;

namespace SimemNetAdmin.Application.Services
{
    public class ExecutionLogService : IExecutionLogService
    {
        private readonly IExecutionLogRepository _executionLogRepository;

        public ExecutionLogService(IExecutionLogRepository executionLogRepository)
        {
            _executionLogRepository = executionLogRepository ?? throw new ArgumentNullException(nameof(executionLogRepository));
        }
        public async Task<string> CancelPipeline(string pipelineId)
        {
            return await _executionLogRepository.CancelPipeline(pipelineId);
        }
    }
}
