using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using DtoLayer;
using MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapper = BusinessLogic.Mapper;

namespace BusinessLogicTests
{
    [TestClass]
    public class MappingTests
    {
        private static string path =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\test.dll"));
        IReflector reflector = new Reflector();

        [TestMethod]
        public void CircuralReferenceTest()
        {
            reflector.Reflect(path);
            IMapper _mapper = Mapper.Initialize();

            AssemblyModel assemblyModel = reflector.AssemblyModel;
            AssemblyDto assemblyDto = _mapper.Map<AssemblyDto>(assemblyModel);

            NamespaceModel circuralNamespaceModel = assemblyModel.Namespaces[1].Namespaces[0];
            NamespaceDto circuralNamespaceDto = assemblyDto.Namespaces[1].Namespaces[0];

            TypeModel typeModel_A = circuralNamespaceModel.Types[0];
            TypeModel typemodel_A_ref = circuralNamespaceModel.Types[1].Fields[0].TypeModel;

            TypeModel typeModel_B = circuralNamespaceModel.Types[1];

            TypeDto typeDto_A = circuralNamespaceDto.Types[0];
            TypeDto typeDto_A_ref = circuralNamespaceDto.Types[1].Fields[0].TypeModel;

            TypeDto typeDto_B = circuralNamespaceDto.Types[1];


            Assert.AreEqual(typeModel_A.Name, typemodel_A_ref.Name);
            Assert.AreSame(typeModel_A, typemodel_A_ref);

            Assert.AreNotEqual(typeModel_A.Name, typeModel_B.Name);
            Assert.AreNotSame(typeModel_A, typeModel_B);



            Assert.AreEqual(typeDto_A.Name, typeDto_A_ref.Name);
            Assert.AreSame(typeDto_A, typeDto_A_ref);

            Assert.AreNotEqual(typeDto_A.Name, typeDto_B.Name);
            Assert.AreNotSame(typeDto_A, typeDto_B);
        }
    }
}
