using SimemNetAdmin.Domain.Models.DataSet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Etiqueta
{
    [ExcludeFromCodeCoverage]
    [Table("GeneracionArchivoEtiqueta", Schema = "Configuracion")]
    public class GeneracionArchivoEtiqueta
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivoxEtiqueta { get; set; }
        [ForeignKey("IdConfiguracionGeneracionArchivo")]
        public FileGenerationModel? GeneracionArchivos { get; set; }
        [ForeignKey("EtiquetaId")]
        public Etiqueta? Etiqueta { get; set; } = null;
        public Guid IdConfiguracionGeneracionArchivo { get; set; }
        public Guid EtiquetaId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
