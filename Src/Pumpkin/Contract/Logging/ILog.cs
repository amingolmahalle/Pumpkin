namespace Pumpkin.Contract.Logging;

public interface ILog
{
    bool IsDebugEnabled { get; }

    bool IsInfoEnabled { get; }

    bool IsWarnEnabled { get; }

    bool IsErrorEnabled { get; }

    bool IsFatalEnabled { get; }

    void Debug(string message);

    void Info(string message);

    void Warn(string message);

    void Warn(string message, Exception exception);

    void Error(string message);

    void Error(string message, Exception exception);

    void Fatal(string message);

    void Fatal(string message, Exception exception);
}