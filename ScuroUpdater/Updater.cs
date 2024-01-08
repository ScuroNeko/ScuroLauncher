using System.Diagnostics;
using ScuroLogger;
using ScuroLogger.Handlers;

namespace ScuroUpdater;

public class UpdateInfo
{
    public bool HasUpdate { get; set; } = false;
    public string NewVersion { get; set; } = "";
}

public class Updater
{
    private static string hpatchz = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hpatchz.exe");
    internal static Logger Logger;
    
    public Updater()
    {
        Logger = new Logger("ScuroUpdater");
        var consoleHandler = new ConsoleHandler();
        Logger.AddHandler(consoleHandler);
    }

    public static void CheckHdiff(string hdiff)
    {
        var info = new ProcessStartInfo
        {
            FileName = "hpatchz.exe",
            Arguments = $"-info {hdiff}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        var process = Process.Start(info);
        if (process == null)
        {
            Updater.Logger.Fatal("Process can't start");
            throw new Exception();
        }

        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            if(line != null) Updater.Logger.Info(line);
        }
    }
}