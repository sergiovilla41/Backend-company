using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;

namespace SimemNetAdmin.Application.Interfaces.NotificationService
{

    public interface INotificationService
    {
        public void GenerateSendNotification(ParameterNotificationViewModel parameterNotification);
        public Task<List<ParameterNotificationViewModel>> GetParameterNotification();

        public  Task<List<ExecutionAndErrorMonitoringViewModel>> GetExecutionAndErrorMonitoring();
        public Task<List<NotificacionDataSetRegulatorioViewModel>> GenerateSendRegulatoryDataSet(bool cumple = false, string? idEmpresa = null);
    }
}
