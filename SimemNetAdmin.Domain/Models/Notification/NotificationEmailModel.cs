using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Models.Notification
{
    [ExcludeFromCodeCoverage]
    [Table("CorreoNotificacion", Schema = "dato")]
    public partial class NotificationEmailModel
    {
        [Key()]
        public Guid IdCorreoNotificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public Guid? IdTipoNotificacion { get; set; }
        public bool Estado { get; set; }
    }
}
