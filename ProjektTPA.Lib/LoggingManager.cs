using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using BusinessLogic.Interfaces;
using MEF;

namespace BusinessLogic
{
    [Export(typeof(ILoggingManager))]
    public class LoggingManager : ILoggingManager
    {
        [Import(typeof(ILogger))]
        public ILogger logger;

        private TraceLevel maxLevel;

        public LoggingManager()
        {
            NameValueCollection loggingSettings = ConfigurationManager.AppSettings;
            Enum.TryParse(loggingSettings["level"], out maxLevel);
        }

        public void Log(string message, TraceLevel level = TraceLevel.Info)
        {
            if (level > maxLevel)
                return;
            logger.Log(message, level);
        }

    }
}