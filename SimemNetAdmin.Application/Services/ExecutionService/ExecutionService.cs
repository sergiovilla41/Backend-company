using SimemNetAdmin.Application.Interfaces.ExecutionService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.ViewModel.Execution;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Application.Services.ExecutionService
{
    [ExcludeFromCodeCoverage]
    public class ExecutionService : IExecutionService
    {
        private readonly IExecutionRepository _executionRepository;

        public ExecutionService(IExecutionRepository executionRepository)
        {
            _executionRepository = executionRepository ?? throw new ArgumentNullException(nameof(executionRepository));
        }

        public async Task<List<ExecutionModel>?> GetById(string idDataSet)
        {
            return await _executionRepository.GetById(idDataSet)!;
        }

        public async Task<string> Save(ExecutionDto executionModel)
        {
            return await _executionRepository.Save(executionModel)!;
        }
        public async Task<string> Update(ExecutionDto executionModel)
        {
            return await _executionRepository.Update(executionModel)!;
        }

        public async Task<string> Delete(string idEjecucion)
        {
            return await _executionRepository.Delete(idEjecucion)!;
        }

    }
}
