using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.GeneracionArchivos
{
    [ExcludeFromCodeCoverage]
    public class GeneracionArchivoJson
    {
        public string? IdDataSet { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? Titulo { get; set; }
        public string Tema { get; set; } = string.Empty;
        public string NombreArchivoDestino { get; set; } = string.Empty;
        public Guid? IdGranularidad { get; set; }
        public string? NombreGranularidad { get; set; }
        public Guid? IdPeriodicidad { get; set; }
        public string? Periodicidad { get; set; }
        public string? NBSynapse { get; set; }
        public DateTime ValorDeltaInicial { get; set; }
        public DateTime? ValorDeltaFinal { get; set; }
        public Guid? IdGeneracionArchivoMaestra { get; set; }
        public string? GeneracionArchivoMaestra { get; set; }
        public bool Estado { get; set; }
        public string? Fail { get; set; }
        public string? PipelineRunId {  get; set; }
    }
}