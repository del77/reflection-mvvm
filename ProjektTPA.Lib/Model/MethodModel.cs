﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using ProjektTPA.Lib.Extensions;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.Model
{
    [DataContract(IsReference = true)]
    public class MethodModel : Model
    {
        public MethodModel(MethodBase method) : base(method.Name)
        {
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
                select new FieldModel(parm.GetCustomAttributes(false), parm.Name, TypeModel.GetTypeWithDetails(parm.ParameterType));
        }

        private TypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeModel.GetType(methodInfo.ReturnType);
        }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        [DataMember]
        public List<FieldModel> Parameters { get; set; }
        [DataMember]
        public TypeModel ReturnType { get; set; }
        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }
        [DataMember]
        public List<TypeModel> Attributes { get; set; }

        public static IEnumerable<MethodModel> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                select new MethodModel(currentMethod);
        }
    }
}