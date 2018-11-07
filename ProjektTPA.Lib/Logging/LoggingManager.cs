using System.Diagnostics;

namespace ProjektTPA.Lib.Logging
{
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