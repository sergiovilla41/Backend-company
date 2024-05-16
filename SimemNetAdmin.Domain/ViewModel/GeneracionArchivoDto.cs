using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class GeneracionArchivoDto
    {
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string Tema { get; set; } = string.Empty;
        public string NombreArchivoDestino { get; set; } = string.Empty;
        public DateTime ValorDeltaInicial { get; set; }
        public DateTime? ValorDeltaFinal { get; set; }
        public string? Periodicidad { get; set; }
        public string? Titulo { get; set; }
        public string? EntidadOrigen { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? IdDataSet { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? UltimaFechaIndexado { get; set; }
        public DateTime? UltimaFechaActualizado { get; set; }
        public bool IndRegulatorio { get; set; }

        public string? MaximaFechaRegulatoria { get; set; }
    }
}
