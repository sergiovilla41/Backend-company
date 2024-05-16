using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Columnas
{
    [ExcludeFromCodeCoverage]
    [Table("ColumnasDestino", Schema = "Configuracion")]
    public class ConfiguracionColumnasDestino
    {
        [Key()]
        public Guid IdColumnaDestino { get; set; }
        public string? NombreColumnaDestino { get; set; }
        public string? TipoDato { get; set; }
        public string? AtributoVariable { get; set; }
        public Guid? VariableId { get; set; }
        public bool? Estado { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
