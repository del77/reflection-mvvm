using System;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business_Logic
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private string path = AppDomain.CurrentDomain.BaseDirectory + "TestLibrary.dll";
        IReflector reflector = new Reflector();

        [TestMethod]
        public void Should_Not_Create_Multiple_ModelTypes_For_One_Type()
        {
            Assert.Equals(0, TypeModel.LoadedTypes.Count);
            TypeModel typeModel = TypeModel.GetType(typeof(test1));
            Assert.Equals(1, TypeModel.LoadedTypes.Count);
            TypeModel typeModel2 = TypeModel.GetType(typeof(test1));
            Assert.Equals(1, TypeModel.LoadedTypes.Count);
        }

        [TestMethod]
        public void Should_Create_Placeholders_Types_When_Getting_Type_Details()
        {
            TypeModel typeModel = TypeModel.GetTypeWithDetails(typeof(test2));
            Assert.Equals(3, TypeModel.LoadedTypes.Count);
        }

        [TestMethod]
        public void Should_Get_1_Namespace_From_Library()
        {
            reflector.Reflect(path);
            Assert.Equals(1, reflector.AssemblyModel.Namespaces.Count);
        }

        [TestMethod]
        public void Should_Get_5_Classess_From_Library()
        {
            reflector.Reflect(path);
            Assert.Equals(5, reflector.AssemblyModel.Namespaces.ElementAt(0).Types.Count());
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
