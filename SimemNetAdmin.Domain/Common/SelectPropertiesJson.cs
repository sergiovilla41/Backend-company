using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SimemNetAdmin.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class SelectPropertiesJson
    {
        [JsonPropertyName("idDestinationColumn")]
        public Guid IdColumnasDestino { get; set; }
        [JsonPropertyName("idRegulatoryClassification")]
        public int IdConfiguracionclasificacionregulatoria { get; set; }
        public int? Id { get; set; }
        public string? Value { get; set; }
        public string? TipoDato { get; set; }
        public string? descripcion { get; set; }
    }
}
