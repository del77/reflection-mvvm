using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.Model
{
    [DataContract(IsReference = true)]
    public class FieldModel : Model
    {
        [DataMember]
        public TypeModel TypeModel { get; set; }

        [DataMember]
        public List<TypeModel> Attributes { get; set; }
        [DataMember]
        public AccessLevel Access { get; set; }

        public FieldModel() : base("name")
        {
            
        }
        public FieldModel(object[] attributes, string name, TypeModel typeModel) : base(name)
        {
            TypeModel = typeModel;
            Attributes = attributes.Select(x => TypeModel.GetType(x.GetType())).ToList();
            if(typeModel.Modifiers != null)
            Access = typeModel.Modifiers.Item1;
        }
    }
}