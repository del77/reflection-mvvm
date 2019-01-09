using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using DtoLayer.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicTests
{
    [TestClass]
    public class ReflectorTests
    {

        private static string path =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\test.dll"));
        IReflector reflector = new Reflector();


        [TestMethod]
        public void Should_Not_Create_Multiple_ModelTypes_For_One_Type()
        {
            Assert.AreEqual(0, TypeModel.LoadedTypes.Count);
            TypeModel typeModel = TypeModel.GetType(typeof(test1));
            Assert.AreEqual(1, TypeModel.LoadedTypes.Count);
            TypeModel typeModel2 = TypeModel.GetType(typeof(test1));
            Assert.AreEqual(1, TypeModel.LoadedTypes.Count);
        }

        [TestMethod]
        public void Should_Not_Create_New_Objects_For_Circural_References()
        {
            reflector.Reflect(path);
            TypeModel type1 = reflector.AssemblyModel.Namespaces[1].Namespaces[0].Types[0];
            TypeModel type1_ref = reflector.AssemblyModel.Namespaces[1].Namespaces[0].Types[1].Properties[0].Type;
            Assert.AreSame(type1,type1_ref);
        }

        [TestMethod]
        public void Should_Get_3_Namespaces_From_Library()
        {
            reflector.Reflect(path);
            Assert.AreEqual(3, reflector.AssemblyModel.Namespaces.Count);
        }

        [TestMethod]
        public void Should_Get_1_Class_From_Library()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces.ElementAt(0).Types.Count());
        }

        [TestMethod]
        public void AbstractClass_Should_Have_1_Abstract_Method()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces[1].Types[0].Methods.Count(x => x.Modifiers.Item2 == AbstractEnum.Abstract));
        }

        [TestMethod]
        public void ClassWithAttribute_Should_Have_1_Class_Attribute_And_1_Field_Attribute()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces[1].Types[1].Attributes.Count);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces[1].Types[1].Fields[0].Attributes.Count);
        }

        [TestMethod]
        public void EnumClass_Should_Have_2_Enum_Values()
        {
            reflector.Reflect(path);
            Assert.AreEqual(2, reflector.AssemblyModel.Namespaces[1].Types[3].Fields.Count(x => x.TypeModel.Name == "Enum"));
        }

        [TestMethod]
        public void GenericClass_Should_Have_Generic_Argument_Of_Type_T()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces[1].Types[4].GenericArguments.Count());
            Assert.AreEqual("T", reflector.AssemblyModel.Namespaces[1].Types[4].GenericArguments[0].Name);
        }


        [TestMethod]
        public void Interface_Should_Have_2_Abstract_Virtual_Methods()
        {
            reflector.Reflect(path);
            Assert.AreEqual(2,
                reflector.AssemblyModel.Namespaces[1].Types[5].Methods.Count(x =>
                    x.Modifiers.Item2 == AbstractEnum.Abstract && x.Modifiers.Item4 == VirtualEnum.Virtual));
        }

        [TestMethod]
        public void Implementation_Of_Interface_Should_Have_2_Methods()
        {
            reflector.Reflect(path);
            Assert.AreEqual(2,
                reflector.AssemblyModel.Namespaces[1].Types[6].Methods.Count());
        }

        [TestMethod]
        public void OuterClass_Should_Have_1_Inner_Class()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1,reflector.AssemblyModel.Namespaces[1].Types[8].NestedTypes.Count());
        }

        [TestMethod]
        public void StaticClass_Modifiers_Should_All_Be_Static()
        {
            reflector.Reflect(path);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces[1].Types[8].NestedTypes.Count());
        }

        [TestMethod]
        public void Structure_Should_Value_Type()
        {
            reflector.Reflect(path);
            //Assert.
        }

        class test1
        {

        }

        class test2
        {
            test1 test1 = new test1();
        }
    }
}
