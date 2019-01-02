using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileTests
{
    [TestClass]
    public class FileSerializerTests
    {
        private static string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\test.dll"));
        private static ModelService modelService = new ModelService();
        private static AssemblyModel deserialized;
        private static AssemblyModel reflectedModel;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(FileSerializer.FileSerializer).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(modelService);
                IReflector reflector = new Reflector();
                reflector.Reflect(path);
                reflectedModel = reflector.AssemblyModel;
                modelService.Save(reflectedModel);
                deserialized = modelService.Load();
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        [TestMethod]
        public void Deserialized_Model_Shouldnt_Create_New_Objects_For_Same_Types()
        {
            TypeModel serviceB = deserialized.Namespaces[2].Types[1];
            TypeModel serviceB_ref = deserialized.Namespaces[2].Types[2].Fields[0].TypeModel;

            Assert.AreSame(serviceB_ref, serviceB);
        }

        [TestMethod]
        public void Check_If_Model_Is_valid()
        {
            Assert.AreEqual(reflectedModel.Name, deserialized.Name);
            Assert.AreEqual(reflectedModel.Namespaces.Count, deserialized.Namespaces.Count);
            Assert.AreEqual(reflectedModel.Namespaces.Sum(x => x.Types.Count), deserialized.Namespaces.Sum(x => x.Types.Count));
        }
    }
}