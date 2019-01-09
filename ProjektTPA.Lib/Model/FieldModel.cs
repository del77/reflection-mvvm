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
        public bool IsReadOnly { get; set; }
        public FieldModel()
        {
        }
        public FieldModel(object[] attributes, string name, bool _private, bool _readonly, TypeModel typeModel)
        {
            Name = name;
            TypeModel = typeModel;
            Attributes = attributes.Select(x => TypeModel.GetType(x.GetType())).ToList();
            if (_private)
                Access = AccessLevel.Private;
            else if(typeModel.Modifiers != null)
                Access = typeModel.Modifiers.Item1;
            IsReadOnly = _readonly;
        }

        public override string ToString()
        {
            string ro = IsReadOnly ? "readonly " : "";
            return Access + " " + ro + TypeModel.Name + " " + Name;
        }
    }
}