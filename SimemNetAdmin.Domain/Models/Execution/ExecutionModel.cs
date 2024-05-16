using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Execution
{
    [ExcludeFromCodeCoverage]
    [Table("Ejecucion", Schema = "Configuracion")]
    public partial class ExecutionModel
    {
        [Key()]
        public Guid IdEjecucion { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public Int16? Dia { get; set; }
        public Int16? Mes { get; set; }
        public Int16 Hora { get; set; }
        public Int16? DiaSemana { get; set; }
        public bool IndDiaHabil { get; set; }
        public bool IndActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
