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
    [Table("TipoNotificacion", Schema = "dato")]
    public partial class NotificacionTypeModel
    {
        [Key()]
        public Guid IdTipoNotificacion { get; set; }
        public string? TipoNotificacion { get; set; }
        public bool Activo { get; set; }
    }
}
