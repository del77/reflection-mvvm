using System.Diagnostics;

namespace BusinessLogic.Interfaces
{
    public interface ILoggingManager
    {
        void Log(string message, TraceLevel level);
    }
}