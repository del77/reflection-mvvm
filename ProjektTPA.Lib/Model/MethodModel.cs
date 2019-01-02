using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using DtoLayer.Enums;

namespace BusinessLogic.Model
{
    public class MethodModel
    {
        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = method.IsGenericMethodDefinition ? TypeModel.GetTypes(method.GetGenericArguments()) : null;
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters()).ToList();
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
            Attributes = EmitAttributes(method).ToList();
        }

        private IEnumerable<TypeModel> EmitAttributes(MethodBase method)
        {
            List<TypeModel> attributes = new List<TypeModel>();
            foreach (var methodAttribute in method.GetCustomAttributes(false))
            {
                attributes.Add(TypeModel.GetType(methodAttribute.GetType()));
            }

            return attributes;
        }

        private bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel _access = AccessLevel.Private;
            if (method.IsPublic)
                _access = AccessLevel.Public;
            else if (method.IsFamily)
                _access = AccessLevel.Protected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevel.Internal;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            else if (method.IsFinal)
                _abstract = AbstractEnum.Sealed;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;
            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }

        private IEnumerable<FieldModel> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                select new FieldModel(parm.GetCustomAttributes(false), parm.Name, TypeModel.GetType(parm.ParameterType));
        }

        private TypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeModel.GetType(methodInfo.ReturnType);
        }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public List<FieldModel> Parameters { get; set; }
        public TypeModel ReturnType { get; set; }
        public List<TypeModel> GenericArguments { get; set; }
        public List<TypeModel> Attributes { get; set; }

        public static IEnumerable<MethodModel> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                select new MethodModel(currentMethod);
        }

        public override string ToString()
        {
            if (Modifiers == null)
                return Name;
            StringBuilder builder = new StringBuilder();
            builder.Append(Modifiers.Item1);
            if (Modifiers.Item2 != AbstractEnum.NotAbstract)
                builder.Append(" ").Append(Modifiers.Item2);
            if (Modifiers.Item3 == StaticEnum.Static)
                builder.Append(" ").Append(Modifiers.Item3);
            if (Modifiers.Item4 == VirtualEnum.Virtual)
                builder.Append(" ").Append(VirtualEnum.Virtual);


            if (ReturnType != null)
                builder.Append(" ").Append(ReturnType.Name).Append(" ");
            builder.Append(Name).Append("(");

            for (int i = 0; i < Parameters.Count() - 1; i++)
            {
                var parm = Parameters.ElementAt(i);
                builder.Append(parm.TypeModel.Name).Append(" ").Append(parm.Name).Append(", ");
            }
            if (Parameters.LastOrDefault() != null)
                builder.Append(Parameters.Last().TypeModel.Name).Append(" ").Append(Parameters.Last().Name);


            builder.Append(")");


            return builder.ToString();
        }

        public MethodModel()
        {
            
        }
    }
}
