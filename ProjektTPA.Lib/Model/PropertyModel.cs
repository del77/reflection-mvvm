using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Extensions;
using DtoLayer.Enums;

namespace BusinessLogic.Model
{
    public class PropertyModel
    {
        //public TypeModel TypeModel { get; set; }

        public PropertyModel(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<TypeModel> Attributes { get; set; }
        public AccessLevel Access { get; set; }
        public MethodModel Getter { get; set; }
        public MethodModel Setter { get; set; }
        public TypeModel Type { get; set; }


        private PropertyModel(PropertyInfo prop, TypeModel typeModel) : this(prop.Name)
        {
            //TypeModel = typeModel;
            Getter = prop.GetMethod == null ? null : new MethodModel(prop.GetMethod);
            Setter = prop.SetMethod == null ? null : new MethodModel(prop.SetMethod);
            Type = typeModel;
            Attributes = prop.GetCustomAttributes(false).Select(x => TypeModel.GetType(x.GetType())).ToList();
            Access = SetAccessLevel();
        }

        private AccessLevel SetAccessLevel()
        {
            if(Setter != null && Getter != null)
                return Setter.Modifiers.Item1 < Getter.Modifiers.Item1 ? Setter.Modifiers.Item1 : Getter.Modifiers.Item1;
            else
                return Setter == null ? Getter.Modifiers.Item1 : Setter.Modifiers.Item1;
        }

        public static IEnumerable<PropertyModel> EmitProperties(PropertyInfo[] props)
        {
            return from prop in props
                //where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                select new PropertyModel(prop, TypeModel.GetType(prop.PropertyType));
        }

        public override string ToString()
        {
            return Access + " " +  Type.Name + " " + Name;
        }

        public PropertyModel()
        {
            
        }
    }
}