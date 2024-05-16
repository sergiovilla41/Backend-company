using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Interfaces.NotificationDomain
{
    public interface IParameterNotificationRepository
    {
        public Task<List<EmailData>> GetNotificationEmail(Guid NotificationType);
        public Task<List<ParameterNotificationViewModel>> GetParameterNotification();
    }
}
