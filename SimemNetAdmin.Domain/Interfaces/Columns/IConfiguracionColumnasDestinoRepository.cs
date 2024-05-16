using SimemNetAdmin.Domain.Models.Columnas;

using SimemNetAdmin.Domain.ViewModel.Colums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Interfaces.Columns
{
    public interface IConfiguracionColumnasDestinoRepository
    {
        Task<List<ConfiguracionColumnasDestinoDTO>> ListColumnaDestino();
        Task UpdateColumnaDestinoAsync(ConfiguracionColumnasDestinoDTO columnaDestinoDTO);
        Task<Guid> CreateColumnaDestino(ConfiguracionColumnasDestinoDTO columnaDestinoDTO);

    }
}
