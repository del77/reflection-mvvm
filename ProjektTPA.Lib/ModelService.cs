using System.ComponentModel.Composition;
using AutoMapper;
using BusinessLogic.Model;
using DtoLayer;
using MEF;

namespace BusinessLogic
{
    [Export(typeof(ModelService))]
    public class ModelService
    {
        private IMapper _mapper;

        [Import(typeof(ISerializer))]
        private ISerializer serializer;

        public ModelService()
        {
            _mapper = Mapper.Initialize();
        }

        public void Save(AssemblyModel model)
        {
            var assemblyDto = _mapper.Map<AssemblyDto>(model);
            serializer.Serialize(assemblyDto);
        }

        public AssemblyModel Load()
        {
            AssemblyDto loaded = (AssemblyDto)serializer.Deserialize();
            AssemblyModel assemblyModel = _mapper.Map<AssemblyModel>(loaded);
            return assemblyModel;
        }
    }
}