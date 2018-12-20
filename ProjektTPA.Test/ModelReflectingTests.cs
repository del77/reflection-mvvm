using System;
using System.Linq;
using System.Threading;
using ProjektTPA.Lib;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Utility;
using Xunit;

namespace ProjektTPA.Test
{
    public class ModelReflectingTests
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory + "TestLibrary.dll";
        IReflector reflector = new Reflector();

        [Fact]
        public void Should_Not_Create_Multiple_ModelTypes_For_One_Type()
        {
            Assert.Empty(TypeModel.LoadedTypes);
            TypeModel typeModel = TypeModel.GetType(typeof(test1));
            Assert.Equal(1, TypeModel.LoadedTypes.Count);
            TypeModel typeModel2 = TypeModel.GetType(typeof(test1));
            Assert.Equal(1, TypeModel.LoadedTypes.Count);
        }

        [Fact]
        public void Should_Create_Placeholders_Types_When_Getting_Type_Details()
        {
            TypeModel typeModel = TypeModel.GetTypeWithDetails(typeof(test2));
            Assert.Equal(3, TypeModel.LoadedTypes.Count);
        }

        [Fact]
        public void Should_Get_1_Namespace_From_Library()
        {
            reflector.Reflect(path);
            Assert.Equal(1, reflector.AssemblyModel.NamespaceModels.Count);
        }

        [Fact]
        public void Should_Get_5_Classess_From_Library()
        {
            reflector.Reflect(path);
            Assert.Equal(5, reflector.AssemblyModel.NamespaceModels.ElementAt(0).Types.Count());
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


