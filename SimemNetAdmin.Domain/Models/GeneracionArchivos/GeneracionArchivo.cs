using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [Table("GeneracionArchivos", Schema = "Configuracion")]
    public class GeneracionArchivo
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string Tema { get; set; } = string.Empty;
        public string NombreArchivoDestino { get; set; } = string.Empty;
        public Guid? IdGeneracionArchivoMaestra {  get; set; }
        public string? SelectXM { get; set; }
        public Guid? IdDuracionISO { get; set; }
        public string? NBSynapse { get; set; }
        public DateTime ValorDeltaInicial { get; set; }
        public DateTime? ValorDeltaFinal { get; set; }
        public string? Periodicidad { get; set; }
        public Int16? OrderPeriodicidad { get; set; }
        public Guid? IdPeriodicidad { get; set; }
        public int? IntervaloPeriodicidad { get; set; }
        public string? Titulo { get; set; }
        public Guid? IdCategoria { get; set; }
        public Guid? IdGranularidad { get; set; }
        public Guid? IdTipoVista { get; set; }
        public bool Privacidad { get; set; }
        public string? EntidadOrigen { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioInsercion { get; set; }
        public DateTime? FechaInsercion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ExtencionArchivo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? IdDataSet { get; set; }
        public int? ContadorVistas { get; set; }
        public int? ContadorDescargas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? UltimaFechaIndexado { get; set; }
        public Guid? IdCategoriaNivel1 { get; set; }
        public Guid? IdColumnaDestino { get; set; }
        public DateTime? UltimaFechaActualizado { get; set; }
        public string URLDataHistorica { get; set; } = string.Empty;
        public DateTime? FechaFin { get; set; }
        public Guid? IdColumnaVersion { get; set; }
        public DateTime? FechaDescarga { get; set; }
        public bool IndRegulatorio { get; set; }
        public Guid? IdConfiguracionClasificacionRegulatoria { get; set; }
    }
}
