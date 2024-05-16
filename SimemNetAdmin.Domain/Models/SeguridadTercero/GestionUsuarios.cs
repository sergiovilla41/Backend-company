using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.SeguridadTercero
{
    [ExcludeFromCodeCoverage]
    [Table("Usuario", Schema = "SeguridadTercero")]
    public class GestionUsuarios
    {
        [Key()]
        public Guid? IdUsuario { get; set; } = default(Guid?);
        public string? Nombre { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Observacion { get; set; }
        public string APP { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaIniUsuario { get; set; } = DateTime.UtcNow.AddHours(-5);
        public DateTime? FechaFinUsuario { get; set; }
        public string? Permisos { get; set; }
        public bool? IsAdmin { get; set; }
        [NotMapped]
        public string? Empresa { get; set; } = string.Empty;
    }
}
