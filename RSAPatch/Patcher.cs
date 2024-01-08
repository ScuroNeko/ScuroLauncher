using ScuroLogger;
using ScuroLogger.Handlers;

namespace RSAPatch;

public class Patcher
{
    public static Logger Logger;

    public Patcher()
    {
        Logger = new Logger("RSAPatch");
        var consoleHandler = new ConsoleHandler();
        Logger.AddHandler(consoleHandler);
        
        Logger.Info("RSAPatcher loaded");
    }

    public void PatchGenshin40()
    {
        Genshin.Patch("Release 3.1.0");
    }
}