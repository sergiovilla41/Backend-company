using SimemNetAdmin.Domain.Common;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IOriginColumnsRepository
    {
        public Task<string> UpdateColumnasOrigen(ConfiguracionColumnasOrigenJson columnasOrigenJson);

        public Task<List<ConfiguracionColumnasOrigenJson>> GetColumnasOrigenJson(Guid idExtraccion);
    }
}
