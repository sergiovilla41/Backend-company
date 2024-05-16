using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Infra.Data.Repository.ClasificacionRegulatoria
{
    public class RegulatoryClassifyRepository : IRegulatoryClassifyRepositoryInterface
    {
        #region Variables
        private readonly SimemNetAdminDbContext _baseContext;
        #endregion

        #region Constructor
        public RegulatoryClassifyRepository()
        {
            _baseContext ??= new SimemNetAdminDbContext();
        }
        #endregion

        public List<ConfiguracionClasificacionRegulatoriaDto> GetRegulatoryClassifyList()
        {
            var ClassifyList = _baseContext.ClasificacionRegulatoriaModel.ToList();
            var dto = DataProfile.Mapper.Map<List<ConfiguracionClasificacionRegulatoriaDto>>(ClassifyList);
            return dto;
        }

        public Guid CreateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto EntityDto)
        {
            var MappedObject = DataProfile.Mapper.Map<ClasificacionRegulatoriaModel>(EntityDto);
            _baseContext.ClasificacionRegulatoriaModel.Add(MappedObject);
            _baseContext.SaveChanges();
            return MappedObject.IdConfiguracionClasificacionRegulatoria;
        }


        public void UpdateRegulatoryClassify(ConfiguracionClasificacionRegulatoriaDto dto)
        {
            var configuracion = _baseContext.ClasificacionRegulatoriaModel.FirstOrDefault(x => x.IdConfiguracionClasificacionRegulatoria == dto.IdConfiguracionClasificacionRegulatoria);

            if (configuracion != null)
            {
                configuracion.CodigoDelta = dto.CodigoDelta;
                configuracion.Descripcion = dto.Descripcion;
                configuracion.FechaCreacion = dto.FechaCreacion;
                configuracion.DeltaInicialDiaMes = dto.DeltaInicialDiaMes;
                configuracion.DeltaInicialDias = dto.DeltaInicialDias;
                configuracion.DeltaInicialDiaSemana = dto.DeltaInicialDiaSemana;
                configuracion.DeltaInicialSemanas = dto.DeltaInicialSemanas;
                configuracion.DeltaInicialMes = dto.DeltaInicialMes;
                configuracion.DeltaInicialMeses = dto.DeltaInicialMeses;
                configuracion.DeltaInicialAno = dto.DeltaInicialAno;
                configuracion.DeltaInicialPeriodo = dto.DeltaInicialPeriodo;
                configuracion.DeltaFinalDiaMes = dto.DeltaFinalDiaMes;
                configuracion.DeltaFinalDias = dto.DeltaFinalDias;
                configuracion.DeltaFinalDiaSemana = dto.DeltaFinalDiaSemana;
                configuracion.DeltaFinalSemanas = dto.DeltaFinalSemanas;
                configuracion.DeltaFinalMes = dto.DeltaFinalMes;
                configuracion.DeltaFinalMeses = dto.DeltaFinalMeses;
                configuracion.DeltaFinalAno = dto.DeltaFinalAno;
                configuracion.DeltaFinalPeriodo = dto.DeltaFinalPeriodo;

                _baseContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("No se encontró el id " + dto.IdConfiguracionClasificacionRegulatoria);
            }
        }

        public void DeleteRegulatoryClassifyForTest(Guid IdConfiguracionClasificacionRegulatoria)
        {
            var obj = _baseContext.ClasificacionRegulatoriaModel.FirstOrDefault(x => x.IdConfiguracionClasificacionRegulatoria == IdConfiguracionClasificacionRegulatoria);
            if (obj != null)
            {
                _baseContext.ClasificacionRegulatoriaModel.Remove(obj);
                _baseContext.SaveChanges();
            }
        }
    }
}
