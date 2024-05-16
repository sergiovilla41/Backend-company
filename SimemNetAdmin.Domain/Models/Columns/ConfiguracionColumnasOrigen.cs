using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Columnas
{
    [ExcludeFromCodeCoverage]
    [Table("ColumnasOrigen", Schema = "Configuracion")]
    public class ConfiguracionColumnasOrigen
    {
        [Key()]
        public Guid IdColumnaOrigen { get; set; }
        public Guid? IdConfiguracionGeneracionArchivos { get; set; } //Modificar
        public string? NombreColumnaOrigen { get; set; } = string.Empty;
        public int? NumeracionColumna { get; set; } = null;
        public Guid? IdColumnaDestino { get; set; } = null;
        public DateTime FechaCreacion { get; set; } = new DateTime();
        public string? FechaActualizacion { get; set; } = string.Empty;

        [NotMapped]
        public ConfiguracionColumnasDestino? ColumnasDestino { get; set; } = new ConfiguracionColumnasDestino();
    }
}
