using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [Table("ClasificacionRegulatoria", Schema = "Configuracion")]
    public partial class ClasificacionRegulatoriaModel
    {
        [Key()]
        public Guid IdConfiguracionClasificacionRegulatoria { get; set; }
        public string? CodigoDelta { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? DeltaInicialDiaMes { get; set; }
        public int? DeltaInicialDias { get; set; }
        public int? DeltaInicialDiaSemana { get; set; }
        public int? DeltaInicialSemanas { get; set; }
        public int? DeltaInicialMes { get; set; }
        public int? DeltaInicialMeses { get; set; }
        public int? DeltaInicialAno { get; set; }
        public string? DeltaInicialPeriodo { get; set; }
        public int? DeltaFinalDiaMes { get; set; }
        public int? DeltaFinalDias { get; set; }
        public int? DeltaFinalDiaSemana { get; set; }
        public int? DeltaFinalSemanas { get; set; }
        public int? DeltaFinalMes { get; set; }
        public int? DeltaFinalMeses { get; set; }
        public int? DeltaFinalAno { get; set; }
        public string? DeltaFinalPeriodo { get; set; }
    }
}
