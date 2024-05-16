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
    [Table("ParametroNotificacion", Schema = "dato")]
    public partial class ParameterNotificationModel
    {
        [Key()]
        public Guid IdParametroNotificacion { get; set; }
        public Byte HoraEjecucion { get; set; }
        public Byte MinutoEjecucion { get; set; }
        public Guid IdTipoNotificacion { get; set; }
        public string IdPlantilla { get; set; } = string.Empty;
        public bool Estado { get; set; }

    }
}
