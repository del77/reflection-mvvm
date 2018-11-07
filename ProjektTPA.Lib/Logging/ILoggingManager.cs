using System.Diagnostics;

namespace ProjektTPA.Lib.Logging
{
    public interface ILoggingManager
    {
        void Log(string message);
        void Log(string message, TraceLevel level);
    }
}