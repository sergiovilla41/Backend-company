using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.NotificationViewModel
{
    [ExcludeFromCodeCoverage]
    public class ParameterNotificationViewModel
    {
        public Guid IdParametroNotificacion { get; set; }
        public Byte ExecutionTime { get; set; }
        public Byte ExecutionMinute { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public Guid IdNotificationType { get; set; }
        public string IdTemplate { get; set; } = string.Empty;
    }
}
