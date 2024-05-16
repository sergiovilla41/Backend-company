using SimemNetAdmin.Domain.Models.SeguridadTercero;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [Table("EmpresaDominio", Schema = "SeguridadTercero")]
    public class EmpresaDominio
    {
        [Key()]
        public Guid IdEmpresaDominio { get; set; }
        [ForeignKey("Empresa")]
        public Guid IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa {  get; set; } = new Empresa();
        public Guid IdDominio { get; set; }
        [ForeignKey("IdDominio")]
        public virtual SeguridadDominio SeguridadDominio { get; set; } = new SeguridadDominio();
        public DateTime FechaRegistro { get; set; }
    }
}
