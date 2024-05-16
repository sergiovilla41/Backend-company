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
    public class NotificationEmailViewModel
    {
        public Guid IdCorreoNotificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public Guid IdTipoNotificacion { get; set; }
        public bool Estado { get; set; }
    }
}
