using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLogging;
using ProjektTPA.Lib.Logging;

namespace DatabaseLogging
{
    [Export(typeof(ILogger))]
    //[ExportMetadata("Logging", "Database")]
    public class DatabaseLogger : ILogger
    {
        [Import(typeof(LoggingContext))]
        public LoggingContext Context { get; set; }
        Log log = new Log();
        //public void Log(string message)
        //{
        //    Log(message, TraceLevel.Info);
        //}

        public void Log(string message, TraceLevel level)
        {
            Log log = new Log
            {
                Message = message,
                Date = DateTime.Now,
            };
            Context.Logs.Add(log);
            Context.SaveChanges();
        }

    }
}
