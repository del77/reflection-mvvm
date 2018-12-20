using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using ProjektTPA.Lib.Logging;

namespace FileLogging
{
    [Export(typeof(ILogger))]
    //[ExportMetadata("Logging", "Database")]
    public class FileLogger : ILogger
    {
        private string fileName;
        public FileLogger()
        {
            var loggingSettings = ConfigurationManager.AppSettings;
            fileName = loggingSettings["fileName"];
        }

        public void Log(string message, TraceLevel level)
        {
            using (StreamWriter writer = File.AppendText(fileName+".log"))
            {
                writer.WriteLine(message + "\t" + level.ToString() + "\t" + DateTime.Now);
            }
        }
    }
}