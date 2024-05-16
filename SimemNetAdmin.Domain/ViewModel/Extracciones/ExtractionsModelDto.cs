using System;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel.Extracciones
{
    [ExcludeFromCodeCoverage]
    public class ExtractionsModelDto
    {
        public string? IdExtraccion { get; set; }
        public string IdConfiguracionGeneracionArchivos { get; set; } = string.Empty;
        public string? Proyecto { get; set; }
        public string Tema { get; set; } = string.Empty;
        public string? NombreExtraccion { get; set; } = string.Empty;
        public string? Periodicidad { get; set; }
        public int? IntervaloPeriodicidad { get; set; }
        public DateTime? FechaDeltaInicial { get; set; }
        public DateTime? FechaDeltaFinal { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
