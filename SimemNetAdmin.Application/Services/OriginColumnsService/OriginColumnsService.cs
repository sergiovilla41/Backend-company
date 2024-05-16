using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Domain.Common;
using SimemNetAdmin.Domain.Interfaces;

namespace SimemNetAdmin.Application.Services.ColumnasOrigenService
{
    public class OriginColumnsService : IOriginColumnsService
    {
        private readonly IOriginColumnsRepository _columnasOrigenRepository;

        public OriginColumnsService(IOriginColumnsRepository columnasOrigenRepository)
        {
            _columnasOrigenRepository = columnasOrigenRepository ?? throw new ArgumentNullException(nameof(columnasOrigenRepository));
        }

        public async Task<string> UpdateColumnasOrigen(ConfiguracionColumnasOrigenJson columnasOrigenJson)
        {
            return await _columnasOrigenRepository.UpdateColumnasOrigen(columnasOrigenJson)!;
        }

        public async Task<List<ConfiguracionColumnasOrigenJson>> GetColumnasOrigenJson(Guid idExtraccion)
        {
            return await _columnasOrigenRepository.GetColumnasOrigenJson(idExtraccion)!;
        }
    }
}
