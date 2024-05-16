using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Publication
{
    [ExcludeFromCodeCoverage]
    [Table("PublicacionRegulatoria", Schema = "Configuracion")]
    public partial class PublicationModel
    {
        [Key()]
        public Guid IdPublicacionRegulatoria { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public Int16? Dia { get; set; }
        public Int16? Mes { get; set; }
        public Int16? DiaSemana { get; set; }
        public bool? IndDiaHabil { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
