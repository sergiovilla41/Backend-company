using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.ViewModel.Execution;

namespace SimemNetAdmin.Application.Interfaces.ExecutionService
{
    public interface IExecutionService
    {
        public Task<List<ExecutionModel>?> GetById(string idDataSet);
        public Task<string> Save(ExecutionDto executionModel);
        public Task<string> Update(ExecutionDto executionModel);
        public Task<string> Delete(string idEjecucion);
    }
}
