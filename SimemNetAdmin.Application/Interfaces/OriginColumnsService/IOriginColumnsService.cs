using SimemNetAdmin.Domain.Common;

namespace SimemNetAdmin.Application.Interfaces.ColumnasOrigenService
{
    public interface IOriginColumnsService
    {
        public Task<string> UpdateColumnasOrigen(ConfiguracionColumnasOrigenJson columnasOrigenJson);

        public Task<List<ConfiguracionColumnasOrigenJson>> GetColumnasOrigenJson(Guid idExtraccion);
    }
}
