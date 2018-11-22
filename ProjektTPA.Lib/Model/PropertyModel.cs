﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using ProjektTPA.Lib.Extensions;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.Model
{
    [DataContract(IsReference = true)]
    public class PropertyModel : Model
    {
        public TypeModel TypeModel { get; set; }

        public PropertyModel(string name) : base(name)
        {
        }

        [DataMember]
        public List<TypeModel> Attributes { get; set; }
        [DataMember]
        public AccessLevel Access { get; set; }
        [DataMember]
        public MethodModel Getter { get; set; }
        [DataMember]
        public MethodModel Setter { get; set; }
        [DataMember]
        public TypeModel Type { get; set; }


        private PropertyModel(PropertyInfo prop, TypeModel typeModel) : this(prop.Name)
        {
            TypeModel = typeModel;
            Getter = prop.GetMethod == null ? null : new MethodModel(prop.GetMethod);
            Setter = prop.SetMethod == null ? null : new MethodModel(prop.SetMethod);
            Access = SetAccessLevel();
            Type = Getter.ReturnType;
            Attributes = prop.GetCustomAttributes(false).Select(x => TypeModel.GetType(x.GetType())).ToList();
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
                where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                select new PropertyModel(prop, TypeModel.GetType(prop.PropertyType));
        }
    }
}