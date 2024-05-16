using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Repository.ClasificacionRegulatoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Services
{
    public class RegulatoryClassifyService : IRegulatoryClassifyServiceInterface
    {
        private readonly RegulatoryClassifyRepository _regulatoryClassifyRepository;

        public RegulatoryClassifyService()
        {
            _regulatoryClassifyRepository = new RegulatoryClassifyRepository();
        }

        public List<ConfiguracionClasificacionRegulatoriaDto> GetRegulatoryClassifyList()
        {
            return _regulatoryClassifyRepository.GetRegulatoryClassifyList();
        }

        public void UpdateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto dto)
        {
            _regulatoryClassifyRepository.UpdateRegulatoryClassify(dto);
        }

        public Guid CreateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto EntityDto)
        {
            return _regulatoryClassifyRepository.CreateRegulatoryClassify(EntityDto);
        }

        public void DeleteRegulatoryClassifyForTest(Guid IdConfiguracionClasificacionRegulatoria)
        {
            _regulatoryClassifyRepository.DeleteRegulatoryClassifyForTest(IdConfiguracionClasificacionRegulatoria);
        }
    }
}
