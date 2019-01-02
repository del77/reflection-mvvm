using System.Diagnostics;

namespace MEF
{
    public interface ILogger
    {
        void Log(string message, TraceLevel level);
    }
}