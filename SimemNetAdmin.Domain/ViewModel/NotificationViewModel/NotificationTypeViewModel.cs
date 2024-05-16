using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.NotificationViewModel
{
    [ExcludeFromCodeCoverage]
    public class NotificationTypeViewModel
    {
        public Guid IdTipoNotificacion { get; set; }
        public string? TipoNotificacion { get; set; }
        public bool Activo { get; set; }
    }
}
