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
    [Table("LogEjecucionEnvioNotificacion", Schema = "dato")]
    public class LogSendNotificationModel
    {
        [Key()]
        public Int32 Id { get; set; }
        public Guid IdTipoNotificacion { get; set; }
        public Guid IdParametroNotificacion { get; set; }
        public string IdCorreoNotificacion { get; set; } = string.Empty;
        public string? DescripcionError { get; set; }
        public string? MetodoError { get; set; }
        public string? EstadoEnvio { get; set; }
        public int NumeroRegistros { get; set; }
        public DateTime FechaEjecucion { get; set; }
    }
}
