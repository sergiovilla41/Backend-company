using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Notification
{
    [ExcludeFromCodeCoverage]
    public partial class NotificacionDataSetRegulatorioModel
    {
        public string IdConfiguracionGeneracionArchivos { get; set; } = string.Empty;
        public string? NombreArchivoDestino { get; set; }
        public string? MaximaFechaRegulatoria { get; set; } = string.Empty;
        public DateTime? FechaProximaEjecucionProgramada { get; set; }
        public string DeltaInicial { get; set; } = string.Empty;
        public string DeltaFinal { get; set; } = string.Empty;
        public int DiasFaltantes { get; set; } = 0;
        public string Estado { get; set; } = string.Empty;
        public string? Tema { get; set; }
    }
}
