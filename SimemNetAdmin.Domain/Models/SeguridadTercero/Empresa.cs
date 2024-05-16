using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.SeguridadTercero
{
    [ExcludeFromCodeCoverage]
    [Table("Empresa", Schema = "SeguridadTercero")]
    public class Empresa
    {
        [Key]
        public Guid IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;

    }
}
