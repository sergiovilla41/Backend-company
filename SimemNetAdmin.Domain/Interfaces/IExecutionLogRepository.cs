using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IExecutionLogRepository
    {
        public Task<string> CancelPipeline(string pipelineId);
    }
}
