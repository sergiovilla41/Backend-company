using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class ExtractionsDto
    {
        public Guid IdExtraccion { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string Proyecto { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string NombreExtraccion { get; set; } = string.Empty;
        public string? SecretoKeyVaultOrigenLakeXM { get; set; }
        public string? SecretoKeyVaultOrigenDBXM { get; set; }
        public string? Periodicidad { get; set; }
        public int? IntervaloPeriodicidad { get; set; }
        public string? FechaDeltaInicial { get; set; }
        public string? FechaDeltaFinal { get; set; }
        public string FechaCreacion { get; set; } = string.Empty;
        public string? FechaActualizacion { get; set; }
    }
}
