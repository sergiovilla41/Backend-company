using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Menu
{
    [ExcludeFromCodeCoverage]
    public partial class MenuJsonModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public Guid IdPadre { get; set; }
        public int Nivel { get; set; }
        public int Secuencia { get; set; }
        public string Enlace { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public bool Activo { get; set; } = true;
        [NotMapped]
        public List<dynamic>? Children { get; set; }
    }
}
