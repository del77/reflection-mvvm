using System.Collections.Generic;
using System.Linq;
using DtoLayer.Enums;

namespace BusinessLogic.Model
{
    public class FieldModel
    {
        public string Name { get; set; }
        public TypeModel TypeModel { get; set; }
        public List<TypeModel> Attributes { get; set; }
        public AccessLevel Access { get; set; }

        public FieldModel()
        {
        }
        public FieldModel(object[] attributes, string name, TypeModel typeModel)
        {
            Name = name;
            TypeModel = typeModel;
            Attributes = attributes.Select(x => TypeModel.GetType(x.GetType())).ToList();
            if(typeModel.Modifiers != null)
            Access = typeModel.Modifiers.Item1;
        }

        public override string ToString()
        {
            return Access + " " + TypeModel.Name + " " + Name;
        }
    }
}