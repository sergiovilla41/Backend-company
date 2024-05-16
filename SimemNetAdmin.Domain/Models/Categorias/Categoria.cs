using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Categorias
{
    [ExcludeFromCodeCoverage]
    [Table("Categoria", Schema = "dato")]
    public class Categoria
    {
        [Key()]
        public Guid Id { get; set; }
        public Guid? IdCategoria { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? OrdenCategoria { get; set; }
        public bool Privado { get; set; }
        public int? CantidadConjuntoDato { get; set; }
        public int CantidadDescarga { get; set; }
        [NotMapped]
        public List<dynamic>? Children { get; set; }
    }
}
