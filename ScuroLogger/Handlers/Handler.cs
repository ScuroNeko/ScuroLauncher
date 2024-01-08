namespace ScuroLogger.Handlers;

public abstract class Handler
{
    public LogLevels LogLevels = LogLevels.Info;
    public abstract void Log(LogLevels level, string msg);
}