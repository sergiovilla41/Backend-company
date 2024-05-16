using SimemNetAdmin.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Interfaces
{
    public interface IRegulatoryClassifyRepositoryInterface
    {
        List<ConfiguracionClasificacionRegulatoriaDto> GetRegulatoryClassifyList();
        void UpdateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto dto);
        Guid CreateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto EntityDto);
        void DeleteRegulatoryClassifyForTest(Guid IdConfiguracionClasificacionRegulatoria);
    }
}
