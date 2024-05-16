using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.DuracionISO
{
    [ExcludeFromCodeCoverage]
    [Table("DuracionISO", Schema = "Configuracion")]
    public class ConfiguracionDuracionIso
    {
        [Key()]
        public Guid IdDuracionISO { get; set; }
        public string? CodigoISO8601 {  get; set; }
        public string? Descripcion {  get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}