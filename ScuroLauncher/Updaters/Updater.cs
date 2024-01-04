using System.Diagnostics;

namespace ScuroLauncher.Updaters;

public class Updater
{
    private static string hpatchz = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lib/hpatchz.exe");

    public static void Test()
    {
        var startInfo = new ProcessStartInfo {
            FileName = Updater.hpatchz,
            UseShellExecute = false,
            Arguments = "-info out.bin",
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        var process = Process.Start(startInfo);
        while (!process.StandardOutput.EndOfStream)
        {
            string line = process.StandardOutput.ReadLine();
            Console.WriteLine(line);
        }
    }
}