using System.Runtime.CompilerServices;
using ScuroLogger.Handlers;

namespace ScuroLogger;

public class Logger(string? name = null)
{
    private readonly List<Handler> _handlers = [];

    public string DateTimeFormat = "hh:mm:ss dd.MM.yyyy";

    // %level - level; %ts - timedate; %name - name; %msg - message
    public string Format = "[%ts] [%level] [%name]: %msg";
    public string? Name = name ?? "ScuroLogger";

    private string FormatOutput(LogLevels level, string msg)
    {
        var formattedTime = DateTime.Now.ToString(DateTimeFormat);
        return Format.Replace("%ts", formattedTime)
            .Replace("%level", ColoredLevels.GetColored(level))
            .Replace("%name", Name)
            .Replace("%msg", msg);
    }

    public void AddHandler(Handler handler)
    {
        _handlers.Add(handler);
    }

    public void Info(string msg)
    {
        Log(LogLevels.Info, msg);
    }

    public void Warn(string msg)
    {
        Log(LogLevels.Warn, msg);
    }

    public void Debug(string msg)
    {
        Log(LogLevels.Debug, msg);
    }

    public void Error(string msg)
    {
        Log(LogLevels.Error, msg);
    }

    public void Fatal(string msg)
    {
        Log(LogLevels.Fatal, msg);
    }

    public void Log(LogLevels level, string msg)
    {
        _handlers.ForEach(handler =>
        {
            if (handler.LogLevels >= level)
                handler.Log(level, FormatOutput(level, msg));
        });
    }
}