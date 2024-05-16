using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Periodicidad
{
    [ExcludeFromCodeCoverage]
    [Table("Periodicidad", Schema = "Configuracion")]
    public class ConfiguracionPeriodicidad
    {
        [Key()]
        public Guid IdPeriodicidad {  get; set; }
        public string Periodicidad {  get; set; } = string.Empty;
        public Int16 OrdenPeriodicidad { get; set;}
        public DateTime FechaCreacion { get; set; }
    }
}
