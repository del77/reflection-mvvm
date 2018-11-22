using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ProjektTPA.Lib.Logging
{
    [Export(typeof(ILoggingManager))]
    public class LoggingManager : ILoggingManager
    {
        public LoggingManager()
        {
            Trace.AutoFlush = true;
        }

        public void Log(string message)
        {
            Log(message, TraceLevel.Info);
        }

        public void Log(string message, TraceLevel level)
        {
            Trace.WriteLine(message, level.ToString());
        }
    }
}