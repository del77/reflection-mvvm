using System.Diagnostics;

namespace ProjektTPA.Lib.Logging
{
    public interface ILogger
    {
        void Log(string message, TraceLevel level);
    }
}