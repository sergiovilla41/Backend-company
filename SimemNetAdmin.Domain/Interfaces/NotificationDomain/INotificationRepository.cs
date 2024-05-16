using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Interfaces.NotificationDomain
{
    public interface INotificationRepository
    {
        public Task<List<NotificationResult>?> GetLogExecution();
        public Task<List<NotificacionDataSetRegulatorioViewModel>> NotificacionDataSetRegulatorio(string? idEmpresa = null);
        public bool UpdateSubmittedErrors(List<NotificationResult> notificationResult);
        public bool SaveLog(LogSendNotificationViewModel logSend);
        public Task<List<ExecutionAndErrorMonitoringViewModel>> GetExecutionAndErrorMonitoring();
        public Task<List<NotificacionDataSetRegulatorioViewModel>> GenerateSendRegulatoryDataSet(bool cumple = true, string? idEmpresa = null);
    }
}
