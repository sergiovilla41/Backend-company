using SimemNetAdmin.Domain.Models.DuracionISO;
using SimemNetAdmin.Domain.Models.Periodicidad;
using SimemNetAdmin.Domain.Models.Granularidad;
using System.Diagnostics.CodeAnalysis;
using SimemNetAdmin.Domain.Models.Etiqueta;

namespace SimemNetAdmin.Domain.Models.GeneracionArchivos
{
    [ExcludeFromCodeCoverage]
    public class DatosBasicosJson
    {
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string Tema { get; set; } = string.Empty;
        public string NombreArchivoDestino { get; set; } = string.Empty;
        public bool DatoObligatorio { get; set; }
        public bool IndRegulatorio { get; set; }
        public string? SelectXM { get; set; }
        public string? NBSynapse { get; set; }
        public Guid? IdDuracionISO { get; set; }
        public string ValorDeltaInicial { get; set; } = string.Empty;
        public string? ValorDeltaFinal { get; set; }
        public string? UltimaFechaIndexado { get; set; }
        public string? UltimaFechaActualizado { get; set; }
        public Guid? IdPeriodicidad { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public bool Privacidad { get; set; }
        public Guid? IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public Guid? IdTipoVista { get; set; }
        public Guid? IdGranularidad { get; set; }
        public string? EntidadOrigen { get; set; }
        public bool Estado { get; set; }
        public Guid? IdConfiguracionClasificacionRegulatoria { get; set; }
        public List<string>? Etiquetas { get; set; }
    }
}
