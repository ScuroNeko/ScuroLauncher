namespace ScuroLogger.Handlers;

public class ConsoleHandler : Handler
{
    public override void Log(LogLevels level, string msg)
    {
        Console.WriteLine(msg);
    }
}