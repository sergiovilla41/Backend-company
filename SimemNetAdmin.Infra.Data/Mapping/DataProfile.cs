using AutoMapper;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.Models.Etiqueta;
using SimemNetAdmin.Domain.ViewModel;
using SimemNetAdmin.Domain.ViewModel.Colums;
using SimemNetAdmin.Domain.ViewModel.Labels;
using System.Diagnostics.CodeAnalysis;


namespace SimemNetAdmin.Infra.Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public static class DataProfile
    {
        public static IMapper Mapper => LazyMapeo.Value;
        private static readonly Lazy<IMapper> LazyMapeo = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
    }

    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClasificacionRegulatoriaModel, ConfiguracionClasificacionRegulatoriaDto>().ReverseMap();
            CreateMap<Etiqueta, LabelsDto>().ReverseMap();
            CreateMap<ConfiguracionColumnasDestino, ConfiguracionColumnasDestinoDTO>().ReverseMap();
            CreateMap<ConfiguracionColumnasDestinoDTO, ConfiguracionColumnasDestino>();
        }
    }
}
