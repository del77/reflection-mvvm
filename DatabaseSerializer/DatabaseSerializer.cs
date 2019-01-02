using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DatabaseSerializer.Model;
using DtoLayer;
using MEF;

namespace DatabaseSerializer
{
    [Export(typeof(ISerializer))]
    public class DatabaseSerializer : ISerializer
    {
        private DatabaseSerializerContext context;

        public DatabaseSerializer()
        {
            context = new DatabaseSerializerContext();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AssemblyDto, AssemblyDb>().ForMember(dest => dest.AssemblyDbId, opt => opt.Ignore());
                cfg.CreateMap<FieldDto, FieldDb>().ForMember(dest => dest.FieldDbId, opt => opt.Ignore());
                cfg.CreateMap<MethodDto, MethodDb>().ForMember(dest => dest.MethodDbId, opt => opt.Ignore());
                cfg.CreateMap<NamespaceDto, NamespaceDb>().ForMember(dest => dest.NamespaceDbId, opt => opt.Ignore());
                cfg.CreateMap<PropertyDto, PropertyDb>().ForMember(dest => dest.PropertyDbId, opt => opt.Ignore());
                cfg.CreateMap<TypeDto, TypeDb>().ForMember(dest => dest.TypeDbId, opt => opt.Ignore());
            });
        }

        public void Serialize(object model)
        {
            context.Assemblies.RemoveRange(context.Assemblies);
            context.Fields.RemoveRange(context.Fields);
            context.Methods.RemoveRange(context.Methods);
            context.Namespaces.RemoveRange(context.Namespaces);
            context.Properties.RemoveRange(context.Properties);
            context.Types.RemoveRange(context.Types);

            AssemblyDto assemblyDto = (AssemblyDto)model;
            AssemblyDb assemblyDb = Mapper.Map<AssemblyDb>(assemblyDto);

            context.Assemblies.Add(assemblyDb);
            context.SaveChanges();
        }

        public object Deserialize()
        {
            context.Namespaces
                .Include(x => x.Namespaces)
                .Include(x => x.Types)
                .Load();

            context.Assemblies
                .Include(x => x.Namespaces)
                .Load();

            context.Fields
                .Include(x => x.Attributes)
                .Include(x => x.TypeModel)
                .Load();

            context.Methods
                .Include(x => x.Attributes)
                .Include(x => x.GenericArguments)
                .Include(x => x.Parameters)
                .Include(x => x.ReturnType)
                .Load();

            context.Properties
                .Include(x => x.Attributes)
                .Include(x => x.Getter)
                .Include(x => x.Setter)
                .Include(x => x.Type)
                .Load();

            context.Types
                .Include(x => x.Attributes)
                .Include(x => x.BaseType)
                .Include(x => x.Constructors)
                .Include(x => x.DeclaringType)
                .Include(x => x.Fields)
                .Include(x => x.GenericArguments)
                .Include(x => x.ImplementedInterfaces)
                .Include(x => x.Methods)
                .Include(x => x.FieldType)
                .Include(x => x.NestedTypes)
                .Load();

            AssemblyDb ret = context.Assemblies.FirstOrDefault();

            return Mapper.Map<AssemblyDto>(ret);
        }
    }
}