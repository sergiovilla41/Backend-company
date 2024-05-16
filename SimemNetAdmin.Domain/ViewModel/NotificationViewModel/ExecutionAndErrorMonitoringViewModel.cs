using SimemNetAdmin.Domain.Models.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.NotificationViewModel
{
    [ExcludeFromCodeCoverage]
    public class ExecutionAndErrorMonitoringViewModel
    {
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? NombreConjuntoDeDatos { get; set; }
        public string? NombreArchivoDestino { get; set; }
        public string? Estado { get; set; }
        public string FechaInicioEjecucion { get; set; }
        public string FechaFinEjecucion { get; set; }
        public bool? EsRegulatorio { get; set; }
        public string? FechaProximaEjecucion { get; set; }
        public string? LanzadoPor { get; set; }
        public Guid IdEjecucion { get; set; }
        public string? PipelineRunId { get; set; }
        public string? FechaDeltaInicial { get; set; }
        public string? FechaDeltaFinal { get; set; }
        public string ValorDeltaInicial { get; set; }
        public string? ValorDeltaFinal { get; set; }
        public int? CuandoEjecutaDia { get; set; }
        public int? CuandoEjecutaMes { get; set; }
        public int CuandoEjecutaHora { get; set; }
        public int? CuandoEjecutaDiaSemana { get; set; }
        public bool IndDiaHabil { get; set; }
        public bool IndActivo { get; set; }
        public string? ClasificacionDeltas { get; set; }
        public string? Observaciones { get; set; }
        public List<ExtractionsDto> Extracciones { get; set; }
        public List<ExecutionModel> Ejecuciones { get; set; }

        public ExecutionAndErrorMonitoringViewModel()
        {
            FechaInicioEjecucion = string.Empty;
            FechaFinEjecucion = string.Empty;
            ValorDeltaInicial = string.Empty;
            Extracciones = new();
            Ejecuciones = new();
        }
    }
}
