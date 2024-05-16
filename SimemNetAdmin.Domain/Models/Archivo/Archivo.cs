using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.Archivo
{
    [Table("Archivo", Schema = "dato")]
    [ExcludeFromCodeCoverage]
    public class Archivo
    {
        [Key()]
        public string? IdArchivo { get; set; }

        [Required(ErrorMessage = "El campo tipoContenido es requerido", AllowEmptyStrings = false)]
        [StringLength(150, ErrorMessage = "El campo TipoContenido no puede tener mas de {1} caracteres")]
        public string TipoContenido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo rutaContenedora es requerido", AllowEmptyStrings = false)]
        [StringLength(500, ErrorMessage = "El campo rutaContenedora no puede tener mas de {1} caracteres")]
        public string RutaContenedora { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo rutaAbsolutaDirectorioPadre es requerido", AllowEmptyStrings = false)]
        [StringLength(500, ErrorMessage = "El campo rutaAbsolutaDirectorioPadre no puede tener mas de {1} caracteres")]
        public string RutaAbsolutaDirectorioPadre { get; set; } = string.Empty;

        public string? NombreArchivo { get; set; }

        [Required(ErrorMessage = "El campo nombreContenedor es requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El campo nombreContenedor no puede tener mas de {1} caracteres")]
        public string NombreContenedor { get; set; } = string.Empty;

        [Required]
        public long Tamanio { get; set; }

        public string? ModificadoPor { get; set; } = string.Empty;

        [Required]
        public string CreadoPor { get; set; } = string.Empty;

        [ExcludeFromCodeCoverage]
        public DateTime? FechaModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public string? Ruta { get; set; }
        public DateTime? RutaFechaExpiracion { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdConfiguracionGeneracionArchivo { get; set; }

        [Required]
        public DateTime FechaIndexado { get; set; }

        [ExcludeFromCodeCoverage]
        public int FilasTotales { get; set; }
    }
}
