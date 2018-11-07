using System;
using System.IO;
using System.Threading;
using ProjektTPA.Lib.Utility;

namespace ProjektTPA.CommandLine
{
    public class DataProvider : IDataProvider
    {
        public string GetPath()
        {
            while (true)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                Console.Write("Enter file name: " + path);
                path = path + Console.ReadLine();

                if (File.Exists(path))
                    return path;
                else
                {
                    Console.WriteLine("There is on such file");
                    Thread.Sleep(300);
                }
            }
        }
    }
}