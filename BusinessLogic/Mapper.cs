using AutoMapper;
using BusinessLogic.Model;
using DtoLayer;

namespace BusinessLogic
{
    public class Mapper
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AssemblyModel, AssemblyDto>().ReverseMap();
                cfg.CreateMap<NamespaceModel, NamespaceDto>().ReverseMap();
                cfg.CreateMap<FieldModel, FieldDto>().ReverseMap();
                cfg.CreateMap<MethodModel, MethodDto>().ReverseMap();
                cfg.CreateMap<PropertyModel, PropertyDto>().ReverseMap();
                cfg.CreateMap<TypeModel, TypeDto>().ReverseMap();
            });
            return configuration.CreateMapper();
        }
    }
}