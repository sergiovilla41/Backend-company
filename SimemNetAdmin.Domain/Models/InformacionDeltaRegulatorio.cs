using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [Table("InformacionDeltaRegulatorio", Schema = "dato")]
    public class InformacionDeltaRegulatorio
    {
        [Key()]
        public Guid IdInformacionDeltaRegulatorio { get; set; }
        public string? MaximaFechaRegulatoria { get; set; }
        public string? DeltaInicial { get; set; }
        public string? DeltaFinal { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
    }
}
