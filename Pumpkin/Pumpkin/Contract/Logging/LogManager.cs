using System;

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

        public static ILog GetLogger<T>()
        {
            try
            {
                return _loggerFactory.Value.GetLogger(typeof(T).FullName);
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
                return _loggerFactory.Value.GetLogger(name);
            }
            catch (Exception)
            {
                return new EmptyLogger();
            }
        }
    }
}