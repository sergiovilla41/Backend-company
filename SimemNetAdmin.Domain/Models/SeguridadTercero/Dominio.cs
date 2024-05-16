using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.SeguridadTercero
{
    [ExcludeFromCodeCoverage]
    [Table("Dominio", Schema = "SeguridadTercero")]
    public class SeguridadDominio
    {
        [Key]
        [ForeignKey("IdDominio")]
        public Guid IdDominio { get; set; }
        public string Dominio { get; set; } = string.Empty;
    }
}
