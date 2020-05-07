using System;
using Pumpkin.Contract.Logging;
using LogManager = NLog.LogManager;

namespace Pumpkin.Core.Logging.NLog
{
    internal class LoggerFactory : ILoggerFactory
    {
        public ILog GetLogger(string name, Type type)
        {
            var logger = LogManager.GetLogger(name,type);
            return new Logger(logger);
        }

        public ILog GetLogger(string name)
        {
            var logger = LogManager.GetLogger(name);
            return new Logger(logger);
        }
    }
}
