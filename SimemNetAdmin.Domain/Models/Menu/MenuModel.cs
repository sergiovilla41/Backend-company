using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Menu
{
    [ExcludeFromCodeCoverage]
    public class MenuModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public Guid? IdPadre { get; set; }
        public int Secuencia { get; set; }
        public string Enlace { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public bool Activo { get; set; } = true;
    }
}
