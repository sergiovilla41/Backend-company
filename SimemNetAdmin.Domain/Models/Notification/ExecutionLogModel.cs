using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Notification
{

    [ExcludeFromCodeCoverage]
    [Table("LogEjecucion", Schema = "dato")]
    public partial class ExecutionLogModel
    {
        [Key()]
        public Guid IdLogsEjecuciones { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? Estado { get; set; }
        public DateTime ValorDeltaInicial { get; set; }
        public DateTime? ValorDeltaFinal { get; set; }
        public Guid? IdDiccionarioError { get; set; }
        public DateTime FechaInicioEjecucion { get; set; }
        public DateTime? FechaFinEjecucion { get; set; }
        public DateTime? FechaProximaEjecucionProgramada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool NotificacionEnviada { get; set; }
        public string? PipelineRunId { get; set; }
        public long? DuracionEjecucionMinutos { get; set; }
        public string? ErrorDescripcion {  get; set; }
        public string? IdUsuario {  get; set; }
    }
}
