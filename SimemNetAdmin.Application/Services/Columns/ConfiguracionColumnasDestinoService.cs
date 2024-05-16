using SimemNetAdmin.Application.Interfaces.Columns;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Interfaces.Columns;
using SimemNetAdmin.Domain.Models.Columnas;

using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Domain.ViewModel.Labels;
using SimemNetAdmin.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Services.Columns
{
    public class ConfiguracionColumnasDestinoService: IConfiguracionColumnasDestinoService
    {
        private readonly IConfiguracionColumnasDestinoRepository _configuracionColumnasDestinoRepository;
       public ConfiguracionColumnasDestinoService(IConfiguracionColumnasDestinoRepository configuracionColumnasDestinoRepository)
        {
            _configuracionColumnasDestinoRepository = configuracionColumnasDestinoRepository ?? throw new ArgumentNullException(nameof(configuracionColumnasDestinoRepository));
        }
        public async Task<List<ConfiguracionColumnasDestinoDTO>> ListColumnaDestino()
        {
            return await _configuracionColumnasDestinoRepository.ListColumnaDestino();
        }
       
        public async Task UpdateColumnaDestinoAsync(ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
              await _configuracionColumnasDestinoRepository.UpdateColumnaDestinoAsync(columnaDestinoDTO);
        }
        public async Task<Guid> CreateColumnaDestino(ConfiguracionColumnasDestinoDTO columnaDestinoDTO)
        {
            return await _configuracionColumnasDestinoRepository.CreateColumnaDestino(columnaDestinoDTO);
        }
       

    }
}
