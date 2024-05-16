
using SimemNetAdmin.Domain.ViewModel.Colums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Interfaces.Columns
{
    public interface IConfiguracionColumnasDestinoService
    {
        Task<List<ConfiguracionColumnasDestinoDTO>> ListColumnaDestino();
        Task UpdateColumnaDestinoAsync(ConfiguracionColumnasDestinoDTO columnaDestinoDTO);
        Task<Guid> CreateColumnaDestino(ConfiguracionColumnasDestinoDTO columnaDestinoDTO);
        
    }
}
