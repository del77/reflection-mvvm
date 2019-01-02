using System;
using System.IO;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicTests
{
    [TestClass]
    public class ReflectorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

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
        public void Should_Create_Placeholders_Types_When_Getting_Type_Details()
        {
            TypeModel typeModel = TypeModel.GetTypeWithDetails(typeof(test2));
            Assert.AreEqual(3, TypeModel.LoadedTypes.Count);
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
        class test1
        {

        }

        class test2
        {
            test1 test1 = new test1();
        }
    }
}
