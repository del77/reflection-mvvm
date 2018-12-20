using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using ProjektTPA.Lib.Logging;
using ViewModel.Interfaces;

namespace ViewModel.ViewModel.Logging
{
    [Export(typeof(ILoggingManager))]
    public class LoggingManager : ILoggingManager
    {
        [Import(typeof(ILogger))]
        public ILogger logger;

        private TraceLevel maxLevel;

        public LoggingManager()
        {
            //NameValueCollection loggingConfiguration = ConfigurationManager.GetSection("logging") as NameValueCollection;
            var loggingSettings = ConfigurationManager.AppSettings;
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