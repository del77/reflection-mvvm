using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using ProjektTPA.Lib.Utility;

namespace ProjektTPA.CommandLine
{
    [Export(typeof(IDataProvider))]
    public class DataProvider : IDataProvider
    {
        public string GetPath()
        {
            while (true)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                Console.Write("Enter file name: " + path);
                path = path + Console.ReadLine();
                //path += "TestLibrary.dll";

                if (File.Exists(path))
                    return path;
                else
                {
                    Console.WriteLine("There is no such file");
                    Thread.Sleep(300);
                }
            }
        }
    }
}