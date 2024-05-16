using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Infra.Data.Repository
{
    public class ExecutionLogRepository: IExecutionLogRepository
    {
        private readonly SimemNetAdminDbContext _baseContext;

        #region Constructor
        public ExecutionLogRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }
        #endregion

        #region Public Methods Simem
        public async Task<string> CancelPipeline(string pipelineId)
        {
            try
            {
                ExecutionLogModel? executionLog = await _baseContext.ExecutionLogModel.FirstOrDefaultAsync(f => f.PipelineRunId!.Equals(pipelineId));
                if (executionLog == null)
                    return "No se encuentran datos de ejecución.";

                executionLog.Estado = "Cancelado";
                _baseContext.Update(executionLog);
                await _baseContext.SaveChangesAsync();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
