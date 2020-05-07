using System;

namespace Pumpkin.Contract.Logging
{
    public interface ILoggerFactory
    {
        ILog GetLogger(string name, Type type);

        ILog GetLogger(string name);
    }
}