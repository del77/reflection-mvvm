using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Ninject;
using ProjektTPA.Lib.Extensions;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.Model
{
    [DataContract(IsReference = true)]
    public class TypeModel : Model
    {
        public static Dictionary<string, TypeModel> LoadedTypes = new Dictionary<string, TypeModel>();

        public TypeModel(Type type) : base(type.Name)
        { }
        

        public static TypeModel GetTypeWithDetails(Type type)
        {
            TypeModel typeModel = GetType(type);
            if (typeModel.Resolved)
                return typeModel;
            typeModel.Resolved = true;
            typeModel.DeclaringType = GetType(type.DeclaringType);
            typeModel.Constructors = MethodModel.EmitMethods(type.GetConstructors(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)).ToList();
            typeModel.Methods = MethodModel.EmitMethods(type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)).ToList();
            typeModel.NestedTypes = GetTypes(type.GetNestedTypes().Where(x => x.GetVisible()));
            typeModel.ImplementedInterfaces = GetTypes(type.GetInterfaces());
            typeModel.GenericArguments = type.IsGenericTypeDefinition ? TypeModel.GetTypesWithDetails(type.GetGenericArguments()) : null;
            if(typeModel.GenericArguments != null)
            {
                foreach (var argument in typeModel.GenericArguments)
                {
                    argument.IsGeneric = true;
                }
            }
            typeModel.Modifiers = typeModel.EmitModifiers(type);
            typeModel.BaseType = typeModel.EmitExtends(type.BaseType);
            typeModel.Properties = PropertyModel.EmitProperties(type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)).ToList();
            typeModel.TypeKind = GetTypeKind(type);
            typeModel.Fields = typeModel.EmitFields(type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance));
            typeModel.Attributes = type.GetCustomAttributes(false).Select(x => GetType(x.GetType())).ToList();

            return typeModel;
        }
        public bool IsGeneric { get; set; }
        private List<FieldModel> EmitFields(IEnumerable<FieldInfo> fields)
        {
            return (from field in fields
                select new FieldModel(field.GetCustomAttributes(false), field.Name, GetType(field.FieldType))).ToList();
            //select new FieldModel(field.GetCustomAttributes(false), field.Name, GetTypeWithDetails(field.FieldType))).ToList();
        }

        private static TypeKind GetTypeKind(Type type) //#80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.@enum :
                type.IsValueType ? TypeKind.@struct :
                type.IsInterface ? TypeKind.@interface :
                TypeKind.@class;
        }

        private TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return GetType(baseType);
        }

        private Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevel _access = AccessLevel.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;
            // check if not default 
            if (type.IsPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedFamily)
                _access = AccessLevel.Protected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.Internal;
            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }

        public static TypeModel GetType(Type type)
        {
            if (type == null)
                return null;

            LoadedTypes.TryGetValue(type.Name, out TypeModel typeModel);
            
            if(typeModel == null)
            {
                typeModel = new TypeModel(type);
                LoadedTypes.Add(typeModel.Name, typeModel);
            }

            return typeModel;
        }

        public static List<TypeModel> GetTypes(IEnumerable<Type> types)
        {
            List<TypeModel> typeModels = new List<TypeModel>();
            foreach (var type in types)
            {
                typeModels.Add(GetType(type));
            }

            return typeModels.ToList();
        }

        public static List<TypeModel> GetTypesWithDetails(IEnumerable<Type> types)
        {
            List<TypeModel> typeModels = new List<TypeModel>();
            foreach (var type in types)
            {
                typeModels.Add(GetTypeWithDetails(type));
            }

            return typeModels.ToList();
        }


        private TypeModel(string name, string namespaceName) : base(name)
        {
            NamespaceName = namespaceName;
        }

        private TypeModel(string name, string namespaceName, IEnumerable<TypeModel> genergicArguments) : this(name,
            namespaceName)
        {
            GenericArguments = genergicArguments.ToList();
        }
        [DataMember]
        public TypeModel DeclaringType { get; set; }
        [DataMember]
        public List<MethodModel> Constructors { get; set; }
        [DataMember]
        public List<MethodModel> Methods { get; set; }
        [DataMember]
        public List<TypeModel> NestedTypes { get; set; }
        [DataMember]
        public List<TypeModel> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }
        [DataMember]
        public List<FieldModel> Fields { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        [DataMember]
        public TypeModel BaseType { get; set; }
        [DataMember]
        public List<PropertyModel> Properties { get; set; }
        [DataMember]
        public TypeKind TypeKind { get; set; }
        [DataMember]
        public List<TypeModel> Attributes { get; set; }
        [DataMember]
        public string NamespaceName;
        [DataMember]
        public bool Resolved { get; set; } = false;
    }
    
}