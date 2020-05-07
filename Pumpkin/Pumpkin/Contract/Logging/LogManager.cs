using System;
using Pumpkin.Utils.Helpers;

namespace Pumpkin.Contract.Logging
{
    public static class LogManager
    {
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

        public static ILog GetLogger(Type type)
        {
            try
            {
                Guard.AgainstNull(nameof(type), type);
                
                return _loggerFactory.Value.GetLogger(nameof(type),type);
            }
            catch (Exception)
            {
                return new EmptyLogger();
            }
        }

        public static ILog GetLogger(string name)
        {
            try
            {
                Guard.AgainstNullAndEmpty(nameof(name), name);
                
                return _loggerFactory.Value.GetLogger(name);
            }
            catch (Exception)
            {

                return new EmptyLogger();
            }
        }

        private static Lazy<ILoggerFactory> _loggerFactory;
    }
}