using System.ComponentModel.Composition;
using System.Diagnostics;
using BusinessLogic.Interfaces;

namespace ViewModelTests
{
    [Export(typeof(ILoggingManager))]
    class Manager : ILoggingManager
    {
        public int messagesSent = 0;
        public void Log(string message, TraceLevel level)
        {
            messagesSent++;
        }
    }
}