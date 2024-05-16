using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Extraction
{
    [ExcludeFromCodeCoverage]
    [Table("Extracciones", Schema = "Configuracion")]
    public partial class ExtractionsModel
    {
        [Key()]
        public Guid IdExtraccion { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string Proyecto { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string NombreExtraccion { get; set; } = string.Empty;
        public string? SecretoKeyVaultOrigenLakeXM { get; set; }
        public string? SecretoKeyVaultOrigenDBXM { get; set; }
        public string? Periodicidad { get; set; }
        public int? IntervaloPeriodicidad { get; set; }
        public DateTime? FechaDeltaInicial { get; set; }
        public DateTime? FechaDeltaFinal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
