using System;
using Pumpkin.Utils.Helpers;

namespace Pumpkin.Contract.Logging
{
    public static class LogManager
    {
        private static Lazy<ILoggerFactory> _loggerFactory;

        public static T Use<T>() where T : LoggingFactoryDefinition, new()
        {
            var loggingDefinition = new T();

            _loggerFactory = new Lazy<ILoggerFactory>(loggingDefinition.GetLoggingFactory);

            return loggingDefinition;
        }

        public static void UseFactory(ILoggerFactory loggerFactory)
        {
            Guard.AgainstNull(nameof(loggerFactory), loggerFactory);

            _loggerFactory = new Lazy<ILoggerFactory>(() => loggerFactory);
        }

        public static ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        private static ILog GetLogger(Type type)
        {
            try
            {
                return _loggerFactory.Value.GetLogger(type.FullName);
            }
            catch (Exception)
            {
                return new EmptyLogger();
            }
        }
    }
}