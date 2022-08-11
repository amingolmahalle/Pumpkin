namespace Pumpkin.Domain.Framework.Logging;

public class EmptyLogger : ILog
{
    public bool IsDebugEnabled => false;

    public bool IsInfoEnabled => false;

    public bool IsWarnEnabled => false;

    public bool IsErrorEnabled => false;

    public bool IsFatalEnabled => false;

    public void Debug(string message)
    {
    }
        
    public void Error(string message)
    {
    }

    public void Error(string message, Exception exception)
    {
    }

    public void Fatal(string message)
    {
    }

    public void Fatal(string message, Exception exception)
    {
    }

    public void Info(string message)
    {
    }

    public void Warn(string message)
    {
    }

    public void Warn(string message, Exception exception)
    {
    }
}