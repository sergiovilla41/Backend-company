using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Interfaces
{
    public interface IExecutionLogService
    {
        public Task<string> CancelPipeline(string pipelineId);
    }
}
