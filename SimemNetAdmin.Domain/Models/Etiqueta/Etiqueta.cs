using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SimemNetAdmin.Domain.Models.DataSet;

namespace SimemNetAdmin.Domain.Models.Etiqueta
{
    [ExcludeFromCodeCoverage]
    [Table("Etiqueta")]
    public class Etiqueta
    {
        [Key()]
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public List<FileGenerationModel>? GeneracionArchivos { get; set; }
    }
}
