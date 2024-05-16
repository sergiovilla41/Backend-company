using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel.NotificationViewModel
{
    [ExcludeFromCodeCoverage]
    public class NotificacionDataSetRegulatorioViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string MaximaFechaRegulatoria { get; set; } = string.Empty;
        public string FechaProximaEjecucion { get; set; } = string.Empty;
        public string DeltaInicialEjecutar { get; set; } = string.Empty;
        public string DeltaFinalEjecutar { get; set; } = string.Empty;
        public int? DiasHabilesFaltantes { get; set; } = 0;
        public string Estado { get; set; } = string.Empty;
        public string Tema {  get; set; } = string.Empty;
    }
}