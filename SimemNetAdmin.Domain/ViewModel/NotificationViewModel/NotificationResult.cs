using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.NotificationViewModel
{
    [ExcludeFromCodeCoverage]
    public class NotificationResult
    {
        public Guid IdLogsEjecuciones { get; set; }
        public string? NombreArchivoDestino { get; set; }
        public Guid IdExtraccion { get; set; }
        public string? FechaEjecucionInicio { get; set; }
        public string? FechaEjecucionFin { get; set; }
        public string? DeltaInicial { get; set; }
        public string? DeltaFinal { get; set; }
        public string? DescripcionError { get; set; }
        public string? ProximaEjecucionProgramada { get; set; }
    }
}
