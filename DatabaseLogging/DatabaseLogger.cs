using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLogging;
using MEF;

namespace DatabaseLogging
{
    [Export(typeof(ILogger))]
    public class DatabaseLogger : ILogger
    {
        public LoggingContext Context { get; set; }

        public DatabaseLogger()
        {
            Context = new LoggingContext();
        }

        public void Log(string message, TraceLevel level)
        {
            Log log = new Log
            {
                Message = message,
                Date = DateTime.Now,
                Level = level.ToString()
            };
            Context.Logs.Add(log);
            Context.SaveChanges();
        }

    }
}
