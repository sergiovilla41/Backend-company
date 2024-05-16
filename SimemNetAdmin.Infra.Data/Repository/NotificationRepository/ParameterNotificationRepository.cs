using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Interfaces.NotificationDomain;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using SimemNetAdmin.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Infra.Data.Repository.NotificationRepository
{
    public class ParameterNotificationRepository : IParameterNotificationRepository
    {

        public ParameterNotificationRepository() { }

        public async Task<List<EmailData>> GetNotificationEmail(Guid NotificationType)
        {
            List<EmailData> notificationEmails = [];
            try
            {

                using var context = new SimemNetAdminDbContext();               
                notificationEmails = await Task.Run(() =>
                 new List<EmailData>(
                         from ne in context.Set<NotificationEmailModel>()
                         join nt in context.Set<NotificacionTypeModel>()
                         on ne.IdTipoNotificacion equals nt.IdTipoNotificacion
                         where ne.IdTipoNotificacion == NotificationType && ne.Estado
                         select new EmailData
                         {
                             name = ne.Nombre,
                             email   = ne.Correo
                         }).ToList());

                return notificationEmails;
            }
            catch (Exception)
            {
                return notificationEmails;
            }
        }

        public async Task<List<ParameterNotificationViewModel>> GetParameterNotification()
        {
            List<ParameterNotificationViewModel> parameterNotifications = [];
            try
            {

                using var context = new SimemNetAdminDbContext();
                parameterNotifications = await Task.Run(() =>
                 new List<ParameterNotificationViewModel>(
                         from pn in context.Set<ParameterNotificationModel>()
                         join nt in context.Set<NotificacionTypeModel>()
                         on pn.IdTipoNotificacion equals nt.IdTipoNotificacion
                         where pn.Estado
                         select new ParameterNotificationViewModel
                         {
                             IdParametroNotificacion = pn.IdParametroNotificacion,
                             ExecutionTime = pn.HoraEjecucion,
                             ExecutionMinute = pn.MinutoEjecucion,
                             IdNotificationType = nt.IdTipoNotificacion,
                             NotificationType = nt.TipoNotificacion!,
                             IdTemplate = pn.IdPlantilla
                         }).OrderBy(x=> x.ExecutionTime).ThenBy(x=>x.ExecutionMinute).ToList());

                return parameterNotifications;
            }
            catch (Exception)
            {
                return parameterNotifications;
            }
        }
    }
}
