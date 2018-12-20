using System.Diagnostics;

namespace ViewModel.Interfaces
{
    public interface ILoggingManager
    {
        void Log(string message, TraceLevel level);
    }
}