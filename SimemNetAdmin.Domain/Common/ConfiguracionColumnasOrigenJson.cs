using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class ConfiguracionColumnasOrigenJson
    {
        public Guid IdColumnaOrigen { get; set; }
        public int? Numeracion { get; set; }
        public string ColumnaOrigen { get; set; } = string.Empty;
        public Guid? IdColumnaDestino { get; set; } = null;
        public Guid IdExtraccion { get; set; }
        public string? NombreColumnaDestino { get; set; } = null;
        public string? TipoDato { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public Guid? ExtraccionIdColumnaDestino { get; set; }
        public Guid? ExtraccionColumnaVersion { get; set; }
    }
}
