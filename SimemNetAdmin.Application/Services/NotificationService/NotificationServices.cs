using SimemNetAdmin.Application.Interfaces.NotificationService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using Newtonsoft.Json;
using SimemNetAdmin.Infra.Data.Repository.NotificationRepository;
using SimemNetAdmin.Transversal.SendNotifications;

namespace SimemNetAdmin.Application.Services.NotificationService
{
    public class NotificationServices : INotificationService
    {
        private readonly NotificationRepository _notificationRepository = new();
        private readonly ParameterNotificationRepository _parameterNotificationRepository = new();
        private readonly EmailSender _emailSender = new();

        public NotificationServices() { }

        public async void GenerateSendNotification(ParameterNotificationViewModel parameterNotification)
        {
            try
            {
                var notificationEmails = await _parameterNotificationRepository.GetNotificationEmail(parameterNotification.IdNotificationType);
                switch (parameterNotification.NotificationType)
                {
                    case NotificationType.ErrorDataSet:
                        var logNotificationResults = await _notificationRepository.GetLogExecution()!;
                        _ = SendNotification(parameterNotification, notificationEmails, logNotificationResults!, logNotificationResults!.Count);
                        _notificationRepository.UpdateSubmittedErrors(logNotificationResults!);
                        break;

                    case NotificationType.DataSetRegulatorio:
                        List<NotificacionDataSetRegulatorioViewModel> recordsNotification = await GenerateSendRegulatoryDataSet(false);
                        _= SendNotification(parameterNotification, notificationEmails, recordsNotification, recordsNotification.Count);
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveLog(parameterNotification, [], 0, ex.Message, ex.Source!, "Error");
            }
        }

        public async Task<List<ParameterNotificationViewModel>> GetParameterNotification()
        {
            return await _parameterNotificationRepository.GetParameterNotification();
        }

        public async Task<List<NotificationResult>?> GetLogExecution()
        {
            return await _notificationRepository.GetLogExecution();
        }

        public async Task<List<ExecutionAndErrorMonitoringViewModel>> GetExecutionAndErrorMonitoring()
        {
            return await _notificationRepository.GetExecutionAndErrorMonitoring();
        }

        public async Task<List<EmailData>?> GetNotificationEmail(Guid TypeNotificacion)
        {
            return await _parameterNotificationRepository.GetNotificationEmail(TypeNotificacion);
        }

        

        public async Task<List<NotificacionDataSetRegulatorioViewModel>> GenerateSendRegulatoryDataSet(bool cumple = false, string? idEmpresa = null)
        {
            return await _notificationRepository.GetNotificacionDataSetRegulatorio(cumple,idEmpresa);
        }

        public async Task SendNotification(ParameterNotificationViewModel parameterNotification, List<EmailData> emails, dynamic templateData, int nroRecords)
        {
            var notificationEmails = await _parameterNotificationRepository.GetNotificationEmail(parameterNotification.IdNotificationType);
            if (notificationEmails.Count > 0 && nroRecords > 0)
            {
                var notificaciones = new TemplateData
                {
                    notificaciones = templateData
                };

                var dataSengrid = new DataSendgrid
                {
                    templateId = parameterNotification.IdTemplate,
                    toEmails = emails,
                    templateData = notificaciones
                };
                var dataSengridJson = JsonConvert.SerializeObject(dataSengrid);
                _ = _emailSender.SendNotificacions(dataSengridJson);
                SaveLog(parameterNotification, emails, nroRecords);
            }

            if (notificationEmails.Count == 0) {
                SaveLog(parameterNotification, [], nroRecords,"No se encontraron correos configurados", "SendNotification");
            }

            if (nroRecords == 0)
            {
                SaveLog(parameterNotification, [], nroRecords, "No se encontraron registros para enviar", "SendNotification");
            }
        }

        public  void SaveLog(ParameterNotificationViewModel parameterNotification, List<EmailData> emails, int numberRecords, string descriptionError = "", string methodFailed = "", string status = "Enviado") 
        {
            List<string> emailsList = emails.Select(email => email.email).ToList()!;
            string mailsString = string.Join(", ", emailsList);
            LogSendNotificationViewModel logSend = new()
            {

                IdTipoNotificacion = parameterNotification.IdNotificationType,
                IdParametroNotificacion = parameterNotification.IdParametroNotificacion,
                IdCorreoNotificacion = mailsString,
                DescripcionError = descriptionError,
                MetodoError = methodFailed,
                EstadoEnvio = status,
                NumeroRegistros = numberRecords,
                FechaEjecucion = DateTime.Now
            };
            _notificationRepository.SaveLog(logSend);
        }

    }
}
