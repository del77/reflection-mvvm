using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.Model
{
    public class FieldModel : Model
    {
        public TypeModel TypeModel { get; set; }
        public IEnumerable<TypeModel> Attributes { get; set; }
        public AccessLevel Access { get; set; }
        public FieldModel(object[] attributes, string name, TypeModel typeModel) : base(name)
        {
            TypeModel = typeModel;
            Attributes = attributes.Select(x => TypeModel.GetType(x.GetType()));
            Access = typeModel.Modifiers.Item1;
        }
    }
}