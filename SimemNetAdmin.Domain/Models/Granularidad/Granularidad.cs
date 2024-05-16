using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Granularidad
{
    [ExcludeFromCodeCoverage]
    [Table("Granularidad", Schema = "Configuracion")]
    public class Granularidad
    {
        [Key()]
        public Guid IdGranularidad { get; set; }
        public string NombreGranularidad { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
